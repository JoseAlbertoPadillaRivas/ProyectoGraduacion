USE [master]
GO
/****** Object:  Database [ProyectoGraduacion]    Script Date: 6/10/2024 18:15:52 ******/
CREATE DATABASE [ProyectoGraduacion]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProyectoGraduacion', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProyectoGraduacion.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProyectoGraduacion_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProyectoGraduacion_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProyectoGraduacion] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProyectoGraduacion].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProyectoGraduacion] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProyectoGraduacion] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProyectoGraduacion] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProyectoGraduacion] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProyectoGraduacion] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProyectoGraduacion] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProyectoGraduacion] SET  MULTI_USER 
GO
ALTER DATABASE [ProyectoGraduacion] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProyectoGraduacion] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProyectoGraduacion] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProyectoGraduacion] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProyectoGraduacion] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProyectoGraduacion] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProyectoGraduacion] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProyectoGraduacion] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProyectoGraduacion]
GO
/****** Object:  Table [dbo].[tCalificacion]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCalificacion](
	[idCalificacion] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[Comentario] [varchar](1020) NULL,
	[Estrellas] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCalificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tCitas]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCitas](
	[idCita] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[idSede] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHistoriaCE]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHistoriaCE](
	[idExpediente] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[Diagnostico] [varchar](100) NULL,
	[Procedimiento] [varchar](100) NULL,
	[Medicacion] [varchar](100) NULL,
	[Observaciones] [varchar](1020) NULL,
	[FechaConsulta] [date] NOT NULL,
	[Archivos] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idExpediente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tInventario]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tInventario](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[NombreProducto] [varchar](100) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[CaducidadProducto] [date] NULL,
	[idProveedor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tPacientes]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tPacientes](
	[idPaciente] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [varchar](20) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Contrasenna] [varchar](15) NOT NULL,
	[Estado] [bit] NOT NULL,
	[IdRol] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idPaciente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tProveedores]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tProveedores](
	[idProveedor] [int] IDENTITY(1,1) NOT NULL,
	[Empresa] [varchar](100) NOT NULL,
	[NumeroTelefono] [int] NOT NULL,
	[Correo] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRol]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRol](
	[IdRol] [tinyint] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tSede]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tSede](
	[idSede] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idSede] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tSeguimiento]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tSeguimiento](
	[idSeguimiento] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[Nombre] [varchar](100) NULL,
	[FechaEntrega] [date] NOT NULL,
	[Estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idSeguimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tCalificacion]  WITH CHECK ADD  CONSTRAINT [FK_idPacienteCalificacion] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tCalificacion] CHECK CONSTRAINT [FK_idPacienteCalificacion]
GO
ALTER TABLE [dbo].[tCitas]  WITH CHECK ADD  CONSTRAINT [FK_idPaciente] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tCitas] CHECK CONSTRAINT [FK_idPaciente]
GO
ALTER TABLE [dbo].[tCitas]  WITH CHECK ADD  CONSTRAINT [FK_idSede] FOREIGN KEY([idSede])
REFERENCES [dbo].[tSede] ([idSede])
GO
ALTER TABLE [dbo].[tCitas] CHECK CONSTRAINT [FK_idSede]
GO
ALTER TABLE [dbo].[tHistoriaCE]  WITH CHECK ADD  CONSTRAINT [FK_idPacienteHistoriaCE] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tHistoriaCE] CHECK CONSTRAINT [FK_idPacienteHistoriaCE]
GO
ALTER TABLE [dbo].[tInventario]  WITH CHECK ADD  CONSTRAINT [FK_idProveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[tProveedores] ([idProveedor])
GO
ALTER TABLE [dbo].[tInventario] CHECK CONSTRAINT [FK_idProveedor]
GO
ALTER TABLE [dbo].[tPacientes]  WITH CHECK ADD  CONSTRAINT [FK_IdRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[tRol] ([IdRol])
GO
ALTER TABLE [dbo].[tPacientes] CHECK CONSTRAINT [FK_IdRol]
GO
ALTER TABLE [dbo].[tSeguimiento]  WITH CHECK ADD  CONSTRAINT [FK_idPacienteSeguimiento] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tSeguimiento] CHECK CONSTRAINT [FK_idPacienteSeguimiento]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarPaciente]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarPaciente]
	@Cedula VARCHAR(20),
	@Nombre VARCHAR(100),
	@Correo VARCHAR(100),
	@IdRol TINYINT,
	@idPaciente INT
AS
BEGIN

	UPDATE	tPacientes
	   SET	Cedula = @Cedula,
			Nombre = @Nombre,
			Correo = @Correo,
			IdRol = CASE WHEN @IdRol = 0 THEN IdRol ELSE @IdRol END
	 WHERE	idPaciente = @idPaciente

END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarProducto]
	@NombreProducto	varchar(100),
	@Cantidad int,
	@CaducidadProducto date,
	@idProveedor int,
	@idProducto INT

AS
BEGIN

	UPDATE	tInventario
	   SET	NombreProducto = @NombreProducto,
			Cantidad = @Cantidad,
			CaducidadProducto = @CaducidadProducto,
			idProveedor = @idProveedor
	 WHERE	idProducto = @idProducto

END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarSeguimiento]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarSeguimiento]
	@idPaciente int,
	@Nombre varchar(100),
	@FechaEntrega date,
	@idSeguimiento int

AS
BEGIN

	UPDATE	tSeguimiento
	   SET	idPaciente = @idPaciente,
			Nombre = @Nombre,
			FechaEntrega = @FechaEntrega
	 WHERE	idSeguimiento = @idSeguimiento

END
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoPaciente]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CambiarEstadoPaciente]
	@idPaciente INT
AS
BEGIN
	
	--borrado lógico
	UPDATE	tPacientes
	SET		Estado = CASE WHEN Estado = 1 THEN 0 ELSE 1 END
	WHERE	idPaciente = @idPaciente

	--borrado físico
	--DELETE FROM tUsuario 
	--WHERE	Consecutivo = @Consecutivo

END
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoSeguimiento]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[CambiarEstadoSeguimiento]
	@idSeguimiento INT
AS
BEGIN
	
	--borrado lógico
	UPDATE	tSeguimiento
	SET		Estado = CASE WHEN Estado = 1 THEN 0 ELSE 1 END
	WHERE	idSeguimiento = @idSeguimiento

END
GO
/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProducto]
	@idProducto INT
AS
BEGIN
	DELETE FROM tInventario 
	WHERE	idProducto = @idProducto

END
GO
/****** Object:  StoredProcedure [dbo].[EliminarSeguimiento]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarSeguimiento]
	@idSeguimiento INT
AS
BEGIN
	DELETE FROM tSeguimiento 
	WHERE	idSeguimiento = @idSeguimiento

END
GO
/****** Object:  StoredProcedure [dbo].[IniciarSesion]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IniciarSesion]
	@Correo			varchar(100),
	@Contrasenna	varchar(15)
AS
BEGIN

	SELECT	idPaciente,Cedula,Nombre,Correo,Estado,IdRol
	FROM	dbo.tPacientes
	WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarInventario]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarInventario]
	@NombreProducto	varchar(100),
	@Cantidad int,
	@CaducidadProducto date,
	@idProveedor int
AS
BEGIN

	
		INSERT INTO dbo.tInventario(NombreProducto,Cantidad,CaducidadProducto,idProveedor)
		VALUES(@NombreProducto, @Cantidad, @CaducidadProducto, @idProveedor)

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarPaciente]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarPaciente]
	@Cedula	varchar(20),
	@Nombre			varchar(100),
	@Correo			varchar(100),
	@Contrasenna	varchar(15)
AS
BEGIN

	
		INSERT INTO dbo.tPacientes(Cedula,Nombre,Correo,Contrasenna,Estado,IdRol)
		VALUES(@Cedula,@Nombre,@Correo,@Contrasenna,1,2)

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarProveedor]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarProveedor]
	@Empresa varchar(100),
	@Correo varchar(100),
	@NumeroTelefono int
AS
BEGIN

	
		INSERT INTO dbo.tProveedores(Empresa,Correo,NumeroTelefono)
		VALUES(@Empresa, @Correo, @NumeroTelefono)

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarSeguimiento]    Script Date: 6/10/2024 18:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarSeguimiento]
	@idPaciente int,
	@Nombre varchar(100),
	@FechaEntrega date
AS
BEGIN

	
		INSERT INTO dbo.tSeguimiento(idPaciente,Nombre,FechaEntrega, Estado)
		VALUES(@idPaciente,@Nombre,@FechaEntrega,1)

END
GO
USE [master]
GO
ALTER DATABASE [ProyectoGraduacion] SET  READ_WRITE 
GO
