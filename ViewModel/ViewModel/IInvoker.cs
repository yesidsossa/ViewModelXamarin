using System;

namespace ViewModel.ViewModel
{
    public interface IInvoker
    {
        void AddViewModel<T>(BaseViewModel<T> viewModel, Action<T> OnNextAction, Action<Exception> OnErrorAction);
        void AddViewModel<T>(BaseViewModel<T> viewModel, Action<T> OnNextAction, Action<Exception> OnErrorAction, Action OnCompleteAction);
        void RemoveViewModel<T>(BaseViewModel<T> observable);
        void ValidateStatus();
    }
}
