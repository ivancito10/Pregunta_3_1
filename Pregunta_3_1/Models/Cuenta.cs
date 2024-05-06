using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pregunta_3_1.Models
{
    public class Cuenta
    {
        public int CuentaID { get; set; }
        public int PersonaID { get; set; }
        public string NombrePersona { get; set; } // Cambio aquí para reflejar el nombre de la persona en lugar del ID
        public string TipoCuenta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaApertura { get; set; }
    }
}