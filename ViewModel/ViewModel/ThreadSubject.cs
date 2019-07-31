using System;
using System.Collections.Generic;

namespace ViewModel.ViewModel
{
    public class ThreadSubject : IThreadSubject
    {
        private List<BaseViewModel<object>> observables;
        private int finishedObservers = 0;

        public void SuscribeObservable(BaseViewModel<object> observable)
        {
            if (!observables.Contains(observable))
            {
                observables.Add(observable);
            }
        }

        public void UnsuscribeObservable(BaseViewModel<object> observable)
        {
            if (observables.Contains(observable))
            {
                observables.Remove(observable);
            }
           
        }

        public void ValidateStatus()
        {
            for (int i = 0; i < observables.Count; i++)
            {
                
            }
        }
    }
}
