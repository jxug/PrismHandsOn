using System;
using Prism;
using Prism.AppModel;
using Prism.Ioc;
using PrismHandsOn.Views;
using Xamarin.Forms;

namespace PrismHandsOn
{
    public partial class App // : Application 宣言を削除する。
    {
        public App()
        {
        }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<ColorsPage>();
            containerRegistry.RegisterForNavigation<SelectedItemPage>();
            containerRegistry.RegisterForNavigation<ChildPage>();
            containerRegistry.RegisterForNavigation<MyTabbedPage>();
        }

        protected override void OnInitialized()
        {
            var applicationStore = Container.Resolve<IApplicationStore>();
            if (applicationStore.Properties.TryGetValue("colorName", out var color))
            {
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ColorsPage)}/{nameof(SelectedItemPage)}?colorName={color}");
            }
            else
            {
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainPage)}");
            }
        }
    }
}