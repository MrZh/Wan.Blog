using System.ComponentModel.DataAnnotations.Schema;
using Wan.Release.Infrastructure.Entity;

namespace Wan.Blog.Domain.Entities
{
    [Table("Category")]
    public class Category : Entity
    {
        public string Name { get; set; }
    }
}
