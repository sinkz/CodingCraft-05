using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CodingCraft_05.Models;
using System.Web;
using System.Diagnostics;
using System.Web.Http.Cors;
using System.IO;

namespace CodingCraft_05.Controllers
{
    public class ArquivosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Arquivos
        public IQueryable<Arquivo> GetArquivos()
        {
            return db.Arquivos;
        }

        // GET: api/Arquivos/5
        [ResponseType(typeof(Arquivo))]
        public async Task<IHttpActionResult> GetArquivo(Guid id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return NotFound();
            }

            return Ok(arquivo);
        }



        // PUT: api/Arquivos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArquivo(Guid id, Arquivo arquivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arquivo.ArquivoId)
            {
                return BadRequest();
            }

            db.Entry(arquivo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArquivoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Arquivos
        [ResponseType(typeof(Arquivo))]
        public async Task<IHttpActionResult> PostArquivo(Arquivo arquivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Arquivos.Add(arquivo);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ArquivoExists(arquivo.ArquivoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = arquivo.ArquivoId }, arquivo);
        }


        [HttpPost, Route("api/Arquivos/upload")]
        public HttpResponseMessage PostFormData()
        {

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                HttpResponseMessage result = null;
                var postedFile = httpRequest.Files[0];
                var tipo = postedFile.ContentType.Split('/').First();
                var ext = postedFile.FileName.Split('.').Last();
                var root = HttpContext.Current.Server.MapPath("~/Uploads");

                switch (tipo)
                {
                    case "image":
                        root = HttpContext.Current.Server.MapPath("~/Uploads/Imagens");
                        break;
                    case "text":
                        root = HttpContext.Current.Server.MapPath("~/Uploads/Texto");
                        break;
                    case "audio":
                        root = HttpContext.Current.Server.MapPath("~/Uploads/Musica");
                        break;
                    case "video":
                        root = HttpContext.Current.Server.MapPath("~/Uploads/Videos");
                        break;
                    case "application":
                        string caminho = uploadArquivoOffice(ext);
                        root = HttpContext.Current.Server.MapPath(caminho);
                        break;

                    default:
                        Console.WriteLine("Default case");
                        break;
                }


                postedFile.SaveAs(Path.Combine(root, postedFile.FileName));

                result = Request.CreateResponse(HttpStatusCode.Created, postedFile);


            }
            return new HttpResponseMessage(HttpStatusCode.Created);
        }


        private string uploadArquivoOffice(string ext)
        {
            string caminho = "";
            switch (ext)
            {
                case "docx":
                    caminho = "~/Uploads/Word";
                    break;
                case "pptx":
                    caminho = "~/Uploads/PowerPoint";
                    break;
                case "xls":
                    caminho = "~/Uploads/Excel";
                    break;
                case "xlsx":
                    caminho = "~/Uploads/Excel";
                    break;
            }

            return caminho;
        }


        // DELETE: api/Arquivos/5
        [ResponseType(typeof(Arquivo))]
        public async Task<IHttpActionResult> DeleteArquivo(Guid id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return NotFound();
            }

            db.Arquivos.Remove(arquivo);
            await db.SaveChangesAsync();

            return Ok(arquivo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArquivoExists(Guid id)
        {
            return db.Arquivos.Count(e => e.ArquivoId == id) > 0;
        }
    }
}