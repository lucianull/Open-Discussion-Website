using System.ComponentModel.DataAnnotations;

namespace OpenDiscussion.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set;}
        public int DiscussionId { get; set;}
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

    }
}
