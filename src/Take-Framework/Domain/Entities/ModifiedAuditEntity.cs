namespace TakeFramework.Domain.Entities
{
    public class ModifiedAuditEntity<TPrimaryKey, TUserId> : CreateAuditEntity<TPrimaryKey, TUserId>, IEntity, ISoftDelete
    {

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? Modified { get; set; }
        /// <summary>
        /// 修改该实体的用户
        /// </summary>
        public TUserId? ModifiedBy { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool Deleted { get; set; }

        public virtual void InIitModified(TUserId userId)
        {
            ModifiedBy = userId;
            Modified = DateTime.Now;
        }
        public virtual void InIitSoftDelete()
        {
            Deleted = true;
        }
    }
}