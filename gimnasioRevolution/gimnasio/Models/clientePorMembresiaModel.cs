namespace gimnasio.Models
{

    public class clientePorMembresiaModel
    {

        public int idCliente { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? membresia { get; set; }
        public DateTime fechaFin { get; set; }

    }

}
