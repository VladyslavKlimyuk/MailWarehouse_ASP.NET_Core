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

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = _localizer["CreateTitle"];
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            ViewData["Title"] = _localizer["IndexTitle"];
            IEnumerable<PackageDto> packages = await _packageService.GetAllPackagesAsync();
            var packageViewModels = _mapper.Map<IEnumerable<PackageViewModel>>(packages);
            return View(packageViewModels);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View(new List<PackageViewModel>());
        }
    }

    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            ViewData["Title"] = _localizer["DetailsTitle"];
            PackageDto package = await _packageService.GetPackageByIdAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            var packageViewModel = _mapper.Map<PackageViewModel>(package);
            return View(packageViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePackage(PackageViewModel packageViewModel)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(packageViewModel);
            }
            PackageDto packageDto = _mapper.Map<PackageDto>(packageViewModel);
            await _packageService.CreatePackageAsync(packageDto);
            TempData["SuccessMessage"] = _localizer["CreateSuccess"];
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View(packageViewModel);
        }
    }

    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            ViewData["Title"] = _localizer["EditTitle"];
            PackageDto package = await _packageService.GetPackageByIdAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            var packageViewModel = _mapper.Map<PackageViewModel>(package);
            return View(packageViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("Edit/{id:int}")]
    public async Task<IActionResult> EditPackage(int id, PackageViewModel packageViewModel)
    {
        try
        {
            if (id != packageViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(packageViewModel);
            }

            PackageDto packageDto = _mapper.Map<PackageDto>(packageViewModel);
            await _packageService.UpdatePackageAsync(packageDto);
            TempData["SuccessMessage"] = _localizer["UpdateSuccess"];
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return View(packageViewModel);
        }
    }

    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            ViewData["Title"] = _localizer["DeleteTitle"];
            PackageDto package = await _packageService.GetPackageByIdAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            var packageViewModel = _mapper.Map<PackageViewModel>(package);
            return View(packageViewModel);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("Delete/{id:int}")]
    public async Task<IActionResult> DeletePackage(int id)
    {
        try
        {
            await _packageService.DeletePackageAsync(id);
            TempData["SuccessMessage"] = _localizer["DeleteSuccess"];
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = _localizer["ErrorMessage", ex.Message];
            return RedirectToAction(nameof(Index));
        }
    }
}