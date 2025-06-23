namespace EcommerceApp.Api.Models;
/// <summary>
/// Representa la información de dirección para envíos y facturación.
/// </summary>
public interface IAddressInfo
{
    /// <summary>
    /// Calle de la dirección.
    /// </summary>
    string Street { get; set; }
    /// <summary>
    /// Dirección completa o adicional.
    /// </summary>
    string Address { get; set; }
    /// <summary>
    /// Ciudad de la dirección.
    /// </summary>
    string City { get; set; }
    /// <summary>
    /// Código postal de la dirección.
    /// </summary>
    string PostalCode { get; set; }
    /// <summary>
    /// Número de teléfono asociado a la dirección.
    /// </summary>
    string PhoneNumber { get; set; }
} 