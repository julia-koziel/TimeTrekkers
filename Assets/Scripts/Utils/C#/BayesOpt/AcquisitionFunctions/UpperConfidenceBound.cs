using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;

namespace BayesOpt.AcquisitionFunctions
{
    using GPR;
    internal class UpperConfidenceBound : AcquisitionFunction
    {
        private double kappa;

        public UpperConfidenceBound(
            GaussianProcessRegressor gp,
            (double min, double max) bounds,
            int resolution,
            double kappa = 2.576
        ) : base(gp, bounds, resolution)
        {
            this.kappa = kappa;
        }

        public UpperConfidenceBound(
            GaussianProcessRegressor gp,
            TargetSpace ts,
            double kappa = 2.576
        ) : base(gp, ts)
        {
            this.kappa = kappa;
        }

        public override double AcqValue(double x)
        {
            // TODO compare to koryakinp and rob
            var res = predict(x, returnStd: true); 
            double mean = res.mean;
            double std = res.covariance;

            return mean + kappa * std;
        }

        public override Vector<double> AcqValue(IEnumerable<double> X)
        {
            var res = _gp.predict(X, returnStd: true);
            mean = res.mean;
            std = res.std;
            
            return mean + kappa * std;
        }
    }
}