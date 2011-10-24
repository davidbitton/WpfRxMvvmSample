using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using MvvmUtility.Infrastructure.ViewUtility;
using WpfRxMvvmSample.Library.Services;

namespace WpfRxMvvmSample.Views {
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class NamesViewModel : WorkspaceViewModel {
        private readonly IDataService _dataService;

        [ImportingConstructor]
        public NamesViewModel(IEventAggregator eventAggregator, IDataService dataService)
            : base(eventAggregator) {
            _dataService = dataService;
        }

        #region Names
        private ObservableCollection<string> _names;

        /// <summary>
        /// Gets the Names property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public ObservableCollection<string> Names {
            get { return _names; }
            set {
                if (_names == value) return;
                _names = value;
                // Update bindings, no broadcast
                RaisePropertyChanged(() => Names);
            }
        }
        #endregion

        #region SelectedName
        private string _selectedName;

        /// <summary>
        /// Gets the SelectedName property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public string SelectedName {
            get { return _selectedName; }
            set {
                if (_selectedName == value) return;
                _selectedName = value;
                // Update bindings, no broadcast
                RaisePropertyChanged(() => SelectedName);
            }
        }
        #endregion

        #region GetNamesCommand

        private ICommand _getNamesCommand;
        public ICommand GetNamesCommand {
            get { return _getNamesCommand ?? (_getNamesCommand = CommandRegistry.Add(GetNames, p => CanGetNames)); }
        }

        private static bool CanGetNames {
            get { return true; }
        }

        private void GetNames(object commandArg) {
            Observable.FromAsyncPattern<IList<string>>(_dataService.BeginGetNames, _dataService.EndGetNames)()
                .SubscribeOn(SynchronizationContext.Current)
                .Subscribe(
                    result => {
                        Names = new ObservableCollection<string>(result);
                    },
                    ex => MessageBox.Show(ex.Message),
                    () => {
                        if (Names != null && Names.Count > 0)
                            SelectedName = Names[0];

                        // refresh commands
                        CommandRegistry.RaiseCanExecuteChanged();
                    }
                );
        }

        #endregion

        #region GetNameCommand

        private ICommand _getNameCommand;
        public ICommand GetNameCommand {
            get { return _getNameCommand ?? (_getNameCommand = CommandRegistry.Add(GetName, p => CanGetName)); }
        }

        private bool CanGetName {
            get { return Names != null && Names.Count > 0; }
        }

        private void GetName(object commandArg) {
            int index;
            var success = Int32.TryParse((string)commandArg, out index);

            if(success) {
                Observable.FromAsyncPattern<int, string>(_dataService.BeginGetName, _dataService.EndGetName)(index)
                    .SubscribeOn(SynchronizationContext.Current)
                    .Subscribe(
                        result => { SelectedName = result; },
                        ex => MessageBox.Show(ex.Message),
                        () => { /* no completion operation */ }
                    );
            }
        }

        #endregion

    }
}