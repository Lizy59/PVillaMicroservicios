namespace MicroservicioAuthenticationApi.DTO
{
	public class ResultadoDTO
	{
		public bool Success { get; set; } = true;
		public string? Message { get; set; }
		public Exception? Exception { get; set; }
		public object? Object { get; set; }
		public List<object>? Objects { get; set; }
	}
}
