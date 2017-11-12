using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MaBoiteASons.Models;
using Android.Media;
using System.IO;
using Android.Content.Res;

namespace MaBoiteASons
{
    public  class DataManager
    {


        //public string GetCount()
        //{

        //    AssetManager assets = this.Assets;

        //    string response;

        //    StreamReader strm = new StreamReader(Assets.Open("form.json"));
        //    response = strm.ReadToEnd();

        //    List<Vraag> vragen = JsonConvert.DeserializeObject<List<Vraag>>(response);

        //    foreach (Vraag vraag in vragen)
        //    {
        //        if (vraag.type == "textField")
        //        {
        //            var editText = new EditText(this);
        //            editText.Text = "Dit is vraag: " + vraag.id + ".";
        //            editText.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
        //            layout.AddView(editText);
        //        }
        //    }
        //    return "";
        //}
 

    }
}