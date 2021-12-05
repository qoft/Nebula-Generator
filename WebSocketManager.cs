using WebSocketSharp;
using System.Diagnostics;
using System;
using System.Text;
using Microsoft.VisualBasic;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

public static class WebSocketManager
{
    public static WebSocket ws;
    public static bool CONFIRM_PRESENTATION, LOGGED_IN, CAN_DO = true;
    public static byte loginStatus;

    public static void SecureConnect()
    {
        try
        {
            ProtectedString theString1 = new ProtectedString("ws://75.119.128.170:4649/NebulaGenerator");
            ws = new WebSocket(theString1.GetValue());
            theString1.Dispose();
            theString1 = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            ws.EnableRedirection = false;
            ws.Compression = CompressionMethod.Deflate;
            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            ws.OnMessage += Ws_OnMessage;
            ws.Connect();
        }
        catch
        {

        }
    }

    private static void Ws_OnMessage(object sender, MessageEventArgs e)
    {
        try
        {
            if (!CAN_DO)
            {
                return;
            }

            if (ws.IsAlive && ws.ReadyState == WebSocketState.Open)
            {
                ProcessMessage(new ProtectedString(CustomDecrypt(Encoding.Unicode.GetString(e.RawData))));
            }
            else
            {
                CAN_DO = false;
                BanMe();
                Process.GetCurrentProcess().Kill();
            }
        }
        catch
        {
            CAN_DO = false;
            BanMe();
            Process.GetCurrentProcess().Kill();
        }
    }

    public static void ProcessMessage(ProtectedString theStr)
    {
        try
        {
            if (!CAN_DO)
            {
                return;
            }

            dynamic jss = JObject.Parse(theStr.GetValue());
            theStr.Dispose();
            theStr = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            int type = (int)jss.t;
            long range = (long)jss.r;
            ProtectedString randomString = new ProtectedString((string)jss.s);
            bool confirm = (bool)jss.c;
            ProtectedString packetType = new ProtectedString((string)jss.p);
            ProtectedString timestamp = new ProtectedString((string)jss.m);

            if (range.ToString().Length != 13)
            {
                CAN_DO = false;
                BanMe();
                Process.GetCurrentProcess().Kill();
                return;
            }

            if (randomString.GetValue().Length != 27)
            {
                CAN_DO = false;
                BanMe();
                Process.GetCurrentProcess().Kill();
                return;
            }

            if (!TimestampUtils.IsTimestampValid(timestamp.GetValue()))
            {
                CAN_DO = false;
                BanMe();
                Process.GetCurrentProcess().Kill();
                return;
            }

            randomString.Dispose();
            randomString = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (!confirm)
            {
                CAN_DO = false;
                BanMe();
                Process.GetCurrentProcess().Kill();
                return;
            }

            if (type == 1 && !CONFIRM_PRESENTATION)
            {
                ProtectedString theString1 = new ProtectedString("CONFIRM_PRESENTATION");

                if (packetType.GetValue() != theString1.GetValue())
                {
                    CAN_DO = false;
                    theString1.Dispose();
                    theString1 = null;
                    packetType.Dispose();
                    packetType = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    BanMe();
                    Process.GetCurrentProcess().Kill();
                    return;
                }

                theString1.Dispose();
                theString1 = null;
                packetType.Dispose();
                packetType = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                CONFIRM_PRESENTATION = true;
            }
            else if (type == 3)
            {
                CAN_DO = false;
                BanMe();
                Process.GetCurrentProcess().Kill();
                return;
            }
            else if (type == 5 && !LOGGED_IN)
            {
                ProtectedString theString1 = new ProtectedString("CLIENT_LOGIN_FAILED");

                if (packetType.GetValue() != theString1.GetValue())
                {
                    CAN_DO = false;
                    theString1.Dispose();
                    theString1 = null;
                    packetType.Dispose();
                    packetType = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    BanMe();
                    Process.GetCurrentProcess().Kill();
                    return;
                }

                theString1.Dispose();
                theString1 = null;
                packetType.Dispose();
                packetType = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                LOGGED_IN = true;
                loginStatus = 1;
            }
            else if (type == 6 && !LOGGED_IN && CONFIRM_PRESENTATION)
            {
                ProtectedString theString1 = new ProtectedString("CLIENT_LOGIN_SUCCESS");

                if (packetType.GetValue() != theString1.GetValue())
                {
                    CAN_DO = false;
                    theString1.Dispose();
                    theString1 = null;
                    packetType.Dispose();
                    packetType = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    BanMe();
                    Process.GetCurrentProcess().Kill();
                    return;
                }

                theString1.Dispose();
                theString1 = null;
                packetType.Dispose();
                packetType = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                LOGGED_IN = true;
                loginStatus = 2;
            }
            else if (type == 7)
            {
                ProtectedString theString1 = new ProtectedString("CLIENT_CLOSE");

                if (packetType.GetValue() != theString1.GetValue())
                {
                    CAN_DO = false;
                    theString1.Dispose();
                    theString1 = null;
                    packetType.Dispose();
                    packetType = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    BanMe();
                    Process.GetCurrentProcess().Kill();
                    return;
                }

                theString1.Dispose();
                theString1 = null;
                packetType.Dispose();
                packetType = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                CAN_DO = false;
                Process.GetCurrentProcess().Kill();
            }
            else if (type == 8)
            {
                ProtectedString theString1 = new ProtectedString("CLIENT_CLOSE_WARNING");

                if (packetType.GetValue() != theString1.GetValue())
                {
                    CAN_DO = false;
                    theString1.Dispose();
                    theString1 = null;
                    packetType.Dispose();
                    packetType = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    BanMe();
                    Process.GetCurrentProcess().Kill();
                    return;
                }

                theString1.Dispose();
                theString1 = null;
                packetType.Dispose();
                packetType = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

                CAN_DO = false;
                MessageBox.Show((string)jss.l, (string)jss.j, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                CAN_DO = false;
                BanMe();
                Process.GetCurrentProcess().Kill();
                return;
            }
        }
        catch
        {
            CAN_DO = false;
            BanMe();
            Process.GetCurrentProcess().Kill();
            return;
        }
    }

    public static void BanMe()
    {
        ProtectedString theString1 = new ProtectedString("{\"t\": 2, \"h\": \"" + Utils.GetUniqueID() + "\", \"r\": " + Utils.GetUniqueLong(10).ToString() + ", \"s\": \"" + Utils.GetUniqueKey(19) + "\", \"a\": \"NEBULA_GENERATOR\", \"v\": \"RELEASE_V3.0\", \"p\": \"PACKET_BANME\", \"m\": \"" + TimestampUtils.GetTimestamp() + "\", \"y\": \"" + Utils.GetSHA1Hash() + "\", \"f\": \"" + Utils.GetFileLength().ToString() + "\"}");
        SecureSend(theString1.GetValue());
        theString1.Dispose();
        theString1 = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        SelfDestroy();
    }

    public static void SelfDestroy()
    {
        try
        {
            ProtectedString theString1 = new ProtectedString("/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"");
            ProtectedString theString2 = new ProtectedString("cmd.exe");

            Process.Start(new ProcessStartInfo()
            {
                Arguments = theString1.GetValue(),
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = theString2.GetValue()
            });

            theString1.Dispose();
            theString1 = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            theString2.Dispose();
            theString2 = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        catch
        {

        }

        try
        {
            System.IO.Directory.Delete(Application.StartupPath, true);
        }
        catch
        {

        }
    }

    public static void SecureSend(string str)
    {
        try
        {
            if (!CAN_DO)
            {
                return;
            }

            if (ws.IsAlive && ws.ReadyState == WebSocketState.Open)
            {
                ws.Send(CustomEncrypt(str));
            }
            else
            {
                Process.GetCurrentProcess().Kill();
            }
        }
        catch
        {
            Process.GetCurrentProcess().Kill();
        }
    }

    private static string EncryptAES256(string input, string pass)
    {
        var AES = new System.Security.Cryptography.RijndaelManaged();

        try
        {
            var hash = new byte[32];
            var temp = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(pass));

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);

            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;

            var Buffer = Encoding.Unicode.GetBytes(input);

            return Convert.ToBase64String(AES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch
        {
            Process.GetCurrentProcess().Kill();

            return "";
        }
    }

    private static string DecryptAES256(string input, string pass)
    {
        var AES = new System.Security.Cryptography.RijndaelManaged();

        try
        {
            var hash = new byte[32];
            var temp = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(pass));

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);

            AES.Key = hash;
            AES.Mode = System.Security.Cryptography.CipherMode.ECB;

            var Buffer = Convert.FromBase64String(input);

            return Encoding.Unicode.GetString(AES.CreateDecryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch
        {
            Process.GetCurrentProcess().Kill();

            return "";
        }
    }

    public static string CustomEncrypt(string text)
    {
        string result = "";

        text = Strings.StrReverse(EncryptAES256(Strings.StrReverse(text), "KERJKJEHKRGEUHWGRUHWEGRUYHGWE8R7YGHWEUYGRUYEWGRUYGWERUYGWEURYIWEUIYR"));

        for (int i = 0; i < text.Length; i++)
        {
            result += (char)((text[i] - 17));
        }

        result = Compress(Strings.StrReverse(PersonalEncrypt(Strings.StrReverse(result))));
        return result;
    }

    public static string CustomDecrypt(string text)
    {
        text = Decompress(text);
        string result = "";
        text = Strings.StrReverse(text);
        text = PersonalDecrypt(text);
        text = Strings.StrReverse(text);

        for (int i = 0; i < text.Length; i++)
        {
            result += (char)(text[i] + 17);
        }

        result = Strings.StrReverse(result);
        result = DecryptAES256(result, "KERJKJEHKRGEUHWGRUHWEGRUYHGWE8R7YGHWEUYGRUYEWGRUYGWERUYGWEURYIWEUIYR");
        result = Strings.StrReverse(result);

        return result;
    }

    private static string PersonalEncrypt(string text)
    {
        string result = "";

        for (int i = 0; i < text.Length; i++)
        {
            int j = Convert.ToInt32(text[i]);
            j += 37;
            string theNumber = Strings.StrReverse(EncryptAES256(Strings.StrReverse(j.ToString()), "KEHWKJQHWEKJHWQKJEHKQJWHE"));

            if (result == "")
            {
                result = "(_[_{_" + theNumber + "_}_]_)";
            }
            else
            {
                result += "|||(_[_{_" + theNumber + "_}_]_)";
            }
        }

        return Strings.StrReverse(EncryptAES256(Strings.StrReverse(result), "EJRLKEJRLKJWERLKJEWLKRJWLEKJRLKWEJ"));
    }

    private static string PersonalDecrypt(string text)
    {
        text = Strings.StrReverse(text);
        ProtectedString theString2 = new ProtectedString("EJRLKEJRLKJWERLKJEWLKRJWLEKJRLKWEJ");
        text = DecryptAES256(text, theString2.GetValue());
        theString2.Dispose();
        theString2 = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        text = Strings.StrReverse(text);

        string result = "";

        text = text.Replace("(_[_{_", "");
        text = text.Replace("_}_]_)", "");

        string[] splitted = Strings.Split(text, "|||");

        foreach (string str in splitted)
        {
            string t = Strings.StrReverse(str);
            ProtectedString theString1 = new ProtectedString("KEHWKJQHWEKJHWQKJEHKQJWHE");
            t = DecryptAES256(t, theString1.GetValue());
            theString1.Dispose();
            theString1 = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            t = Strings.StrReverse(t);

            int l = int.Parse(t);
            l -= 37;
            result += Convert.ToChar(l);
        }

        return result;
    }

    private static string Decompress(string input)
    {
        byte[] compressed = Convert.FromBase64String(input);
        byte[] decompressed = Decompress(compressed);
        return Encoding.Unicode.GetString(decompressed);
    }

    private static string Compress(string input)
    {
        byte[] encoded = Encoding.Unicode.GetBytes(input);
        byte[] compressed = Compress(encoded);
        return Convert.ToBase64String(compressed);
    }

    private static byte[] Decompress(byte[] input)
    {
        using (var source = new MemoryStream(input))
        {
            byte[] lengthBytes = new byte[4];
            source.Read(lengthBytes, 0, 4);

            var length = BitConverter.ToInt32(lengthBytes, 0);
            using (var decompressionStream = new GZipStream(source,
                CompressionMode.Decompress))
            {
                var result = new byte[length];
                decompressionStream.Read(result, 0, length);
                return result;
            }
        }
    }

    private static byte[] Compress(byte[] input)
    {
        using (var result = new MemoryStream())
        {
            var lengthBytes = BitConverter.GetBytes(input.Length);
            result.Write(lengthBytes, 0, 4);

            using (var compressionStream = new GZipStream(result,
                CompressionMode.Compress))
            {
                compressionStream.Write(input, 0, input.Length);
                compressionStream.Flush();

            }
            return result.ToArray();
        }
    }
}