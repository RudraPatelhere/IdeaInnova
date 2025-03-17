using System;
using System.ComponentModel.DataAnnotations;

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

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}
