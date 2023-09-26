using TakeFramework.Domain.Entities;

namespace TakeFramework.WorkflowEngine.PO.Base
{
    /// <summary>
    /// 流程父类
    /// </summary>
    public class Template : FullAuditEntity<long,long>
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
        /// 版本
        /// </summary>
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
