using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WetherApp.Models
{
    //都道府県管理クラス
    [DataContract]
    class Prefecture
    {
        //都道府県名
        [DataMember]
        public string Name { get; set; }

        //都道府県に対応する市町村メンバー
        [DataMember]
        public List<City> Cities { get; set; }
    }

    //市町村管理クラス
    [DataContract]
    class City
    {
        // <summary>
        // 市町村ID
        // </summary>
        [DataMember]
        public string Id { get; set; }

        //市町村名
        [DataMember]
        public string Name { get; set; }
    }
}