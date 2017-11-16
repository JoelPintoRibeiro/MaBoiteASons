using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MaBoiteASons
{
    [Activity(Label = "@string/toolbar_text", MainLauncher = true)]
    public class MainActivity : Activity
    {

        private GridView _songsGridView;
        private Button _addSong;

        private bool resetFiles = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            if (!Directory.Exists(path + "/records"))
            {
                Directory.CreateDirectory(path + "/records");
            }
            if (!Directory.Exists(path + "/records/temp"))
            {
                Directory.CreateDirectory(path + "/records/temp");
            }
            if (resetFiles)
            {
                File.Delete(path + "appData.json");
                File.Delete(path + "recordsAudio.json");
            }

            if (!File.Exists(path + "appData.json"))
            {
                string json = JsonConvert.SerializeObject(new dataJson { currentCount = 1});

                //write string to file
                System.IO.File.WriteAllText(path + "appData.json", json);
            
            }

            if (!File.Exists(path + "recordsAudio.json"))
            {
                string jsons= JsonConvert.SerializeObject(new List<AudioManager>());

                //write string to file
                System.IO.File.WriteAllText(path + "recordsAudio.json", jsons);

            }


            var audioManager = new AudioManager();

            if (audioManager.GetAllAudios().Count == 0)
            {
                SetContentView(Resource.Layout.Main);
           
            }
            else
            {
                SetContentView(Resource.Layout.MainSongs);
                _songsGridView = FindViewById<GridView>(Resource.Id.gridviewSong);
                _songsGridView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
               
                    var t = args.View.FindViewById<TextView>(Resource.Id.ContactName);
  
                    audioManager.PlayAudio(t.Text + ".3gp");
                };

                _songsGridView.Adapter = new ImageAdapter(this,this);

            }

            FindViews();



            MakeHandlers();



        }

        private void MakeHandlers()
        {

 
            _addSong.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(RecordSongActivity));
                StartActivity(intent);
            };
        }
        private void FindViews()
        {
            _addSong = FindViewById<Button>(Resource.Id.addRecord);
        }
    }
}

