using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoDeGraduacion.Models
{
    public class InventarioModel
    {
        // Método para registrar un nuevo producto en el inventario usando LINQ
        public bool RegistrarInventario(Inventario inventario)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                tInventario nuevoProducto = new tInventario
                {
                    NombreProducto = inventario.NombreProducto,
                    Cantidad = inventario.Cantidad,
                    CaducidadProducto = inventario.CaducidadProducto,
                    idProveedor = inventario.idProveedor,
                    NivelMinimoStock = inventario.NivelMinimoStock  // Aseguramos que el nuevo campo se use
                };

                context.tInventario.Add(nuevoProducto);
                context.SaveChanges(); // Guardamos los cambios en la base de datos
            }

            return true;
        }

        // Método para consultar todo el inventario usando LINQ
        public List<Inventario> ConsultarInventario()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var inventario = (from inv in context.tInventario
                                  join prov in context.tProveedores on inv.idProveedor equals prov.idProveedor
                                  select new Inventario
                                  {
                                      idProducto = inv.idProducto,
                                      NombreProducto = inv.NombreProducto,
                                      Cantidad = inv.Cantidad,
                                      CaducidadProducto = inv.CaducidadProducto,
                                      idProveedor = inv.idProveedor,
                                      NivelMinimoStock = inv.NivelMinimoStock,
                                      NombreProveedor = prov.Empresa // Rellenamos el nombre del proveedor
                                  }).ToList();
                return inventario;
            }
        }

        // Método para consultar un producto en específico por su ID
        public tInventario ConsultarProductoID(int idProducto)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tInventario
                        where x.idProducto == idProducto
                        select x).FirstOrDefault();
            }
        }

        public bool VerificarRelacionOrden(int idProducto)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                // Verificar si hay órdenes relacionadas en estado "Pendiente"
                return context.tOrdenesCompra.Any(o => o.EstadoOrden == "Pendiente");
            }
        }

        public bool EliminarProducto(int idProducto)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                // Validar si tiene órdenes relacionadas en estado "Pendiente"
                if (VerificarRelacionOrden(idProducto))
                {
                    return false; // No eliminar si hay órdenes en estado "Pendiente"
                }

                // Eliminar el producto si no hay restricciones
                var producto = context.tInventario.FirstOrDefault(p => p.idProducto == idProducto);
                if (producto != null)
                {
                    context.tInventario.Remove(producto);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        // Método para actualizar un producto existente en el inventario usando LINQ
        public bool ActualizarProducto(Inventario inventario)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var producto = context.tInventario.FirstOrDefault(p => p.idProducto == inventario.idProducto);

                if (producto != null)
                {
                    producto.NombreProducto = inventario.NombreProducto;
                    producto.Cantidad = inventario.Cantidad;
                    producto.CaducidadProducto = inventario.CaducidadProducto;
                    producto.idProveedor = inventario.idProveedor;
                    producto.NivelMinimoStock = inventario.NivelMinimoStock;  // Actualizamos el nivel mínimo de stock

                    context.SaveChanges(); // Guardamos los cambios en la base de datos
                    return true;
                }
                return false;          
            }
        }

        public bool RegistrarProveedor(Proveedor proveedor)
        {
            try
            {
                var rowsAffected = 0;

                using (var context = new ProyectoGraduacionEntities())
                {
                    rowsAffected = context.RegistrarProveedor(proveedor.Empresa, proveedor.NumeroTelefono, proveedor.Correo);
                }                

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el proveedor: " + ex.Message);
            }
        }



        public List<tProveedores> ConsultarProveedores()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tProveedores                        
                        select x).ToList();
            }
        }

        public bool ActualizarProveedor(Proveedor proveedor)
        {
            try
            {
                using (var context = new ProyectoGraduacionEntities())
                {
                    var prov = context.tProveedores.FirstOrDefault(p => p.idProveedor == proveedor.idProveedor);

                    if (prov != null)
                    {
                        prov.Empresa = proveedor.Empresa;
                        prov.NumeroTelefono = proveedor.NumeroTelefono;
                        prov.Correo = proveedor.Correo;

                        context.SaveChanges(); // Guardamos los cambios en la base de datos
                        return true;
                    }
                    return false; // Si no se encuentra el proveedor
                }
            }
            catch (Exception ex)
            {
                // Registrar o manejar la excepción según sea necesario
                throw new Exception("Error al actualizar el proveedor: " + ex.Message, ex);
            }
        }

        public bool CambiarEstadoProveedor(Proveedor proveedor)
        {
            var rowsAffected = 0;
            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.CambiarEstadoProveedor(proveedor.idProveedor);
            }

            return (rowsAffected > 0 ? true : false);
        }

    }
}
