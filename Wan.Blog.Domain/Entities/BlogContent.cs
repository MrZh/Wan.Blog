using System;
using System.ComponentModel.DataAnnotations.Schema;
using Wan.Release.Infrastructure.Base;
using Wan.Release.Infrastructure.Command;
using Wan.Release.Infrastructure.CommandBus;
using Wan.Release.Infrastructure.Entity;

namespace Wan.Blog.Domain.Entities
{
    [Table("BlogContent")]
    public class BlogContent : Entity
    {
        public string BlogId { get; set; }

        public string ParentId { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        public string UserId { get; set; }


        public static void Publish(BlogContent blogContent)
        {
            BaseCommandBus.AsyncBaseSendCommand(Command<BlogContent>.InitBaseCommand(blogContent));
        }

        public static void Delete(string id)
        {
            BaseCommandBus.AsyncBaseSendCommand(Command<BlogContent>.InitBaseCommand(new BlogContent { Id = id }, CommandEnum.Delete));
        }
    }
}
