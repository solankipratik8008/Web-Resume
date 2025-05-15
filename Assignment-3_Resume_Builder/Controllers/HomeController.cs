using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Assignment_3_Resume_Builder.Models;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System.Reflection.Metadata;


namespace Assignment_3_Resume_Builder.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;


    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Comment()
    {
        List<Comment> comments = _context.Comments.OrderByDescending(c => c.CreatedAt).ToList();
        return View(comments);
    }

    public IActionResult AddComment(string name, string text)
    {
        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(text))
        {
            var comment = new Comment { Name = name, Text = text };
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        return RedirectToAction("Comment");
    }
    public IActionResult Delete(int id)
    {
        var comment = _context.Comments.Find(id);
        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        _context.SaveChanges();

        return RedirectToAction("Comment"); 
    }

    //public IActionResult Edit(int id)
    //{
    //    var comment = _context.Comments.Find(id); // Assuming _context is your DbContext
    //    if (comment == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(comment); // Pass the comment to the Edit view
    //}


    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comment = _context.Comments.Find(id);

        if (comment == null)
        {
            return NotFound();
        }

        return View(comment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Comment obj)
    {
        if (obj == null || obj.Id == 0)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
           
                _context.Comments.Update(obj); 
                _context.SaveChanges();
                return RedirectToAction("Comment"); 
            
        }

        return View(obj);
    }

    public IActionResult Resume()
    {


        return View();


    }

    public IActionResult Contact()
    {


        return View();

    }
    public IActionResult DownloadResume()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "PRATIK SOLANKI ONLINE RESUME.pdf");
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        var fileName = "PRATIK SOLANKI ONLINE RESUME.pdf";

        return File(fileBytes, "application/pdf", fileName);
    }
    public IActionResult DownloadProjectI()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "myapp.zip");
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        var fileName = "myapp.zip";

        return File(fileBytes, "application/zip", fileName);
    }




    public IActionResult DownloadProjectII()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "IOS_Project.zip");
        var fileBytes = System.IO.File.ReadAllBytes(filePath);
        var fileName = "IOS_Project.zip";

        return File(fileBytes, "application/zip", fileName);
    }
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
