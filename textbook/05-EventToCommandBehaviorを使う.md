# `EventToCommandBehavior`を使う

## 目的

* Commandを直接バインドできないイベントへCommandをバインドする

MVVMパターンを実装しているとよく遭遇する「あるある」に

「特定のイベントが発行されたらViewModelのCommandを実行したいのにCommandがバインドできない！」

というものがあります。

そこでPrismでは`EventToCommandBehavior`を提供しており、それを利用することで、あらゆるイベントから簡単にCommandを実行できる手段を提供しています。

ここでは、MainPage起動時の`Appearing`イベント発生時に、画面の表示メッセージを更新するよう実装します。

## 手順

具体的な手順は次の通りです。

1. `MainPageViewModel.cs`に`Appearing`イベントに対応するCommandを定義する
2. `MainPage.xaml`に`EventToCommandBehavior`を定義する

## MainPageViewModel.csにAppearingイベントに対応するCommandを定義する

`MainPageViewModel.cs`にCommandを定義し、Command実行時に`Message`を更新するよう実装します。

```cs
public ICommand AppearingCommand => new Command(() => Message = $"Appearing on {DateTime.Now}");
```

## `MainPage.xaml`に`EventToCommandBehavior`を定義する

つづいてPageの`Appearing`イベント発生時に、`AppearingCommand`を実行するよう、`MainPage.xaml`を更新します。

`ContentPage`の属性に「xmlns:behaviors=～」の宣言を追加するのを忘れないように注意しましょう。

変更前

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:PrismHandsOn"
             x:Class="PrismHandsOn.Views.MainPage">
             ...
```

変更後
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:PrismHandsOn"
             xmlns:behaviors="clr-namespace:Prism.Behaviors;assembly=Prism.Forms"
             x:Class="PrismHandsOn.Views.MainPage">
    <ContentPage.Behaviors>
        <behaviors:EventToCommandBehavior EventName="Appearing" Command="{Binding AppearingCommand}"/>
    </ContentPage.Behaviors>
    ...
```

ContentPageに`EventToCommandBehavior`が追記され、EventNameに`Appearing`が、Commandに`UpdateMessageCommand`が設定されているのが見て取れるでしょう。


それでは実行してみましょう。「Appearing on ～」と表示されれば実装は成功です。

![](assets/03-01.gif)

# Next

[NavigationPageの適用](06-01-NavigationPageの適用.md)  