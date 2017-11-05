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

namespace MaBoiteASons
{
    public class AudioManager
    {
        private List<AudioFile> _listAudio = new List<AudioFile>();
        private MediaRecorder _recorder { get; set; }
        private MediaPlayer _player { get; set; }

       

        public AudioManager()
        {
            _recorder = new MediaRecorder(); // Initial state.
            _player = new MediaPlayer();
        }

        public void PlayAudio(string filename)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + filename;

            _player.Reset();
            _player.SetDataSource(path);
            _player.Prepare();
            _player.Start();
        }

        public void RecordAudio(string filename)
        {
            try
            {

                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + filename;

                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                _recorder.Reset();
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                _recorder.SetAudioEncoder(AudioEncoder.AmrNb);
                // Initialized state.
                _recorder.SetOutputFile(path);
                // DataSourceConfigured state.
                _recorder.Prepare(); // Prepared state
                _recorder.Start(); // Recording state.

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public void StopRecord()
        {
            try
            {
                //if (File.Exists("./"))
                //{
                //    File.Delete("./");
                //}


                _recorder.Stop();
                _recorder.Reset();


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        public void AddAudio(AudioFile audioFile)
        {
            _listAudio.Add(audioFile);
        }

        public void RemoveAudio(AudioFile audioFile)
        {
            _listAudio.Add(audioFile);
        }

        public List<AudioFile> GetAllAudios()
        {
            return _listAudio;
        }

    }
}