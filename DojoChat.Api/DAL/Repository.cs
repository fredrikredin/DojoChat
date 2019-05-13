using System;
using System.Collections.Generic;
using System.Linq;
using DojoChat.Api.Model;

namespace DojoChat.Api.DAL
{
    public static class Repository
    {
        private static readonly Object obj = new Object();
        private static List<Message> _messages;

        static Repository()
        {
            _messages = new List<Message>();
        }

        public static IEnumerable<Message> GetMessages()
        {
            lock (obj)
            {
                return _messages;
            }
        }

        public static Message GetMessage(int id)
        {
            lock (obj)
            {
                return _messages.FirstOrDefault(o => o.Id == id);
            }
        }

        public static Message AddMessage(Message message)
        {
            lock (obj)
            {
                if (String.IsNullOrWhiteSpace(message.User))
                    throw new ArgumentNullException("message user must be specified");

                if (String.IsNullOrWhiteSpace(message.Text))
                    throw new ArgumentNullException("message text must be specified");

                message.Id = _messages.Count + 1;
                message.Created = DateTime.Now;
                _messages.Add(message);
                return message;
            }
        }
    }
}