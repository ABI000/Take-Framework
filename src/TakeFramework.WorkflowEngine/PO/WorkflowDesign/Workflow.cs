using TakeFramework.WorkflowEngine.PO.Base;

namespace TakeFramework.WorkflowEngine.PO.WorkflowDesign
{
    /// <summary>
    /// 流程
    /// </summary>
    public class Workflow : Template
    {
        /// <summary>
        /// 流程表单分类Id
        /// </summary>
        public long WFCategoryId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 流水号规则
        /// </summary>
        public string SNRules { get; set; }

        /// <summary>
        /// 绑定业务表
        /// </summary>
        public string BusinessTableName { get; set; }
        /// <summary>
        /// 绑定表单
        /// </summary>
        public long WFFormId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}
