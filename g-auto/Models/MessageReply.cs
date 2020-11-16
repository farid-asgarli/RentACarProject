using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class MessageReply
    {
        public int Id { get; set; }
        [MaxLength(1000),Required]
        public string Content { get; set; }

        public Message Message { get; set; }
        public int MessageId { get; set; }
        public DateTime PostDate { get; set; }
        public Admin Admin { get; set; }
        public int AdminId { get; set; }
        [Column(TypeName ="bit")]
        public bool isReadByTheUser { get; set; }

    }
}