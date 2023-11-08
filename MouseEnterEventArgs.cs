namespace FrequencyDomainCanvas
{
    public class MouseEnterEventArgs
    {
        public int Frequency { get; private set; }
        public double Value { get; private set; }
        public MouseEnterEventArgs(int freq,double val)
        {
            Frequency = freq;
            Value = val;
        }
    }
}
