
// MFC-Win32-ClientDlg.h : 標頭檔
//

#pragma once

#include <tlhelp32.h>
#include "afxwin.h"
#include <windowsx.h>
#include <stdlib.h> 



//(+)新增應用程式(dID 、PID、結構變數)，2016.10.13，Brian
typedef struct
{
    unsigned int dID;
	unsigned int pID;
}HandshakeMsg;

typedef struct EnumFunArg
{
	HWND hWnd;
    DWORD dwProcessId;
}EnumFunArg;
//(-)2016.10.13，Brian


// CMFCWin32ClientDlg 對話方塊
class CMFCWin32ClientDlg : public CDialogEx
{
// 建構
public:
	CMFCWin32ClientDlg(CWnd* pParent = NULL);	// 標準建構函式

// 對話方塊資料
	enum { IDD = IDD_MFCWIN32CLIENT_DIALOG};

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支援


// 程式碼實作
protected:
	HICON m_hIcon;

	// 產生的訊息對應函式
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();

	//(+)以下為保護事件動作行為，2016.10.13，Brian
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct); 
	
	LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);
	//(-)2016.10.13，Brian
	
	DECLARE_MESSAGE_MAP()

public:
	//(+)以下為公用實作事件動作行為，2016.10.13，Brian

    //資料結購原型宣告
	COPYDATASTRUCT copydata;

	//按鈕傳送MSG事件		
	afx_msg void OnBnClickedBtnsendcsharp();
	afx_msg void OnTimer(UINT_PTR nIDEvent);

	//新增傳遞參數
	afx_msg void SendCSharpMSG(bool bSend1,bool bSend2);
};
