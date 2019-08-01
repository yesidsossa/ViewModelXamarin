using System;
using System.Collections.Generic;
using Android.Util;

namespace ViewModel
{
    public class ItemViewModel : BaseViewModel<List<Item>>, IOnNextListener<List<Item>>, IOnErrorListener
    {
        private readonly string Tag = "Observable";

        public ItemViewModel()
        {
            SetOnNextListener(this);
            SetOnErrorListener(this);
        }

        public void OnErrorListener(Exception error)
        {
            OnErrorAction?.Invoke(error);
        }

        public void OnNextListener(List<Item> value)
        {
            OnNextAction?.Invoke(value);
        }

        protected override List<Item> LoadInBackground()
        {
            var list = new List<Item>();
            for (int i = 0; i < 500; i++)
            {
                list.Add(new Item { Name = i.ToString() });
                Log.Debug(Tag, "Loop: {0}", i);
                System.Diagnostics.Debug.WriteLine("Loop: {0}", i);
            }
            return list;
        }
    }
}
