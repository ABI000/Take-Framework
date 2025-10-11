using TakeFramework.Domain.Entities;
using TakeFramework.WorkflowEngine.Enums;

namespace TakeFramework.WorkflowEngine.PO.Base
{
    /// <summary>
    /// Base class for workflow tasks.
    /// </summary>
    public class BaseTask : FullAuditEntity<long, long>
    {
        /// <summary>
        /// Workflow identifier.
        /// </summary>
        public long WorkflowId { get; set; }
        /// <summary>
        /// Task name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Step identifier.
        /// </summary>
        public int StepId { get; set; }
        /// <summary>
        /// Next step identifier.
        /// </summary>
        public int NextStepId { get; set; }
        /// <summary>
        /// Event identifier.
        /// </summary>
        public string EventId { get; set; } = string.Empty;
        /// <summary>
        /// Event name.
        /// </summary>
        public string EventName { get; set; } = string.Empty;
        /// <summary>
        /// Event data in JSON or string format.
        /// </summary>
        public string EventData { get; set; } = string.Empty;
        /// <summary>
        /// Task approval method type.
        /// </summary>
        public EnumTaskApprovalMethodType TaskApprovalMethodType { get; set; }
    }
}
