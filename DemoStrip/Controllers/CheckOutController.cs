using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using StripDemo.Controllers.Request;

namespace StripDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession(PaymentRequest paymentRequest)
        {
            try
            {
                //Create a customer
                //note for hesham  this is the data of the user of my application

               var customerOptions = new CustomerCreateOptions
               {
                   Email = "mohamed@gmail.com",
                   Name = "Test Test"
               };

               var customerService = new CustomerService();
                var customer = customerService.Create(customerOptions);

                // Create a Checkout session
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = paymentRequest.Currency,
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = paymentRequest.Description,
                                },
                                UnitAmount = paymentRequest.Amount,
                                
                            },
                            Quantity = 1,
                        },

                    },
                    Mode = "payment",
                    SuccessUrl = "http://localhost:4200/cms",
                    CancelUrl = "http://localhost:4200/cms/setting",
                    //Customer = customer.Id,
                };

                var service = new SessionService();
                var session = service.Create(options);
                return Ok(new { sessionId = session.Id, SussessUrl = options.SuccessUrl, CancelUrl = options.CancelUrl});
                // if return session.url ==> like paypal 

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("SucessPayment")]
        public ActionResult SucessPayment(string sessionId)
        {
            try
            {

                var service = new SessionService(); 
                var resopnse =  service.Get(sessionId);
                return Ok(resopnse);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
