
// MFC-Win32-Client.h : PROJECT_NAME ���ε{�����D�n���Y��
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�� PCH �]�t���ɮ׫e���]�t 'stdafx.h'"
#endif

#include "resource.h"		// �D�n�Ÿ�


// CMFCWin32ClientApp:
// �аѾ\��@�����O�� MFC-Win32-Client.cpp
//

class CMFCWin32ClientApp : public CWinApp
{
public:
	CMFCWin32ClientApp();

// �мg
public:
	virtual BOOL InitInstance();

// �{���X��@

	DECLARE_MESSAGE_MAP()
};

extern CMFCWin32ClientApp theApp;