using System.Net;
using System.Net.Http;
using System;
using System.Threading;
using System.Security.Cryptography;
using System.Text;
using BrotliSharpLib;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;
using CapMonsterCloud;
using CapMonsterCloud.Models.CaptchaTasks;
using CapMonsterCloud.Models.CaptchaTasksResults;
using WebSocketSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Reflection;

public class TokenGenerator
{
    internal static readonly char[] hypesquad = "123".ToCharArray();
    internal static readonly char[] everything = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
    internal static readonly char[] characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    internal static readonly char[] passwordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789#?=)(/&%$£€{}òçù§@".ToCharArray();

    public string dcfduid = "";

    public string fingerprint = "", token = "", verifyToken = "", otherToken = "", emailCookie = "", fbp = "";
    public bool captchaForEmail = false, capMonster = true, phoneVerification = false, emailVerification = true, customAvatar = true, customHypeSquad = true, customAboutMe = true;
    public string phoneOrderId = "", phoneNumber = "";
    public bool useProxies = true;
    public string captchaKey = "", phoneKey = "", email = "", cookieStr = "", proxyToUse = "";
    public bool invalidPhone = false;

    internal static readonly char[] numbers = "123456789".ToCharArray();

    public string Generate(string username, string avatar, string proxy, string password, string hypesquad, string aboutMe, string invite, ns1.SiticoneCheckBox siticoneCheckBox5, Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox4, Guna.UI.WinForms.GunaTextBox gunaTextBox1, Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox9, Guna.UI.WinForms.GunaLineTextBox gunaLineTextBox7, ns1.SiticoneCheckBox siticoneCheckBox2, ns1.SiticoneCheckBox siticoneCheckBox3)
    {
        try
        {
            phoneVerification = false;

            proxyToUse = proxy;
            GlobalVariables.operationsExecuted++;
            FindEmail();
            GlobalVariables.operationsExecuted++;
            fbp = "fb.1." + GetUniqueLong(13).ToString() + "." + GetUniqueLong(10).ToString();
            GlobalVariables.operationsExecuted++;
            GetCookie();
            GlobalVariables.operationsExecuted++;
            GetFingerprint();
            GlobalVariables.operationsExecuted++;
            GenerateToken1(username, password, invite);
            GlobalVariables.operationsExecuted++;

            if (token != "")
            {
                GlobalVariables.operationsExecuted++;
                ConnectToWS();
                GlobalVariables.operationsExecuted++;

                if (emailVerification)
                {
                    GlobalVariables.operationsExecuted++;
                    EmailVerification2();
                    GlobalVariables.operationsExecuted++;

                    if (customAboutMe)
                    {
                        string data = "{\"bio\":\"" + aboutMe + "\"}";
                        var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me");

                        request.UseDefaultCredentials = false;
                        request.AllowAutoRedirect = false;
                        request.Proxy = null;

                        var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                        request.Method = "PATCH";

                        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                        requestStream.Close();

                        var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                        {
                            ["Host"] = "discord.com",
                            ["Connection"] = "keep-alive",
                            ["Content-Length"] = requestBytes.Length.ToString(),
                            ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                            ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzkzLjAuNDU3Ny42MyBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTMuMC40NTc3LjYzIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk2OTY3LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                            ["Accept-Language"] = "it",
                            ["sec-ch-ua-mobile"] = "?0",
                            ["Authorization"] = token,
                            ["Content-Type"] = "application/json",
                            ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                            ["sec-ch-ua-platform"] = "\"Windows\"",
                            ["Accept"] = "*/*",
                            ["Origin"] = "https://discord.com",
                            ["Sec-Fetch-Site"] = "same-origin",
                            ["Sec-Fetch-Mode"] = "cors",
                            ["Sec-Fetch-Dest"] = "empty",
                            ["Referer"] = "https://discord.com/channels/@me",
                            ["Accept-Encoding"] = "gzip, deflate, br",
                            ["Cookie"] = cookieStr + "; OptanonConsent=" + GetOptaNonConsent()
                        });

                        field.SetValue(request, headers);
                        GlobalVariables.operationsExecuted++;
                        var response = request.GetResponse();
                        GlobalVariables.operationsExecuted++;
                        response.Close();
                        response.Dispose();
                    }

                    if (customHypeSquad)
                    {
                        string data = "{\"house_id\":" + hypesquad + "}";
                        var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/hypesquad/online");

                        request.UseDefaultCredentials = false;
                        request.AllowAutoRedirect = false;
                        request.Proxy = null;

                        var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                        request.Method = "POST";

                        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                        requestStream.Close();

                        var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                        {
                            ["Host"] = "discord.com",
                            ["Authorization"] = token,
                            ["Accept-Encoding"] = "gzip, deflate, br",
                            ["Content-Length"] = requestBytes.Length.ToString(),
                            ["Content-Type"] = "application/json"
                        });

                        field.SetValue(request, headers);
                        GlobalVariables.operationsExecuted++;
                        var response = request.GetResponse();
                        GlobalVariables.operationsExecuted++;
                        response.Close();
                        response.Dispose();
                    }

                    if (customAvatar)
                    {
                        var ms = new System.IO.MemoryStream();
                        System.Drawing.Image image = System.Drawing.Image.FromFile(avatar);
                        image.Save(ms, image.RawFormat);

                        // string content = "{\"avatar\":\"data:image/png;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(avatar)) + "\"}";
                        string content = "{\"avatar\":\"data:image/png;base64," + Convert.ToBase64String(ms.ToArray()) + "\"}";
                        ms.Dispose();
                        ms = null;
                        image.Dispose();
                        image = null;
                        var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me");

                        request.UseDefaultCredentials = false;
                        request.AllowAutoRedirect = false;
                        request.Proxy = null;

                        var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                        request.Method = "PATCH";

                        byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(content);
                        Stream requestStream = request.GetRequestStream();
                        requestStream.Write(requestBytes, 0, requestBytes.Length);
                        requestStream.Close();

                        var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                        {
                            ["Host"] = "discord.com",
                            ["Connection"] = "keep-alive",
                            ["Content-Length"] = requestBytes.Length.ToString(),
                            ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                            ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzkzLjAuNDU3Ny42MyBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTMuMC40NTc3LjYzIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk2OTY3LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                            ["X-Fingerprint"] = fingerprint,
                            ["X-Debug-Options"] = "bugReporterEnabled",
                            ["Accept-Language"] = "it",
                            ["sec-ch-ua-mobile"] = "?0",
                            ["Authorization"] = token,
                            ["Content-Type"] = "application/json",
                            ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                            ["sec-ch-ua-platform"] = "\"Windows\"",
                            ["Accept"] = "*/*",
                            ["Origin"] = "https://discord.com",
                            ["Sec-Fetch-Site"] = "same-origin",
                            ["Sec-Fetch-Mode"] = "cors",
                            ["Sec-Fetch-Dest"] = "empty",
                            ["Referer"] = "https://discord.com/channels/@me",
                            ["Accept-Encoding"] = "gzip, deflate, br",
                            ["Cookie"] = cookieStr + "; OptanonConsent=" + GetOptaNonConsent()
                        });

                        field.SetValue(request, headers);
                        GlobalVariables.operationsExecuted++;
                        var response = request.GetResponse();
                        GlobalVariables.operationsExecuted++;
                        response.Close();
                        response.Dispose();
                    }

                    GlobalVariables.operationsExecuted++;

                    if (phoneVerification)
                    {
                        GlobalVariables.operationsExecuted++;
                        PhoneVerification(password);
                        GlobalVariables.operationsExecuted++;
                    }
                }

                GlobalVariables.operationsExecuted++;

                Thread thread = new Thread(() => Utils.EmitTheToken(token, siticoneCheckBox5, gunaLineTextBox4, gunaTextBox1, gunaLineTextBox9, gunaLineTextBox7, siticoneCheckBox2, siticoneCheckBox3));
                thread.Priority = ThreadPriority.Highest;
                thread.Start();

                GlobalVariables.operationsExecuted++;

                return token;
            }
            else
            {
                GlobalVariables.generationFails++;
                return "";
            }
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            System.Windows.Forms.MessageBox.Show(ex.Data.ToString());
            System.Windows.Forms.MessageBox.Show(ex.StackTrace.ToString());
            System.Windows.Forms.MessageBox.Show(ex.Source.ToString());
            GlobalVariables.generationFails++;
            return "";
        }
    }

    public void PhoneVerification(string password)
    {
        {
            var request = (HttpWebRequest)WebRequest.Create("https://5sim.net/v1/user/buy/activation/russia/any/discord");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "GET";

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "5sim.net",
                ["Authorization"] = "Bearer " + phoneKey,
                ["Accept"] = "application/json"
            });

            field.SetValue(request, headers);

            var response = request.GetResponse();

            GlobalVariables.operationsExecuted++;
            dynamic jss = JObject.Parse(DecompressResponse(ReadFully(response.GetResponseStream())));
            GlobalVariables.operationsExecuted++;

            response.Close();
            response.Dispose();

            phoneNumber = (string)jss.phone;
            phoneOrderId = (string)jss.id;
        }

        {
            string data = "{\"phone\":\"" + phoneNumber + "\"}";
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me/phone");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "POST";

            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["Content-Length"] = requestBytes.Length.ToString(),
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzkzLjAuNDU3Ny42MyBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTMuMC40NTc3LjYzIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk2OTY3LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                ["X-Fingerprint"] = fingerprint,
                ["X-Debug-Options"] = "bugReporterEnabled",
                ["Accept-Language"] = "it",
                ["sec-ch-ua-mobile"] = "?0",
                ["Authorization"] = token,
                ["Content-Type"] = "application/json",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept"] = "*/*",
                ["Origin"] = "https://discord.com",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "cors",
                ["Sec-Fetch-Dest"] = "empty",
                ["Referer"] = "https://discord.com/channels/@me",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Cookie"] = cookieStr + "; OptanonConsent=" + GetOptaNonConsent()
            });

            field.SetValue(request, headers);
            var response = request.GetResponse();
            response.Close();
            response.Dispose();
        }

        string verificationCode = "";

        {
            while (verificationCode == "")
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create("https://5sim.net/v1/user/check/" + phoneOrderId);

                    request.UseDefaultCredentials = false;
                    request.AllowAutoRedirect = false;
                    request.Proxy = null;

                    var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                    request.Method = "GET";

                    var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                    {
                        ["Host"] = "5sim.net",
                        ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:92.0) Gecko/20100101 Firefox/92.0",
                        ["Accept"] = "application/json, text/plain, */*",
                        ["Accept-Language"] = "it-IT,it;q=0.8,en-US;q=0.5,en;q=0.3",
                        ["Accept-Encoding"] = "gzip, deflate, br",
                        ["Connection"] = "keep-alive",
                        ["Referer"] = "https://5sim.net/v1/user/check/" + phoneOrderId,
                        ["Cookie"] = "token=" + phoneKey,
                        ["Sec-Fetch-Dest"] = "empty",
                        ["Sec-Fetch-Mode"] = "cors",
                        ["Sec-Fetch-Site"] = "same-origin",
                    });

                    field.SetValue(request, headers);

                    var response = request.GetResponse();
                    string str = DecompressResponse(ReadFully(response.GetResponseStream()));

                    response.Close();
                    response.Dispose();

                    if (str.Contains("\"code\":\""))
                    {
                        string[] splitted = Strings.Split(str, "\"code\":\"");
                        splitted = Strings.Split(splitted[1], "\"");
                        verificationCode = splitted[0];
                    }
                }
                catch
                {

                }
            }
        }

        string verificationToken = "";

        {
            string data = "{\"phone\":\"" + phoneNumber + "\",\"code\":\"" + verificationCode + "\"}";
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/phone-verifications/verify");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "POST";

            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["Content-Length"] = requestBytes.Length.ToString(),
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzkzLjAuNDU3Ny42MyBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTMuMC40NTc3LjYzIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk2OTY3LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                ["X-Fingerprint"] = fingerprint,
                ["X-Debug-Options"] = "bugReporterEnabled",
                ["Accept-Language"] = "it",
                ["sec-ch-ua-mobile"] = "?0",
                ["Authorization"] = token,
                ["Content-Type"] = "application/json",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept"] = "*/*",
                ["Origin"] = "https://discord.com",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "cors",
                ["Sec-Fetch-Dest"] = "empty",
                ["Referer"] = "https://discord.com/channels/@me",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Cookie"] = cookieStr + "; OptanonConsent=" + GetOptaNonConsent()
            });

            field.SetValue(request, headers);
            var response = request.GetResponse();

            dynamic jss = JObject.Parse(DecompressResponse(ReadFully(response.GetResponseStream())));
            verificationToken = (string)jss.token;

            response.Close();
            response.Dispose();
        }

        {
            string data = "{\"phone_token\":\"" + verificationToken + "\",\"password\":\"" + password + "\"}";
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/users/@me/phone");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "POST";

            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["Content-Length"] = requestBytes.Length.ToString(),
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzkzLjAuNDU3Ny42MyBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTMuMC40NTc3LjYzIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk2OTY3LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                ["X-Fingerprint"] = fingerprint,
                ["X-Debug-Options"] = "bugReporterEnabled",
                ["Accept-Language"] = "it",
                ["sec-ch-ua-mobile"] = "?0",
                ["Authorization"] = token,
                ["Content-Type"] = "application/json",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept"] = "*/*",
                ["Origin"] = "https://discord.com",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "cors",
                ["Sec-Fetch-Dest"] = "empty",
                ["Referer"] = "https://discord.com/channels/@me",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Cookie"] = cookieStr + "; OptanonConsent=" + GetOptaNonConsent()
            });

            field.SetValue(request, headers);
            var response = request.GetResponse();
            response.Close();
            response.Dispose();
        }
    }

    public WebProxy GetProxy()
    {
        try
        {
            if (proxyToUse == "" || proxyToUse == null)
            {
                return null;
            }

            int colons = 0;

            foreach (char ch in proxyToUse)
            {
                if (ch == ':')
                {
                    colons++;
                }
            }

            if (colons == 0)
            {
                return null;
            }

            string[] splitted = Strings.Split(proxyToUse, ":");

            if (colons == 1)
            {
                return new WebProxy(splitted[0], int.Parse(splitted[1]));
            }
            else if (colons == 3)
            {
                WebProxy proxy = new WebProxy(splitted[0], int.Parse(splitted[1]));

                proxy.UseDefaultCredentials = false;
                proxy.Credentials = new System.Net.NetworkCredential(splitted[2], splitted[3]);

                return proxy;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public void ConnectToWS()
    {
        try
        {
            GlobalVariables.operationsExecuted++;
            WebSocket ws = new WebSocket("wss://gateway.discord.gg/?encoding=json&v=9");
            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            ws.Origin = "https://discord.com";
            ws.EnableRedirection = false;
            ws.EmitOnPing = false;
            GlobalVariables.operationsExecuted++;
            ws.Connect();
            GlobalVariables.operationsExecuted++;
            ws.Send("{\"op\":2,\"d\":{\"token\":\"" + token + "\",\"capabilities\":125,\"properties\":{\"os\":\"Windows\",\"browser\":\"Chrome\",\"device\":\"\",\"system_locale\":\"it-IT\",\"browser_user_agent\":\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.82 Safari/537.36\",\"browser_version\":\"93.0.4577.82\",\"os_version\":\"10\",\"referrer\":\"\",\"referring_domain\":\"\",\"referrer_current\":\"\",\"referring_domain_current\":\"\",\"release_channel\":\"stable\",\"client_build_number\":97309,\"client_event_source\":null},\"presence\":{\"status\":\"online\",\"since\":0,\"activities\":[],\"afk\":false},\"compress\":false,\"client_state\":{\"guild_hashes\":{},\"highest_last_message_id\":\"0\",\"read_state_version\":0,\"user_guild_settings_version\":-1}}}");
            GlobalVariables.operationsExecuted++;
            ws.Close();
            GlobalVariables.operationsExecuted++;
            ws = null;
        }
        catch
        {

        }
    }

    public void EmailVerification1()
    {
        string verifyToken = "";

        {
            string emailContent = "";

            while (!emailContent.ToLower().Contains("discord"))
            {
                var request = (HttpWebRequest)WebRequest.Create("https://tempmail.dev/Email/inbox");

                request.UseDefaultCredentials = false;
                request.AllowAutoRedirect = false;
                request.Proxy = null;

                var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                request.Method = "POST";

                var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                {
                    ["Host"] = "tempmail.dev",
                    ["Connection"] = "keep-alive",
                    ["Accept-Encoding"] = "gzip, deflate, br",
                    ["Cookie"] = emailCookie,
                    ["Origin"] = "https://tempmail.dev",
                    ["Content-Length"] = "0"
                });

                field.SetValue(request, headers);
                var response = request.GetResponse();
                GlobalVariables.operationsExecuted++;
                emailContent = DecompressResponse(ReadFully(response.GetResponseStream()));
                GlobalVariables.operationsExecuted++;
                response.Close();
                response.Dispose();
            }

            GlobalVariables.operationsExecuted++;
            string[] splitted = Strings.Split(emailContent, "Clicca qui sotto per verificare il tuo indirizzo e-mail:");

            splitted = Strings.Split(splitted[1], "upn=");
            splitted = Strings.Split(splitted[1], "\"");

            verifyToken = splitted[0].Replace('\r'.ToString(), "").Replace('\n'.ToString(), "").Replace('\t'.ToString(), "").Replace(" ", "").Replace("\\n", "").Replace("\n", "").Replace("\\r", "").Replace("\r", "");
            GlobalVariables.operationsExecuted++;
        }

        GlobalVariables.operationsExecuted++;

        {
            var request = (HttpWebRequest)WebRequest.Create("https://click.discord.com/ls/click?upn=" + verifyToken);

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "GET";

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "click.discord.com",
                ["Connection"] = "keep-alive",
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["sec-ch-ua-mobile"] = "?0",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Upgrade-Insecure-Requests"] = "1",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
                ["Sec-Fetch-Site"] = "none",
                ["Sec-Fetch-Mode"] = "navigate",
                ["Sec-Fetch-User"] = "?1",
                ["Sec-Fetch-Dest"] = "document",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Accept-Language"] = "it-IT,it;q=0.9,en-US;q=0.8,en;q=0.7",
                ["Cookie"] = "OptanonConsent=" + GetOptaNonConsent() + "; fbp=" + fbp,
            });

            field.SetValue(request, headers);
            var response = request.GetResponse();
            GlobalVariables.operationsExecuted++;
            verifyToken = Strings.Split(response.Headers.GetValues("Location")[0], "#token=")[1];
            response.Close();
            response.Dispose();
            GlobalVariables.operationsExecuted++;
        }

        bool captcha = false;

        {
            string data = "{\"token\":\"" + verifyToken + "\",\"captcha_key\":null}";
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/auth/verify");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "POST";

            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["Content-Length"] = requestBytes.Length.ToString(),
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzk0LjAuNDYwNi43MSBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTQuMC40NjA2LjcxIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk5OTQ4LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                ["X-Debug-Options"] = "bugReporterEnabled",
                ["Accept-Language"] = "it",
                ["sec-ch-ua-mobile"] = "?0",
                ["Authorization"] = token,
                ["Content-Type"] = "application/json",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept"] = "*/*",
                ["Origin"] = "https://discord.com",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "cors",
                ["Sec-Fetch-Dest"] = "empty",
                ["Referer"] = "https://discord.com/verify",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Cookie"] = GetCookie() + "; OptanonConsent=" + GetOptaNonConsent(),
            });

            field.SetValue(request, headers);
            var response = request.GetResponse();

            string str = DecompressResponse(ReadFully(response.GetResponseStream()));
            GlobalVariables.operationsExecuted++;

            if (str.Contains("captcha_required"))
            {
                captcha = true;
            }

            response.Close();
            response.Dispose();
        }

        if (captcha)
        {
            string captchaResult = "";

            GlobalVariables.operationsExecuted++;
            if (capMonster)
            {
                var client = new CapMonsterClient(captchaKey);

                var captchaTask = new HCaptchaTaskProxyless
                {
                    WebsiteUrl = "https://discord.com/register",
                    WebsiteKey = "f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36"
                };

                int taskId = client.CreateTaskAsync(captchaTask).Result;

                var solution = client.GetTaskResultAsync<HCaptchaTaskProxylessResult>(taskId).Result;
                captchaResult = solution.GRecaptchaResponse;
                GlobalVariables.captchaSuccess++;
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage CaptchaID = client.PostAsync("http://2captcha.com/in.php?key=" + captchaKey + "&method=hcaptcha&sitekey=f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34&pageurl=https://discord.com/register", null).Result;

                    if (CaptchaID.IsSuccessStatusCode)
                    {
                        string Captcha_ID = (CaptchaID.Content.ReadAsStringAsync().Result).Split('|')[1];
                        string content = (client.GetAsync("http://2captcha.com/res.php?key=" + captchaKey + "&action=get&id=" + Captcha_ID)).Result.Content.ReadAsStringAsync().Result;

                        while (content.Contains("NOT_READY"))
                        {
                            content = (client.GetAsync("http://2captcha.com/res.php?key=" + captchaKey + "&action=get&id=" + Captcha_ID)).Result.Content.ReadAsStringAsync().Result;
                            Thread.Sleep(5000);
                        }

                        captchaResult = content.Split('|')[1];
                        GlobalVariables.captchaSuccess++;
                    }
                }
            }

            GlobalVariables.operationsExecuted++;

            string data = "{\"token\":\"" + verifyToken + "\",\"captcha_key\":\"" + captchaResult + "\"}";
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/auth/verify");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "POST";

            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["Content-Length"] = requestBytes.Length.ToString(),
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzk0LjAuNDYwNi43MSBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTQuMC40NjA2LjcxIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk5OTQ4LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                ["X-Debug-Options"] = "bugReporterEnabled",
                ["Accept-Language"] = "it",
                ["sec-ch-ua-mobile"] = "?0",
                ["Authorization"] = token,
                ["Content-Type"] = "application/json",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept"] = "*/*",
                ["Origin"] = "https://discord.com",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "cors",
                ["Sec-Fetch-Dest"] = "empty",
                ["Referer"] = "https://discord.com/verify",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Cookie"] = GetCookie() + "; OptanonConsent=" + GetOptaNonConsent()
            });

            requestBytes = null;
            field.SetValue(request, headers);

            var response = request.GetResponse();
            dynamic jss = JObject.Parse(DecompressResponse(ReadFully(response.GetResponseStream())));
            GlobalVariables.operationsExecuted++;

            response.Close();
            response.Dispose();
        }
    }

    public void EmailVerification2()
    {
        string verifyToken = "";

        {
            string emailContent = "";

            while (!emailContent.ToLower().Contains("discord"))
            {
                var request = (HttpWebRequest)WebRequest.Create("https://tempmail.dev/Email/inbox");

                request.UseDefaultCredentials = false;
                request.AllowAutoRedirect = false;
                request.Proxy = null;

                var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                request.Method = "POST";

                var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                {
                    ["Host"] = "tempmail.dev",
                    ["Connection"] = "keep-alive",
                    ["Accept-Encoding"] = "gzip, deflate, br",
                    ["Cookie"] = emailCookie,
                    ["Origin"] = "https://tempmail.dev",
                    ["Content-Length"] = "0"
                });

                field.SetValue(request, headers);
                var response = request.GetResponse();
                GlobalVariables.operationsExecuted++;
                emailContent = DecompressResponse(ReadFully(response.GetResponseStream()));
                GlobalVariables.operationsExecuted++;
                response.Close();
                response.Dispose();
            }

            GlobalVariables.operationsExecuted++;
            string[] splitted = Strings.Split(emailContent, "Clicca qui sotto per verificare il tuo indirizzo e-mail:");

            splitted = Strings.Split(splitted[1], "upn=");
            splitted = Strings.Split(splitted[1], "\"");

            verifyToken = splitted[0].Replace('\r'.ToString(), "").Replace('\n'.ToString(), "").Replace('\t'.ToString(), "").Replace(" ", "").Replace("\\n", "").Replace("\n", "").Replace("\\r", "").Replace("\r", "");
            GlobalVariables.operationsExecuted++;
        }

        GlobalVariables.operationsExecuted++;

        {
            var request = (HttpWebRequest)WebRequest.Create("https://click.discord.com/ls/click?upn=" + verifyToken);

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "GET";

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "click.discord.com",
                ["Connection"] = "keep-alive",
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["sec-ch-ua-mobile"] = "?0",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Upgrade-Insecure-Requests"] = "1",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
                ["Sec-Fetch-Site"] = "none",
                ["Sec-Fetch-Mode"] = "navigate",
                ["Sec-Fetch-User"] = "?1",
                ["Sec-Fetch-Dest"] = "document",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Accept-Language"] = "it-IT,it;q=0.9,en-US;q=0.8,en;q=0.7",
                ["Cookie"] = "OptanonConsent=" + GetOptaNonConsent() + "; fbp=" + fbp,
            });

            field.SetValue(request, headers);
            var response = request.GetResponse();
            GlobalVariables.operationsExecuted++;
            verifyToken = Strings.Split(response.Headers.GetValues("Location")[0], "#token=")[1];
            response.Close();
            response.Dispose();
            GlobalVariables.operationsExecuted++;
        }

        {
            string captchaResult = "";

            GlobalVariables.operationsExecuted++;

            if (capMonster)
            {
                var client = new CapMonsterClient(captchaKey);

                var captchaTask = new HCaptchaTaskProxyless
                {
                    WebsiteUrl = "https://discord.com/register",
                    WebsiteKey = "f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36"
                };

                int taskId = client.CreateTaskAsync(captchaTask).Result;

                var solution = client.GetTaskResultAsync<HCaptchaTaskProxylessResult>(taskId).Result;
                captchaResult = solution.GRecaptchaResponse;
                GlobalVariables.captchaSuccess++;
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage CaptchaID = client.PostAsync("http://2captcha.com/in.php?key=" + captchaKey + "&method=hcaptcha&sitekey=f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34&pageurl=https://discord.com/register", null).Result;

                    if (CaptchaID.IsSuccessStatusCode)
                    {
                        string Captcha_ID = (CaptchaID.Content.ReadAsStringAsync().Result).Split('|')[1];
                        string content = (client.GetAsync("http://2captcha.com/res.php?key=" + captchaKey + "&action=get&id=" + Captcha_ID)).Result.Content.ReadAsStringAsync().Result;

                        while (content.Contains("NOT_READY"))
                        {
                            content = (client.GetAsync("http://2captcha.com/res.php?key=" + captchaKey + "&action=get&id=" + Captcha_ID)).Result.Content.ReadAsStringAsync().Result;
                            Thread.Sleep(5000);
                        }

                        captchaResult = content.Split('|')[1];
                        GlobalVariables.captchaSuccess++;
                    }
                }
            }

            GlobalVariables.operationsExecuted++;

            string data = "{\"token\":\"" + verifyToken + "\",\"captcha_key\":\"" + captchaResult + "\"}";
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/auth/verify");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "POST";

            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["Content-Length"] = requestBytes.Length.ToString(),
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzk0LjAuNDYwNi43MSBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTQuMC40NjA2LjcxIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk5OTQ4LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                ["X-Debug-Options"] = "bugReporterEnabled",
                ["Accept-Language"] = "it",
                ["sec-ch-ua-mobile"] = "?0",
                ["Authorization"] = token,
                ["Content-Type"] = "application/json",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept"] = "*/*",
                ["Origin"] = "https://discord.com",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "cors",
                ["Sec-Fetch-Dest"] = "empty",
                ["Referer"] = "https://discord.com/verify",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Cookie"] = GetCookie() + "; OptanonConsent=" + GetOptaNonConsent()
            });

            requestBytes = null;
            field.SetValue(request, headers);

            var response = request.GetResponse();
            dynamic jss = JObject.Parse(DecompressResponse(ReadFully(response.GetResponseStream())));
            GlobalVariables.operationsExecuted++;

            response.Close();
            response.Dispose();
        }
    }

    public void FindEmail()
    {
        try
        {
            GlobalVariables.operationsExecuted++;

            {
                var request = (HttpWebRequest)WebRequest.Create("https://tempmail.dev/");

                request.UseDefaultCredentials = false;
                request.AllowAutoRedirect = false;
                request.Proxy = null;

                var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                request.Method = "GET";

                var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                {
                    ["Host"] = "tempmail.dev",
                    ["Connection"] = "keep-alive",
                    ["Accept-Encoding"] = "gzip, deflate, br"
                });

                field.SetValue(request, headers);
                var response = request.GetResponse();

                foreach (string cookie in response.Headers.GetValues("Set-Cookie"))
                {
                    emailCookie += Strings.Split(cookie, ";")[0] + "; ";
                }

                emailCookie = emailCookie.Substring(0, emailCookie.Length - 2);

                response.Close();
                response.Dispose();
            }

            GlobalVariables.operationsExecuted++;

            {
                var request = (HttpWebRequest)WebRequest.Create("https://tempmail.dev/Email/");

                request.UseDefaultCredentials = false;
                request.AllowAutoRedirect = false;
                request.Proxy = null;

                var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                request.Method = "POST";

                var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                {
                    ["Host"] = "tempmail.dev",
                    ["Connection"] = "keep-alive",
                    ["Accept-Encoding"] = "gzip, deflate, br",
                    ["Cookie"] = emailCookie,
                    ["Origin"] = "https://tempmail.dev",
                    ["Content-Length"] = "0"
                });

                field.SetValue(request, headers);
                var response = request.GetResponse();
                string str = DecompressResponse(ReadFully(response.GetResponseStream()));
                response.Close();
                response.Dispose();

                dynamic jss = JObject.Parse(str);
                email = (string)jss.Email;

                while (!email.EndsWith("@mails.omvvim.edu.in"))
                {
                    email = GetNewEmail();
                }
            }

            GlobalVariables.operationsExecuted++;
        }
        catch
        {

        }
    }

    public string GetNewEmail()
    {
        GlobalVariables.operationsExecuted++;
        var request = (HttpWebRequest)WebRequest.Create("https://tempmail.dev/Email/newEmail");

        request.UseDefaultCredentials = false;
        request.AllowAutoRedirect = false;
        request.Proxy = null;

        var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

        request.Method = "POST";

        var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
        {
            ["Host"] = "tempmail.dev",
            ["Connection"] = "keep-alive",
            ["Accept-Encoding"] = "gzip, deflate, br",
            ["Cookie"] = emailCookie,
            ["Origin"] = "https://tempmail.dev",
            ["Content-Length"] = "0"
        });

        field.SetValue(request, headers);
        var response = request.GetResponse();
        GlobalVariables.operationsExecuted++;
        string str = DecompressResponse(ReadFully(response.GetResponseStream()));
        GlobalVariables.operationsExecuted++;
        response.Close();
        response.Dispose();

        dynamic jss = JObject.Parse(str);
        return (string)jss.Email;
    }

    public void GenerateToken1(string username, string password, string invite)
    {
        try
        {
            GlobalVariables.operationsExecuted++;
            bool toDo = false;

            {
                string theInvite = "null";

                if (invite != "" && invite != null)
                {
                    theInvite = "\"" + invite + "\"";
                }

                string data = "{\"fingerprint\":\"" + GetFingerprint() + "\",\"email\":\"" + email + "\",\"username\":\"" + username + "\",\"password\":\"" + password + "\",\"invite\":" + theInvite + ",\"consent\":true,\"date_of_birth\":\"1997-05-05\",\"gift_code_sku_id\":null,\"captcha_key\":null,\"promotional_email_opt_in\":false}";
                var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/auth/register");

                request.UseDefaultCredentials = false;
                request.AllowAutoRedirect = false;
                request.Proxy = GetProxy();

                var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                request.Method = "POST";

                byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();

                var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                {
                    ["Host"] = "discord.com",
                    ["Connection"] = "keep-alive",
                    ["Content-Length"] = requestBytes.Length.ToString(),
                    ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                    ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzk0LjAuNDYwNi43MSBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTQuMC40NjA2LjcxIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk5OTQ4LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                    ["X-Fingerprint"] = GetFingerprint(),
                    ["X-Debug-Options"] = "bugReporterEnabled",
                    ["Accept-Language"] = "it",
                    ["sec-ch-ua-mobile"] = "?0",
                    ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                    ["Content-Type"] = "application/json",
                    ["sec-ch-ua-platform"] = "\"Windows\"",
                    ["Accept"] = "*/*",
                    ["Origin"] = "https://discord.com",
                    ["Sec-Fetch-Site"] = "same-origin",
                    ["Sec-Fetch-Mode"] = "cors",
                    ["Sec-Fetch-Dest"] = "empty",
                    ["Referer"] = "https://discord.com/register",
                    ["Accept-Encoding"] = "gzip, deflate, br",
                    ["Cookie"] = GetCookie() + "; OptanonConsent=" + GetOptaNonConsent(),
                });

                requestBytes = null;
                field.SetValue(request, headers);

                try
                {
                    GlobalVariables.operationsExecuted++;
                    var response = request.GetResponse();
                    string str = DecompressResponse(ReadFully(response.GetResponseStream()));

                    if (str.Contains("captcha_required"))
                    {
                        toDo = true;
                    }
                    else if (str.Contains("token"))
                    {
                        GlobalVariables.operationsExecuted++;
                        dynamic jss = JObject.Parse(DecompressResponse(ReadFully(response.GetResponseStream())));

                        response.Close();
                        response.Dispose();

                        token = (string)jss.token;
                        GlobalVariables.operationsExecuted++;

                        return;
                    }

                    GlobalVariables.operationsExecuted++;

                    response.Close();
                    response.Dispose();
                }
                catch
                {
                    toDo = true;
                }
            }

            GlobalVariables.operationsExecuted++;

            if (toDo)
            {
                GlobalVariables.operationsExecuted++;
                string captchaResult = "";

                if (capMonster)
                {
                    GlobalVariables.operationsExecuted++;
                    var client = new CapMonsterClient(captchaKey);

                    var captchaTask = new HCaptchaTaskProxyless
                    {
                        WebsiteUrl = "https://discord.com/register",
                        WebsiteKey = "f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34",
                        UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36"
                    };

                    int taskId = client.CreateTaskAsync(captchaTask).Result;

                    var solution = client.GetTaskResultAsync<HCaptchaTaskProxylessResult>(taskId).Result;
                    captchaResult = solution.GRecaptchaResponse;
                    GlobalVariables.captchaSuccess++;
                    GlobalVariables.operationsExecuted++;
                }
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage CaptchaID = client.PostAsync("http://2captcha.com/in.php?key=" + captchaKey + "&method=hcaptcha&sitekey=f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34&pageurl=https://discord.com/register", null).Result;

                        if (CaptchaID.IsSuccessStatusCode)
                        {
                            string Captcha_ID = (CaptchaID.Content.ReadAsStringAsync().Result).Split('|')[1];
                            string content = (client.GetAsync("http://2captcha.com/res.php?key=" + captchaKey + "&action=get&id=" + Captcha_ID)).Result.Content.ReadAsStringAsync().Result;

                            while (content.Contains("NOT_READY"))
                            {
                                GlobalVariables.operationsExecuted++;
                                content = (client.GetAsync("http://2captcha.com/res.php?key=" + captchaKey + "&action=get&id=" + Captcha_ID)).Result.Content.ReadAsStringAsync().Result;
                                Thread.Sleep(5000);
                            }

                            captchaResult = content.Split('|')[1];
                            GlobalVariables.captchaSuccess++;
                            GlobalVariables.operationsExecuted++;
                        }
                    }
                }

                string theInvite = "null";

                if (invite != "" && invite != null)
                {
                    theInvite = "\"" + invite + "\"";
                }

                string data = "{\"fingerprint\":\"" + GetFingerprint() + "\",\"email\":\"" + email + "\",\"username\":\"" + username + "\",\"password\":\"" + password + "\",\"invite\":" + theInvite + ",\"consent\":true,\"date_of_birth\":\"1997-05-05\",\"gift_code_sku_id\":null,\"captcha_key\":\"" + captchaResult + "\",\"promotional_email_opt_in\":false}";
                var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/auth/register");

                request.UseDefaultCredentials = false;
                request.AllowAutoRedirect = false;
                request.Proxy = GetProxy();

                var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

                request.Method = "POST";

                byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();

                var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
                {
                    ["Host"] = "discord.com",
                    ["Connection"] = "keep-alive",
                    ["Content-Length"] = requestBytes.Length.ToString(),
                    ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                    ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzk0LjAuNDYwNi43MSBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTQuMC40NjA2LjcxIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk5OTQ4LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                    ["X-Fingerprint"] = GetFingerprint(),
                    ["X-Debug-Options"] = "bugReporterEnabled",
                    ["Accept-Language"] = "it",
                    ["sec-ch-ua-mobile"] = "?0",
                    ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                    ["Content-Type"] = "application/json",
                    ["sec-ch-ua-platform"] = "\"Windows\"",
                    ["Accept"] = "*/*",
                    ["Origin"] = "https://discord.com",
                    ["Sec-Fetch-Site"] = "same-origin",
                    ["Sec-Fetch-Mode"] = "cors",
                    ["Sec-Fetch-Dest"] = "empty",
                    ["Referer"] = "https://discord.com/register",
                    ["Accept-Encoding"] = "gzip, deflate, br",
                    ["Cookie"] = GetCookie() + "; OptanonConsent=" + GetOptaNonConsent(),
                });

                requestBytes = null;
                field.SetValue(request, headers);

                var response = request.GetResponse();
                GlobalVariables.operationsExecuted++;
                dynamic jss = JObject.Parse(DecompressResponse(ReadFully(response.GetResponseStream())));
                GlobalVariables.operationsExecuted++;

                response.Close();
                response.Dispose();

                token = (string)jss.token;
            }
        }
        catch
        {

        }
    }

    public void GenerateToken2(string username, string password, string invite)
    {
        try
        {
            GlobalVariables.operationsExecuted++;
            string captchaResult = "";

            if (capMonster)
            {
                GlobalVariables.operationsExecuted++;
                var client = new CapMonsterClient(captchaKey);

                var captchaTask = new HCaptchaTaskProxyless
                {
                    WebsiteUrl = "https://discord.com/register",
                    WebsiteKey = "f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34",
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36"
                };

                int taskId = client.CreateTaskAsync(captchaTask).Result;

                var solution = client.GetTaskResultAsync<HCaptchaTaskProxylessResult>(taskId).Result;
                captchaResult = solution.GRecaptchaResponse;
                GlobalVariables.captchaSuccess++;
                GlobalVariables.operationsExecuted++;
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage CaptchaID = client.PostAsync("http://2captcha.com/in.php?key=" + captchaKey + "&method=hcaptcha&sitekey=f5561ba9-8f1e-40ca-9b5b-a0b3f719ef34&pageurl=https://discord.com/register", null).Result;

                    if (CaptchaID.IsSuccessStatusCode)
                    {
                        string Captcha_ID = (CaptchaID.Content.ReadAsStringAsync().Result).Split('|')[1];
                        string content = (client.GetAsync("http://2captcha.com/res.php?key=" + captchaKey + "&action=get&id=" + Captcha_ID)).Result.Content.ReadAsStringAsync().Result;

                        while (content.Contains("NOT_READY"))
                        {
                            GlobalVariables.operationsExecuted++;
                            content = (client.GetAsync("http://2captcha.com/res.php?key=" + captchaKey + "&action=get&id=" + Captcha_ID)).Result.Content.ReadAsStringAsync().Result;
                            Thread.Sleep(5000);
                        }

                        captchaResult = content.Split('|')[1];
                        GlobalVariables.captchaSuccess++;
                        GlobalVariables.operationsExecuted++;
                    }
                }
            }

            string theInvite = "null";

            if (invite != "" && invite != null)
            {
                theInvite = "\"" + invite + "\"";
            }

            string data = "{\"fingerprint\":\"" + GetFingerprint() + "\",\"email\":\"" + email + "\",\"username\":\"" + username + "\",\"password\":\"" + password + "\",\"invite\":" + theInvite + ",\"consent\":true,\"date_of_birth\":\"1997-05-05\",\"gift_code_sku_id\":null,\"captcha_key\":\"" + captchaResult + "\",\"promotional_email_opt_in\":false}";
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/auth/register");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = GetProxy();

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "POST";

            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(data);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["Content-Length"] = requestBytes.Length.ToString(),
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["X-Super-Properties"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzk0LjAuNDYwNi43MSBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTQuMC40NjA2LjcxIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk5OTQ4LCJjbGllbnRfZXZlbnRfc291cmNlIjpudWxsfQ==",
                ["X-Fingerprint"] = GetFingerprint(),
                ["X-Debug-Options"] = "bugReporterEnabled",
                ["Accept-Language"] = "it",
                ["sec-ch-ua-mobile"] = "?0",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["Content-Type"] = "application/json",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept"] = "*/*",
                ["Origin"] = "https://discord.com",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "cors",
                ["Sec-Fetch-Dest"] = "empty",
                ["Referer"] = "https://discord.com/register",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Cookie"] = GetCookie() + "; OptanonConsent=" + GetOptaNonConsent(),
            });

            requestBytes = null;
            field.SetValue(request, headers);

            var response = request.GetResponse();
            GlobalVariables.operationsExecuted++;
            dynamic jss = JObject.Parse(DecompressResponse(ReadFully(response.GetResponseStream())));
            GlobalVariables.operationsExecuted++;

            response.Close();
            response.Dispose();

            token = (string)jss.token;
        }
        catch
        {

        }
    }

    public string GetCookie()
    {
        if (cookieStr == "")
        {
            SetCookies();
        }

        return cookieStr;
    }

    public string GetFingerprint()
    {
        if (cookieStr == "")
        {
            SetCookies();
        }

        if (fingerprint == "")
        {
            SetFingerprint();
            cookieStr += "; _fbp=" + fbp;
        }

        return fingerprint;
    }

    public void SetCookies()
    {
        try
        {
            GlobalVariables.operationsExecuted++;
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "GET";

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["Upgrade-Insecure-Requests"] = "1",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
                ["Sec-Fetch-Site"] = "none",
                ["Sec-Fetch-Mode"] = "navigate",
                ["Sec-Fetch-User"] = "?1",
                ["Sec-Fetch-Dest"] = "document",
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["sec-ch-ua-mobile"] = "?0",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Accept-Language"] = "it-IT,it;q=0.9,en-US;q=0.8,en;q=0.7",
            });

            field.SetValue(request, headers);

            var response = request.GetResponse();
            GlobalVariables.operationsExecuted++;

            foreach (string cookie in response.Headers.GetValues("Set-Cookie"))
            {
                cookieStr += Strings.Split(cookie, ";")[0] + "; ";
            }

            GlobalVariables.operationsExecuted++;

            response.Close();
            response.Dispose();

            cookieStr = cookieStr.Substring(0, cookieStr.Length - 2) + "; locale=it";

            GlobalVariables.operationsExecuted++;
        }
        catch
        {

        }
    }

    public void SetFingerprint()
    {
        try
        {
            GlobalVariables.operationsExecuted++;
            var request = (HttpWebRequest)WebRequest.Create("https://discord.com/api/v9/experiments");

            request.UseDefaultCredentials = false;
            request.AllowAutoRedirect = false;
            request.Proxy = null;

            var field = typeof(HttpWebRequest).GetField("_HttpRequestHeaders", BindingFlags.Instance | BindingFlags.NonPublic);

            request.Method = "GET";

            var headers = new CustomWebHeaderCollection(new Dictionary<string, string>
            {
                ["Host"] = "discord.com",
                ["Connection"] = "keep-alive",
                ["sec-ch-ua"] = "\"Google Chrome\";v=\"93\", \" Not;A Brand\";v=\"99\", \"Chromium\";v=\"93\"",
                ["X-Track"] = "eyJvcyI6IldpbmRvd3MiLCJicm93c2VyIjoiQ2hyb21lIiwiZGV2aWNlIjoiIiwic3lzdGVtX2xvY2FsZSI6Iml0LUlUIiwiYnJvd3Nlcl91c2VyX2FnZW50IjoiTW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzkzLjAuNDU3Ny42MyBTYWZhcmkvNTM3LjM2IiwiYnJvd3Nlcl92ZXJzaW9uIjoiOTMuMC40NTc3LjYzIiwib3NfdmVyc2lvbiI6IjEwIiwicmVmZXJyZXIiOiIiLCJyZWZlcnJpbmdfZG9tYWluIjoiIiwicmVmZXJyZXJfY3VycmVudCI6IiIsInJlZmVycmluZ19kb21haW5fY3VycmVudCI6IiIsInJlbGVhc2VfY2hhbm5lbCI6InN0YWJsZSIsImNsaWVudF9idWlsZF9udW1iZXIiOjk5OTksImNsaWVudF9ldmVudF9zb3VyY2UiOm51bGx9",
                ["sec-ch-ua-mobile"] = "?0",
                ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36",
                ["sec-ch-ua-platform"] = "\"Windows\"",
                ["Accept"] = "*/*",
                ["Sec-Fetch-Site"] = "same-origin",
                ["Sec-Fetch-Mode"] = "cors",
                ["Sec-Fetch-Dest"] = "empty",
                ["Referer"] = "https://discord.com/",
                ["Accept-Encoding"] = "gzip, deflate, br",
                ["Accept-Language"] = "it-IT,it;q=0.9,en-US;q=0.8,en;q=0.7",
                ["Cookie"] = GetCookie() + "; OptanonConsent=" + GetOptaNonConsent()
            });

            field.SetValue(request, headers);

            var response = request.GetResponse();
            GlobalVariables.operationsExecuted++;
            dynamic jss = JObject.Parse(DecompressResponse(ReadFully(response.GetResponseStream())));
            GlobalVariables.operationsExecuted++;
            response.Close();
            response.Dispose();

            fingerprint = (string)jss.fingerprint;
            GlobalVariables.operationsExecuted++;
        }
        catch
        {

        }
    }

    public long GetUniqueLong(int size)
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

    public string GetUniqueString(int size)
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

            result.Append(characters[idx]);
        }

        return result.ToString();
    }


    public static byte[] ReadFully(Stream input)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }

    public string DecompressResponse(byte[] payload)
    {
        try
        {
            return Encoding.UTF8.GetString(BrotliSharpLib.Brotli.DecompressBuffer(payload, 0, payload.Length));
        }
        catch
        {
            return Encoding.UTF8.GetString(payload);
        }
    }

    public DateTime GetCurrentRealDateTime()
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime().AddSeconds((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds + 7200L);
    }

    public string GetOptaNonConsent()
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

        return "isIABGlobal=false&datestamp=" + dayOfWeek + "+" + month + "+" + day.ToString() + "+" + year.ToString() + "+" + hour.ToString() + "%3A" + minute.ToString() + "%3A" + second.ToString() + "+GMT%2B0200+(Ora+legale+dell%E2%80%99Europa+centrale)&version=6.17.0&hosts=&landingPath=https%3A%2F%2Fdiscord.com%2F&groups=C0001%3A1%2CC0002%3A0%2CC0003%3A0";
    }
}