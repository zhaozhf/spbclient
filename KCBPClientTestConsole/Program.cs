using System;
using System.Collections.Generic;
//using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//using System.Threading.Tasks;


namespace KCBPClientTestConsole
{
    //class Program
    //{
    //    //        [DllImport(@"d:\KCBPCli.dll", EntryPoint = "KCBPCLI_Init")]
    //    [DllImport(@"d:\KCBPCli.dll")]

    //    //"d:\VsCode\KCBPClientTest\KCBPClientTest\KCBPClientTestConsole\bin\Debug\KCBPCli.dll"
    //    //, EntryPoint = "_Enc7481_Set_Encoder", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
    //    public extern static int KCBPCLI_Init(IntPtr hHandle);
    //    [DllImport(@"d:\KCBPCli.dll")]
    //    public extern static int KCBPCLI_GetVersion(IntPtr hHandle, IntPtr pnVersion);

    //    static void Main(string[] args)
    //    {
    //        IntPtr hHandle = new IntPtr(0);

    //        IntPtr version = new IntPtr(0);
    //        //if (KCBPCLI_Init(hHandle) != 0)
    //        if (KCBPCLI_GetVersion(hHandle, version) != 0)
    //        {
    //            Console.WriteLine("KCBPCLI_Init error\n");
    //            Console.ReadLine();
    //        }

    //    }
    //}

    unsafe class Program
    {
        const int KCBP_SERVERNAME_MAX = 32;
        const int KCBP_DESCRIPTION_MAX = 32;


        [DllImport("Kernel32.dll")]
        public extern static bool Beep(uint iFreq, uint iDuration);

        [DllImport("KCBPCli.dll")]
        public extern static int KCBPCLI_Init(IntPtr hHandle);

        [DllImport("KCBPCli.dll")]
        public extern static int KCBPCLI_SetConnectOption(IntPtr hHandle, tagKCBPConnectOption connection);

        [DllImport("KCBPCli.dll")]
        public extern static int KCBPCLI_SetCliTimeOut(IntPtr hHandle, int TimeOut);

        [DllImport("KCBPCli.dll", CharSet = CharSet.Ansi)]
        //public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, char* ServerName, char* UserName, char* Password);
        //public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, string ServerName, string UserName, string Password);
        //public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, ref string ServerName, ref string UserName, ref string Password);
        //public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, string ServerName, string UserName, string Password);
        public extern static int KCBPCLI_SQLConnect(IntPtr hHandle, StringBuilder ServerName, StringBuilder UserName, StringBuilder Password);


        [DllImport("KCBPCli.dll", CharSet = CharSet.Ansi)]
        public extern static int KCBPCLI_ConnectServer(IntPtr hHandle, string ServerName, string UserName, string Password);
        [DllImport("KCBPCli.dll", CharSet = CharSet.Unicode)]
        public extern static int KCBPCLI_GetErrorCode(IntPtr hHandle, ref int pnErrno);
        [DllImport("KCBPCli.dll", CharSet = CharSet.Unicode)]
        public extern static int KCBPCLI_GetErrorMsg(IntPtr hHandle, string szError);

        //[StructLayout(LayoutKind.Sequential)]
        //public struct tagKCBPConnectOption
        //{
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = KCBP_SERVERNAME_MAX + 1)]
        //    public char[] szServerName;//= new char[KCBP_SERVERNAME_MAX + 1];
        //    public uint nProtocal;
        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = KCBP_DESCRIPTION_MAX + 1)]
        //    public char[] szAddress;// = new char[KCBP_DESCRIPTION_MAX + 1];
        //    public uint nPort;

        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = KCBP_DESCRIPTION_MAX + 1)]
        //    public char[] szSendQName;//= new char[KCBP_DESCRIPTION_MAX + 1];

        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = KCBP_DESCRIPTION_MAX + 1)]
        //    public char[] szReceiveQName;// = new char[KCBP_DESCRIPTION_MAX + 1];

        //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = KCBP_DESCRIPTION_MAX + 1)]
        //    public char[] szReserved;// = new char[KCBP_DESCRIPTION_MAX + 1];
        //}
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct tagKCBPConnectOption
        {

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szServerName;

            /// int
            public int nProtocal;

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szAddress;

            /// int
            public int nPort;

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szSendQName;

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
            public string szReceiveQName;

            /// char[33]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 33)]
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

            //bool b = Beep(100, 100);
            //Console.WriteLine(b.ToString());

            IntPtr ptr = Marshal.AllocHGlobal(1024);
            int resultInit = KCBPCLI_Init(ptr);

            int resultCon = KCBPCLI_SetConnectOption(ptr, connection);

            int resultTimeOut = KCBPCLI_SetCliTimeOut(ptr, 60);

            //char[] szUserName = "KCXP00".ToCharArray();
            //char[] szPassword = "888888".ToCharArray();
            //char[] serverName = "KCBP01".ToCharArray();

            //char* d, e, f;

            //fixed (char* a = &szUserName[0])
            //fixed (char* b = &szPassword[0])
            //fixed (char* c = &serverName[0])
            //{
            //    d = a;
            //    e = b;
            //    f = c;
            //}

            //string szUserName = "KCXP00";
            //string szPassword = "888888";
            //string serverName = "kcbp1";

            StringBuilder szUserName = new StringBuilder("KCXP00", 64);
            StringBuilder szPassword = new StringBuilder("888888", 64);
            StringBuilder serverName = new StringBuilder("kcbp1", 32);



            //int nReturnCode = KCBPCLI_SQLConnect(ptr, &serverName[0], &szUserName[0], &szPassword[0]);
            //int nReturnCode = KCBPCLI_SQLConnect(ptr, f, d, e);
            //int nReturnCode = KCBPCLI_SQLConnect(ptr, ref serverName,ref szUserName, ref szPassword);
            int nReturnCode = KCBPCLI_SQLConnect(ptr, serverName, szUserName, szPassword);
            Console.WriteLine(resultInit.ToString());
            Console.WriteLine(resultCon.ToString());
            Console.WriteLine(resultTimeOut.ToString());
            string message = string.Format("Connecting {0}:{1}, ReqQ:{2}, AnsQ:{3} ... ...\n",
                connection.szAddress, connection.nPort,
                connection.szSendQName, connection.szReceiveQName);
            Console.WriteLine(message);
            string message2 = string.Format("Connecting ServerName:{0},szUserName:{1},szPassword:{2} ... ...\n",
               serverName, szUserName, szPassword);
            Console.WriteLine(message2);
            Console.WriteLine(nReturnCode);

            nReturnCode = 2003;
            string szErrMsg = "";
            int x = KCBPCLI_GetErrorCode(ptr, ref nReturnCode);
            KCBPCLI_GetErrorMsg(ptr, szErrMsg);

            Console.WriteLine(szErrMsg.ToString());
            Console.WriteLine("x:" + x.ToString());
            Console.WriteLine(Marshal.PtrToStringAnsi(ptr));

            Console.ReadLine();
            Marshal.FreeHGlobal(ptr);
        }
    }
}
