namespace TakeFramework.Domain.Entities
{
    public interface IModifiedAuditEntity<TUserId>
    {
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? Modified { get; set; }
        /// <summary>
        /// 修改该实体的用户
        /// </summary>
        public TUserId? ModifiedBy { get; set; }


        public virtual void InIitModified(TUserId userId)
        {
            ModifiedBy = userId;
            Modified = DateTime.Now;
        }
    }
}
