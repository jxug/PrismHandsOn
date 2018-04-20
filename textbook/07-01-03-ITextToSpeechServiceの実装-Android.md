# ITextToSpeechServiceの実装：Android  

次の手順で実装します。

1. ITextToSpeechServiceの実装クラスを作成する  
2. DIコンテナへTextToSpeechServiceを登録する  

## ITextToSpeechServiceの実装クラスを作成する  

PrismHandsOn.AndroidプロジェクトにTextToSpeechService.csクラスを作成します。  

```cs
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
                _speaker.Speak(_text, QueueMode.Flush, null, "messageId");
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                _speaker.Speak(_text, QueueMode.Flush, null, "messageId");
            }
        }
        #endregion
    }
}
```

## DIコンテナへTextToSpeechServiceを登録する  

PrismHandsOn.AndroidプロジェクトのMainActivityを開き、OnCreateメソッドの下に内部クラスPlatformInitializerを作成します。  

```cs
private class PlatformInitializer : IPlatformInitializer
{
    private readonly Context _context;

    public PlatformInitializer(Context context)
    {
        _context = context;
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<ITextToSpeechService, TextToSpeechService>();
        containerRegistry.RegisterInstance(_context);
    }
}
```

PlatformInitializerをAppクラスにインジェクションします。MainActivityを修正してください。  

■変更前  
```cs
protected override void OnCreate(Bundle bundle)
{
    ...
    LoadApplication(new App());
}
```

■変更後
```cs
protected override void OnCreate(Bundle bundle)
{
    ...
    LoadApplication(new App(new PlatformInitializer(this)));
}
```

以上で実装は終了です。
SelectedItemPageでSpeakボタンを押下してみましょう。

# Next

[DeepLinkを利用する](08-01-DeepLinkを利用する.md)
