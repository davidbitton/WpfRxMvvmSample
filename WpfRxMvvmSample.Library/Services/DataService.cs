using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace WpfRxMvvmSample.Library.Services {
    [Export(typeof(IDataService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataService : IDataService {

        private readonly IList<string> _names = new[] { "Billy", "Joe", "Jim", "Bob" };

        private delegate IList<string> GetNamesInvoker();
        private delegate string GetNameInvoker(int index);

        private GetNamesInvoker _getNamesInvoker;
        private GetNameInvoker _getNameInvoker;

        private IList<string> GetNames() {
            return _names;
        }

        public IAsyncResult BeginGetNames(AsyncCallback callback, object state) {
            _getNamesInvoker = GetNames;
            return _getNamesInvoker.BeginInvoke(callback, state);
        }

        public IList<string> EndGetNames(IAsyncResult result) {
            return _getNamesInvoker.EndInvoke(result);
        }

        private string GetName(int index) {
            return _names[index];
        }

        public IAsyncResult BeginGetName(int index, AsyncCallback callback, object state) {
            _getNameInvoker = GetName;
            return _getNameInvoker.BeginInvoke(index, callback, state);
        }

        public string EndGetName(IAsyncResult result) {
            return _getNameInvoker.EndInvoke(result);
        }
    }
}