using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesk.Models
{
    public class RequestDTO : BaseEntityDTO
    {
        [Required, StringLength(250, MinimumLength = 8)]
        public string Description { get; set; }

        [Required, DataType(DataType.DateTime), Display(Name = "Deadline")]
        public DateTime DueDateTime { get; set; }

        public bool Completed { get; set; }

        public string LateIndicator()
        {
            if (DateTime.Now.AddHours(1) > DueDateTime)
            {
                return "late";
            }
            return "notlate";
        }

        public RequestDTO()
        {
        }
    }
}
