using ChartTest;
using System.Collections;
using System.ComponentModel;

namespace FrequencyDomainCanvas.Controls
{
    public class FrequencyDomainCanvas : UserControl
    {
        public event EventHandler<MouseHoverEventArgs> OnMouseEnterItem = delegate { };
        public event EventHandler<ActualWidthChangeEventArgs> OnActualWidthChange = delegate { };
        private Rectangle? _HoverRange = null;
        protected Rectangle? HoverRange
        {
            get => _HoverRange;
            set
            {
                _HoverRange = value;
                Invalidate();
            }
        }
        public class FrequencyDomainCanvasItemCollection : IEnumerable<FrequencyDomainCanvasItem>
        {
            private readonly HashSet<FrequencyDomainCanvasItem> _InnerCollection = new();
            private readonly FrequencyDomainCanvas _ParentControl;

            public int Count => _InnerCollection.Count;
            public FrequencyDomainCanvasItemCollection(FrequencyDomainCanvas c)
            {
                _ParentControl = c;
            }


            public void Add(FrequencyDomainCanvasItem item)
            {
                if (item.Frequency > _ParentControl.MaxFrequency)
                    throw new ArgumentOutOfRangeException($"Frequency must be below MaxFrequency {_ParentControl.MaxFrequency}");
                if (!_InnerCollection.Contains(item))
                {
                    int minRequiredWidth = _ParentControl.ItemWidth * (item.Frequency + 1) + item.Frequency * _ParentControl.SplitDistance + 100;
                    if (_ParentControl.ActualWidth < minRequiredWidth)
                        _ParentControl.ActualWidth = minRequiredWidth;
                    _InnerCollection.Add(item);
                    _ParentControl.Invalidate();
                }
            }

            public void AddRange(IEnumerable<FrequencyDomainCanvasItem> items)
            {
                foreach (var item in items)
                {
                    if (item.Frequency > _ParentControl.MaxFrequency)
                        throw new ArgumentOutOfRangeException($"Frequency must be below MaxFrequency {_ParentControl.MaxFrequency}");
                    if (!_InnerCollection.Contains(item))
                    {
                        int minRequiredWidth = _ParentControl.ItemWidth * (item.Frequency + 1) + item.Frequency * _ParentControl.SplitDistance + 100;
                        if (_ParentControl.ActualWidth < minRequiredWidth)
                            _ParentControl.ActualWidth = minRequiredWidth;
                        _InnerCollection.Add(item);
                    }
                }
                _ParentControl.Invalidate();


            }


            public void Remove(FrequencyDomainCanvasItem item)
            {
                if (_InnerCollection.Contains(item))
                {
                    _InnerCollection.Remove(item);
                    _ParentControl.Invalidate();
                }
            }

            public IEnumerator<FrequencyDomainCanvasItem> GetEnumerator()
            {
                return _InnerCollection.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _InnerCollection.GetEnumerator();
            }
        }
        public class FrequencyDomainCanvasItem
        {
            private int _Frequency;
            public int Frequency
            {
                get=> _Frequency;
                set
                {
                    _Frequency = value;
                    _ParentControl?.Invalidate();
                }
            }
            private double _Value;
            public double Value
            {
                get => _Value;
                set
                {
                    if (value < 0 || value > 1)
                        throw new Exception("value must be between 0 and 1");
                    _Value = value;
                    _ParentControl?.Invalidate();
                }
            }
            public Color ForegroundColor { get; set; } = Color.Red;
            private FrequencyDomainCanvas? _ParentControl;
            public FrequencyDomainCanvasItem(int freq, double val)
            {
                Frequency = freq;
                Value = val;
            }
            public FrequencyDomainCanvasItem()
            {

            }
        }
        private int _SplitDistance = 5;
        private int _ItemWidth = 8;
        private int _ScrollOffset = 0;

        

        public int ScrollOffset
        {
            get => _ScrollOffset;
            set
            {
                if (_ScrollOffset != value)
                {
                    _ScrollOffset = value;
                    Invalidate();
                }

            }
        }
        public int ActualWidth
        {
            get => _ActualWidth;
            private set
            {
                if (_ActualWidth != value)
                {
                    _ActualWidth = value;
                    OnActualWidthChange(this, new ActualWidthChangeEventArgs(value));
                }

            }
        }
        private int _ActualWidth;

        /// <summary>
        /// 双击创建项
        /// </summary>
        [Description("双击创建项")]
        public bool DoubleClickCreate { get; set; }
        public event EventHandler<MouseItemClickArgs> OnMouseItemCLick = delegate { };


        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (DoubleClickCreate&&e.Button == MouseButtons.Left)
            {
                HoverRange = null;
                int freq = (e.Location.X+ScrollOffset) / (SplitDistance + ItemWidth);
                double val = (Height - e.Location.Y) / (double)Height;
                if (e.Location.X % (SplitDistance + ItemWidth) < ItemWidth)
                    Items.Add(new FrequencyDomainCanvasItem(freq, val));
            }
        }

        /// <summary>
        /// 最大频率，单位Hz
        /// </summary>
        public int MaxFrequency { get; private set; } = 20000;

        [Description("两个频率间的间隔")]
        /// <summary>
        /// 两个频率间的间隔，单位:px
        /// </summary>
        public int SplitDistance
        {
            get => _SplitDistance;
            set
            {
                if (value < 0)
                    _SplitDistance = 0;
                else
                    _SplitDistance = value;
                Invalidate();
            }
        }

        [Description("每一频率项的宽度")]
        /// <summary>
        /// 每一频率项的宽度，单位Hz
        /// </summary>
        public int ItemWidth
        {
            get => _ItemWidth;
            set
            {
                _ItemWidth = value;
                Invalidate();
            }
        }

        public FrequencyDomainCanvasItemCollection Items { get; private set; }
        public FrequencyDomainCanvas()
        {
            Items = new FrequencyDomainCanvasItemCollection(this);
            BackColor = Color.AliceBlue;
            _ActualWidth = Width;
            DoubleBuffered = true;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int freq = (e.Location.X + ScrollOffset) / (SplitDistance + ItemWidth);
            if (e.Location.X % (SplitDistance + ItemWidth) < ItemWidth)
            {
                OnMouseEnterItem(this, new MouseHoverEventArgs(freq, (Height - e.Location.Y) / (double)Height));
                HoverRange = new Rectangle(new Point((SplitDistance+ItemWidth)*freq-ScrollOffset,e.Location.Y), new Size(ItemWidth, Height - e.Location.Y));
            }
            else
            {
                HoverRange = null;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (ActualWidth > Width)
            {
                SplitDistance += Convert.ToInt32(e.Delta / 220f);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            HoverRange = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            Brush brush;
            foreach (var item in Items)
            {
                brush = new SolidBrush(item.ForegroundColor);
                graphics.FillRectangle(brush, new Rectangle(new Point((ItemWidth + SplitDistance) * item.Frequency - _ScrollOffset, Convert.ToInt32(Height * (1 - item.Value))), new Size(ItemWidth, Convert.ToInt32(Height * item.Value))));
                graphics.DrawString($"{item.Frequency}", new Font(FontFamily.GenericSansSerif, 10), brush, new PointF((ItemWidth + SplitDistance) * item.Frequency - _ScrollOffset, Convert.ToSingle(Height * (1 - item.Value) - 20)));
            }
            if (_HoverRange != null)
            {
                graphics.FillRectangle(new SolidBrush(Color.White), _HoverRange.Value);
            }

        }



    }
}
