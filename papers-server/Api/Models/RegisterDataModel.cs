namespace Papers.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterDataModel
    {
        [Required, Phone]
        public string Phone { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Password { get; set; }
        public string LastName { get; set; }
    }
}
