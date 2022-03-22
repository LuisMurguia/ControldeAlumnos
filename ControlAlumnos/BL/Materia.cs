using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetAllWebAPI()
        {
            ML.Result result = new ML.Result();
            try
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.GetAsync("Materia");
                    responseTask.Wait();
                    result.Objects = new List<object>();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
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


        public static ML.Result AddWebAPI(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.PostAsJsonAsync<ML.Materia>("Materia/Add", materia);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        result.Correct = true;

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

        public static ML.Result UpdateWebAPI(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.PutAsJsonAsync<ML.Materia>("Materia/Update/" + materia.IdMateria, materia);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        result.Correct = true;

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

        public static ML.Result DeleteWebAPI(int IdMateria)
        {
            ML.Result result = new ML.Result();
            //int IdDepartamento = departamento.IdDepartamento;
            try
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];
                using (var client = new HttpClient())
                {
                    //int IdDepartamento = departamento.IdDepartamento;
                    client.BaseAddress = new Uri(urlAPI);
                    //HTTP POST
                    var potTask = client.DeleteAsync("Materia/Delete/" + IdMateria);
                    potTask.Wait();
                    var resultAPI = potTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        result.Correct = true;
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

        public static ML.Result GetByIdWebAPI(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["URLapi"];


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.GetAsync("Materia/" + IdMateria);
                    responseTask.Wait();

                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Materia resultItemList = new ML.Materia();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron los registros";
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




        public static ML.Result AddEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {
                    var Query = context.MateriaAdd(materia.Nombre, materia.Costo);

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

        public static ML.Result UpdateEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {
                    var Query = context.MateriaUpdate(materia.IdMateria,materia.Nombre, materia.Costo);

                    if (Query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar la materia";
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

        public static ML.Result DeleteEF(int IdMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {
                    var Query = context.MateriaDelete(IdMateria);
                    if (Query > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar ña materia";
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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {
                    var Query = context.MateriaGetAll().ToList();
                    result.Objects = new List<object>();

                    if (Query != null)
                    {
                        foreach (var obj in Query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Costo = Convert.ToDecimal(obj.Costo);

                            result.Objects.Add(materia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se mostraron los registros";
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

        public static ML.Result GetByIdLinQ(int IdMateria)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.ControlAlumnosEntities context = new DL.ControlAlumnosEntities())
                {

                    var obj = (from mate in context.Materias
                               where mate.IdMateria == IdMateria
                               select mate).FirstOrDefault();
                    //result.Objects = new List<object>();

                    if (obj != null)
                    {

                        ML.Materia materia = new ML.Materia();

                        materia.IdMateria = obj.IdMateria;
                        materia.Nombre = obj.Nombre;
                        materia.Costo = Convert.ToDecimal(obj.Costo);

                        result.Object = materia;

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
    }
}
