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
                hScrollBar1.Maximum = frequencyDomainCanvas1.ActualWidth - frequencyDomainCanvas1.Width;
            };
            hScrollBar1.Scroll += (sender, e) =>
            {
                frequencyDomainCanvas1.ScrollOffset = e.NewValue;
            };
            frequencyDomainCanvas1.Items.Add(new FrequencyDomainCanvas.Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem(0, 0.5));
            frequencyDomainCanvas1.Items.Add(new FrequencyDomainCanvas.Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem(100, 0.5));
            frequencyDomainCanvas1.Items.Add(new FrequencyDomainCanvas.Controls.FrequencyDomainCanvas.FrequencyDomainCanvasItem(1000, 0.5));
        }
    }
}