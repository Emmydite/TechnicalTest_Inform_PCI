using Inform.BLL.Models;
using Inform.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TechnicalTest_Inform.Models;

namespace TechnicalTest_Inform.Controllers
{
    public class HomeController : Controller
    {
       // private readonly ILogger<HomeController> _logger;
        private readonly ContactService _contactService;

        public HomeController(ContactService contactService)
        {
            //_logger = logger;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var getContacts = await _contactService.GetContacts();

                return View(getContacts);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult AddContact()
        {
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(ContactDTO contactDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var addContact = await _contactService.AddContact(contactDTO);

                if (addContact)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult EditContact(int id)
        {
            try
            {
                var updateContact = _contactService.GetContact(id);

                return View(updateContact);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContactRecord(ContactDTO contactDTO)
        {
            try
            {
                var updateContact = await _contactService.UpdateContact(contactDTO);

                if (updateContact)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
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