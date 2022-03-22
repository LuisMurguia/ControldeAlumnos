using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result GetByIdLinQ(int IdAlumno)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {


                    var Query = context.MateriasAlumnoGetById(IdAlumno).ToList();
                    result.Objects = new List<object>();

                    if (Query != null)
                    {
                        foreach (var obj in Query)
                        {
                            ML.AlumnoMateria alumnomateria = new ML.AlumnoMateria();
                            alumnomateria.IdAlumnoMateria = obj.IdAlumnoMateria;

                            alumnomateria.Materia = new ML.Materia();
                            alumnomateria.Materia.Nombre = obj.NombreMateria;
                            alumnomateria.Materia.IdMateria = obj.IdMateria;
                            alumnomateria.Materia.Costo = Convert.ToDecimal(obj.Costo);

                            alumnomateria.Alumno = new ML.Alumno();
                            alumnomateria.Alumno.Nombre = obj.NombreAlumno;
                            alumnomateria.Alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumnomateria.Alumno.ApellidoMaterno = obj.ApellidoMaterno;
                            alumnomateria.Alumno.IdAlumno = obj.IdAlumno;
                            result.Objects.Add(alumnomateria);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result DeleteEF(int IdAlumnoMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {
                    var Query = context.AlumnoMateriaDelete(IdAlumnoMateria);
                    if (Query > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el Alumno";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;

        }

        public static ML.Result GetByIdMateria(int IdAlumno)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {


                    var Query = context.MateriasNoAsignadas(IdAlumno).ToList();
                    result.Objects = new List<object>();

                    if (Query != null)
                    {
                        foreach (var obj in Query)
                        {
                            ML.AlumnoMateria alumnomateria = new ML.AlumnoMateria();
                            alumnomateria.Materia = new ML.Materia();
                            alumnomateria.Materia.Nombre = obj.Nombre;
                            alumnomateria.Materia.IdMateria = obj.IdMateria;
                            alumnomateria.Materia.Costo = Convert.ToDecimal(obj.Costo);

                            result.Objects.Add(alumnomateria);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result AddEF(ML.AlumnoMateria alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {
                    var Query = context.AlumnoMateriaAdd(alumno.Alumno.IdAlumno, alumno.Materia.IdMateria);

                    if (Query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo ingresar la materia";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }
    }
}
