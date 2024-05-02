﻿namespace MicroservicioWebApi.DTO
{
    public class ResultDTO
    {
        public bool Correct { get; set; } = true;
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public List<Object> Objects { get; set; }
        public object? Object { get; set; }
    }
}
