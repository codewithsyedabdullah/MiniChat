using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace MiniChat.Models
{
    public class Message // internal class
    {
        // Unique ID from database
        public int Id { get; set; }

        // User who sent this message
        public int UserId { get; set; }

        // Message text
        public string Text { get; set; }

        // Timestamp
        public DateTime Timestamp { get; set; }

        // Optional: Constructor
        public Message() { }

        public Message(int id, int userId, string text, DateTime timestamp)
        {
            Id = id;
            UserId = userId;
            Text = text;
            Timestamp = timestamp;
        }
    }
}
