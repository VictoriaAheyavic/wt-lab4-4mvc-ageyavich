using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_ageyavich.Models;

namespace wt_lab4_4mvc_ageyavich.Controllers;

public class ProductController : Controller
{
    // Статический список — данные в памяти, общий для всех запросов
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Ноутбук", Category = "Компьютеры", Price = 2500m, Stock = 5, IsAvailable = true, Description = "Мощный ноутбук для работы" },
        new Product { Id = 2, Name = "Мышь", Category = "Аксессуары", Price = 80m, Stock = 30, IsAvailable = true, Description = "Беспроводная мышь" },
        new Product { Id = 3, Name = "Клавиатура", Category = "Аксессуары", Price = 150m, Stock = 0, IsAvailable = false, Description = "Механическая клавиатура" },
        new Product { Id = 4, Name = "Монитор", Category = "Компьютеры", Price = 350m, Stock = 10, IsAvailable = true, Description = "27\" 4K монитор" },
        new Product { Id = 5, Name = "Наушники", Category = "Аудио", Price = 120m, Stock = 15, IsAvailable = true, Description = "Шумоподавляющие" },
    };

    private static int _nextId = 6;

    /// <summary>
    /// GET /Product/Index — список всех товаров.
    /// </summary>
    public IActionResult Index()
    {
        return View(_products);
    }

    /// <summary>
    /// GET /Product/Details/5 — подробности о товаре.
    /// </summary>
    public IActionResult Details(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }

    /// <summary>
/// GET /Product/Create — показать форму.
/// </summary>
public IActionResult Create()
{
    return View();
}

/// <summary>
/// POST /Product/Create — сохранить новый товар.
/// </summary>
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Product product)
{
    if (!ModelState.IsValid)
    {
        return View(product);   // вернуть с ошибками
    }

    product.Id = _nextId++;
    _products.Add(product);
    return RedirectToAction(nameof(Index));
}

/// <summary>
/// GET /Product/Edit/5 — показать форму редактирования.
/// </summary>
public IActionResult Edit(int id)
{
    var product = _products.FirstOrDefault(p => p.Id == id);
    if (product == null) return NotFound();
    return View(product);
}

/// <summary>
/// POST /Product/Edit/5 — сохранить изменения.
/// </summary>
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(int id, Product product)
{
    if (id != product.Id) return BadRequest();

    if (!ModelState.IsValid) return View(product);

    var existing = _products.FirstOrDefault(p => p.Id == id);
    if (existing == null) return NotFound();

    existing.Name = product.Name;
    existing.Category = product.Category;
    existing.Price = product.Price;
    existing.Stock = product.Stock;
    existing.IsAvailable = product.IsAvailable;
    existing.Description = product.Description;

    return RedirectToAction(nameof(Index));
}
}