namespace TakeFramework.Domain.Entities
{
    public interface IEntity
    {

    }
    public interface IEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
