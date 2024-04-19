﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WazeCredit.Data;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Service;
using WazeCredit.Utility.AppSettingsClasses;

namespace WazeCredit.Controllers
{
    public class HomeController : Controller
    {
        public HomeViewModel ViewModel { get; set; }
        private readonly IMarketForecaster _marketForecaster;
        private readonly ICreditValidator _creditValidator;
        private readonly ApplicationDbContext _db;
        private readonly ILogger _logger;

        [BindProperty]
        public CreditApplication CreditModel { get; set; }

        public HomeController(IMarketForecaster marketForecaster, 
            ICreditValidator creditValidator,
            ApplicationDbContext db,
            ILogger<HomeController> logger)
        {
            ViewModel = new HomeViewModel();
            _marketForecaster = marketForecaster;
            _creditValidator = creditValidator;
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Home Controller Index Action Called");
            MarketResult currentMarket = _marketForecaster.GetMarketPrediction();

            switch (currentMarket.MarketCondition)
            {
                case MarketCondition.StableDown:
                    ViewModel.MarketForecast = "Market shows signs to go down in a stable state! It is a not a good sign to apply for credit applications. But extra credit is always piece of mind if you have handy when you need it.";
                    break;
                case MarketCondition.StableUp:
                    ViewModel.MarketForecast = "Market shows signs to go up in a stable state. It is a great sign to apply for credit applications!";
                    break;
                case MarketCondition.Volatile:
                    ViewModel.MarketForecast = "Market shows signs of volatility. In uncertain times, it is good to have credit handy if you need extra funds.";
                    break;
                default:
                    ViewModel.MarketForecast = "Please apply for a credit card using our application.";
                    break;
            }

            _logger.LogInformation("Home Controller Index Action Ended");
            return View(ViewModel);
        }

        public IActionResult AllConfigSettings(
            [FromServices] IOptions<StripeSettings> stripeOptions,
            [FromServices] IOptions<WazeForecastSettings> wazeOptions,
            [FromServices] IOptions<TwilioSettings> twilioOptions,
            [FromServices] IOptions<SendGridSettings> sendGridOptions
            )
        {
            List<string> messages = new List<string>();
            messages.Add($"Waze Config - Forecast Tracker: " + wazeOptions.Value.ForecastTrackerEnabled);
            messages.Add($"Stripe Publishable Key: " + stripeOptions.Value.PublishableKey);
            messages.Add($"Stripe Secret Key: " + stripeOptions.Value.SecretKey);
            messages.Add($"Twilio Phone: " + twilioOptions.Value.PhoneNumber);
            messages.Add($"Twilio Token: " + twilioOptions.Value.AuthToken);
            messages.Add($"Twilio SID: " + twilioOptions.Value.AccountSid);
            messages.Add($"Send Grid Key: " + sendGridOptions.Value.SendGridKey);

            return View(messages);
        }

        public IActionResult CreditApplication()
        {
            CreditModel = new CreditApplication();
            return View(CreditModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("CreditApplication")]
        public async Task<IActionResult> CreditApplicationPOST(
            [FromServices] Func<CreditApprovedEnum, ICreditApproved> _creditService
            )
        {
            if(ModelState.IsValid)
            {
                var (validationPassed, errors) = await _creditValidator.PassAllValidations(CreditModel);

                CreditResultViewModel creditResult = new CreditResultViewModel()
                {
                    ErrorList = errors,
                    CreditID = 0,
                    Success = validationPassed,
                };
                if (validationPassed)
                {
                    CreditModel.CreditApproved = _creditService(
                        CreditModel.Salary > 50000 ? 
                        CreditApprovedEnum.High : CreditApprovedEnum.Low)
                        .GetCreditApproved(CreditModel);

                    // Add record to database
                    _db.CreditApplicationModel.Add(CreditModel);
                    _db.SaveChanges();

                    creditResult.CreditID = CreditModel.Id;
                    creditResult.CreditApproved = CreditModel.CreditApproved;

                    return RedirectToAction(nameof(CreditResult), creditResult);
                }
                else
                {
                    return RedirectToAction(nameof(CreditResult), creditResult);
                }
            }
            return View(CreditModel);
        }

        public IActionResult CreditResult(CreditResultViewModel creditResult)
        {
            return View(creditResult);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
