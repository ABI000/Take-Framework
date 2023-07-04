using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TakeFramework.Domain.Entities;

namespace Sample.Core
{
    public class User : FullEntity<long, long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}