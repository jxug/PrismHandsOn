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
```

## DIコンテナへTextToSpeechServiceを登録する  

PrismHandsOn.AndroidプロジェクトのMainActivityを開き、OnCreateメソッドの下に内部クラスPlatformInitializerを作成します。  

```cs
private class PlatformInitializer : IPlatformInitializer
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<ITextToSpeechService, TextToSpeechService>();
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
    LoadApplication(new App(new PlatformInitializer()));
}
```

以上で実装は終了です。
SelectedItemPageでSpeakボタンを押下してみましょう。

# Next

[演習問題](90.演習問題.md)