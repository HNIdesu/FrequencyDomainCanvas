using HNIdesu.AudioExperiement.Controls;

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
            frequencyDomainCanvasControl1 = new FrequencyDomainCanvasControl();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel_Status });
            statusStrip1.Location = new Point(0, 539);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(957, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Status
            // 
            toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            toolStripStatusLabel_Status.Size = new Size(0, 17);
            // 
            // frequencyDomainCanvasControl1
            // 
            frequencyDomainCanvasControl1.Dock = DockStyle.Fill;
            frequencyDomainCanvasControl1.DoubleClickCreate = true;
            frequencyDomainCanvasControl1.Location = new Point(0, 0);
            frequencyDomainCanvasControl1.Name = "frequencyDomainCanvasControl1";
            frequencyDomainCanvasControl1.Size = new Size(957, 539);
            frequencyDomainCanvasControl1.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(957, 561);
            Controls.Add(frequencyDomainCanvasControl1);
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
        private FrequencyDomainCanvasControl frequencyDomainCanvasControl1;
    }
}