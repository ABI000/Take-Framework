namespace TakeFramework.Domain.Entities
{
    public class FullEntity<TPrimaryKey, TUserId> : AuditEntity<TPrimaryKey, TUserId>, IEntity<TPrimaryKey>, ISoftDelete
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Delete { get; set; }

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
        public void SoftDelete(TUserId userId)
        {
            this.Delete = true;
            this.DeleteTime = DateTime.Now;
            this.DeleteBy = userId;
        }

    }
}