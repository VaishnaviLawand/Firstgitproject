using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class UpdateModel
    {
       public int Id { get; set; }
        public int RollNo { get; set; }

      
        public string Name { get; set; }


        
        public string EmailId { get; set; }
        public string Password { get; set; }

        public DateTime dob { get; set; }
        
        public string Mobile { get; set; }
        public string gender { get; set; }

        public double Fee { get; set; }
        public string dept { get; set; }
        public bool status { get; set; }

    }
}
