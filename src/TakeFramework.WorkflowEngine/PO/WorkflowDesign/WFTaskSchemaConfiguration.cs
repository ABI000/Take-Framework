using TakeFramework.Domain.Entities;

namespace TakeFramework.WorkflowEngine.PO.WorkflowDesign
{
    /// <summary>
    /// 表单配置表
    /// </summary>
    public class WFTaskSchemaConfiguration : FullAuditEntity
    {
        /// <summary>
        /// 流程任务id
        /// </summary>
        public long WFTaskId { get; set; }
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
    }
}
