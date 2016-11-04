using System;
using System.ComponentModel.DataAnnotations;

namespace Wan.Release.Infrastructure.Entity
{
    public class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }
    }
}