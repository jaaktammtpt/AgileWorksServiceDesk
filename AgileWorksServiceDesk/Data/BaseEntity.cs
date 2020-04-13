using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesk.Data
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
