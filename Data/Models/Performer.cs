namespace MusicHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Performer
    {

        [Key]
        public int Id { get; set; }


        [MinLength(3), MaxLength(20), Required]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(20), Required]
        public string LastName { get; set; }

        [Range(18, 70)]
        public int Age { get; set; }

        [Range(0, double.MaxValue)]
        public decimal NetWorth { get; set; }

        public ICollection<SongPerformer> PerformerSongs { get; set; }= new HashSet<SongPerformer>();





















        //public Performer()
        //{
        //    this.PerformerSongs = new HashSet<SongPerformer>();
        //}

        //public int Id { get; set; }

        //[Required]
        //[MinLength(3), MaxLength(20)]
        //public string FirstName { get; set; }

        //[Required]
        //[MinLength(3), MaxLength(20)]
        //public string LastName { get; set; }

        //[Range(18,70)]
        //public int Age { get; set; }

        //[Range(typeof(decimal), "0", "79228162514264337593543950335")]
        //public decimal NetWorth { get; set; }

        //public ICollection<SongPerformer> PerformerSongs { get; set; }
    }
}
