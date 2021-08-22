using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamVR_WebKit;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;

namespace SteamVR_PhasmoMatrix
{
    class Program
    {
        static WebKitOverlay basicOverlay;
        static void Main(string[] args)
        {
            SteamVR_WebKit.SteamVR_WebKit.UseExperimentalOGL = true;
            SteamVR_WebKit.SteamVR_WebKit.DefaultFragmentShaderPath = Environment.CurrentDirectory + "\\Resources\\fragShader.frag";
            SteamVR_WebKit.SteamVR_WebKit.Init(new CefSharp.CefSettings() { FocusedNodeChangedEnabled = true });
            SteamVR_WebKit.SteamVR_WebKit.FPS = 30;
            SteamVR_WebKit.SteamVR_WebKit.LogEvent += SteamVR_WebKit_LogEvent;
            //SteamVR_WebKit.SteamVR_WebKit.TraceLevel = true;

            basicOverlay = new WebKitOverlay(new Uri("https://www.phasmophobia-matrix.com"), 1280, 1024, "PhasmoMatrix", "Phasmophobia Matrix", OverlayType.Dashboard);
            basicOverlay.DashboardOverlay.Width = 3.0f;
            basicOverlay.DashboardOverlay.SetThumbnail("Resources/logo.png");
            basicOverlay.BrowserPreInit += Overlay_BrowserPreInit;
            basicOverlay.BrowserReady += Overlay_BrowserReady;
            basicOverlay.StartBrowser();
            basicOverlay.EnableKeyboard = true;
            basicOverlay.MouseDeltaTolerance = 20;
            //basicOverlay.MessageHandler.DebugMode = true;

            //SteamVR_Application application = new SteamVR_Application();
            //application.InstallManifest(true);
            //application.SetAutoStartEnabled(false);
            //application.RemoveManifest();

            SteamVR_WebKit.SteamVR_WebKit.RunOverlays();
        }

        private static void SteamVR_WebKit_LogEvent(string line)
        {
            Console.WriteLine(line);
        }

        private static void Overlay_BrowserReady(object sender, EventArgs e)
        {
            //basicOverlay.Browser.GetBrowser().GetHost().ShowDevTools();
        }

        private static void Browser_ConsoleMessage(object sender, CefSharp.ConsoleMessageEventArgs e)
        {
            string[] srcSplit = e.Source.Split('/');
            SteamVR_WebKit.SteamVR_WebKit.Log("[CONSOLE " + srcSplit[srcSplit.Length - 1] + ":" + e.Line + "] " + e.Message);
        }

        private static void Overlay_BrowserPreInit(object sender, EventArgs e)
        {
            SteamVR_WebKit.SteamVR_WebKit.Log("Browser ready");
            basicOverlay.Browser.ConsoleMessage += Browser_ConsoleMessage;
        }
    }
}