using HNIdesu.AudioExperiement.Controls;

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
            frequencyDomainCanvasControl1.OnMouseEnterItem += (sender, e) =>
            {
                toolStripStatusLabel_Status.Text = $"({e.Frequency}Hz,{e.Value:0.##})";
            };
            frequencyDomainCanvasControl1.Items.AddRange(GetSamples());

        }

        private static IEnumerable<FrequencyDomainCanvas.FrequencyDomainCanvasItem> GetSamples()
        {
            for (int i = 0; i <= 20; i++)
                yield return new FrequencyDomainCanvas.FrequencyDomainCanvasItem(i * 1000, 0.5);
        }

    }
}