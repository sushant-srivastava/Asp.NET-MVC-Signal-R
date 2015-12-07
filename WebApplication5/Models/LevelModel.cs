using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class LevelModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public int LevelId { get; set; }
        public string Question { get; set; }
        [Required]
        [Display(Name = "Your Answer Here")]
        public string Answer { get; set; }
        public string Hint { get; set; }
        public int DifficultyId { get; set; }
        public virtual DifficultyModel Difficulty { get; set; }
    }
}
