using MetroSuite;
using System.Diagnostics;
using System;
using System.Text;
using System.Threading;
using WebSocketSharp;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Management;
using System.Net;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using MetroSuite;
using System.Net.NetworkInformation;
using System.Linq;

public partial class LoginForm : MetroForm
{
    public int ticks = 0;

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    static extern int OutputDebugString(string str);

    [DllImport("ntdll.dll")]
    internal static extern NtStatus NtSetInformationThread(IntPtr ThreadHandle, ThreadInformationClass ThreadInformationClass, IntPtr ThreadInformation, int ThreadInformationLength);

    [DllImport("kernel32.dll")]
    static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

    [DllImport("kernel32.dll")]
    static extern uint SuspendThread(IntPtr hThread);

    [DllImport("kernel32.dll")]
    static extern int ResumeThread(IntPtr hThread);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern bool CloseHandle(IntPtr handle);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtQueryInformationProcess([In] IntPtr ProcessHandle, [In] PROCESSINFOCLASS ProcessInformationClass, out IntPtr ProcessInformation, [In] int ProcessInformationLength, [Optional] out int ReturnLength);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtClose([In] IntPtr Handle);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtRemoveProcessDebug(IntPtr ProcessHandle, IntPtr DebugObjectHandle);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtSetInformationDebugObject([In] IntPtr DebugObjectHandle, [In] DebugObjectInformationClass DebugObjectInformationClass, [In] IntPtr DebugObjectInformation, [In] int DebugObjectInformationLength, [Out][Optional] out int ReturnLength);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtQuerySystemInformation([In] SYSTEM_INFORMATION_CLASS SystemInformationClass, IntPtr SystemInformation, [In] int SystemInformationLength, [Out][Optional] out int ReturnLength);

    static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] ref bool isDebuggerPresent);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsDebuggerPresent();

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

    private static string[] GetArray()
    {
        return new string[] { "procmon64", "codecracker", "x96dbg", "pizza", "OLLYDBG", "idaq", "idaq64", "pepper", "reverse", "reversal", "de4dot", "pc-ret", "crack", "ILSpy", "x32dbg", "sharpod", "x64dbg", "x32_dbg", "x64_dbg", "debug", "dbg", "strongod", "PhantOm", "titanHide", "scyllaHide", "ilspy", "graywolf", "simpleassemblyexplorer", "MegaDumper", "megadumper", "X64NetDumper", "x64netdumper", "HxD", "hxd", "PETools", "petools", "Protection_ID", "protection_id", "die", "process hacker 2", "process", "hacker", "ollydbg", "x32dbg", "x64dbg", "ida -", "charles", "dnspy", "simpleassembly", "peek", "httpanalyzer", "httpdebug", "fiddler", "Fiddler", "wireshark", "proxifier", "mitmproxy", "process hacker", "process monitor", "process hacker 2", "system explorer", "systemexplorer", "systemexplorerservice", "WPE PRO", "ghidra", "folderchangesview", "pc-ret", "folder", "dump", "proxy", "de4dotmodded", "StringDecryptor", "Centos", "SAE", "monitor", "brute", "checker", "zed", "sniffer", "http", "debugger", "james", "exeinfope", "codecracker", "x32dbg", "x64dbg", "ollydbg", "ida -", "charles", "dnspy", "simpleassembly", "peek", "httpanalyzer", "httpdebug", "fiddler", "wireshark", "dbx", "mdbg", "gdb", "windbg", "dbgclr", "kdb", "kgdb", "mdb", "sandboxierpcss", "e", "ProcessHacker", "FSociety" };
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr ZeroMemory(IntPtr addr, IntPtr size);

    [DllImport("kernel32.dll")]
    private static extern IntPtr VirtualProtect(IntPtr lpAddress, IntPtr dwSize, IntPtr flNewProtect, ref IntPtr lpflOldProtect);

    public LoginForm()
    {
        ProtectedString String1 = new ProtectedString("75.119.128.170");
        IPAddress[] addresslist = Dns.GetHostAddresses(String1.GetValue());

        foreach (IPAddress theaddress in addresslist)
        {
            if (theaddress.ToString() != String1.GetValue())
            {
                String1.Dispose();
                String1 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                WebSocketManager.CAN_DO = false;
                WebSocketManager.SelfDestroy();
                Process.GetCurrentProcess().Kill();
                return;
            }
        }

        String1.Dispose();
        String1 = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        foreach (string drive in Utils.GetDrives())
        {
            if (System.IO.File.Exists(drive + "\\Windows\\System32\\drivers\\etc\\hosts"))
            {
                if (System.IO.File.ReadAllText(drive + "\\Windows\\System32\\drivers\\etc\\hosts").Replace(" ", "").Replace('\t'.ToString(), "").Trim().Contains("75.119.128.170"))
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.SelfDestroy();
                    Process.GetCurrentProcess().Kill();
                    return;
                }
            }
        }

        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            ProtectedString theString = new ProtectedString("75.119.128.170");

            if (ip.ToString().Equals(theString.GetValue()))
            {
                theString.Dispose();
                theString = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                WebSocketManager.CAN_DO = false;
                WebSocketManager.SelfDestroy();
                Process.GetCurrentProcess().Kill();
                return;
            }

            theString.Dispose();
            theString = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        IPAddress[] addresslist1 = Dns.GetHostAddresses("localhost");

        foreach (IPAddress theaddress in addresslist1)
        {
            ProtectedString theString = new ProtectedString("75.119.128.170");

            if (theaddress.ToString() == theString.GetValue())
            {
                theString.Dispose();
                theString = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                WebSocketManager.CAN_DO = false;
                WebSocketManager.SelfDestroy();
                Process.GetCurrentProcess().Kill();
                return;
            }

            theString.Dispose();
            theString = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        new Thread(badHooks).Start();
        new Thread(antiSandbox).Start();
        new Thread(antiDebug).Start();

        try
        {

        }
        catch
        {
            Process.GetCurrentProcess().Kill();
            return;
        }

        CheckForIllegalCrossThreadCalls = false;
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
        WebSocketManager.SecureConnect();

        ProtectedString theString1 = new ProtectedString("{\"t\": 0, \"h\": \"" + Utils.GetUniqueID() + "\", \"r\": " + Utils.GetUniqueLong(10).ToString() + ", \"s\": \"" + Utils.GetUniqueKey(19) + "\", \"a\": \"NEBULA_GENERATOR\", \"v\": \"RELEASE_V3.0\", \"p\": \"PACKET_PRESENTATION\", \"m\": \"" + TimestampUtils.GetTimestamp() + "\", \"y\": \"" + Utils.GetSHA1Hash() + "\", \"f\": \"" + Utils.GetFileLength().ToString() + "\"}");
        WebSocketManager.SecureSend(theString1.GetValue());
        theString1.Dispose();
        theString1 = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        while (!WebSocketManager.CONFIRM_PRESENTATION)
        {
            Thread.Sleep(1000);
        }

        Thread checkWebSocketThread = new Thread(CheckWebSocket);
        checkWebSocketThread.Priority = ThreadPriority.Highest;
        checkWebSocketThread.Start();

        InitializeComponent();

        if (System.IO.File.Exists("credentials.txt"))
        {
            System.Collections.Generic.List<string> credentials = new System.Collections.Generic.List<string>();

            foreach (string line in Utils.SplitToLines(System.IO.File.ReadAllText("credentials.txt")))
            {
                credentials.Add(line);
            }

            if (credentials.Count != 2)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }

            if (credentials[0].Length > 24)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }

            if (credentials[1].Length > 80)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }

            gunaLineTextBox1.Text = credentials[0];
            gunaLineTextBox2.Text = credentials[1];
        }
    }

    public void hideFromDebugger()
    {
        Thread.Sleep(10000);

        try
        {
            HideOSThreads();
        }
        catch
        {

        }
    }

    public static bool CheckDebuggerManagedPresent()
    {
        try
        {
            return Debugger.IsAttached;
        }
        catch
        {
            return false;
        }
    }

    public static bool CheckDebuggerUnmanagedPresent()
    {
        try
        {
            return IsDebuggerPresent();
        }
        catch
        {
            return false;
        }
    }

    public static bool CheckRemoteDebugger()
    {
        try
        {
            var isDebuggerPresent = false;
            var bApiRet = CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);

            return bApiRet && isDebuggerPresent;
        }
        catch
        {
            return false;
        }
    }

    public static bool CheckDebugPort()
    {
        try
        {
            NtStatus status;
            IntPtr DebugPort = new IntPtr(0);
            int ReturnLength;

            unsafe
            {
                status = NtQueryInformationProcess(Process.GetCurrentProcess().Handle, PROCESSINFOCLASS.ProcessDebugPort, out DebugPort, Marshal.SizeOf(DebugPort), out ReturnLength);

                if (status == NtStatus.Success)
                {
                    return DebugPort == new IntPtr(-1);
                }
            }
        }
        catch
        {

        }

        return false;
    }

    public static bool DetachFromDebuggerProcess()
    {
        try
        {
            IntPtr hDebugObject = INVALID_HANDLE_VALUE;
            var dwFlags = 0U;
            NtStatus ntStatus;
            int retLength_1;
            int retLength_2;

            unsafe
            {
                try
                {
                    ntStatus = NtQueryInformationProcess(Process.GetCurrentProcess().Handle, PROCESSINFOCLASS.ProcessDebugObjectHandle, out hDebugObject, IntPtr.Size, out retLength_1);

                    if (ntStatus != NtStatus.Success)
                    {
                        return false;
                    }
                }
                catch
                {

                }

                try
                {
                    ntStatus = NtSetInformationDebugObject(hDebugObject, DebugObjectInformationClass.DebugObjectFlags, new IntPtr(&dwFlags), Marshal.SizeOf(dwFlags), out retLength_2);

                    if (ntStatus != NtStatus.Success)
                    {
                        return false;
                    }
                }
                catch
                {

                }

                try
                {
                    ntStatus = NtRemoveProcessDebug(Process.GetCurrentProcess().Handle, hDebugObject);

                    if (ntStatus != NtStatus.Success)
                    {
                        return false;
                    }
                }
                catch
                {

                }

                try
                {
                    ntStatus = NtClose(hDebugObject);

                    if (ntStatus != NtStatus.Success)
                    {
                        return false;
                    }
                }
                catch
                {

                }
            }
        }
        catch
        {

        }

        return true;
    }
    public static bool CheckKernelDebugInformation()
    {
        try
        {
            SYSTEM_KERNEL_DEBUGGER_INFORMATION pSKDI;
            int retLength;
            NtStatus ntStatus;

            unsafe
            {
                ntStatus = NtQuerySystemInformation(SYSTEM_INFORMATION_CLASS.SystemKernelDebuggerInformation, new IntPtr(&pSKDI), Marshal.SizeOf(pSKDI), out retLength);

                if (ntStatus == NtStatus.Success)
                {
                    return pSKDI.KernelDebuggerEnabled && !pSKDI.KernelDebuggerNotPresent;
                }
            }
        }
        catch
        {

        }

        return false;
    }

    public static void HideOSThreads()
    {
        try
        {
            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;

            foreach (ProcessThread thread in currentThreads)
            {
                try
                {
                    IntPtr pOpenThread = OpenThread(ThreadAccess.SET_INFORMATION, false, (uint)thread.Id);

                    if (pOpenThread == IntPtr.Zero)
                    {
                        continue;
                    }

                    HideFromDebugger(pOpenThread);
                    CloseHandle(pOpenThread);
                }
                catch
                {

                }
            }
        }
        catch
        {

        }
    }

    public static bool HideFromDebugger(IntPtr Handle)
    {
        try
        {
            return NtSetInformationThread(Handle, ThreadInformationClass.ThreadHideFromDebugger, IntPtr.Zero, 0) == NtStatus.Success;
        }
        catch
        {
            return false;
        }
    }

    public void antiDebug()
    {
        while (true)
        {
            Thread.Sleep(1000);

            try
            {
                if (CheckDebuggerManagedPresent())
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
                else if (CheckDebuggerUnmanagedPresent())
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
                else if (CheckRemoteDebugger())
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
                else if (CheckDebugPort())
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
                else if (DetachFromDebuggerProcess())
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
                else if (CheckKernelDebugInformation())
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
                else if (Debugger.IsLogging())
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
                else if (string.Compare(Environment.GetEnvironmentVariable("COR_ENABLE_PROFILING"), "1", StringComparison.Ordinal) == 0)
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
                else
                {
                    if (Process.GetCurrentProcess().Handle == IntPtr.Zero)
                    {
                        WebSocketManager.CAN_DO = false;
                        WebSocketManager.BanMe();
                        Process.GetCurrentProcess().Kill();

                        return;
                    }

                    if (OutputDebugString("") > IntPtr.Size)
                    {
                        WebSocketManager.CAN_DO = false;
                        WebSocketManager.BanMe();
                        Process.GetCurrentProcess().Kill();

                        return;
                    }

                    try
                    {
                        CloseHandle(IntPtr.Zero);
                    }
                    catch
                    {
                        WebSocketManager.CAN_DO = false;
                        WebSocketManager.BanMe();
                        Process.GetCurrentProcess().Kill();

                        return;
                    }

                    if (GetModuleHandle("vehdebug-i386.dll").ToInt32() != 0)
                    {
                        WebSocketManager.CAN_DO = false;
                        WebSocketManager.BanMe();
                        Process.GetCurrentProcess().Kill();

                        return;
                    }

                    else if (GetModuleHandle("vehdebug-x86_64.dll").ToInt32() != 0)
                    {
                        WebSocketManager.CAN_DO = false;
                        WebSocketManager.BanMe();
                        Process.GetCurrentProcess().Kill();

                        return;
                    }
                }
            }
            catch
            {

            }
        }
    }

    public void antiSandbox()
    {
        while (true)
        {
            Thread.Sleep(1000);

            try
            {
                if (GetModuleHandle("SbdieDll.dll").ToInt32() != 0)
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }

                if (Process.GetCurrentProcess().MainWindowTitle.StartsWith("[#]") && Process.GetCurrentProcess().MainWindowTitle.EndsWith("[#]"))
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
            }
            catch
            {

            }
        }
    }

    public void badProcess()
    {
        while (true)
        {
            Thread.Sleep(1000);

            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    foreach (string proc in GetArray())
                    {
                        try
                        {
                            if ((process.ProcessName == proc || process.ProcessName.ToLower().Contains("cheat") || process.ProcessName.ToLower().Contains("fiddler") || process.MainWindowTitle.ToLower().Contains("fiddler") || process.ProcessName.ToLower().Contains("exploit") || process.ProcessName.ToLower().Contains("clicker")) && process.Id != Process.GetCurrentProcess().Id)
                            {
                                Process.GetCurrentProcess().Kill();

                                return;
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                catch
                {

                }
            }
        }
    }

    public void badHooks()
    {
        while (true)
        {
            Thread.Sleep(1000);

            try
            {
                IntPtr kernel32 = LoadLibrary("kernel32.dll");
                IntPtr GetProcessId = GetProcAddress(kernel32, "IsDebuggerPresent");

                byte[] data = new byte[1];

                Marshal.Copy(GetProcessId, data, 0, 1);

                if (data[0] == 0xE9)
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }

                GetProcessId = GetProcAddress(kernel32, "CheckRemoteDebuggerPresent");
                data = new byte[1];
                Marshal.Copy(GetProcessId, data, 0, 1);

                if (data[0] == 0xE9)
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }

                GetProcessId = GetProcAddress(kernel32, "WriteProcessMemory");
                data = new byte[1];
                Marshal.Copy(GetProcessId, data, 0, 1);

                if (data[0] == 0xE9)
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }

                GetProcessId = GetProcAddress(kernel32, "ReadProcessMemory");
                data = new byte[1];
                Marshal.Copy(GetProcessId, data, 0, 1);

                if (data[0] == 0xE9)
                {
                    WebSocketManager.CAN_DO = false;
                    WebSocketManager.BanMe();
                    Process.GetCurrentProcess().Kill();

                    return;
                }
            }
            catch
            {

            }
        }
    }

    public void CheckWebSocket()
    {
        while (true)
        {
            Thread.Sleep(1000);

            if (WebSocketManager.LOGGED_IN && WebSocketManager.CONFIRM_PRESENTATION)
            {
                return;
            }

            if (!(WebSocketManager.ws.IsAlive && WebSocketManager.ws.ReadyState == WebSocketState.Open))
            {
                WebSocketManager.BanMe();
                Process.GetCurrentProcess().Kill();
            }

            ProtectedString theString1 = new ProtectedString("ws://75.119.128.170:4649/NebulaGenerator");

            if (WebSocketManager.ws.Url.ToString() != theString1.GetValue())
            {
                theString1.Dispose();
                theString1 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                WebSocketManager.CAN_DO = false;
                WebSocketManager.BanMe();
                Process.GetCurrentProcess().Kill();

                return;
            }

            theString1.Dispose();
            theString1 = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }

    public string AES_Encrypt(string input, string pass)
    {
        var AES = new System.Security.Cryptography.RijndaelManaged();

        try
        {
            var hash = new byte[32];
            var temp = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(pass));

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);

            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;

            var Buffer = Encoding.ASCII.GetBytes(input);

            return Convert.ToBase64String(AES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch
        {
            Process.GetCurrentProcess().Kill();

            return "";
        }
    }

    public string AES_Decrypt(string input, string pass)
    {
        var AES = new System.Security.Cryptography.RijndaelManaged();

        try
        {
            var hash = new byte[32];
            var temp = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(pass));

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);

            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;
            var Buffer = Convert.FromBase64String(input);

            return Encoding.ASCII.GetString(AES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch
        {
            Process.GetCurrentProcess().Kill();

            return "";
        }
    }

    private void gunaButton4_Click(object sender, EventArgs e)
    {
        gunaButton4.Enabled = false;

        foreach (char c in gunaLineTextBox1.Text)
        {
            bool valid = false;

            foreach (char s in Utils.charsToCheck)
            {
                if (c.ToString() == s.ToString())
                {
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }
        }

        foreach (char c in gunaLineTextBox2.Text)
        {
            bool valid = false;

            foreach (char s in Utils.charsToCheck)
            {
                if (c.ToString() == s.ToString())
                {
                    valid = true;
                    break;
                }
            }

            if (!valid)
            {
                Process.GetCurrentProcess().Kill();
                return;
            }
        }

        WebSocketManager.loginStatus = 0;
        ProtectedString theString1 = new ProtectedString("{\"t\": 4, \"h\": \"" + Utils.GetUniqueID() + "\", \"r\": " + Utils.GetUniqueLong(10).ToString() + ", \"s\": \"" + Utils.GetUniqueKey(19) + "\", \"a\": \"NEBULA_GENERATOR\", \"v\": \"RELEASE_V3.0\", \"p\": \"PACKET_LOGIN\", \"u\": \"" + gunaLineTextBox1.Text + "\", \"n\": \"" + gunaLineTextBox2.Text + "\", \"m\": \"" + TimestampUtils.GetTimestamp() + "\", \"y\": \"" + Utils.GetSHA1Hash() + "\", \"f\": \"" + Utils.GetFileLength().ToString() + "\"}");
        WebSocketManager.SecureSend(theString1.GetValue());
        theString1.Dispose();
        theString1 = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        timer1.Start();
    }

    private void vapp_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        Process.GetCurrentProcess().Kill();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        if (ticks >= 10)
        {
            Process.GetCurrentProcess().Kill();
            return;
        }
        else
        {
            if (WebSocketManager.loginStatus == 1)
            {
                timer1.Stop();
                Process.GetCurrentProcess().Kill();
            }
            else if (WebSocketManager.loginStatus == 2)
            {
                timer1.Stop();

                System.IO.File.WriteAllText("credentials.txt", gunaLineTextBox1.Text + Environment.NewLine + gunaLineTextBox2.Text);

                new MainForm().Show();

                this.Hide();
                this.Size = new System.Drawing.Size(0, 0);
                this.Visible = false;
                this.Enabled = false;
                this.Opacity = 0;
            }
            else
            {
                ticks++;
            }
        }
    }
}