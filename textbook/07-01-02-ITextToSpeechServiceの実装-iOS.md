# ITextToSpeechServiceの実装：iOS  

次の手順で実装します。

1. ITextToSpeechServiceの実装クラスを作成する  
2. DIコンテナへTextToSpeechServiceを登録する  

## ITextToSpeechServiceの実装クラスを作成する  

PrismHandsOn.iOSプロジェクトにTextToSpeechService.csクラスを作成します。  

```cs
using AVFoundation;
using PrismHandsOn.Models;

namespace PrismHandsOn.iOS
{
    public class TextToSpeechService : ITextToSpeechService
    {
        public void Speak(string text)
        {
            var speechSynthesizer = new AVSpeechSynthesizer();

            var speechUtterance = new AVSpeechUtterance(text)
            {
                Rate = AVSpeechUtterance.MaximumSpeechRate / 4,
                Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
                Volume = 0.5f,
                PitchMultiplier = 1.0f
            };

            speechSynthesizer.SpeakUtterance(speechUtterance);
        }
    }
}
```

## DIコンテナへTextToSpeechServiceを登録する  

PrismHandsOn.iOSプロジェクトのAppDelegateを開き、FinishedLaunchingメソッドの下に内部クラスPlatformInitializerを作成します。  

```cs
private class PlatformInitializer : IPlatformInitializer
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<ITextToSpeechService, TextToSpeechService>();
    }
}
```

PlatformInitializerをAppクラスにインジェクションする。  
AppDelegateのFinishedLaunchingを修正する。  

■変更前  
```cs
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    global::Xamarin.Forms.Forms.Init();
    LoadApplication(new App());

    return base.FinishedLaunching(app, options);
}
```

■変更後
```cs
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    global::Xamarin.Forms.Forms.Init();
    LoadApplication(new App(new PlatformInitializer()));

    return base.FinishedLaunching(app, options);
}
```

以上で実装は終了です。
SelectedItemPageでSpeakボタンを押下してみましょう。

# Next

[演習問題](90.演習問題.md)