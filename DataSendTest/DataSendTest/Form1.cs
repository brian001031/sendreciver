using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//DLL Import 命名空間 2016.09.26,Brian
using System.Runtime.InteropServices;
using System.Diagnostics;
//Serial Port 端口
using System.IO.Ports;
using System.IO;

//使用NET 通道傳遞 2016.09.29,Brian
using System.Net;
using System.Net.Sockets;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Text.RegularExpressions;
using System.Security.Permissions; //使用存取權限2016.10.24，Brian
using System.Threading;

namespace DataSendTest
{
    public partial class FormTest : Form
    {
        string[] sSendType = { "數值", "字串"};

        //宣告靜態存取字串變數，2016.10.24，Brian
        static string sRunExeFile = null;
        
        const int WM_USER     = 0x0400;
        const int WM_COPYDATA = 0x004A;
        const int WM_APP      = 0x8000;
        const int WM_SETTEXT  = 0x000C;
        const int WM_CHILDFRAMEDBCLK = WM_USER + 1;
        const int WM_MY_MESSAGE = WM_USER + 100;
        const int WM_COMMAND    = 0x0111;

        //新增windows close驅動,2016.10.21,Brian
        const int WM_CLOSE      = 0x0010;

        //新增延遲時間，2016.10.28，Brian
        public int iWait = 0;

        //傳遞次數
        int icount = 0;

        public int iStop = 0;

        //判斷APPTest2是否有開啟,2016.10.20,Brian
        public bool bApExist = false;

        //動態字型大小宣告 ，提供接收做判斷
        private FontStyle g_fsView;
        private FontFamily g_fmInfo;
        private Font g_ftType;
        
        [DllImport("user32.dll")]
        private static extern int SendMessageInt(IntPtr hwnd, uint wMsg, int wParam, IntPtr lParam);
        

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);
        uint MSG_SHOW = RegisterWindowMessage("Show Message");

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        
        //新增字串串送連結方式，2016.10.05，Brian
        [DllImport("user32.dll")]       
        public static extern long SendMessage(
         IntPtr hWnd,
         uint Msg,
         uint wParam,
         ref COPYDATASTRUCT lParam //新的結構變數(多)參考引入，2016.10.05，Brian
         );

        //將結構佈局加入，因有新的Method ，2016.10.05，Brian
        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        public FormTest()
        {
            InitializeComponent();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            string sMfcPath = @"C:\MFC AP";                   //存放路徑
            string sDeskPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //桌面路徑(來源)
            string sMfcExeName = "MFC-Win32-Client.exe";      //來源檔

            //GLOBAL靜態變數存取
            sRunExeFile = sMfcPath + "\\" + sMfcExeName;

            //讀取權限全磁碟,需參考要不然會有存取錯誤ERROR碼，2016.10.24，Brian
            FileIOPermission fIOmission = new FileIOPermission(PermissionState.None);
            fIOmission.AllLocalFiles = FileIOPermissionAccess.Read;     

            //初始化Here
            for (int i = 0; i < sSendType.Count(); i++)
            {
                cbx_SelectOne.Items.Add(sSendType[i]);
            }

            /* 判斷APPTest2視窗，2016.10.20,Brian*/
            IntPtr IprAp = FindWindow(null, "APPTest2");//找APPTest2視窗的IntPtr 用來代表指標或控制代碼

            if (IprAp != IntPtr.Zero) //如果找到APPTest2應用視窗
            {
                bApExist = true;   //啟動代數
            }

            //預設60秒延遲，2016.11.02，Brian
            tbx_DelayInput.Text = "60";

            try 
            {
                //判斷檔案路徑資料夾是否存在，2016.10.24，Brian
                if (!Directory.Exists(sMfcPath)) //如果沒有此路徑資料夾
                {
                    Directory.CreateDirectory(sMfcPath); //建立路徑資料夾                   
                }

                //判斷MFC檔案是否存在            
                if (!File.Exists(sMfcPath + "\\" + sMfcExeName)) //如果沒有指定MFC.exe檔
                {
                    if (!File.Exists(sDeskPath + "\\" + sMfcExeName))
                    {
                        MessageBox.Show("請把MFC-Win32-Client.exe移至桌面，才能自動開啟");
                        return;
                    }

                    //source MFC CopyTo 指定 Path                     
                    File.Copy(Path.Combine(sDeskPath, sMfcExeName), Path.Combine(sMfcPath, sMfcExeName), true);                
                }

                ThreadStart th_Begin = new ThreadStart(WaitOpenTime);
                Thread th_Run = new Thread(th_Begin);
                th_Run.Start();
             
            }
            catch (System.Security.SecurityException s)
            {
                MessageBox.Show("~ Not Acess File ~");
            }
        }

        //static void WaitOpenTime(object tm)
        private void WaitOpenTime() 
        { 
           //int iTotal = int.Parse(tm.ToString());
           int iBegin = 0;

           var sRunPath = (string)DataSendTest.FormTest.sRunExeFile;

           //(+) 等待延遲一段時間(ms)，2016.10.24，Brian
           //for (int time = 0; time < iTotal; time++) ;        
           while (iBegin++ < 10) ;
       
           Thread.Sleep(5000);
           //啟動應用程式
           Process.Start(sRunExeFile);

           MessageBox.Show("@ -> Open MFC.exe Sucessful (Enter Next Step & GO)");
                    
        }

        //MSG傳送
        private void btn_Send_Click(object sender, EventArgs e)
        {
            string sSend = "";
            long lgResult;
            IntPtr iprMsg;
            IntPtr iprForm;
            IntPtr iprForm2;
            IntPtr iprSend;
            byte[] btArray;
            int ilen;


            lbl_ReciveFirst.Text = "等待收MFC";
            lbl_ReciveFirst.BackColor = Color.LightGreen;
            lbl_ReciveFirst.ForeColor = Color.Black;
            tbx_String.Text = ""; 
            

            IntPtr IprFormName = FindWindow(null, "NBAutoValue"); //找MFC的IntPtr 用來代表指標或控制代碼

            icount++;  //次數累加

            if (IprFormName != IntPtr.Zero ) //如果找到應用視窗
            {
                try
                {
                    if (tbx_DelayInput.Text == string.Empty)
                    {
                        MessageBox.Show("請輸入延遲秒數，TKS!");
                        return;
                    }

                    iStop = int.Parse(tbx_DelayInput.Text.ToString());

                    tbx_DelayInput.ReadOnly = true;

                    btn_Send.Enabled = false;

                    if (cbx_SelectOne.Text.ToString() == "數值")                    
                    {
                       
                        //輸入之數值如果其一空值
                        if( tbx_Num1.Text=="" || tbx_Num2.Text=="")
                        {
                            MessageBox.Show("請在數值兩個欄位key數字，TKS!");
                            return;
                        }

                        COPYDATASTRUCT cdsInt = new COPYDATASTRUCT();

                        //相加總合
                        int iSum = int.Parse(tbx_Num1.Text) + int.Parse(tbx_Num2.Text);

                        sSend = Convert.ToString(iSum);

                        iprMsg = Marshal.StringToHGlobalAnsi(sSend);

                        cdsInt.dwData = IntPtr.Zero;
                        cdsInt.cbData = sSend.Length;
                        cdsInt.lpData = iprMsg;

                        lgResult = SendMessage(IprFormName, WM_COPYDATA, 0, ref cdsInt);

                        Marshal.FreeHGlobal(iprMsg); //Free 所暫借用之Unmanage記憶體

                    }
                    else if (cbx_SelectOne.Text.ToString() == "字串") //新增2016.10.05，Brian
                    {
                        //欄位字串
                        sSend = tbx_String.Text.ToString();

                        iprMsg = Marshal.StringToHGlobalAnsi(sSend);

                        COPYDATASTRUCT cds = new COPYDATASTRUCT();
                        
                        cds.dwData = IntPtr.Zero;
                        cds.cbData = sSend.Length + 1;  //防止NULL，Memory Carsh
                        cds.lpData = iprMsg;

                        lgResult = SendMessage(IprFormName, WM_COPYDATA, 0, ref cds);

                        Marshal.FreeHGlobal(iprMsg); //Free 所暫借用之Unmanage記憶體
                    }
                    else
                    {
                        iprForm = FindWindow(null, "NBAutoValue");
                        iprForm2 = FindWindow(null, "APPTest2");

                        if (iprForm != IntPtr.Zero || iprForm2 != IntPtr.Zero)
                        {
                            //string sSendcharall = "MFC=577"; //測試字串，保留
                            //string sSendcharall = tbx_String.Text; //用欄位做傳遞
                            string sSendcharall ="22";  //使用MFC 初期溝通定義"22"，2016.10.28，Brian

                            if (sSendcharall.ToString() == "")
                            {
                                MessageBox.Show("請輸入字串，TKS!");
                                return;
                            }

                            //轉換成Unicode字元
                            iprSend = Marshal.StringToHGlobalAnsi(sSendcharall);

                            //以Byte為單位取值
                            btArray = System.Text.Encoding.Default.GetBytes(sSendcharall);

                            //字串長度
                            ilen = btArray.Length;

                            //結構原型宣告
                            COPYDATASTRUCT cds;
                            cds.dwData = (IntPtr)100;
                            cds.lpData = iprSend;
                            cds.cbData = ilen + 1;  //防止NULL，Memory Carsh
                            lgResult = SendMessage(iprForm, WM_COPYDATA, 0, ref cds);


                            Marshal.FreeHGlobal(iprSend); //Free 所暫借用之Unmanage記憶體

                          //  MessageBox.Show("請在MFC recive C#字串");

                            //開始計時
                            tm_Tcount.Interval = 1000; //以毫秒為單位，設定1秒觸發一次
                            tm_Tcount.Enabled = true; //啟動tm_Tcount


                        }
                        else
                        {
                            MessageBox.Show("~沒有找到視窗 ~");
                            return;                        
                        }
                    }               
                }
                catch (Exception y) //例外處理宣告
                {
                    throw y;
                }
            }
            else
            {
                MessageBox.Show("~Not Find 另一端視窗 ~");
            }            
        }

        //接收端回傳之訊息
        protected override void WndProc(ref Message m)
        {
            string sRecive = string.Empty; //宣告空值
            bool bIsNum = false;           //初始設0

            if (m.Msg == WM_COPYDATA) //接收字串狀態
            {
               
                COPYDATASTRUCT cds2 = new COPYDATASTRUCT();

                cds2 = (COPYDATASTRUCT)Marshal.PtrToStructure(m.LParam, typeof(COPYDATASTRUCT));

                if (cds2.cbData > 0)
                {
                    Type mytype = cds2.GetType();
                    cds2 = (COPYDATASTRUCT)m.GetLParam(mytype);

                    sRecive = Marshal.PtrToStringAnsi(cds2.lpData).ToString();

                    bIsNum = IsNumeric(sRecive);

                    //if (bIsNum && bApExist) //存數字且對方是APPTest2視窗傳來的狀況，2016.10.20.，Brian                    
                    if (bIsNum)  //數字型態 ，2016.10.28，Brian                                        
                    {
                        //tbx_String.Text = sRecive.ToString() + " -> 長度" + Convert.ToString(sRecive.Length);  //Debug 專用，判斷字元數是否正確
                        //顯示動態Label ，2016.10.28，Brian
                        g_fsView = lbl_ReciveFirst.Font.Style;
                        g_fmInfo = new FontFamily(lbl_ReciveFirst.Font.Name);
                        g_ftType = new Font(g_fmInfo, 15, g_fsView);
                        lbl_ReciveFirst.Font = g_ftType;
                        lbl_ReciveFirst.ForeColor = Color.Green;
                        lbl_ReciveFirst.BackColor = Color.DarkBlue;

                        lbl_ReciveFirst.Text = "收到:" + sRecive.ToString(); 

                        //再次傳送MFC (GUID)
                        SendMfcGuidMsg();

                    }
                    else //字串型態
                    {
                        //當停止秒數已到達時,不接收任何對方訊息，2016.11.02，Brian
                        if (label3.Text.ToString() == Convert.ToString(iStop ))
                        {

                            //顯示在接收字串欄位
                            tbx_String.Text = "沒收到調焦機終端處理字串  NG Here!";
                            iWait = 0;
                            tm_Tcount.Enabled = false;
                            tbx_DelayInput.ReadOnly = false;
                            btn_Send.Enabled = true;   
                        }
                        else  //接收對方字串
                        {
                            //顯示在接收字串欄位
                            tbx_String.Text = sRecive.ToString();

                            //計數器歸零，停止計時，2016.10.28，Brian
                            iWait = 0;
                            tm_Tcount.Enabled = false;
                            MessageBox.Show("收到調焦機終端處理字串 Final OK !");

                            tbx_DelayInput.ReadOnly = false;
                            btn_Send.Enabled = true;                                                             
                        }                               
                    }
                }                
            }                                   
            base.WndProc(ref m);
        }

        //定義一個函數,作用:判斷strNumber是否為數字,是數字返回True,不是數字返回False
        public bool IsNumeric(String strNumber)
        {
            string sReglar = "[^0-9.-]"; //正整數
            Regex NumberPattern = new Regex(sReglar);
            return !NumberPattern.IsMatch(strNumber);
        }

        //產生短字節的GUID且機率超低不重複
        private string GenerateStringID()
        {
            string sSubGuid = "";
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            sSubGuid = string.Format("{0:x}", i - DateTime.Now.Ticks);
            
            sSubGuid = sSubGuid.Substring(0,5);
            return sSubGuid;
            //return string.Format("{0:x}", i - DateTime.Now.Ticks);            
        }

        //傳遞GUID給MFC ，2016.10.28，Brian
        public void SendMfcGuidMsg()
        {
            IntPtr iprForm;
            IntPtr iprSend;
            int ilen;
            long lgResultGuid;
            byte[] btLenArray;
            string sGuid = string.Empty;

            iprForm = FindWindow(null, "NBAutoValue");         

            if (iprForm != IntPtr.Zero)
            {
                sGuid = GenerateStringID()+"-GUID";  //呼叫GUID 

                //轉換成Unicode字元
                iprSend = Marshal.StringToHGlobalAnsi(sGuid);

                //以Byte為單位取值
                btLenArray = System.Text.Encoding.Default.GetBytes(sGuid);

                //字串長度
                ilen = btLenArray.Length;

                //結構原型宣告
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = iprSend;
                cds.cbData = ilen + 1;  //防止NULL，Memory Carsh
                lgResultGuid = SendMessage(iprForm, WM_COPYDATA, 0, ref cds);

                Marshal.FreeHGlobal(iprSend); //Free 所暫借用之Unmanage記憶體
                
            }
        
        }

        //計時事件函式，2016.10.28，Brian
        private void tm_Tcount_Tick(object sender, EventArgs e)
        {                  
            iWait++;

            label3.Text = Convert.ToString(iWait);


            // 總等待時間 1000 ms * 60 =60000毫秒=60秒=1分鐘 
           // if (iWait == 60 && tbx_String.Text.ToString() == "") //已經60秒而且沒收到調焦字串
            //if (iWait == iStop) //已經逾時       
            if (iWait == iStop && tbx_String.Text.ToString() == "") //已經60秒而且沒收到調焦字串
            {
                //MessageBox.Show("已經逾時60秒，請重新再按 Send(傳) 按鈕  !");
                //tbx_String.Text="";
               // lbl_ReciveFirst.Text = "Cleat ALL";
               // MessageBox.Show("已經逾時" + Convert.ToString(iStop) + "秒");
               // iWait = 0;
                tm_Tcount.Enabled = false;                  
                return;
            }           
        }
    }
}
