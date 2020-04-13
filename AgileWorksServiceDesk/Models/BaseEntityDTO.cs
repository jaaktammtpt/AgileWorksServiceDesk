using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesk.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEntityDTO
    {
        [Key]
        public int Id { get; set; }
    }
}
