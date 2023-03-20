using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationPermissions.Controllers;
using WebApplicationPermissions.Interfaces;
using WebApplicationPermissions.Models;
using WebApplicationPermissions.Services;

namespace PermissionTests
{
    public class Tests
    {
        private PermissionController _controller;
        private Mock<IPermissionService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IPermissionService>();
            _controller = new PermissionController(_mockService.Object);
        }

        [Test]
        public async Task GetPermissions_ReturnsListOfPermissions()
        {
            // Arrange
            var expectedPermissions = new List<Permission>
            {
                new Permission { Id = 1, NombreEmpleado = "Juan", ApellidoEmpleado = "Herrera", TipoPermiso = 1, FechaPermiso = new System.DateTime(2023, 3, 19) },
                new Permission { Id = 2, NombreEmpleado = "Pedro", ApellidoEmpleado = "Gonzalez", TipoPermiso = 2, FechaPermiso = new System.DateTime(2023, 3, 18) }
            };

            _mockService.Setup(x => x.GetPermissions())
                .ReturnsAsync(expectedPermissions);

            // Act
            var actionResult = await _controller.GetPermissions();
            var result = ((Microsoft.AspNetCore.Mvc.ObjectResult)actionResult.Result);
            var data = result.Value as List<Permission>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, data.Count);
        }

        [Test]
        public async Task RequestPermission_ReturnsCreatedPermission()
        {
            // Arrange
            var permission = new Permission
            {
                NombreEmpleado = "Lorena",
                ApellidoEmpleado = "Rey",
                TipoPermiso = 1,
                FechaPermiso = DateTime.Now,
                PermissionType = new PermissionType { Id = 1, Descripcion = "Test Permission" }
            };

            var mockPermissionService = new Mock<IPermissionService>();
            mockPermissionService.Setup(s => s.RequestPermission(It.IsAny<Permission>()))
                .ReturnsAsync(new Permission { Id = 1, NombreEmpleado = permission.NombreEmpleado, ApellidoEmpleado = permission.ApellidoEmpleado, TipoPermiso = permission.TipoPermiso, FechaPermiso = permission.FechaPermiso, PermissionType = permission.PermissionType });

            var controller = new PermissionController(mockPermissionService.Object);

            // Act
            var actionResult = await controller.RequestPermission(permission);
            var result = actionResult.Result as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            Assert.AreEqual("GetPermission", result.ActionName);
            Assert.AreEqual(1, result.RouteValues["id"]);

            var createdPermission = result.Value as Permission;
            Assert.NotNull(createdPermission);
            Assert.AreEqual(1, createdPermission.Id);
            Assert.AreEqual(permission.NombreEmpleado, createdPermission.NombreEmpleado);
            Assert.AreEqual(permission.ApellidoEmpleado, createdPermission.ApellidoEmpleado);
            Assert.AreEqual(permission.TipoPermiso, createdPermission.TipoPermiso);
            Assert.AreEqual(permission.FechaPermiso, createdPermission.FechaPermiso);
            Assert.AreEqual(permission.PermissionType.Id, createdPermission.PermissionType.Id);
            Assert.AreEqual(permission.PermissionType.Descripcion, createdPermission.PermissionType.Descripcion);
        }

        [Test]
        public async Task ModifyPermission_ReturnsNoContent()
        {
            // Arrange
            var permission = new Permission
            {
                Id = 1,
                NombreEmpleado = "Juan",
                ApellidoEmpleado = "Pérez",
                TipoPermiso = 1,
                FechaPermiso = DateTime.Now,
                PermissionType = new PermissionType
                {
                    Id = 1,
                    Descripcion = "Test Permission"
                }
            };
            var mockPermissionService = new Mock<IPermissionService>();
            mockPermissionService.Setup(x => x.ModifyPermission(permission)).Returns(Task.FromResult(permission));
            var controller = new PermissionController(mockPermissionService.Object);

            // Act
            var result = await controller.ModifyPermission(permission.Id, permission) as NoContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }


    }
}