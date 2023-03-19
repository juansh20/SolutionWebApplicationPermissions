using WebApplicationPermissions.Dtos;
using WebApplicationPermissions.Interfaces;
using WebApplicationPermissions.Models;
using WebApplicationPermissions.Utils;

namespace WebApplicationPermissions.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticsearchService _elasticsearchService;
        private readonly IKafkaProducerService _kafkaProducerService;

        public PermissionService(IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository, IUnitOfWork unitOfWork, IElasticsearchService elasticsearchService, IKafkaProducerService kafkaProducerService)
        {
            _permissionRepository = permissionRepository;
            _permissionTypeRepository = permissionTypeRepository;
            _unitOfWork = unitOfWork;
            _elasticsearchService = elasticsearchService;
            _kafkaProducerService = kafkaProducerService;
        }

        public async Task<Permission> RequestPermission(Permission permission)
        {
            var permissionType = await _permissionTypeRepository.GetByIdAsync(permission.TipoPermiso);
            permission.PermissionType = permissionType;
            permission.FechaPermiso = DateTime.Now;
            await _permissionRepository.Add(permission);
            var i = await _unitOfWork.SaveChangesAsync();

            await _elasticsearchService.InsertAsync<Permission>(permission);

            var message = new KafkaMessageDto
            {
                Id = Guid.NewGuid().ToString(),
                OperationName = "request"
            };
          //  await _kafkaProducerService.SendMessageAsync("permission-topic", message);

            return permission;
        }

        public async Task<Permission> ModifyPermission(Permission permission)
        {
            var permissionType = await _permissionTypeRepository.GetByIdAsync(permission.TipoPermiso);
            permission.PermissionType = permissionType;
            permission.FechaPermiso = DateTime.Now;

            _permissionRepository.Update(permission);
            await _unitOfWork.SaveChangesAsync();

            await _elasticsearchService.UpdateAsync<Permission>(permission);

            var message = new KafkaMessageDto
            {
                Id = Guid.NewGuid().ToString(),
                OperationName = "modify"
            };
            //await _kafkaProducerService.SendMessageAsync("permission-topic", message);

            return permission;
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            var permissions = await _permissionRepository.GetAllWithIncludesAsync(p => p.PermissionType);

            var message = new KafkaMessageDto
            {
                Id = Guid.NewGuid().ToString(),
                OperationName = "get"
            };
            //await _kafkaProducerService.SendMessageAsync("permission-topic", message);

            return permissions;
        }

        public async Task<Permission> GetPermission(int id)
        {
            var permission = await _permissionRepository.GetByIdAsync(id, "PermissionType");

            if (permission == null)
            {
                throw new PermissionNotFoundException(id);
            }

            return permission;
        }

    }
}
