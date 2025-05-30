USE [master]
GO
/****** Object:  Database [ProyectoGraduacion]    Script Date: 22/11/2024 12:24:32 ******/
CREATE DATABASE [ProyectoGraduacion]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProyectoGraduacion', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProyectoGraduacion.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProyectoGraduacion_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ProyectoGraduacion_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [ProyectoGraduacion] SET AUTO_CLOSE OFF 
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
ALTER DATABASE [ProyectoGraduacion] SET RECOVERY FULL 
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
EXEC sys.sp_db_vardecimal_storage_format N'ProyectoGraduacion', N'ON'
GO
ALTER DATABASE [ProyectoGraduacion] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProyectoGraduacion] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProyectoGraduacion]
GO
/****** Object:  Table [dbo].[tArchivosPaciente]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tArchivosPaciente](
	[idArchivo] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[NombreArchivo] [varchar](100) NOT NULL,
	[TipoArchivo] [varchar](50) NOT NULL,
	[Archivo] [varbinary](max) NOT NULL,
	[FechaSubida] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idArchivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tCalificaciones]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCalificaciones](
	[idCalificaciones] [int] IDENTITY(1,1) NOT NULL,
	[Calificaciones] [int] NOT NULL,
	[idPaciente] [int] NOT NULL,
	[Opinion] [text] NOT NULL,
	[idServicio] [int] NULL,
	[Fecha] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCalificaciones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tCitas]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCitas](
	[idCita] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[idSede] [int] NOT NULL,
	[idCitaDisponible] [int] NOT NULL,
 CONSTRAINT [PK__tCitas__814F3126296C50DB] PRIMARY KEY CLUSTERED 
(
	[idCita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tCitasDisponibles]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tCitasDisponibles](
	[idCitaDisponible] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_CitasDisponibles] PRIMARY KEY CLUSTERED 
(
	[idCitaDisponible] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tHistorial]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tHistorial](
	[idHistorial] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[FechaConsulta] [date] NOT NULL,
	[Diagnostico] [varchar](200) NULL,
	[Tratamiento] [varchar](200) NULL,
	[Medicacion] [varchar](200) NULL,
	[Observaciones] [varchar](200) NULL,
	[Archivo] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[idHistorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tInventario]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tInventario](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[NombreProducto] [varchar](100) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[CaducidadProducto] [date] NULL,
	[idProveedor] [int] NOT NULL,
	[NivelMinimoStock] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tMovimientosInventario]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tMovimientosInventario](
	[idMovimiento] [int] IDENTITY(1,1) NOT NULL,
	[idProducto] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[TipoMovimiento] [varchar](20) NOT NULL,
	[FechaMovimiento] [date] NOT NULL,
	[Observaciones] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[idMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tOrdenesCompra]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tOrdenesCompra](
	[idOrdenCompra] [int] IDENTITY(1,1) NOT NULL,
	[idProveedor] [int] NOT NULL,
	[NombreProducto] [nvarchar](100) NOT NULL,
	[CantidadTotalSolicitada] [int] NOT NULL,
	[EstadoOrden] [nvarchar](20) NOT NULL,
	[FechaSolicitud] [date] NOT NULL,
	[idProducto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idOrdenCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tOrdenesProductos]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tOrdenesProductos](
	[idOrdenProducto] [int] IDENTITY(1,1) NOT NULL,
	[idOrdenCompra] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[CantidadSolicitada] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idOrdenProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tPacientes]    Script Date: 22/11/2024 12:24:32 ******/
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
/****** Object:  Table [dbo].[tProductosTratamiento]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tProductosTratamiento](
	[idProductoTratamiento] [int] IDENTITY(1,1) NOT NULL,
	[idProducto] [int] NOT NULL,
	[idCita] [int] NOT NULL,
	[CantidadUtilizada] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idProductoTratamiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tProveedores]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tProveedores](
	[idProveedor] [int] IDENTITY(1,1) NOT NULL,
	[Empresa] [varchar](100) NOT NULL,
	[NumeroTelefono] [varchar](20) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRecomendacionesProducto]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tRecomendacionesProducto](
	[idRecomendacion] [int] IDENTITY(1,1) NOT NULL,
	[idSeguimiento] [int] NOT NULL,
	[RecomendacionTexto] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[idRecomendacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tRol]    Script Date: 22/11/2024 12:24:32 ******/
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
/****** Object:  Table [dbo].[tSede]    Script Date: 22/11/2024 12:24:32 ******/
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
/****** Object:  Table [dbo].[tSeguimiento]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tSeguimiento](
	[idSeguimiento] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[NombreProducto] [varchar](100) NULL,
	[Estado] [bit] NOT NULL,
	[FechaEntrega] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idSeguimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tSeguimientoProducto]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tSeguimientoProducto](
	[idSeguimiento] [int] IDENTITY(1,1) NOT NULL,
	[idPaciente] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[FechaEntregaEstimada] [date] NULL,
	[Estado] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idSeguimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tServicio]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tServicio](
	[idServicio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](120) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tArchivosPaciente]  WITH CHECK ADD  CONSTRAINT [FK_ArchivoPaciente] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tArchivosPaciente] CHECK CONSTRAINT [FK_ArchivoPaciente]
GO
ALTER TABLE [dbo].[tCalificaciones]  WITH CHECK ADD FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tCalificaciones]  WITH CHECK ADD FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tCalificaciones]  WITH CHECK ADD FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tCalificaciones]  WITH CHECK ADD FOREIGN KEY([idServicio])
REFERENCES [dbo].[tServicio] ([idServicio])
GO
ALTER TABLE [dbo].[tCalificaciones]  WITH CHECK ADD FOREIGN KEY([idServicio])
REFERENCES [dbo].[tServicio] ([idServicio])
GO
ALTER TABLE [dbo].[tCalificaciones]  WITH CHECK ADD FOREIGN KEY([idServicio])
REFERENCES [dbo].[tServicio] ([idServicio])
GO
ALTER TABLE [dbo].[tCitas]  WITH CHECK ADD  CONSTRAINT [FK_CitaPaciente] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tCitas] CHECK CONSTRAINT [FK_CitaPaciente]
GO
ALTER TABLE [dbo].[tCitas]  WITH CHECK ADD  CONSTRAINT [FK_CitaSede] FOREIGN KEY([idSede])
REFERENCES [dbo].[tSede] ([idSede])
GO
ALTER TABLE [dbo].[tCitas] CHECK CONSTRAINT [FK_CitaSede]
GO
ALTER TABLE [dbo].[tCitas]  WITH CHECK ADD  CONSTRAINT [FK_tCitas_tCitasDisponibles] FOREIGN KEY([idCita])
REFERENCES [dbo].[tCitasDisponibles] ([idCitaDisponible])
GO
ALTER TABLE [dbo].[tCitas] CHECK CONSTRAINT [FK_tCitas_tCitasDisponibles]
GO
ALTER TABLE [dbo].[tHistorial]  WITH CHECK ADD  CONSTRAINT [FK_HistorialPaciente] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tHistorial] CHECK CONSTRAINT [FK_HistorialPaciente]
GO
ALTER TABLE [dbo].[tInventario]  WITH CHECK ADD  CONSTRAINT [FK_ProductoProveedor] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[tProveedores] ([idProveedor])
GO
ALTER TABLE [dbo].[tInventario] CHECK CONSTRAINT [FK_ProductoProveedor]
GO
ALTER TABLE [dbo].[tMovimientosInventario]  WITH CHECK ADD  CONSTRAINT [FK_ProductoMovimiento] FOREIGN KEY([idProducto])
REFERENCES [dbo].[tInventario] ([idProducto])
GO
ALTER TABLE [dbo].[tMovimientosInventario] CHECK CONSTRAINT [FK_ProductoMovimiento]
GO
ALTER TABLE [dbo].[tOrdenesCompra]  WITH CHECK ADD  CONSTRAINT [FK_ProductoOrdenCompra] FOREIGN KEY([idProducto])
REFERENCES [dbo].[tInventario] ([idProducto])
GO
ALTER TABLE [dbo].[tOrdenesCompra] CHECK CONSTRAINT [FK_ProductoOrdenCompra]
GO
ALTER TABLE [dbo].[tOrdenesCompra]  WITH CHECK ADD  CONSTRAINT [FK_ProveedorOrdenCompra] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[tProveedores] ([idProveedor])
GO
ALTER TABLE [dbo].[tOrdenesCompra] CHECK CONSTRAINT [FK_ProveedorOrdenCompra]
GO
ALTER TABLE [dbo].[tOrdenesProductos]  WITH CHECK ADD  CONSTRAINT [FK_OrdenCompra] FOREIGN KEY([idOrdenCompra])
REFERENCES [dbo].[tOrdenesCompra] ([idOrdenCompra])
GO
ALTER TABLE [dbo].[tOrdenesProductos] CHECK CONSTRAINT [FK_OrdenCompra]
GO
ALTER TABLE [dbo].[tOrdenesProductos]  WITH CHECK ADD  CONSTRAINT [FK_ProductoOrden] FOREIGN KEY([idProducto])
REFERENCES [dbo].[tInventario] ([idProducto])
GO
ALTER TABLE [dbo].[tOrdenesProductos] CHECK CONSTRAINT [FK_ProductoOrden]
GO
ALTER TABLE [dbo].[tPacientes]  WITH CHECK ADD  CONSTRAINT [FK_PacienteRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[tRol] ([IdRol])
GO
ALTER TABLE [dbo].[tPacientes] CHECK CONSTRAINT [FK_PacienteRol]
GO
ALTER TABLE [dbo].[tProductosTratamiento]  WITH CHECK ADD  CONSTRAINT [FK_CitaTratamiento] FOREIGN KEY([idCita])
REFERENCES [dbo].[tCitas] ([idCita])
GO
ALTER TABLE [dbo].[tProductosTratamiento] CHECK CONSTRAINT [FK_CitaTratamiento]
GO
ALTER TABLE [dbo].[tProductosTratamiento]  WITH CHECK ADD  CONSTRAINT [FK_ProductoTratamiento] FOREIGN KEY([idProducto])
REFERENCES [dbo].[tInventario] ([idProducto])
GO
ALTER TABLE [dbo].[tProductosTratamiento] CHECK CONSTRAINT [FK_ProductoTratamiento]
GO
ALTER TABLE [dbo].[tRecomendacionesProducto]  WITH CHECK ADD  CONSTRAINT [FK_RecomendacionSeguimiento] FOREIGN KEY([idSeguimiento])
REFERENCES [dbo].[tSeguimientoProducto] ([idSeguimiento])
GO
ALTER TABLE [dbo].[tRecomendacionesProducto] CHECK CONSTRAINT [FK_RecomendacionSeguimiento]
GO
ALTER TABLE [dbo].[tSeguimiento]  WITH CHECK ADD  CONSTRAINT [FK_idPaciente] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tSeguimiento] CHECK CONSTRAINT [FK_idPaciente]
GO
ALTER TABLE [dbo].[tSeguimientoProducto]  WITH CHECK ADD  CONSTRAINT [FK_SeguimientoPaciente] FOREIGN KEY([idPaciente])
REFERENCES [dbo].[tPacientes] ([idPaciente])
GO
ALTER TABLE [dbo].[tSeguimientoProducto] CHECK CONSTRAINT [FK_SeguimientoPaciente]
GO
ALTER TABLE [dbo].[tSeguimientoProducto]  WITH CHECK ADD  CONSTRAINT [FK_SeguimientoProducto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[tInventario] ([idProducto])
GO
ALTER TABLE [dbo].[tSeguimientoProducto] CHECK CONSTRAINT [FK_SeguimientoProducto]
GO

USE [ProyectoGraduacion]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarArchivo]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarArchivo]
	@idHistorial INT,
	@Archivo VARCHAR(1000)
AS
BEGIN
	UPDATE dbo.tHistorial
	SET Archivo = @Archivo
	WHERE idHistorial = @idHistorial;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarPaciente]    Script Date: 22/11/2024 12:24:32 ******/
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
/****** Object:  StoredProcedure [dbo].[ActualizarProducto]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarProducto]
	@NombreProducto VARCHAR(100),
	@Cantidad INT,
	@CaducidadProducto DATE,
	@idProveedor INT,
	@idProducto INT
AS
BEGIN
	UPDATE dbo.tInventario
	SET NombreProducto = @NombreProducto,
		Cantidad = @Cantidad,
		CaducidadProducto = @CaducidadProducto,
		idProveedor = @idProveedor
	WHERE idProducto = @idProducto;
END;
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoPaciente]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CambiarEstadoPaciente]
	@idPaciente INT
AS
BEGIN
	-- Borrado lógico
	UPDATE	tPacientes
	SET		Estado = CASE WHEN Estado = 1 THEN 0 ELSE 1 END
	WHERE	idPaciente = @idPaciente
END
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstadoSeguimiento]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CambiarEstadoSeguimiento]
	@idSeguimiento INT
AS
BEGIN
	-- Borrado lógico
	UPDATE	tSeguimiento
	SET		Estado = CASE WHEN Estado = 1 THEN 0 ELSE 1 END
	WHERE	idSeguimiento = @idSeguimiento
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarProducto]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProducto]
	@idProducto INT
AS
BEGIN
	DELETE FROM dbo.tInventario
	WHERE idProducto = @idProducto;
END;
GO
/****** Object:  StoredProcedure [dbo].[GenerarCalificacion]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GenerarCalificacion]
	@Calificaciones	int
    ,@idPaciente	int
    ,@Opinion		text
	,@idServicio	int
	,@Fecha	date
AS
BEGIN

INSERT INTO [dbo].[tCalificaciones]
           (Calificaciones
           ,idPaciente
           ,Opinion
		   ,idServicio
		   ,Fecha)
     VALUES
           (@Calificaciones
           ,@idPaciente
           ,@Opinion
		   ,@idServicio
		   ,@Fecha)

END
GO
/****** Object:  StoredProcedure [dbo].[IniciarSesion]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IniciarSesion]
	@Correo			VARCHAR(100),
	@Contrasenna	VARCHAR(15)
AS
BEGIN
	SELECT	idPaciente, Cedula, Nombre, Correo, Estado, IdRol
	FROM	dbo.tPacientes
	WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[insertar_citasDisponibles]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[insertar_citasDisponibles]
AS
BEGIN
    DECLARE @fecha_hora datetime;
    DECLARE @Estado bit = 1;

    -- Obtener la fecha de mañana a las 07:00 horas
    SET @fecha_hora = DATEADD(hour, 7, DATEADD(day, 1, CAST(CAST(GETDATE() AS date) AS datetime)));

    WHILE DATEPART(HOUR, @fecha_hora) <= 17
    BEGIN
        -- Evitar insertar a las 12 PM
        IF DATEPART(HOUR, @fecha_hora) <> 12
        BEGIN
            PRINT @fecha_hora; -- Depuración: Imprimir el valor de @fecha_hora

            INSERT INTO [dbo].tCitasDisponibles (FECHA, ESTADO)
            VALUES (@fecha_hora, @Estado);
        END

        SET @fecha_hora = DATEADD(hour, 1, @fecha_hora);
    END
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarCita]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarCita]
	@idPaciente int,
	@idSede int,
	@citaDisponible int
AS
BEGIN	
    -- Insertar la nueva cita
    INSERT INTO [dbo].[tCitas]([idPaciente], [idSede], [idCitaDisponible])
    VALUES(@idPaciente, @idSede, @citaDisponible)

    -- Actualizar el estado de la cita disponible a 'false' (0)
    UPDATE [dbo].[tCitasDisponibles]
    SET [Estado] = 0
    WHERE [idCitaDisponible] = @citaDisponible
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarHistorial]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarHistorial]
    @idPaciente INT,
    @FechaConsulta DATE,
    @Diagnostico VARCHAR(200), 
    @Tratamiento VARCHAR(200), 
    @Medicacion VARCHAR(200), 
    @Observaciones VARCHAR(200), 
    @Archivo VARCHAR(1000) 
AS
BEGIN
    INSERT INTO dbo.tHistorial(idPaciente, FechaConsulta, Diagnostico, Tratamiento, Medicacion, Observaciones, Archivo)
    VALUES(@idPaciente, @FechaConsulta, @Diagnostico, @Tratamiento, @Medicacion, @Observaciones, ISNULL(@Archivo, ''));
    SELECT SCOPE_IDENTITY() AS 'idHistorial';
END;
GO
/****** Object:  StoredProcedure [dbo].[RegistrarInventario]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarInventario]
	@NombreProducto VARCHAR(100),
	@Cantidad INT,
	@CaducidadProducto DATE,
	@idProveedor INT
AS
BEGIN
	INSERT INTO dbo.tInventario (NombreProducto, Cantidad, CaducidadProducto, idProveedor)
	VALUES (@NombreProducto, @Cantidad, @CaducidadProducto, @idProveedor);
END;
GO
/****** Object:  StoredProcedure [dbo].[RegistrarPaciente]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarPaciente]
	@Cedula			VARCHAR(20),
	@Nombre			VARCHAR(100),
	@Correo			VARCHAR(100),
	@Contrasenna	VARCHAR(15)
AS
BEGIN
	INSERT INTO dbo.tPacientes(Cedula, Nombre, Correo, Contrasenna, Estado, IdRol)
	VALUES(@Cedula, @Nombre, @Correo, @Contrasenna, 1, 2) -- Se asume que el rol "Usuario" tiene IdRol = 2
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarProveedor]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarProveedor]
	@Empresa VARCHAR(100),
	@Correo VARCHAR(100),
	@NumeroTelefono VARCHAR(20)
AS
BEGIN
	INSERT INTO dbo.tProveedores (Empresa, Correo, NumeroTelefono)
	VALUES (@Empresa, @Correo, @NumeroTelefono);
END;
GO
/****** Object:  StoredProcedure [dbo].[RegistrarSeguimiento]    Script Date: 22/11/2024 12:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarSeguimiento]
	@idPaciente INT,
	@Nombre VARCHAR(100),
	@FechaEntrega DATE
AS
BEGIN
	INSERT INTO dbo.tSeguimientoProducto (idPaciente, idProducto, FechaEntregaEstimada, Estado)
	VALUES (@idPaciente, @Nombre, @FechaEntrega, 'Solicitado');
END;
GO
USE [master]
GO
ALTER DATABASE [ProyectoGraduacion] SET  READ_WRITE 
GO

CREATE PROCEDURE [dbo].[insertar_citasDisponibles]
AS
BEGIN
    SET NOCOUNT ON;
    SET DATEFIRST 1; -- Lunes como primer día de la semana

    DECLARE @Estado BIT = 1;
    DECLARE @fecha_hoy DATE = CAST(GETDATE() AS DATE); -- Solo la fecha de hoy
    DECLARE @fecha_hora DATETIME;
    DECLARE @hora DATETIME;

    -- ======================================
    -- INSERTAR CITAS DEL DÍA DE HOY
    -- ======================================
    DECLARE @diaHoy INT = DATEPART(WEEKDAY, GETDATE());

    IF @diaHoy <> 7 -- No insertar si es domingo
    BEGIN
        SET @hora = DATEADD(HOUR, 9, CAST(GETDATE() AS DATE)); -- Hoy a las 9:00 AM
        DECLARE @horaLimiteHoy INT = CASE 
                                        WHEN @diaHoy = 6 THEN 11 -- sábado: hasta 11 AM
                                        ELSE 17 -- lunes a viernes: hasta 5 PM
                                     END;

        WHILE DATEPART(HOUR, @hora) <= @horaLimiteHoy
        BEGIN
            IF DATEPART(HOUR, @hora) <> 12
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM dbo.tCitasDisponibles WHERE FECHA = @hora)
                BEGIN
                    INSERT INTO dbo.tCitasDisponibles (FECHA, ESTADO)
                    VALUES (@hora, @Estado);
                END
            END
            SET @hora = DATEADD(HOUR, 1, @hora);
        END
    END

    -- ======================================
    -- INSERTAR CITAS DESDE MAÑANA HASTA SÁBADO
    -- ======================================
    SET @fecha_hora = DATEADD(HOUR, 9, DATEADD(DAY, 1, CAST(@fecha_hoy AS DATETIME))); -- 9:00 AM mañana

    DECLARE @hoy INT = DATEPART(WEEKDAY, GETDATE());
    DECLARE @diasRestantes INT = 6 - @hoy;

    WHILE @diasRestantes >= 0
    BEGIN
        DECLARE @diaSemana INT = DATEPART(WEEKDAY, @fecha_hora);

        IF @diaSemana <> 7
        BEGIN
            SET @hora = @fecha_hora;
            DECLARE @horaLimite INT = CASE 
                                        WHEN @diaSemana = 6 THEN 11 -- sábado
                                        ELSE 17 -- lunes a viernes
                                      END;

            WHILE DATEPART(HOUR, @hora) <= @horaLimite
            BEGIN
                IF DATEPART(HOUR, @hora) <> 12
                BEGIN
                    IF NOT EXISTS (SELECT 1 FROM dbo.tCitasDisponibles WHERE FECHA = @hora)
                    BEGIN
                        INSERT INTO dbo.tCitasDisponibles (FECHA, ESTADO)
                        VALUES (@hora, @Estado);
                    END
                END
                SET @hora = DATEADD(HOUR, 1, @hora);
            END
        END

        SET @fecha_hora = DATEADD(DAY, 1, @fecha_hora);
        SET @fecha_hora = DATEADD(HOUR, -DATEPART(HOUR, @fecha_hora), @fecha_hora); -- Reset hora
        SET @fecha_hora = DATEADD(HOUR, 9, @fecha_hora); -- Volver a 9 AM

        SET @diasRestantes = @diasRestantes - 1;
    END
END

