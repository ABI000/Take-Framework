using TakeFramework.Domain.Entities;

namespace TakeFramework.WorkflowEngine.PO.Instances
{
    /// <summary>
    /// 流程实例表单编码
    /// </summary>
    public class WFInstanceFormSchema : FullAuditEntity
    {
        /// <summary>
        /// 流程实例
        /// </summary>
        public long WFInstanceId { get; set; }
        /// <summary>
        /// 流程表单id
        /// </summary>
        public long WFFormId { get; set; }
        /// <summary>
        /// 表单Schema
        /// </summary>
        public string Content { get; set; } = string.Empty;

    }
}
