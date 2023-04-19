using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using ContactManager.Interfaces;

namespace ContactManager.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IContactService _contactService;

    public HomeController(ILogger<HomeController> logger, IContactService contactService)
    {
        _logger = logger;
        _contactService = contactService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var token = GetToken();
            var contacts = await _contactService.GetContacts(token);
            return View(contacts);
        }
        catch
        {
            return RedirectToAction("Logout", "Login");
        }
    }

    [Authorize(Policy = "Admin")]
    public IActionResult Create()
    {
        var contact = new Contact();
        contact.Emails.Add(new Email());
        return View(new Contact());
    }

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(Contact model)
    {
        var token = GetToken();
        await _contactService.CreateContact(model, token);
        return RedirectToAction("Index");
    }

    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update(int id)
    {
        var token = GetToken();
        var contact = await _contactService.GetContact(id, token);
        return View(contact);
    }

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Update(int id, UpdateContactModel model)
    {
        var token = GetToken();
        await _contactService.UpdateContact(id, model, token);
        return RedirectToAction("Index");
    }

    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var token = GetToken();
        await _contactService.DeleteContact(id, token);
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private string GetToken()
    {
        var tokenClaim = User.Claims.FirstOrDefault(c => c.Type == "Token");
        return tokenClaim.Value;
    }
}
