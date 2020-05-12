using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//追加
using Windows.Storage;
using System.Text;
using WeatherApp.Models;
using System.Runtime.Serialization.Json;


// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace WetherApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Dictionary<string, List<City>> _city = new Dictionary<string, List<City>>();

        public MainPage()
        {
            this.InitializeComponent();

            ReadJson();
        }

        ///<summary>
        ///Assets フォルダの JSON ファイルを読み込む
        /// </summary>
        private async void ReadJson()
        {
            // Assets からのファイル取り出し
            var file = await StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///Assets/Prefecture.json"));

                //ファイルの読み込み
                string json = await FileIO.ReadTextAsync(file);

                //json データからクラスへのデシリアライズ
                List<Prefecture> pref;
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    //List<Prefecture> に変換できるシリアライザーを作成
                    var serializer = new DataContractJsonSerializer(typeof(List<Prefecture>));

                    //クラスにデータを読み込む
                    pref = serializer.ReadObject(stream) as List<Prefecture>;
                }

                //市町村データを取得する
                foreach (var item in pref)
                {
                    //県名と市の集まりをcity に追加
                    _city.Add(item.Name, item.Cities);
                }

                //都道府県を cmbP/>refectune にセット
                cmbPrefecture.ItemsSource = pref;
                cmbPrefecture.DisplayMemberPath = "NAME";
                cmbPrefecture.SelectedValuePath = "NAME";
                cmbPrefecture.SelectedIndex = 0;
        }
    }
}
