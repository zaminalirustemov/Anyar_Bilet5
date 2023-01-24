using System.ComponentModel.DataAnnotations;

namespace Anyar_ECommerse.Models
{
    public class Position
    {
        public int Id { get; set; }
        [StringLength(maximumLength:35)]
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public List<Team>? Teams { get; set; }
    }
}
