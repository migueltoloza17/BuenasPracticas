using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_SOLID
{
    class Program
    {
        public static List<DiasFechasDTO> _lstEventos;
        static void Main(string[] args)
        {
            string cRuta = string.Empty, cError = string.Empty;
            string[] aDatos;
            string[] aDatosEventos;
            _lstEventos = new List<DiasFechasDTO>();
            try
            {
                cRuta = @"D:\CURSOS\Dias_Fechas.txt";
                aDatos = System.IO.File.ReadAllLines(cRuta);
                foreach (string item in aDatos)
                {
                    if (!string.IsNullOrWhiteSpace(item) && item.Contains(","))
                    {
                        aDatosEventos = item.Split(',');
                        if (aDatosEventos.Any())
                        {
                            _lstEventos.Add(new DiasFechasDTO { cEvento = aDatosEventos[0], dtFechaEvento = DateTime.Parse(aDatosEventos[1]) });
                        }
                    }
                }
                if (_lstEventos.Any())
                {
                    ValidarFechas(_lstEventos);
                }
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                Console.WriteLine(cError);
                System.Console.ReadKey();
            }
            Console.WriteLine("Press any key to exit.");
           System.Console.ReadKey();
        }

        /// <summary>
        /// Método que se encarga de validar los dias
        /// </summary>
        /// <param name="lstEventoDias"></param>
        public static void ValidarFechas(List<DiasFechasDTO> _lstEventoDias)
        {
            string cRespuesta = string.Empty;
            DateTime dtValidar = DateTime.Now;
            if (_lstEventoDias.Any())
            {
                foreach (DiasFechasDTO item in _lstEventoDias)
                {

                    cRespuesta = ObtenerDiferenciaTiempo(item.cEvento, dtValidar, item.dtFechaEvento);
                    Console.WriteLine("\t" + cRespuesta);

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_dtValidar"></param>
        /// <param name="_dtOriginal"></param>
        /// <returns></returns>
        public static string ObtenerDiferenciaTiempo(string _cEvento ,DateTime _dtValidar, DateTime _dtOriginal)
        {
            string cRespuesta = string.Empty;
            int iMes = (int)decimal.Zero;
            TimeSpan itiempo = _dtOriginal.Date - _dtValidar.Date;
            TimeSpan itiempot = _dtOriginal - _dtValidar;
            if (itiempo.Days > 0 && Math.Abs(itiempo.Days) < 30)
            {
                cRespuesta = string.Format("{0} ocurrirá dentro de {1} días ", _cEvento, itiempo.Days.ToString());
            }
            else if (itiempo.Days < 0 && Math.Abs(itiempo.Days) < 30)
            {
                cRespuesta = string.Format("{0} ocurrió hace {1}", _cEvento, Math.Abs(itiempo.Days).ToString());
            }
            else if (Math.Abs(itiempo.Days) > 30)
            {
                iMes = Math.Abs((_dtValidar.Month - _dtOriginal.Month) + 12 * (_dtValidar.Year - _dtOriginal.Year));
                cRespuesta = string.Format("{0} ocurrirá dentro de {1} meses ", _cEvento, iMes);
            }
            else if (itiempot.Hours > 0 && itiempot.Hours < 24)
            {
                cRespuesta = string.Format("{0} Ocurrirá dentro de: {1} Horas", _cEvento, itiempot.Hours.ToString());
            }
            else if (itiempot.Hours < 0 && Math.Abs(itiempot.Hours) < 24)
            {
                cRespuesta = string.Format("{0} Ocurrió hace: {1} Horas", _cEvento, Math.Abs(itiempot.Hours).ToString());
            }
            else if (itiempot.Minutes > 0 && itiempot.Minutes < 60)
            {
                cRespuesta = string.Format("{0} Ocurrirá dentro de: {1} Minutos", _cEvento, itiempot.Minutes.ToString());
            }
            else if (itiempot.Minutes < 0 && Math.Abs(itiempot.Minutes) < 60)
            {
                cRespuesta = string.Format("{0} Ocurrió hace: {1} Minutos", _cEvento, Math.Abs(itiempot.Minutes).ToString());
            }
            return cRespuesta;
        }      

    }
}
