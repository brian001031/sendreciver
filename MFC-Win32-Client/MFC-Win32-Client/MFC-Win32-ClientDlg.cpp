
// MFC-Win32-ClientDlg.cpp : 實作檔

#include "stdafx.h"
#include "MFC-Win32-Client.h"
#include "MFC-Win32-ClientDlg.h"
#include "afxdialogex.h"
#include <stdio.h>
#include "Winbase.h"

//新增標頭
#include "windows.h"
#include "winuser.h"

#include <stdlib.h>
#include <string.h>
//#include <string> //字元轉換字串，不加h
#include <afxmsg_.h>

//for 長字元加入參考lib
#include <tchar.h> 
#include "winNT.h"

#define ACTION_DISPLAY_TEXT         100  //傳送字元長度

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

//(+)全域 HWND VARIABLE(變數)，2016.10.13，Brian
HWND hName;
//(-)2016.10.13，Brian


//加入user32.dll 做呼叫連結,2016.10.12,Brian
PVOID pAddr = GetProcAddress(GetModuleHandleA("user32.dll") , "EndTask");
PVOID pAddr2 = GetProcAddress(GetModuleHandleA("user32.dll"), "FindWindowExA");
PVOID pAddr3 = GetProcAddress(GetModuleHandleA("user32.dll"), "RegisterWindowMessage");
PVOID pAddr4 = GetProcAddress(GetModuleHandleA("user32.dll"), "SendMessage");
PVOID pAddr5 = GetProcAddress(GetModuleHandleA("user32.dll"), "WindowProc");

//宣告物件指標
CButton * cbtn_Connect;
CEdit * cdit_String;
CEdit *	cdit_Recive;
CStatic * csta_String;
CStatic * csta_Recive;
CStatic * csta_Label;
CStatic * csta_GotRecive;

//倒數(單位:秒)
int i_timer = 5;
int i_timer2 = 10;
//兩次Send 旗標 ，用布林變數來判定
bool fMsg1 = false,fMsg2 = false;

//GUID全域變數儲存
CString strTextToDisplay;

// 對 App About 使用 CAboutDlg 對話方塊
class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// 對話方塊資料
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支援

// 程式碼實作
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


// CMFCWin32ClientDlg 對話方塊
CMFCWin32ClientDlg::CMFCWin32ClientDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CMFCWin32ClientDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMFCWin32ClientDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CMFCWin32ClientDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()

	//(+)新增驅動事件 2016.10.13，Brian
	ON_WM_CREATE()  	
	ON_BN_CLICKED(IDC_BtnSendCsharp, &CMFCWin32ClientDlg::OnBnClickedBtnsendcsharp)
	//(-)2016.10.13，Brian
	ON_WM_TIMER()
END_MESSAGE_MAP()


// CMFCWin32ClientDlg 訊息處理常式

BOOL CMFCWin32ClientDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 將 [關於...] 功能表加入系統功能表。

	// IDM_ABOUTBOX 必須在系統命令範圍之中。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 設定此對話方塊的圖示。當應用程式的主視窗不是對話方塊時，
	// 框架會自動從事此作業
	SetIcon(m_hIcon, TRUE);			// 設定大圖示
	SetIcon(m_hIcon, FALSE);		// 設定小圖示
	 
	//各Dialog元件初始化狀態，2016.10.13,Brian
	cdit_String = (CEdit *) GetDlgItem(	IDC_EDITRecive);
	cdit_Recive = (CEdit *) GetDlgItem(IDC_EDITSend);		
	csta_String = (CStatic *) GetDlgItem(IDC_STATICString);
	csta_Recive = (CStatic *) GetDlgItem(IDC_STATICRecive);	
	csta_Label  = (CStatic *) GetDlgItem(IDC_STATICTitle);	
	//新增收到標籤"22"做顯示，2016.10.28，Brian
	csta_GotRecive = (CStatic *) GetDlgItem(IDC_STATIC_GotRecive);

	//初始EditControl設定
	cdit_Recive->SetWindowText(L"MFC Give C#");

	return TRUE;  // 傳回 TRUE，除非您對控制項設定焦點
}

void CMFCWin32ClientDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// 如果將最小化按鈕加入您的對話方塊，您需要下列的程式碼，
// 以便繪製圖示。對於使用文件/檢視模式的 MFC 應用程式，
// 框架會自動完成此作業。

void CMFCWin32ClientDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 繪製的裝置內容

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 將圖示置中於用戶端矩形
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 描繪圖示
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// 當使用者拖曳最小化視窗時，
// 系統呼叫這個功能取得游標顯示。
HCURSOR CMFCWin32ClientDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

 int CMFCWin32ClientDlg::OnCreate(LPCREATESTRUCT lpCreateStruct)
 {
	 return TRUE;
 }


LRESULT CMFCWin32ClientDlg::WindowProc(UINT uMsg, WPARAM wParam,LPARAM lParam)
{	
	COPYDATASTRUCT* pCopyDataStruct;
	HWND hMySelf; 
	CWnd *pCdName;
	int iGUIDMTFLen = 1024;
    
	const char  FormName[]="C#FORM";

	char buf[1024];

    switch(uMsg)
    {
		case WM_COPYDATA:
		{				
			//記憶體轉換傳收
			pCopyDataStruct = (COPYDATASTRUCT*)lParam;
			memset(&buf, 0, sizeof(buf));
			memcpy(&buf,pCopyDataStruct->lpData,pCopyDataStruct->cbData);

			char cbuffer[128];
				
			//清掉布林變數
			
			if(fMsg1 && fMsg2 )
			{
			  fMsg1 = fMsg2 = false;
			  i_timer = 5;
			  i_timer2 = 10;
			}
			

			//if (0 == strcmp(buf,"MFC=577"))//在這裡判斷就可以了，判斷收到的字串內容是否一致 <-練習測試用 
			if (0 == strcmp(buf,"22"))//確認有接收到"22"，執行此區塊 ，2016.10.28，Brian
			{		        
				wchar_t szBuf;
				strcpy(cbuffer,buf);

				//接收字串
				CString aStr =(CString)cbuffer;					
				//szBuf++;

				csta_GotRecive->SetWindowTextW(aStr); //用標籤顯示，2016.10.28，Brian				
				//cdit_String->SetWindowTextW(aStr); //用欄位顯示

				//2個字元數
				//if(csta_GotRecive->GetWindowTextLengthW()==2)
				if(csta_GotRecive->GetWindowTextLengthW() > 0 )
				{
					fMsg1 =true;

					pCdName = GetDlgItem(IDD_MFCWIN32CLIENT_DIALOG);
					hMySelf = pCdName->GetSafeHwnd();

				    SetTimer(0,1000,NULL);//啟動計時器編號0每一秒觸發一次								  
				}

				//如果有配置記憶體
				if(aStr)
				{
					aStr.ReleaseBuffer(); //釋放記憶體，以免過度占用				 
				}

			}
			else  //收到Barcode R/W 的字串CodeEN128 (這邊先用 GUID做驗證)，2016.10.28，Brian
			{				
				//LPCTSTR szNewString = (LPCTSTR)(pCopyDataStruct->lpData);
				LPCTSTR szNewString;
				strcpy(cbuffer,buf);

				strTextToDisplay = (CString)cbuffer;

				LPCTSTR  lpstr = (LPCTSTR)strTextToDisplay;
			    
				//cdit_String->SetWindowTextW(lpstr);

				/********* (+)SendMessage to C＃FORM ,2016.10.19,Brian********/
				//if(cdit_String->GetWindowTextLengthW() > 0) //長度大於0，代表有接收到字串
				if(strTextToDisplay !="")
				{
					csta_GotRecive->SetWindowTextW(strTextToDisplay); //用標籤顯示，2016.10.28，Brian	

					fMsg2 = true;

				   //回傳給C#
					SetTimer(1,1000,NULL);//啟動計時器編號1每一秒觸發一次	
				
				}
				/*********(-) 2016.10.19,Brian********/	 
			}			
		}
		 break;		 
    }
	
	return CDialog::WindowProc(uMsg,wParam,lParam);	
	//return (LONG)DefWindowProc(uMsg,wParam,lParam); //輸出Release可用PASS!
}


void CMFCWin32ClientDlg::OnBnClickedBtnsendcsharp()
{
	
	//1.找視窗 這邊用"C#FORM"作範例，也可以用其他做驗證
	HWND hwnd = FindWindowExA(0 , 0 , 0 ,"C#FORM");

	if (hwnd == 0) 
	{
		MessageBox(L"can Not find C# ,Fail");
	}
	else
	{			
		unsigned int iLen2;
		unsigned int icount;
		CWnd * pCdName1;
		HWND  hMySelf1l;

		// Get the text from the edit control		
		CString strDisplayText;		
		unsigned int iLen = cdit_Recive->GetWindowTextLengthW();
		cdit_Recive->GetWindowTextW(strDisplayText);
							
		pCdName1 = GetDlgItem(IDD_MFCWIN32CLIENT_DIALOG);
		
		hMySelf1l = pCdName1->GetSafeHwnd(); //取得物件HWND		
		//::SendMessage(AfxGetMainWnd()->m_hWnd,WM_CHILDFRAMEDBCLK ,0,0);
		//PVOID msg ="OK Send to C#!";  //為測試字串		
		//******************************		
		//將CString 轉換 TCHAR 再轉換 PVOID
		iLen2 = strDisplayText.GetLength();  //字串長度

		//TCHAR *pUnicode = new TCHAR[strDisplayText.GetLength() + 1]; //只能秀一個字元，轉態有誤(NG)
		char *pUnicode = new char[strDisplayText.GetLength() + 1]; //正常(OK)

		for(icount = 0 ; icount < iLen2 ; icount++)
		{
			pUnicode[icount] = strDisplayText.GetAt(icount);		 
		}

		pUnicode[icount]='\0';

		PVOID msg = (PVOID)pUnicode;
		//******************************
		
		//結構變數原型宣告
		COPYDATASTRUCT  cds;

		memset(&cds, 0, sizeof(cds));					
		
		//cds.dwData = ACTION_DISPLAY_TEXT;
		cds.dwData = 0;
		cds.cbData = icount+1;	// +1 for the NULL
		cds.lpData = msg;
		
		//如果有字串內容
		if(strDisplayText)
		{			
			::SendMessageA(hwnd,WM_COPYDATA, (WPARAM)hName, (LPARAM)&cds); //傳送接收端
		}	
		//釋出配置記憶體
		strDisplayText.ReleaseBuffer();	   
		MessageBox(L"OK Button(按鈕) 送出MFC 字串 ");
	} 
}


void CMFCWin32ClientDlg::SendCSharpMSG(bool bSend1,bool bSend2)
{
	PVOID msg;
	int  GuidMtfLen = 1024;

	//1.找視窗 這邊用"C#FORM"作範例，也可以用其他做驗證
	HWND hwnd = FindWindowExA(0 , 0 , 0 ,"C#FORM");

	if (hwnd == 0) 
	{
		MessageBox(L"can Not find C# ,Fail");
	}
	else
	{			
		unsigned int iLen2;
		unsigned int icount;
		CWnd * pCdName1;
		HWND  hMySelf1l;		

		// Get the text from the edit control	
		/*
		CString strDisplayText;		
		unsigned int iLen = cdit_Recive->GetWindowTextLengthW();
		cdit_Recive->GetWindowTextW(strDisplayText);
							
		pCdName1 = GetDlgItem(IDD_MFCWIN32CLIENT_DIALOG);
		
		hMySelf1l = pCdName1->GetSafeHwnd(); //取得物件HWND				
		//******************************		
		//將CString 轉換 TCHAR 再轉換 PVOID
		iLen2 = strDisplayText.GetLength();  //字串長度

		//TCHAR *pUnicode = new TCHAR[strDisplayText.GetLength() + 1]; //只能秀一個字元，轉態有誤(NG)
		char *pUnicode = new char[strDisplayText.GetLength() + 1]; //正常(OK)

		for(icount = 0 ; icount < iLen2 ; icount++)
		{
			pUnicode[icount] = strDisplayText.GetAt(icount);		 
		}
		pUnicode[icount]='\0';
		*/

		/******(+)加入 bSend1 & bSend2 布林變數做判斷 ，2016.10.28，Brian*****/
		if(bSend1) 
		{
			if(bSend2)
			{								
				//LPCTSTR  lpstr = (LPCTSTR)strTextToDisplay;
				int len = strTextToDisplay.GetLength();                
				char *pBuf = new char[strTextToDisplay.GetLength()+1]; //正常(OK)
                			
				for(int i=0;i<len;i++)
				{
				   pBuf[i]=strTextToDisplay.GetAt(i);
				}

				pBuf[strTextToDisplay.GetLength()] = '\0';
				
			    //msg = (PVOID)pBuf;
			     msg = (PVOID)"SaveType=2;ItemName=focus;ItemResult=0;ErrorCode=DI;MtfCN=271;MtfCO=174;MtfUL=374;MtfUR=35;MtfBL=157;MtfBR=133;MtfCN2=0;MtfCO2=0;MtfUL2=0;MtfUR2=0;MtfBL2=0;MtfBR2=0;MaxCN=271;MaxUL=374;MaxUR=35;MaxBL=157;MaxBR=133;OfsCN=100;OfsCO=9;OfsUL=100;OfsUR=100;OfsBL=100;OfsBR=100"; //為Test測試字串
			
			}
			else
			{				
			    msg = (PVOID)"33";	//回傳給c# "33" 互通確認				
			}
		
		}
		//******************************
		
		//結構變數原型宣告
		COPYDATASTRUCT  cds;

		memset(&cds, 0, sizeof(cds));					
		
		//cds.dwData = ACTION_DISPLAY_TEXT;
		cds.dwData = 0;
		
		if(bSend1)
		{
			if(bSend2)
			{
			   //cds.cbData = icount+1;	// +1 for the NULL		
			   cds.cbData = GuidMtfLen;		
			}
			else
			{
			  cds.cbData = 10;						
			}
		}
		
		cds.lpData = msg;
			
	     //如果有字串內容
		if(msg)
		{		
		   ::SendMessageA(hwnd,WM_COPYDATA, (WPARAM)hName, (LPARAM)&cds); //傳送接收端	
		}
		else //都沒收到，判定有timeover逾時或視窗城市有異常可能
		{
		  AfxMessageBox(_T("沒有調焦數據字串，Fail,重新再一次!"));
		  return;		 
		}

	} 
}

void CMFCWin32ClientDlg::OnTimer(UINT_PTR nIDEvent)
{
	 HWND hTimer; 
	 CWnd *pCd;
	 
	 pCd = GetDlgItem(nIDEvent);
	 hTimer = pCd->GetSafeHwnd();

	 switch(nIDEvent)
	 {
		 case 0:
			 i_timer = i_timer-1; //每一秒數值減1
			 if( i_timer == 0)  //停止計時
		 	 {
				KillTimer(0);									
				//傳給C#視窗			
				SendCSharpMSG(fMsg1,fMsg2);
				//AfxMessageBox(_T("時間到0秒!"));
			 }
			 break;
		 case 1:
			 i_timer2 = i_timer2-1;
			 if( i_timer2 == 0)  //停止計時
		 	 {
				KillTimer(1);									
				//傳給C#視窗			
				SendCSharpMSG(fMsg1,fMsg2);
				AfxMessageBox(_T("MSG Send Final 時間到0秒!"));
			 }			 
			 break;
	 
	 }


	 /*
	 if(nIDEvent== 0) //編號255 TIMERID
	 {
	    i_timer = i_timer-1; //每一秒數值減1

		if( i_timer == 0)  //停止計時
		{
			//::KillTimer(hTimer,IDT_TIMER1);
			KillTimer(0);
									
			//傳給C#視窗			
			SendCSharpMSG(fMsg1,fMsg2);

			AfxMessageBox(_T("時間到0秒!"));

		}
	 }
	 */

	CDialogEx::OnTimer(nIDEvent);
}
