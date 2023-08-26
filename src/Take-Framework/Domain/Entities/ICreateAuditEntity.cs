namespace TakeFramework.Domain.Entities
{
    public interface ICreateAuditEntity<TUserId>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public TUserId CreatedBy { get; set; }


        public virtual void InIit(TUserId userId)
        {
            CreatedBy = userId;
            Created = DateTime.Now;
        }
    }
}
