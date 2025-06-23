namespace EcommerceApp.Api.Models;
/// <summary>
/// Representa un ítem dentro del carrito de compras.
/// </summary>
public interface ICartItem
{
    /// <summary>
    /// Identificador del producto.
    /// </summary>
    string ProductId { get; set; }
    /// <summary>
    /// Cantidad de productos.
    /// </summary>
    int Quantity { get; set; }
    /// <summary>
    /// Precio unitario del producto.
    /// </summary>
    double Price{ get; set; }
} 