using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoViajes.API.Constants;
using ProyectoViajes.API.Dtos.Common;
using ProyectoViajes.API.Dtos.Users;
using ProyectoViajes.API.Services.Interfaces;

namespace ProyectoViajes.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // Traer todos
        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<List<UserDto>>>> GetAll(
            string searchTerm = "",
            int page = 1)
        {
            var response = await _usersService.GetUsersListAsync(searchTerm, page);
            return StatusCode(response.StatusCode, response);
        }

        // Traer por id
        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.USER} || {RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> Get(string id)
        {
            var response = await _usersService.GetUserByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        // Editar
        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.USER}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> Edit(UserEditDto dto, string id)
        {
            var response = await _usersService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        // Eliminar
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<ResponseDto<UserDto>>> Delete(string id)
        {
            var response = await _usersService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
