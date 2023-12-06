using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POPITKAPOPASTNASTAJIROVKU.Entyties
{
    public class Book
    {
       [Key]
       [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Description { get; set; }
        public int ISBN { get; set; }

    }
}
