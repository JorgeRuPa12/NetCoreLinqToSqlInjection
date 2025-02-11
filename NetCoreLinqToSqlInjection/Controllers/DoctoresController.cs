using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlInjection.Models;
using NetCoreLinqToSqlInjection.Repositories;

namespace NetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        IRepositoryDoctores repo;

        public DoctoresController(IRepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Doctor doc)
        {
            this.repo.InsertDoctor(doc.IdDoctor, doc.Apellido
                , doc.Especialidad, doc.Salario, doc.IdHospital);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int iddoctor)
        {
            this.repo.DeleteDoctor(iddoctor);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int iddoctor)
        {
            Doctor doc = this.repo.FindDoctor(iddoctor);
            return View(doc);
        }

        [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            this.repo.UpdateDoctor(doctor);
            return RedirectToAction("Index");
        }

        public IActionResult BuscarDoctores()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BuscarDoctores(string especialidad)
        {
            List<Doctor> doctores = this.repo.BuscarDoctores(especialidad);
            return View(doctores);
        }
    }
}
