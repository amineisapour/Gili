namespace LoggingMicroservice.Domain.Models.Base
{
    public abstract class Entity : GiliX.Domain.IEntity
    {
        protected Entity() : base()
        {
            Id = System.Guid.NewGuid();
            RegisterDateTime = System.DateTime.Now;
            IsActive = true;
        }

        public System.Guid Id { get; set; }
        public System.DateTime RegisterDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}
