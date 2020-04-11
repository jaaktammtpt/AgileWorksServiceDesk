using AgileWorksServiceDesk.Data;
using AgileWorksServiceDesk.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgileWorksServiceDesk.Services
{
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RequestService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RequestDTO>> GetAllActiveRequests()
        {
            var requests = await _context.Requests
                .Where(x => x.Completed == false)
                .OrderByDescending(x => x.DueDateTime)
                .ToListAsync();

            return _mapper.Map<List<RequestDTO>>(requests);
        }

        public async Task<RequestDTO> CreateAsync(RequestDTO requestDTO)
        {
            var request = _mapper.Map<Request>(requestDTO);
            _context.Add(request);
            await _context.SaveChangesAsync();

            return _mapper.Map<RequestDTO>(request);
        }

        public async Task<RequestDTO> GetByIdAsync(int? id)
        {
            var request = await _context.Requests
                .FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<RequestDTO>(request);
        }

        public async Task<RequestDTO> UpdateAsync(RequestDTO requestDTO)
        {
            var request = _mapper.Map<Request>(requestDTO);
            _context.Update(request);
            await _context.SaveChangesAsync();

            return _mapper.Map<RequestDTO>(request);
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Requests.AnyAsync(e => e.Id == id);
        }
    }
}
