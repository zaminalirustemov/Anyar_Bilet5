using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Anyar_ECommerse.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int PositionId { get; set; }

        [StringLength(maximumLength: 100)]
        public string? ImageName { get; set; }
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }
        [StringLength(maximumLength: 500)]
        public string? TwUrl { get; set; }
        [StringLength(maximumLength: 500)]
        public string? FbUrl { get; set; }
        [StringLength(maximumLength: 500)]
        public string? InstUrl { get; set; }
        [StringLength(maximumLength: 500)]
        public string? InUrl { get; set; }
        public bool IsDelete { get; set; }

        public Position? Position { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
