using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
