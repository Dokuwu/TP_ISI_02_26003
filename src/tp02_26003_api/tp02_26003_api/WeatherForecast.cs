namespace tp02_26003_api
{
    /// <summary>
    /// Classe de weather
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// data do weather
        /// </summary>
        public DateOnly Date { get; set; }
        /// <summary>
        /// temperature
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// temperatura farenheint
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// sumario sobre o weather
        /// </summary>
        public string? Summary { get; set; }
    }
}
