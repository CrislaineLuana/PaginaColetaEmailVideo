namespace PaginaColetaEmailVideo.Models
{
    public class EmailModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime DataDeRegistro { get; set; } = DateTime.Now;
    }
}
