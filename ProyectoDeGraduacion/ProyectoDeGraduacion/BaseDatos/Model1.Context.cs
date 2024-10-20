﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
        public virtual DbSet<tCitas> tCitas { get; set; }
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
        public virtual DbSet<tSeguimientoProducto> tSeguimientoProducto { get; set; }
    
        public virtual int ActualizarPaciente(string cedula, string nombre, string apellidos, string correo, Nullable<byte> idRol, Nullable<int> idPaciente)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("Cedula", cedula) :
                new ObjectParameter("Cedula", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var idRolParameter = idRol.HasValue ?
                new ObjectParameter("IdRol", idRol) :
                new ObjectParameter("IdRol", typeof(byte));
    
            var idPacienteParameter = idPaciente.HasValue ?
                new ObjectParameter("idPaciente", idPaciente) :
                new ObjectParameter("idPaciente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ActualizarPaciente", cedulaParameter, nombreParameter, apellidosParameter, correoParameter, idRolParameter, idPacienteParameter);
        }
    
        public virtual int CambiarEstadoPaciente(Nullable<int> idPaciente)
        {
            var idPacienteParameter = idPaciente.HasValue ?
                new ObjectParameter("idPaciente", idPaciente) :
                new ObjectParameter("idPaciente", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CambiarEstadoPaciente", idPacienteParameter);
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
    
        public virtual int RegistrarPaciente(string cedula, string nombre, string apellidos, string correo, string contrasenna)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("Cedula", cedula) :
                new ObjectParameter("Cedula", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var apellidosParameter = apellidos != null ?
                new ObjectParameter("Apellidos", apellidos) :
                new ObjectParameter("Apellidos", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var contrasennaParameter = contrasenna != null ?
                new ObjectParameter("Contrasenna", contrasenna) :
                new ObjectParameter("Contrasenna", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarPaciente", cedulaParameter, nombreParameter, apellidosParameter, correoParameter, contrasennaParameter);
        }
    }
}
