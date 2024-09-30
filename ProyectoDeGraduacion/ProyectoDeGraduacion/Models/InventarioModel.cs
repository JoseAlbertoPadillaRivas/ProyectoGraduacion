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
        public bool RegistrarInventario(Inventario inventario)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.RegistrarInventario(inventario.NombreProducto, inventario.Cantidad, inventario.CaducidadProducto, inventario.idProveedor);
            }

            return (rowsAffected > 0 ? true : false);
        }
        public List<tInventario> ConsultarInventario()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tInventario
                        select x).ToList();
            }
        }

        public tInventario ConsultarProductoID(int idProducto)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                return (from x in context.tInventario
                        where x.idProducto == idProducto
                        select x).FirstOrDefault();
            }
        }

        public bool EliminarProducto(Inventario inventario)
        {
            var rowsAffected = 0;
            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.EliminarProducto(inventario.idProducto);
            }

            return (rowsAffected > 0 ? true : false);
        }

        public bool ActualizarProducto(Inventario inventario)
        {
            var rowsAffected = 0;

            using (var context = new ProyectoGraduacionEntities())
            {
                rowsAffected = context.ActualizarProducto(inventario.NombreProducto,inventario.Cantidad,inventario.CaducidadProducto,inventario.idProveedor, inventario.idProducto);
            }

            return (rowsAffected > 0 ? true : false);
        }
    }
}