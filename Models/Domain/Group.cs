namespace AlumniNetworkAPI.Models.Domain
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool isPrivate { get; set; }
        //public ICollection<User>? Users { get; set; }
        //public ICollection<Post>? Posts { get; set; }
    }
}
