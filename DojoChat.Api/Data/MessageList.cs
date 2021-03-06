using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoChat.Api.Model;

namespace DojoChat.Api.Data
{
    public static class MessageList
    {
        private static readonly Object obj = new Object();
        private static List<Message> _messages;

        static MessageList()
        {
            _messages = new List<Message>();
        }

        public static Task<IEnumerable<Message>> GetMessagesForChannelAsync(int channelId)
        {
            return Task.Run(() => 
            {
                lock (obj)
                {
                    return _messages.Where(o => o.ChannelId == channelId);
                }
            });
        }

        public static Task<Message> GetMessageAsync(int id)
        {
            return Task.Run(() =>
            {
                lock (obj)
                {
                    return _messages.FirstOrDefault(o => o.Id == id);
                }
            });
        }

        public static Task<IEnumerable<int>> GetChannelIds()
        {
            return Task.Run(() => 
            {
                lock(obj)
                {
                    return _messages.Select(o => o.ChannelId).OrderBy(o => o).Distinct();
                }
            });
        }

        public static Task<Message> AddMessageAsync(Message message)
        {
            return Task.Run(() =>
            {
                lock (obj)
                {
                    message.Id = _messages.Count + 1;
                    _messages.Add(message);
                    return message;
                }
            });
        }

        public static Task<bool> DeleteMessageAsync(int channelId, int messageId)
        {
            return Task.Run(() =>
            {
                lock (obj)
                {
                    return _messages.Remove(_messages.FirstOrDefault(o => o.ChannelId == channelId && o.Id == messageId));
                }
            });
        }
    }
}