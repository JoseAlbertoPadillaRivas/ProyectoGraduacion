using ProyectoDeGraduacion.BaseDatos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProyectoGraduacion.Models
{
    public class ReabastecimientoModel
    {
        [Key]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreProducto { get; set; }

        [Required]
        public int CantidadActual { get; set; }

        [Required]
        public int NivelMinimoStock { get; set; }

        // Constructor opcional
        public ReabastecimientoModel() { }

        public ReabastecimientoModel(int idProducto, string nombreProducto, int cantidadActual, int nivelMinimoStock)
        {
            IdProducto = idProducto;
            NombreProducto = nombreProducto;
            CantidadActual = cantidadActual;
            NivelMinimoStock = nivelMinimoStock;
        }

        public bool EliminarOrdenCompre(int idOrdenCompra)
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                var orden = context.tOrdenesCompra.FirstOrDefault(p => p.idOrdenCompra == idOrdenCompra);
                if (orden != null)
                {
                    context.tOrdenesCompra.Remove(orden);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
