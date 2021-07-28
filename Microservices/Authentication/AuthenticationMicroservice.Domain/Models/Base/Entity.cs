using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationMicroservice.Domain.Models.Base
{
    public abstract class Entity : GiliX.Domain.IEntity
    {
        protected Entity() : base()
        {
            Id = System.Guid.NewGuid();
            IsActive = true;
            RegisterDateTime = DateTime.Now;
        }

        [Required, Key] 
        public System.Guid Id { get; set; }
        
        [Required]
        public DateTime RegisterDateTime { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
