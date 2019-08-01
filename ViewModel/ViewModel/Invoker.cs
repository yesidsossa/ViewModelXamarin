using System;
using System.Collections.Generic;

namespace ViewModel.ViewModel
{
    public class Invoker : IInvoker
    {
        private readonly List<BaseViewModel<object>> viewModels;
        private int finishedViewModels;

        private IReceptor Receptor { get; }

        public Invoker(IReceptor receptor)
        {
            Receptor = receptor;
            viewModels = new List<BaseViewModel<object>>();
        }

        public void AddViewModel<T>(BaseViewModel<T> viewModel, Action<T> OnNextAction, Action<Exception> OnErrorAction, Action OnCompleteAction)
        {
            if (!viewModels.Contains(viewModel as BaseViewModel<object>))
            {
                ValidateEvents(viewModel, OnNextAction, OnErrorAction, OnCompleteAction);

                viewModels.Add(viewModel as BaseViewModel<object>);
            }
        }

        public void AddViewModel<T>(BaseViewModel<T> viewModel, Action<T> OnNextAction, Action<Exception> OnErrorAction)
        {
            AddViewModel(viewModel, OnNextAction, OnErrorAction, null);
        }

        public void RemoveViewModel<T>(BaseViewModel<T> viewModel)
        {
            if (viewModels.Contains(viewModel as BaseViewModel<object>))
            {
                viewModels.Remove(viewModel as BaseViewModel<object>);
            }
        }

        public void ValidateStatus()
        {
            for (int i = 0; i < viewModels.Count; i++)
            {
                if (viewModels[i].Status == StatusObserver.Ready)
                {
                    finishedViewModels++;
                }

                if (finishedViewModels == viewModels.Count)
                {
                    Receptor.Complete();
                }
            }
        }

        private void ValidateEvents<T>(BaseViewModel<T> viewModel, Action<T> OnNextAction, Action<Exception> OnErrorAction, Action OnCompleteAction)
        {
            if (viewModel.OnNextAction == null)
            {
                viewModel.OnNextAction += OnNextAction;
            }
            else
            {
                viewModel.OnNextAction = null;
                viewModel.OnNextAction += OnNextAction;
            }

            if (viewModel.OnErrorAction == null)
            {
                viewModel.OnErrorAction += OnErrorAction;
            }
            else
            {
                viewModel.OnErrorAction = null;
                viewModel.OnErrorAction += OnErrorAction;
            }

            if (viewModel.OnCompleteAction == null)
            {
                viewModel.OnCompleteAction += OnCompleteAction;
            }
            else
            {
                viewModel.OnCompleteAction = null;
                viewModel.OnCompleteAction += OnCompleteAction;
            }
        }
    }
}
