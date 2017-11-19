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
using Newtonsoft.Json;
using System.IO;

namespace MaBoiteASons
{
    public class AudioManager
    {
        private List<AudioFile> _listAudio = new List<AudioFile>();
        private MediaRecorder _recorder { get; set; }
        private MediaPlayer _player { get; set; }

        private string _songFolder { get; set; }

        public AudioManager()
        {
            _recorder = new MediaRecorder(); // Initial state.
            _player = new MediaPlayer();
            _songFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/records/";
            using (StreamReader reader = new StreamReader(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "recordsAudio.json"))
            {
                string response;
                response = reader.ReadToEnd();
                List<AudioFile> responseData = JsonConvert.DeserializeObject<List<AudioFile>>(response);
                _listAudio = responseData;
            }
        }

        public void PlayAudio(string filename)
        {
            var path = _songFolder + filename;

            _player.Reset();
            _player.SetDataSource(path);
            _player.Prepare();
            _player.Start();
        }

        public void RecordAudio(string filename)
        {
            try
            {

                var path = _songFolder + "temp/" + filename;

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
            string json;

            File.Move(_songFolder + "temp/" + audioFile.FileName(), _songFolder + audioFile.FileName());
            using (StreamReader reader = new StreamReader(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "recordsAudio.json"))
            {
                string response;
                response = reader.ReadToEnd();
                List<AudioFile> responseData = JsonConvert.DeserializeObject<List<AudioFile>>(response);
                responseData.Add(audioFile);
                json = JsonConvert.SerializeObject(responseData);
            }

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //write string to file
            System.IO.File.WriteAllText(path + "recordsAudio.json", json);

            _listAudio.Add(audioFile);
        }

        public void RemoveAudio(AudioFile audioFile)
        {
            string json;

            File.Delete(_songFolder + audioFile.FileName());
            using (StreamReader reader = new StreamReader(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "recordsAudio.json"))
            {
                string response;
                response = reader.ReadToEnd();
                List<AudioFile> responseData = JsonConvert.DeserializeObject<List<AudioFile>>(response);
                var item = responseData.Where(s=>s.Id==audioFile.Id).FirstOrDefault();
                responseData.Remove(item);
                json = JsonConvert.SerializeObject(responseData);
            }

            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //write string to file
            System.IO.File.WriteAllText(path + "recordsAudio.json", json);

            _listAudio.Remove(audioFile);
        }

        public List<AudioFile> GetAllAudios()
        {
            return _listAudio;
        }

    }
}