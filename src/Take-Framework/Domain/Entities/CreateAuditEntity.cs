namespace TakeFramework.Domain.Entities
{
    public class CreateAuditEntity<TPrimaryKey, TUserId> : BaseEntity<TPrimaryKey>, IEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TUserId CreatedBy { get; set; }


        public virtual void InIit(TUserId userId)
        {
            CreatedBy = userId;
            Created = DateTime.Now;
        }
    }
}