using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HaverProject.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public ICollection<NCR> Ncrs { get; set; } = new HashSet<NCR>();
    }
}
