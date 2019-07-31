using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.App;
using ViewModel.ViewModel;

namespace ViewModel
{
    public abstract class BaseViewModel<T> : Android.Arch.Lifecycle.ViewModel, IObserver<T>
    {
        public Action<T> OnNext;
        public Action<Exception> OnError;
        public Action OnComplete;

        public StatusObserver Status { get; private set; }

        protected abstract Func<IObserver<T>, IDisposable> LoadInBackground { get; }

        private IObservable<T> observable;

        public virtual void InitObserver()
        {
            observable = Observable.Create(LoadInBackground);
            observable.SubscribeOn(new NewThreadScheduler()).ObserveOn(Application.SynchronizationContext).Subscribe(this);
        }

        void IObserver<T>.OnNext(T value)
        {
            ExecuteOnNext(value);
        }

        void IObserver<T>.OnError(Exception error)
        {
            // No implemented
        }

        public void OnCompleted()
        {
            // No implemented
        }

        protected abstract void ExecuteOnNext(T value);
    }
}
