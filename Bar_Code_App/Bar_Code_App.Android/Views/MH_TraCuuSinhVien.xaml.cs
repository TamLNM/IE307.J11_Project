using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Bar_Code_App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]

	public partial class MH_TraCuuSinhVien : ContentPage
	{
        ZXingScannerPage scanPage;

        public MH_TraCuuSinhVien ()
		{
			InitializeComponent ();

            btnScanDefault.Clicked += btnScanDefault_ClickedAsync;
        }

        // Add btnScanDefault click event
        private async void btnScanDefault_ClickedAsync(object sender, EventArgs e)
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                // Do ... with result
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopModalAsync();
                    DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };
            await Navigation.PushModalAsync(scanPage);
        }

        private void btn_TraCuuTheoMSSV_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MH_TraCuuTheoMSSV());
        }
    }
}