namespace TakeFramework.WorkflowEngine.Enums
{

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum EnumTaskStatus
    {
        /// <summary>
        /// 未开始
        /// </summary>
        NotStarted = 0,
        /// <summary>
        /// 处理中（进行中）
        /// </summary>
        Processing = 1,
        /// <summary>
        /// 同意
        /// </summary>
        Agree = 2,
        /// <summary>
        /// 拒绝
        /// </summary>
        Rejected = 3,
    }
}