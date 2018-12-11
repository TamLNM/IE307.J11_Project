using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
using ZXing.Net.Mobile;
using ZXing.Mobile;
using ZXing.Rendering;
using Android.Graphics;
using Android.Content;

namespace Bar_Code_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MH_ThemThongTinSinhVien : ContentPage
	{
		public MH_ThemThongTinSinhVien ()
		{
			InitializeComponent ();
		}
        public void btn_taobarcode_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (contentEntry.Text != string.Empty)
                {
                    var barcode = new ZXingBarcodeImageView
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    };
                    barcode.BarcodeFormat = BarcodeFormat.QR_CODE;
                    barcode.BarcodeOptions.Width = 500;
                    barcode.BarcodeOptions.Height = 500;
                    barcode.BarcodeValue = contentEntry.Text.Trim();
                    // Hiện barcode đã tạo 
                    //img_barcode.Source = barcode.BarcodeValue;
                    //Content = barcode;
                    SaveQRAsImage(contentEntry.Text.Trim());
                }
                else
                {
                    DisplayAlert("Error", "cảnh báo", "OK");
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                DisplayAlert("Error", "Bạn phải nhập giá trị", "OK");
            }

        }
        TaskCompletionSource<string> SaveQRComplete = null;
        public Task SaveQRAsImage(string text)
        {
            SaveQRComplete = new TaskCompletionSource<string>();

            try
            {
                var barcodeWriter = new ZXing.BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 500,
                        Height = 500,
                        Margin = 10
                    }
                };
                barcodeWriter.Renderer = new ZXing.Rendering.BitmapRenderer();
                var bitmap = barcodeWriter.Write(text);
                var stream = new MemoryStream();
                bitmap.Compress(System.Drawing.Bitmap.CompressFormat.Jpeg, 100, stream);
                stream.Position = 0;

                byte[] imageData = stream.ToArray();

                var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
                var pictures = dir.AbsolutePath;
                //định dạng tên file ảnh theo ngày tháng để k0 lưu đè ảnh cũ
                string name = "MY_QR" + DateTime.Now.ToString("ddMMyyyyHHmmssfff") + ".jpeg";

                string filePath = System.IO.Path.Combine(pictures, name);
                File.WriteAllBytes(pictures, imageData);
                //mediascan thêm ảnh vào gallery
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));
                //dùng Android.App.Application.Context thay cho Xamarin.Forms.Forms.Context
                Android.App.Application.Context.SendBroadcast(mediaScanIntent);
                SaveQRComplete.SetResult(filePath);
                return SaveQRComplete.Task;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

        }
    }
}