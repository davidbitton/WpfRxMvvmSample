using System.ComponentModel.Composition.Hosting;
using System.Windows;
using MvvmUtility.Infrastructure;
using WpfRxMvvmSample.Library.Services;

namespace WpfRxMvvmSample {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {

        protected override void ApplicationStartup(object sender, StartupEventArgs e) {
            base.ApplicationStartup(sender, e);

            Catalog.Catalogs.Add(new AggregateCatalog(
                new AssemblyCatalog(typeof(App).Assembly),
                new AssemblyCatalog(typeof(DataService).Assembly)
                ));
            ShowMainWindow();
        }

        private void ShowMainWindow() {
            var uiService = Container.GetExportedValue<ISingleViewUiService>();
            Current.MainWindow = (Window)uiService.MainWindow;
            uiService.ShowView(ViewNames.NamesView);

            Current.MainWindow.Show();
        }
    }
}
