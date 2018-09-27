package com.UHF.scanlable;

import android.content.Context;
import android.widget.EditText;
import android.widget.Toast;

public class Util {
	
	public static boolean showWarning(Context context, int resRes){
		Toast.makeText(context, resRes, Toast.LENGTH_LONG).show();
		return false;
	}
	
	public static boolean isEtEmpty(EditText editText){
		String str = editText.getText().toString();
		return str==null||str.equals("");
	}
	
	public static boolean isLenLegal(EditText editText){
		if(isEtEmpty(editText))return false;
		String str = editText.getText().toString();
		return str!=null && str.length() % 2 == 0;
	}
	
	public static boolean isEtsLegal(EditText[] ets){
		for(EditText et:ets){
			if (isLenLegal(et)) return true;
		}
		return false;
	}

}
