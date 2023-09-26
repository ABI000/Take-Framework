namespace TakeFramework.WorkflowEngine.Enums
{
    /// <summary>
    /// 任务审批方式
    /// </summary>
    public enum EnumTaskApprovalMethodType
    {
        /// <summary>
        /// 或签（抢占）
        /// </summary>
        OrSign = 0,
        /// <summary>
        /// 会签
        /// </summary>
        Sign = 1,
        /// <summary>
        /// 等待一个同意
        /// </summary>
        WaitAnResponse = 3
    }
}
