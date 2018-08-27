# `ITextToSpeechService`の実装：iOS  

次の手順で実装します。

1. `ITextToSpeechService`の実装クラスを作成する
2. DIコンテナへ`TextToSpeechService`を登録する

## `ITextToSpeechService`の実装クラスを作成する

PrismHandsOn.iOSプロジェクトに`TextToSpeechService.cs`クラスを作成します。

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

## DIコンテナへ`TextToSpeechService`を登録する

PrismHandsOn.iOSプロジェクトの`AppDelegate`を開き、`FinishedLaunching`メソッドの下に内部クラス`PlatformInitializer`を作成します。

```cs
private class PlatformInitializer : IPlatformInitializer
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<ITextToSpeechService, TextToSpeechService>();
    }
}
```

`PlatformInitializer`を`App`クラスにインジェクションする。
`AppDelegate`の`FinishedLaunching`を修正する。

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
`SelectedItemPage`でSpeakボタンを押下してみましょう。

# Next

[DeepLinkを利用する](08-01-DeepLinkを利用する.md)
