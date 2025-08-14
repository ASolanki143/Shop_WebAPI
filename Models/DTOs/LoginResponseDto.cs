namespace MyWebApiApp.Models.DTOs
{
    public class LoginResponseDto
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}