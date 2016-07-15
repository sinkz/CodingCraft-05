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

namespace CodingCraft_05.Controllers
{
    public class DiretoriosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Diretorios
        public IQueryable<Diretorio> GetDiretorios()
        {
            return db.Diretorios;
        }

        // GET: api/Diretorios/5
        [ResponseType(typeof(Diretorio))]
        public async Task<IHttpActionResult> GetDiretorio(Guid id)
        {
            Diretorio diretorio = await db.Diretorios.FindAsync(id);
            if (diretorio == null)
            {
                return NotFound();
            }

            return Ok(diretorio);
        }

        // PUT: api/Diretorios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDiretorio(Guid id, Diretorio diretorio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != diretorio.DiretorioId)
            {
                return BadRequest();
            }

            db.Entry(diretorio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiretorioExists(id))
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

        // POST: api/Diretorios
        [ResponseType(typeof(Diretorio))]
        public async Task<IHttpActionResult> PostDiretorio(Diretorio diretorio)
        {
            diretorio.DiretorioId = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Diretorios.Add(diretorio);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiretorioExists(diretorio.DiretorioId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = diretorio.DiretorioId }, diretorio);
        }

        // DELETE: api/Diretorios/5
        [ResponseType(typeof(Diretorio))]
        public async Task<IHttpActionResult> DeleteDiretorio(Guid id)
        {
            Diretorio diretorio = await db.Diretorios.FindAsync(id);
            if (diretorio == null)
            {
                return NotFound();
            }

            db.Diretorios.Remove(diretorio);
            await db.SaveChangesAsync();

            return Ok(diretorio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiretorioExists(Guid id)
        {
            return db.Diretorios.Count(e => e.DiretorioId == id) > 0;
        }
    }
}