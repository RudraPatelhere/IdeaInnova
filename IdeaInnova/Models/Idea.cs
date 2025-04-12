using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaInnova.Models
{
    public class Idea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }  // Nullable

        [Required]
        public string? Description { get; set; }  // Nullable

        public int Votes { get; set; } = 0;

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public int? UserId { get; set; } // FK to User
        [ForeignKey("UserId")]
        public User? User { get; set; }

    }
}