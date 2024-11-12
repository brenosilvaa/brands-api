namespace AuthService.Domain.VMs.Users
{
    public class UserVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
