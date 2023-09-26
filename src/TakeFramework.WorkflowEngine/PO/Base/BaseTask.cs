using TakeFramework.Domain.Entities;
using TakeFramework.WorkflowEngine.Enums;

namespace TakeFramework.WorkflowEngine.PO.Base
{
    /// <summary>
    /// 任务父类
    /// </summary>
    public class BaseTask : FullAuditEntity<long, long>
    {
        /// <summary>
        /// 流程id
        /// </summary>
        public long WorkflowId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 步骤id    
        /// </summary>
        public int StepId { get; set; }
        /// <summary>
        /// 下一步骤    
        /// </summary>
        public int NextStepId { get; set; }
        /// <summary>
        /// 事件id    
        /// </summary>
        public string EventId { get; set; } = string.Empty;
        /// <summary>
        /// 事件名称   
        /// </summary>
        public string EventName { get; set; } = string.Empty;
        /// <summary>
        /// 事件数据
        /// </summary>
        public string EventData { get; set; } = string.Empty;
        /// <summary>
        /// 审批方式枚举
        /// </summary>
        public EnumTaskApprovalMethodType TaskApprovalMethodType { get; set; }
    }
}
