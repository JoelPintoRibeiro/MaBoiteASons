﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MaBoiteASons.Models
{
    public class AudioFile
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string FileName()
        {

          return this.Id+".3gp";
        }


    }
}