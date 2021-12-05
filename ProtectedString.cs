using System;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

public partial class ProtectedString
{
    private string protectedValue = "";

    private string encryptionKey = "";

    private string fakeValue1 = "";
    private string fakeValue2 = "";
    private string fakeValue3 = "";
    private string fakeValue4 = "";
    private string fakeValue5 = "";
    private string fakeValue6 = "";

    public ProtectedString(string valueToProtect)
    {
        encryptionKey = Utils.GetUniqueKey(5);

        protectedValue = ProtectString(valueToProtect);

        valueToProtect = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        fakeValue1 = protectedValue;
        fakeValue2 = protectedValue;
        fakeValue3 = protectedValue;
        fakeValue4 = protectedValue;
        fakeValue5 = protectedValue;
        fakeValue6 = protectedValue;
    }

    private string ProtectString(string valueToProtect)
    {
        return Reverse(EncryptAES256(Reverse(valueToProtect), encryptionKey));
    }

    private string UnprotectString(string valueToUnprotect)
    {
        return Reverse(DecryptAES256(Reverse(valueToUnprotect), encryptionKey));
    }

    private string Reverse(string value)
    {
        var arr = value.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    public string GetValue()
    {
        if (IsViolated())
        {
            Dispose();
            Process.GetCurrentProcess().Kill();
            return "";
        }

        return UnprotectString(protectedValue);
    }

    public void SetValue(string valueToProtect)
    {
        protectedValue = ProtectString(valueToProtect);
    }

    public void Dispose()
    {
        protectedValue = null;
        encryptionKey = null;

        fakeValue1 = null;
        fakeValue2 = null;
        fakeValue3 = null;

        fakeValue4 = null;
        fakeValue5 = null;
        fakeValue6 = null;

        GC.Collect();
        GC.WaitForPendingFinalizers();
    }

    private string EncryptAES256(string input, string pass)
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

    private string DecryptAES256(string input, string pass)
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

    public bool IsViolated()
    {
        if (fakeValue1 != protectedValue || fakeValue2 != protectedValue || fakeValue3 != protectedValue || fakeValue4 != protectedValue || fakeValue5 != protectedValue || fakeValue6 != protectedValue)
        {
            Dispose();
            Process.GetCurrentProcess().Kill();
            return true;
        }

        return false;
    }
}