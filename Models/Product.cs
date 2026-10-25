using System.ComponentModel.DataAnnotations;

namespace wt_lab4_4mvc_ageyavich.Models;

/// <summary>
/// Товар интернет-магазина.
/// </summary>
public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название обязательно")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Категория обязательна")]
    public string Category { get; set; } = string.Empty;

    [Range(0.01, 1000000, ErrorMessage = "Цена должна быть от 0.01 до 1 000 000")]
    public decimal Price { get; set; }

    public bool IsAvailable { get; set; } = true;

    public int Stock { get; set; }

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}