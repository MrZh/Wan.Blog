using System.ComponentModel.DataAnnotations.Schema;
using Wan.Release.Infrastructure.Base;
using Wan.Release.Infrastructure.Command;
using Wan.Release.Infrastructure.CommandBus;
using Wan.Release.Infrastructure.Entity;
using Wan.Release.Infrastructure.Query;

namespace Wan.Blog.Domain.Entities
{
    [Table("User")]
    public class User : Entity
    {
        public string UserName { get; set; }

        public string RealName { get; set; }

        public string NickName { get; set; }

        public string PassWord { get; set; }

        public static void Register(User user)
        {
            BaseCommandBus.AsyncBaseSendCommand(Command<User>.InitBaseCommand(user));
        }

        public static void Update(User user)
        {
            BaseCommandBus.AsyncBaseSendCommand(Command<User>.InitBaseCommand(user, CommandEnum.Update));
        }
    }
}
