using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Bar_Code_App
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void btn_DanhSachSinhVien_Clicked(object sender, EventArgs e)
        {
           
        }

        private void btn_ThemSinhVien_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MH_ThemThongTinSinhVien());
        }

        private void btn_TraCuuSinhVien_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MH_TraCuuSinhVien());
        }
    }
}
