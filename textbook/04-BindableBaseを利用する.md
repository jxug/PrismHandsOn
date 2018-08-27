# BindableBaseを利用する

## 目的  

* `BindableBase`を利用して`INotifyPropertyChanged`を簡便に実装する

MVVMパターンでは、画面の状態はViewModelで管理します。この時、ViewModelの値が変わったら、画面の表示状態が変更される必要があります。

このため、ViewModelの状態が変更されたタイミングでViewに変更を通知する必要があります。その為のインターフェースが`INotifyPropertyChanged`です。

`INotifyPropertyChanged`の実装は煩雑であるため、PrismではViewModelの基底クラスとして`BindableBase`を提供しており、簡便に実装できるようにしています。

## 手順

1. `MainPageViewModel`を`BindableBase`が親クラスとなるよう修正する
1. `MainPageViewModel`の`Message`プロパティの変更通知を実装する
1. `MainPageViewModel`の`Message`を更新するCommandを作成する
1. MainPageにボタンを追加し、`Message`を更新するCommandをバインドする

## `MainPageViewModel`を`BindableBase`が親クラスとなるよう修正する  

`MainPageViewModel.cs`を開き、`BindableBase`が親クラスとなるよう、次のように変更します。

変更前

```cs
namespace PrismHandsOn.ViewModels
{
    public class MainPageViewModel
    {
        ...
```

変更後

```cs
using Prism.Mvvm;

namespace PrismHandsOn.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        ...
```

## MainPageViewModelの`Message`プロパティの変更通知を実装する

次の手順で実装します。

1. `Message`プロパティに対応するバッキングフィールド「`_message`」を追加
1. `Message`プロパティのgetter・setterを明示的に実装
1. setterでは`BindableBase`の`SetProperty`を呼ぶ

変更前

```cs
    public class MainPageViewModel : BindableBase
    {
        public string Message { get; } = "Hello, Prism for Xamarin.Forms!";
        ...
```

変更後

```cs
    public class MainPageViewModel : BindableBase
    {
        private string _message = "Hello, Prism for Xamarin.Forms!";

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        ...
```

`BindableBase`の`SetProperty`を呼ぶことで、`_message`と`value`の値が異なった場合（つまり変更が発生した場合）に、`INotifyPropertyChanged`で定義されている`PropertyChanged`が発行され、Viewへ変更が通知されます。

## MainPageViewModelのMessageを更新するCommandを作成する  

次のようなコマンドを実装します。

usingが追加になっていることに注意しましょう。

```cs
using System;
using Prism.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace PrismHandsOn.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        ...
        public ICommand UpdateMessageCommand => new Command(() => Message = $"Updated on {DateTime.Now}");
    }
}
```

## `MainPage`にボタンを追加し、`Message`を更新するCommandをバインドする

変更前

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage ...

	<Label Text="{Binding Message}"
           VerticalOptions="Center"
           HorizontalOptions="Center" />

</ContentPage>
```

変更前は`ContentPage`の直下に`Label`が存在していましたが、`ContentPage`直下は`StackLayout`に変更し、ボタンを追加します。そしてボタンに`UpdateMessageCommand`をバインドします。

変更後

```cs
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage ...

    <StackLayout HorizontalOptions="Center" VerticalOptions="Center">
        <Label Text="{Binding Message}"/>
        <Button Text="Update Message"
                Command="{Binding UpdateMessageCommand}"/>
    </StackLayout>

</ContentPage>
```

それでは実行してみましょう。表示メッセージが次のように更新されれば実装成功です。

![](assets/04-01.gif)

# Next

[EventToCommandBehaviorを使う](05-EventToCommandBehaviorを使う.md)
