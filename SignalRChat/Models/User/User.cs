﻿
using SingnalRChat.Models;

public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<PostModel> Posts { get; set; }
    }

