namespace Streaming_Tool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flipClockButton = new System.Windows.Forms.Button();
            this.clockLabel2 = new System.Windows.Forms.Label();
            this.clockLabel1 = new System.Windows.Forms.Label();
            this.pauseButton = new System.Windows.Forms.Button();
            this.scoreLabel1 = new System.Windows.Forms.Label();
            this.scoreLabel2 = new System.Windows.Forms.Label();
            this.scoreButton1Minus = new System.Windows.Forms.Button();
            this.scoreButton2Plus = new System.Windows.Forms.Button();
            this.scoreButton1Plus = new System.Windows.Forms.Button();
            this.scoreButton2Minus = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.boxLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.resetButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flipClockButton
            // 
            this.flipClockButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flipClockButton.Location = new System.Drawing.Point(257, 150);
            this.flipClockButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flipClockButton.Name = "flipClockButton";
            this.flipClockButton.Size = new System.Drawing.Size(239, 90);
            this.flipClockButton.TabIndex = 0;
            this.flipClockButton.Text = "Flip Clock";
            this.flipClockButton.UseVisualStyleBackColor = true;
            this.flipClockButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // clockLabel2
            // 
            this.clockLabel2.AutoSize = true;
            this.clockLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 62.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clockLabel2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.clockLabel2.Location = new System.Drawing.Point(13, 9);
            this.clockLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clockLabel2.Name = "clockLabel2";
            this.clockLabel2.Size = new System.Drawing.Size(311, 118);
            this.clockLabel2.TabIndex = 1;
            this.clockLabel2.Text = "60:00";
            this.clockLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.clockLabel2.Click += new System.EventHandler(this.label1_Click);
            // 
            // clockLabel1
            // 
            this.clockLabel1.AutoSize = true;
            this.clockLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 62.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clockLabel1.Location = new System.Drawing.Point(415, 9);
            this.clockLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.clockLabel1.Name = "clockLabel1";
            this.clockLabel1.Size = new System.Drawing.Size(311, 118);
            this.clockLabel1.TabIndex = 2;
            this.clockLabel1.Text = "60:00";
            this.clockLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pauseButton
            // 
            this.pauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pauseButton.Location = new System.Drawing.Point(257, 247);
            this.pauseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(239, 86);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // scoreLabel1
            // 
            this.scoreLabel1.AutoSize = true;
            this.scoreLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 70F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel1.Location = new System.Drawing.Point(550, 247);
            this.scoreLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scoreLabel1.Name = "scoreLabel1";
            this.scoreLabel1.Size = new System.Drawing.Size(120, 132);
            this.scoreLabel1.TabIndex = 4;
            this.scoreLabel1.Text = "0";
            this.scoreLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scoreLabel2
            // 
            this.scoreLabel2.AutoSize = true;
            this.scoreLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 70F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel2.Location = new System.Drawing.Point(80, 247);
            this.scoreLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.scoreLabel2.Name = "scoreLabel2";
            this.scoreLabel2.Size = new System.Drawing.Size(120, 132);
            this.scoreLabel2.TabIndex = 5;
            this.scoreLabel2.Text = "0";
            this.scoreLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // scoreButton1Minus
            // 
            this.scoreButton1Minus.Location = new System.Drawing.Point(501, 391);
            this.scoreButton1Minus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scoreButton1Minus.Name = "scoreButton1Minus";
            this.scoreButton1Minus.Size = new System.Drawing.Size(96, 58);
            this.scoreButton1Minus.TabIndex = 6;
            this.scoreButton1Minus.Text = "-";
            this.scoreButton1Minus.UseVisualStyleBackColor = true;
            this.scoreButton1Minus.Click += new System.EventHandler(this.scoreButton1_Click);
            // 
            // scoreButton2Plus
            // 
            this.scoreButton2Plus.Location = new System.Drawing.Point(136, 391);
            this.scoreButton2Plus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scoreButton2Plus.Name = "scoreButton2Plus";
            this.scoreButton2Plus.Size = new System.Drawing.Size(96, 58);
            this.scoreButton2Plus.TabIndex = 7;
            this.scoreButton2Plus.Text = "+";
            this.scoreButton2Plus.UseVisualStyleBackColor = true;
            this.scoreButton2Plus.Click += new System.EventHandler(this.scoreButton2_Click);
            // 
            // scoreButton1Plus
            // 
            this.scoreButton1Plus.Location = new System.Drawing.Point(605, 391);
            this.scoreButton1Plus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scoreButton1Plus.Name = "scoreButton1Plus";
            this.scoreButton1Plus.Size = new System.Drawing.Size(96, 58);
            this.scoreButton1Plus.TabIndex = 8;
            this.scoreButton1Plus.Text = "+";
            this.scoreButton1Plus.UseVisualStyleBackColor = true;
            this.scoreButton1Plus.Click += new System.EventHandler(this.scoreButton1Plus_Click);
            // 
            // scoreButton2Minus
            // 
            this.scoreButton2Minus.Location = new System.Drawing.Point(32, 391);
            this.scoreButton2Minus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scoreButton2Minus.Name = "scoreButton2Minus";
            this.scoreButton2Minus.Size = new System.Drawing.Size(96, 58);
            this.scoreButton2Minus.TabIndex = 9;
            this.scoreButton2Minus.Text = "-";
            this.scoreButton2Minus.UseVisualStyleBackColor = true;
            this.scoreButton2Minus.Click += new System.EventHandler(this.scoreButton2Minus_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.boxLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 494);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(739, 25);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // boxLabel
            // 
            this.boxLabel.Name = "boxLabel";
            this.boxLabel.Size = new System.Drawing.Size(121, 20);
            this.boxLabel.Text = "No box detected";
            // 
            // resetButton
            // 
            this.resetButton.Enabled = false;
            this.resetButton.Location = new System.Drawing.Point(313, 400);
            this.resetButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(111, 41);
            this.resetButton.TabIndex = 11;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 519);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.scoreButton2Minus);
            this.Controls.Add(this.scoreButton1Plus);
            this.Controls.Add(this.scoreButton2Plus);
            this.Controls.Add(this.scoreButton1Minus);
            this.Controls.Add(this.scoreLabel2);
            this.Controls.Add(this.scoreLabel1);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.clockLabel1);
            this.Controls.Add(this.clockLabel2);
            this.Controls.Add(this.flipClockButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Warlords Streaming Tool";
            this.TopMost = true;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button flipClockButton;
        private System.Windows.Forms.Label clockLabel2;
        private System.Windows.Forms.Label clockLabel1;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Label scoreLabel1;
        private System.Windows.Forms.Label scoreLabel2;
        private System.Windows.Forms.Button scoreButton1Minus;
        private System.Windows.Forms.Button scoreButton2Plus;
        private System.Windows.Forms.Button scoreButton1Plus;
        private System.Windows.Forms.Button scoreButton2Minus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel boxLabel;
        private System.Windows.Forms.Button resetButton;
    }
}

