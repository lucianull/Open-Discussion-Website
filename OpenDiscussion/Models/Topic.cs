using System.ComponentModel.DataAnnotations;

namespace OpenDiscussion.Models
{
    public class Topic
    {
        [Key]   
        public int TopicId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public int Name { get; set; }

        public virtual ICollection<Discussion> Discussions { get; set; }

    }
}
