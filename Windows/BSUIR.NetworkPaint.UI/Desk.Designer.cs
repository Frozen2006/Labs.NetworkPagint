namespace BSUIR.NetworkPaint.UI
{
	partial class Desk
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
			this.paintSurface = new System.Windows.Forms.PictureBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.label3 = new System.Windows.Forms.Label();
			this.colorDisplay = new System.Windows.Forms.TextBox();
			this.pointRadio = new System.Windows.Forms.RadioButton();
			this.rectabgleRadio = new System.Windows.Forms.RadioButton();
			this.lineRadio = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.paintSurface)).BeginInit();
			this.SuspendLayout();
			// 
			// paintSurface
			// 
			this.paintSurface.Location = new System.Drawing.Point(3, -1);
			this.paintSurface.Name = "paintSurface";
			this.paintSurface.Size = new System.Drawing.Size(1280, 640);
			this.paintSurface.TabIndex = 0;
			this.paintSurface.TabStop = false;
			this.paintSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.paintSurface_MouseDown);
			this.paintSurface.MouseLeave += new System.EventHandler(this.paintSurface_MouseLeave);
			this.paintSurface.MouseMove += new System.Windows.Forms.MouseEventHandler(this.paintSurface_MouseMove);
			this.paintSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.paintSurface_MouseUp);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(1096, 663);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "Current color:";
			// 
			// colorDisplay
			// 
			this.colorDisplay.BackColor = System.Drawing.Color.Black;
			this.colorDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.colorDisplay.Location = new System.Drawing.Point(1188, 659);
			this.colorDisplay.Name = "colorDisplay";
			this.colorDisplay.ReadOnly = true;
			this.colorDisplay.Size = new System.Drawing.Size(83, 22);
			this.colorDisplay.TabIndex = 4;
			this.colorDisplay.Click += new System.EventHandler(this.colorDisplay_Click);
			// 
			// pointRadio
			// 
			this.pointRadio.AutoSize = true;
			this.pointRadio.Checked = true;
			this.pointRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pointRadio.Location = new System.Drawing.Point(35, 661);
			this.pointRadio.Name = "pointRadio";
			this.pointRadio.Size = new System.Drawing.Size(63, 24);
			this.pointRadio.TabIndex = 5;
			this.pointRadio.TabStop = true;
			this.pointRadio.Text = "Point";
			this.pointRadio.UseVisualStyleBackColor = true;
			this.pointRadio.CheckedChanged += new System.EventHandler(this.pointRadio_CheckedChanged);
			// 
			// rectabgleRadio
			// 
			this.rectabgleRadio.AutoSize = true;
			this.rectabgleRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rectabgleRadio.Location = new System.Drawing.Point(132, 661);
			this.rectabgleRadio.Name = "rectabgleRadio";
			this.rectabgleRadio.Size = new System.Drawing.Size(100, 24);
			this.rectabgleRadio.TabIndex = 6;
			this.rectabgleRadio.Text = "Rectangle";
			this.rectabgleRadio.UseVisualStyleBackColor = true;
			this.rectabgleRadio.CheckedChanged += new System.EventHandler(this.rectabgleRadio_CheckedChanged);
			// 
			// lineRadio
			// 
			this.lineRadio.AutoSize = true;
			this.lineRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lineRadio.Location = new System.Drawing.Point(238, 663);
			this.lineRadio.Name = "lineRadio";
			this.lineRadio.Size = new System.Drawing.Size(57, 24);
			this.lineRadio.TabIndex = 7;
			this.lineRadio.Text = "Line";
			this.lineRadio.UseVisualStyleBackColor = true;
			this.lineRadio.CheckedChanged += new System.EventHandler(this.lineRadio_CheckedChanged);
			// 
			// Desk
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1283, 699);
			this.Controls.Add(this.lineRadio);
			this.Controls.Add(this.rectabgleRadio);
			this.Controls.Add(this.pointRadio);
			this.Controls.Add(this.colorDisplay);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.paintSurface);
			this.Name = "Desk";
			this.Text = "Interactive desk";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Desk_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.paintSurface)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox paintSurface;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox colorDisplay;
		private System.Windows.Forms.RadioButton pointRadio;
		private System.Windows.Forms.RadioButton rectabgleRadio;
		private System.Windows.Forms.RadioButton lineRadio;
	}
}

