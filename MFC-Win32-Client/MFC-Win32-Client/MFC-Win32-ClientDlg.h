
// MFC-Win32-ClientDlg.h : ���Y��
//

#pragma once

#include <tlhelp32.h>
#include "afxwin.h"
#include <windowsx.h>
#include <stdlib.h> 



//(+)�s�W���ε{��(dID �BPID�B���c�ܼ�)�A2016.10.13�ABrian
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
//(-)2016.10.13�ABrian


// CMFCWin32ClientDlg ��ܤ��
class CMFCWin32ClientDlg : public CDialogEx
{
// �غc
public:
	CMFCWin32ClientDlg(CWnd* pParent = NULL);	// �зǫغc�禡

// ��ܤ�����
	enum { IDD = IDD_MFCWIN32CLIENT_DIALOG};

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV �䴩


// �{���X��@
protected:
	HICON m_hIcon;

	// ���ͪ��T�������禡
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();

	//(+)�H�U���O�@�ƥ�ʧ@�欰�A2016.10.13�ABrian
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct); 
	
	LRESULT WindowProc(UINT message, WPARAM wParam, LPARAM lParam);
	//(-)2016.10.13�ABrian
	
	DECLARE_MESSAGE_MAP()

public:
	//(+)�H�U�����ι�@�ƥ�ʧ@�欰�A2016.10.13�ABrian

    //��Ƶ��ʭ쫬�ŧi
	COPYDATASTRUCT copydata;

	//���s�ǰeMSG�ƥ�		
	afx_msg void OnBnClickedBtnsendcsharp();
	afx_msg void OnTimer(UINT_PTR nIDEvent);

	//�s�W�ǻ��Ѽ�
	afx_msg void SendCSharpMSG(bool bSend1,bool bSend2);
};
