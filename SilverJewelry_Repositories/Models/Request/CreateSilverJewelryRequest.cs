using SilverJewelry_BOs;
using SilverJewelry_BOs.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SilverJewelry_Repositories.Models.Request
{
    public class CreateSilverJewelryRequest
    {
        [Required]
        [NameValidation]
        public string SilverJewelryName { get; set; } = null!;

        [Required]
        public string? SilverJewelryDescription { get; set; }

        [Required]
        public decimal? MetalWeight { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal? Price { get; set; }

        [Required]
        [YearRange(1900)]
        public int? ProductionYear { get; set; }

        [Required]
        public string? CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
