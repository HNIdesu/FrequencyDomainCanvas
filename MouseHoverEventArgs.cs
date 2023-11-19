namespace HNIdesu.AudioExperiement;

public class MouseHoverEventArgs
{
    public int Frequency { get; private set; }
    public double Value { get; private set; }
    public MouseHoverEventArgs(int freq,double val)
    {
        Frequency = freq;
        Value = val;
    }
}
