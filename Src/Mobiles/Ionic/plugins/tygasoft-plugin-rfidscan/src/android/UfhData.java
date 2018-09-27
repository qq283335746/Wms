package com.UHF.scanlable;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.lang.Thread;
import com.UHF.scanlable.UhfLib;

import android.R.integer;
import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;
import android.media.AudioManager;
import android.media.SoundPool;

public class UfhData {
	
	String[] epc = new String[4];
	//static SQLiteDatabase db = SQLiteDatabase.openOrCreateDatabase("/data/test.db", null);  
	public static Map<String, Long> scanResult6c = new HashMap<String, Long>();
	public static Map<String, Long> Result6c = new HashMap<String, Long>();
	public static Map<String, Long> scanResult6b = new HashMap<String, Long>();
	static Map<String, byte[]> epcBytes = new HashMap<String, byte[]>();
	static int cmdnum;
	static long cmdsl;
	public static boolean FirstOpen = true;
	private static int scaned_num;
	private static String[] lable;
	private String mem;
	private String wordPtr;
	private String len;
	private String pwd;
	private String id;
	private String addr;
	private String num;
	private static boolean isDeviceOpen = false;
	static SoundPool soundpool = new SoundPool(1, AudioManager.STREAM_NOTIFICATION, 100);;
	static ExecutorService soundThread = Executors.newSingleThreadExecutor();
	static int soundid = soundpool.load("/etc/Scan_new.ogg", 1);
	private static String ufh_id;
	public static boolean SoundFlag=false;//控制读卡信号
	public static boolean SoundTimer=false;//发声开关，打开了读到信号就发声
	public static Timer timer;
	private static int notagTime=0;//没读到卡的次数
	public static void Set_sound(boolean flag) {
		UfhData.SoundTimer=flag;
	}

	public static String getUfh_id() {
		return ufh_id;
	}
	
	public static void setUfh_id(String ufh_id) {
		UfhData.ufh_id = ufh_id;
	}

	public static boolean isDeviceOpen() {
		return isDeviceOpen;
	}

	public static void setDeviceOpen(boolean b) {
		isDeviceOpen = b;
	}
	
	public static int getScanedNum(){
		return scaned_num;
	}
    public static int Num=0;
    public static byte target=0;
	public static void read6c(int selectedEd){
		try{
			
			Date date = new Date();//创建现在的日期
			long value = date.getTime();//获得毫秒数
			if((selectedEd==0)||(selectedEd==1))
				target=0;
			String[] lable = UhfGetData.Scan6C(selectedEd,target);
			
			if(lable == null){ 
				scaned_num = 0;
				cmdnum=0;
				cmdsl = 0;
				notagTime++;	
				if(notagTime>10)
				{
					if(target==1)
						target=0;
					else
						target=1;
				}
				return ;
			}
			notagTime=0;
			scaned_num = lable.length;
			cmdnum = scaned_num;
			Date date1 = new Date();//创建现在的日期
			long value1 = date1.getTime();//获得毫秒数
			cmdsl = (scaned_num*1000) /(value1 -value);
			
			for (int i = 0; i < scaned_num; i++) {
				 String key = lable[i].substring(0,lable[i].length()-2);
				 String RSSI=  lable[i].substring(lable[i].length()-2);
				if(key == null || key.equals("")) return;
				if(scanResult6c.size()<200)
				{
					long num = scanResult6c.get(key) == null ? 0 : scanResult6c.get(key);
					scanResult6c.put(key, Long.valueOf(RSSI,16));	
				}
				Result6c.put(key, (long)1);
				Num = Result6c.size();
				
			}	
		}catch(Exception e)
		{
			scanResult6c.clear();
		}
	}
	
	public static void read6b(){
		String[] lable = UhfGetData.Scan6B();
		if(lable == null) {
			scaned_num = 0;
			return ;
		}
		scaned_num = lable.length;
		for (int i = 0; i < scaned_num; i++) {
			String key = lable[i];
			if(key == null || key.equals("")) return;
			Long num = scanResult6b.get(key) == null ? 0 : scanResult6b.get(key);
			scanResult6b.put(key, num + 1);
		}
	}
	
	public String getMem() {
		return mem;
	}
	public void setMem(String mem) {
		this.mem = mem;
	}
	public String getWordPtr() {
		return wordPtr;
	}
	public void setWordPtr(String wordPtr) {
		this.wordPtr = wordPtr;
	}
	public String getLen() {
		return len;
	}
	public void setLen(String len) {
		this.len = len;
	}
	public String getPwd() {
		return pwd;
	}
	public void setPwd(String pwd) {
		this.pwd = pwd;
	}
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	public String getAddr() {
		return addr;
	}
	public void setAddr(String addr) {
		this.addr = addr;
	}
	public String getNum() {
		return num;
	}
	public void setNum(String num) {
		this.num = num;
	}
	public static class UhfGetData
	{	
//		private static final String "" = null;
		private static byte Read6Bdata[]=new byte[256];
		private static byte Read6Cdata[]=new byte[256];
		private static byte EPC_6B[][]=new byte[100][100];
		private static int Read6CLen=-1;
		
		private static byte UhfVersion[]={-1,-1};
		private static byte UhfTime[]={-1};
		private static byte UhfMaxFre[]={-1};
		private static byte UhfBand[]={-1};
		public static byte[] getRead6Bdata() {
			return Read6Bdata;
		}
		
		public static int getRead6CLen(){
			return Read6CLen;
		}

		public static byte[] getRead6Cdata() {
			return Read6Cdata;
		}

		public static int getScan6BNum() {
			return Scan6BNum[0];
		}

		public static byte[] getScan6BData() {
			return Scan6BData;
		}

		public static byte[][] getEPC_6B() {
			return EPC_6B;
		}

		public static byte[] getUhfVersion() {
			return UhfVersion;
		}

		public static byte[] getUhfTime() {
			return UhfTime;
		}

		public static byte[] getUhfMaxFre() {
			return UhfMaxFre;
		}

		public static byte[] getUhfMinFre() {
			return UhfMinFre;
		}
		
		public static byte[] getband() {
			return UhfBand;
		}
		
		public static byte[] getUhfdBm() {
			return UhfdBm;
		}

		public static int getScan6CNum() {
			return Scan6CNum;
		}

		public static byte[] getScan6CData() {
			return Scan6CData;
		}

		public static byte[][] getEPC_6C() {
			return EPC_6C;
		}

		public static UhfLib getUhf() {
			return uhf;
		}

		private static byte UhfMinFre[]={-1};
		private static byte UhfdBm[]={-1};
		private static int Scan6CNum=-1;
		private static byte Scan6CData[]=new byte[256];
		private static int Scan6BNum[]={-1};
		private static byte Scan6BData[]=new byte[256];
		private static byte EPC_6C[][]=new byte[100][100];
		static UhfLib uhf = null;
		private static boolean Beep_finish=false;
		public static int OpenUhf(int tty_speed,byte addr,int OS_versin,int log_swith,Context mCt )
		{	
			 String serial;
			 if (OS_versin==4)
				 serial= "/dev/ttyHSL2";
			 else
				 serial= "/dev/ttyHSL2";
			 
			 uhf =new UhfLib(tty_speed, addr,  serial, 1,mCt);
			 int result=uhf.open_reader();
			 if(result!=0)return -1;
			 result=GetUhfInfo();
			 if(result==-1){
				 uhf.close_reader();
				 return -1;
			 }
			 FirstOpen=false;
			 UfhData.setDeviceOpen(true);
			// UfhData.createTable(UfhData.db);//创建表
			 if(timer == null)
			 {
				 timer = new Timer();
					timer.schedule(new TimerTask() {
						@Override
						public void run() {
							if(Beep_finish)return;
							Beep_finish=true;
							MessageBeep();
							Beep_finish=false;
						}
					}, 0, 50);
			 }
			 return 0;
			 
		}
		public static void MessageBeep()
		{
			boolean flag=SoundFlag;
			if(flag&&SoundTimer){
				soundThread.execute(soundRun);
			}
		}
		
		public static int CloseUhf()
		{
			if(uhf==null)return 0;
			if(timer != null){
				timer.cancel();
				timer = null;
			}
			uhf.close_reader();
			UfhData.setDeviceOpen(false);
			//UfhData.drop(UfhData.db);//创建表
			return 0;
		}
		
		
		public static int GetUhfInfo()
		{
			UhfVersion=uhf.Get_TVersionInfo();
			UhfTime=uhf.Get_ScanTime();
			UhfMaxFre=uhf.Get_dmaxfre();
			UhfMinFre=uhf.Get_dminfre();
			UhfdBm=uhf.Get_powerdBm();
			UhfBand[0] = (byte)((((UhfMaxFre[0]&255) & 0xc0) >> 4) | ((UhfMinFre[0]&255) >> 6));
			/***************************************************/
			Log.d("yl", "*********UhfVersion= = "+UhfVersion[0]+UhfVersion[1]);
			if(UhfVersion[0]==-1 && UhfVersion[1]==-1 && UhfTime[0]==-1)
			return -1;
			else
			return 0;
		}
		
		
		
		public static int SetUhfInfo(byte maxfre, byte minfre,byte power,byte scantime)
		{
			int result1=uhf.SetReader_Freq(maxfre, minfre);
			int result2=uhf.SetReader_Power(power);
			if(result1==0&&result2==0)
			{
				uhf.ReGetInfo();
				return 0;
			}
			else 
			return -1;
		}
		
		public static String byteToString(byte[] b){
			StringBuffer sb = new StringBuffer("");
			for(int i = 0; i < b.length; i++){
				sb.append(Integer.toHexString(b[i] & 0xff));
			}
			return sb.toString();
		}
		
		public static byte[] stringToByte(String str){
			byte[] b = new byte[str.length()];
			for(int i = 0; i < str.length(); i++){
				b[i] = Byte.valueOf(str.substring(i, i+1));
			}
			return b;
		}
		
		public static String byteToString(byte[] b, int len){
			StringBuffer sb = new StringBuffer("");
			for(int i = 0; i < len; i++){
				sb.append(Integer.toHexString(b[i] & 0xff));
			}
			return sb.toString();
		}
		
		private static Runnable soundRun = new Runnable(){
			public void run(){
				try {
					soundpool.play(soundid, 1, 1, 0, 0, 1f);
					Thread.sleep(10);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
		};
		
		public static String[] Scan6C(int Session,byte target)
		{	
			try{
				int result=uhf.EPCC1G2_ScanEPC((byte)0x06, (byte)Session,target);
				SoundFlag=false;
				if(result==0){
					SoundFlag=true;
					Scan6CNum=uhf.EPCC1G2_Inventory_POUcharTagNum();
				    Scan6CData=uhf.EPCC1G2_Inventory_pOUcharUIDList();
				    String[] lable = new String[Scan6CNum];
				    StringBuffer bf;
				    int j = 0, k;
				    String str;
				    byte[] epc;
				    Log.i("yl","num = "+ Scan6CNum + ">>>>>>"+"len = "+ Scan6CData.length);
				    for(int i = 0; i < Scan6CNum; i++){
				    	bf = new StringBuffer("");
				    	Log.i("yl","length = " + Scan6CData[j]);
				    	epc = new byte[(Scan6CData[j]+1) & 0xff];//多了RSSI字节
				    	for(k = 0; k < ((Scan6CData[j]+1) & 0xff); k++){
				    		str = Integer.toHexString(Scan6CData[j+k+1] & 0xff);
				    		if(str.length() == 1){
				    			bf.append("0");
				    		}
				    		bf.append(str);
				    		epc[k] = Scan6CData[j+k+1];
				    	}
				    	lable[i] = bf.toString().toUpperCase();
				    	epcBytes.put(lable[i], epc);
				    	j = j+k+1;
				    }
				    return lable;
				}
			}catch(Exception e)
			{}
			return null;
		}
	 	private static void append(String hex) {
			// TODO Auto-generated method stub
			
		}



	 	public static String[] Scan6B()
	 	{
			byte Condition =0;
 			byte StartAddress=0;
 			byte mask=0;
 			byte err[]={-1};
 			byte ConditionContent[]={0,0,0,0,0,0,0,0};
	 		int result=uhf.Scan6B((byte)0xff, Condition, StartAddress, mask, ConditionContent, Scan6BData, Scan6BNum, err);
	 		
	 		Log.i("zhouxin", "==============Scan6B=============="+result+"====="+(byte)result);
			if(result==0){
				if(Scan6BNum[0]>0){
					soundThread.execute(soundRun);
				}
				
//				Scan6BNum=uhf.EPCC1G2_Inventory_POUcharTagNum();
//			    Scan6BData=uhf.EPCC1G2_Inventory_pOUcharUIDList();
			    /*int flag=(int)Scan6BData[0];
			    for(int i=0;i<Scan6BNum;i++)
			    {	
			    	
			    	for(int j=0;j<flag-1;j++)
			    	EPC_6C[i][j]=Scan6BData[1+j];
			    	
			    }*/
			    Log.i("zhouxin", "num = " + Scan6BNum[0] +" ******** data = "+bytesToHexString(Scan6BData));
			    
			    String[] uids = new String[Scan6BNum[0]];
			    byte[] uid;
			    for(int i = 0; i < Scan6BNum[0]; i++){
			    	uid = new byte[8];
			    	for(int j = 0; j < uid.length; j++){
			    		uid[j] = Scan6BData[i*10+j+1];
			    		Log.i("zhouxin", ">>>>>>>>>>>>>>>>>>"+uid[j]);
			    	}
			    	uids[i] = bytesToHexString(uid).toUpperCase();
			    }
			    
			    return uids;
			}
			return null;
		}
	 	
		
		public static int Read6C(byte ENum,
				byte EPC[],
				byte Mem,
				byte WordPtr,
				byte Num,
				byte Password[])
		{
			int result=uhf.ReadEPCC1G2(ENum, EPC, Mem, WordPtr, Num, Password);
			Read6Cdata=uhf.ReadEPCC1G2_Data();
			if(result==0)
			{	 uhf.get_CmdLen();
				 Read6CLen=uhf.get_presentLen()-6;
				 soundThread.execute(soundRun);
				return 0;
			}
			Read6CLen=0;
			return -1;
		}
		
		public static int Write6c(byte WNum,
				byte ENum, 
				byte EPC[],
				byte Mem,
				byte WordPtr,
				byte Writedata[],
				byte Password[])
		{
			int result=uhf.EPCC1G2_WriteCard_Errorcode(WNum, ENum, EPC, Mem, WordPtr, Writedata, Password);
			if (result==0)
			{
				soundThread.execute(soundRun);
			}
			return result;
		}
		
		
		public static int Write6B( byte ID_6B1[] ,
				 byte StartAddress, 
				 byte Writedata[], 
				 byte Writedatalen)
		{
			int result=uhf.ISO180006B_WriteCard_state(ID_6B1, StartAddress, Writedata, Writedatalen);
			if (result==0)
			{
				soundThread.execute(soundRun);
				return 0;
			}
			return -1;
		}
		
		
		public static int Read6B( byte ID_6B1[] ,
				  byte  StartAddress,
				  byte  Num)
		{	
			Read6Bdata=uhf.ISO180006B_ReadCard_Data(ID_6B1, StartAddress, Num);
			if(Read6Bdata[0]!=-1)
			{
				soundThread.execute(soundRun);
			    return 0;
			}
			else
			return -1;
		}
		
		   /**
	     * Convert byte[] to hex
	     * string
	     * 
	     * @param src byte[] data
	     * @return hex string
	     */
	    public static String bytesToHexString(byte[] src) {
	        StringBuilder stringBuilder = new StringBuilder("");
	        if (src == null || src.length <= 0) {
	            return null;
	        }
	        for (int i = 0; i < src.length; i++) {
	            int v = src[i] & 0xFF;
	            String hv = Integer.toHexString(v);
				if (hv.length() == 1){
					hv = '0' + hv;
				}
	            stringBuilder.append(hv);
	        }
	        return stringBuilder.toString();
	    }

	    public static String bytesToHexString(byte[] src, int offset, int length) {
	        StringBuilder stringBuilder = new StringBuilder("");
	        if (src == null || src.length <= 0) {
	            return null;
	        }
	        for (int i = offset; i < length; i++) {
	            int v = src[i] & 0xFF;
	            String hv = Integer.toHexString(v);
	            if (hv.length() == 1) {
	                stringBuilder.append(0);
	            }
	            stringBuilder.append(hv);
	        }
	        return stringBuilder.toString();
	    }


	    public static byte[] hexStringToBytes(String hexString) {  
	        if (hexString == null || hexString.equals("")) {  
	            return null;  
	        }  
	        hexString = hexString.toUpperCase();  
	        int length = hexString.length() / 2;  
	        char[] hexChars = hexString.toCharArray();  
	        byte[] d = new byte[length];  
	        for (int i = 0; i < length; i++) {  
	            int pos = i * 2;  
	            d[i] = (byte) (charToByte(hexChars[pos]) << 4 | charToByte(hexChars[pos + 1]));  
	        }  
	        return d;  
	    }   
	    private static byte charToByte(char c) {  
	        return (byte) "0123456789ABCDEF".indexOf(c);  
	    } 
		

}
}
