using AgileWorksServiceDesk.Data;
using AgileWorksServiceDesk.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesk.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Request, RequestDTO>().ReverseMap();
        }        
    }
}
