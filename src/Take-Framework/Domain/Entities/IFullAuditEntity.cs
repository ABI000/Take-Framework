namespace TakeFramework.Domain.Entities
{
    public interface IFullAuditEntity<TUserId> : ICreateAuditEntity<TUserId>, IModifiedAuditEntity<TUserId>, ISoftDelete, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TUserId? DeleteBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        public virtual void InIitSoftDelete(TUserId userId)
        {
            this.Deleted = true;
            this.DeleteTime = DateTime.Now;
            this.DeleteBy = userId;
        }
    }
}
