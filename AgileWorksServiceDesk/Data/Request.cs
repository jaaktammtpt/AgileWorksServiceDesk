using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesk.Data
{
    public class Request : BaseEntity
    {
        [Required(ErrorMessage = "Description is required"), StringLength(250, MinimumLength = 8) ]
        public string Description { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DueDateTime { get; set; }

        public bool Completed { get; set; }

        public Request()
        {
        }

    }
}
