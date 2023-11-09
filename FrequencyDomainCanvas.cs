using ChartTest;
using System.Collections;
using System.ComponentModel;

namespace FrequencyDomainCanvas.Controls
{
    public class FrequencyDomainCanvas : UserControl
    {
        public event EventHandler<MouseHoverEventArgs> OnMouseEnterItem = delegate { };
        public event EventHandler<ActualWidthChangeEventArgs> OnActualWidthChange = delegate { };
        private Rectangle? _HoverRange;
        private Color _HoverColor = Color.Gray;
        private Color _ForegroundColor = Color.Red;
        public Color ForegroundColor
        {
            get=> _ForegroundColor;
            set
            {
                _ForegroundColor = value;
                Invalidate();
            }
        }
        public Color HoverColor
        {
            get => _HoverColor;
            set
            {
                if (_HoverColor != value)
                {
                    _HoverColor = value;
                    Invalidate();
                }
                
            }
        }
        private Rectangle? _VisibleRange;
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
                    if (item.Frequency > _ParentControl.MaxFrequency)
                        _ParentControl.MaxFrequency = item.Frequency;
                    _InnerCollection.Add(item);
                    _ParentControl.Invalidate();
                }
            }

            public void AddRange(IEnumerable<FrequencyDomainCanvasItem> items)
            {
                foreach (var item in items)
                {
                    if (!_InnerCollection.Contains(item))
                    {
                        if (item.Frequency > _ParentControl.MaxFrequency)
                            _ParentControl.MaxFrequency = item.Frequency;
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

            private Color? _FontColor;

            public Color? FontColor
            {
                get => _FontColor;
                set
                {
                    _FontColor = value;
                    _ParentControl?.Invalidate();
                }
            }
            private Color? _FillColor;
            public Color? FillColor
            {
                get => _FillColor;
                set
                {
                    _FillColor = value;
                    _ParentControl?.Invalidate();
                }
            }

            private readonly FrequencyDomainCanvas? _ParentControl;
            public FrequencyDomainCanvasItem(int freq, double val)
            {
                Frequency = freq;
                Value = val;
            }
            public FrequencyDomainCanvasItem(){}


        }
        private int _SplitDistance = 8;
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

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _VisibleRange = new Rectangle(0, 0, Width, Height);
        }
        public int ActualWidth=> ItemWidth * (MaxFrequency + 1) + MaxFrequency * SplitDistance + 100;

        /// <summary>
        /// 双击创建项
        /// </summary>
        [Description("双击创建项")]
        public bool DoubleClickCreate { get; set; }
        public event EventHandler<MouseItemClickArgs> OnMouseItemCLick = delegate { };


        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            HoverRange = null;
            if (DoubleClickCreate&&e.Button == MouseButtons.Left)
            {
                int freq = (e.Location.X+ScrollOffset) / (SplitDistance + ItemWidth);
                double val = (Height - e.Location.Y) / (double)Height;
                if ((e.Location.X + ScrollOffset) % (SplitDistance + ItemWidth) < ItemWidth)
                    Items.Add(new FrequencyDomainCanvasItem(freq, val));
            }
        }

        private int _MaxFrequency;
        /// <summary>
        /// 最大频率，单位Hz
        /// </summary>
        public int MaxFrequency
        {
            get => _MaxFrequency;
            set
            {
                if (value < 0)
                    throw new Exception();
                _MaxFrequency = value;
                OnActualWidthChange(this, new ActualWidthChangeEventArgs(ActualWidth));
            }
        }

        [Description("两个项的间隔")]
        /// <summary>
        /// 两个频率间的间隔，单位:px
        /// </summary>
        public int SplitDistance
        {
            get => _SplitDistance;
            set
            {
                if (value <= 0)
                    _SplitDistance = 1;
                else
                    _SplitDistance = value;
                OnActualWidthChange(this, new ActualWidthChangeEventArgs(ActualWidth));
                Invalidate();
            }
        }

        [Description("每一项的宽度")]
        /// <summary>
        /// 每一频率项的宽度，单位Hz
        /// </summary>
        public int ItemWidth
        {
            get => _ItemWidth;
            set
            {
                if(value<=0)
                    _ItemWidth = 1;
                else
                    _ItemWidth = value;
                OnActualWidthChange(this, new ActualWidthChangeEventArgs(ActualWidth));
                Invalidate();
            }
        }

        public FrequencyDomainCanvasItemCollection Items { get; private set; }
        public FrequencyDomainCanvas()
        {
            Items = new FrequencyDomainCanvasItemCollection(this);
            BackColor = Color.AliceBlue;
            DoubleBuffered = true;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int freq = (e.Location.X + ScrollOffset) / (SplitDistance + ItemWidth);
            if ((e.Location.X+ ScrollOffset) % (SplitDistance + ItemWidth) < ItemWidth)
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
                ItemWidth += Convert.ToInt32(e.Delta / 220f);
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
            if (_VisibleRange == null)
                return;
            var graphics = e.Graphics;
            Brush brush=new SolidBrush(ForegroundColor),fontBrush=new SolidBrush(ForeColor);

            foreach (var item in Items)
            {
                Rectangle rect = new(
                    new Point(
                        (ItemWidth + SplitDistance) * item.Frequency - _ScrollOffset, 
                        Convert.ToInt32(Height * (1 - item.Value))), 
                    new Size(ItemWidth, Convert.ToInt32(Height * item.Value)));
                if (!_VisibleRange.Value.IntersectsWith(rect))
                    continue;
                graphics.FillRectangle(item.FillColor!=null?new SolidBrush(item.FillColor.Value) :brush, rect);
                graphics.DrawString($"{item.Frequency}", Font, item.FontColor != null ? new SolidBrush(item.FontColor.Value) : fontBrush, new PointF((ItemWidth + SplitDistance) * item.Frequency - _ScrollOffset, Convert.ToSingle(Height * (1 - item.Value) - 20)));
            }
            if (_HoverRange != null)
            {
                brush = new SolidBrush(Color.FromArgb(HoverColor.ToArgb() & 0x50FFFFFF));
                graphics.FillRectangle(brush, _HoverRange.Value);
            }

        }

    }
}
