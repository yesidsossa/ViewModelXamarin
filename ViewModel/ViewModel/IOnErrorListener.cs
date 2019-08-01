using System;

namespace ViewModel
{
    public interface IOnErrorListener
    {
        void OnErrorListener(Exception error);
    }
}