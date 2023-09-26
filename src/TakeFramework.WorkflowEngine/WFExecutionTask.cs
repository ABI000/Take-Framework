using TakeFramework.Domain.Entities;
using TakeFramework.WorkflowEngine.Enums;

namespace TakeFramework.WorkflowEngine
{
    /// <summary>
    /// 流程的执行任务
    /// </summary>
    public class WFExecutionTask : FullAuditEntity
    {
        /// <summary>
        /// 任务id
        /// 该字段为空，则为启动任务
        /// </summary>
        public long? WFTaskId { get; set; }
        /// <summary>
        /// 任务实例id
        /// </summary>
        public long WFTaskInstanceId { get; set; }
        /// <summary>
        /// 流程实例Id
        /// </summary>
        public long WFInstanceId { get; set; }
        /// <summary>
        /// 流程id
        /// </summary>
        public long WorkflowId { get; set; }
        /// <summary>
        /// 指定处理人
        /// </summary>
        public long AssignedPrincipal { get; set; }
        /// <summary>
        /// 执行任务
        /// </summary>
        public EnumWFExecutionTaskType WFExecutionTaskType { get; set; }
        /// <summary>
        /// 执行任务
        /// </summary>
        public EnumWFExecutionTaskStatus Status { get; set; }
        /// <summary>
        /// 执行任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 申请人职位
        /// </summary>
        public long? InitiatePosition { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNumber { get; set; }
        /// <summary>
        /// 批次次序
        /// </summary>
        public int BatchSort { get; set; }

    }
}
