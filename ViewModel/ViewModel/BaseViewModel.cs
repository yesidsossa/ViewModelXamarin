using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Android.App;
using ViewModel.ViewModel;

namespace ViewModel
{
    public abstract class BaseViewModel<T> : Android.Arch.Lifecycle.ViewModel, IObserver<T>
    {
        public Action<T> OnNextAction;
        public Action<Exception> OnErrorAction;
        public Action OnCompleteAction;

        public StatusObserver Status { get; private set; }

        private IOnNextListener<T> NextListener;
        private IOnErrorListener ErrorListener;
        private IOnCompleteListener CompleteListener;
        private IObservable<T> observable;

        public BaseViewModel()
        {
            Status = StatusObserver.InProgress;
        }

        protected abstract T LoadInBackground();

        protected void SetOnNextListener(IOnNextListener<T> nextListener)
        {
            NextListener = nextListener;
        }

        protected void SetOnErrorListener(IOnErrorListener errorListener)
        {
            ErrorListener = errorListener;
        }

        protected void SetOnCompleteListener(IOnCompleteListener completeListener)
        {
            CompleteListener = completeListener;
        }

        public virtual void InitObserver()
        {
            observable = Observable.Create<T>(observer => {
                var cancel = new CancellationDisposable();

                T response = LoadInBackground();

                observer.OnNext(response);
                observer.OnCompleted();

                return cancel;
            });
            observable.SubscribeOn(new NewThreadScheduler()).ObserveOn(Application.SynchronizationContext).Subscribe(this);
        }

        public void OnNext(T value)
        {
            Status = StatusObserver.Ready;
            NextListener?.OnNextListener(value);
        }

        public void OnError(Exception error)
        {
            Status = StatusObserver.Failed;
            ErrorListener?.OnErrorListener(error);
        }

        public void OnCompleted()
        {
            CompleteListener?.OnCompleteListener();
        }
    }
}
