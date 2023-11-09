

namespace  FrequencyDomainCanvas.Controls
{
    public class MouseItemClickArgs
    {
        public FrequencyDomainCanvas.FrequencyDomainCanvasItem Item { get; private set; }
        public MouseItemClickArgs(FrequencyDomainCanvas.FrequencyDomainCanvasItem item)
        {
            Item = item;
        }
    }
}
