using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OjVolunteer.Common.LogWriter
{
    public class LogHelper
    {
        //错误日志队列
        public static Queue<string> ExceptionStringQueue = new Queue<string>();
        //日志记录对象队列 如数据库 Log4Net Text等
        public static List<ILogWriter> LogWriterList = new List<ILogWriter>();
        static LogHelper() {
            //观察者模式应用 日志队列中的信息可能写入数据库或日志文件等不确认或同时写入多个目标中。
            //向日志记录对象队列中添加  Log4NetWriter对象
            LogWriterList.Add(new Log4NetWriter());
            //添加对象实例 注意：以下方法均未实现
            //LogWriterList.Add(new TextFileWriter());
            //LogWriterList.Add(new SqlServerWriter());

            //开启线程 让日志队列中的信息按顺序写入日志文件和数据库等中
            ThreadPool.QueueUserWorkItem(o =>
            {
                while (true)
                {
                    if (ExceptionStringQueue.Count > 0)
                    {
                        lock (ExceptionStringQueue)
                        {
                            String str = ExceptionStringQueue.Dequeue();
                            foreach (var logWriter in LogWriterList)
                            {
                                logWriter.WriteLogInfo(str);
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(300);
                    }
                }
            });

        }


        /// <summary>
        /// 将错误日志添加到日志队列中
        /// </summary>
        /// <param name="exceptionText">错误日志内容</param>
        public static void WriteLog(string exceptionText)
        {
            lock (ExceptionStringQueue) {
                ExceptionStringQueue.Enqueue(exceptionText);
            }
        }
    }
}
