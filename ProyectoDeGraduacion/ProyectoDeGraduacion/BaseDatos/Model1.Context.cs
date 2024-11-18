﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoDeGraduacion.BaseDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ProyectoGraduacionEntities : DbContext
    {
        public ProyectoGraduacionEntities()
            : base("name=ProyectoGraduacionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tArchivosPaciente> tArchivosPaciente { get; set; }
        public virtual DbSet<tCalificaciones> tCalificaciones { get; set; }
        public virtual DbSet<tCitas> tCitas { get; set; }
        public virtual DbSet<tHistorial> tHistorial { get; set; }
        public virtual DbSet<tInventario> tInventario { get; set; }
        public virtual DbSet<tMovimientosInventario> tMovimientosInventario { get; set; }
        public virtual DbSet<tOrdenesCompra> tOrdenesCompra { get; set; }
        public virtual DbSet<tOrdenesProductos> tOrdenesProductos { get; set; }
        public virtual DbSet<tPacientes> tPacientes { get; set; }
        public virtual DbSet<tProductosTratamiento> tProductosTratamiento { get; set; }
        public virtual DbSet<tProveedores> tProveedores { get; set; }
        public virtual DbSet<tRecomendacionesProducto> tRecomendacionesProducto { get; set; }
        public virtual DbSet<tRol> tRol { get; set; }
        public virtual DbSet<tSede> tSede { get; set; }
        public virtual DbSet<tSeguimiento> tSeguimiento { get; set; }
        public virtual DbSet<tSeguimientoProducto> tSeguimientoProducto { get; set; }
        public virtual DbSet<tServicio> tServicio { get; set; }
    
        public virtual int ActualizarArchivo(Nullable<int> idHistorial, string archivo)
        {
            var idHistorialParameter = idHistorial.HasValue ?
                new ObjectParameter("idHistorial", idHistorial) :
                new ObjectParameter("idHistorial", typeof(int));
    
            var archivoParameter = archivo != null ?
                new ObjectParameter("Archivo", archivo) :
                new ObjectParameter("Archivo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarArchivo", idHistorialParameter, archivoParameter);
        }
    
        public virtual int ActualizarPaciente(string cedula, string nombre, string correo, Nullable<byte> idRol, Nullable<int> idPaciente)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("Cedula", cedula) :
                new ObjectParameter("Cedula", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(byte));
    
            var idPacienteParameter = idPaciente.HasValue ?
                new ObjectParameter("idPaciente", idPaciente) :
                new ObjectParameter("idPaciente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarPaciente", cedulaParameter, nombreParameter, correoParameter, idRolParameter, idPacienteParameter);
        }
    
        public virtual int ActualizarProducto(string nombreProducto, Nullable<int> cantidad, Nullable<System.DateTime> caducidadProducto, Nullable<int> idProveedor, Nullable<int> idProducto)
        {
            var nombreProductoParameter = nombreProducto != null ?
                new ObjectParameter("NombreProducto", nombreProducto) :
                new ObjectParameter("NombreProducto", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("Cantidad", cantidad) :
                new ObjectParameter("Cantidad", typeof(int));
    
            var caducidadProductoParameter = caducidadProducto.HasValue ?
                new ObjectParameter("CaducidadProducto", caducidadProducto) :
                new ObjectParameter("CaducidadProducto", typeof(System.DateTime));
    
            var idProveedorParameter = idProveedor.HasValue ?
                new ObjectParameter("idProveedor", idProveedor) :
                new ObjectParameter("idProveedor", typeof(int));
    
            var idProductoParameter = idProducto.HasValue ?
                new ObjectParameter("idProducto", idProducto) :
                new ObjectParameter("idProducto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarProducto", nombreProductoParameter, cantidadParameter, caducidadProductoParameter, idProveedorParameter, idProductoParameter);
        }
    
        public virtual int CambiarEstadoPaciente(Nullable<int> idPaciente)
        {
            var idPacienteParameter = idPaciente.HasValue ?
                new ObjectParameter("idPaciente", idPaciente) :
                new ObjectParameter("idPaciente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CambiarEstadoPaciente", idPacienteParameter);
        }
    
        public virtual int CambiarEstadoSeguimiento(Nullable<int> idSeguimiento)
        {
            var idSeguimientoParameter = idSeguimiento.HasValue ?
                new ObjectParameter("idSeguimiento", idSeguimiento) :
                new ObjectParameter("idSeguimiento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CambiarEstadoSeguimiento", idSeguimientoParameter);
        }
    
        public virtual int EliminarProducto(Nullable<int> idProducto)
        {
            var idProductoParameter = idProducto.HasValue ?
                new ObjectParameter("idProducto", idProducto) :
                new ObjectParameter("idProducto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EliminarProducto", idProductoParameter);
        }
    
        public virtual int GenerarCalificacion(Nullable<int> calificaciones, Nullable<int> idPaciente, string opinion, Nullable<int> idServicio, Nullable<System.DateTime> fecha)
        {
            var calificacionesParameter = calificaciones.HasValue ?
                new ObjectParameter("Calificaciones", calificaciones) :
                new ObjectParameter("Calificaciones", typeof(int));
    
            var idPacienteParameter = idPaciente.HasValue ?
                new ObjectParameter("idPaciente", idPaciente) :
                new ObjectParameter("idPaciente", typeof(int));
    
            var opinionParameter = opinion != null ?
                new ObjectParameter("Opinion", opinion) :
                new ObjectParameter("Opinion", typeof(string));
    
            var idServicioParameter = idServicio.HasValue ?
                new ObjectParameter("idServicio", idServicio) :
                new ObjectParameter("idServicio", typeof(int));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GenerarCalificacion", calificacionesParameter, idPacienteParameter, opinionParameter, idServicioParameter, fechaParameter);
        }
    
        public virtual ObjectResult<IniciarSesion_Result> IniciarSesion(string correo, string contrasenna)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<IniciarSesion_Result>("IniciarSesion", correoParameter, contrasennaParameter);
        }
    
        public virtual int RegistrarCita(Nullable<int> idPaciente, Nullable<int> idSede, Nullable<System.DateTime> fecha, Nullable<System.TimeSpan> hora)
        {
            var idPacienteParameter = idPaciente.HasValue ?
                new ObjectParameter("idPaciente", idPaciente) :
                new ObjectParameter("idPaciente", typeof(int));
    
            var idSedeParameter = idSede.HasValue ?
                new ObjectParameter("idSede", idSede) :
                new ObjectParameter("idSede", typeof(int));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("fecha", fecha) :
                new ObjectParameter("fecha", typeof(System.DateTime));
    
            var horaParameter = hora.HasValue ?
                new ObjectParameter("hora", hora) :
                new ObjectParameter("hora", typeof(System.TimeSpan));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarCita", idPacienteParameter, idSedeParameter, fechaParameter, horaParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> RegistrarHistorial(Nullable<int> idPaciente, Nullable<System.DateTime> fechaConsulta, string diagnostico, string tratamiento, string medicacion, string observaciones, string archivo)
        {
            var idPacienteParameter = idPaciente.HasValue ?
                new ObjectParameter("idPaciente", idPaciente) :
                new ObjectParameter("idPaciente", typeof(int));
    
            var fechaConsultaParameter = fechaConsulta.HasValue ?
                new ObjectParameter("FechaConsulta", fechaConsulta) :
                new ObjectParameter("FechaConsulta", typeof(System.DateTime));
    
            var diagnosticoParameter = diagnostico != null ?
                new ObjectParameter("Diagnostico", diagnostico) :
                new ObjectParameter("Diagnostico", typeof(string));
    
            var tratamientoParameter = tratamiento != null ?
                new ObjectParameter("Tratamiento", tratamiento) :
                new ObjectParameter("Tratamiento", typeof(string));
    
            var medicacionParameter = medicacion != null ?
                new ObjectParameter("Medicacion", medicacion) :
                new ObjectParameter("Medicacion", typeof(string));
    
            var observacionesParameter = observaciones != null ?
                new ObjectParameter("Observaciones", observaciones) :
                new ObjectParameter("Observaciones", typeof(string));
    
            var archivoParameter = archivo != null ?
                new ObjectParameter("Archivo", archivo) :
                new ObjectParameter("Archivo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("RegistrarHistorial", idPacienteParameter, fechaConsultaParameter, diagnosticoParameter, tratamientoParameter, medicacionParameter, observacionesParameter, archivoParameter);
        }
    
        public virtual int RegistrarInventario(string nombreProducto, Nullable<int> cantidad, Nullable<System.DateTime> caducidadProducto, Nullable<int> idProveedor)
        {
            var nombreProductoParameter = nombreProducto != null ?
                new ObjectParameter("NombreProducto", nombreProducto) :
                new ObjectParameter("NombreProducto", typeof(string));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("Cantidad", cantidad) :
                new ObjectParameter("Cantidad", typeof(int));
    
            var caducidadProductoParameter = caducidadProducto.HasValue ?
                new ObjectParameter("CaducidadProducto", caducidadProducto) :
                new ObjectParameter("CaducidadProducto", typeof(System.DateTime));
    
            var idProveedorParameter = idProveedor.HasValue ?
                new ObjectParameter("idProveedor", idProveedor) :
                new ObjectParameter("idProveedor", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarInventario", nombreProductoParameter, cantidadParameter, caducidadProductoParameter, idProveedorParameter);
        }
    
        public virtual int RegistrarPaciente(string cedula, string nombre, string correo, string contrasenna)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("Cedula", cedula) :
                new ObjectParameter("Cedula", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarPaciente", cedulaParameter, nombreParameter, correoParameter, contrasennaParameter);
        }
    
        public virtual int RegistrarProveedor(string empresa, string correo, string numeroTelefono)
        {
            var empresaParameter = empresa != null ?
                new ObjectParameter("Empresa", empresa) :
                new ObjectParameter("Empresa", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var numeroTelefonoParameter = numeroTelefono != null ?
                new ObjectParameter("NumeroTelefono", numeroTelefono) :
                new ObjectParameter("NumeroTelefono", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarProveedor", empresaParameter, correoParameter, numeroTelefonoParameter);
        }
    
        public virtual int RegistrarSeguimiento(Nullable<int> idPaciente, string nombre, Nullable<System.DateTime> fechaEntrega)
        {
            var idPacienteParameter = idPaciente.HasValue ?
                new ObjectParameter("idPaciente", idPaciente) :
                new ObjectParameter("idPaciente", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var fechaEntregaParameter = fechaEntrega.HasValue ?
                new ObjectParameter("FechaEntrega", fechaEntrega) :
                new ObjectParameter("FechaEntrega", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarSeguimiento", idPacienteParameter, nombreParameter, fechaEntregaParameter);
        }
    }
}
