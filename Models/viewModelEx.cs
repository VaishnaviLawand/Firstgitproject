using Microsoft.AspNetCore.Authentication;

namespace WebApplication2.Models
{
    public class viewModelEx
    {
      public studinfo studinfo { get; set; }
      public StudAddr studAddr { get; set; }    
    }

    public class studinfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string EmailId { get; set; }
    }
    public class StudAddr
    {
        public string address{ get; set; }
        public string  city { get; set; }
    }
}
