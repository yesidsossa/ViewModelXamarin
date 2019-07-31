package md53595b0804b7f3e236faf3afb3ac63b52;


public class ItemViewModel
	extends android.arch.lifecycle.ViewModel
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ViewModel.ItemViewModel, ViewModel", ItemViewModel.class, __md_methods);
	}


	public ItemViewModel ()
	{
		super ();
		if (getClass () == ItemViewModel.class)
			mono.android.TypeManager.Activate ("ViewModel.ItemViewModel, ViewModel", "", this, new java.lang.Object[] {  });
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
