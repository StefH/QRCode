namespace QRCodeDecoderDemo
{
	partial class QRCodeDecoderDemo
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRCodeDecoderDemo));
			this.ImageFileLabel = new System.Windows.Forms.Label();
			this.DecodedDataLabel = new System.Windows.Forms.Label();
			this.LoadImageButton = new System.Windows.Forms.Button();
			this.HeaderLabel = new System.Windows.Forms.Label();
			this.DataTextBox = new System.Windows.Forms.TextBox();
			this.ECIAssignLabel = new System.Windows.Forms.Label();
			this.ECIValueLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ImageFileLabel
			// 
			this.ImageFileLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ImageFileLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ImageFileLabel.Location = new System.Drawing.Point(180, 527);
			this.ImageFileLabel.Name = "ImageFileLabel";
			this.ImageFileLabel.Size = new System.Drawing.Size(407, 20);
			this.ImageFileLabel.TabIndex = 6;
			this.ImageFileLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DecodedDataLabel
			// 
			this.DecodedDataLabel.AutoSize = true;
			this.DecodedDataLabel.Location = new System.Drawing.Point(7, 390);
			this.DecodedDataLabel.Name = "DecodedDataLabel";
			this.DecodedDataLabel.Size = new System.Drawing.Size(88, 16);
			this.DecodedDataLabel.TabIndex = 1;
			this.DecodedDataLabel.Text = "Decoded data";
			// 
			// LoadImageButton
			// 
			this.LoadImageButton.Location = new System.Drawing.Point(8, 519);
			this.LoadImageButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.LoadImageButton.Name = "LoadImageButton";
			this.LoadImageButton.Size = new System.Drawing.Size(164, 38);
			this.LoadImageButton.TabIndex = 5;
			this.LoadImageButton.Text = "Load QR Code Image";
			this.LoadImageButton.UseVisualStyleBackColor = true;
			this.LoadImageButton.Click += new System.EventHandler(this.OnLoadImage);
			// 
			// HeaderLabel
			// 
			this.HeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.HeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.HeaderLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HeaderLabel.Location = new System.Drawing.Point(234, 8);
			this.HeaderLabel.Name = "HeaderLabel";
			this.HeaderLabel.Size = new System.Drawing.Size(180, 32);
			this.HeaderLabel.TabIndex = 0;
			this.HeaderLabel.Text = "QR Code Decoder";
			this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DataTextBox
			// 
			this.DataTextBox.AcceptsReturn = true;
			this.DataTextBox.BackColor = System.Drawing.SystemColors.Info;
			this.DataTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.DataTextBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.DataTextBox.Location = new System.Drawing.Point(8, 410);
			this.DataTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DataTextBox.Multiline = true;
			this.DataTextBox.Name = "DataTextBox";
			this.DataTextBox.ReadOnly = true;
			this.DataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.DataTextBox.Size = new System.Drawing.Size(572, 101);
			this.DataTextBox.TabIndex = 4;
			this.DataTextBox.TabStop = false;
			this.DataTextBox.Text = "\r\n";
			// 
			// ECIAssignLabel
			// 
			this.ECIAssignLabel.AutoSize = true;
			this.ECIAssignLabel.Location = new System.Drawing.Point(370, 390);
			this.ECIAssignLabel.Name = "ECIAssignLabel";
			this.ECIAssignLabel.Size = new System.Drawing.Size(137, 16);
			this.ECIAssignLabel.TabIndex = 2;
			this.ECIAssignLabel.Text = "ECI Assignment Value";
			// 
			// ECIValueLabel
			// 
			this.ECIValueLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ECIValueLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ECIValueLabel.Location = new System.Drawing.Point(513, 386);
			this.ECIValueLabel.Name = "ECIValueLabel";
			this.ECIValueLabel.Size = new System.Drawing.Size(67, 20);
			this.ECIValueLabel.TabIndex = 3;
			this.ECIValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// QRCodeDecoderDemo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.ClientSize = new System.Drawing.Size(624, 561);
			this.Controls.Add(this.ECIValueLabel);
			this.Controls.Add(this.ECIAssignLabel);
			this.Controls.Add(this.ImageFileLabel);
			this.Controls.Add(this.LoadImageButton);
			this.Controls.Add(this.DataTextBox);
			this.Controls.Add(this.DecodedDataLabel);
			this.Controls.Add(this.HeaderLabel);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(640, 600);
			this.Name = "QRCodeDecoderDemo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.OnLoad);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
			this.Resize += new System.EventHandler(this.OnResize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label ImageFileLabel;
		private System.Windows.Forms.Label DecodedDataLabel;
		private System.Windows.Forms.Button LoadImageButton;
		private System.Windows.Forms.Label HeaderLabel;
		private System.Windows.Forms.TextBox DataTextBox;
		private System.Windows.Forms.Label ECIAssignLabel;
		private System.Windows.Forms.Label ECIValueLabel;
		}
}

