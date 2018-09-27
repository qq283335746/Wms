package com.tygasoft.plugins;

import java.util.Map;
import java.util.Timer;
import java.util.TimerTask;

import org.apache.cordova.CordovaPlugin;
import org.apache.cordova.CallbackContext;

import org.json.JSONArray;
import org.json.JSONException;

import com.UHF.scanlable.UfhData;
import com.UHF.scanlable.UfhData.UhfGetData;

/**
 * This class echoes a string called from JavaScript.
 */
public class RfidScan extends CordovaPlugin {

	int tty_speed = 57600;
	byte addr = (byte) 0xff; 
	Timer timer;
	boolean Scanflag=false;
	int selectTime = 1;
	int selectedEd = 0;
	String mode = "6C";
	private static boolean IsPause = false;
	private static final int SCAN_INTERVAL = 100;
	public static final String TABLE_6B = "6B";
	public static final String TABLE_6C = "6C";
	
    @Override
    public boolean execute(String action, JSONArray args, final CallbackContext callbackContext) throws JSONException {
        if (action.equals("HelloWord")) {
            String message = args.getString(0);
            this.HelloWord(message, callbackContext);
            return true;
        }
        else if (action.equals("onOpen")) {
        	this.onOpen(callbackContext);
        	return true;
        }
        else if (action.equals("onClose")) {
        	this.onClose(callbackContext);
            return true;
        }
        else if (action.equals("onScan")) {
        	this.onScan(callbackContext);
            return true;
        }
        else if (action.equals("onPause")) {
        	this.onPause(args.getInt(0),callbackContext);
            return true;
        }
        else if (action.equals("onResume")) {
            return true;
        }
        else if (action.equals("clearData")) {
        	this.clearData(callbackContext);
            return true;
        }
        else if (action.equals("getData")) {
        	this.getData(callbackContext);
            return true;
        }
        
        return false;
    }

    private void HelloWord(String message, CallbackContext callbackContext) {
    	callbackContext.success(message);
    }
    
    private void onOpen(CallbackContext callbackContext) {
    	if(UfhData.isDeviceOpen()){
    		callbackContext.success(0);
    		return;
    	}
    	int result=UhfGetData.OpenUhf(tty_speed, addr, 4, 0, null);
		if(result==0){
		    UhfGetData.GetUhfInfo();
		}
    	callbackContext.success(result);
    }
    
    private void onClose(CallbackContext callbackContext) {
    	UfhData.Set_sound(false);
		UfhData.SoundFlag=false;
		if(timer != null){
			timer.cancel();
			timer = null;
		}
    	if(UfhData.isDeviceOpen()){
			UhfGetData.CloseUhf();
			UfhData.FirstOpen=false;
		}
    	callbackContext.success(1);
    }
    
    private void onScan(CallbackContext callbackContext) {
    	if(!UfhData.isDeviceOpen()){
    		return;
		}
		if(timer == null) {
			UfhData.Set_sound(true);
			UfhData.SoundFlag=false;
			Scanflag=false;
			UfhData.target=0;
			if(selectedEd==3)selectedEd=255;

			if(mode.equals(TABLE_6B)){
				UfhData.scanResult6b.clear();
				UfhData.Result6c.clear();
			}else if(mode.equals(TABLE_6C)){
				UfhData.scanResult6c.clear();
				UfhData.Result6c.clear();
			}
			timer = new Timer();
		}
		timer.schedule(new TimerTask() {
			@Override
			public void run() {
				if(Scanflag) return;
				Scanflag=true;
				if(mode.equals(TABLE_6B)){
					if(!IsPause) UfhData.read6b();
				}
				else if(mode.equals(TABLE_6C)){
					if(!IsPause) UfhData.read6c(selectedEd);
				}
				try {
					Thread.sleep(selectTime*100);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
				Scanflag=false;
			}
		}, 0, SCAN_INTERVAL);
		
		callbackContext.success(1);
    }
    
    private void onPause(int isPause, CallbackContext callbackContext){
    	IsPause = isPause == 1;
    	callbackContext.success(1);
    }
    
    private void clearData(CallbackContext callbackContext){
    	UfhData.scanResult6c.clear();
		UfhData.Result6c.clear();
    	callbackContext.success(1);
    }
    
    private void getData(CallbackContext callbackContext) {
    	String s = "";
		Map<String, Long> items = UfhData.scanResult6c;
		if(items != null && items.size()>0){
			int sIndex = 0;
			for(String item : items.keySet()){
				if(sIndex > 0) s += ",";
				s += item;
				sIndex++;
			}
		}
    	callbackContext.success(s);
    }
}
