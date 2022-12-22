using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenDiscussion.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayName { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }

        public virtual ICollection<Discussion>? Discussions { get; set; }

        public virtual Profile? Profile { get; set; }   

        [NotMapped]
        public int? CommentCount { get; set; }

        [NotMapped]
        public int? DiscussionCount { get; set; }
    }
}
