using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using ProyectoFinal.Rules;
using System.Diagnostics;

namespace ProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Post(int id)
        {
            var rule = new PublicacionRule(_configuration);
            var post = rule.GetPostById(id);
            if (post == null)
            {
                return NotFound(); // ----->>> HACER PAGINA 404
            }
            return View(post);
        }

        [Authorize] // Permite o no su uso segun el login - Puede ponerse a nivel de controlador o de accion
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        [Authorize] // Permite o no su uso segun el login - Puede ponerse a nivel de controlador o de accion
        public IActionResult Add(Publicacion data)
        {
            var rule = new PublicacionRule(_configuration);
            rule.InsertPost(data);

            return RedirectToAction("Index");
        }

        //[AllowAnonymous] // Permite que cualquiera sin estar logeado pueda acceder
        public IActionResult Index()
        {
            var rule = new PublicacionRule(_configuration);
            var posts = rule.GetPostsHome();
            return View(posts);
        }

        public IActionResult Publicaciones(int cant = 5, int pagina = 0)
        {
            var rule = new PublicacionRule(_configuration);
            var posts = rule.GetPublicaciones(cant, pagina);

            return View(posts);
        }
        public IActionResult Suerte()
        {
            var rule = new PublicacionRule(_configuration);
            var post = rule.GetOnePostRandom();
            return View(post);
        }
        public IActionResult AcercaDe()
        {
            return View();
        }
        public IActionResult Contacto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contacto(Contacto contacto)
        {
            if(!ModelState.IsValid)
            {
                return View("Contacto", contacto);
            }
            var rule = new ContactoRule(_configuration);
            var mensaje = @"<h1>Gracias por contactarse</h1>
                        <p>Hemos recibido su correo exitosamente.</p>
                        <p>A la brevedad nos pondremos en contacto.</p>
                        <hr/><p>Saludos</p><p><b>Proyecto Blog</b></p>";
            rule.SendEmail(contacto.Email,mensaje, "Mensaje Recibido", "Proyecto Blog", null); // Mensaje enviado al cliente
            rule.SendEmail("micorreo@mail.com", contacto.Mensaje, "Nuevo Mensaje", contacto.Nombre, contacto.Email); // Mensaje de aviso de nuevo contacto

            return View("Contacto");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
