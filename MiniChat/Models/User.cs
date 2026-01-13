using System;
using System.Collections.Generic;
using System.Text;

namespace MiniChat.Models
{
    public class User // internal class
    {
        // Unique ID from database
        public int Id { get; set; }

        // User name
        public string Name { get; set; }

        // Optional: Constructor
        public User() { }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

