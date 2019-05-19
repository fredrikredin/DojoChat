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

        // ??? maybe later create a post OpenChannel(string username, string passcode) returning a channel guid 
        [Required]
        [Range(1, int.MaxValue)]
        public int ChannelId { get; set; }
    }
}
