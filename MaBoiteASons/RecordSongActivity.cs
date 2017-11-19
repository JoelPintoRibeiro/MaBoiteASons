using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Views;
using System.IO;
using Newtonsoft.Json;

namespace MaBoiteASons
{

    public class dataJson
    {
        public int currentCount { get; set; }
    }

    [Activity(Label = "@string/toolbar_text",NoHistory =true)]
    public class RecordSongActivity : Activity
    {

        private ImageButton _recordSongButton;
        private ImageButton _playSongButton;
        private ImageButton _cancelButton;
        private ImageButton _cancelRecord;
        private ImageButton _saveButton;
        private ImageButton _stopRecord;
        private Chronometer _chrono;
        private View _recordOverR;
        private LinearLayout _audioCommandsLayout;
        private LinearLayout _recordOverLayout;
        private LinearLayout _cancelLayout;
        private LinearLayout _recordButtonsLayout;
        private AudioManager _audioManager = new AudioManager();
        private TextView _audioName;
        private int counter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RecordSong);

            FindViews();
            MakeHandlers();

            using (StreamReader reader = new StreamReader(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "appData.json"))
            {
                string response;
                response = reader.ReadToEnd();
                dataJson responseData = JsonConvert.DeserializeObject<dataJson>(response);
                this.counter = responseData.currentCount;
            }

        }

        private void MakeHandlers()
        {
            _cancelButton.Click += CancelRecord;
            _cancelRecord.Click += CancelRecord;

            _recordSongButton.Click += (sender, e) =>
            {
                _cancelLayout.Visibility = ViewStates.Gone;
                _chrono.Base = SystemClock.ElapsedRealtime();
                _chrono.Start();
                _audioManager.RecordAudio(counter.ToString() + ".3gp");
                _recordButtonsLayout.WeightSum = 2;
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
                _audioManager.PlayAudio(counter.ToString() + ".3gp", true);
            };
            _saveButton.Click += (sender, e) =>
            {
                
                if (String.IsNullOrEmpty(_audioName.Text))
                {
                    _audioName.RequestFocus();
                    _audioName.SetError("Un nom de son est obligatoire",null);
                    return;
                }
                else
                {
                    _audioManager.AddAudio(new Models.AudioFile { Id = this.counter, Name = _audioName.Text });
                    int newCount = counter + 1;
                    string json = JsonConvert.SerializeObject(new dataJson { currentCount = newCount });
                    string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    //write string to file
                    System.IO.File.WriteAllText(path + "appData.json", json);

                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
            };
        }

        void CancelRecord(object sender,EventArgs e)
        {
            string _songFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/records/";
            var path = _songFolder + counter.ToString() + ".3gp";

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void FindViews()
        {
            _recordSongButton = FindViewById<ImageButton>(Resource.Id.recordButton);
            _playSongButton = FindViewById<ImageButton>(Resource.Id.playRecord);
            _cancelRecord = FindViewById<ImageButton>(Resource.Id.cancelRecord);
            _cancelButton = FindViewById<ImageButton>(Resource.Id.cancelRecordButton);
            _saveButton = FindViewById<ImageButton>(Resource.Id.saveRecord);
            _audioCommandsLayout = FindViewById<LinearLayout>(Resource.Id.audioCommandsLayout);
            _recordOverLayout = FindViewById<LinearLayout>(Resource.Id.recordOver);
            _cancelLayout = FindViewById<LinearLayout>(Resource.Id.cancelLayout);
            _recordButtonsLayout = FindViewById<LinearLayout>(Resource.Id.recordButtonLayout);
            _stopRecord = FindViewById<ImageButton>(Resource.Id.stopRecord);
            _chrono = FindViewById<Chronometer>(Resource.Id.chronometerSong);
            _audioName = FindViewById<TextView>(Resource.Id.audioName);
            _recordOverR = FindViewById(Resource.Id.recordOverR);
        }
    }
}

