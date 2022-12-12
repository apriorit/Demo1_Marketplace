using Microsoft.AspNetCore.Mvc;
using Marketplace.Integrations.Payments.Stripe.Models;
using System.Diagnostics;

namespace Marketplace.Integrations.Payments.Stripe.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ILogger<CheckoutController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string clientSecret, string publicKey)
        {
            ViewData["ClientSecret"] = clientSecret;
            ViewData["ReturnURL"] = $"{Request.Scheme}://{Request.Host}";
            ViewData["PublicKey"] = publicKey;
            return View();
        }

        public IActionResult CheckoutSuccess()
        {
            return View();
        }
    }
}