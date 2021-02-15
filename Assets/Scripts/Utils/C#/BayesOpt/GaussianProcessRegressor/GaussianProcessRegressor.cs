using BayesOpt.Kernels;
using BayesOpt.Utils;
using BayesOpt.Optimisers;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.Distributions;
using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BayesOpt.GPR
{
    public class GaussianProcessRegressor
    {
        public Kernel kernel;
        private Matrix<double> K;
        // private Matrix<double> K_inv;
        private double alpha;
        private bool optimizer;
        private int optResolution;
        // nRestartsOptimizer
        private bool normalizeY;
        // random state (int?)
        private List<double> xTrain;
        private List<double> yTrain;
        private double yTrainMean;
        private Vector<double> yTrainNormalized;
        private Cholesky<double> cho;
        private Vector<double> alpha_;
        public double logMarginalLikelihoodValue;
        VectorBuilder<double> V = Vector<double>.Build;
        MatrixBuilder<double> M = Matrix<double>.Build;
        public GaussianProcessRegressor(
            Kernel kernel = null,
            bool normalizeY = false,
            double alpha = 1e-5,
            bool optimizer = true,
            int optResolution = 100
        ) {
            // TEST speed
            this.kernel = kernel ?? new RBF(lenScale: 1) * new ConstantKernel(constantValue: 1); // default kernel if none supplied
            this.normalizeY = normalizeY;
            this.alpha = alpha;
            this.optimizer = optimizer;
            this.optResolution = optResolution;
            xTrain = new List<double>();
            yTrain = new List<double>();
            K = M.Dense(1, 1);
        }

        public void fit(IEnumerable<double> X, IEnumerable<double> Y)
        {
            xTrain = (List<double>) X;
            yTrain = (List<double>) Y;
            // TODO review this
            yTrainMean = normalizeY ? yTrain.Average() : 0;
            yTrainNormalized = V.DenseOfEnumerable(yTrain) - yTrainMean;

            if (optimizer && kernel.theta.Length > 0)
            {
                var gs = new GridSearch(logMarginalLikelihood, kernel.bounds, optResolution);
                var res = gs.maximise();
                // TODO does this need to be an instance object?
                kernel.theta = res.thetaOpt;
                logMarginalLikelihoodValue = res.funcResult;
            }

            // updateCovariance(x);
            K = kernel.Compute(xTrain);
            K += M.DenseDiagonal(K.RowCount, K.ColumnCount, alpha);
            // TODO compare denseofenumerable to denseofarray

            try
            {
                cho = K.Cholesky();
                // K_inv = null;
            }
            catch (System.ArgumentException)
            {
                throw new System.ArgumentException(
                    ("The kernel is not returning a positive definite matrix. " +
                     "Try gradually increasing the 'alpha' parameter of your GaussianProcessRegressor estimator.")
                );
            }
            
            alpha_ = cho.Solve(yTrainNormalized);
            logMarginalLikelihoodValue = logMarginalLikelihood(kernel.theta);
        }

        public void fit(double x, double y)
        {
            // TODO allow for array/vector input

            xTrain.Add(x);
            yTrain.Add(y);
            yTrainMean = normalizeY ? yTrain.Average() : 0;
            yTrainNormalized = V.DenseOfEnumerable(yTrain) - yTrainMean;

            if (optimizer && kernel.theta.Length > 0)
            {
                var gs = new GridSearch(logMarginalLikelihood, kernel.bounds, optResolution);
                var res = gs.maximise();
                // TODO does this need to be an instance object?
                kernel.theta = res.thetaOpt;
                logMarginalLikelihoodValue = res.funcResult;
            }

            // updateCovariance(x);
            K = kernel.Compute(xTrain);
            K += M.DenseDiagonal(K.RowCount, K.ColumnCount, alpha);
            // TODO compare denseofenumerable to denseofarray

            try
            {
                cho = K.Cholesky();
                // K_inv = null;
            }
            catch (System.ArgumentException)
            {
                throw new System.ArgumentException(
                    ("The kernel is not returning a positive definite matrix. " +
                     "Try gradually increasing the 'alpha' parameter of your GaussianProcessRegressor estimator.")
                );
            }
            
            alpha_ = cho.Solve(yTrainNormalized);
            logMarginalLikelihoodValue = logMarginalLikelihood(kernel.theta);
        }

        public (double mean, double covariance) predict(double xQuery, bool returnStd = false)
        {
            if (xTrain.Count == 0)
            {
                if (returnStd)
                    return (mean: 0, covariance: Math.Sqrt(kernel.Compute(xQuery)));
                return (mean: 0, covariance: kernel.Compute(xQuery));
            }

            double[] kstarArray = xTrain.ToArray();
            kstarArray.ForEach(x => kernel.Compute(xQuery, x));
            var kstar = V.DenseOfArray(kstarArray);

            double mean = kstar.DotProduct(alpha_);
            mean = normalizeY ? mean + yTrainMean : mean;

            if (returnStd)
            {
                // if (K_inv == null)
                //     K_inv = cho.Solve(Matrix<double>.Build.DenseIdentity(this.covariance.RowCount()));
                var v = cho.Solve(kstar);
                double covariance = Math.Sqrt(kernel.Compute(xQuery, xQuery) - kstar.DotProduct(v));
                covariance = covariance < 0 ? 0 : covariance;
                
                return (mean, covariance);
            }
            else
            {
                var v = cho.Solve(kstar);
                double covariance = kernel.Compute(xQuery, xQuery) - kstar.DotProduct(v);
                
                return (mean, covariance);
            }            
        }

        // public (double[] mean, double[] covariance) predict(double[] xQueries, bool returnStd = false)
        // {
        //     double[] mean = new double[xQueries.Length];
        //     double[] covariance = new double[xQueries.Length];

        //     for (int i = 0; i < xQueries.Length; i++)
        //     {
        //         var res = predict(xQueries[i], returnStd);
        //         mean[i] = res.mean;
        //         covariance[i] = res.covariance;
        //     }

        //     return (mean, covariance);
        // }

        public (Vector<double> mean, Matrix<double> cov, Vector<double> std) predict(
            Vector<double> X,
            bool returnStd = false,
            bool returnCov = false
        ) {

            Vector<double> yMean;

            if (xTrain.Count == 0)
            {
                yMean = V.Dense(X.Count);
                
                if (returnCov)
                {
                    return (mean: yMean, cov: kernel.Compute(X), std: null);
                }
                else if (returnStd)
                {
                    var yStd = kernel.Compute(X).Diagonal().PointwiseSqrt();
                    // TODO add diag method to kernels
                    // TODO implement solve-triangular
                    return (mean: yMean, cov: null, std: yStd);
                }
                else
                {
                    return (mean: yMean, null, null);
                }
            }

            var Kstar = kernel.Compute(X, xTrain);
            yMean = Kstar * alpha_;
            yMean += yTrainMean;

            if (returnCov)
            {
                var v = cho.Solve(Kstar.Transpose());
                var cov = kernel.Compute(X) - Kstar * v;
                return (mean: yMean, cov: cov, std: null);
            }
            else if (returnStd)
            {
                var v = cho.Solve(Kstar.Transpose());
                var cov = kernel.Compute(X) - Kstar * v;
                var yVar = cov.Diagonal();
                return (mean: yMean, cov: null, std: yVar.PointwiseSqrt());
            }
            else
            {
                return (mean: yMean, null, null);
            }             
        }

        public (Vector<double> mean, Matrix<double> cov, Vector<double> std) predict(
            IEnumerable<double> X,
            bool returnStd = false,
            bool returnCov = false
        ) {
            return predict(V.DenseOfEnumerable(X), returnStd, returnCov);
        }

        private void updateCovariance(double xNew)
        {
            int size = xTrain.Count;
            var updated = Matrix<double>.Build.Dense(size, size);
            K.ForEach((i, j, q) => updated[i, j] = q);

            for (int i = 0; i < size - 1; i++)
            {
                var value = kernel.Compute(xTrain[i], xNew);
                updated[i, size - 1] = value;
                updated[size - 1, i] = value;
            }

            updated[size - 1, size - 1] = kernel.Compute(xNew) + alpha;
            // updated.MapInplace(q => Math.Round(q, 5));
            K = updated;
        }

        private double logMarginalLikelihood(double[] theta)
        {
            Control.UseSingleThread();
            kernel.theta = theta;
            K = kernel.Compute(xTrain);
            K += M.DenseDiagonal(K.RowCount, K.ColumnCount, alpha);

            try
            {
                cho = K.Cholesky();
            }
            catch (System.ArgumentException)
            {
                return double.NegativeInfinity;
            }

            alpha_ = cho.Solve(yTrainNormalized);

            double lml = -0.5 * yTrainNormalized.DotProduct(alpha_);
            Control.UseSingleThread();
            // TODO switch back
            var diag = cho.Factor.Diagonal();
            var diagLog = diag.Select(x => Math.Log(x));
            lml -= diagLog.Sum();
            lml -= (K.RowCount / 2.0) * Math.Log(2 * Math.PI);
            return lml;
        }

        public (double[] s1, double[] s2, double[] s3) sampleY(double[] X)
        {
            // TODO look at in detail
            var res = predict(X, returnCov: true);
            var cov = res.cov;
            
            Cholesky<double> cho = this.cho;
            var i = 0;
            while (true)
            {
                i++;
                if (i > 100)
                {
                    break;
                }
                try
                {
                    cho = cov.Cholesky();
                    break;
                }
                catch (System.ArgumentException)
                {
                    cov += M.DenseDiagonal(cov.RowCount, cov.ColumnCount, 1e-10);
                }
            }

            double[] s1 = new double[res.mean.Count];
            Normal.Samples(s1, 0, 1);
            double[] s2 = new double[res.mean.Count];
            Normal.Samples(s2, 0, 1);
            double[] s3 = new double[res.mean.Count];
            Normal.Samples(s3, 0, 1);
            
            s1 = (res.mean + cho.Factor * V.DenseOfArray(s1)).AsArray();
            s2 = (res.mean + cho.Factor * V.DenseOfArray(s2)).AsArray();
            s3 = (res.mean + cho.Factor * V.DenseOfArray(s3)).AsArray();
            
            return (s1, s2, s3);
        }

        // constrainedOptimization...
        public void setParams(
            Kernel kernel = null,
            double? alpha = null,
            bool? normalizeY = null
        )
        {
            if (kernel != null)
                this.kernel = kernel;
            
            if (alpha is double alphaValue)
                this.alpha = alphaValue;
            
            if (normalizeY is bool shouldNormalizeY)
                this.normalizeY = shouldNormalizeY;
                
            // optimizer
            // nRestartsOptimizer
            // random state (int?)
        }
    }
}