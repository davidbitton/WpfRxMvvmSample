using System.ComponentModel.Composition;
using MvvmUtility.Infrastructure;

namespace WpfRxMvvmSample {
    [Export(typeof(ISingleViewUiService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellUiService : SingleViewUiService<MainWindow>{
    }
}
