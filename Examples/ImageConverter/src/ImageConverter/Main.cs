using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace ImageConverter
{
	public partial class Main : Form
	{
		private readonly Int32 _initialBitmapWidth;
		private readonly Int32 _initialBitmapHeight;

		private String _filePath;
		private Bitmap _bitmap;


		public Main()
		{
			InitializeComponent();

			_initialBitmapWidth = PictureBox.Size.Width;
			_initialBitmapHeight = PictureBox.Size.Height;

			SaveFormatCb.SelectedIndex = 0;
			PictureBox.AllowDrop = true;
		}


		// HANDLERS ///////////////////////////////////////////////////////////////////////////////
		private void OpenBtn_Click(Object sender, EventArgs e)
		{
			var ofd = new OpenFileDialog
			{
				Filter = "Image files|*.bmp;*.png;*.jpg;*.gif;*.tif;*.webp",
				Title = "Open image file",
				RestoreDirectory = true
			};

			if(ofd.ShowDialog() != DialogResult.OK)
				return;

			LoadImage(ofd.FileName);
		}
		private void PictureBox_DragEnter(Object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}
		private void PictureBox_DragDrop(Object sender, DragEventArgs e)
		{
			if(!(e.Data.GetData(DataFormats.FileDrop) is String[] fileNames))
				return;

			LoadImage(fileNames[0]);
		}
		private void SaveBtn_Click(Object sender, EventArgs e)
		{
			SaveImage();
		}


		// FUNCTIONS //////////////////////////////////////////////////////////////////////////////
		private void LoadImage(String filePath)
		{
			_filePath = filePath.Split('\\').Last().Split('.')[0];
			_bitmap = new Bitmap(filePath);

			if(_bitmap.Width > _bitmap.Height)
			{
				PictureBox.Width = _initialBitmapWidth;
				PictureBox.Height = (Int32)((Double)_bitmap.Height * _initialBitmapWidth / _bitmap.Width);
			}
			else
			{
				PictureBox.Height = _initialBitmapHeight;
				PictureBox.Width = (Int32)((Double)_bitmap.Width * _initialBitmapHeight / _bitmap.Height);
			}

			PictureBox.Image = _bitmap;
		}
		private void LoadWebpImage(String filePath)
		{
			// https://stackoverflow.com/questions/13220436/convert-bitmap-to-webp-image
			// https://developers.google.com/speed/webp/docs/cwebp
			// https://developers.google.com/speed/webp/docs/dwebp

			//using(Bitmap image = WebPFormat.LoadFromStream(new FileStream("image.webp", FileMode.Open, FileAccess.Read)))
			//{
			//	image.Save("image.png", ImageFormat.Png);
			//}
		}
		private void SaveImage()
		{
			switch(SaveFormatCb.Text)
			{
				case "*.bmp":
					_bitmap.Save($"{_filePath}{SaveFormatCb.Text}", ImageFormat.Bmp);
					break;

				case "*.jpg":
					_bitmap.Save($"{_filePath}{SaveFormatCb.Text}", ImageFormat.Jpeg);
					break;

				case "*.gif":
					_bitmap.Save($"{_filePath}{SaveFormatCb.Text}", ImageFormat.Gif);
					break;

				case "*.tif":
					_bitmap.Save($"{_filePath}{SaveFormatCb.Text}", ImageFormat.Tiff);
					break;
			}
		}
	}
}
