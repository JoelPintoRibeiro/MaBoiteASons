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

namespace MaBoiteASons
{
    public class AudioManager
    {
        private List<AudioFile> _listAudio = new List<AudioFile>();

        public AudioManager()
        {

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