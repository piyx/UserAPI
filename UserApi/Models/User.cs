using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "First Name cannot be more than 50 characters long")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Last Name cannot be more than 50 characters long")]
        public string LastName { get; set; }

        //public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age()
        {
            DateTime now = DateTime.Now;
            DateTime birthDate = DateOfBirth;
            int age = now.Year - DateOfBirth.Year;

            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
