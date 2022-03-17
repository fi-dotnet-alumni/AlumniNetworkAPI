namespace AlumniNetworkAPI.Models.DTO.User
{
	public class UserReadDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public string Status { get; set; }
        public string Bio { get; set; }
        public string FunFact { get; set; }
        public ICollection<int> Topics { get; set; }
        public ICollection<int> Groups { get; set; }
    }
}

