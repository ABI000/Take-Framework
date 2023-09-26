using TakeFramework.Domain.Entities;
using TakeFramework.WorkflowEngine.Enums;

namespace TakeFramework.WorkflowEngine.PO.WorkflowDesign
{
    /// <summary>
    /// 流程任务操作权限
    /// </summary>
    public class WorkflowOperation : FullAuditEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 键值
        /// </summary>
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// 数据值
        /// </summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public EnumWFOperationType OperationType { get; set; }
    }
}
