namespace TakeFramework.Domain.Entities
{
    public class AuditEntity<TPrimaryKey, TUserId> : CreateAuditEntity<TPrimaryKey, TUserId>, IEntity
    {
       
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Modified { get; set; }
        /// <summary>
        /// 修改该实体的用户
        /// </summary>
        public TUserId? ModifiedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Deleted { get; set; }     

        public void InIntModified(TUserId userId)
        {
            ModifiedBy = userId;
            Modified = DateTime.Now;
        }
    }
}