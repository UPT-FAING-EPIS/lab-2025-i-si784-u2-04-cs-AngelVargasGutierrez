namespace EcommerceApp.Api.Services;
/// <summary>
/// Servicio para aplicar descuentos sobre el monto total.
/// </summary>
public interface IDiscountService
{
    /// <summary>
    /// Aplica un descuento al monto total.
    /// </summary>
    /// <param name="total">Monto total antes del descuento.</param>
    /// <returns>Monto total después de aplicar el descuento.</returns>
    double ApplyDiscount(double total);
} 