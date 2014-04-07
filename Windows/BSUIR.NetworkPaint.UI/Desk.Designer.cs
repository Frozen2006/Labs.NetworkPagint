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
			// 
			// Desk
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1283, 699);
			this.Controls.Add(this.paintSurface);
			this.Name = "Desk";
			this.Text = "Interactive desk";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Desk_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.paintSurface)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox paintSurface;
	}
}

