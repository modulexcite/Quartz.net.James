using System;

namespace Quartz.Net.Demo
{
    /// <summary>
    /// 实现IJob接口
    /// </summary>
    public class DemoJob1 : IJob
    {
        //使用Common.Logging.dll日志接口实现日志记录
        private static readonly Common.Logging.ILog logger = Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Info("DemoJob1 任务开始运行");

                for (int i = 0; i < 10; i++)
                {
                    logger.InfoFormat("DemoJob1 正在运行{0}", i);
                }

                logger.Info("DemoJob1任务运行结束");
            }
            catch (Exception ex)
            {
                logger.Error("DemoJob2 运行异常", ex);
            }

        }

        #endregion
    }
}
