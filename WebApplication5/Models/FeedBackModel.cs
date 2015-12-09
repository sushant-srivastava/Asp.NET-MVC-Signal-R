using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class FeedbackModel
    {
        [Key]
        public int FeedbackId { get; set; }
        public string Comments { get; set; }
    }
}
