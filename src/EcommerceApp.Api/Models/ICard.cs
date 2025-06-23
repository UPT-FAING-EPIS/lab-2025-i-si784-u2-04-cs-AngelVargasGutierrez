namespace EcommerceApp.Api.Models;
/// <summary>
/// Representa la información de una tarjeta de pago.
/// </summary>
public interface ICard
{
    /// <summary>
    /// Número de la tarjeta.
    /// </summary>
    string CardNumber { get; set; }
    /// <summary>
    /// Nombre del titular de la tarjeta.
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// Fecha de vencimiento de la tarjeta.
    /// </summary>
    DateTime ValidTo { get; set; }
} 