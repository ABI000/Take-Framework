namespace TakeFramework.Domain.Entities
{
    public class FullEntity<TPrimaryKey, TUserId> : ModifiedAuditEntity<TPrimaryKey, TUserId>, IEntity<TPrimaryKey>, ISoftDelete
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
            InIitSoftDelete();
            this.DeleteTime = DateTime.Now;
            this.DeleteBy = userId;
        }

    }
}