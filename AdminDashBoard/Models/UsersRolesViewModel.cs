namespace AdminDashBoard.Models
{
    public class UsersRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<EditRoleViewModel> Roles{ get; set; }
    }
}
