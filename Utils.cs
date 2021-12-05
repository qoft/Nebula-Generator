﻿using System.Collections.Generic;
using Leaf.xNet;
using Microsoft.VisualBasic;
using System;
using System.Net.Sockets;
using System.Net;
using System.Net.Http;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Management;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;

public static class Utils
{
    public static List<string> users = new List<string>(), roles = new List<string>();
    public static bool globalAutoReconnect = false;
    public static string lastChannelId = "";
    public static ResourceSemaphore semaphore = new ResourceSemaphore();

    public static string GetSHA1Hash()
    {
        /*System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        System.IO.FileStream stream = new System.IO.FileStream(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

        md5.ComputeHash(stream);
        stream.Close();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < md5.Hash.Length; i++)
        {
            sb.Append(md5.Hash[i].ToString("x2"));
        }

        return sb.ToString().ToUpperInvariant();*/
        return "CF37E2872C6ABE0C443AEB6F912BA35F";
    }

    public static long GetFileLength()
    {
        return 913920L;
        //return new System.IO.FileStream(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read).Length;
    }

    public static List<string> GetDrives()
    {
        List<string> drives = new List<string>();

        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                    drives.Add(drive.Name.Substring(0, drive.Name.Length - 1));
                }
                catch
                {

                }
            }
        }
        catch
        {

        }

        return drives;
    }

    public static string GetUniqueID()
    {
        try
        {
            string id = "", theSerial = "", productID = "";

            try
            {
                ManagementObjectCollection mbsList = new ManagementObjectSearcher("Select ProcessorId From Win32_processor").Get();

                foreach (ManagementObject mo in mbsList)
                {
                    try
                    {
                        id = mo["ProcessorId"].ToString();
                        break;
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }

            try
            {
                var query = new SelectQuery("select * from Win32_Bios");
                var search = new ManagementObjectSearcher(query);

                foreach (ManagementBaseObject item in search.Get())
                {
                    try
                    {
                        string serial = item["SerialNumber"] as string;

                        if (serial != null)
                        {
                            theSerial = serial;
                            break;
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

            try
            {
                RegistryKey localMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey windowsNTKey = localMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion");
                productID = windowsNTKey.GetValue("ProductId").ToString();
            }
            catch
            {

            }

            string thing = EncryptAES256(id + theSerial + productID, "brMu348m").Replace("+", "X");

            if (thing == "")
            {
                Process.GetCurrentProcess().Kill();
                return "";
            }

            return thing;
        }
        catch
        {
            return "";
        }
    }

    private static string EncryptAES256(string input, string pass)
    {
        var AES = new RijndaelManaged();

        try
        {
            var hash = new byte[32];
            var temp = new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(pass));

            Array.Copy(temp, 0, hash, 0, 16);
            Array.Copy(temp, 0, hash, 15, 16);

            AES.Key = hash;
            AES.Mode = CipherMode.ECB;

            var Buffer = Encoding.Unicode.GetBytes(input);

            return Convert.ToBase64String(AES.CreateEncryptor().TransformFinalBlock(Buffer, 0, Buffer.Length));
        }
        catch
        {
            Process.GetCurrentProcess().Kill();

            return "";
        }
    }

    public static string smethod_9()
    {
        string right = Environment.SystemDirectory.Substring(0, 2) + "\\";
        ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_Volume");

        try
        {
            try
            {
                foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
                {
                    ManagementObject managementObject = (ManagementObject)managementBaseObject;

                    if (managementObject["Name"].ToString() == right)
                    {
                        return managementObject["DeviceID"].ToString().Substring(11, 36).Replace("-", "");
                    }
                }
            }
            catch
            {

            }
        }
        catch
        {

        }

        return "";
    }

    public static string smethod_0(string string_0, string string_1)
    {
        string_0 = string.Join("", Enumerable.Repeat<string>(string_0, 10)).Substring(0, string_1.Length);
        string string_2 = smethod_3(smethod_1(string_0.smethod_2()), smethod_1(string_1.smethod_2()));
        int num = new Random().Next(80, 90);
        string text = num.ToString("X");
        int num2 = 1;

        checked
        {
            do
            {
                text += (num + num2).ToString("X");
                num2++;
            }
            while (num2 <= 31);

            string string_3 = num.ToString("X") + smethod_3(smethod_1(string_2.smethod_2()), smethod_1(text)).smethod_2();
            return smethod_1_1(string_3);
        }
    }

    public static string smethod_2(this string string_0)
    {
        return BitConverter.ToString(Encoding.Default.GetBytes(string_0)).Replace("-", "");
    }

    public static string smethod_3(string string_0, string string_1)
    {
        StringBuilder stringBuilder = new StringBuilder();

        checked
        {
            int num = string_1.Length - 1;

            for (int i = 0; i <= num; i++)
            {
                stringBuilder.Append(Strings.ChrW((int)(string_1[i] ^ string_0[i % string_0.Length])));
            }

            return stringBuilder.ToString();
        }
    }

    private static string smethod_1(string string_0)
    {
        if (string_0.Length % 2 == 1)
        {
            string_0 += "0";
        }

        checked
        {
            byte[] array = new byte[(int)Math.Round(unchecked((double)string_0.Length / 2.0 - 1.0)) + 1];
            int num = string_0.Length - 1;

            for (int i = 0; i <= num; i += 2)
            {
                array[(int)Math.Round((double)i / 2.0)] = Convert.ToByte(string_0.Substring(i, 2), 16);
            }

            return Encoding.Default.GetString(array);
        }
    }

    public static string smethod_1_1(string string_0)
    {
        StringBuilder stringBuilder = new StringBuilder();

        checked
        {
            int num = string_0.Length - 1;

            for (int i = 0; i <= num; i += 2)
            {
                string value = string_0.Substring(i, 2);
                stringBuilder.Append(Convert.ToChar(Convert.ToUInt32(value, 16)).ToString());
            }

            return stringBuilder.ToString();
        }
    }

    internal static readonly char[] charsToCheck = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789#".ToCharArray();








    public static void EmitTheToken(string token, ns1.SiticoneCheckBox siticoneCheckBox5, Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox4, Guna.UI.WinForms.GunaTextBox gunaTextBox1, Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox9, Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox7, ns1.SiticoneCheckBox siticoneCheckBox2, ns1.SiticoneCheckBox siticoneCheckBox3)
    {
    cycle: while (!semaphore.IsResourceAvailable())
        {
            System.Threading.Thread.Sleep(1000);
        }

        if (!semaphore.LockResource())
        {
            goto cycle;
        }

        try
        {
            if (token == "")
            {
                GlobalVariables.generationFails++;
                return;
            }

            GlobalVariables.tokensGenerated++;

            try
            {
                if (siticoneCheckBox2.Checked)
                {
                    Utils.SendMessageToWebhook(gunaLineTextBox9.Text, "NebulaGenerator | Tokens", "", token);
                }
            }
            catch
            {

            }

            try
            {
                if (gunaTextBox1.Text == "")
                {
                    gunaTextBox1.Text = token;
                }
                else
                {
                    gunaTextBox1.Text += Environment.NewLine + token;
                }
            }
            catch
            {

            }

            try
            {
                if (siticoneCheckBox3.Checked)
                {
                    if (System.IO.File.Exists(gunaLineTextBox7.Text))
                    {
                        if (System.IO.File.ReadAllText(gunaLineTextBox7.Text).Replace(" ", "").Replace('\t'.ToString(), "").Trim() == "")
                        {
                            System.IO.File.WriteAllText(gunaLineTextBox7.Text, token);
                        }
                        else
                        {
                            System.IO.File.AppendAllText(gunaLineTextBox7.Text, Environment.NewLine + token);
                        }
                    }
                    else
                    {
                        System.IO.File.WriteAllText(gunaLineTextBox7.Text, token);
                    }
                }
            }
            catch
            {

            }
        }
        catch
        {
            GlobalVariables.generationFails++;
        }

        semaphore.UnlockResource();
    }

    public static bool IsFriendValid(string friend)
    {
        try
        {
            return Utils.IsIDValid(friend) || Utils.IsTagValid(friend);
        }
        catch
        {
            return false;
        }
    }

    public static string GetLagMessage()
    {
        return ":chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains: :flag_ac: :chains: :flag_ac: :chains: :flag_ad: :laughing: :brain: :chains: :chains:";
    }

    public static string ReplaceFirst(string text, string search, string replace)
    {
        try
        {
            int pos = text.IndexOf(search);

            if (pos < 0)
            {
                return text;
            }

            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
        catch
        {

        }

        return text;
    }

    public static bool AreIDsValid(string ids)
    {
        try
        {
            ids = ids.Replace(" ", "").Replace('\t'.ToString(), "").Trim();

            if (!ids.Contains(","))
            {
                if (!IsIDValid(ids))
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    string[] splitted = Microsoft.VisualBasic.Strings.Split(ids, ",");

                    try
                    {
                        foreach (string id in splitted)
                        {
                            try
                            {
                                if (!IsIDValid(id))
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

                    splitted = null;
                }
                catch
                {

                }
            }

            ids = null;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static List<string> GetIDs(string ids)
    {
        List<string> idList = new List<string>();

        try
        {
            ids = ids.Replace(" ", "").Replace('\t'.ToString(), "").Trim();

            if (ids.Contains(","))
            {
                string[] splitted = Microsoft.VisualBasic.Strings.Split(ids, ",");

                foreach (string id in splitted)
                {
                    idList.Add(id);
                }

                splitted = null;
            }
            else
            {
                idList.Add(ids);
            }

            ids = null;
        }
        catch
        {

        }

        return idList;
    }

    public static string GetXCP(DiscordInvite invite)
    {
        try
        {
            return GetXCP(invite.guildId.ToString(), invite.channelId.ToString(), invite.channelType.ToString());
        }
        catch
        {
            return "";
        }
    }

    public static string GetXCP(string guildId, string channelId, string channelType)
    {
        try
        {
            return Base64Encode("{\"location\":\"Join Guild\",\"location_guild_id\":\"" + guildId + "\",\"location_channel_id\":\"" + channelId + "\",\"location_channel_type\":" + channelType + "}");
        }
        catch
        {
            return "eyJsb2NhdGlvbiI6IkpvaW4gR3VpbGQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6IjgyMjU4NDA5NTg5MTY1MjYyOSIsImxvY2F0aW9uX2NoYW5uZWxfaWQiOiI4MjI1ODQwOTYzNzA3MjA3NjgiLCJsb2NhdGlvbl9jaGFubmVsX3R5cGUiOjB9";
        }
    }

    public static string Base64Encode(string plainText)
    {
        try
        {
            return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
        }
        catch
        {
            return "";
        }
    }

    public static DateTime GetCurrentRealDateTime()
    {
        try
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds + 7200L);
        }
        catch
        {
            return DateTime.Now;
        }
    }

    public static DiscordInvite GetInviteInformations(string invite, bool groupMode)
    {
        try
        {
            string inviteCode = GetInviteCodeByInviteLink(invite), response = "";
            HttpRequest request = CreateCleanRequest();
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");

            if (groupMode)
            {
                response = DecompressResponse(request.Get("https://discord.com/api/v9/invites/" + inviteCode + "?with_counts=true&with_expiration=true"));
            }
            else
            {
                response = DecompressResponse(request.Get("https://discord.com/api/v9/invites/" + inviteCode + "?inputValue=https://discord.gg/" + inviteCode + "&with_counts=true&with_expiration=true"));
            }

            dynamic jss = JObject.Parse(response);
            string guildId = "0", channelId = "0", channelType = "0", statusCode = (string)jss.code, membersCount = "";
            bool status = true, isGroup = groupMode;

            if (statusCode == "10006" || statusCode == "0" || statusCode != inviteCode)
            {
                status = false;
            }

            if (status)
            {
                if (!groupMode)
                {
                    guildId = (string)jss.guild.id;
                }

                channelId = (string)jss.channel.id;
                channelType = (string)jss.channel.type;
                membersCount = (string)jss.approximate_member_count;
            }

            return new DiscordInvite(inviteCode, status, isGroup, ulong.Parse(guildId), ulong.Parse(channelId), ulong.Parse(membersCount), int.Parse(channelType));
        }
        catch
        {
            return new DiscordInvite(GetInviteCodeByInviteLink(invite), false, false, 0, 0, 0, 0);
        }
    }

    public static string GetTest()
    {
        try
        {
            string dayOfWeek = "", month = "", day = "", year = "", hour = "", minute = "", second = "";

            DateTime nowTime = GetCurrentRealDateTime();
            day = nowTime.Day.ToString();

            if (day.Length == 1)
            {
                day = "0" + day;
            }

            year = nowTime.Year.ToString();
            hour = nowTime.Hour.ToString();
            minute = nowTime.Minute.ToString();
            second = nowTime.Second.ToString();

            if (hour.Length == 1)
            {
                hour = "0" + hour;
            }

            if (minute.Length == 1)
            {
                minute = "0" + minute;
            }

            if (second.Length == 1)
            {
                second = "0" + second;
            }

            if (nowTime.Month == 1)
            {
                month = "Jan";
            }
            else if (nowTime.Month == 2)
            {
                month = "Feb";
            }
            else if (nowTime.Month == 3)
            {
                month = "Mar";
            }
            else if (nowTime.Month == 4)
            {
                month = "Apr";
            }
            else if (nowTime.Month == 5)
            {
                month = "May";
            }
            else if (nowTime.Month == 6)
            {
                month = "Jun";
            }
            else if (nowTime.Month == 7)
            {
                month = "Jul";
            }
            else if (nowTime.Month == 8)
            {
                month = "Aug";
            }
            else if (nowTime.Month == 9)
            {
                month = "Sep";
            }
            else if (nowTime.Month == 10)
            {
                month = "Oct";
            }
            else if (nowTime.Month == 11)
            {
                month = "Nov";
            }
            else if (nowTime.Month == 12)
            {
                month = "Dec";
            }

            if (nowTime.DayOfWeek == DayOfWeek.Monday)
            {
                dayOfWeek = "Mon";
            }
            else if (nowTime.DayOfWeek == DayOfWeek.Tuesday)
            {
                dayOfWeek = "Tue";
            }
            else if (nowTime.DayOfWeek == DayOfWeek.Wednesday)
            {
                dayOfWeek = "Wed";
            }
            else if (nowTime.DayOfWeek == DayOfWeek.Thursday)
            {
                dayOfWeek = "Thu";
            }
            else if (nowTime.DayOfWeek == DayOfWeek.Friday)
            {
                dayOfWeek = "Fri";
            }
            else if (nowTime.DayOfWeek == DayOfWeek.Saturday)
            {
                dayOfWeek = "Sat";
            }
            else if (nowTime.DayOfWeek == DayOfWeek.Sunday)
            {
                dayOfWeek = "Sun";
            }

            return "isIABGlobal=false&datestamp=" + dayOfWeek + "+" + month + "+" + day + "+" + year + "+" + hour + ":" + minute + ":" + second + "+GMT+0200+(Ora+legale+dellâEuropa+centrale)&version=6.17.0&hosts=&landingPath=NotLandingPage&groups=C0001:1,C0002:1,C0003:1&geolocation=IT;62&AwaitingReconsent=false";
        }
        catch
        {
            return "";
        }
    }

    public static string GetRandomCookie(string token, string language)
    {
        try
        {
            return "__cfduid=" + GetUniqueKey1(43) + "; __dcfduid=" + GetUniqueKey1(32) + "; rebrand_bucket=" + GetUniqueKey1(32) + "; OptanonAlertBoxClosed=2021-05-30T14:59:00.092Z; locale=" + language + "; token=\"" + token + "\"";
        }
        catch
        {
            return "";
        }
    }

    public static string GetInviteCodeByInviteLink(string inviteLink)
    {
        try
        {
            if (inviteLink.EndsWith("/"))
            {
                inviteLink = inviteLink.Substring(0, inviteLink.Length - 1);
            }

            if (inviteLink.Contains("discord") && inviteLink.Contains("/") && inviteLink.Contains("http"))
            {
                string[] splitter = Microsoft.VisualBasic.Strings.Split(inviteLink, "/");

                return splitter[splitter.Length - 1];
            }
        }
        catch
        {

        }

        return inviteLink;
    }

    public static IEnumerable<string> SplitToLines(string input)
    {
        if (input == null)
        {
            yield break;
        }

        using (System.IO.StringReader reader = new System.IO.StringReader(input))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }

    public static bool IsTokenValid(string token)
    {
        try
        {
            HttpRequest request = CreateCleanRequest();

            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Authorization", token);

            try
            {
                request.Get("https://discord.com/api/v9/users/@me/library");

                if (request.Response.IsOK)
                {
                    return true;
                }
            }
            catch
            {

            }
        }
        catch
        {

        }

        return false;
    }

    public static string GetGroupXCP(DiscordInvite invite)
    {
        try
        {
            return Base64Encode("{\"location\":\"Invite Button Embed\",\"location_guild_id\":null,\"location_channel_id\":\"" + invite.channelId + "\",\"location_channel_type\":" + invite.channelType + ",\"location_message_id\":null}");
        }
        catch
        {
            return "eyJsb2NhdGlvbiI6Ikludml0ZSBCdXR0b24gRW1iZWQiLCJsb2NhdGlvbl9ndWlsZF9pZCI6bnVsbCwibG9jYXRpb25fY2hhbm5lbF9pZCI6IjgzNzM5NzUzMDAzODg5NDY0MiIsImxvY2F0aW9uX2NoYW5uZWxfdHlwZSI6MSwibG9jYXRpb25fbWVzc2FnZV9pZCI6IjgzNzU5MjQyMDAxNDA5NjM4NCJ9";
        }
    }

    public static string GetCleanToken(string token)
    {
        try
        {
            return token.Replace(" ", "").Trim().Replace('\t'.ToString(), "");
        }
        catch
        {
            return token;
        }
    }

    public static bool IsTokenFormatValid(string token)
    {
        try
        {
            string tok = GetCleanToken(token);

            if (tok == null || tok == "")
            {
                return false;
            }

            if (tok.Length != 88 && tok.Length != 59)
            {
                return false;
            }

            if (tok.Length == 88)
            {
                if (!tok.StartsWith("mfa."))
                {
                    return false;
                }
            }
            else
            {
                char[] chars = tok.ToCharArray();
                int dots = 0;

                foreach (char c in chars)
                {
                    if (c == '.')
                    {
                        dots++;
                    }
                }

                if (dots != 2)
                {
                    return false;
                }

                string[] splitted = Strings.Split(tok, ".");

                if (splitted[0].Length != 24)
                {
                    return false;
                }

                if (splitted[1].Length != 6)
                {
                    return false;
                }

                if (splitted[2].Length != 27)
                {
                    return false;
                }

                string decodedFirstPart = Base64Decode(splitted[0]);

                if (!IsIDValid(decodedFirstPart))
                {
                    return false;
                }

                string inBinary = DecimalToBinary(decodedFirstPart);

                if (inBinary.Length != 60)
                {
                    return false;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsProxyFormatValid(string proxy)
    {
        try
        {
            string theProxy = GetCleanToken(proxy);

            if (theProxy == "" || theProxy == null)
            {
                return false;
            }

            char[] chars = theProxy.ToCharArray();
            int colons = 0;

            foreach (char c in chars)
            {
                if (c == ':')
                {
                    colons++;
                }
            }

            if (colons != 1 && colons != 3)
            {
                return false;
            }

            string[] splitted = Strings.Split(theProxy, ":");

            string ip = splitted[0], port = splitted[1];

            if (ip.Length > 15)
            {
                return false;
            }

            if (port.Length > 5)
            {
                return false;
            }

            if (!Microsoft.VisualBasic.Information.IsNumeric(port))
            {
                return false;
            }

            int thePort = int.Parse(port);

            if (!(thePort >= 0 && thePort <= 65535))
            {
                return false;
            }

            char[] theChars = ip.ToCharArray();
            int dots = 0;

            foreach (char c in theChars)
            {
                if (c == '.')
                {
                    dots++;
                }
            }

            if (dots != 3)
            {
                return false;
            }

            string[] octets = Strings.Split(ip, ".");

            foreach (string octect in octets)
            {
                if (octect.Length != 1 && octect.Length != 2 && octect.Length != 3)
                {
                    return false;
                }

                if (!Microsoft.VisualBasic.Information.IsNumeric(octect))
                {
                    return false;
                }

                int theOctect = int.Parse(octect);

                if (!(theOctect >= 0 && theOctect <= 255))
                {
                    return false;
                }
            }

            if (colons == 3)
            {
                if (GetCleanToken(splitted[2]) == "" || GetCleanToken(splitted[3]) == "")
                {
                    return false;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsIDValid(string id)
    {
        try
        {
            if (id.Length != 18)
            {
                return false;
            }

            if (!Microsoft.VisualBasic.Information.IsNumeric(id))
            {
                return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string DecimalToBinary(string data)
    {
        try
        {
            return Convert.ToString(long.Parse(data), 2);
        }
        catch
        {
            return "";
        }
    }

    public static string Base64Decode(string base64EncodedData)
    {
        try
        {
            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
        }
        catch
        {
            return "";
        }
    }

    public static HttpRequest CreateCleanRequest()
    {
        try
        {
            HttpRequest request = new HttpRequest();

            request.KeepTemporaryHeadersOnRedirect = false;
            request.EnableMiddleHeaders = false;
            request.ClearAllHeaders();
            request.AllowEmptyHeaderValues = false;
            request.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            request.Proxy = null;
            request.Username = null;
            request.UserAgent = null;
            request.UseCookies = false;
            request.CookieSingleHeader = true;
            request.Authorization = null;
            request.BaseAddress = null;
            request.Referer = null;
            request.Reconnect = false;
            request.ReconnectDelay = 0;
            request.Password = null;
            request.KeepAlive = false;
            request.IgnoreInvalidCookie = true;
            request.IgnoreProtocolErrors = true;
            request.KeepTemporaryHeadersOnRedirect = false;
            request.MaximumKeepAliveRequests = 1;
            request.Cookies = null;
            request.CharacterSet = null;
            request.AcceptEncoding = null;
            request.Culture = null;
            request.AllowAutoRedirect = false;
            request.MaximumAutomaticRedirections = 1;

            return request;
        }
        catch
        {
            return new HttpRequest();
        }
    }

    public static HttpProxyClient ParseProxy(string proxy)
    {
        try
        {
            char[] chars = proxy.ToCharArray();
            int colons = 0;

            foreach (char c in chars)
            {
                if (c == ':')
                {
                    colons++;
                }
            }

            string[] splitted = Strings.Split(proxy, ":");

            if (colons == 1)
            {
                return new HttpProxyClient(splitted[0], int.Parse(splitted[1]));
            }
            else
            {
                return new HttpProxyClient(splitted[0], int.Parse(splitted[1]), splitted[2], splitted[3]);
            }
        }
        catch
        {
            return null;
        }
    }

    public static string DecompressResponse(Leaf.xNet.HttpResponse response)
    {
        if (response.ContainsHeader("content-encoding"))
        {
            byte[] resp = response.ToBytes();
            return System.Text.Encoding.UTF8.GetString(BrotliSharpLib.Brotli.DecompressBuffer(resp, 0, resp.Length));
        }

        return response.ToString();
    }

    internal static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    internal static readonly char[] numbers = "123456789".ToCharArray();
    internal static readonly char[] everything = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

    public static string GetUniqueKey(int size)
    {
        try
        {
            byte[] data = new byte[4 * size];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(size);

            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }
        catch
        {
            return "";
        }
    }

    public static string GetUniqueKey1(int size)
    {
        try
        {
            byte[] data = new byte[4 * size];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(size);

            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % everything.Length;

                result.Append(everything[idx]);
            }

            return result.ToString();
        }
        catch
        {
            return "";
        }
    }

    public static int GetUniqueInt(int size)
    {
        try
        {
            byte[] data = new byte[4 * size];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(size);

            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % numbers.Length;

                result.Append(numbers[idx]);
            }

            return int.Parse(result.ToString());
        }
        catch
        {
            return 0;
        }
    }

    public static long GetUniqueLong(int size)
    {
        try
        {
            byte[] data = new byte[4 * size];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(size);

            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % numbers.Length;

                result.Append(numbers[idx]);
            }

            return long.Parse(result.ToString());
        }
        catch
        {
            return 0L;
        }
    }

    public static bool IsCaptchaKeyValid(string captchaKey)
    {
        try
        {
            return captchaKey.Length == 32;
        }
        catch
        {
            return false;
        }
    }

    public static void SendMessageToWebhook(string url, string username, string avatar_url, string content)
    {
        try
        {
            HttpRequest request = CreateCleanRequest();
            string data = "username=" + username + "&avatar_url=" + avatar_url + "&content=" + content;
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            request.AddHeader("Content-Length", data.Length.ToString());
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.Post(url, data, "application/x-www-form-urlencoded");
        }
        catch
        {

        }
    }

    public static byte[] Post(string uri, NameValueCollection pairs)
    {
        try
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.UploadValues(uri, pairs);
            }
        }
        catch
        {
            return new byte[] { };
        }
    }

    public static bool AreFriendsValid(string ids)
    {
        ids = ids.Replace(" ", "").Replace('\t'.ToString(), "").Trim();

        try
        {
            if (!ids.Contains(","))
            {
                if (!IsIDValid(ids) && !IsTagValid(ids))
                {
                    return false;
                }
            }
            else
            {
                string[] splitted = Microsoft.VisualBasic.Strings.Split(ids, ",");

                foreach (string id in splitted)
                {
                    if (!IsIDValid(id) && !IsTagValid(id))
                    {
                        return false;
                    }
                }

                splitted = null;
            }

            ids = null;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static List<string> GetFriends(string ids)
    {
        List<string> idList = new List<string>();

        try
        {
            ids = ids.Replace(" ", "").Replace('\t'.ToString(), "").Trim();

            if (ids.Contains(","))
            {
                string[] splitted = Microsoft.VisualBasic.Strings.Split(ids, ",");

                foreach (string id in splitted)
                {
                    idList.Add(id);
                }

                splitted = null;
            }
            else
            {
                idList.Add(ids);
            }

            ids = null;
        }
        catch
        {

        }

        return idList;
    }

    public static bool IsTagValid(string tag)
    {
        try
        {
            if (tag.Length > 37)
            {
                return false;
            }

            if (!tag.Contains("#"))
            {
                return false;
            }

            string[] splitted = Microsoft.VisualBasic.Strings.Split(tag, "#");

            if (splitted[0].Replace(" ", "").Trim().Replace('\t'.ToString(), "") == "" || splitted[1].Replace(" ", "").Trim().Replace('\t'.ToString(), "") == "")
            {
                return false;
            }

            if (splitted[1].Replace(" ", "").Trim().Replace('\t'.ToString(), "").Length != 4)
            {
                return false;
            }

            if (!Microsoft.VisualBasic.Information.IsNumeric(splitted[1].Replace(" ", "").Trim().Replace('\t'.ToString(), "")))
            {
                return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsWebhookValid(string webhook)
    {
        try
        {
            if (!webhook.StartsWith("https://discord.com/api/webhooks/"))
            {
                return false;
            }

            if (webhook.Length != 120)
            {
                return false;
            }

            string toCheck = webhook.Replace("https://discord.com/api/webhooks/", "");
            string[] splitted = Strings.Split(toCheck, "/");

            if (!IsIDValid(splitted[0]))
            {
                return false;
            }

            if (splitted[1].Length != 68)
            {
                return false;
            }

            HttpRequest request = Utils.CreateCleanRequest();
            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
            return Utils.DecompressResponse(request.Get(webhook)).Contains("id");
        }
        catch
        {
            return false;
        }
    }

    public static bool IsEmojiValid(string emoji)
    {
        if (emoji.Length > 3 || emoji.Replace(" ", "").Trim().Replace('\t'.ToString(), "").Length > 3)
        {
            return false;
        }

        return true;
    }

    public static bool IsEmoteValid(string emote)
    {
        if (!emote.Contains(":") && !emote.Contains("%3A"))
        {
            return false;
        }
        else
        {
            string[] splitter = null;

            if (emote.Contains(":"))
            {
                splitter = Microsoft.VisualBasic.Strings.Split(emote, ":");
            }
            else if (emote.Contains("%3A"))
            {
                splitter = Microsoft.VisualBasic.Strings.Split(emote, "%3A");
            }
            else
            {
                return false;
            }

            if (!IsIDValid(splitter[1]))
            {
                return false;
            }

            if (splitter[0].Replace(" ", "").Replace('\t'.ToString(), "") == "")
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsReactionValid(string reaction)
    {
        return IsEmoteValid(reaction) || IsEmojiValid(reaction);
    }
}