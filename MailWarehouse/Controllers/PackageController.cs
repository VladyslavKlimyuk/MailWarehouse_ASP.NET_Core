using AutoMapper;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Models;
using MailWarehouse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailWarehouse.Presentation.Controllers;

public class PackageController : Controller
{
    private readonly IPackageService _packageService;
    private readonly IMapper _mapper;

    public PackageController(IPackageService packageService, IMapper mapper)
    {
        _packageService = packageService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            IEnumerable<PackageDto> packages = await _packageService.GetAllPackagesAsync();
            var packageViewModels = _mapper.Map<IEnumerable<PackageViewModel>>(packages);
            return View(packageViewModels);
        }
        catch (Exception ex)
        {
            // Передача помилки до представлення
            TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
            return View(new List<PackageViewModel>()); // Повернення порожнього списку або іншого представлення
        }
    }

    [HttpGet("Details/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
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
            TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
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
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
            return View(packageViewModel);
        }
    }

    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
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
            TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
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
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
            return View(packageViewModel);
        }
    }

    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
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
            TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("Delete/{id:int}")]
    public async Task<IActionResult> DeletePackage(int id)
    {
        try
        {
            await _packageService.DeletePackageAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Internal server error: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }
}