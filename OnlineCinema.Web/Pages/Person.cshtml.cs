using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Models;

namespace OnlineCinema.Web.Pages
{
    public class PersonModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        
        public Person Person { get; set; }

        public IEnumerable<Production> Productions { get; set; }

        private DbPersonService PersonService;

        public PersonModel(DbPersonService personService)
        {
            PersonService = personService;
        }

        public IActionResult OnGet()
        {
            Person = new Person(Id, Name, "");

            Productions = PersonService.GetProductions(Person, out int errorCode);

            if (errorCode != 0)
                return Redirect($"Error?DbError={errorCode}");

            return Page();
        }
    }
}
