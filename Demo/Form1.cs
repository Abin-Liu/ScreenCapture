using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ScreenCapture.MainForm form = new ScreenCapture.MainForm();
			form.ShowDialog(this);
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			ScreenCapture.MainForm form = new ScreenCapture.MainForm();
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				pictureBox1.Image = form.CapturedBmp;
				label1.Text = "Region: " + form.CapturedRect.ToString();
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
