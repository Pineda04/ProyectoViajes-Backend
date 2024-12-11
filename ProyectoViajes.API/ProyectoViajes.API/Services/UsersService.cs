using AutoMapper;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Database;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Users;
using Microsoft.EntityFrameworkCore;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly ProyectoViajesContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        public UsersService(ProyectoViajesContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
        }
        public async Task<ResponseDto<PaginationDto<List<UserDto>>>> GetUsersListAsync(
            string searchTerm = "",
            int page = 1)
        {
            int startIndex = (page - 1) * PAGE_SIZE;

            var usersQuery = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                usersQuery = usersQuery
                    .Where(x => (x.FirstName + " " + x.LastName)
                    .ToLower().Contains(searchTerm.ToLower()));
            }

            int totalUsers = await usersQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalUsers / PAGE_SIZE);

            var usersEntity = await usersQuery
                .OrderByDescending(x => x.FirstName)
                .Skip(startIndex)
                .Take(PAGE_SIZE)
                .ToListAsync();

            var usersDto = _mapper.Map<List<UserDto>>(usersEntity);

            return new ResponseDto<PaginationDto<List<UserDto>>>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORDS_FOUND,
                Data = new PaginationDto<List<UserDto>>
                {
                    CurrentPage = page,
                    PageSize = PAGE_SIZE,
                    TotalItems = totalUsers,
                    TotalPages = totalPages,
                    Items = usersDto,
                    HasPreviousPage = page > 1,
                    HasNextPage = page < totalPages
                }
            };
        }
        public async Task<ResponseDto<UserDto>> GetUserByIdAsync(string id)
        {
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity == null)
            {
                return new ResponseDto<UserDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            var userDto = _mapper.Map<UserDto>(userEntity);
            return new ResponseDto<UserDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.RECORD_FOUND,
                Data = userDto
            };
        }
        public async Task<ResponseDto<UserDto>> EditAsync(UserEditDto dto, string id)
        {
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == id);
            if (userEntity == null)
            {
                return new ResponseDto<UserDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            _mapper.Map(dto, userEntity);
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
            var userDto = _mapper.Map<UserDto>(userEntity);
            return new ResponseDto<UserDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.UPDATE_SUCCESS,
                Data = userDto
            };
        }
        public async Task<ResponseDto<UserDto>> DeleteAsync(string id)
        {
            var rolesEntity = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == id);
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(a => a.Id == id);
            if (userEntity == null)
            {
                return new ResponseDto<UserDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = MessagesConstants.RECORD_NOT_FOUND
                };
            }
            _context.UserRoles.Remove(rolesEntity);
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
            return new ResponseDto<UserDto>
            {
                StatusCode = 200,
                Status = true,
                Message = MessagesConstants.DELETE_SUCCESS
            };
        }
    }
}
