using System;
using System.ComponentModel.DataAnnotations;

namespace DojoChat.Api.Model
{
    public class Message
    {
        public int Id { get; set; }

        public DateTime Created { get; private set; } = DateTime.Now;

        [Required]
        public string User { get; set; }

        [Required]
        [StringLength(256)]
        public string Text { get; set; }

        [Required]
        [Range(1, 65536)]
        public int ChannelId { get; set; } = 1;
    }
}
