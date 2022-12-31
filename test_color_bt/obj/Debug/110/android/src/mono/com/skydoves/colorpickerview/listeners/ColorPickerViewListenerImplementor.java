package mono.com.skydoves.colorpickerview.listeners;


public class ColorPickerViewListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.skydoves.colorpickerview.listeners.ColorPickerViewListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Skydoves.ColorPickerViewLib.Listeners.IColorPickerViewListenerImplementor, Skydoves.ColorPickerView", ColorPickerViewListenerImplementor.class, __md_methods);
	}


	public ColorPickerViewListenerImplementor ()
	{
		super ();
		if (getClass () == ColorPickerViewListenerImplementor.class)
			mono.android.TypeManager.Activate ("Skydoves.ColorPickerViewLib.Listeners.IColorPickerViewListenerImplementor, Skydoves.ColorPickerView", "", this, new java.lang.Object[] {  });
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
