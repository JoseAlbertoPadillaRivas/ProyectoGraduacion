﻿using ProyectoDeGraduacion.BaseDatos;
using ProyectoDeGraduacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    public class ReabastecimientoController : Controller
    {
        private ProyectoGraduacionEntities db = new ProyectoGraduacionEntities(); // Contexto de la base de datos

        // Vista principal del módulo de Reabastecimiento
        public ActionResult IndexReabastecimiento()
        {
            return View();
        }

        // Vista para Configurar Valores de Reabastecimiento
        public ActionResult ConfigurarValores()
        {
            InventarioModel inventarioModel = new InventarioModel();
            var productos = inventarioModel.ConsultarInventario();
            return View(productos);
        }

        // Método POST para actualizar los valores mínimos de reabastecimiento
        [HttpPost]
        public ActionResult ConfigurarValores(List<tInventario> updatedProducts)
        {
            foreach (var updatedProduct in updatedProducts)
            {
                var product = db.tInventario.FirstOrDefault(p => p.idProducto == updatedProduct.idProducto);
                if (product != null)
                {
                    product.NivelMinimoStock = updatedProduct.NivelMinimoStock; // Actualizamos nivel mínimo de stock
                }
            }

            db.SaveChanges(); // Guardamos los cambios en la base de datos
            TempData["SuccessMessage"] = "Los valores de reabastecimiento se han configurado correctamente.";
            return RedirectToAction("ConfigurarValores");
        }


        // Vista para Generar Órdenes de Compra
        public ActionResult GenerarOrdenes()
        {
            using (var context = new ProyectoGraduacionEntities())
            {
                // Obtener productos y proveedores
                var productos = context.tInventario.ToList();
                var proveedores = context.tProveedores.ToList();

                // Pasar los productos y proveedores a la vista usando ViewBag
                ViewBag.Proveedores = proveedores;
                return View(productos);
            }
        }

        [HttpPost]
        public ActionResult GenerarOrdenes(FormCollection form)
        {
            using (var db = new ProyectoGraduacionEntities())
            {
                // Inicializamos variables de control
                bool ordenGenerada = false;

                // Recorrer los productos del inventario (deberían venir del modelo)
                foreach (var key in form.AllKeys)
                {
                    if (key.StartsWith("cantidad_")) // Identificar inputs que tienen la cantidad de productos
                    {
                        // Obtener el idProducto a partir del nombre del campo (después de 'cantidad_')
                        var idProductoStr = key.Replace("cantidad_", "");
                        int idProducto = int.Parse(idProductoStr);

                        // Obtener la cantidad solicitada
                        var cantidadString = form[key];
                        if (string.IsNullOrEmpty(cantidadString) || cantidadString == "0")
                        {
                            continue; // Si la cantidad es nula o 0, pasamos al siguiente producto
                        }
                        int cantidadSolicitada = int.Parse(cantidadString);

                        // Obtener el proveedor relacionado a este producto
                        var proveedorString = form["proveedor_" + idProductoStr];
                        if (string.IsNullOrEmpty(proveedorString))
                        {
                            continue; // Si no hay proveedor, pasamos al siguiente producto
                        }
                        int idProveedor = int.Parse(proveedorString);

                        // Crear una nueva orden de compra solo si no se ha creado ya
                        if (!ordenGenerada)
                        {
                            tOrdenesCompra nuevaOrdenCompra = new tOrdenesCompra
                            {
                                idProveedor = idProveedor,
                                FechaSolicitud = DateTime.Now,
                                EstadoOrden = "Pendiente"
                            };

                            db.tOrdenesCompra.Add(nuevaOrdenCompra);
                            db.SaveChanges(); // Guardamos para obtener el idOrdenCompra generado

                            // Marcamos que ya se generó la orden
                            ordenGenerada = true;
                        }

                        // Ahora que ya tenemos la orden, podemos agregar los productos a la tabla tOrdenesProductos
                        tOrdenesProductos nuevaOrdenProducto = new tOrdenesProductos
                        {
                            idOrdenCompra = db.tOrdenesCompra.OrderByDescending(o => o.idOrdenCompra).FirstOrDefault().idOrdenCompra, // Última orden de compra generada
                            idProducto = idProducto,
                            CantidadSolicitada = cantidadSolicitada
                        };

                        db.tOrdenesProductos.Add(nuevaOrdenProducto);
                    }
                }

                // Guardar los cambios en la base de datos
                db.SaveChanges();

                // Redirigir o mostrar mensaje de éxito
                TempData["mensaje"] = "Las órdenes de compra han sido generadas correctamente.";
                return RedirectToAction("ConfirmarRecepcion", "Reabastecimiento");
            }
        }


        // Vista para Confirmar Recepción de Productos
        public ActionResult ConfirmarRecepcion()
        {
            return View();
        }

        // Vista para el Historial de Reabastecimientos
        public ActionResult HistorialReabastecimiento()
        {
            return View();
        }
    }
}