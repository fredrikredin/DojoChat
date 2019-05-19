using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoChat.Api.Model;
using DojoChat.Api.DAL;

namespace DojoChat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // ApiController attr. indicates that the controller responds to web API requests.
    // controller (and constructor) is instantiated every time there's a new HTTP request.
    public class MessagesController : ControllerBase
    {
        public MessagesController()
        {

        }

        // GET: api/Messages
        // The [HttpGet] attribute denotes a method that responds to an HTTP GET request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            //return await Task.Run(() => Repository.GetMessages());
            IEnumerable<Message> messages = await Task.Run(() => Repository.GetMessages());
            return Ok(messages);
        }

        // GET: api/messages/channel/4 
        [HttpGet]
        [Route("channel/{channelId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(int channelId)
        {
            //return await Task.Run(() => Repository.GetMessages());
            IEnumerable<Message> channelMessages = await Task.Run(() => Repository.GetChannelMessages(channelId));
            return Ok(channelMessages);
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            Message message = await Task.Run(() => Repository.GetMessage(id));

            if (message == null)
                return NotFound();
            else
                return Ok(message);
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Task.Run(() => Repository.AddMessage(message));
            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }
    }
}

/* 
The CreatedAtAction method;
- Returns an HTTP 201 status code, if successful; the standard response for an HTTP POST method that creates a new resource.
- Adds a Location header to the response.The Location header specifies the URI of the newly created to -do item.
- References the GetTodoItem action to create the Location header's URI.
*/
