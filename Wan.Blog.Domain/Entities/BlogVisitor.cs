using System;
using System.ComponentModel.DataAnnotations.Schema;
using Wan.Release.Infrastructure.Base;
using Wan.Release.Infrastructure.Command;
using Wan.Release.Infrastructure.CommandBus;
using Wan.Release.Infrastructure.Entity;

namespace Wan.Blog.Domain.Entities
{
    [Table("BlogVisitor")]
    public class BlogVisitor : Entity
    {
        public string BlogId { get; set; }

        public string UserId { get; set; }

        public DateTime VisitTime { get; set; }

        public static void Visit(string blogId, string userId)
        {
            var blogVisitor = new BlogVisitor { BlogId = blogId, UserId = userId, VisitTime = DateTime.Now };
            BaseCommandBus.AsyncBaseSendCommand(Command<BlogVisitor>.InitBaseCommand(blogVisitor));
        }

        public static void Delete(string visitorId)
        {
            BaseCommandBus.AsyncBaseSendCommand(Command<BlogVisitor>.InitBaseCommand(new BlogVisitor { Id = visitorId }, CommandEnum.Delete));
        }
    }
}
