using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenCapture
{
	/// <summary>
	/// 截取结果
	/// </summary>
	public class CaptureResult
	{
		/// <summary>
		/// 截取的矩形范围
		/// </summary>
		public Rectangle Rect { get; set; }

		/// <summary>
		/// 截取的位图数据
		/// </summary>
		public Bitmap Image { get; set; }
	}

	/// <summary>
	/// 截图静态帮助类
	/// </summary>
	public static class CaptureAgent
	{
		/// <summary>
		/// 截取屏幕
		/// </summary>
		/// <param name="dpi">DPI，为0则使用设备默认值</param>
		/// <returns>截取结果对象</returns>
		public static CaptureResult Capture(int dpi = 0)
		{
			return Capture(new Rectangle(), dpi);
		}

		/// <summary>
		/// 截取屏幕
		/// </summary>
		/// <param name="rect">初始矩形范围</param>
		/// <param name="dpi">DPI，为0则使用设备默认值</param>
		/// <returns>截取结果对象</returns>
		public static CaptureResult Capture(Rectangle rect, int dpi = 0)
		{
			MainForm form = new MainForm();
			form.CapturedRect = rect;
			form.DPI = dpi;

			if (form.ShowDialog() != DialogResult.OK)
			{
				return null;
			}

			if (!form.IsValid)
			{
				return null;
			}

			return new CaptureResult()
			{
				Rect = form.CapturedRect,
				Image = form.CapturedBmp,
			};
		}
	}
}
