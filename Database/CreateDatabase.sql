-- SCRIPT SQL COMPLETO (SQL Server)
CREATE DATABASE CentroDeportivo;
GO
USE CentroDeportivo;
GO

-- Tabla Socios
CREATE TABLE Socios (
    IdSocio INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Activo BIT NOT NULL DEFAULT(1)
);
GO

-- Tabla Actividades
CREATE TABLE Actividades (
    IdActividad INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(120) NOT NULL UNIQUE,
    AforoMaximo INT NOT NULL
);
GO

-- Tabla Reservas
CREATE TABLE Reservas (
    IdReserva INT IDENTITY(1,1) PRIMARY KEY,
    IdSocio INT NOT NULL,
    IdActividad INT NOT NULL,
    Fecha DATETIME NOT NULL,

    CONSTRAINT FK_Reservas_Socios
        FOREIGN KEY (IdSocio) REFERENCES Socios(IdSocio),

    CONSTRAINT FK_Reservas_Actividades
        FOREIGN KEY (IdActividad) REFERENCES Actividades(IdActividad)
);
GO 