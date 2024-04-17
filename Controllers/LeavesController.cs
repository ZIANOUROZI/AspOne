using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspOne.Data;
using AspOne.Models;

namespace AspOne.Controllers
{
    public class LeavesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeavesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Den här metoden hämtar alla Leaves från databasen(SQL) inklusive information om den tillhörande employee för varje leav-post och sedan skickar listan 
        // med leaves till VYN för att visas (View) för användaren
        public IActionResult Index()
        {
            var Leaves = _context.Leaves.Include(l => l.Employee).ToList();
            return View(Leaves);
        }
        //[HttpGet] är som en skylt som säger att svara när någon frågar efter en sida eller data om jag skulle sälja en produkt i min hemsida så skulle
        // jag sätta [HttpGet] på den metoden som hämtar produkten från databasen och vissar dem på hemsidan




        [HttpGet]
		//metoden visar formuläret för att ansöka om ledighet och fyller i listan med anställda. 
		public IActionResult Apply()
		{
			// Hämta alla anställd för att fylla i en propdown Lista sen
			ViewBag.Employees = _context.Employees.Select(e => new SelectListItem
			{
				Value = e.EmployeeId.ToString(),
				Text = e.EmployeeName
			}).ToList();
			return View();
		}
        //Det används för att köra en metod när data skickas till serverna via en HTTP-POST-föråga
		[HttpPost]

		//metoden hanterar inskickad data från formuläret.
		//Om datan är korrekt, läggs ledighetsinformationen
		//till i databasen och användaren omdirigeras. Annars visas formuläret igen med felmeddelanden.
		public IActionResult Apply(Leave Leave)
		{
			if (ModelState.IsValid)
			{
				_context.Leaves.Add(Leave);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.Employees = _context.Employees.Select(e => new SelectListItem
			{
				Value = e.EmployeeId.ToString(),
				Text = e.EmployeeName
			}).ToList();
			return View(Leave);
		}
		[HttpGet]
		public IActionResult EditStatus(int id)
		{
			var leave = _context.Leaves.Include(l => l.Employee).FirstOrDefault(l => l.LeaveId == id);
			if (leave == null)
			{
				return NotFound();
			}
			return View(leave);
		}
		[HttpPost]
		public IActionResult EditStatus(Leave model)
		{
			var leave = _context.Leaves.Find(model.LeaveId);
			if (leave == null)
			{
				return NotFound();
			}

			leave.Status = model.Status;
			_context.SaveChanges();
			return RedirectToAction("Index");
			return View(model);
		}
	}
}
