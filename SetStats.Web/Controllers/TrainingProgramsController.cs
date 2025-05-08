using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetStats.Core.DTOs;
using SetStats.Core.Interfaces.Services;

namespace SetStats.Web.Controllers;

[Authorize]
public class TrainingProgramsController(ITrainingProgramService service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index() => View(await service.GetAllTrainingProgramsAsync());

    [HttpGet]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id == null)
        {
            return NotFound();
        }

        var trainingProgram = await service.GetTrainingProgramAsync(id.Value);
        if (trainingProgram == null)
        {
            return NotFound();
        }

        return View(trainingProgram);
    }

    [HttpGet]
    public IActionResult Create() => View();

    // POST: TrainingPrograms/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTrainingProgramDto dto)
    {
        if (ModelState.IsValid)
        {
            await service.AddTrainingProgramAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trainingProgram = await service.GetUpdateTrainingProgramAsync(id);
        if (trainingProgram == null)
        {
            return NotFound();
        }

        return View(trainingProgram);
    }

    // POST: TrainingPrograms/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateTrainingProgramDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await service.UpdateTrainingProgramAsync(id, dto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingProgramExists(dto.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        return View(dto);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var trainingProgram = await service.GetTrainingProgramAsync(id);
        if (trainingProgram == null)
        {
            return NotFound();
        }

        return View(trainingProgram);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await service.DeleteTrainingProgramAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private bool TrainingProgramExists(Guid id) => service.GetTrainingProgramAsync(id) is not null;
}
