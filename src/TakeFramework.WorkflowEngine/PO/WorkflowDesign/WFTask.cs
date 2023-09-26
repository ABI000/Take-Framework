using TakeFramework.WorkflowEngine.Enums;
using TakeFramework.WorkflowEngine.PO.Base;

namespace TakeFramework.WorkflowEngine.PO.WorkflowDesign
{
    /// <summary>
    /// 流程父类
    /// </summary>
    public class WFTask : BaseTask
    {
        /// <summary>
        /// 分配人员类型
        /// </summary>
        public EnumAssigneType AssigneType { get; set; }
        /// <summary>
        /// 执行人规则
        /// </summary>
        public string AssigneRule { get; set; } = string.Empty;
        /// <summary>
        /// 操作
        /// </summary>
        public string Operations { get; set; } = string.Empty;
        /// <summary>
        /// 跳过重复审批
        /// </summary>
        public bool SkipDuplicateApproval { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        public EnumTaskType TaskType { get; set; }
        /// <summary>
        /// 分支步骤id
        /// </summary>
        public int BranchNextStepId { get; set; }
        /// <summary>
        /// 分支条件
        /// </summary>
        public string BranchingConditions { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

    }
}
