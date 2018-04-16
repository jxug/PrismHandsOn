using Android.Speech.Tts;
using PrismHandsOn.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PrismHandsOn.Droid
{
    public class TextToSpeechService : Java.Lang.Object, ITextToSpeechService, TextToSpeech.IOnInitListener
    {
        TextToSpeech _speaker;
        string _text;

        public void Speak(string text)
        {
            _text = text;
            if (_speaker == null)
            {
                _speaker = new TextToSpeech(Forms.Context, this);
            }
            else
            {
                var p = new Dictionary<string, string>();
                _speaker.Speak(_text, QueueMode.Flush, p);
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                var p = new Dictionary<string, string>();
                _speaker.Speak(_text, QueueMode.Flush, p);
            }
        }
        #endregion
    }
}