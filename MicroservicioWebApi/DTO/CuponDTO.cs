﻿namespace MicroservicioWebApi.DTO
{
    public class CuponDTO
    {
        public int IdCupon { get; set; }

        public string Codigo { get; set; }

        public double Descuento { get; set; }
        public int CantidadMinima { get; set; }

        public List<Object> ? CuponesL { get; set; }
    }
}
