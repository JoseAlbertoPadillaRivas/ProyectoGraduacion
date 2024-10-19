using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Entidades;
using System.Collections.Generic;
using System.Linq;

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
        public List<tInventario> ConsultarInventario()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tInventario
                        select x).ToList();
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

        // Método para eliminar un producto del inventario usando LINQ
        public bool EliminarProducto(int idProducto)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
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
    }
}
