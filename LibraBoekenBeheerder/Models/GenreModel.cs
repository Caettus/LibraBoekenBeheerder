using System.ComponentModel.DataAnnotations;

namespace LibraBoekenBeheerder.Models
{
    public class GenreModel
    {
        [Key]
        public int GenreId { get; set; }

        [StringLength(50)]
        public string GenreName { get; set; }
    }
}
