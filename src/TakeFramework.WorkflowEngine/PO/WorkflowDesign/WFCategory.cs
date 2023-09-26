using TakeFramework.Domain.Entities;

namespace TakeFramework.WorkflowEngine.PO.WorkflowDesign
{
    /// <summary>
    /// 表单分类
    /// </summary>
    public class WFCategory : FullAuditEntity
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
        /// 父级id
        /// </summary>
        public long? ParentId { get; set; }
    }
}
