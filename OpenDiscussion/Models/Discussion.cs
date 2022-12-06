using System.ComponentModel.DataAnnotations;

namespace OpenDiscussion.Models
{
    public class Discussion
    {
        [Key]
        public int DiscussionId { get; set; }
        public int TopicId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
