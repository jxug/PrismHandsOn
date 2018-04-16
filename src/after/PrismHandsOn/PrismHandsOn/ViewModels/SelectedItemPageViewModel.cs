using System;
using System.Reflection;
using System.Windows.Input;
using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;
using PrismHandsOn.Models;
using PrismHandsOn.Views;
using Xamarin.Forms;

namespace PrismHandsOn.ViewModels
{
    public class SelectedItemPageViewModel : BindableBase, INavigatingAware, INavigatedAware
    {
        private readonly INavigationService _navigationService;

        private readonly ITextToSpeechService _textToSpeechService;

        private readonly IApplicationStore _applicationStore;

        private string _colorName;

        private Color _color;

        public string ColorName
        {
            get => _colorName;
            set => SetProperty(ref _colorName, value);
        }

        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public ICommand GoBackCommand =>
            new Command(() => _navigationService.GoBackAsync());

        public ICommand NavigateOrangeCommand =>
            new Command(() =>
                _navigationService.NavigateAsync(
                    $"/{nameof(NavigationPage)}/{nameof(ColorsPage)}/{nameof(SelectedItemPage)}?colorName=Orange"));

        public ICommand SpeakCommand => new Command(() => _textToSpeechService.Speak(ColorName));

        public SelectedItemPageViewModel(INavigationService navigationService, ITextToSpeechService textToSpeechService, IApplicationStore applicationStore)
        {
            _navigationService = navigationService;
            _textToSpeechService = textToSpeechService;
            _applicationStore = applicationStore;
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            ColorName = (string)parameters["colorName"];
            Color = (Color)typeof(Color).GetRuntimeField(ColorName).GetValue(null);
            _applicationStore.Properties["colorName"] = ColorName;
            _applicationStore.SavePropertiesAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            _applicationStore.Properties.Remove("colorName");
            _applicationStore.SavePropertiesAsync();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
        }
    }
}