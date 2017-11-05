using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System;

namespace MaBoiteASons
{
    [Activity(Label = "@string/toolbar_text", MainLauncher = true)]
    public class MainActivity : Activity
    {

        private GridView _songsGridView;
        private Button _addSong;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            var audioManager = new AudioManager();
            audioManager.AddAudio(new Models.AudioFile { FileName = "test", Name = "name" });
            if (audioManager.GetAllAudios().Count == 0)
            {
                SetContentView(Resource.Layout.Main);
            }
            else
            {
                SetContentView(Resource.Layout.MainSongs);
            }

            FindViews();

            _songsGridView.Adapter = new ImageAdapter(this);

            MakeHandlers();



        }

        private void MakeHandlers()
        {

            _songsGridView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
                Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
            };
            _addSong.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(RecordSongActivity));
                StartActivity(intent);
            };
        }
        private void FindViews()
        {
            _songsGridView = FindViewById<GridView>(Resource.Id.gridviewSong);
            _addSong = FindViewById<Button>(Resource.Id.addRecord);
        }
    }
}

