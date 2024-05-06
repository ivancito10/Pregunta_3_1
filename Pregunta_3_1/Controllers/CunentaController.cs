using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pregunta_3_1.Models;
using System.Data.SqlClient;
using System.Data;

namespace Pregunta_3_1.Controllers
{
    public class CuentaController : Controller
    {
        //private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        string conexion = "Data Source=DESKTOP-J53QCGG;Initial Catalog=BDIvan;Integrated Security=True";

        private static List<Cuenta> olista = new List<Cuenta>();

        //
        // GET: /Cuenta/
        public ActionResult Inicio()
        {
            olista = new List<Cuenta>();
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT cb.CuentaID, p.Nombre AS NombrePersona, cb.TipoCuenta, cb.Saldo, cb.FechaApertura FROM CuentaBancaria cb INNER JOIN Persona p ON cb.PersonaID = p.PersonaID", oconexion);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cuenta nuevaCuenta = new Cuenta();
                        nuevaCuenta.CuentaID = Convert.ToInt32(dr["CuentaID"]);
                        nuevaCuenta.NombrePersona = dr["NombrePersona"].ToString(); // Aquí asignamos el nombre de la persona en lugar del ID
                        nuevaCuenta.TipoCuenta = dr["TipoCuenta"].ToString();
                        nuevaCuenta.Saldo = Convert.ToInt32(dr["Saldo"]);
                        nuevaCuenta.FechaApertura = Convert.ToDateTime(dr["FechaApertura"]);

                        olista.Add(nuevaCuenta);
                    }
                }
            }
            return View(olista);
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Editar(int? idcuenta)
        {
            if(idcuenta == null)
                return RedirectToAction("Inicio", "Cuenta");
            Cuenta ocuenta = olista.Where(c => c.CuentaID == idcuenta).FirstOrDefault();



            return View(ocuenta);
        }

        [HttpGet]
        public ActionResult Eliminar(int? idcuenta)
        {
            if (idcuenta == null)
                return RedirectToAction("Inicio", "Cuenta");
            Cuenta ocuenta = olista.Where(c => c.CuentaID == idcuenta).FirstOrDefault();



            return View(ocuenta);
        }


        [HttpPost]
        public ActionResult Registrar(Cuenta ocuenta)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("RegistrarCuentaBancaria", oconexion);


                cmd.Parameters.AddWithValue("PersonaID", ocuenta.PersonaID);
                cmd.Parameters.AddWithValue("TipoCuenta", ocuenta.TipoCuenta);
                cmd.Parameters.AddWithValue("Saldo", ocuenta.Saldo);

                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }


            return RedirectToAction("Inicio", "Cuenta");
        }

        [HttpPost]
        public ActionResult Editar(Cuenta ocuenta)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("EditarCuentaBancaria", oconexion);

                cmd.Parameters.AddWithValue("@CuentaID", ocuenta.CuentaID);
                cmd.Parameters.AddWithValue("TipoCuenta", ocuenta.TipoCuenta);
                cmd.Parameters.AddWithValue("Saldo", ocuenta.Saldo);

                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }


            return RedirectToAction("Inicio", "Cuenta");
        }



        [HttpPost]
        public ActionResult Eliminar(string CuentaID)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("EliminarCuentaBancaria", oconexion);

                cmd.Parameters.AddWithValue("@CuentaID", CuentaID);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }


            return RedirectToAction("Inicio", "Cuenta");
        }

       
    }
}