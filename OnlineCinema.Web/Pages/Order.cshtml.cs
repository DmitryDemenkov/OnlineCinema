using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCinema.Web.Services;
using OnlineCinema.Web.Models;
using OnlineCinema.Web.Extensions;

namespace OnlineCinema.Web.Pages
{
    public class OrderModel : PageModel
    {
        public OrderModel(DbOrderService orderService)
        {
            OrderService = orderService;
        }

        private DbOrderService OrderService;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Order Order { get; set; }

        public IActionResult OnGet()
        {
            if (Request.Cookies.TryGetValue<User>("User", out User user))
            {
                Order = OrderService.GetOrder(Id, out int errorCode);

                if (errorCode != 0)
                    return Redirect($"Error/DbError={errorCode}");

                if (Order.IdUser != user.Id)
                    return Redirect("/Profile/History");

                return Page();
            }
            else
            {
                return RedirectToPage("Index");
            }
        }
    }
}
