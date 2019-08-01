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
        private IObservable<T> observable;

        public BaseViewModel()
        {
            Status = StatusObserver.InProgress;
        }

        public virtual void InitObserver()
        {
            observable = Observable.Create(FunctionToExecute());
            observable.SubscribeOn(new NewThreadScheduler()).ObserveOn(Application.SynchronizationContext).Subscribe(this);
        }

        public void OnNext(T value)
        {
            Status = StatusObserver.Ready;
            OnNextAction?.Invoke(value);
        }

        public void OnError(Exception error)
        {
            Status = StatusObserver.Failed;
            OnErrorAction?.Invoke(error);
        }

        public void OnCompleted()
        {
            OnCompleteAction?.Invoke();
        }

        protected abstract T LoadInBackground();

        private Func<IObserver<T>, IDisposable> FunctionToExecute()
        {
            return observer =>
            {
                var cancel = new CancellationDisposable();

                T response = LoadInBackground();

                observer.OnNext(response);
                observer.OnCompleted();

                return cancel;
            };
        }
    }
}
