using System.Collections;
using System.ComponentModel;

namespace HNIdesu.AudioExperiement.Controls
{
    public class FrequencyDomainCanvas : UserControl
    {
        public event EventHandler<MouseHoverEventArgs> OnMouseEnterItem = delegate { };
        public event EventHandler<ActualWidthChangeEventArgs> OnActualWidthChanged = delegate { };
        public event EventHandler<ItemWidthChangeEventArgs> OnItemWidthChanged = delegate { };
        private RectangleF? _HoverRange;
        private Color _HoverColor = Color.Gray;
        private Color _ForegroundColor = Color.Red;
        private float _OldItemWidth;
        public Color ForegroundColor
        {
            get => _ForegroundColor;
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
        private RectangleF? _VisibleRange;
        protected RectangleF? HoverRange
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
                if (!_InnerCollection.Contains(item))
                {
                    item._ParentControl = _ParentControl;
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
                        item._ParentControl = _ParentControl;
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
                    int freq = item.Frequency;
                    _InnerCollection.Remove(item);
                    if (freq == _ParentControl.MaxFrequency)
                        _ParentControl.MaxFrequency = _InnerCollection.Max(i => i.Frequency);
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
                get => _Frequency;
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
            private byte? _Opacity;
            public byte? Opacity
            {
                get => _Opacity == null ? _ParentControl?.Opacity : _Opacity;
                set
                {
                    if (value == null) return;
                    if (value < 0 || value > 0xFF)
                    {
                        throw new Exception("Opacity must be a value between 0 and 255");
                    }
                    if (value != _Opacity)
                    {
                        _Opacity = value;
                        _ParentControl?.Invalidate();
                    }
                }
            }
            public Color? FontColor
            {
                get => _FontColor == null ? _ParentControl?.ForeColor : _FontColor;
                set
                {
                    _FontColor = value;
                    _ParentControl?.Invalidate();
                }
            }
            private Color? _FillColor;
            public Color? FillColor
            {
                get => _FillColor == null ? _ParentControl?.ForegroundColor : _FillColor;
                set
                {
                    _FillColor = value;
                    _ParentControl?.Invalidate();
                }
            }

            internal FrequencyDomainCanvas? _ParentControl;
            public FrequencyDomainCanvasItem(int freq, double val)
            {
                Frequency = freq;
                Value = val;
            }
            public FrequencyDomainCanvasItem() { }


        }
        private float _SplitRate = 0;
        private float _ItemWidth = 1;
        private int _ScrollOffset = 0;

        protected virtual void OnItemWidthChange(object? sender,ItemWidthChangeEventArgs e)
        {
            OnActualWidthChanged(this, new ActualWidthChangeEventArgs(ActualWidth));
        }

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
            _VisibleRange = new RectangleF(0, 0, Width, Height);
        }

        public int ActualWidth => Convert.ToInt32(ItemWidth * (MaxFrequency + 1 + MaxFrequency * _SplitRate)) + 100;

        /// <summary>
        /// 双击创建项
        /// </summary>
        [Description("双击创建项")]
        public bool DoubleClickCreate { get; set; }
        public event EventHandler<MouseItemClickArgs> OnMouseItemClick = delegate { };


        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            HoverRange = null;
            if (DoubleClickCreate && e.Button == MouseButtons.Left)
            {
                int freq = (int)((e.Location.X + ScrollOffset) / (ItemWidth * (_SplitRate + 1)));
                double val = (Height - e.Location.Y) / (double)MaxItemHeight;
                if ((e.Location.X + ScrollOffset) % (ItemWidth * _SplitRate + ItemWidth) < ItemWidth)
                    Items.Add(new FrequencyDomainCanvasItem(freq, val));
            }
        }

        public int MaxItemHeight => Convert.ToInt32(Height * 0.95);

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
                if (value != _MaxFrequency)
                {
                    _MaxFrequency = value;
                    OnActualWidthChanged(this, new ActualWidthChangeEventArgs(ActualWidth));
                }

            }
        }


        private byte _Opacity = 0xFF;

        public byte Opacity
        {
            get => _Opacity;
            set
            {
                if (value < 0 || value > 0xFF)
                    throw new Exception("Opacity must be a value between 0 and 255");

                if (value != _Opacity)
                {
                    _Opacity = value;
                    Invalidate();
                }
            }
        }

        private float _MinItemWidth = 0.04f;
        private float MinItemWidth
        {
            get => _MinItemWidth;
            set
            {
                if (value < 0) throw new Exception("MinItemWidth must be bigger than 0");
                if (value != _MinItemWidth)
                {
                    _MinItemWidth = value;
                    if (_ItemWidth < _MinItemWidth)
                        ItemWidth = _ItemWidth;
                }
            }
        }
        [Description("每一项的宽度")]
        /// <summary>
        /// 每一频率项的宽度，单位Hz
        /// </summary>
        public float ItemWidth
        {
            get => _ItemWidth;
            set
            {
                if (value != _ItemWidth)
                {
                    _OldItemWidth = _ItemWidth;
                    if (value < MinItemWidth)
                        _ItemWidth = MinItemWidth;
                    else
                        _ItemWidth = value;
                    OnItemWidthChanged(this, new ItemWidthChangeEventArgs(_OldItemWidth, _ItemWidth));
                    Invalidate();
                }

            }
        }

        public FrequencyDomainCanvasItemCollection Items { get; private set; }
        public FrequencyDomainCanvas()
        {
            Items = new FrequencyDomainCanvasItemCollection(this);
            BackColor = Color.AliceBlue;
            DoubleBuffered = true;
            OnItemWidthChanged+= OnItemWidthChange;
            _OldItemWidth = ItemWidth;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int maxHeight = MaxItemHeight;
            if (Height-e.Y > maxHeight)
            {
                if (HoverRange != null)
                    HoverRange = null;
                return;
            }
            int freq = (int)((e.Location.X + ScrollOffset) / (ItemWidth * (_SplitRate + 1)));
            if ((e.Location.X + ScrollOffset) % (ItemWidth * (_SplitRate + 1)) < ItemWidth)
            {
                OnMouseEnterItem(this, new MouseHoverEventArgs(freq, (Height - e.Location.Y) / (double)maxHeight));
                HoverRange = new RectangleF(new PointF(ItemWidth * (_SplitRate + 1) * freq - ScrollOffset, e.Location.Y), new SizeF(ItemWidth, Height - e.Location.Y));
            }
            else
            {
                if (HoverRange != null)
                    HoverRange = null;
            }
        }


        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            ItemWidth += e.Delta / 500f;//滚动实现缩放
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if(HoverRange!=null)
                HoverRange = null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_VisibleRange == null)
                return;
            var graphics = e.Graphics;
            int maxHeight = MaxItemHeight;
            foreach (var item in Items)
            {
                RectangleF rect = new(
                    new PointF(
                        (ItemWidth + _SplitRate * ItemWidth) * item.Frequency - _ScrollOffset,
                        Convert.ToSingle(Height - maxHeight*item.Value)),
                    new SizeF(ItemWidth, Convert.ToSingle(maxHeight * item.Value)));
                if (!_VisibleRange.Value.IntersectsWith(rect))
                    continue;
                Brush fill_brush, font_brush;
                {
#pragma warning disable CS8629 // 可为 null 的值类型可为 null。
                    fill_brush = new SolidBrush(Color.FromArgb(item.FillColor.Value.ToArgb() & (0x00FFFFFF | item.Opacity.Value << 24)));
                    font_brush = new SolidBrush(item.FontColor.Value);
#pragma warning restore CS8629 // 可为 null 的值类型可为 null。

                }
                graphics.FillRectangle(fill_brush, rect);
                graphics.DrawString($"{item.Frequency}", Font, font_brush, new PointF((ItemWidth + _SplitRate * ItemWidth) * item.Frequency - _ScrollOffset, Convert.ToSingle(Height - maxHeight*item.Value) - 20));
            }
            if (_HoverRange != null)
            {
                Brush brush = new SolidBrush(Color.FromArgb(HoverColor.ToArgb() & 0x50FFFFFF));
                graphics.FillRectangle(brush, _HoverRange.Value);
            }

        }

    }
}
