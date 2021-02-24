using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using DatAdmin;
using System.IO;
using Routrek.SSHC;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Plugin.sshtunnel
{
    public enum SshAuthentization { Password, PublicKey };

    public class STunReader : ISSHConnectionEventReceiver, ISSHChannelEventReceiver
    {
        public SSHChannel Channel;
        Stream m_stream;

        public STunReader(Stream stream)
        {
            m_stream = stream;
        }

        #region ISSHConnectionEventReceiver Members

        public void OnDebugMessage(bool always_display, byte[] msg)
        {
        }

        public void OnIgnoreMessage(byte[] msg)
        {
        }

        public void OnUnknownMessage(byte type, byte[] data)
        {
        }

        public void OnError(Exception error, string msg)
        {
        }

        public void OnConnectionClosed()
        {
        }

        public void OnAuthenticationPrompt(string[] prompts)
        {
        }

        public PortForwardingCheckResult CheckPortForwardingRequest(string remote_host, int remote_port, string originator_ip, int originator_port)
        {
            PortForwardingCheckResult r = new PortForwardingCheckResult();
            r.allowed = true;
            r.channel = this;
            return r;
        }

        public void EstablishPortforwarding(ISSHChannelEventReceiver receiver, SSHChannel channel)
        {
            Channel = channel;
        }

        #endregion

        Stream m_fw = new FileStream(@"c:\test\_stuncon\" + Guid.NewGuid().ToString(), FileMode.Create);

        #region ISSHChannelEventReceiver Members

        public void OnData(byte[] data, int offset, int length)
        {
            m_stream.Write(data, offset, length);
            m_fw.Write(data, offset, length);
            m_fw.Flush();
        }

        public void OnExtendedData(int type, byte[] data)
        {
        }

        public void OnChannelClosed()
        {
        }

        public void OnChannelEOF()
        {
            Channel.Close();
        }

        public void OnChannelError(Exception error, string msg)
        {
        }

        public void OnChannelReady()
        {
        }

        public void OnMiscPacket(byte packet_type, byte[] data, int offset, int length)
        {
        }

        #endregion
    }

    public class StunConnection : TunnelConnection, ICommandShell, ICmdLineConnection
    {
        //string m_currentDatabase;
        //SshShell m_shell;
        SSHConnection m_conn;
        //Stream m_stream;
        //StreamReader m_reader;
        //PipedInputStream m_instream;
        PipeStream m_stream;
        STunReader m_treader;
        //PipeStream m_outstream;
        ICmdLineAdapter m_cmdLineAdapter;

        Mutex m_mutex = new Mutex();
        bool m_openedReader;

        private string m_version = null;
        bool m_isDbConnected;
        //PtunConnectionStringBuilder m_params = new PtunConnectionStringBuilder();

        public StunConnection()
            : base(StunTunnellingDriver.Instance)
        {
        }
        public StunConnection(string conns)
            : base(StunTunnellingDriver.Instance, conns)
        {
        }
        public StunConnection(StunConnectionStringBuilder pars)
            : base(StunTunnellingDriver.Instance, pars)
        {
        }

        public StunConnectionStringBuilder Params
        {
            get { return (StunConnectionStringBuilder)m_params; }
        }
        //public string EncodingStyleName { get { return m_encodingstyle.ToString(); } }

        public Encoding ResultSetEncoding
        {
            get
            {
                return Params.RealEncoding;
            }
        }

        public override void ChangeDatabase(string databaseName)
        {
            //m_currentDatabase = databaseName;
            this.ExecuteNonQuery("use " + Dialect.QuoteIdentifier(databaseName));
        }

        public override string Database
        {
            get { return m_params.InitialDatabase; }
        }

        public override void Close()
        {
            CloseDatabase();
            CloseShell();
        }

        public ISqlDialect Dialect
        {
            get { return StunDefaults.Instance.Dialect(m_params.Engine); }
        }

        private void OpenShell()
        {
            m_cmdLineAdapter = Dialect.CreateCmdLineAdapter();
            m_stream = new PipeStream();
            //m_reader = new StreamReader(m_stream);
            SSHConnectionParameter f = new SSHConnectionParameter();
            f.UserName = Params.SshLogin;
            f.Password = Params.SshPassword;
            f.Protocol = SSHProtocol.SSH2;
            f.AuthenticationType = AuthenticationType.Password;
            f.WindowSize = 0x1000;
            m_treader = new STunReader(m_stream);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(Params.SshHost, Params.SshPort);
            m_conn = SSHConnection.Connect(f, m_treader, s);

            m_treader.Channel = m_conn.OpenShell(m_treader);

            //SSHConnectionInfo ci = m__conn.ConnectionInfo;

            //byte[] b = new byte[1];
            //while (true)
            //{
            //    int input = System.Console.Read();

            //    b[0] = (byte)input;
            //    //Debug.WriteLine(input);
            //    reader._pf.Transmit(b);
            //}


            //m_shell = new SshShell(Params.SshHost, Params.SshLogin);
            //if (!String.IsNullOrEmpty(Params.SshPassword)) m_shell.Password = Params.SshPassword;
            //if (!String.IsNullOrEmpty(Params.IdentifyFile)) m_shell.AddIdentityFile(Params.IdentifyFile);
            //m_shell.Connect(Params.SshPort);
            //m_shell.WriteLine("echo AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            //m_instream = new PipeStream();
            //m_outstream = new PipeStream();
            //m_outstream.BlockLastReadBuffer = false;
            //m_shell.SetStream(m_instream, m_outstream);
            ////m_stream = m_shell.GetStream();
            ////m_shell.m_instream = (PipedInputStream)((CombinedStream)m_stream).InputStream;
            //m_reader = new StreamReader(m_outstream);
            //ConsumeOutput();
        }

        private void ReadLine(MemoryStream ms)
        {
            for (int i = 0; i < 2; i++)
            {
                while (m_stream.Length > 0)
                {
                    int ch = m_stream.ReadByte();
                    if (ch < 0) return;
                    if (ch == '\n')
                    {
                        return;
                    }
                    else
                    {
                        ms.WriteByte((byte)ch);
                    }
                }
                Thread.Sleep(20);
            }
        }

        public string ReadLineAndWait()
        {
            var ms = new MemoryStream();
            int ch = m_stream.ReadByte();
            ms.WriteByte((byte)ch);
            ReadLine(ms);
            return Encoding.UTF8.GetString(ms.ToArray());
            //if (ch < 0) return Encoding.UTF8.GetString(ms.ToArray());
            //if (ch == '\n')
            //{
            //    return Encoding.UTF8.GetString(ms.ToArray());
            //}
            //else
            //{
            //    ms.WriteByte((byte)ch);
            //}
        }

        public string ReadLineFixedLenght(int length)
        {
            var ms = new MemoryStream();
            ReadLineFixedLenght(length, ms);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public string ReadLineFixedLenghtAndWait(int length)
        {
            var ms = new MemoryStream();
            int ch = m_stream.ReadByte();
            ms.WriteByte((byte)ch);
            ReadLineFixedLenght(length - 1, ms);
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public void ReadLineFixedLenght(int length, MemoryStream ms)
        {
            int written = 0;
            for (int i = 0; i < 2; i++)
            {
                while (m_stream.Length > 0)
                {
                    int ch = m_stream.ReadByte();
                    if (ch < 0) return;
                    if (ch == '\r') ch = m_stream.ReadByte(); // skip 13

                    if (ch == '\n' && written >= length)
                    {
                        return;
                    }
                    else
                    {
                        if (written < length)
                        {
                            ms.WriteByte((byte)ch);
                            written++;
                        }
                    }
                }
                Thread.Sleep(20);
            }
        }

        public string ReadLine()
        {
            if (m_stream.Length == 0) return null;
            var ms = new MemoryStream();
            ReadLine(ms);
            return Encoding.UTF8.GetString(ms.ToArray());
            //return m_reader.ReadLine();
            //if (m_instream.available() > 0) return m_reader.ReadLine();
            //return null;
        }

        private void Write(string msg)
        {
            m_treader.Channel.Transmit(Encoding.UTF8.GetBytes(msg));
        }

        public void WriteLine(string msg)
        {
            Write(msg + "\n");
        }

        private void OpenDatabase()
        {
            m_cmdLineAdapter.OpenDatabase(this, this);
        }

        private void CloseDatabase()
        {
            WriteLine("\\q");
        }

        public void ConsumeOutput()
        {
            if (!m_isDbConnected)
            {
                for (; ; )
                {
                    string s = ReadLine();
                    if (s == null) return;
                    //if (s == null)
                    //{
                    //    m_reader = new StreamReader(m_outstream);
                    //    return;
                    //}
                }
                //while (m_outstream.Length > 0)
                //{
                //    string s = m_reader.ReadLine();
                //}
            }
        }

        private void CloseShell()
        {
            m_conn.Close();
            m_conn = null;
            m_treader = null;
            m_stream = null;
        }

        public override void Open()
        {
            OpenShell();
            OpenDatabase();
            //m_version = Dialect.SelectVersion(this);
        }

        public override string ServerVersion
        {
            get { return m_version; }
        }

        public override System.Data.ConnectionState State
        {
            get
            {
                if (m_conn != null) return ConnectionState.Open;
                return ConnectionState.Closed;
            }
        }

        public override void PingTunnel()
        {
            OpenShell();
            CloseShell();
        }

        public override void PingDb()
        {
            OpenShell();
            OpenDatabase();
            CloseDatabase();
            CloseShell();
            //using (var ping = WebResultPing.CreatePingDb(this)) { }
        }

        public ITunnelResultSet ExecuteResultSet(string command)
        {
            LockReader();
            ConsumeOutput();
            Thread.Sleep(50);
            ConsumeOutput();
            WriteLine(command + ";");
            var res = new SshResultSet(this);
            return res;
        }

        public void LockReader()
        {
            m_mutex.WaitOne();
            if (m_openedReader) throw new InternalError("Cannot open two symultaneous readers");
            m_openedReader = true;
        }

        public void UnlockReader()
        {
            m_openedReader = false;
            m_mutex.ReleaseMutex();
        }

        #region ICmdLineConnection Members

        TunnelConnectionStringBuilder ICmdLineConnection.Params
        {
            get { return this.Params; }
        }

        void ICmdLineConnection.SetVersion(string version)
        {
            m_version = version;
        }

        #endregion
    }
}
