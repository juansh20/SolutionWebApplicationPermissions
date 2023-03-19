using Microsoft.AspNetCore.Mvc;
using WebApplicationPermissions.Interfaces;
using WebApplicationPermissions.Models;
using WebApplicationPermissions.Utils;

namespace WebApplicationPermissions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<ActionResult<Permission>> RequestPermission([FromBody] Permission permission)
        {
            var result = await _permissionService.RequestPermission(permission);

            return CreatedAtAction(nameof(GetPermission), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyPermission(int id, [FromBody] Permission permission)
        {
            if (id != permission.Id)
            {
                return BadRequest();
            }

            await _permissionService.ModifyPermission(permission);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
            var result = await _permissionService.GetPermissions();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetPermission(int id)
        {
            try
            {
                var result = await _permissionService.GetPermission(id);
                return Ok(result);
            }
            catch (PermissionNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
