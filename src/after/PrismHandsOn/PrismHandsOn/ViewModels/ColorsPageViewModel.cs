using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Prism.Navigation;
using PrismHandsOn.Views;
using Xamarin.Forms;

namespace PrismHandsOn.ViewModels
{
    public class ColorsPageViewModel
    {
        public IReadOnlyCollection<ColorInfo> ColorInfos { get; } =
            typeof(Color)
                .GetRuntimeFields()
                .Where(x => x.IsPublic && x.IsStatic && x.FieldType == typeof(Color))
                .Select(x => new ColorInfo { Name = x.Name, Color = (Color)x.GetValue(null) })
                .ToList();

        private readonly INavigationService _navigationService;

        public Command<ColorInfo> ItemSelectedCommand =>
            new Command<ColorInfo>(colorInfo =>
            {
                var parameter = new NavigationParameters();
                parameter.Add("colorName", colorInfo.Name);
                _navigationService.NavigateAsync(nameof(SelectedItemPage), parameter);
            });

        public ColorsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

    }
}