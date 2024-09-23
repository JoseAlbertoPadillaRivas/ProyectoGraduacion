USE MASTER;
CREATE DATABASE ProyectoGraduacion; 
USE ProyectoGraduacion;

CREATE TABLE tPacientes(
idPaciente int primary key IDENTITY(1,1) NOT NULL,
Cedula varchar(20) NOT NULL,
Nombre varchar(100) NOT NULL,
Correo varchar(100) NOT NULL,
Contrasenna varchar (15) NOT NULL,
Estado bit NOT NULL,
IdRol tinyint NOT NULL,
);

CREATE TABLE tRol(
IdRol tinyint primary key IDENTITY(1,1) NOT NULL,
NombreRol varchar(20)
)

CREATE TABLE tSede(
idSede int primary key IDENTITY(1,1) NOT NULL,
Nombre varchar(20) NOT NULL,
);

CREATE TABLE tCitas(
idCita int primary key IDENTITY(1,1) NOT NULL,
idPaciente int NOT NULL,
idSede int NOT NULL,
Fecha Date NOT NULL,
Hora time NOT NULL
);

CREATE TABLE tCalificacion(
idCalificacion int primary key IDENTITY(1,1) NOT NULL,
idPaciente int NOT NULL,
Comentario varchar(1020),
Estrellas int NOT NULL
);

CREATE TABLE tSeguimiento(
idSeguimiento int primary key IDENTITY(1,1) NOT NULL,
idPaciente int NOT NULL,
Nombre varchar(100),
Estado varchar(100),
FechaEntrega Date NOT NULL,
);

CREATE TABLE tHistoriaCE(
idExpediente int primary key IDENTITY(1,1) NOT NULL,
idPaciente int NOT NULL,
Diagnostico varchar(100),
Procedimiento varchar(100),
Medicacion varchar(100),
Observaciones varchar(1020),
FechaConsulta Date NOT NULL,
Archivos VARBINARY(MAX)
);

CREATE TABLE tInventario(
idProducto int primary key IDENTITY(1,1) NOT NULL,
NombreProducto varchar(100) NOT NULL,
Cantidad int NOT NULL,
CaducidadProducto date,
idProveedor int 
);

CREATE TABLE tProveedores(
idProveedor int primary key IDENTITY(1,1) NOT NULL,
Empresa varchar(100) NOT NULL,
NumeroTelefono int NOT NULL,
Correo varchar(100) NOT NULL
);

ALTER TABLE tPacientes
ADD CONSTRAINT FK_IdRol
FOREIGN KEY (IdRol)
REFERENCES tRol(IdRol);

ALTER TABLE tCitas
ADD CONSTRAINT FK_idSede
FOREIGN KEY (idSede)
REFERENCES tSede(idSede);

ALTER TABLE tCitas
ADD CONSTRAINT FK_idPaciente
FOREIGN KEY (idPaciente)
REFERENCES tPacientes(idPaciente);

ALTER TABLE tCalificacion
ADD CONSTRAINT FK_idPacienteCalificacion
FOREIGN KEY (idPaciente)
REFERENCES tPacientes(idPaciente);

ALTER TABLE tSeguimiento
ADD CONSTRAINT FK_idPacienteSeguimiento
FOREIGN KEY (idPaciente)
REFERENCES tPacientes(idPaciente);

ALTER TABLE tHistoriaCE
ADD CONSTRAINT FK_idPacienteHistoriaCE
FOREIGN KEY (idPaciente)
REFERENCES tPacientes(idPaciente);

ALTER TABLE tInventario
ADD CONSTRAINT FK_idProveedor
FOREIGN KEY (idProveedor)
REFERENCES tProveedores(idProveedor);






