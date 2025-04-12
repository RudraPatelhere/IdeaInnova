using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaInnova.Models
{
    public class Idea
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        public string Description { get; set; }

        public int Votes { get; set; } = 0;

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public int? UserId { get; set; } // FK to User
        [ForeignKey("UserId")]
        public User? User { get; set; }

    }
}