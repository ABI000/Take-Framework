namespace TakeFramework.Domain.Entities
{
    public class FullAuditEntity<TPrimaryKey, TUserId> : ModifiedAuditEntity<TPrimaryKey, TUserId>, IEntity<TPrimaryKey>, IFullAuditEntity<TUserId>
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TUserId? DeleteBy { get; set; }

    }
}