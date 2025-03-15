using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Get_data_from_CSV_file.Models;
using Get_data_from_CSV_file.Models;
using Get_data_from_CSV_file.Data;

namespace Get_data_from_CSV_file.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _dbo;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbo)
    {
        _logger = logger;
        _dbo = dbo;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult UploadCSV(IFormFile file)
    {                                                                                               
        if (file == null || file.Length == 0)
        {
            TempData["Error"] = "Please upload a valid CSV file.";
            return RedirectToAction("Index");
        }

        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            var records = reader.ReadToEnd().Split("\n")
                .Skip(1) 
                .Select(line => line.Split(','))
                .Where(fields => fields.Length == 7) 
                .Select(fields => new Student
                {
                    Rollnumber = int.Parse(fields[0]),
                    Name = fields[1],
                    Mobilenumber = fields[2].StartsWith("+91") ? fields[2] : "+91" + fields[2],
                    City = fields[3],
                    Address = fields[4],
                    Email = fields[5],
                    Pincode = fields[6].Length == 6 ? fields[6] : null
                })
                .Where(student => !string.IsNullOrEmpty(student.Email) && student.Pincode != null)
                .ToList();

            foreach (var student in records)
            {
                var existingStudent = _dbo.Students.FirstOrDefault(s => s.Rollnumber == student.Rollnumber);

                if (existingStudent == null)
                {
                    _dbo.Students.Add(student); 
                }
                else
                {
                    existingStudent.Name = student.Name;
                    existingStudent.Mobilenumber = student.Mobilenumber;
                    existingStudent.City = student.City;
                    existingStudent.Address = student.Address;
                    existingStudent.Email = student.Email;
                    existingStudent.Pincode = student.Pincode;

                    _dbo.Students.Update(existingStudent); 
                }
            }
            _dbo.SaveChanges();
            
        }
        TempData["Success"] = "CSV data has been successfully uploaded!";
        return RedirectToAction("Index");
    }

    public IActionResult List()
    {
        var students = _dbo.Students.ToList();
        return View(students);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
