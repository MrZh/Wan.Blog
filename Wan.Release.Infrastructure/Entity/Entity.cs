using System;
using System.ComponentModel.DataAnnotations;

namespace Wan.Release.Infrastructure.Entity
{
    public class Entity
    {
        [Key]
        [StringLength(50)]
        public string Id { get;  set; }

        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
