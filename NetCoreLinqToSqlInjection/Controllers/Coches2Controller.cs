﻿using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class Coches2Controller : Controller
    {
        private ICoche car;

        public Coches2Controller(ICoche car)
        {
            this.car = car;
        }

        public IActionResult Index()
        {
            return View(this.car);
        }

        [HttpPost]
        public IActionResult Index(string accion)
        {
            if (accion.ToLower() == "acelerar")
            {
                this.car.Acelerar();
            }
            else
            {
                this.car.Frenar();
            }
            return View(this.car);
        }
    }
}
