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
        public MessagesController() { }

        // GET: api/messages
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        //{
        //    //return Ok(await Repository.GetMessagesAsync());
        //    return Ok(await Repository.GetMessagesForChannelAsync(1));
        //}

        // GET: api/messages/channel/4 
        // The [HttpGet] attribute denotes a method that responds to an HTTP GET request
        [HttpGet]
        [Route("channel/{channelId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(int channelId)
        {
            if (channelId < 1 || channelId > 65536)
                return BadRequest("The field ChannelId must be between 1 and 65536.");

            return Ok(await Repository.GetMessagesForChannelAsync(channelId));
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            Message message = await Repository.GetMessageAsync(id);

            if (message == null)
                return NotFound();
            else
                return Ok(message);
        }

        [HttpGet]
        [Route("channels")]
        public async Task<ActionResult<IEnumerable<int>>> GetChannels()
        {
            return Ok(await Repository.GetChannelIds());
        }

        // POST: api/messages/channel/4
        [HttpPost]
        [Route("channel/{channelId}")]
        public async Task<ActionResult<Message>> PostMessage(int channelId, Message message)
        {
            message.ChannelId = channelId;

            TryValidateModel(message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Repository.AddMessageAsync(message);
            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            if (await Repository.DeleteMessageAsync(id))
                return Ok();
            else
                return NotFound();
        }
    }
}

/* 
The CreatedAtAction method;
- Returns an HTTP 201 status code, if successful; the standard response for an HTTP POST method that creates a new resource.
- Adds a Location header to the response.The Location header specifies the URI of the newly created to -do item.
- References the GetTodoItem action to create the Location header's URI.
*/
