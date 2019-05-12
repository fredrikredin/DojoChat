using System;

namespace DojoChat.Api.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}
