using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaInnova.Models
{
    // Represents an idea submitted in the IdeaInnova system
    public class Idea
    {
        [Key]      // Marks this property as the primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "A title is required.")]  //Title must be provided, otherwise, show error
        public string Title { get; set; }

        [Required(ErrorMessage = "A description is required.")]   //Description must be provided with error message
        public string Description { get; set; }

        // Tracks number of votes for idea, initialized to 0
        public int Votes { get; set; } = 0;

        //Records when the idea was submitted
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        //Optional foreign key linking idea of user
        public int? UserId { get; set; } 

        //Navigation property for the associated User entity.
        [ForeignKey("UserId")]
        public User? User { get; set; }

    }
}