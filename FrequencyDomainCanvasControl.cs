namespace HNIdesu.AudioExperiement.Controls
{
    public class FrequencyDomainCanvasControl : UserControl
    {
        private FrequencyDomainCanvas frequencyDomainCanvas1;
        private HScrollBar hScrollBar1;
        public bool DoubleClickCreate
        {
            get => frequencyDomainCanvas1.DoubleClickCreate;
            set =>frequencyDomainCanvas1.DoubleClickCreate = value;

        }
        public event EventHandler<MouseHoverEventArgs> OnMouseEnterItem = delegate { };

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        public FrequencyDomainCanvasControl()
#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
        {
            InitializeComponent();
        }

        public FrequencyDomainCanvas.FrequencyDomainCanvasItemCollection Items => frequencyDomainCanvas1.Items;

        private void InitializeComponent()
        {
            frequencyDomainCanvas1 = new FrequencyDomainCanvas();
            hScrollBar1 = new HScrollBar();
            SuspendLayout();
            // 
            // frequencyDomainCanvas1
            // 
            frequencyDomainCanvas1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            frequencyDomainCanvas1.BackColor = Color.AliceBlue;
            frequencyDomainCanvas1.DoubleClickCreate = false;
            frequencyDomainCanvas1.ForegroundColor = Color.Red;
            frequencyDomainCanvas1.HoverColor = Color.Gray;
            frequencyDomainCanvas1.ItemWidth = 8F;
            frequencyDomainCanvas1.Location = new Point(0, 0);
            frequencyDomainCanvas1.MaxFrequency = 0;
            frequencyDomainCanvas1.Name = "frequencyDomainCanvas1";
            frequencyDomainCanvas1.Opacity = 255;
            frequencyDomainCanvas1.ScrollOffset = 0;
            frequencyDomainCanvas1.Size = new Size(719, 462);
            frequencyDomainCanvas1.TabIndex = 0;
            frequencyDomainCanvas1.OnMouseEnterItem += frequencyDomainCanvas1_OnMouseEnterItem;
            frequencyDomainCanvas1.OnActualWidthChanged += frequencyDomainCanvas1_OnActualWidthChange;
            frequencyDomainCanvas1.OnItemWidthChanged += frequencyDomainCanvas1_OnItemWidthChanged;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Dock = DockStyle.Bottom;
            hScrollBar1.Location = new Point(0, 465);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(722, 17);
            hScrollBar1.TabIndex = 1;
            hScrollBar1.ValueChanged += hScrollBar1_ValueChanged;
            // 
            // FrequencyDomainCanvasControl
            // 
            Controls.Add(hScrollBar1);
            Controls.Add(frequencyDomainCanvas1);
            Name = "FrequencyDomainCanvasControl";
            Size = new Size(722, 482);
            ResumeLayout(false);
        }

        private void frequencyDomainCanvas1_OnActualWidthChange(object? sender, ActualWidthChangeEventArgs e)
        {
            int maxOffset = frequencyDomainCanvas1.ActualWidth - frequencyDomainCanvas1.Width;
            if (maxOffset <= 1) maxOffset = 1;
            hScrollBar1.Maximum = maxOffset;   
        }


        private void frequencyDomainCanvas1_OnMouseEnterItem(object? sender, MouseHoverEventArgs e)
        {
            OnMouseEnterItem(sender, e);
        }

        private void hScrollBar1_ValueChanged(object? sender, EventArgs e)
        {
            int offset = hScrollBar1.Value - 1;
            frequencyDomainCanvas1.ScrollOffset = offset >= 0 ? offset : 0;
        }

        private void frequencyDomainCanvas1_OnItemWidthChanged(object? sender, ItemWidthChangeEventArgs e)
        {
            int mx = frequencyDomainCanvas1.PointToClient(MousePosition).X;
            int ax = frequencyDomainCanvas1.ScrollOffset + mx;
            double zoon_rate = e.NewWidth / (double)e.OldWidth;
            ax = Convert.ToInt32(ax * zoon_rate);
            //int offset = ax -frequencyDomainCanvas1.Width/2;放大至中心
            int offset = ax - mx;//在原处放大
            if (offset < 1)
                offset = 1;
            if (offset > hScrollBar1.Maximum)
                offset = hScrollBar1.Maximum;
            hScrollBar1.Value = offset;
        }
    }
}
