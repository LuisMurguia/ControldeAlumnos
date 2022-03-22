using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria
        public ActionResult AlumnoGetAll()
        {
            ML.Result result = BL.Alumno.GetAllEF();
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = result.Objects;
            return View(alumno);
        }

        // GET: AlumnoMateria
        public ActionResult MateriasNoAsignadas(int? IdAlumno)
        {

            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();

            ML.Result result = BL.AlumnoMateria.GetByIdMateria(IdAlumno.Value);
            if (result.Correct)
            {
                ML.Result resultAlumno = BL.Alumno.GetByIdLinQ(IdAlumno.Value);
                alumnoMateria.Alumno.Nombre = ((ML.Alumno)resultAlumno.Object).Nombre;
                alumnoMateria.Alumno.ApellidoPaterno = ((ML.Alumno)resultAlumno.Object).ApellidoPaterno;
                alumnoMateria.Alumno.ApellidoMaterno = ((ML.Alumno)resultAlumno.Object).ApellidoMaterno;
                alumnoMateria.Alumno.IdAlumno = ((ML.Alumno)resultAlumno.Object).IdAlumno;
                alumnoMateria.AlumnoMaterias = result.Objects;
                return View(alumnoMateria);
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("Modal");
            }
        }


        [HttpGet]
        public ActionResult MateriasAsignadas(int? IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();

            ML.Result result = BL.AlumnoMateria.GetByIdLinQ(IdAlumno.Value);

            if (result.Correct)
            {
                ML.Result resultAlumno = BL.Alumno.GetByIdLinQ(IdAlumno.Value);
                alumnoMateria.Alumno.Nombre = ((ML.Alumno)resultAlumno.Object).Nombre;
                alumnoMateria.Alumno.ApellidoPaterno = ((ML.Alumno)resultAlumno.Object).ApellidoPaterno;
                alumnoMateria.Alumno.ApellidoMaterno = ((ML.Alumno)resultAlumno.Object).ApellidoMaterno;
                alumnoMateria.Alumno.IdAlumno = ((ML.Alumno)resultAlumno.Object).IdAlumno;
                alumnoMateria.AlumnoMaterias = result.Objects;

                return View(alumnoMateria);
            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("Modal");
            }


        }

        public ActionResult Delete(int IdAlumnoMateria, int IdAlumno)
        {
            ML.Result result = BL.AlumnoMateria.DeleteEF(IdAlumnoMateria);


            return RedirectToAction("MateriasAsignadas", new { IdAlumno = IdAlumno });
        }



        [HttpPost]
        public ActionResult MateriasNoAsignadas(ML.AlumnoMateria alumnoMateria)
        {
            if (alumnoMateria.AlumnoMaterias != null)
            {
                foreach (string IdMateria in alumnoMateria.AlumnoMaterias)
                {
                    ML.AlumnoMateria alumnoMateriaItem = new ML.AlumnoMateria();

                    alumnoMateriaItem.Alumno = new ML.Alumno();
                    alumnoMateriaItem.Alumno.IdAlumno = alumnoMateria.Alumno.IdAlumno;

                    alumnoMateriaItem.Materia = new ML.Materia();
                    alumnoMateriaItem.Materia.IdMateria = Convert.ToInt32(IdMateria);

                    ML.Result result = BL.AlumnoMateria.AddEF(alumnoMateriaItem);
                }
            }
            else
            {

            }
            return RedirectToAction("MateriasAsignadas", new { IdAlumno = alumnoMateria.Alumno.IdAlumno });
        }
    }
}