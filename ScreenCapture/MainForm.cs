using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenCapture
{
	partial class MainForm : Form
	{
		public Rectangle CapturedRect { get; set; }
		public Bitmap CapturedBmp { get; private set; }
		public int DPI { get; set; }
		public bool IsValid => CapturedRect.Width > 0 && CapturedRect.Height > 0;

		private Point m_startPoint;
		private Bitmap m_screenBmp = null;
		private Brush m_brush = null;
		private Pen m_pen = null;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			timer1.Enabled = false;
			m_brush = new SolidBrush(Color.Red);
			m_pen = new Pen(m_brush, 2);
			CapturedBmp = null;

			int PenWidth = 3;
			Rectangle rect = Screen.PrimaryScreen.Bounds;

			Visible = false; // 截屏开始，先隐藏自身
			m_screenBmp = new Bitmap(rect.Width, rect.Height);
			if (DPI > 0)
			{
				m_screenBmp.SetResolution(DPI, DPI);
			}

			using (Graphics g = Graphics.FromImage(m_screenBmp))
			{
				g.CopyFromScreen(0, 0, 0, 0, rect.Size);
				Brush brush = new SolidBrush(Color.GreenYellow);
				Pen pen = new Pen(brush, PenWidth);
				g.DrawRectangle(pen, new Rectangle(0, 0, rect.Width - PenWidth, rect.Height - PenWidth));
			}
			Visible = true; // 截屏结束

			pictureBox1.Left = 0;
			pictureBox1.Top = 0;
			pictureBox1.Width = rect.Width;
			pictureBox1.Height = rect.Height;			
			DrawCapturedRect();			
		}

		private void CaptureScreen()
		{
			int PenWidth = 3;
			Rectangle rect = Screen.PrimaryScreen.Bounds;
			m_screenBmp = new Bitmap(rect.Width, rect.Height);

			using (Graphics g = Graphics.FromImage(m_screenBmp))
			{
				g.CopyFromScreen(0, 0, 0, 0, rect.Size);
				Brush brush = new SolidBrush(Color.GreenYellow);
				Pen pen = new Pen(brush, PenWidth);
				g.DrawRectangle(pen, new Rectangle(0, 0, rect.Width - PenWidth, rect.Height - PenWidth));
			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Return)
			{
				DialogResult = DialogResult.Cancel;
				Close();
			}
		}

		private void DrawCapturedRect()
		{
			if (IsValid)
			{
				Bitmap newBmp = new Bitmap(m_screenBmp);
				using (Graphics g = Graphics.FromImage(newBmp))
				{
					g.DrawRectangle(m_pen, CapturedRect.X, CapturedRect.Y, CapturedRect.Width, CapturedRect.Height);
				}

				pictureBox1.Image = newBmp;
			}
			else
			{
				pictureBox1.Image = m_screenBmp;
			}			
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			Point cursor = Cursor.Position;
			int x = Math.Min(m_startPoint.X, cursor.X);
			int y = Math.Min(m_startPoint.Y, cursor.Y);
			int width = Math.Abs(cursor.X - m_startPoint.X);
			int height = Math.Abs(cursor.Y - m_startPoint.Y);
			Rectangle rect = new Rectangle(x, y, width, height);
			if (rect != CapturedRect)
			{
				CapturedRect = rect;
				DrawCapturedRect();
			}
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			m_startPoint = Cursor.Position;
			timer1.Enabled = true;
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			timer1.Enabled = false;
			DialogResult = IsValid ? DialogResult.OK : DialogResult.Cancel;
			Close();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (IsValid)
			{
				CapturedBmp = new Bitmap(CapturedRect.Width, CapturedRect.Height);
				if (DPI > 0)
				{
					CapturedBmp.SetResolution(DPI, DPI);
				}
				using (Graphics g = Graphics.FromImage(CapturedBmp))
				{
					g.DrawImage(m_screenBmp, new Rectangle(0, 0, CapturedRect.Width, CapturedRect.Height), CapturedRect, GraphicsUnit.Pixel);
				}
			}
			else
			{
				CapturedBmp = null;
			}
		}
	}
}
