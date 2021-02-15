using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;

namespace BayesOpt.AcquisitionFunctions
{
    using GPR;
    internal class ExpectedImprovement : AcquisitionFunction
    {
        private double xi;
        private Normal norm;

        public ExpectedImprovement(
            GaussianProcessRegressor gp, (double min, double max) bounds, int resolution,
            double xi = 0
        ) : base(gp, bounds, resolution)
        {
            init(xi);
        }

        public ExpectedImprovement(
            GaussianProcessRegressor gp, TargetSpace ts,
            double xi = 0
        ) : base(gp, ts)
        {
            init(xi);
        }

        private void init(double xi)
        {
            this.xi = xi;
            norm = new Normal();
        }

        public override double AcqValue(double x)
        {
            var res = predict(x, returnStd: true);
            double mean = res.mean;
            double std = res.covariance;
            double yMax = this.yMax;

            double z = (mean - yMax - xi) / std;
            return (mean - yMax - xi) * norm.CumulativeDistribution(z) + std * norm.Density(z);
        }

        public override Vector<double> AcqValue(IEnumerable<double> X)
        {
            var res = _gp.predict(X, returnStd: true);
            mean = res.mean;
            std = res.std;
            
            var z = (mean - yMax - xi).PointwiseDivide(std);
            var cdf = z.Map<double>(norm.CumulativeDistribution, Zeros.Include);
            var pdf = z.Map<double>(norm.Density, Zeros.Include);
            return (mean - yMax - xi).PointwiseMultiply(cdf) + std.PointwiseMultiply(pdf);
        }
    }
}