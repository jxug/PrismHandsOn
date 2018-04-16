using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using Prism;

namespace PrismHandsOn.ViewModels
{
    public class ChildPageViewModel : BindableBase, IActiveAware
    {
        public ObservableCollection<DateTime> ActiveTimes { get; } =
            new ObservableCollection<DateTime>();

        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (SetProperty(ref _isActive, value) && _isActive)
                    ActiveTimes.Add(DateTime.Now);
            }
        }

        public event EventHandler IsActiveChanged;
    }
}