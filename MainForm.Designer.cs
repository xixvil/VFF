namespace VFF
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.gbControl = new System.Windows.Forms.GroupBox();
            this.udFreq = new System.Windows.Forms.NumericUpDown();
            this.lbFreq = new System.Windows.Forms.Label();
            this.cbCSIL = new System.Windows.Forms.ComboBox();
            this.lbCSIL = new System.Windows.Forms.Label();
            this.btStop = new System.Windows.Forms.Button();
            this.btStart = new System.Windows.Forms.Button();
            this.lbCamera = new System.Windows.Forms.Label();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.gbCamera = new System.Windows.Forms.GroupBox();
            this.pbCamera = new System.Windows.Forms.PictureBox();
            this.FrameTimer = new System.Windows.Forms.Timer(this.components);
            this.gbControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFreq)).BeginInit();
            this.gbCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // gbControl
            // 
            this.gbControl.Controls.Add(this.udFreq);
            this.gbControl.Controls.Add(this.lbFreq);
            this.gbControl.Controls.Add(this.cbCSIL);
            this.gbControl.Controls.Add(this.lbCSIL);
            this.gbControl.Controls.Add(this.btStop);
            this.gbControl.Controls.Add(this.btStart);
            this.gbControl.Controls.Add(this.lbCamera);
            this.gbControl.Controls.Add(this.cbCameras);
            this.gbControl.Location = new System.Drawing.Point(15, 12);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(417, 81);
            this.gbControl.TabIndex = 3;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "Control";
            // 
            // udFreq
            // 
            this.udFreq.Location = new System.Drawing.Point(290, 50);
            this.udFreq.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.udFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udFreq.Name = "udFreq";
            this.udFreq.Size = new System.Drawing.Size(112, 20);
            this.udFreq.TabIndex = 7;
            this.udFreq.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.udFreq.ValueChanged += new System.EventHandler(this.udFreq_ValueChanged);
            // 
            // lbFreq
            // 
            this.lbFreq.AutoSize = true;
            this.lbFreq.Location = new System.Drawing.Point(224, 53);
            this.lbFreq.Name = "lbFreq";
            this.lbFreq.Size = new System.Drawing.Size(60, 13);
            this.lbFreq.TabIndex = 6;
            this.lbFreq.Text = "Frequency:";
            // 
            // cbCSIL
            // 
            this.cbCSIL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCSIL.FormattingEnabled = true;
            this.cbCSIL.Location = new System.Drawing.Point(55, 49);
            this.cbCSIL.Name = "cbCSIL";
            this.cbCSIL.Size = new System.Drawing.Size(163, 21);
            this.cbCSIL.TabIndex = 5;
            this.cbCSIL.SelectedIndexChanged += new System.EventHandler(this.cbCSIL_SelectedIndexChanged);
            // 
            // lbCSIL
            // 
            this.lbCSIL.AutoSize = true;
            this.lbCSIL.Location = new System.Drawing.Point(6, 53);
            this.lbCSIL.Name = "lbCSIL";
            this.lbCSIL.Size = new System.Drawing.Size(33, 13);
            this.lbCSIL.TabIndex = 4;
            this.lbCSIL.Text = "CSIL:";
            // 
            // btStop
            // 
            this.btStop.Enabled = false;
            this.btStop.Location = new System.Drawing.Point(316, 17);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(86, 27);
            this.btStop.TabIndex = 3;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(224, 17);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(86, 27);
            this.btStart.TabIndex = 2;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // lbCamera
            // 
            this.lbCamera.AutoSize = true;
            this.lbCamera.Location = new System.Drawing.Point(6, 23);
            this.lbCamera.Name = "lbCamera";
            this.lbCamera.Size = new System.Drawing.Size(46, 13);
            this.lbCamera.TabIndex = 1;
            this.lbCamera.Text = "Camera:";
            // 
            // cbCameras
            // 
            this.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(55, 20);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(163, 21);
            this.cbCameras.TabIndex = 0;
            // 
            // gbCamera
            // 
            this.gbCamera.Controls.Add(this.pbCamera);
            this.gbCamera.Location = new System.Drawing.Point(15, 99);
            this.gbCamera.Name = "gbCamera";
            this.gbCamera.Size = new System.Drawing.Size(417, 305);
            this.gbCamera.TabIndex = 4;
            this.gbCamera.TabStop = false;
            this.gbCamera.Text = "Camera Input";
            // 
            // pbCamera
            // 
            this.pbCamera.Location = new System.Drawing.Point(9, 19);
            this.pbCamera.Name = "pbCamera";
            this.pbCamera.Size = new System.Drawing.Size(402, 272);
            this.pbCamera.TabIndex = 0;
            this.pbCamera.TabStop = false;
            // 
            // FrameTimer
            // 
            this.FrameTimer.Tick += new System.EventHandler(this.FrameTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 415);
            this.Controls.Add(this.gbCamera);
            this.Controls.Add(this.gbControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Virtual Force Field";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbControl.ResumeLayout(false);
            this.gbControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFreq)).EndInit();
            this.gbCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbControl;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Label lbCamera;
        private System.Windows.Forms.ComboBox cbCameras;
        private System.Windows.Forms.GroupBox gbCamera;
        private System.Windows.Forms.PictureBox pbCamera;
        private System.Windows.Forms.Timer FrameTimer;
        private System.Windows.Forms.NumericUpDown udFreq;
        private System.Windows.Forms.Label lbFreq;
        private System.Windows.Forms.ComboBox cbCSIL;
        private System.Windows.Forms.Label lbCSIL;
    }
}

