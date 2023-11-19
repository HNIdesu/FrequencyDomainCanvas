using System.Diagnostics;
using System.IO.Pipes;

namespace HNIdesu.Logger
{
    public class PopupWindowLogger
    {
        private AnonymousPipeServerStream _PipeServer;
        private StreamWriter _StreamWriter;
        private Process _Process;
        public bool IsClosed { get; private set; } = true;
        public string Handle { get; private set; }
        public PopupWindowLogger()
        {
            _PipeServer = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
            Handle = _PipeServer.GetClientHandleAsString();
            _StreamWriter = new StreamWriter(_PipeServer);
            _Process = new Process() { StartInfo = new ProcessStartInfo(@"E:\Documents\Visual Studio 2022\Code\Csharp\Test\AnonymousPipeTest\Receiver\bin\Debug\net6.0\Receiver.exe", Handle) };
            
        }
        public void Open()
        {
            if (!IsClosed)
                throw new InvalidOperationException("The logger has already been opened");
            try
            {
                if (_Process.HasExited);
            }
            catch (Exception)
            {
                _Process.Start();
                IsClosed = false;
                return;
            }
            throw new InvalidOperationException("The logger has been closed");
        }
        public void WriteLine(string msg)
        {
            if (IsClosed)
                throw new InvalidOperationException("Send message to a closed logger");
            _StreamWriter.WriteLine(msg);
            _StreamWriter.Flush();
        }
        public void Close()
        {
            _StreamWriter.Close();
            if (!_Process.WaitForExit(3000))
            {
                _Process.CloseMainWindow();
                _Process.Close();
            }
            IsClosed = true;
            
        }
        public void Write(string msg)
        {
            if (IsClosed)
                throw new InvalidOperationException("Send message to a closed logger");
            _StreamWriter.Write(msg);
            _StreamWriter.Flush();
        }
    }
}