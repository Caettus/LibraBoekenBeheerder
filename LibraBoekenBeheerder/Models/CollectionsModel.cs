using System.ComponentModel.DataAnnotations;

namespace LibraBoekenBeheerder.Models;

public class CollectionsModel
{
    [Key]
    public int CollectionID { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
}