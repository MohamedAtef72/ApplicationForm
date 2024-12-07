using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Models
{
    public class Hobbie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string HobbyName1 { get; set; }
        public string HobbyName2 { get; set; }
        public string HobbyName3 { get; set; }
        public string HobbyName4 { get; set; }

        [ForeignKey("User")] 
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
