using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using Wan.Release.Infrastructure.Attribute;
using Wan.Release.Infrastructure.Base;
using Wan.Release.Infrastructure.Command;
using Wan.Release.Infrastructure.CommandBus;
using Wan.Release.Infrastructure.Entity;
using Wan.Release.Infrastructure.Query;

namespace Wan.Blog.Domain.Entities
{
    [Table("Blog")]
    public class Blogs : Entity
    {
        [RelId]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        [RelId]
        public string UserId { get; set; }

        public static void PublishBlog(Blogs blog, List<string> categoryList = null)
        {
            var commands = new List<BaseCommand>();
            var command = Command<Blogs>.InitBaseCommand(blog);
            if (categoryList != null)
            {
                var list = categoryList.Select(category => new BlogCategory { Id = Guid.NewGuid().ToString(), BlogId = blog.Id, CategoryId = category }).ToList();
                commands.AddRange(Command<BlogCategory>.InitBaseCommands(list));
            }
            commands.Insert(0, command);
            BaseCommandBus.AsyncBaseSendCommands(commands);
        }

        public static void DeleteBlog(string id)
        {
            var command = Command<Blogs>.InitBaseCommand(new Blogs { Id = id }, CommandEnum.Delete);
            var list = QueryContext.BaseGetListByParam<BlogCategory>(Query<BlogCategory>.InitQuery(new BlogCategory { BlogId = id }));
            var commands = Command<BlogCategory>.InitBaseCommands(list, CommandEnum.Delete);
            commands.Insert(0, command);
            BaseCommandBus.AsyncBaseSendCommands(commands);
        }

        public static void UpdateBlog(Blogs blog, List<string> categoryList)
        {
            var command = Command<Blogs>.InitBaseCommand(blog, CommandEnum.Update);
            var list = QueryContext.BaseGetListByParam<BlogCategory>(Query<BlogCategory>.InitQuery(new BlogCategory { BlogId = blog.Id }));
            var commands = Command<BlogCategory>.InitBaseCommands(list, CommandEnum.Delete);
            commands.Insert(0, command);
            list = categoryList.Select(category => new BlogCategory { Id = Guid.NewGuid().ToString(), BlogId = blog.Id, CategoryId = category }).ToList();
            commands.AddRange(Command<BlogCategory>.InitBaseCommands(list));
        }

        public static void TestString(Blogs blog)
        {
            var command = Command<Blogs>.InitBaseCommand(blog, CommandEnum.RelDelete);
            Command<Blogs> c = new Command<Blogs>(blog, true, CommandEnum.RelUpdate);
        }
    }
}
