using System.ComponentModel.Composition;
using System.Windows.Controls;
using MvvmUtility.Infrastructure;

namespace WpfRxMvvmSample {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export]
    public partial class MainWindow : IMainView {
        public MainWindow() {
            InitializeComponent();
        }

        [Import]
        public MainWindowViewModel ViewModel {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }

        public UserControl CurrentView {
            get { return ClientArea.Content as UserControl; }
            set { ClientArea.Content = value; }
        }
    }
}
