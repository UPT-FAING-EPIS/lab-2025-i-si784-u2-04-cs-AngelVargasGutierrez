using EcommerceApp.Api.Models;
namespace EcommerceApp.Api.Services;
/// <summary>
/// Servicio para gestionar envíos de productos.
/// </summary>
public interface IShipmentService
{
    /// <summary>
    /// Realiza el envío de los ítems a la dirección indicada.
    /// </summary>
    /// <param name="info">Información de la dirección de envío.</param>
    /// <param name="items">Lista de ítems a enviar.</param>
    void Ship(IAddressInfo info, IEnumerable<ICartItem> items);
} 