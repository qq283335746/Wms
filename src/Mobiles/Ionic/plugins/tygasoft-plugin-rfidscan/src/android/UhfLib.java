package com.UHF.scanlable;

import android.content.Context;
import android.util.Log;

public class UhfLib {
	private static final String TAG = null;
	
	private	byte TVersionInfo[]={-1,-1};
	private	byte ReaderType[]={-1};
	private	byte TrType[]={-1};
	private	byte dmaxfre[]={-1};
	private	byte dminfre[]={-1};;
	private	byte powerdBm[]={-1};
	private	byte ScanTime[]={-1};
	private	byte Ant[]={-1};
	private	byte BeepEn[]={-1};
	private	byte OutputRep[]={-1};
	private	byte CheckAnt[]={-1};
	byte cmd[]=new byte[1];
	int len[]={-1};
	int scan_len[]={-1};
	private byte s1[]=new byte[25600];
	
	private byte state[]=new byte[1];
	private int Cardnum[]=new int[1];
	private byte pOUcharUIDList[]=new byte[25600];
	private byte pOUcharUIDList1[]=new byte[25600];
	private int  pOUcharTagNum[]=new int[1];
	private byte Data[]=new byte[256];
	private byte ID_6B[]=new byte[256];
	private byte ID_6Bs[]=new byte[256];
	private byte Errorcode[]=new byte[1];
	private byte read_data[]=new byte[256];
	
	private int uhf_speed;
	private byte uhf_addr;
	private Context mContext;
	private String Serial;
	private int logswitch;
	public int array_clear(byte array_clear0[])
	{
		int clear_len=array_clear0.length;
		for(int i=0;i<clear_len;i++)
		{
			array_clear0[i]=0;
		}
		return 0;
	}
	//
	public UhfLib(int tty_speed,byte addr,String serial, int log_swith,Context mCt)
	{	
		//ModulePowerOn();
		uhf_speed = tty_speed;
		uhf_addr = addr;
		mContext = mCt;
		Serial = serial;
		logswitch = log_swith;
	}

public int open_reader()
  {
	  	int reply1=1,reply2=1;
	  	reply2=ModulePowerOn(Serial);
	  	reply1=ConnectReader(uhf_speed,logswitch);
	  	
	  	if((reply1==0)&&(reply2==0))
	  	{
	  		GetReaderInformation(uhf_addr, TVersionInfo, ReaderType, TrType, dmaxfre, dminfre, powerdBm, ScanTime, Ant, BeepEn, OutputRep, CheckAnt);
	  		return 0;
	  	}
  		return -1;
  	}
public int ReGetInfo()
{
	
	int result =GetReaderInformation(uhf_addr, TVersionInfo, ReaderType, TrType, dmaxfre, dminfre, powerdBm, ScanTime, Ant, BeepEn, OutputRep, CheckAnt);
	if  (result==0)
	return 0;
	return -1;
}

  public int close_reader()
  {
	  	
	  DisconnectReader();
	  return 0;
  }
  public int SetReader_Newaddress(byte newaddr)
  {
	  if(WriteAddress(uhf_addr,newaddr)==0)
	  {
		  uhf_addr=newaddr;
		  return 0;
	  }
	  return -1;
	 
  }
  

  public int SetReader_ScanTime(byte scantime)
  {
	
		 if(WriteScanTime(uhf_addr,scantime)==0)
		 {
			 ScanTime[0]=scantime;
			  return 0;
		 }
	  return -1;
  }

  public int SetReader_Power(byte power)
  {
	  if(WritePower(uhf_addr,power)==0)
	  {
		  powerdBm[0]=power;
		  return 0;
	  
	  }
	  	return -1;
  }
  
 
  public int SetReader_Freq(byte maxfre, byte minfre)
  {
	  if(WriteFreq(uhf_addr,maxfre,minfre)==0)
	  {  	
		  dmaxfre[0]=maxfre;
		  dminfre[0]=minfre;
		  return 0;
	  }
		  return -1;
  }
  

  public int SetReader_BaudRate(int fbaud)
  {	  byte fbaud1;
	  if(fbaud==9600)
		  fbaud1=0x00;
	  else if(fbaud==19200)
		  fbaud1=0x01;
	  else if(fbaud==38400)
		  fbaud1=0x02;
	  else if(fbaud==57600)
		  fbaud1=0x05;
	  else if(fbaud==115200)
		  fbaud1=0x06;
	  else
		  return -1;
	  if(WriteBaudRate(uhf_addr,fbaud1)==0)
	  {
		  uhf_speed=fbaud;
		  return 0;
	  }
	  return -1;
  }
  
  public byte[] Get_TVersionInfo()
  {
		  return TVersionInfo;
	  
  }
  public byte[] Get_ReaderType()
  {
		  return ReaderType;
	  
  }
  public  byte[] Get_TrType()
  {
		  return TrType;
	  
  }
  public byte[] Get_dmaxfre()
  {
		  return dmaxfre;
	  
  }
  public byte[] Get_dminfre()
  {
		  return dminfre;
	  
  }
  public byte[] Get_powerdBm()
  {
		  return powerdBm;
	  
  } 
  public byte[] Get_ScanTime()
  {
		  return ScanTime;
	  
  } 
  public byte[] Get_Ant()
  {
		  return Ant;
	  
  } 
  public byte[] Get_BeepEn()
  {
		  return BeepEn;
	  
  } 
  public byte[] Get_OutputRep()
  {
		  return OutputRep;
	  
  } 
  public byte[] Get_CheckAnt()
  {
		  return CheckAnt;
	  
  } 
 
  public int EPCC1G2_ScanEPC(
			byte QValue,
			byte Session,
			byte target
	)
  {  
	  byte s3[]={0x00};
	  byte s2=0x00;
	  int reply=-1;
	  //byte target=0;
	  byte ant=(byte)0x80;
	  byte scantime =10;
	  array_clear(pOUcharUIDList1);
	  reply=EPCC1G2_Inventory(uhf_addr,
			  scan_len,
				s1,
				QValue,
				Session,
				s2,
				s3,
				s2,
				s3,
				s2,
				s2,
				target,
				ant,
				scantime,
				state,
				pOUcharUIDList1,		
				pOUcharTagNum);
/*	  for(int i=0;i<100;i++)
		{
			Log.d(TAG, "pOUcharUIDList1 ["+i+"] = "+pOUcharUIDList1[i]);
		}
	  Log.d(TAG, "pOUcharTagNum  = "+pOUcharUIDList1[0]);
*/	  
	  if(reply==0)
		  return 0;
	  return -1;

	}

public byte EPCC1G2_Inventory_State()
	{
			
				return state[0];

	}
public byte[] EPCC1G2_Inventory_pOUcharUIDList()
	{
			
				return pOUcharUIDList1;
	} 
  
public int EPCC1G2_Inventory_POUcharTagNum()
	{
				return pOUcharTagNum[0];
	}  
public int EPCC1G2_Inventory_POUcharReadlen()
{
	int len=scan_len[0];
	String temp="";
	for(int i=0;i<len;i++)
	{
		String asd="";
		asd= Integer.toHexString(s1[i]&255).toUpperCase();
		if(asd.length()==1)
		{
			asd="0"+asd;
		}
		temp+=asd;
	}
	
	return scan_len[0];

} 

public int ReadEPCC1G2(
		byte ENum,
		byte EPC[],
		byte Mem,
		byte WordPtr,
		byte Num,
		byte Password[])
{
	array_clear(Data);
	if(EPCC1G2_ReadCard(uhf_addr,
			ENum,
			EPC,
			Mem,
			WordPtr,
			Num,
			Password,
			Data,
			Errorcode)==0)
		return 0;
	return -1;
	
}

public byte[] ReadEPCC1G2_Data()
	{
				return Data;
	}



public byte ReadEPCC1G2_Errorcode()
	{
				return Errorcode[0];

	}



public byte EPCC1G2_WriteCard_Errorcode(
		byte WNum,
		byte ENum, 
		byte EPC[],
		byte Mem,
		byte WordPtr,
		byte Writedata[],
		byte Password[])
{
			int reply=-1;
			reply=EPCC1G2_WriteCard (uhf_addr,
			WNum,
			ENum, 
			EPC,
		    Mem,
			WordPtr,
			Writedata,
			Password,
			Errorcode);
			if(reply==0)
				return Errorcode[0];
			return -1;
}


public byte EPCC1G2_EraseCard_Errorcode (
										byte ENum,
										byte EPC[], 
										byte Mem,
										byte WordPtr,
										byte Num,
										byte Password[])
{	int reply=-1;
	reply=EPCC1G2_EraseCard (uhf_addr, 
			ENum,
			EPC, 
			Mem,
			WordPtr,
			Num,
			Password, 
			Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;
}



public byte EPCC1G2_SetCardProtect_Errorcode(
		byte ENum,
		byte EPC[],
		byte select,
		byte setprotect,
		byte Password[])
{
		int reply=-1;
		reply=EPCC1G2_SetCardProtect (uhf_addr,
								ENum,
								EPC,
								select,
								setprotect,
								Password,
								Errorcode);
		if(reply==0)
			return Errorcode[0];
		return -1;
}



public byte EPCC1G2_DestroyCard_Errorcode(
										byte ENum,
										byte EPC[], 
										byte Password[])
{
	int reply=-1;
	EPCC1G2_DestroyCard(uhf_addr, 
					ENum,
					EPC, 
					Password,
					Errorcode);
	
	if(reply==0)
		return Errorcode[0];
	return -1;
}


public byte EPCC1G2_WriteEPC_Errorcode(
		byte ENum,
		byte Password[],
		byte WriteEPC[])
{
	int reply=-1;
	reply=EPCC1G2_WriteEPC (uhf_addr,
		ENum,
		Password,
		WriteEPC, 
		Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;
}


public byte EPCC1G2_SetReadProtect_Errorcode(
		byte ENum,
		byte EPC[], 
		byte Password[])
{
	int reply=-1;
	reply=EPCC1G2_SetReadProtect (uhf_addr,
		ENum,
		EPC, 
		Password,
		Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;
}



public byte EPCC1G2_SetMultiReadProtect_Errorcode(
		byte Password[])
{
	int reply=-1;
	reply=EPCC1G2_SetMultiReadProtect(uhf_addr, 
		Password,
		Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;
}


public byte EPCC1G2_RemoveReadProtect_Errorcode(
		byte Password[])
{
	int reply=-1;
	EPCC1G2_RemoveReadProtect (uhf_addr,
	Password, 
	Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;
}




public int CheckEPC_ReadProtected()
{
	if(EPCC1G2_CheckReadProtected (uhf_addr,
			state,
			Errorcode)==0)
		return 0;
	return -1;
}

public byte EPCC1G2_CheckEPC_ReadProtected_State()
{
		return state[0];

}
public byte CheckEPC_ReadProtected_Errorcodebyte()
{
		return Errorcode[0];
}


public byte EPCC1G2_SetEASAlarm_Errorcode(
		byte ENum,
		byte EPC[],
		byte Password[],
		byte EAS)
{
	int reply=-1;
	reply=EPCC1G2_SetEASAlarm (uhf_addr, 
		ENum,
		EPC,
		Password,
		EAS,
		Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;
	
}


public byte EPCC1G2_CheckEASAlarm_Errorcode(byte Errorcode[])
{
	int reply=-1;
	reply=EPCC1G2_CheckEASAlarm(uhf_addr,Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;
	
}


public byte EPCC1G2_LockUserBlock_Errorcode(
		byte ENum,
		byte EPC[],
		byte Password[],
		byte WordPtr)
{	
	int reply=-1;
	reply=EPCC1G2_LockUserBlock (uhf_addr, 
		ENum,
		EPC,
		Password,
		WordPtr, 
		Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;
}


public int EPCC1G2_QuerySinlgEPC()
{
	array_clear(pOUcharUIDList);
	if(EPCC1G2_QuerySinlgCard(uhf_addr,
			state,
			Ant,
			pOUcharUIDList,
			pOUcharTagNum)==0)
		return 0;
	return -1;
}

public byte EPCC1G2_QuerySinlgEPC_state()
{
		return Errorcode[0];
}

public byte EPCC1G2_QuerySinlgEPC_Ant()
{
		return Ant[0];
}
public int EPCC1G2_QuerySinlgEPC_pOUcharTagNum()
{
		return pOUcharTagNum[0];

}

public byte[] EPCC1G2_QuerySinlgEPC_pOUcharUIDList()
{
		return pOUcharUIDList;

}



public byte EPCC1G2_WriteBlock_Errorcode(
		byte WNum,
		byte ENum, 
		byte EPC[],
		byte Mem,
		byte WordPtr,
		byte Writedata[],
		byte Password[])
{	
	int reply=-1;
	reply=EPCC1G2_WriteBlock(uhf_addr,
		WNum,
		ENum, 
		EPC,
		Mem,
		WordPtr,
		Writedata,
		Password,
		Errorcode);
	if(reply==0)
		return Errorcode[0];
	return -1;

}




public int ISO180006B_SingleInventory()
{
	if(ISO180006B_Inventory(uhf_addr,ID_6B,Errorcode)==0)
		return 0;
	return -1;
}
/*
public  byte ISO180006B_SingleInventory_Errorcode()
{	
		return Errorcode[0];
}
*/
public  byte[]  ISO180006B_SingleInventory_ID_6B()
{	
		return ID_6B;
}







public int ISO180006B_multiInventory(byte Condition , 
byte StartAddress, 
byte mask , 
byte ConditionContent[])
{	
	array_clear(ID_6Bs);
	if(ISO180006B_Inventory2(uhf_addr, 
			Condition , 
			StartAddress, 
			mask , 
			ConditionContent,
			ID_6Bs,
			Cardnum,
			Errorcode)==0)
		return 0;
	return -1;
	
}

public byte[] ISO180006B_multiInventory_ID_6B()
{
		return ID_6Bs;
}
public int ISO180006B_multiInventory_Cardnum()
{
		return Cardnum[0];

}

/*public int ISO180006B_multiInventory_Errorcode()
{
		return Errorcode[0];

}

*/	
public byte[] ISO180006B_ReadCard_Data(
		  byte ID_6B1[] ,
		  byte  StartAddress,
		  byte  Num)
{
	array_clear(Data);
	int reply=-1;
	byte err[]={-1};
	reply=ISO180006B_ReadCard (uhf_addr,
					ID_6B1,
					StartAddress,
				    Num,
					Data,
					Errorcode);
	if(reply==0)
		return Data;
	return err;

}
public int ISO180006B_WriteCard_state(
		 byte ID_6B1[] ,
		 byte StartAddress, 
		 byte Writedata[], 
		 byte Writedatalen)
{
	
	if(ISO180006B_WriteCard(uhf_addr,
		 ID_6B1,
		 StartAddress, 
		 Writedata, 
		 Writedatalen,
		 Errorcode)==0)
		return 0;
	return -1;
}

public byte ISO180006B_CheckLock_State(
		   byte ID_6B1[] , 
		   byte CheckAddress)
{
	int reply=-1;
	ISO180006B_CheckLock(uhf_addr,
			   ID_6B1, 
			   CheckAddress, 
			   state,
			   Errorcode);
	if(reply==0)
		return state[0];
	return -1;
}

public int ISO180006B_Lockjbyte_State(
		   byte ID_6B1[] , 
		   byte LockAddress)
{
	if(ISO180006B_Lockjbyte(uhf_addr,
		   ID_6B1, 
		   LockAddress,
		   Errorcode)==0)
		return 0;
	return -1;
}

public int get_CmdLen(){

	//byte flag[]={0x00};
	array_clear(read_data);
	ModulePowerOffEx(cmd,len,read_data);
	return 0;
}

public int get_presentCMD(){
	return (int)cmd[0]; 
}

public int get_presentLen(){
	return (int)len[0]; 
}
public byte[] get_readdata(){
	return read_data;
}


public int Scan6B (byte addr, 
		byte Condition , 
		byte StartAddress, 
		byte mask , 
		byte ConditionContent[],
		byte ID_6B[] ,
		int  Cardnum[],
		byte Errorcode[])
{
	return ISO180006B_Inventory2 ( addr, 
			 Condition , 
			 StartAddress, 
			 mask , 
			 ConditionContent,
			 ID_6B ,
			  Cardnum,
			 Errorcode);
	
}


	static{
	 	System.loadLibrary("Uhf_jni");
	}
//5 test function	
static native int fsSayHello();
static native int fsLedOpen();
static native int fsLedOn();
static native int fsLedOff();
static native int ModulePowerOffEx(byte cmd_present[],int len_present[],byte c[]);


static native int ModulePowerOn(String serial);
static native int ConnectReader(int speed,int log_swith);
static native int DisconnectReader();
static native int WriteScanTime(byte addr, byte ScanTime);
static native int WritePower(byte addr, byte power);
static native int WriteAddress(byte addr, byte newAddr);
static native int WriteFreq (byte addr, byte maxfre, byte minfre);
static native int WriteBaudRate (byte addr, byte fbaud);
static native int GetReaderInformation(byte addr,
				byte TVersionInfo[],
				byte ReaderType[],
				byte TrType[],
				byte dmaxfre[],
				byte dminfre[],
				byte powerdBm[],
				byte ScanTime[],
				byte Ant[],
				byte BeepEn[],
				byte OutputRep[],
				byte CheckAnt[]);

/*static native int EPCC1G2_QuickInventoryEPC(byte addr,
		byte QValue,
		byte Session,
		byte Target,
		byte Ant,
		byte ScanTime,
		byte state[],
		byte pOUcharUIDList[],
		int pOUcharTagNum[]);

*/
//G2
/*static native int EPCC1G2_Inventory(byte addr,
		int len[],
		byte data[],
		byte QValue,
		byte Session,
		byte MaskMem,
		byte MaskAdr[],
		byte  MaskLen,
		byte MaskData[],
		byte  AdrTID,
		byte  LenTID,
		byte  Target,
		byte  Ant,
		byte  Scantime,
		byte state[],
		byte pOUcharIDList[],	
		int pOUcharTagNum[]);*/
static native int EPCC1G2_Inventory(byte addr,
		int len[],
		byte data[],
		byte QValue,
		byte Session,
		byte MaskMem,
		byte MaskAdr[],
		byte  MaskLen,
		byte MaskData[],
		byte  AdrTID,
		byte  LenTID,
		byte  Target,
		byte  Ant,
		byte  Scantime,
		byte state[],
		byte pOUcharIDList[],	
		int pOUcharTagNum[]);

static native int EPCC1G2_Inventory1(byte addr,
		int len[],
		byte data[],
		byte QValue,
		byte Session,
		byte MaskMem,
		byte MaskAdr[],
		byte  MaskLen,
		byte MaskData[],
		byte  AdrTID,
		byte  LenTID,
		byte  Target,
		byte  Ant,
		byte  Scantime,
		byte state[],
		byte pOUcharIDList[],	
		int pOUcharTagNum[]);

static native int EPCC1G2_Inventory_Mask(byte addr,
		byte MatchType,
		int MatchLen,
		int MatchOffset,
		byte EPCData[],
		byte pOUcharIDList[],
		int pOUcharTagNum[]);

static native int EPCC1G2_ReadCard(byte addr,
		byte ENum,
		byte EPC[],
		byte Mem,
		byte WordPtr,
		byte Num,
		byte Password[],
		byte Data[],
		byte Errorcode[]);

static native int EPCC1G2_WriteCard (byte addr,
	byte WNum,
	byte ENum, 
	byte EPC[],
	byte Mem,
	byte WordPtr,
	byte Writedata[],
	byte Password[],
	byte Errorcode[]);
	
static native int EPCC1G2_EraseCard (byte addr, 
	byte ENum,
	byte EPC[], 
	byte Mem,
	byte WordPtr,
	byte Num,
	byte Password[], 
	byte Errorcode[]);

static native int EPCC1G2_SetCardProtect (byte addr,
	byte ENum,
	byte EPC[],
	byte select,
	byte setprotect,
	byte Password[],
	byte  Errorcode[]);
	
static native int EPCC1G2_DestroyCard (byte addr, 
	byte ENum,
	byte EPC[], 
	byte Password[],
	byte Errorcode[]);

static native int EPCC1G2_WriteEPC (byte addr,
	byte ENum,
	byte Password[],
	byte WriteEPC[], 
	byte  Errorcode[]);

static native int EPCC1G2_SetReadProtect (byte addr,
	byte ENum,
	byte EPC[], 
	byte Password[],
	byte Errorcode[]);

static native int EPCC1G2_SetMultiReadProtect (byte addr, 
	byte Password[],
	byte Errorcode[]);

static native int EPCC1G2_RemoveReadProtect (byte addr,
	byte Password[], 
	byte Errorcode[]);

static native int  EPCC1G2_CheckReadProtected (byte addr,
	byte  State[],
	byte Errorcode[]);

static native int EPCC1G2_SetEASAlarm (byte addr, 
	byte ENum,
	byte EPC[],
	byte Password[],
	byte EAS,
	byte  Errorcode[]);

static native int EPCC1G2_CheckEASAlarm (byte addr,
	byte Errorcode[]);

static native int EPCC1G2_LockUserBlock (byte addr, 
	byte ENum,
	byte EPC[],
	byte Password[],
	byte WordPtr, 
	byte Errorcode[]);

static native int  EPCC1G2_QuerySinlgCard(byte addr,
	byte  state[],
	byte  Ant[],
	byte pOUcharIDList[],
	int pOUcharTagNum[]);

static native int EPCC1G2_WriteBlock(byte addr,
	byte WNum,
	byte ENum, 
	byte EPC[],
	byte Mem,
	byte WordPtr,
	byte Writedata[],
	byte Password[],
	byte Errorcode[]);
static native int ISO180006B_Inventory (byte addr,
	byte ID_6B[],
	byte Errorcode[]);

static native int  ISO180006B_Inventory2 (byte addr, 
	byte Condition , 
	byte StartAddress, 
	byte mask , 
	byte ConditionContent[],
	byte ID_6B[] ,
	int  Cardnum[],
	byte Errorcode[]);

static native int ISO180006B_ReadCard (byte addr,
		byte ID_6B[] ,
		byte StartAddress,
		byte Num,
		byte Data[],
		byte Errorcode[]);
	
static native int ISO180006B_WriteCard (byte addr,
	 byte ID_6B[] ,
	 byte StartAddress, 
	 byte Writedata[], 
	 byte Writedatalen,
	 byte Errorcode[]);

static native int  ISO180006B_CheckLock (byte addr,
   byte ID_6B[] , 
   byte CheckAddress, 
   byte State[],
   byte Errorcode[]);

static native int ISO180006B_Lockjbyte(byte addr,
   byte ID_6B[] , 
   byte LockAddress,
   byte Errorcode[]);

}

