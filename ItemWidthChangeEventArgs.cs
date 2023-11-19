namespace HNIdesu.AudioExperiement;

public class ItemWidthChangeEventArgs
{
    public float OldWidth { get; private set; }
    public float NewWidth { get; private set; }
    public ItemWidthChangeEventArgs(float oldWidth,float newWidth)
    {
        OldWidth = oldWidth;
        NewWidth = newWidth;
    }
}
