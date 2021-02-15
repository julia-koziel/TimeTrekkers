namespace BayesOpt.Kernels
{
    public class Hyperparameter
    {
        public string name;
        public double value;
        public (double min, double max) bounds;
        public bool isFixed;

        public Hyperparameter(string name, double value,
                              double min = 0, double max = 0,
                              bool isFixed = false)
        {
            this.name = name;
            this.value = value;
            this.isFixed = isFixed || min == max;
            bounds.min = min;
            bounds.max = max;
        }
        // TODO does this need an equals method
    }
}