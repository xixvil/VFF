using System;
using System.Windows.Forms;


namespace VFF
{
    public class VerticalProgressBar : ProgressBar // вертикальный ProgressBar :) прочитал тут: http://social.msdn.microsoft.com/Forums/windows/en-US/60b2493d-c8ff-495d-b845-d114fe456f54/how-to-create-a-vertical-progressbar-in-visual-cnet-2005?forum=winforms
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04;
                return cp;
            }
        }
    }

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
            this.gbSignalStrength = new System.Windows.Forms.GroupBox();
            this.gbBar = new System.Windows.Forms.GroupBox();
            this.pbSignalValue = new System.Windows.Forms.PictureBox();
            this.pbSignalStrength = new System.Windows.Forms.PictureBox();
            this.gbControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udFreq)).BeginInit();
            this.gbCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera)).BeginInit();
            this.gbSignalStrength.SuspendLayout();
            this.gbBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSignalValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSignalStrength)).BeginInit();
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
            this.gbControl.Location = new System.Drawing.Point(12, 12);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(339, 157);
            this.gbControl.TabIndex = 3;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "Control";
            // 
            // udFreq
            // 
            this.udFreq.Location = new System.Drawing.Point(72, 81);
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
            this.lbFreq.Location = new System.Drawing.Point(6, 83);
            this.lbFreq.Name = "lbFreq";
            this.lbFreq.Size = new System.Drawing.Size(60, 13);
            this.lbFreq.TabIndex = 6;
            this.lbFreq.Text = "Frequency:";
            // 
            // cbCSIL
            // 
            this.cbCSIL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCSIL.FormattingEnabled = true;
            this.cbCSIL.Location = new System.Drawing.Point(72, 49);
            this.cbCSIL.Name = "cbCSIL";
            this.cbCSIL.Size = new System.Drawing.Size(257, 21);
            this.cbCSIL.TabIndex = 5;
            this.cbCSIL.SelectedIndexChanged += new System.EventHandler(this.cbCSIL_SelectedIndexChanged);
            // 
            // lbCSIL
            // 
            this.lbCSIL.AutoSize = true;
            this.lbCSIL.Location = new System.Drawing.Point(6, 53);
            this.lbCSIL.Name = "lbCSIL";
            this.lbCSIL.Size = new System.Drawing.Size(52, 13);
            this.lbCSIL.TabIndex = 4;
            this.lbCSIL.Text = "Interface:";
            // 
            // btStop
            // 
            this.btStop.Enabled = false;
            this.btStop.Location = new System.Drawing.Point(243, 117);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(86, 27);
            this.btStop.TabIndex = 3;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(151, 117);
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
            this.cbCameras.Location = new System.Drawing.Point(72, 20);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(257, 21);
            this.cbCameras.TabIndex = 0;
            // 
            // gbCamera
            // 
            this.gbCamera.Controls.Add(this.pbCamera);
            this.gbCamera.Location = new System.Drawing.Point(12, 175);
            this.gbCamera.Name = "gbCamera";
            this.gbCamera.Size = new System.Drawing.Size(339, 269);
            this.gbCamera.TabIndex = 4;
            this.gbCamera.TabStop = false;
            this.gbCamera.Text = "Camera Input";
            // 
            // pbCamera
            // 
            this.pbCamera.Location = new System.Drawing.Point(9, 19);
            this.pbCamera.Name = "pbCamera";
            this.pbCamera.Size = new System.Drawing.Size(320, 240);
            this.pbCamera.TabIndex = 0;
            this.pbCamera.TabStop = false;
            // 
            // FrameTimer
            // 
            this.FrameTimer.Tick += new System.EventHandler(this.FrameTimer_Tick);
            // 
            // gbSignalStrength
            // 
            this.gbSignalStrength.Controls.Add(this.gbBar);
            this.gbSignalStrength.Location = new System.Drawing.Point(357, 12);
            this.gbSignalStrength.Name = "gbSignalStrength";
            this.gbSignalStrength.Size = new System.Drawing.Size(55, 433);
            this.gbSignalStrength.TabIndex = 5;
            this.gbSignalStrength.TabStop = false;
            this.gbSignalStrength.Text = "Signal";
            // 
            // gbBar
            // 
            this.gbBar.Controls.Add(this.pbSignalValue);
            this.gbBar.Controls.Add(this.pbSignalStrength);
            this.gbBar.Location = new System.Drawing.Point(14, 22);
            this.gbBar.Name = "gbBar";
            this.gbBar.Size = new System.Drawing.Size(26, 399);
            this.gbBar.TabIndex = 0;
            this.gbBar.TabStop = false;
            // 
            // pbSignalValue
            // 
            this.pbSignalValue.Location = new System.Drawing.Point(3, 10);
            this.pbSignalValue.Name = "pbSignalValue";
            this.pbSignalValue.Size = new System.Drawing.Size(19, 382);
            this.pbSignalValue.TabIndex = 1;
            this.pbSignalValue.TabStop = false;
            // 
            // pbSignalStrength
            // 
            this.pbSignalStrength.Location = new System.Drawing.Point(3, 10);
            this.pbSignalStrength.Name = "pbSignalStrength";
            this.pbSignalStrength.Size = new System.Drawing.Size(19, 382);
            this.pbSignalStrength.TabIndex = 0;
            this.pbSignalStrength.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 457);
            this.Controls.Add(this.gbSignalStrength);
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
            this.gbSignalStrength.ResumeLayout(false);
            this.gbBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSignalValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSignalStrength)).EndInit();
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
        private System.Windows.Forms.GroupBox gbSignalStrength;
        private GroupBox gbBar;
        private PictureBox pbSignalStrength;
        private PictureBox pbSignalValue;
    }
}

