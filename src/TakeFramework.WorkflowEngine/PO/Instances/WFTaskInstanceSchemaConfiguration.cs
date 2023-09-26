using TakeFramework.Domain.Entities;

namespace TakeFramework.WorkflowEngine.PO.Instances
{
    /// <summary>
    /// 流程实例表单配置
    /// </summary>
    public class WFTaskInstanceSchemaConfiguration : FullAuditEntity
    {
        /// <summary>
        /// 流程任务实例id
        /// </summary>
        public long WFTaskInstanceId { get; set; }

        /// <summary>
        /// 数据库字段名
        /// </summary>
        public string DBIndex { get; set; } = string.Empty;
        /// <summary>
        /// 字段在当前任务的权限
        /// </summary>
        public string Permission { get; set; } = string.Empty;
        /// <summary>
        /// 组件id
        /// </summary>
        public string DesignableId { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 流程任务id
        /// </summary>
        public long WFTaskId { get; set; }
    }
}
