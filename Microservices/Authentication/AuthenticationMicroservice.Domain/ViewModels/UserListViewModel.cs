using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationMicroservice.Domain.ViewModels
{
    public class UserListViewModel
    {
        public UserListViewModel()
        {
        }

        public System.Guid Id { get; set; }
        public string Username { get; set; }
        public System.DateTime RegisterDateTime { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public Enums.Gender? Gender { get; set; }
        public System.DateTime? Birthdate { get; set; }
    }
}
