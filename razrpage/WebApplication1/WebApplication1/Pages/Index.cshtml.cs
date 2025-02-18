using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RazorPagesExample.Models;
using System.Collections.Generic;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public User NewUser { get; set; } = new User();

        public List<User> users { get; set; } = new List<User>();

        public void OnGet()
        {
            if (TempData.ContainsKey("Users"))
            {
                var usersJson = TempData.Peek("Users") as string;
                if (!string.IsNullOrEmpty(usersJson))
                {
                    users = JsonConvert.DeserializeObject<List<User>>(usersJson);
                }
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (TempData.ContainsKey("Users"))
                {
                    var usersJson = TempData.Peek("Users") as string;
                    if (!string.IsNullOrEmpty(usersJson))
                    {
                        users = JsonConvert.DeserializeObject<List<User>>(usersJson);
                    }
                }

                users.Add(NewUser);

                TempData["Users"] = JsonConvert.SerializeObject(users);

                return RedirectToPage();
            }

            return Page();
        }
    }
}
