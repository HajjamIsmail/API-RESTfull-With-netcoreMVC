using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoueursController : ControllerBase
    {
        private readonly TournoiDBContext _context;

        public JoueursController(TournoiDBContext context)
        {
            _context = context;
            
        }

        // GET: api/Joueurs
        [HttpGet]

        public async Task<ActionResult<List<Joueur>>> GetJoueurs()
        {
            var request = (from j in _context.Joueurs
                           join e in _context.Equipes on j.IdE equals e.Id
                           select new
                           {
                               Id = j.Id,
                               NomJ = j.NomJ,
                               AgeJ = j.AgeJ,
                               SexeJ = j.SexeJ,
                               NomE = e.NomE
                           });
            return Ok(request.ToList());
        }

        // GET: api/Joueurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Joueur>> GetJoueur(int id)
        {
            var joueur = await _context.Joueurs.FindAsync(id);

            if (joueur == null)
            {
                return NotFound();
            }

            return joueur;
        }

        // PUT: api/Joueurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJoueur(int id, Joueur joueur)
        {
            if (id != joueur.Id)
            {
                return BadRequest();
            }

            _context.Entry(joueur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JoueurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Joueurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Joueur>> PostJoueur(Joueur joueur)
        {
            _context.Joueurs.Add(joueur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJoueur", new { id = joueur.Id }, joueur);
        }

        // DELETE: api/Joueurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJoueur(int id)
        {
            var joueur = await _context.Joueurs.FindAsync(id);
            if (joueur == null)
            {
                return NotFound();
            }

            _context.Joueurs.Remove(joueur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JoueurExists(int id)
        {
            return _context.Joueurs.Any(e => e.Id == id);
        }
    }
}
