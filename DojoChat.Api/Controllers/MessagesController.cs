using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoChat.Api.Model;
using DojoChat.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DojoChat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // ApiController attr. indicates that the controller responds to web API requests.
    // controller (and constructor) is instantiated every time there's a new HTTP request.
    public class MessagesController : ControllerBase
    {
        private readonly MessagesContext _context;

        //public MessagesController() { }

        public MessagesController(MessagesContext context)
        {
            _context = context;
        }

        // GET: api/messages/channel/4 
        [HttpGet]
        [Route("channel/{channelId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(int channelId)
        {
            if (channelId < 1 || channelId > 65536)
                return BadRequest("The field ChannelId must be between 1 and 65536.");

            //IEnumerable<Message> messages = await MessageList.GetMessagesForChannelAsync(channelId);
            var messages = await _context.Messages.Where(m => m.ChannelId == channelId).ToListAsync();

            return base.Ok(messages);
        }

        //GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            //Message message = await MessageList.GetMessageAsync(id);
            Message message = await _context.Messages.FirstOrDefaultAsync(o => o.Id == id);

            if (message == null)
                return NotFound();
            else
                return Ok(message);
        }

        [HttpGet]
        [Route("channels")]
        public async Task<ActionResult<IEnumerable<int>>> GetChannels()
        {
            //return base.Ok(await MessageList.GetChannelIds());
            IEnumerable<int> channels = await _context.Messages.Select(m => m.ChannelId)
                                                               .OrderBy(o => o)
                                                               .Distinct()
                                                               .ToListAsync();

            return Ok(channels);
        }

        // POST: api/messages/channel/4
        [HttpPost]
        [Route("channel/{channelId}")]
        public async Task<ActionResult<Message>> PostMessage(int channelId, [FromBody]Message message)
        {
            message.ChannelId = channelId;

            if (!TryValidateModel(message))
                return BadRequest(ModelState);

            //await MessageList.AddMessageAsync(message);
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }

        // DELETE: api/messages/2/channel/4
        [HttpDelete]
        [Route("{messageId}/channel/{channelId}")]
        public async Task<ActionResult> DeleteMessage(int messageId, int channelId)
        {
            //bool success = await MessageList.DeleteMessageAsync(channelId, messageId)
            Message msg = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId && m.ChannelId == channelId);

            if (msg == null)
                return NotFound();

            _context.Messages.Remove(msg);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

/* 
The CreatedAtAction method;
- Returns an HTTP 201 status code, if successful; the standard response for an HTTP POST method that creates a new resource.
- Adds a Location header to the response.The Location header specifies the URI of the newly created to -do item.
- References the GetTodoItem action to create the Location header's URI.
*/
