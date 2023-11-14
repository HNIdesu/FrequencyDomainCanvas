namespace ChartTest
{
    public partial class MainForm : Form
    {
        private static MainForm? _Instance;
        private MainForm()
        {
            InitializeComponent();
        }
        public static MainForm Instance
        {
            get
            {
                _Instance ??= new MainForm();
                return _Instance;
            }
        }
        public static void Log(string msg)
        {
            if (Instance != null)
                Instance.textBox_Log.Text += msg + "\n";
        }

        public static void ClearLog()
        {
            Instance.textBox_Log.Text = "";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            frequencyDomainCanvas1.OnMouseEnterItem += (sender, e) =>
            {
                toolStripStatusLabel_Status.Text = $"({e.Frequency}Hz,{e.Value:0.##})";
            };
            frequencyDomainCanvas1.OnActualWidthChange += (sender, e) =>
            {
                int maxOffset = frequencyDomainCanvas1.ActualWidth - frequencyDomainCanvas1.Width;
                if (maxOffset <= 1) maxOffset = 1;
                int mx = frequencyDomainCanvas1.PointToClient(MousePosition).X;
                int ax = frequencyDomainCanvas1.ScrollOffset + mx;
                double zoon_rate = frequencyDomainCanvas1.ItemWidth / (double)frequencyDomainCanvas1.OldItemWidth;
                ax = Convert.ToInt32(ax * zoon_rate);
                //int offset = ax -frequencyDomainCanvas1.Width/2;中心放大
                int offset = ax - mx;
                if (offset < 1||offset> maxOffset)
                    offset = 1;
                hScrollBar1.Maximum = maxOffset;
                hScrollBar1.Value = offset;
            };

            frequencyDomainCanvas1.Items.AddRange(GetSamples());

        }

        private static IEnumerable<FrequencyDomainCanvas.Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem> GetSamples()
        {
            for (int i = 0; i <= 100; i++)
                yield return new FrequencyDomainCanvas.Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem(i, 0.5);
        }

        private void HScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            int value = ((HScrollBar)sender).Value;
            frequencyDomainCanvas1.ScrollOffset = value;
        }
    }
}