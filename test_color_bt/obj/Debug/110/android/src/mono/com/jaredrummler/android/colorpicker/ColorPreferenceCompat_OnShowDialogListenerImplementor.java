package mono.com.jaredrummler.android.colorpicker;


public class ColorPreferenceCompat_OnShowDialogListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.jaredrummler.android.colorpicker.ColorPreferenceCompat.OnShowDialogListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onShowColorPickerDialog:(Ljava/lang/String;I)V:GetOnShowColorPickerDialog_Ljava_lang_String_IHandler:JaredRummler.Android.ColorPicker.ColorPreferenceCompat/IOnShowDialogListenerInvoker, JaredRummler.ColorPicker\n" +
			"";
		mono.android.Runtime.register ("JaredRummler.Android.ColorPicker.ColorPreferenceCompat+IOnShowDialogListenerImplementor, JaredRummler.ColorPicker", ColorPreferenceCompat_OnShowDialogListenerImplementor.class, __md_methods);
	}


	public ColorPreferenceCompat_OnShowDialogListenerImplementor ()
	{
		super ();
		if (getClass () == ColorPreferenceCompat_OnShowDialogListenerImplementor.class)
			mono.android.TypeManager.Activate ("JaredRummler.Android.ColorPicker.ColorPreferenceCompat+IOnShowDialogListenerImplementor, JaredRummler.ColorPicker", "", this, new java.lang.Object[] {  });
	}


	public void onShowColorPickerDialog (java.lang.String p0, int p1)
	{
		n_onShowColorPickerDialog (p0, p1);
	}

	private native void n_onShowColorPickerDialog (java.lang.String p0, int p1);

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
