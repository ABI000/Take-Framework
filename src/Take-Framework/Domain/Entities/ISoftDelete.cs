namespace TakeFramework.Domain.Entities
{
    public interface ISoftDelete
    {
        public bool Deleted { get; set; }

        public virtual void InIitSoftDelete()
        {
            Deleted = true;
        }
    }
}
