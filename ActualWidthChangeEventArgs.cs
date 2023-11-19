namespace HNIdesu.AudioExperiement
{
    public class ActualWidthChangeEventArgs
    {
        public int ActualWidth { get; private set; }
        
        public ActualWidthChangeEventArgs(int x)
        {
            ActualWidth = x;
        }
    }
}
