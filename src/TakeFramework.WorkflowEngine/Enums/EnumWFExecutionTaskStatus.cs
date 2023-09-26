namespace TakeFramework.WorkflowEngine.Enums
{
    /// <summary>
    /// 流程执行任务状态
    /// </summary>
    public enum EnumWFExecutionTaskStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NotStarted = 0,
        /// <summary>
        /// 系统处理
        /// </summary>
        SystemProcessing = 1,
        /// <summary>
        /// 同意
        /// </summary>
        Agree = 2,
        /// <summary>
        /// 拒绝
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// 取回
        /// </summary>
        Retrieve = 4,
        /// <summary>
        /// 退回
        /// </summary>
        Refunded = 5,
    }
}
