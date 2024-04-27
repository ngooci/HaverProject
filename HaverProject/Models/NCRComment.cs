namespace HaverProject.Models
{
    public class NCRComment
    {
        public int ID { get; set; } // Primary key for the comment
        public int NCRId { get; set; } // Foreign key referencing the NCR
        public string CommentText { get; set; } // Text content of the comment
        public DateTime CreatedAt { get; set; } // Timestamp of comment creation

    }
}
