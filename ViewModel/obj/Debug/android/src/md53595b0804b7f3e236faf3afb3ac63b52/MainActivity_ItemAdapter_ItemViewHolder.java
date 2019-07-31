package md53595b0804b7f3e236faf3afb3ac63b52;


public class MainActivity_ItemAdapter_ItemViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ViewModel.MainActivity+ItemAdapter+ItemViewHolder, ViewModel", MainActivity_ItemAdapter_ItemViewHolder.class, __md_methods);
	}


	public MainActivity_ItemAdapter_ItemViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == MainActivity_ItemAdapter_ItemViewHolder.class)
			mono.android.TypeManager.Activate ("ViewModel.MainActivity+ItemAdapter+ItemViewHolder, ViewModel", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
