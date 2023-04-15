using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestoreBrani.Models;
using Microsoft.Extensions.Options;

namespace GestoreBrani.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SongsController : Controller // CONTROLLER
    {
        private readonly SongManagerContext _context;
        private readonly ILogger<SongsController> _logger;
        private readonly MySettings _settings;


        // CONSTRUCTOR
        public SongsController(SongManagerContext context, ILogger<SongsController> logger, IOptions<MySettings> settings)
            // si passa il contexto: contenitore con all'interno tutto ciò che mi serve per poter lavorare con EF - database, in _context
            // per funzionare dobbiamo aggiungere in Program.cs il service
            // aggiungo ILogger come parametro nel constructor
        {
            _context = context;
            _logger = logger;
            _settings = settings.Value;
        }

        // mancano gli atributti perché questo template lavora con i controller tipici della MVC: usa un modo di chiamata sulla naming convention degli metodi stessi
        // GET: SongsTable
        [HttpGet]
        public async Task<IActionResult> Index()
        {   
            _logger.LogInformation("This is an information");
            _logger.LogInformation(_settings.StringSettings); 

            var songManagerContext = await _context.SongTables.ToListAsync<SongTable>(); // DbSet e la rapresentazione in memoria della rispettiva tabella sul database
            return Ok(songManagerContext); // restituisce Ok() perché nel metodo diche che resituisce una IActionResult

            //return View(await songManagerContext.ToListAsync()); // la view non c'è l'abbiamo
        }

        //// GET: SongsTable/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null || _context.SongTables == null)
        //    {
        //        return NotFound();
        //    }

        //    var songTable = await _context.SongTables
        //        .Include(s => s.GenreNavigation)
        //        .FirstOrDefaultAsync(m => m.SongId == id);
        //    if (songTable == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(songTable);
        //}

        //// GET: SongsTable/Create
        //public IActionResult Create()
        //{
        //    ViewData["Genre"] = new SelectList(_context.GenreTables, "GenreId", "GenreId");
        //    return View();
        //}

        //// POST: SongsTable/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("SongId,SongTitle,AlbumTitle,Author,Interpreter,Genre,PublicationYear,SongDuration,OccupiedSpace")] SongTable songTable)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        songTable.SongId = Guid.NewGuid();
        //        _context.Add(songTable);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Genre"] = new SelectList(_context.GenreTables, "GenreId", "GenreId", songTable.Genre);
        //    return View(songTable);
        //}

        //// GET: SongsTable/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.SongTables == null)
        //    {
        //        return NotFound();
        //    }

        //    var songTable = await _context.SongTables.FindAsync(id);
        //    if (songTable == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["Genre"] = new SelectList(_context.GenreTables, "GenreId", "GenreId", songTable.Genre);
        //    return View(songTable);
        //}

        //// POST: SongsTable/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("SongId,SongTitle,AlbumTitle,Author,Interpreter,Genre,PublicationYear,SongDuration,OccupiedSpace")] SongTable songTable)
        //{
        //    if (id != songTable.SongId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(songTable);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SongTableExists(songTable.SongId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Genre"] = new SelectList(_context.GenreTables, "GenreId", "GenreId", songTable.Genre);
        //    return View(songTable);
        //}

        //// GET: SongsTable/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null || _context.SongTables == null)
        //    {
        //        return NotFound();
        //    }

        //    var songTable = await _context.SongTables
        //        .Include(s => s.GenreNavigation)
        //        .FirstOrDefaultAsync(m => m.SongId == id);
        //    if (songTable == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(songTable);
        //}

        //// POST: SongsTable/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    if (_context.SongTables == null)
        //    {
        //        return Problem("Entity set 'SongManagerContext.SongTables'  is null.");
        //    }
        //    var songTable = await _context.SongTables.FindAsync(id);
        //    if (songTable != null)
        //    {
        //        _context.SongTables.Remove(songTable);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SongTableExists(Guid id)
        //{
        //  return (_context.SongTables?.Any(e => e.SongId == id)).GetValueOrDefault();
        //}
    }
}
