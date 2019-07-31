using System;
namespace ViewModel.ViewModel
{
    public interface IThreadSubject
    {
        void SuscribeObservable(BaseViewModel<object> observable);
        void UnsuscribeObservable(BaseViewModel<object> observable);
        void ValidateStatus();
    }
}
