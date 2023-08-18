﻿using System;
namespace AspNetMvcApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        // Связь с пользователями (например, создателем)
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public object Name { get; internal set; }
        // Дополнительные поля по необходимости
    }

}
