using TakeFramework.Domain.Entities;

namespace TakeFramework.WorkflowEngine.PO.WorkflowDesign
{
    /// <summary>
    /// 流程表单
    /// </summary>
    public class WFForm : FullAuditEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 流程表单分类Id
        /// </summary>
        public long WFFormCategoryId { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// 表单Schema
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
