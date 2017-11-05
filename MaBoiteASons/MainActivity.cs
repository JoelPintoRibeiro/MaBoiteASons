using Android.App;
using Android.Widget;
using Android.OS;

namespace MaBoiteASons
{
    [Activity(Label = "@string/toolbar_text", MainLauncher = true)]
    public class MainActivity : Activity
    {

        private GridView _songsGridView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            FindViews();

            _songsGridView.Adapter = new ImageAdapter(this);

            MakeHandlers();


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

        }
        private void MakeHandlers()
        {

            _songsGridView.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args) {
                Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
            };
        }
        private void FindViews()
        {
            _songsGridView = FindViewById<GridView>(Resource.Id.gridviewSongs);
        }
    }
}

