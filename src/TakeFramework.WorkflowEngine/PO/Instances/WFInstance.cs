using TakeFramework.WorkflowEngine.Enums;
using TakeFramework.WorkflowEngine.PO.Base;

namespace TakeFramework.WorkflowEngine.PO.Instances
{
    /// <summary>
    /// 流程实例
    /// </summary>
    public class WFInstance : Template
    {
        /// <summary>
        /// 申请人职位
        /// </summary>
        public long? InitiatePosition { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        /// <summary>
        /// 流程分类id
        /// </summary>
        public long WFCategoryId { get; set; }
        /// <summary>
        /// 流程分类id
        /// </summary>
        public long WorkflowId { get; set; }
        /// <summary>
        /// SN
        /// </summary>
        public string SN { get; set; } = string.Empty;
        /// <summary>
        /// 流程状态
        /// </summary>
        public EnumWFStatus Status { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? ComplateTime { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public long Sponsor { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime InitiationTime { get; set; }
        /// <summary>
        /// 绑定业务表
        /// </summary>
        public string BusinessTableName { get; set; }
        /// <summary>
        /// 绑定业务表
        /// </summary>
        public string BusinessId { get; set; } = string.Empty;
        /// <summary>
        /// 任务实例，辅助字段
        /// </summary>

        public List<WFTaskInstance> WFTaskInstances { get; set; }
    }
}
