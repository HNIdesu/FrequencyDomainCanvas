
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
            splitContainer1 = new SplitContainer();
            textBox_Log = new TextBox();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel_Status });
            statusStrip1.Location = new Point(0, 539);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1097, 22);
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
            frequencyDomainCanvas1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            frequencyDomainCanvas1.BackColor = Color.AliceBlue;
            frequencyDomainCanvas1.DoubleClickCreate = true;
            frequencyDomainCanvas1.ForeColor = Color.Red;
            frequencyDomainCanvas1.ForegroundColor = Color.Red;
            frequencyDomainCanvas1.HoverColor = Color.Chartreuse;
            frequencyDomainCanvas1.ItemWidth = 0.04f;
            frequencyDomainCanvas1.Location = new Point(0, 0);
            frequencyDomainCanvas1.MaxFrequency = 0;
            frequencyDomainCanvas1.Name = "frequencyDomainCanvas1";
            frequencyDomainCanvas1.Opacity = 100;
            frequencyDomainCanvas1.ScrollOffset = 0;
            frequencyDomainCanvas1.Size = new Size(727, 514);
            frequencyDomainCanvas1.TabIndex = 2;
            // 
            // hScrollBar1
            // 
            hScrollBar1.LargeChange = 2;
            hScrollBar1.Location = new Point(0, 517);
            hScrollBar1.Maximum = 1;
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(727, 22);
            hScrollBar1.TabIndex = 3;
            hScrollBar1.ValueChanged += HScrollBar1_ValueChanged;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(hScrollBar1);
            splitContainer1.Panel1.Controls.Add(frequencyDomainCanvas1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(textBox_Log);
            splitContainer1.Size = new Size(1097, 539);
            splitContainer1.SplitterDistance = 730;
            splitContainer1.TabIndex = 4;
            // 
            // textBox_Log
            // 
            textBox_Log.Location = new Point(3, 3);
            textBox_Log.Multiline = true;
            textBox_Log.Name = "textBox_Log";
            textBox_Log.ReadOnly = true;
            textBox_Log.Size = new Size(357, 533);
            textBox_Log.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1097, 561);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Name = "MainForm";
            Text = "频域显示";
            Load += MainForm_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel_Status;
        private FrequencyDomainCanvas.Controls.FrequencyDomainCanvas frequencyDomainCanvas1;
        private HScrollBar hScrollBar1;
        private SplitContainer splitContainer1;
        private TextBox textBox_Log;
    }
}