using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Android.App;
using Android.Util;

namespace ViewModel
{
	public class ItemViewModel : Android.Arch.Lifecycle.ViewModel
	{
        private const string Tag = "Observable";
        List<Item> Items;

        public EventHandler<List<Item>> OnItems;

		public void GetItems()
		{
            IObservable<List<Item>> observable = Observable.Create<List<Item>>(observer => {
                var cancel = new CancellationDisposable();

                var list = new List<Item>();
                for (int i = 0; i < 500; i++)
                {
                    list.Add(new Item { Name = i.ToString() });
                    Log.Debug(Tag, "Loop: {0}", i);
                    //System.Diagnostics.Debug.WriteLine("Loop: {0}", i);
                }

                observer.OnNext(list);
                observer.OnCompleted();

                return cancel;
            });

            observable.SubscribeOn(new NewThreadScheduler()).ObserveOn(Application.SynchronizationContext).Subscribe(
                (x) => {
                    Log.Debug(Tag, "respuesta en onNext");
                    System.Diagnostics.Debug.WriteLine("respuesta en onNext");
                    Items = x;
                    OnItems?.Invoke(new Item(), x);

                },
                (x) => {
                    Log.Debug(Tag, "respuesta en error" + x.StackTrace);
                    System.Diagnostics.Debug.WriteLine("respuesta en error" + x.StackTrace);
                },
                () => {
                    Log.Debug(Tag, "respuesta en complete");
                    System.Diagnostics.Debug.WriteLine("respuesta en complete");
                }
            );
        }
	}
}
