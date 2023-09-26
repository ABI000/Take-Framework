namespace TakeFramework.WorkflowEngine.Enums
{
    /// <summary>
    /// 流程状态
    /// </summary>
    public enum EnumWFStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Drafted = 0,
        /// <summary>
        /// 进行中
        /// </summary>
        Processing = 1,
        /// <summary>
        /// 同意
        /// </summary>
        Agree = 2,
        /// <summary>
        /// 拒绝？？需求未明确
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// 撤销
        /// 所有相关节点和表单将被冻结，不可更改
        /// </summary>
        Revocation = 4,
    }
}
