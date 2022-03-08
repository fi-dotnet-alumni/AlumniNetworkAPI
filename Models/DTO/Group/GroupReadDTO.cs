namespace AlumniNetworkAPI.Models.DTO.Group
{
    public class GroupReadDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool isPrivate { get; set; }
        public List<int>? Users { get; set; }
        public List<int>? Posts { get; set; }
    }
}
