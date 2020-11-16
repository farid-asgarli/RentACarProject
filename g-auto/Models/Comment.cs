using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g_auto.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public User User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public int UpvoteCount { get; set; }
        public Blog Blog { get; set; }

        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public List<Reply> Replies { get; set; }

    }
}