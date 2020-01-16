using System;
using System.ComponentModel.DataAnnotations;

namespace SlutProjekt2k19.Models
{
    public class PostMessage
    {
        [Key] public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public PostMessage(PostMessageDto postMessageDto)
        {
            Message = postMessageDto.Message;
            Timestamp = DateTime.Parse(postMessageDto.Timestamp);
            UserId = postMessageDto.UserId;
        }

        public PostMessage()
        {
        }
    }

    public class PostMessageDto
    {
        public string Message { get; set; }
        public string Timestamp { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public PostMessageDto()
        {
        }

        public PostMessageDto(PostMessage postMessage)
        {
            Message = postMessage.Message;
            Timestamp = postMessage.Timestamp.ToString(@"yyyy-MM-dd HH\:mm\:ss");
            UserId = postMessage.UserId;
            // Använd profilens namn som användar namn
            UserName = postMessage.User?.UserName;
        }
    }
}