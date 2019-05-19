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

        public static IEnumerable<Message> GetChannelMessages(int channelId)
        {
            lock (obj)
            {
                return _messages.Where(o => o.ChannelId == channelId);
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
                message.Id = _messages.Count + 1;
                _messages.Add(message);
                return message;
            }
        }
    }
}