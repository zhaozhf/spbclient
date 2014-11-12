using System;
using System.Collections.Generic;
//using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//using System.Threading.Tasks;


namespace KCBPClientTestConsole
{

    unsafe class Program
    //class Program
    {
        [DllImport("KCBPCli.dll")]
        public extern static int KCBPCLI_Init(IntPtr hHandle);

        [DllImport("KCBPCli.dll")]
        public extern static int KCBPCLI_SetConnectOption(IntPtr hHandle, tagKCBPConnectOption connection);

        [DllImport("KCBPCli.dll")]
        public extern static int KCBPCLI_SetCliTimeOut(IntPtr hHandle, int TimeOut);

        [DllImport("KCBPCli.dll", CharSet = CharSet.Auto)]
        //public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, char* ServerName, char* UserName, char* Password);
        //public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, string ServerName, string UserName, string Password);
        //public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, ref string ServerName, ref string UserName, ref string Password);
        public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, StringBuilder ServerName, StringBuilder UserName, StringBuilder Password);

        [DllImport("KCBPCli.dll", CharSet = CharSet.Ansi)]
        public extern static int KCBPCLI_ConnectServer(IntPtr hHandle, string ServerName, string UserName, string Password);
        [DllImport("KCBPCli.dll", CharSet = CharSet.Unicode)]
        public extern static int KCBPCLI_GetErrorCode(IntPtr hHandle, ref int pnErrno);
        [DllImport("KCBPCli.dll", CharSet = CharSet.Unicode)]
        public extern static int KCBPCLI_GetErrorMsg(IntPtr hHandle, string szError);
        [DllImport("KCBPCli.dll", CharSet = CharSet.Unicode)]
        public extern static int KCBPCLI_BeginWrite(IntPtr hHandle);

        [DllImport("KCBPCli.dll", CharSet = CharSet.Unicode)]
        public extern static int KCBPCLI_SetSystemParam(IntPtr hHandle, int nIndex, string szValue);

        [System.Runtime.InteropServices.StructLayoutAttribute(
            System.Runtime.InteropServices.LayoutKind.Sequential,
            CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct tagKCBPConnectOption
        {
            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute
                (System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szServerName;

            /// int
            public int nProtocal;

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute
                (System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szAddress;

            /// int
            public int nPort;

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute
                (System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szSendQName;

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute
                (System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szReceiveQName;

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute
                (System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szReserved;
        }

        unsafe static void Main(string[] args)
        {
            tagKCBPConnectOption connection = new tagKCBPConnectOption();
            connection.szServerName = "kcbp1";
            connection.nProtocal = 0;
            connection.szAddress = "10.1.12.32";
            connection.nPort = 21000;
            connection.szSendQName = "req_cs";
            connection.szReceiveQName = "ans_cs";

            IntPtr ptr = Marshal.AllocHGlobal(1024);

            int resultInit = KCBPCLI_Init(ptr);
            Console.WriteLine(resultInit.ToString());

            int resultCon = KCBPCLI_SetConnectOption(ptr, connection);
            Console.WriteLine(resultCon.ToString());

            int resultTimeOut = KCBPCLI_SetCliTimeOut(ptr, 60);
            Console.WriteLine(resultTimeOut.ToString());

            string message = string.Format("Connecting {0}:{1}, ReqQ:{2}, AnsQ:{3} ... ...\n",
               connection.szAddress, connection.nPort,
               connection.szSendQName, connection.szReceiveQName);
            Console.WriteLine(message);

            #region 使用char指针
            //char[] szUserName = "KCXP00".ToCharArray();
            //char[] szPassword = "888888".ToCharArray();
            //char[] serverName = "kcbp01".ToCharArray();

            //int nReturnCode = 0;
            //fixed (char* a = &szUserName[0])
            //fixed (char* b = &szPassword[0])
            //fixed (char* c = &serverName[0])
            //{
            //    nReturnCode = KCBPCLI_SQLConnect(ptr, c, a, b);
            //}

            //nReturnCode = KCBPCLI_SQLConnect(ptr, &serverName[0], &szUserName[0], &szPassword[0]);

            #endregion

            #region 使用string
            //string szUserName = "KCXP00";
            //string szPassword = "888888";
            //string serverName = "kcbp1";

            //int nReturnCode = KCBPCLI_SQLConnect(ptr, serverName, szUserName, szPassword);
            #endregion

            #region 使用&string
            //string szUserName = "KCXP00";
            //string szPassword = "888888";
            //string serverName = "kcbp1";

            //int nReturnCode = KCBPCLI_SQLConnect(ptr, ref serverName, ref szUserName, ref szPassword);

            #endregion

            #region 使用StringBuilder
            StringBuilder szUserName = new StringBuilder("KCXP00", 64);
            StringBuilder szPassword = new StringBuilder("888888", 64);
            StringBuilder serverName = new StringBuilder("kcbp1", 32);

            int nReturnCode = KCBPCLI_SQLConnect(ptr, serverName, szUserName, szPassword);
            #endregion

            string message2 = string.Format("Connecting ServerName:{0},szUserName:{1},szPassword:{2} ... ...\n",
               serverName, szUserName, szPassword);
            Console.WriteLine(message2);
            Console.WriteLine(nReturnCode);

            //nReturnCode = 2003;
            //string szErrMsg = "";
            //int ErrorResult = KCBPCLI_GetErrorCode(ptr, ref nReturnCode);
            //KCBPCLI_GetErrorMsg(ptr, szErrMsg);

            //Console.WriteLine(szErrMsg.ToString());
            //Console.WriteLine("x:" + ErrorResult.ToString());
            //Console.WriteLine(Marshal.PtrToStringAnsi(ptr));

            Console.ReadLine();
            Marshal.FreeHGlobal(ptr);
        }
    }
}
