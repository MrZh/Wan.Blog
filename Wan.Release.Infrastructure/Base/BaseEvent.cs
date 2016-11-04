using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Wan.Release.Infrastructure.Base
{
    public class BaseEvent
    {
        protected BaseEvent(BaseCommand command, string author = null)
        {
            Author = author;
            EventBody = JsonConvert.SerializeObject(command);
            Id = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
        }

        protected BaseEvent(List<BaseCommand> command, string author = null)
        {
            Author = author;
            EventBody = JsonConvert.SerializeObject(command);
            Id = Guid.NewGuid().ToString();
            CreateTime = DateTime.Now;
        }

        public string Id { get; protected set; }
        public DateTime CreateTime { get; protected set; }

        public string EventBody { get; protected set; }

        public string Author { get; protected set; }

        public virtual void SentEvent()
        {
            //do nothing
            // if you want to do sth please override this method
        }

        public static BaseEvent InitEvent(BaseCommand command, string author = null)
        {
            return new BaseEvent(command, author);
        }

        public static BaseEvent InitEvent(List<BaseCommand> command, string author = null)
        {
            return new BaseEvent(command, author);
        }

        public static List<BaseEvent> InitEvents(List<BaseCommand> command, string author = null)
        {
            return command.Select(baseCommand => new BaseEvent(baseCommand, author)).ToList();
        }
    }
}