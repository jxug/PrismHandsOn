# ITextToSpeechServiceの実装：UWP  

次の手順で実装します。

1. ITextToSpeechServiceの実装クラスを作成する  
2. DIコンテナへTextToSpeechServiceを登録する  

## ITextToSpeechServiceの実装クラスを作成する  

PrismHandsOn.UWPプロジェクトにTextToSpeechService.csクラスを作成する  

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

## DIコンテナへTextToSpeechServiceを登録する  

PrismHandsOn.UWPプロジェクトのMainPage.xaml.csを開き、コンストラクタの下に内部クラスPlatformInitializerを作成します。  

```cs
private class PlatformInitializer : IPlatformInitializer
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<ITextToSpeechService, TextToSpeechService>();
    }
}
```

PlatformInitializerをAppクラスにインジェクションします。MainPage.xaml.csを修正してください。  

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

[DeepLinkを利用する](textbook/08-01-DeepLinkを利用する.md)
