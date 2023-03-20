CREATE DATABASE DBPermissions
GO

USE DBPermissions
GO

CREATE TABLE PermissionType(
Id INT IDENTITY(1,1) NOT NULL,
Descripcion TEXT NOT NULL
)
GO

ALTER TABLE dbo.PermissionType
ADD CONSTRAINT pk_PermissionType PRIMARY KEY(Id)
GO


CREATE TABLE Permission(
Id INT IDENTITY(1,1) NOT NULL,
NombreEmpleado TEXT NOT NULL,
APellidoEmpleado TEXT NOT NULL,
TipoPermiso INT NOT NULL,
FechaPermiso DATETIME NOT NULL
)
GO

ALTER TABLE dbo.Permission
ADD CONSTRAINT pk_Permission PRIMARY KEY(Id),
CONSTRAINT fk_Permission_PermissionType FOREIGN KEY(TipoPermiso)
REFERENCES dbo.PermissionType(Id)
GO

INSERT INTO dbo.PermissionType
(
    Descripcion
)
VALUES
('Test Permission' -- Descripcion - text
    )
GO

INSERT INTO dbo.Permission
(
    NombreEmpleado,
    APellidoEmpleado,
    TipoPermiso,
    FechaPermiso
)
VALUES
(   'Juan',       -- NombreEmpleado - text
    'Herrera',       -- APellidoEmpleado - text
    1,        -- TipoPermiso - int
    GETDATE() -- FechaPermiso - datetime
    )
GO

INSERT INTO dbo.Permission
(
    NombreEmpleado,
    APellidoEmpleado,
    TipoPermiso,
    FechaPermiso
)
VALUES
(   'Alba Mariana',       -- NombreEmpleado - text
    'Herrera',       -- APellidoEmpleado - text
    1,        -- TipoPermiso - int
    GETDATE() -- FechaPermiso - datetime
    )
GO
