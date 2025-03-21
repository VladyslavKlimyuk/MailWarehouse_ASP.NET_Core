﻿using AutoMapper;
using MailWarehouse.Application.Interfaces;
using MailWarehouse.Application.Models;
using MailWarehouse.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MailWarehouse.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackageController : ControllerBase
{
    private readonly IPackageService _packageService;
    private readonly IMapper _mapper;

    public PackageController(IPackageService packageService, IMapper mapper)
    {
        _packageService = packageService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PackageViewModel>> GetPackages()
    {
        IEnumerable<PackageDto> packages = _packageService.GetAllPackages();
        return Ok(_mapper.Map<IEnumerable<PackageViewModel>>(packages));
    }

    [HttpGet("{id}")]
    public ActionResult<PackageViewModel> GetPackage(int id)
    {
        PackageDto package = _packageService.GetPackageById(id);
        if (package == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<PackageViewModel>(package));
    }

    [HttpPost]
    public ActionResult<PackageViewModel> CreatePackage(PackageViewModel packageViewModel)
    {
        if (ModelState.IsValid)
        {
            PackageDto packageDto = _mapper.Map<PackageDto>(packageViewModel);
            _packageService.CreatePackage(packageDto);
            return CreatedAtAction(nameof(GetPackage), new { id = packageDto.Id }, _mapper.Map<PackageViewModel>(packageDto));
        }
        return BadRequest(ModelState);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePackage(int id, PackageViewModel packageViewModel)
    {
        if (id != packageViewModel.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            PackageDto packageDto = _mapper.Map<PackageDto>(packageViewModel);
            _packageService.UpdatePackage(packageDto);
            return NoContent();
        }
        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePackage(int id)
    {
        _packageService.DeletePackage(id);
        return NoContent();
    }
}