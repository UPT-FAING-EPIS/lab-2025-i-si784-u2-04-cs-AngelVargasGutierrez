using EcommerceApp.Api.Models;
namespace EcommerceApp.Api.Services;
/// <summary>
/// Servicio para gestionar el carrito de compras.
/// </summary>
public interface ICartService
{
    /// <summary>
    /// Calcula el total del carrito.
    /// </summary>
    /// <returns>Total en valor monetario.</returns>
    double Total();
    /// <summary>
    /// Obtiene los ítems actuales del carrito.
    /// </summary>
    /// <returns>Lista de ítems en el carrito.</returns>
    IEnumerable<ICartItem> Items();         
} 