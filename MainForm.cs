namespace ChartTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            frequencyDomainCanvas1.OnMouseEnterItem += (sender, e) =>
            {
                toolStripStatusLabel_Status.Text = $"({e.Frequency}Hz,{e.Value:0.##})";
            };
            frequencyDomainCanvas1.OnActualWidthChange += (sender, e) =>
            {
                int value = frequencyDomainCanvas1.ActualWidth - frequencyDomainCanvas1.Width;
                if (value < 0) value = 1;
                hScrollBar1.Maximum = value;
            };

            frequencyDomainCanvas1.Items.AddRange(GetSamples());

        }

        private static IEnumerable<FrequencyDomainCanvas.Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem> GetSamples()
        {
            for (int i = 0; i < 20000; i++)
                yield return new FrequencyDomainCanvas.Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem(i, 0.5);
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            int value = ((HScrollBar)sender).Value;
            frequencyDomainCanvas1.ScrollOffset = value;
        }
    }
}