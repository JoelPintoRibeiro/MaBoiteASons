using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace MaBoiteASons
{
    [Activity(Label = "@string/toolbar_text")]
    public class RecordSongActivity : Activity
    {

        private ImageButton _recordSongButton;
        private ImageButton _playSongButton;
        private ImageButton _cancelButton;
        private ImageButton _saveButton;
        private ImageButton _stopRecord;
        private Chronometer _chrono;
        private LinearLayout _audioCommandsLayout;
        private LinearLayout _recordOverLayout;
        private AudioManager _audioManager = new AudioManager();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            _audioManager.AddAudio(new Models.AudioFile { FileName = "test", Name = "name" });
     
            SetContentView(Resource.Layout.RecordSong);
    
            FindViews();
            MakeHandlers();

        }

        private void MakeHandlers()
        {
            _cancelButton.Click += (sender ,e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };
            _recordSongButton.Click += (sender, e) =>
            {
                _chrono.Start();
                _audioManager.RecordAudio("audio.3gp");
                _audioCommandsLayout.Visibility = Android.Views.ViewStates.Visible;
            };
            _stopRecord.Click += (sender, e) =>
            {
                _chrono.Stop();
                _recordOverLayout.Visibility = Android.Views.ViewStates.Visible;
                _audioCommandsLayout.Visibility = Android.Views.ViewStates.Gone;
                _audioManager.StopRecord();

            };
            _playSongButton.Click += (sender, e) =>
            {
                _audioManager.PlayAudio("audio.3gp");
            };
        }

        private void FindViews()
        {
            _recordSongButton = FindViewById<ImageButton>(Resource.Id.recordButton);
            _playSongButton = FindViewById<ImageButton>(Resource.Id.playRecord);
            _cancelButton = FindViewById<ImageButton>(Resource.Id.cancelRecord);
            _saveButton = FindViewById<ImageButton>(Resource.Id.saveRecord);
            _audioCommandsLayout = FindViewById<LinearLayout>(Resource.Id.audioCommandsLayout);
            _recordOverLayout = FindViewById<LinearLayout>(Resource.Id.recordOver);
            _stopRecord = FindViewById<ImageButton>(Resource.Id.stopRecord);
            _chrono = FindViewById<Chronometer>(Resource.Id.chronometerSong);
        }
    }
}

