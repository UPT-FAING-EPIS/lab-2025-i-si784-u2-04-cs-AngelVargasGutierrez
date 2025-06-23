using EcommerceApp.Api.Models;
namespace EcommerceApp.Api.Services;
/// <summary>
/// Servicio para procesar pagos.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Realiza el cobro de un monto a una tarjeta.
    /// </summary>
    /// <param name="total">Monto total a cobrar.</param>
    /// <param name="card">Tarjeta a utilizar.</param>
    /// <returns>True si el cobro fue exitoso, false en caso contrario.</returns>
    bool Charge(double total, ICard card);      
} 