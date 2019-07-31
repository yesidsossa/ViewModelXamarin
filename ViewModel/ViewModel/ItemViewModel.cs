using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using Android.Util;

namespace ViewModel
{
    public class ItemViewModel : BaseViewModel<List<Item>>
    {
        private const string Tag = "Observable";

        public EventHandler<List<Item>> OnItems;

        protected override Func<IObserver<List<Item>>, IDisposable> LoadInBackground => (observer) => {

            var list = new List<Item>();
            for (int i = 0; i < 500; i++)
            {
                list.Add(new Item { Name = i.ToString() });
                Log.Debug(Tag, "Loop: {0}", i);
            }

            observer.OnNext(list);
            observer.OnCompleted();

            return Disposable.Empty;
        };

        protected override void ExecuteOnNext(List<Item> value)
        {
            Log.Debug(Tag, "respuesta en onNext");
            System.Diagnostics.Debug.WriteLine("respuesta en onNext");
            OnItems?.Invoke(new Item(), value);
        }
    }
}
