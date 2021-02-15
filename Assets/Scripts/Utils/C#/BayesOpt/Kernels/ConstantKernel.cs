namespace BayesOpt.Kernels
{
    public class ConstantKernel : Kernel
    { 
        private double constantValue { get { return hyperparameters[0].value; } }
        public ConstantKernel(
            double constantValue = 1,
            double constantValueMin = 1e-5,
            double constantValueMax = 1e5,
            bool isFixed = false
        ) {
            hyperparameters = new Hyperparameter[1];
            hyperparameters[0] = new Hyperparameter("constantValue", constantValue, constantValueMin, constantValueMax, isFixed);
        }
        internal override double Compute(double left, double right) 
        {
            return constantValue;
        }
    }
}