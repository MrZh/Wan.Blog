using System.ComponentModel.DataAnnotations.Schema;
using Wan.Release.Infrastructure.Attribute;
using Wan.Release.Infrastructure.Entity;


namespace Wan.Blog.Domain.Entities
{
    [Table("BlogCategory")]
    public class BlogCategory : Entity
    {
        [RelId]
        public string BlogId { get; set; }

        [RelId]
        public string CategoryId { get; set; }
    }
}
