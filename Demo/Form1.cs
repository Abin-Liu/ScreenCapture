using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScreenCapture;

namespace Demo
{
	public partial class Form1 : Form
	{
		Rectangle m_rect;
		public Form1()
		{
			InitializeComponent();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			CaptureResult result = CaptureAgent.Capture(m_rect);
			if (result == null)
			{
				return;
			}

			m_rect = result.Rect;
			pictureBox1.Image = result.Image;
			label1.Text = result.Rect.ToString();
		}		

		private void button1_Click_1(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
