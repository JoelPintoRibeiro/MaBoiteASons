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
using MaBoiteASons.Models;
using System.Drawing;
using Android.Graphics;
using System.Threading.Tasks;

namespace MaBoiteASons
{
    public class ImageAdapter : BaseAdapter<AudioFile>
    {
        Context context;
        Activity _activity;
        private readonly List<AudioFile> _list;
        int[] thumbIds = {
        Resource.Drawable.sample_2, Resource.Drawable.sample_3,
        Resource.Drawable.sample_4, Resource.Drawable.sample_5,
        Resource.Drawable.sample_6, Resource.Drawable.sample_7,
        Resource.Drawable.sample_0, Resource.Drawable.sample_1,
        Resource.Drawable.sample_2, Resource.Drawable.sample_3,
        Resource.Drawable.sample_4, Resource.Drawable.sample_5,
        Resource.Drawable.sample_6, Resource.Drawable.sample_7,
        Resource.Drawable.sample_0, Resource.Drawable.sample_1,
        Resource.Drawable.sample_2, Resource.Drawable.sample_3,
        Resource.Drawable.sample_4, Resource.Drawable.sample_5,
        Resource.Drawable.sample_6, Resource.Drawable.sample_7
    };

     
        public ImageAdapter(Context c,Activity activity)
        {
            var audio = new AudioManager();
            this._list = audio.GetAllAudios();
            context = c;
            _activity = activity;
        }

        public override int Count
        {
            get { return _list.Count; }
        }

        public override AudioFile this[int position]
        {
            get { return _list[position]; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

 
        // create a new ImageView for each item referenced by the Adapter
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = LayoutInflater.From(_activity).Inflate(Resource.Layout.SongLayout, parent, false);
     
            const string space = " ";
            string description = this[position].Name.PadRight(25, Convert.ToChar(space)).Substring(0, 25);
             var t = view.FindViewById<TextView>(Resource.Id.ContactName);
            var t1 = view.FindViewById<TextView>(Resource.Id.AudioNameSong);
            //alternate the row colours and set the text foreground / background colours
            var bgColor = position % 2 == 0
                              ? Android.Graphics.Color.White
                              : Android.Graphics.Color.LightGray;
            Button but = view.FindViewById<Button>(Resource.Id.button1);
            but.Click += delegate
            {

                new AlertDialog.Builder(context)
                    .SetPositiveButton("Oui", (sender, args) =>
                    {
                        var audioToRemove = this[position];
                        new AudioManager().RemoveAudio(audioToRemove);
                        _list.Remove(audioToRemove);
                        if (_list.Count == 0)
                        {
                            var intent = new Intent(_activity, typeof(MainActivity));
                            _activity.StartActivity(intent);
                            _activity.Finish();
                        }
                        else
                        {
                            NotifyDataSetChanged();
                        }
                    })
                    .SetNegativeButton("Non", (sender, args) =>
                    {
                        // User pressed no 
                    })
                    .SetMessage("Etes vous sur de vouloir supprimer ce son?")
                    .SetTitle("Suppression")
                    .Show();

            };
            Button but1 = view.FindViewById<Button>(Resource.Id.PlayGrid);
            but1.Click += delegate {
                new AudioManager().PlayAudio(this[position].FileName());

            };
            view.SetBackgroundColor(bgColor);
            t1.Text = _list[position].Name;

            t.Text = this[position].Id.ToString();

            return view;
        }


    }
}