using Android.Speech.Tts;
using PrismHandsOn.Models;
using System.Collections.Generic;
using Android.Content;

namespace PrismHandsOn.Droid
{
    public class TextToSpeechService : Java.Lang.Object, ITextToSpeechService, TextToSpeech.IOnInitListener
    {
        private readonly Context _context;
        private TextToSpeech _speaker;
        private string _text;

        public TextToSpeechService(Context context)
        {
            _context = context;
        }

        public void Speak(string text)
        {
            _text = text;
            if (_speaker == null)
            {
                _speaker = new TextToSpeech(_context, this);
            }
            else
            {
                var p = new Dictionary<string, string>();
                _speaker.Speak(_text, QueueMode.Flush, null, "messageId");
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                var p = new Dictionary<string, string>();
                _speaker.Speak(_text, QueueMode.Flush, null, "messageId");
            }
        }
        #endregion
    }
}