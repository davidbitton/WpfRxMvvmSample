using System.ComponentModel.Composition;
using MvvmUtility.Infrastructure;

namespace WpfRxMvvmSample.Views {
    /// <summary>
    /// Interaction logic for NamesView.xaml
    /// </summary>
    [ExportView("NamesView")]
    public partial class NamesView {
        public NamesView() {
            InitializeComponent();
        }

        [Import]
        public NamesViewModel ViewModel {
            get { return DataContext as NamesViewModel; }
            set { DataContext = value; }
        }
    }
}
