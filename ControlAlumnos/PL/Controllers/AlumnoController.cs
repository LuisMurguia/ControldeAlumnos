using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        //GET: Alumno
        public ActionResult GetAll()
        {


            ServiceAlumno.AlumnoClient ServicioAlumno = new ServiceAlumno.AlumnoClient();

            var result = ServicioAlumno.GetAll();  //SL.Result
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = new List<object>();

            foreach (var obj in result.Objects)
            {
                ML.Alumno alumnoItem = new ML.Alumno();
                alumnoItem = ((ML.Alumno)obj);

                //alumnoItem.IdAlumno = ((ML.Alumno)obj).IdAlumno;
                alumno.Alumnos.Add(alumnoItem);
            }

            return View(alumno);
        }


        [HttpGet]//Mostrar el formulario
        public ActionResult Form(int? IdAlumno)
        {
            ServiceAlumno.AlumnoClient ServicioAlumno = new ServiceAlumno.AlumnoClient();
            ML.Alumno alumno = new ML.Alumno();

            if (IdAlumno == null)
            {
                return View(alumno);
            }
            else
            {

                var result = ServicioAlumno.GetById(IdAlumno.Value);

                if (result.Correct)
                {
                    alumno.IdAlumno = ((ML.Alumno)result.Object).IdAlumno;
                    alumno.Nombre = ((ML.Alumno)result.Object).Nombre;
                    alumno.ApellidoPaterno = ((ML.Alumno)result.Object).ApellidoPaterno;
                    alumno.ApellidoMaterno = ((ML.Alumno)result.Object).ApellidoMaterno;

                    return View(alumno);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }


            }

        }// Aqui termina 

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            ServiceAlumno.AlumnoClient ServicioAlumno = new ServiceAlumno.AlumnoClient();
            //ML.Result result = new ML.Result();
            if (alumno.IdAlumno == 0)
            {
                var result = ServicioAlumno.Add(alumno);
                ViewBag.Message = "La materia se agregó correctamente ";
            }
            else
            {
                var result = ServicioAlumno.Update(alumno);
                ViewBag.Message = "La materia se actualizó correctamente ";

            }

            return PartialView("Modal");
        }//aqui termina


        [HttpGet]
        public ActionResult Delete(int IdAlumno)
        {
            ServiceAlumno.AlumnoClient ServicioAlumno = new ServiceAlumno.AlumnoClient();
            var result = ServicioAlumno.Delete(IdAlumno);
            return RedirectToAction("GetAll");
        }

    }


}