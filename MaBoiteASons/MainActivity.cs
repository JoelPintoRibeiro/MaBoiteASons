using Android.App;
using Android.Widget;
using Android.OS;

namespace MaBoiteASons
{
    [Activity(Label = "@string/toolbar_text", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            var audioManager = new AudioManager();

            if (audioManager.GetAllAudios().Count == 0)
            {

            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

