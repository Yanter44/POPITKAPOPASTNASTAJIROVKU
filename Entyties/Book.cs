using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace POPITKAPOPASTNASTAJIROVKU.Entyties
{
    public class Book
    {
        [Key]
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; } 
        public string Description { get; set; }
        public int ISBN { get; set; }

    }
}
