using TakeFramework.Domain.Entities;
using TakeFramework.WorkflowEngine.Enums;

namespace TakeFramework.WorkflowEngine
{
    /// <summary>
    /// 任务执行记录
    /// </summary>
    public class WFTaskRecord : FullAuditEntity
    {
        /// <summary>
        /// 流程实例Id
        /// </summary>
        public long WFInstanceId { get; set; }
        /// <summary>
        /// 流程任务实例id
        /// </summary>
        public long? WFTaskInstanceId { get; set; }
        /// <summary>
        /// 执行任务id
        /// </summary>
        public long? WFExecutionTaskId { get; set; }
        /// <summary>
        /// 任务名
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        public EnumTaskStatus Status { get; set; }
        /// <summary>
        /// 指定处理人
        /// </summary>
        public long AssignedPrincipal { get; set; }
        /// <summary>
        /// 指定处理人
        /// </summary>
        public string AssignedPrincipalName { get; set; }
        /// <summary>
        /// 任务发放的时刻
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 任务操作的时刻
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// 发起职位
        /// </summary>
        public long? InitiatePosition { get; set; }
        /// <summary>
        /// 执行记录类型
        /// </summary>
        public EnumTaskRecordType TaskRecordType { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string? BatchNumber { get; set; }
    }
}
