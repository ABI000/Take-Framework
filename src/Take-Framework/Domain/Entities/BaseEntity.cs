namespace TakeFramework.Domain.Entities
{
    public class BaseEntity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual required TPrimaryKey Id { get; set; }
    }
}