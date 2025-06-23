using EcommerceApp.Api.Models;
using EcommerceApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Api.Controllers;
/// <summary>
/// Controlador para operaciones del carrito de compras, pagos y envíos.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CartController
{
    private readonly ICartService _cartService;
    private readonly IPaymentService _paymentService;
    private readonly IShipmentService _shipmentService;
    private readonly IDiscountService _discountService;
    
    /// <summary>
    /// Constructor del controlador CartController.
    /// </summary>
    /// <param name="cartService">Servicio de carrito.</param>
    /// <param name="paymentService">Servicio de pagos.</param>
    /// <param name="shipmentService">Servicio de envíos.</param>
    /// <param name="discountService">Servicio de descuentos.</param>
    public CartController(
      ICartService cartService,
      IPaymentService paymentService,
      IShipmentService shipmentService,
      IDiscountService discountService
    ) 
    {
      _cartService = cartService;
      _paymentService = paymentService;
      _shipmentService = shipmentService;
      _discountService = discountService;
    }

    /// <summary>
    /// Realiza el proceso de checkout: cobra el total (con descuento) y realiza el envío si el pago es exitoso.
    /// </summary>
    /// <param name="card">Tarjeta de pago.</param>
    /// <param name="addressInfo">Dirección de envío.</param>
    /// <returns>"charged" si el pago fue exitoso y se realizó el envío, "not charged" en caso contrario.</returns>
    [HttpPost]
    public string CheckOut(ICard card, IAddressInfo addressInfo) 
    {
        var total = _cartService.Total();
        var discountedTotal = _discountService.ApplyDiscount(total);
        var result = _paymentService.Charge(discountedTotal, card);
        if (result)
        {
            _shipmentService.Ship(addressInfo, _cartService.Items());
            return "charged";
        }
        else {
            return "not charged";
        }
    }    
} 