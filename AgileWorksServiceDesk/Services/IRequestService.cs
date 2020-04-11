using AgileWorksServiceDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesk.Services
{
    public interface IRequestService
    {
        Task<List<RequestDTO>> GetAllActiveRequests();
        Task<RequestDTO> CreateAsync(RequestDTO requestDTO);
    }
}
