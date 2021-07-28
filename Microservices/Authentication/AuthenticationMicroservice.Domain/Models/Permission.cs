using System.ComponentModel.DataAnnotations;

namespace AuthenticationMicroservice.Domain.Models
{
    public class Permission : Base.Entity
    {
        public Permission() : base()
        {
        }

        [Required]
        [MaxLength(254)]
        public string Name { get; set; }
    }
}
