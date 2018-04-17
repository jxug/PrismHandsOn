using System;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismHandsOn.Views;
using Xamarin.Forms;

namespace PrismHandsOn.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _pageDialogService;

        private string _message = "Hello, Prism for Xamarin.Forms!";

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ICommand UpdateMessageCommand => new Command(() => Message = $"Updated on {DateTime.Now}");
        public ICommand AppearingCommand => new Command(() => Message = $"Appearing on {DateTime.Now}");

        public Command<string> NavigateCommand =>
            new Command<string>(
                name => _navigationService.NavigateAsync(name));

        public ICommand DeepLinkCommand =>
            new Command(() =>
                _navigationService.NavigateAsync(
                    $"{nameof(ColorsPage)}/{nameof(SelectedItemPage)}?colorName=Red"));

        public ICommand DisplayAlertCommand => new Command(() =>
        {
            _pageDialogService.DisplayAlertAsync("Title", "Hello, Dialog.", "OK");
        });

        public ICommand DisplayConfirmCommand => new Command(async () =>
        {
            var result = await _pageDialogService.DisplayAlertAsync(
                "Title", "何れかを選んでください。", "はい", "いいえ");
            Message = $"Selected:{result}";
        });

        public ICommand DisplayActionSheetCommand => new Command(() =>
        {
            _pageDialogService.DisplayActionSheetAsync(
                "共有先を選択してください。",
                ActionSheetButton.CreateCancelButton("キャンセル", OnCancel),
                ActionSheetButton.CreateDestroyButton("削除", OnDestory),
                ActionSheetButton.CreateButton("Twitter", OnSelectTwitter),
                ActionSheetButton.CreateButton("LINE", OnSelectLine),
                ActionSheetButton.CreateButton("Facebook", OnSelectFacebook));
        });

        private void OnCancel() => Message = "Selected:キャンセル";
        private void OnDestory() => Message = "Selected:削除";
        private void OnSelectTwitter() => Message = "Selected:Twitter";
        private void OnSelectLine() => Message = "Selected:LINE";
        private void OnSelectFacebook() => Message = "Selected:Facebook";

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
        }
    }
}