namespace HNIdesu.AudioExperiement
{
    public class MouseItemClickArgs
    {
        public Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem Item { get; private set; }
        public MouseItemClickArgs(Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem item)
        {
            Item = item;
        }
    }
}
