namespace TakeFramework.Domain.Entities
{
    public class BaseEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }
    }
}