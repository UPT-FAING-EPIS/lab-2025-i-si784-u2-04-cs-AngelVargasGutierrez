using EcommerceApp.Api.Controllers;
using EcommerceApp.Api.Models;
using EcommerceApp.Api.Services;
using Moq;

namespace EcommerceApp.Tests;
public class ControllerTests
{
      private CartController controller;
      private Mock<IPaymentService> paymentServiceMock;
      private Mock<ICartService> cartServiceMock;

      private Mock<IShipmentService> shipmentServiceMock;
      private Mock<ICard> cardMock;
      private Mock<IAddressInfo> addressInfoMock;
      private List<ICartItem> items;
      private Mock<IDiscountService> discountServiceMock;

      [SetUp]
      public void Setup()
      {
          
          cartServiceMock = new Mock<ICartService>();
          paymentServiceMock = new Mock<IPaymentService>();
          shipmentServiceMock = new Mock<IShipmentService>();
          discountServiceMock = new Mock<IDiscountService>();

          // arrange
          cardMock = new Mock<ICard>();
          addressInfoMock = new Mock<IAddressInfo>();

          // 
          var cartItemMock = new Mock<ICartItem>();
          cartItemMock.Setup(item => item.Price).Returns(10);

          items = new List<ICartItem>()
          {
              cartItemMock.Object
          };

          cartServiceMock.Setup(c => c.Items()).Returns(items.AsEnumerable());
          cartServiceMock.Setup(c => c.Total()).Returns(() => items.Sum(i => i.Price));

          controller = new CartController(cartServiceMock.Object, paymentServiceMock.Object, shipmentServiceMock.Object, discountServiceMock.Object);
      }

      [Test]
      public void ShouldReturnCharged()
      {
          string expected = "charged";
          paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(true);
          discountServiceMock.Setup(d => d.ApplyDiscount(It.IsAny<double>())).Returns<double>(t => t * 0.9);

          // act
          var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

          // assert
          shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Once());
          paymentServiceMock.Verify(p => p.Charge(It.Is<double>(v => v == items.Sum(i => i.Price) * 0.9), cardMock.Object), Times.Once());
          Assert.That(expected, Is.EqualTo(result));
      }

      [Test]
      public void ShouldReturnNotCharged() 
      {
          string expected = "not charged";
          paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(false);
          discountServiceMock.Setup(d => d.ApplyDiscount(It.IsAny<double>())).Returns<double>(t => t * 0.8);

          // act
          var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

          // assert
          shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Never());
          paymentServiceMock.Verify(p => p.Charge(It.Is<double>(v => v == items.Sum(i => i.Price) * 0.8), cardMock.Object), Times.Once());
          Assert.That(expected, Is.EqualTo(result));
      }    

      [TestCase(true, 0.7, "charged")]
      [TestCase(false, 0.5, "not charged")]
      public void ShouldHandleDiscountAndCharge(bool chargeResult, double discountFactor, string expected)
      {
          paymentServiceMock.Setup(p => p.Charge(It.IsAny<double>(), cardMock.Object)).Returns(chargeResult);
          discountServiceMock.Setup(d => d.ApplyDiscount(It.IsAny<double>())).Returns<double>(t => t * discountFactor);

          var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);

          if (chargeResult)
              shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Once());
          else
              shipmentServiceMock.Verify(s => s.Ship(addressInfoMock.Object, items.AsEnumerable()), Times.Never());

          paymentServiceMock.Verify(p => p.Charge(It.Is<double>(v => v == items.Sum(i => i.Price) * discountFactor), cardMock.Object), Times.Once());
          Assert.That(expected, Is.EqualTo(result));
      }
} 