﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BigBlog.Models.Db
{
    [Table("articles")]
    public class Article
    {
        public Guid Id {  get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }
        public Teg Teg { get; set; }
        public Guid TegId { get; set; }

    }
}
