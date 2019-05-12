using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoChat.Api.Model;
using System;

namespace DojoChat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // ApiController attr. indicates that the controller responds to web API requests.
    // controller (and constructor) is instantiated every time there's a new HTTP request.
    public class MessagesController : ControllerBase
    {
        //private readonly TodoContext _context;
        private List<Message> _messages;

        public MessagesController()
        {
            // todo: get seriialized messages.
            _messages = new List<Message>();
            _messages.Add(new Message() { Id = 1, User = "User 1", Text = "Text for message 1", Created = DateTime.Now });
            _messages.Add(new Message() { Id = 2, User = "User 2", Text = "Text for message 2", Created = DateTime.Now });
            _messages.Add(new Message() { Id = 3, User = "User 1", Text = "Text for message 3", Created = DateTime.Now });
        }

        // GET: api/Messages
        // The [HttpGet] attribute denotes a method that responds to an HTTP GET request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await Task.Run(() => _messages);
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = _messages.FirstOrDefault(o => o.Id == id);

            if (message == null)
                return NotFound();
            else
                return await Task.Run(() => message);
        }
    }
}