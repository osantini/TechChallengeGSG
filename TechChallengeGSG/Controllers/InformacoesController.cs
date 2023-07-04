using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechChallengeGSG.Models;
using TechChallengeGSG.Service;

namespace TechChallengeGSG.Controllers
{
    public class InformacoesController : Controller
    {
        private readonly InformacoesDataContext _context;
        private string _filePath;

        public InformacoesController(InformacoesDataContext context, IWebHostEnvironment env)
        {
            _filePath = env.WebRootPath;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.InformacoesModel != null ?
                        View(await _context.InformacoesModel.ToListAsync()) :
                        Problem("Entity set 'InformacoesDataContext.InformacoesModel'  is null.");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeArquivo,Arquivo")] InformacoesModel informacoesModel, IFormFile anexo)
        {

            if (!ValidaImagem(anexo))
            {
                return View(informacoesModel);
            }

            var memoryStream = new MemoryStream();
            await anexo.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            var base64 = Convert.ToBase64String(bytes);

            var nome = UploadBase64(base64, "nomeContainer"); //Nome do Container no Azure

            _context.Add(informacoesModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public bool ValidaImagem(IFormFile anexo)
        {
            switch (anexo.ContentType)
            {
                case "image/jpeg":
                    return true;
                case "image/bmp":
                    return true;
                case "image/gif":
                    return true;
                case "image/png":
                    return true;
                default:
                    return false;
                    break;
            }
        }

        public string UploadBase64(string base64, string container)
        {
            var nomeImagem = Guid.NewGuid().ToString() + "jpg";

            var hash = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64, "");

            byte[] imagebytes = Convert.FromBase64String(hash);

            string storageConnection = AzureConnection.OpenConnectionStorage();

            var clientBob = new BlobClient(storageConnection, container, nomeImagem);

            using (var stream = new MemoryStream(imagebytes))
            {
                clientBob.Upload(stream);
            }

            return clientBob.Uri.AbsoluteUri;
        }
    }
}
