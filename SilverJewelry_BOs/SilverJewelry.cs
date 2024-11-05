using SilverJewelry_BOs.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SilverJewelry_BOs;

public partial class SilverJewelry
{
    [Required]
    public string SilverJewelryId { get; set; } = null!;

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
    public DateTime? CreatedDate { get; set; }

    [Required]
    public string? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
