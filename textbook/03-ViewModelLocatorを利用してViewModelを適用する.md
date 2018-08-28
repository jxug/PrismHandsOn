# ViewModelLocatorを利用してViewModelを適用する

## 目的  

* `ViewModelLocator`を利用してViewへViewModelを自動的にバインドする

Prism for Xamarin.FormsではMVVMパターンを採用しています。このため、Viewには1:1に対応するViewのルートとなるViewModelクラスが必要です。

`MainPage`に対し、`MainPageViewModel`を新たに作成し、`MainPageのBindingContext`プロパティに自動的にバインドすることで、ViewとViewModelのバインディングと通知機構を有効化します。

## 手順

1. `MainPageViewModel`の作成  
2. ViewModelのプロパティをViewへバインド

## MainPageViewModelの追加  

ViewModelsフォルダに`MainPageViewModel.cs`を追加し、次のように実装します。

```cs
namespace PrismHandsOn.ViewModels
{
    public class MainPageViewModel
    {
        public string Message { get; } = "Hello, Prism for Xamarin.Forms!";
    }
}
```

## ViewModelのプロパティをViewへバインド  

追加したViewModelの`Message`プロパティを、`MainPage`のラベルにバインドします。

変更前のコードではラベルの`Text`プロパティにはリテラル値が設定されていました。

変更前
```xml
	<Label Text="Welcome to Xamarin.Forms!"
           VerticalOptions="Center"
           HorizontalOptions="Center" />
```

これを`Message`プロパティをバインドするように変更します。

変更後  
```xml
	<Label Text="{Binding Message}"
           VerticalOptions="Center"
           HorizontalOptions="Center" />
```

それでは実行してみましょう。

![](assets/02-01.png)

表示される文字列が

> Welcome to Xamarin.Forms!

から

> Hello, Prism for Xamarin.Forms!

に変更されていれば成功です。

## 補足説明

通常、例えばXAML上でViewに対してViewModelをバインドしようとした場合、次のようなコードが必要です。

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewModels="clr-namespace:PrismHandsOn.ViewModels;assembly=PrismHandsOn"
             x:Class="PrismHandsOn.Views.MainPage">
    <ContentPage.BindingContext>
        <viewModels:MainPageViewModel/>
    </ContentPage.BindingContext>
```

C#で書いた場合はコンストラクタで次のように記述します。

```cs
public MainPage()
{
    InitializeComponent();
    BindingContext = new MainPageViewModel();
}
```

Prismでは基本ケースではViewとViewModelは命名規則により自動的に特定してバインドしてくれます。

|分類|名前空間|クラス名|
|:--|:--|:--|
|View|Xxx.Yyy.Views|FooPage|
|ViewMoldel|Xxx.Yyy.ViewModels|FooPageViewModel|

この役割を果たしてくれているのがViewModelLocator（＋α）になります。

これは一見ちょっとした便利機能に見えますが、実はPrismのアーキテクチャの根幹を支える重要な機能です。Dependency Injectionの起点になっているからです。

演習を進めるうちに、少しその辺りは見えてくるかもしれません。

# Next

[BindableBaseを利用する](04-BindableBaseを利用する.md)
