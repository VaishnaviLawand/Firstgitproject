using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
 
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Roll no is required")]
        public int RollNo { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }
        public string Password { get; set; }

        public DateTime dob { get; set; }
        [RegularExpression(@"^(\+\d{1, 2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-] ?\d{4}$", ErrorMessage = "The PhoneNumber field is not a valid phone number .")]
        public string Mobile { get; set; }
        public string gender { get; set; }

        public double Fee{ get; set; }
        public string dept { get; set; }
        public bool status { get; set; }



    }
}
