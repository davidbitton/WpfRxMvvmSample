using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace WpfRxMvvmSample.Library.Services {
    [Export(typeof(IDataService))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataService : IDataService {

        private static readonly IList<string> Names = new[] { "Billy", "Joe", "Jim", "Bob" };

        private delegate IList<string> GetNamesInvokerDelegate();
        private delegate string GetNameInvokerDelegate(int index);

        private static readonly GetNamesInvokerDelegate GetNamesInvoker = GetNames;
        private static readonly GetNameInvokerDelegate GetNameInvoker = GetName;

        private static IList<string> GetNames() {
            return Names;
        }

        public IAsyncResult BeginGetNames(AsyncCallback callback, object state) {
            return GetNamesInvoker.BeginInvoke(callback, state);
        }

        public IList<string> EndGetNames(IAsyncResult result) {
            return GetNamesInvoker.EndInvoke(result);
        }

        private static string GetName(int index) {
            return Names[index];
        }

        public IAsyncResult BeginGetName(int index, AsyncCallback callback, object state) {
            return GetNameInvoker.BeginInvoke(index, callback, state);
        }

        public string EndGetName(IAsyncResult result) {
            return GetNameInvoker.EndInvoke(result);
        }
    }
}