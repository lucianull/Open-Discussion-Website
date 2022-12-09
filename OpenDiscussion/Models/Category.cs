using System.ComponentModel.DataAnnotations;

namespace OpenDiscussion.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
