using System;
using System.Collections.Generic;

namespace WpfRxMvvmSample.Library.Services {
    public interface IDataService {
        IAsyncResult BeginGetNames(AsyncCallback callback, object state);
        IList<string> EndGetNames(IAsyncResult result);

        IAsyncResult BeginGetName(int index, AsyncCallback callback, object state);
        string EndGetName(IAsyncResult result);
    }
}
