using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLauncher.NewIdea
{
    class Syringe
    {
        public int Run(string[] args)
        {
            const string VersionString = "Syringe 0.7.2.0 C#";

            Logger logger = new Logger();
            logger.Writer(LogGrade.info, VersionString);
            
                string argStr = "";
                foreach (var arg in args)
                    argStr += arg + " ";
                logger.Writer(LogGrade.info, string.Format("WinMain: arguments = \"{0}\"", argStr));
            
            var failure = "Could not load executable.";
            var exit_code = 774L;

            try
            {
                if (!(args.Length>1))
                {
                    throw new InvalidArgumentsException();
                }

                logger.Writer(LogGrade.info, string.Format("WinMan: Trying to load execultable file \"{0}\" ...", args[1]));

                //SyringeDebugger Debugger

                failure = "Could not run executable";

                logger.Writer(LogGrade.info, "WinMain: SyringeDebugger::FindDLLs();");
                //debugger.finddlls();

                logger.Writer(LogGrade.info, string.Format("WinMain: SyringeDebugger::Run(\"{0}\");", argStr));

                //debugger.Run();
                logger.Writer(LogGrade.info, "WinMain: SyringeDebugger::Run finished.");
                logger.Writer(LogGrade.info, "WinMain: Exiting on success.");
                return 0;
            }catch(InvalidArgumentsException e)
            {
                logger.Writer(LogGrade.fatal, "WinMain: No or invalid command line arguments given, exiting...");
                new ErrorBox("Syringe cannot be run just like that.\n\nUsage:\n\tSyringe.exe \"<exe name>\" <arguments>");
                exit_code = 87L;
            }
            catch (Exception e)
            {
                logger.Writer(LogGrade.fatal, e);
                new ErrorBox(e, false);
            }
            logger.Writer(LogGrade.info, "WinMain: Exiting on failure.");
            return (int)exit_code;

        }
    }
    class SyringeDebugger
    {
        static uint MaxNameLength = 0x100u;
        static byte INIT = 0;
        static byte INT3 = 0xCC;
        static byte NOP = 0x90;

        #region Private
        private string exe;
        /*
        private void RetrieveInfo()
        {
            Logger logger = new Logger();
            logger.Writer(LogGrade.info, "SyringeDebugger.RetrieveInfo(): Retrieving info from the executable file...");

            try
            {
                PortableExecutable pe = new PortableExecutable(exe);

            }
            catch (Exception)
            {
                logger.Writer(LogGrade.fatal, "SyringeDebugger.RetrieveInfo(): Failed to open the executable!");
                throw;
            }
            //if (!pImGetProcAddress || !pImLoadLibrary)
            {
                logger.Writer( LogGrade.error,
                    "SyringeDebugger.RetrieveInfo(): ERROR: Either a LoadLibraryA or a GetProcAddress import could not be found!");

                //throw_lasterror_or(ERROR_PROC_NOT_FOUND, exe);
            }

            // read meta information: size and checksum
            if (ifstream is { exe, ifstream::binary })
            {

        is.seekg(0, ifstream::end);
                dwExeSize = static_cast<DWORD>(is.tellg());

        is.seekg(0, ifstream::beg);

                CRC32 crc;
                char buffer[0x1000];
                while (auto const read = is.read(buffer, std::size(buffer)).gcount()) {
                    crc.compute(buffer, read);
                }
                dwExeCRC = crc.value();
            }

            logger.Writer(LogGrade.info, "SyringeDebugger.RetrieveInfo(): Executable information successfully retrieved.");
            logger.Writer(LogGrade.msg, "exe = %s", exe.c_str());
            logger.Writer(LogGrade.msg, "pImLoadLibrary = 0x%08X", pImLoadLibrary);
            logger.Writer(LogGrade.msg, "pImGetProcAddress = 0x%08X", pImGetProcAddress);
            logger.Writer(LogGrade.msg, "pcEntryPoint = 0x%08X", pcEntryPoint);
            logger.Writer(LogGrade.msg, "dwExeSize = 0x%08X", dwExeSize);
            logger.Writer(LogGrade.msg, "dwExeCRC = 0x%08X", dwExeCRC);
            logger.Writer(LogGrade.msg, "dwTimestamp = 0x%08X", dwTimeStamp);

            logger.Writer(LogGrade.info, "SyringeDebugger.RetrieveInfo(): Opening %s to determine imports.", exe.c_str());
        }
        //*/
        #endregion

        public SyringeDebugger(string filename)
        {
            exe = filename;
            //RetrieveInfo();
        }
        public void FindDLLs()
        {
            /*
            std::string_view const fn(file->cFileName);
            PortableExecutable const DLL{ fn };
            HookBuffer buffer;

            auto canLoad = false;
            if (auto const hooks = DLL.FindSection(".syhks00")) {
                canLoad = ParseHooksSection(DLL, *hooks, buffer);
            } else
            {
                canLoad = ParseInjFileHooks(fn, buffer);
            }

            if (canLoad)
            {
                Log::WriteLine(
                    __FUNCTION__ ": Recognized DLL: \"%.*s\"", printable(fn));

                if (auto const res = Handshake(
                     DLL.GetFilename(), static_cast<int>(buffer.count),
                     buffer.checksum.value()))
				{
                    canLoad = *res;
                } else if (auto const hosts = DLL.FindSection(".syexe00")) {
                    canLoad = CanHostDLL(DLL, *hosts);
                }
            }

            if (canLoad)
            {
                for (auto const&it : buffer.hooks) {
                    auto const eip = it.first;
                    auto & h = Breakpoints[eip];
                    h.p_caller_code.clear();
                    h.original_opcode = 0x00;
                    h.hooks.insert(
                        h.hooks.end(), it.second.begin(), it.second.end());
                }
            }
            else if (!buffer.hooks.empty())
            {
                Log::WriteLine(
                    __FUNCTION__ ": DLL load was prevented: \"%.*s\"",
                    printable(fn));
            }
            */
            DirectoryInfo folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            
                foreach (FileInfo file in folder.GetFiles("*.dll"))
                {
                    
                }
        }
    }
    class PortableExecutable
    {
        #region Private
        private string Filename;
        //private IMAGE_DOS_HEADER uDOSHeader;
        //private IMAGE_NT_HEADERS uPEHeader;
        #endregion
        public PortableExecutable(string exe)
        {

        }
    }


    [Serializable]
    public class InvalidArgumentsException : Exception
    {
        public InvalidArgumentsException() { }
        public InvalidArgumentsException(string message) : base(message) { }
        public InvalidArgumentsException(string message, Exception inner) : base(message, inner) { }
        protected InvalidArgumentsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// 日志管理类
    /// </summary>
    public class Logger
    {
        private string LogPath { get => Logname + ".log"; }
        private string Logname = @"Syringe";

        #region 内部方法
        private void writerHeader(LogGrade grade)
        {
            string logGrade = gradeSwitch(grade);
            string logTime = DateTime.Now.ToLongTimeString();
            writer("-----------+-------------------------------------------");
            writer(" " + logGrade + "\t|\t" + logTime);
        }
        private void writerMsg(string msg)
        {
            writer("\t| Message :\r\n\t|  " + msg);
        }
        private void writerMsg(Exception e)
        {
            writer(string.Format("\t| Exception:\r\n\t|  {0} : {1}", e.GetType().FullName, e.Message));
            writer("\t| Source:\r\n\t|  " + e.Source);
            writer("\t| TargetSite:\r\n\t|  " + e.TargetSite);
            writer("\t| StackTrace:\r\n\t|" + e.StackTrace.Replace("\n", "\n\t|"));
            writer("\t| Data:\r\n\t|  " + e.Data);
        }
        private void writerMsg(string msg, Exception e)
        {
            writerMsg(msg);
            writerMsg(e);
        }
        private void writer(string str)
        {
            try
            {
                TextWriter textWriter = new StreamWriter(LogPath, true);
                textWriter.WriteLine(str);
                textWriter.Close();
            }
            catch (UnauthorizedAccessException e)
            {
                Clean();
                writerMsg(e);
            }
        }
        private string gradeSwitch(LogGrade grade)
        {
            switch (grade)
            {
                case LogGrade.msg:
                    return "";
                case LogGrade.info:
                    return "Info";
                case LogGrade.trace:
                    return "Trace";
                case LogGrade.debug:
                    return "Debug";
                case LogGrade.warn:
                    return "Warn";
                case LogGrade.error:
                    return "Error";
                case LogGrade.fatal:
                    return "Fatal";
                default:
                    return "";
            }
        }
        #endregion
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="grade">日志等级</param>
        /// <param name="Msg">信息</param>
        public void Writer(LogGrade grade, string Msg)
        {
            writerHeader(grade);
            writerMsg(Msg);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="grade">日志等级</param>
        /// <param name="e">异常</param>
        public void Writer(LogGrade grade, Exception e)
        {
            writerHeader(grade);
            writerMsg(e);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="grade">日志等级</param>
        /// <param name="msg">信息</param>
        /// <param name="e">异常</param>
        public void Writer(LogGrade grade, string msg, Exception e)
        {
            writerHeader(grade);
            writerMsg(msg, e);
        }
        /// <summary>
        /// 清除日志记录
        /// </summary>
        public void Clean()
        {
            File.WriteAllText(LogPath,
                "\t| " + DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString() + "\r\n" +
                "\t| Crape Launcher Is Working!\r\n");
        }
        /// <summary>
        /// 构造方法 实例化日志管理功能
        /// </summary>
        public Logger()
        {
        }
    }
    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogGrade
    {
        /// <summary>
        /// 无标记消息
        /// </summary>
        msg,
        /// <summary>
        /// 消息
        /// </summary>
        info,
        /// <summary>
        /// 追踪
        /// </summary>
        trace,
        /// <summary>
        /// 调试
        /// </summary>
        debug,
        /// <summary>
        /// 警告
        /// </summary>
        warn,
        /// <summary>
        /// 错误
        /// </summary>
        error,
        /// <summary>
        /// 致命错误
        /// </summary>
        fatal
    }
    /// <summary>
    /// 错误提示消息框类
    /// </summary>
    public class ErrorBox
    {
        /// <summary>
        /// 返回异常信息字符串的方法
        /// </summary>
        /// <param name="e">异常</param>
        /// <returns>转换的字符串</returns>
        private string getErrorMsg(Exception e)
        {
            return
                "\nTargetSite\t: " + e.TargetSite +
                "\nMessage\t: " + e.Message +
                "\nSource\t: " + e.Source +
                "\nPlease Show the Log";
        }
        /// <summary>
        /// 用来判断是否结束应用的内部方法
        /// </summary>
        /// <param name="logGrade">日志等级</param>
        /// <returns>是否关闭程序</returns>
        private static bool exitSwitch(LogGrade logGrade)
        {
            switch (logGrade)
            {
                case LogGrade.msg:
                case LogGrade.info:
                case LogGrade.trace:
                case LogGrade.debug:
                case LogGrade.warn:
                case LogGrade.error:
                    return false;
                case LogGrade.fatal:
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// 弹出错误窗口 并关闭程序
        /// </summary>
        public ErrorBox() : this(null, null, true)
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="close">弹出窗口后是否关闭程序</param>
        public ErrorBox(bool close) : this(null, null, close)
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="message">错误窗口额外信息</param>
        public ErrorBox(string message) : this(message, null, false)
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="e">错误窗口输出简要异常信息</param>
        public ErrorBox(Exception e) : this(null, e, false)
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="logGrade">日志错误等级 用来判断是否关闭程序</param>
        public ErrorBox(LogGrade logGrade) : this(null, null, exitSwitch(logGrade))
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="message">错误窗口额外信息</param>
        /// <param name="e">错误窗口输出简要异常信息</param>
        public ErrorBox(string message, Exception e) : this(message, e, false)
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="message">错误窗口额外信息</param>
        /// <param name="logGrade">日志错误等级 用来判断是否关闭程序</param>
        public ErrorBox(string message, LogGrade logGrade) : this(message, null, exitSwitch(logGrade))
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="e">错误窗口输出简要异常信息</param>
        /// <param name="logGrade">日志错误等级 用来判断是否关闭程序</param>
        public ErrorBox(Exception e, LogGrade logGrade) : this(null, e, exitSwitch(logGrade))
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="message">错误窗口额外信息</param>
        /// <param name="close">弹出窗口后是否关闭程序</param>
        public ErrorBox(string message, bool close) : this(message, null, close)
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="e">错误窗口输出简要异常信息</param>
        /// <param name="close">弹出窗口后是否关闭程序</param>
        public ErrorBox(Exception e, bool close) : this(null, e, close)
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="message">错误窗口额外信息</param>
        /// <param name="e">错误窗口输出简要异常信息</param>
        /// <param name="logGrade">日志错误等级 用来判断是否关闭程序</param>
        public ErrorBox(string message, Exception e, LogGrade logGrade) : this(message, e, exitSwitch(logGrade))
        {
        }
        /// <summary>
        /// 弹出错误窗口
        /// </summary>
        /// <param name="message">错误窗口额外信息</param>
        /// <param name="e">错误窗口输出简要异常信息</param>
        /// <param name="close">弹出窗口后是否关闭程序</param>
        public ErrorBox(string message, Exception e, bool close)
        {
            string msg =
                "            Crape Client has encountered an Internal Error\n" +
                "                  and is unable to continue normally.\n\n" +
                "Please Visit our website at https://github.com/frg2089/Crape-Client" + "\n" +
                "                   Or Mail To frg2089@foxmail.com\n" +
                "                for the latest updates and technical.\n";
            if (message != null)
                msg += "\nMessage :" + message;
            if (e != null)
                msg += getErrorMsg(e);
            System.Windows.MessageBox.Show(msg, "Internal Error!");
            if (close) Environment.Exit(0);
        }
    }
}
