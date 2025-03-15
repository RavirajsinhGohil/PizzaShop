using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using test.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using test.ViewModel;
using test.Services;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace test.Controllers;

// [Route("api/[controller]")]
// [ApiController]
// [Authorize]
public class HomeController : Controller
{

    private readonly SchemaTestContext _dbo;
    private readonly ILogger<HomeController> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IAuthService _authService;

    public string UserEmail { get; private set; }

    public HomeController(ILogger<HomeController> logger, SchemaTestContext obj, IEmailSender emailSender, IAuthService authService)
    {
        _logger = logger;
        _dbo = obj;
        _emailSender = emailSender;
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        string request_cookie = Request.Cookies["Email"];
        if (!string.IsNullOrEmpty(request_cookie))
        {
            return RedirectToAction("UserList", "Home");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(LoginViewModel l)
    {
        if (ModelState.IsValid)
        {
            // Console.WriteLine(l.password = BCrypt.Net.BCrypt.HashPassword(l.password));

            var user = _dbo.Users.FirstOrDefault(u => u.Email == l.Email);
            if (user == null) return NotFound();
            if (BCrypt.Net.BCrypt.Verify(l.password, user.Password))
            {
                if (user != null)
                {
                    string token = _authService.GenerateJwtToken(user.Email, user.Roleid);

                    var cookie = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    };
                    Response.Cookies.Append("Token", token, cookie);

                    if (l.RememberMe)
                    {
                        Response.Cookies.Append("Email", l.Email, cookie);
                        cookie.Expires = DateTime.UtcNow.AddDays(30);
                    }
                    return RedirectToAction("UserList", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "Wrong Email");
                ModelState.AddModelError("Password", "Wrong Password");
                return View("Index");
            }
        }
        return View();
    }

    public IActionResult Logout()
    {
        foreach (var cookie in Request.Cookies.Keys)
        {
            Response.Cookies.Delete(cookie);
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }



    [HttpGet]
    public IActionResult Forgot(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            ViewData["Email"] = email;
        }
        else
        {
            ViewData["Email"] = "";
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Forgot(ForgotViewModel objUser)
    {
        if (ModelState.IsValid)
        {
            string filePath = @"C:\Users\pct216\Downloads\Pizza Shop\Main Project\test\EmailTemplate\ResetPasswordEmailTemplate.html";
            string emailBody = System.IO.File.ReadAllText(filePath);

            var url = Url.Action("ResetPassword", "Login", new { email = objUser.Email }, Request.Scheme);
            emailBody = emailBody.Replace("{ResetLink}", url);

            string subject = "Reset Password";
            _emailSender.SendEmailAsync(objUser.Email, subject, emailBody);

            TempData["success"] = "Password reset instructions have been set to your email";
        }
        return View(objUser);
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ResetPassword(ResetPasswordViewModel modelData)
    {
        if (ModelState.IsValid)
        {
            var user = _dbo.Users.FirstOrDefault(u => u.Email == modelData.Email);

            if (user != null)
            {
                user.Password = modelData.NewPassword;
                _dbo.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
            }
        }
        return View(modelData);
    }

    [HttpGet]
    public IActionResult Profile(UserViewModel model)
    {
        var token = Request.Cookies["Token"];
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var JwtToken = handler.ReadJwtToken(token);
            UserEmail = JwtToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? "";
        }
        ViewBag.Email = UserEmail;
        if (UserEmail == "")
        {
            return RedirectToAction("LoginPage", "Login");
        }

        var user = _dbo.Users.FirstOrDefault(u => u.Email == UserEmail);

        if (user == null)
        {
            return NotFound("User Not Found");
        }
        model.Firstname = user.Firstname;
        model.Lastname = user.Lastname;
        model.Username = user.Username;
        model.Phone = user.Phone;
        model.Country = user.Country;
        model.States = user.States;
        model.City = user.City;
        model.Address = user.Address;
        model.Zipcode = user.Zipcode;

        return View(model);
    }

    [HttpPost]
    public IActionResult Profile(UserViewModel model, string Customers)
    {
        var token = Request.Cookies["Token"];
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var JwtToken = handler.ReadJwtToken(token);
            UserEmail = JwtToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? "";
        }
        ViewBag.Email = UserEmail;
        if (UserEmail == "")
        {
            return RedirectToAction("LoginPage", "Login");
        }
        var user = _dbo.Users.FirstOrDefault(u => u.Email == UserEmail);

        if (user == null)
        {
            return NotFound("User Not Found");
        }
        var countryname = _dbo.Countries.FirstOrDefault(c => c.Countryid.ToString() == model.Country);
        var statename = _dbo.States.FirstOrDefault(c => c.Stateid.ToString() == model.States);
        var cityname = _dbo.Cities.FirstOrDefault(c => c.Cityid.ToString() == model.City);

        model.Country = countryname?.Countryname;
        model.States = statename?.Statename;
        model.City = cityname?.Cityname;

        user.Firstname = model.Firstname;
        user.Lastname = model.Lastname;
        user.Username = model.Username;
        user.Phone = model.Phone;
        user.Country = model.Country;
        user.States = model.States;
        user.City = model.City;
        user.Address = model.Address;
        user.Zipcode = model.Zipcode;

        _dbo.SaveChanges();
        ViewBag.message = "Profile updated Successfully";
        return View(model);
    }

    [HttpGet]
    public IActionResult ProfileChangePassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ProfileChangePassword(ProfileChangePasswordViewModel model)
    {
        var token = Request.Cookies["Token"];
        string userEmail = "";

        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var authToken = handler.ReadJwtToken(token);
            userEmail = authToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "";
        }

        if (string.IsNullOrEmpty(userEmail))
        {
            return RedirectToAction("Index", "Home");
        }

        var user = _dbo.Users.FirstOrDefault(u => u.Email == userEmail);

        if (user == null)
        {
            TempData["error"] = "User not found.";
            return View(model);
        }

        if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, user.Password))
        {
            TempData["error"] = "Current password is incorrect.";
            return View(model);
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
        _dbo.SaveChanges();

        TempData["success"] = "Password updated successfully.";
        return RedirectToAction("Index", "Home");
    }

    [CustomAuthorize("Admin")]
    [HttpGet("UserList")]
    public IActionResult UserList(string searchTerm = "", int page = 1, int pageSize = 5, string sortBy = "Name", string sortOrder = "asc")
    {
        var query = _dbo.Users.Include(u => u.Role).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.Firstname.Contains(searchTerm) || u.Lastname.Contains(searchTerm));
        }

        query = sortBy switch
        {
            "Name" => sortOrder == "asc"
                ? query.OrderBy(u => u.Firstname).ThenBy(u => u.Lastname).Where(u => u.Isdeleted == false)
                : query.OrderByDescending(u => u.Firstname).ThenByDescending(u => u.Lastname),

            "Role" => sortOrder == "asc"
                ? query.OrderBy(u => u.Role.Rolename)
                : query.OrderByDescending(u => u.Role.Rolename),

            _ => query.OrderBy(u => u.Roleid)
        };

        var totalItems = query.Count();
        var users = query.Skip((page - 1) * pageSize)
                         .Take(pageSize)
                         .ToList();

        var token = Request.Cookies["Token"];
        string userEmail = "";

        var handler = new JwtSecurityTokenHandler();
        var authToken = handler.ReadJwtToken(token);
        userEmail = authToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "";

        var user = _dbo.Users.FirstOrDefault(u => u.Email == userEmail);

        var paginatedUsers = new UserPaginationViewModel
        {
            Users = users,
            Username = user.Username,
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            SortBy = sortBy,
            SortOrder = sortOrder
        };
        return View(paginatedUsers);
    }

    [HttpGet]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddUser(User model)
    {
        ViewBag.Roles = _dbo.Roles.Select(r => new { r.Roleid, r.Rolename }).ToList();

        if (ModelState.IsValid)
        {
            var role = _dbo.Roles.FirstOrDefault(r => r.Roleid == model.Roleid);
            var user = new User
            {
                Userid = 13,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Username = model.Username,
                Phone = model.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Roleid = model.Roleid,
                Country = model.Country,
                States = model.States,
                City = model.City,
                Address = model.Address,
                Zipcode = model.Zipcode,
                // Createdby = model.Role,
                Createdat = DateTime.Now,
                Isdeleted = false
            };
            
            _dbo.Users.Add(user);
            _dbo.SaveChanges();

            string filePath = @"C:\Users\pct216\Downloads\Pizza Shop\Main Project\test\EmailTemplate\AddUserEmailTemplate.html";
            string emailBody = System.IO.File.ReadAllText(filePath);

            emailBody = emailBody.Replace("{abc123}", user.Username);
            emailBody = emailBody.Replace("{abc@123}", model.Password);

            string subject = "User Details";
            _emailSender.SendEmailAsync(user.Email, subject, emailBody);
            return RedirectToAction("UsersList", "Home");
        }
        else
        {
            if (model.Firstname == null || model.Lastname == null || model.Email == null || model.Username == null || model.Phone == null || model.Password == null
             || model.Roleid == null || model.Country == null || model.States == null || model.City == null || model.Address == null || model.Zipcode == null)
            {
                ModelState.AddModelError("Firstname", "Please Enter Firstname");
                ModelState.AddModelError("Lastname", "Please Enter Lastname");
                ModelState.AddModelError("Email2", "Please Enter Email");
                ModelState.AddModelError("Username", "Please Enter Username");
                ModelState.AddModelError("Phone", "Please Enter Phone");
                ModelState.AddModelError("Password", "Please Enter Password");
                // ModelState.AddModelError("Roleid", "Please Enter Roleid");
                // ModelState.AddModelError("Country", "Please Enter Country");
                // ModelState.AddModelError("States", "Please Enter States");
                // ModelState.AddModelError("City", "Please Enter City");
                ModelState.AddModelError("Address", "Please Enter Address");
                ModelState.AddModelError("Zipcode2", "Please Enter Zipcode");
            }
        }
        return View();
    }

    [HttpGet]
    public IActionResult EditUser(int userid)
    {
        var user = _dbo.Users.FirstOrDefault(u => u.Userid == userid);
        var model = new AddUserViewModel
        {
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            Phone = user.Phone,
            RoleId = user.Roleid,
            Country = user.Country,
            State = user.States,
            City = user.City,
            Address = user.Address,
            Zipcode = user.Zipcode,
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult EditUser(int userid, AddUserViewModel model)
    {
        var user = _dbo.Users.FirstOrDefault(u => u.Userid == userid);
        user.Firstname = model.Firstname;
        user.Lastname = model.Lastname;
        user.Username = model.Username;
        user.Email = model.Email;
        user.Password = model.Password;
        user.Phone = model.Phone;
        user.Roleid = model.RoleId;
        user.Country = model.Country;
        user.States = model.State;
        user.City = model.City;
        user.Address = model.Address;
        user.Zipcode = model.Zipcode;

        _dbo.SaveChanges();
        return RedirectToAction("UserList", "Home");
    }

    public IActionResult DeleteUser(int userid)
    {
        var user = _dbo.Users.FirstOrDefault(u => u.Userid == userid);
        user.Isdeleted = true;
        _dbo.SaveChanges();
        return RedirectToAction("UserList", "Home");
    }

    [HttpGet]
    public IActionResult Role(UserViewModel model)
    {
        var token = Request.Cookies["Token"];
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var JwtToken = handler.ReadJwtToken(token);
            UserEmail = JwtToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? "";
        }
        ViewBag.Email = UserEmail;
        if (UserEmail == "")
        {
            return RedirectToAction("LoginPage", "Login");
        }

        var user = _dbo.Users.FirstOrDefault(u => u.Email == UserEmail);

        if (user == null)
        {
            return NotFound("User Not Found");
        }
        model.Username = user.Username;
        return View(model);
    }

    [HttpGet]
    public IActionResult Permission(UserViewModel model)
    {
        var token = Request.Cookies["Token"];
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var JwtToken = handler.ReadJwtToken(token);
            UserEmail = JwtToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Email)?.Value ?? "";
        }
        ViewBag.Email = UserEmail;
        if (UserEmail == "")
        {
            return RedirectToAction("LoginPage", "Login");
        }

        var user = _dbo.Users.FirstOrDefault(u => u.Email == UserEmail);

        if (user == null)
        {
            return NotFound("User Not Found");
        }
        model.Username = user.Username;
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
