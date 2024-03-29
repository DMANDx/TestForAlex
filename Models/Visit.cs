using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Avto1Test.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public string? UserAgent { get; set; }
        public string? RemoteAddr { get; set; }
        public DateTime VisitDate { get; set; }
    }
}
