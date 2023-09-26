using TakeFramework.WorkflowEngine.Enums;
using TakeFramework.WorkflowEngine.PO.WorkflowDesign;

namespace TakeFramework.WorkflowEngine.PO.Instances
{
    /// <summary>
    /// 流程实例表单
    /// </summary>

    public class WFTaskInstance : WFTask
    {
        /// <summary>
        /// 流程实例
        /// </summary>
        public long WFInstanceId { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public long? WFTaskId { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public EnumTaskStatus Status { get; set; }

        /// <summary>
        /// 限期
        /// </summary>
        public DateTime? DeadLine { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }
        /// <summary>
        /// 批次次序
        /// </summary>
        public int BatchSort { get; set; }

        /// <summary>
        /// 辅助
        /// </summary>
        public WFInstance WFInstance { get; set; }
        /// <summary>
        /// 辅助
        /// </summary>
        public List<WFExecutionTask> WFExecutionTasks { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class AssigneRuleStructureByJobTitle
    {
        public long JobTitleId { get; set; }
        public long OrganizationId { get; set; }
    }
}
