# NavigationPageの適用

## 目的

* MainPageをNavigationPage内に表示されるよう修正する

画面遷移の解説するための前準備になります

## 手順

1. App.xaml.csでNavigationPageをDIコンテナへ追加する  
2. App.xaml.csで初期画面の画面遷移を修正する  
3. MainPage.xamlでTitleプロパティを指定する

## App.xaml.csでNavigationPageをDIコンテナへ追加する  

RegisterTypesを修正し、NavigationPageをDIコンテナへ追加するコードを追記しましょう。

変更前
```cs
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage>();
        }
```

変更後
```cs
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }
```

Prism for Xamarin.Formsでは画面遷移対象のクラスは全てDIコンテナに登録しておく必要があります。これはXamarin.Formsで標準で含まれているクラスに対しても同様で、NavigationPageを利用する場合はそれ自体をDIコンテナへ追加する必要があります。

## App.xaml.csで初期画面の画面遷移を修正する  

Prismでは画面遷移時に遷移名の指定をスラッシュで区切ってURLのように指定することで、複数画面を一気に遷移することが可能です。

アプリケーション起動時にNavigationPageの中にMainPageを表示する際にも、その機能を利用します。

App.xaml.csを開きOnInitializedメソッドを修正します。

```cs
        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync({nameof(MainPage)});
        }
```

つぎのように、遷移名が「NavigationPage/MainPage」となるように修正してください。

```cs
        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
        }
```

## MainPage.xamlでTitleプロパティを指定する

NavigationPageを利用した場合、画面上部にナビゲーションバーが表示されます。

ナビゲーションバーには内部に表示している画面の名称を表示する必要があります。このため、MainPageにTitleプロパティの指定を追記します。

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:PrismHandsOn"
             x:Class="PrismHandsOn.Views.MainPage">
             ...
```

XAML上でContentPageのプロパティのTitleにページ名を指定してください。

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:PrismHandsOn"
             x:Class="PrismHandsOn.Views.MainPage"
             Title="Main Page">
             ...
```

実行すると、次のようにページの頭にナビゲーションバーが追加され、ページ名が表示されます。

![](assets/06-01-01.png)


# Next

[基本の階層ナビゲーション](06-02-基本の階層ナビゲーション.md)  