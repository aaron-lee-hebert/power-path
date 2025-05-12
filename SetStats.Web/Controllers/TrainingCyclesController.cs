using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetStats.Core.DTOs;
using SetStats.Core.Interfaces.Services;

namespace SetStats.Web.Controllers;

[Authorize]
public class TrainingCyclesController(ITrainingCycleService service) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index() => View(await service.GetAllTrainingCyclesAsync());

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

        var trainingCycle = await service.GetTrainingCycleAsync(id.Value);
        if (trainingCycle == null)
        {
            return NotFound();
        }

        return View(trainingCycle);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTrainingCycleDto dto)
    {
        if (ModelState.IsValid)
        {
            await service.AddTrainingCycleAsync(dto);
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

        var trainingCycle = await service.GetUpdateTrainingCycleAsync(id);
        if (trainingCycle == null)
        {
            return NotFound();
        }

        return View(trainingCycle);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, UpdateTrainingCycleDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await service.UpdateTrainingCycleAsync(id, dto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingCycleExists(dto.Id))
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

        var trainingCycle = await service.GetTrainingCycleAsync(id);
        if (trainingCycle == null)
        {
            return NotFound();
        }

        return View(trainingCycle);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await service.DeleteTrainingCycleAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private bool TrainingCycleExists(Guid id) => service.GetTrainingCycleAsync(id) is not null;
}
