using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeGraduacion.Controllers
{
    public class ReabastecimientoController : Controller
    {
        // Vista principal del módulo de Reabastecimiento
        public ActionResult IndexReabastecimiento()
        {
            return View();
        }

        // Vista para Configurar Valores de Reabastecimiento
        public ActionResult ConfigurarValores()
        {
            return View();
        }

        // Vista para Generar Órdenes de Compra
        public ActionResult GenerarOrdenes()
        {
            return View();
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