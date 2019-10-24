using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLauncher.LogMgr
{
    /// <summary>
    /// 日志管理类
    /// </summary>
    class Logger
    {
        private string LogPath { get => Logname + ".log"; }
        private string Logname= @"Crape_Launcher";

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
