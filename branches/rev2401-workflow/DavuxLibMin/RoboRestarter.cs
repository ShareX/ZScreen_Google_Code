/*
 * Author: David Amenta
 * Release Date: 12/12/2009
 * License: Free for any use.
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DavuxLib
{
    /// <summary>
    /// Robust restarting framework.  Ideal for a crash reporting process.
    /// </summary>
    public class RoboRestarter
    {
        /// <summary>
        /// Exit code to indicate a graceful exit.  ALL other exit codes will be assumed to be a crash.
        /// </summary>
        public static readonly int GracefulExitCode = 75;

        private RoboRestarter() { }

        private int Port = 0;

        /// <summary>
        /// Enable hosting.  The application will check for hosting, and enable it if not already enabled.
        /// </summary>
        /// <param name="ProgramArgs">Program Arguments</param>
        public static void EnableHosting(string[] ProgramArgs)
        {
            RoboRestarter r = new RoboRestarter();
            if (!r.IsHosted(ProgramArgs))
            {
                Trace.WriteLine("Starting Hosted");
                r.SelfHost(ProgramArgs);
            }
        }

        /// <summary>
        /// Determine if the application is hosted.
        /// </summary>
        /// <param name="ProgramArgs"></param>
        /// <returns></returns>
        private bool IsHosted(string[] ProgramArgs)
        {
            bool hosted = ProgramArgs.Contains("--robo_hosted");

            foreach (string s in ProgramArgs)
            {
                if (s.StartsWith("--robo_port:"))
                {
                    string p = s.Substring(12);
                    Port = int.Parse(p);
                }
            }

            if (hosted && Port > 0)
            {
                // start the watchdog server
                Thread t = new Thread(ThreadStart);
                t.Start();
            }

            return hosted || ProgramArgs.Contains("--no-hosting");
        }

        /// <summary>
        /// Start this process under hosting with the specified arguments
        /// </summary>
        /// <param name="program_args">Program Arguments</param>
        private void StartHosted(string[] program_args)
        {
            string args = "\"" + System.Windows.Forms.Application.ExecutablePath
                + "\"";
            foreach (string s in program_args)
            {
                args += " \"" + s + "\"";
            }
            string path = System.Windows.Forms.Application.ExecutablePath;
            System.Diagnostics.Process.Start(path, args);
        }

        /// <summary>
        /// Hosting thread entry point.
        /// </summary>
        private void ThreadStart()
        {
            while (true)
            {
                TcpClient t = new TcpClient();
                try
                {
                    t.Connect("127.0.0.1", Port);

                    if (t.Connected)
                    {
                        StreamWriter sw = new StreamWriter(t.GetStream());
                        sw.AutoFlush = true;

                        while (true)
                        {
                            sw.WriteLine(Environment.MachineName);
                            Thread.Sleep(1000 * 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("DavuxLib/RoboRestarter/ThreadStart Error: " + ex);
                }

                Thread.Sleep(1000 * 2);
            }
        }

        public void SelfHost(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            while (true)
            {
                string program_args = "";

                for (int i = 0; i < args.Length; i++)
                {
                    program_args += args[i] + " ";
                }

                RoboHost h = new RoboHost();
                program_args += "--robo_hosted --robo_port:" + h.Port;
                Process p = Process.Start(System.Windows.Forms.Application.ExecutablePath, program_args.Trim());
                while (!p.WaitForExit(1000 * 10))
                {
                    h.KeepAlives++;
                    // 60 seconds
                    if (h.KeepAlives > 6)
                    {
                        Trace.WriteLine("Client is not alive: " + h.KeepAlives);
                        // the client is not alive.
                        p.Kill();
                    }
                }
                // process has quit

                Trace.WriteLine("Process has quit: " + p.ExitCode);
                if (p.ExitCode == GracefulExitCode)
                {
                    Trace.WriteLine("Application closed gracefully, shutting down.");
                    Environment.Exit(0);
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Trace.WriteLine("Unhandled Exception: " + e.ExceptionObject);
        }
    }

    internal class RoboHost
    {
        private TcpListener _tcpListener = null;
        private Thread _worker = null;

        public int KeepAlives { get; set; }
        public int Port
        {
            get
            {
                return ((IPEndPoint)_tcpListener.LocalEndpoint).Port;
            }
        }

        internal RoboHost()
        {
            _tcpListener = new TcpListener(IPAddress.Any, 0);
            _tcpListener.Start();
            _worker = new Thread(ThreadStart);
            _worker.Start();
        }

        private void ThreadStart()
        {
            while (_tcpListener.Server.IsBound)
            {
                try
                {
                    TcpClient tcpConnection = _tcpListener.AcceptTcpClient();
                    Trace.WriteLine("Client Connected: " + tcpConnection.Client.RemoteEndPoint.ToString());
                    ThreadPool.QueueUserWorkItem(new WaitCallback(_ConnectionThreadEntry), tcpConnection);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error accepting client: " + ex);
                }
            }
            Trace.WriteLine("Socket is not bound.");
        }

        private void _ConnectionThreadEntry(object otcp)
        {
            TcpClient tcp = (TcpClient)otcp;

            while (true)
            {
                if (tcp.Available > 0)
                {
                    byte[] buff = new byte[tcp.Client.ReceiveBufferSize];
                    if (tcp.Client.Receive(buff) > 0)
                    {
                        KeepAlives = 0;
                    }
                }
                Thread.Sleep(1000 * 5); // 5 seconds between ping checks
            }
        }
    }
}