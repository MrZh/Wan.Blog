using System.Collections.Generic;
using System.Threading.Tasks;
using Wan.Release.Infrastructure.Base;

namespace Wan.Release.Infrastructure.CommandBus
{
    public class BaseCommandBus
    {
        public static async void AsyncBaseSendCommand(BaseCommand command)
        {
            await Task.Run(() => CommandContext.BaseCommand(command));
        }

        public static void AsyncBaseSendCommands(List<BaseCommand> commands)
        {
            Task.Run(() => CommandContext.BaseTransaction(commands));
        }

        public static async void BaseSendCommand(BaseCommand command)
        {
            await Task.Run(() => CommandContext.BaseCommand(command));
        }

        public static void BaseSendCommands(List<BaseCommand> commands)
        {
            Task.Run(() => CommandContext.BaseTransaction(commands));
        }

        public virtual void SendCommand(BaseCommand command)
        {
            Task.Run(() => CommandContext.BaseCommand(command));
        }

        public virtual void SendCommands(List<BaseCommand> commands)
        {
            Task.Run(() => CommandContext.BaseTransaction(commands));
        }
    }
}
