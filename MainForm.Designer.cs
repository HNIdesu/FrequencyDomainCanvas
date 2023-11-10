
namespace ChartTest
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel_Status = new ToolStripStatusLabel();
            frequencyDomainCanvas1 = new FrequencyDomainCanvas.Controls.FrequencyDomainCanvas();
            hScrollBar1 = new HScrollBar();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel_Status });
            statusStrip1.Location = new Point(0, 539);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(784, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Status
            // 
            toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            toolStripStatusLabel_Status.Size = new Size(0, 17);
            // 
            // frequencyDomainCanvas1
            // 
            frequencyDomainCanvas1.BackColor = Color.AliceBlue;
            frequencyDomainCanvas1.DoubleClickCreate = true;
            frequencyDomainCanvas1.ForeColor = Color.Red;
            frequencyDomainCanvas1.ForegroundColor = Color.Red;
            frequencyDomainCanvas1.HoverColor = Color.Chartreuse;
            frequencyDomainCanvas1.ItemWidth = 5;
            frequencyDomainCanvas1.Location = new Point(0, 0);
            frequencyDomainCanvas1.Name = "frequencyDomainCanvas1";
            frequencyDomainCanvas1.Opacity = 100;
            frequencyDomainCanvas1.ScrollOffset = 0;
            frequencyDomainCanvas1.Size = new Size(784, 519);
            frequencyDomainCanvas1.SplitDistance = 3;
            frequencyDomainCanvas1.TabIndex = 2;
            // 
            // hScrollBar1
            // 
            hScrollBar1.LargeChange = 2;
            hScrollBar1.Location = new Point(0, 522);
            hScrollBar1.Maximum = 1;
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(775, 17);
            hScrollBar1.TabIndex = 3;
            hScrollBar1.ValueChanged += hScrollBar1_ValueChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(hScrollBar1);
            Controls.Add(frequencyDomainCanvas1);
            Controls.Add(statusStrip1);
            Name = "MainForm";
            Text = "频域显示";
            Load += MainForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel_Status;
        private FrequencyDomainCanvas.Controls.FrequencyDomainCanvas frequencyDomainCanvas1;
        private HScrollBar hScrollBar1;
    }
}