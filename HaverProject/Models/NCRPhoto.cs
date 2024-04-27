using System.ComponentModel.DataAnnotations;

namespace HaverProject.Models
{
    public class NCRPhoto
    {
        public int ID { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Content { get; set; }

        [StringLength(255)]
        public string MimeType { get; set; }

        public int NCRID { get; set; }
        public NCR NCR { get; set; }
    }
}
