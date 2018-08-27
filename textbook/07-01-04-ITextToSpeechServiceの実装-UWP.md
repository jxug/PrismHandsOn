# `ITextToSpeechService`の実装：UWP  

次の手順で実装します。

1. `ITextToSpeechService`の実装クラスを作成する
2. DIコンテナへ`TextToSpeechService`を登録する

## `ITextToSpeechService`の実装クラスを作成する

PrismHandsOn.UWPプロジェクトに`TextToSpeechService.cs`クラスを作成する

```cs
using System;
using Windows.UI.Xaml.Controls;
using PrismHandsOn.Models;

namespace PrismHandsOn.UWP
{
    public class TextToSpeechService : ITextToSpeechService
    {
        public async void Speak(string text)
        {
            var mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            var stream = await synth.SynthesizeTextToStreamAsync(text);

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}
```

## DIコンテナへ`TextToSpeechService`を登録する

PrismHandsOn.UWPプロジェクトの`MainPage.xaml.cs`を開き、コンストラクタの下に内部クラス`PlatformInitializer`を作成します。

```cs
private class PlatformInitializer : IPlatformInitializer
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<ITextToSpeechService, TextToSpeechService>();
    }
}
```

`PlatformInitializer`を`App`クラスにインジェクションします。`MainPage.xaml.cs`を修正してください。

■変更前

```cs
public MainPage()
{
    this.InitializeComponent();

    LoadApplication(new PrismHandsOn.App());
}
```

■変更後

```cs
public MainPage()
{
    this.InitializeComponent();

    LoadApplication(new PrismHandsOn.App(new PlatformInitializer()));
}
```

以上で実装は終了です。
SelectedItemPageでSpeakボタンを押下してみましょう。

# Next

[演習問題](90.演習問題.md)