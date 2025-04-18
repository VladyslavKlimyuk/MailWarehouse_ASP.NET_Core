using AutoMapper;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Models;
using MailWarehouse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace MailWarehouse.Controllers;

public class PackageController : Controller
{
    private readonly IPackageService _packageService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<PackageController> _localizer;

    public PackageController(IPackageService packageService, IMapper mapper, IStringLocalizer<PackageController> localizer)
    {
        _packageService = packageService;
        _mapper = mapper;
        _localizer = localizer;
    }

    // GET: Package/Index
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = _localizer["PackageIndexTitle"];
        try
        {
            IEnumerable<PackageDto> packages = await _packageService.GetAllPackagesAsync();
            var packageViewModels = _mapper.Map<IEnumerable<PackageViewModel>>(packages);
            return View("Index", packageViewModels);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View("Index", new List<PackageViewModel>());
        }
    }

    // GET: Package/Create
    public IActionResult Create()
    {
        ViewData["Title"] = _localizer["PackageCreateTitle"];
        return View("Create");
    }

    // POST: Package/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PackageViewModel packageViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View("Create", packageViewModel);
            }
            PackageDto packageDto = _mapper.Map<PackageDto>(packageViewModel);
            await _packageService.CreatePackageAsync(packageDto);
            TempData["SuccessMessage"] = _localizer["CreateSuccess"];
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View("Create", packageViewModel);
        }
    }

    // GET: Package/Edit/5
    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            ViewData["Title"] = _localizer["PackageEditTitle"];
            PackageDto packageDto = await _packageService.GetPackageByIdAsync(id);
            if (packageDto == null)
            {
                return NotFound();
            }
            var packageViewModel = _mapper.Map<PackageViewModel>(packageDto);
            return View("Edit", packageViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Package/Edit/5
    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PackageViewModel packageViewModel)
    {
        try
        {
            if (id != packageViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View("Edit", packageViewModel);
            }

            PackageDto packageDto = _mapper.Map<PackageDto>(packageViewModel);
            await _packageService.UpdatePackageAsync(packageDto);
            TempData["SuccessMessage"] = _localizer["UpdateSuccess"];
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View("Edit", packageViewModel);
        }
    }

    // GET: Package/Details/5
    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            ViewData["Title"] = _localizer["PackageDetailsTitle"];
            PackageDto packageDto = await _packageService.GetPackageByIdAsync(id);
            if (packageDto == null)
            {
                return NotFound();
            }
            var packageViewModel = _mapper.Map<PackageViewModel>(packageDto);
            return View("Details", packageViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }


    // GET: Package/Delete/5
    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            ViewData["Title"] = _localizer["PackageDeleteTitle"];
            PackageDto packageDto = await _packageService.GetPackageByIdAsync(id);
            if (packageDto == null)
            {
                return NotFound();
            }
            var packageViewModel = _mapper.Map<PackageViewModel>(packageDto);
            return View("Delete", packageViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Package/Delete/5
    [HttpPost("Delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _packageService.DeletePackageAsync(id);
            TempData["SuccessMessage"] = _localizer["PackageDeleteSuccess"];
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }
}