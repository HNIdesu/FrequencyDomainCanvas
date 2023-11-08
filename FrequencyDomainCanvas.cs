using ChartTest;
using System.Collections;
using System.ComponentModel;

namespace FrequencyDomainCanvas.Controls
{
    public class FrequencyDomainCanvas : UserControl
    {
        public event EventHandler<MouseEnterEventArgs> OnMouseEnterItem = delegate { };
        public event EventHandler<ActualWidthChangeEventArgs> OnActualWidthChange = delegate { };
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
            public int Frequency { get; private set; }
            public double Value { get; private set; }
            public Color ForegroundColor { get; set; } = Color.Red;
            public FrequencyDomainCanvasItem(int freq, double val)
            {
                if (val < 0 || val > 1)
                    throw new ArgumentOutOfRangeException(nameof(val), val, "must be between 0 and 1");
                Frequency = freq;
                Value = val;
            }
        }
        private int _SplitDistance = 3;
        private int _ItemWidth = 5;
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
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int freq = e.Location.X / (SplitDistance + ItemWidth);
            if (e.Location.X % (SplitDistance + ItemWidth) < ItemWidth)
            {
                OnMouseEnterItem(this, new MouseEnterEventArgs(freq, (Height - e.Location.Y) / (double)Height));
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



        }


    }
}
