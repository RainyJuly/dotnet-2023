﻿using AutoMapper;
using CarSharingDomain;
using CarSharingServer.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSharingServer.Controllers;
/// <summary>
/// Rented car controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class RentedCarController : ControllerBase
{
    private readonly ILogger<RentedCarController> _logger;
    private readonly IDbContextFactory<CarSharingDbContext> _contextFactory;
    private readonly IMapper _mapper;
    /// <summary>
    /// Constructor for RentedCarController
    /// </summary>
    /// <param name="contextFactory"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public RentedCarController(IDbContextFactory<CarSharingDbContext> contextFactory, ILogger<RentedCarController> logger, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Get info about all rented cars
    /// </summary>
    /// <returns>
    /// List of all rented cars
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RentedCarGetDto>>> Get()
    {
        await using var ctx = await _contextFactory.CreateDbContextAsync();
        if (ctx.RentedCars == null)
        {
            return NotFound();
        }
        _logger.LogInformation("Get the rented cars");
        return await _mapper.ProjectTo<RentedCarGetDto>(ctx.RentedCars).ToListAsync();
    }
    /// <summary>
    /// Get rented car by id
    /// </summary>
    /// <param name="id">
    /// Identification number of required rented car
    /// </param>
    /// <returns>
    /// Rented car by id
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RentedCarGetDto>> Get(int id)
    {
        _logger.LogInformation("Get the rented car with id {id} ", id);
        var ctx = await _contextFactory.CreateDbContextAsync();
        if (ctx.RentedCars == null)
        {
            return NotFound();
        }
        var rentedCar = await ctx.RentedCars.FindAsync(id);
        if (rentedCar == null)
        {
            return NotFound();
        }
        return _mapper.Map<RentedCarGetDto>(rentedCar);
    }
    /// <summary>
    /// Post a new rented car
    /// </summary>
    /// <param name="rentedCar">
    /// Info about new rented car you want to add
    /// </param>
    [HttpPost]
    public async Task <IActionResult>Post([FromBody] RentedCarPostDto rentedCar)
    {
        _logger.LogInformation("Post a new rented car");
        var ctx = await _contextFactory.CreateDbContextAsync();
        if (ctx.RentedCars == null)
        {
            return NotFound();
        }
        await ctx.RentedCars.AddAsync(_mapper.Map<RentedCar>(rentedCar));
        await ctx.SaveChangesAsync();
        return Ok();
    }
    /// <summary>
    /// Put a rented car
    /// </summary>
    /// <param name="id">
    /// Identification number of rented car which should be edited
    /// </param>
    /// <param name="rentedCarToPut">
    /// Info about new rented car which should be edited 
    /// </param>
    /// <returns>
    /// Success or error code
    /// </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] RentedCarPostDto rentedCarToPut)
    {
        var ctx = await _contextFactory.CreateDbContextAsync();
        if (ctx.RentedCars == null)
        {
            return NotFound();
        }
        var rentedCar = await ctx.RentedCars.FindAsync(id);
        if (rentedCar == null)
        {
            _logger.LogInformation("Not found rented car with id {id}", id);
            return NotFound();
        }
        else
        {
            _logger.LogInformation("Updating a rented car with id {id}", id);
            _mapper.Map(rentedCarToPut, rentedCar);
            ctx.Update(_mapper.Map<RentedCar>(rentedCar));
            await ctx.SaveChangesAsync();
            return Ok();
        }
    }
    /// <summary>
    /// Delete rented car
    /// </summary>
    /// <param name="id">
    /// Identification number of rented car which should be deleted
    /// </param>
    /// <returns>
    /// Success or error code
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ctx = await _contextFactory.CreateDbContextAsync();
        if (ctx.RentedCars == null)
        {
            return NotFound();
        }
        var rentedCar = await ctx.RentedCars.FindAsync(id);
        if (rentedCar == null)
        {
            _logger.LogInformation("Not found rented car with id {id}", id);
            return NotFound();
        }
        else
        {
            _logger.LogInformation("Delete a rented car - success");
            ctx.RentedCars.Remove(rentedCar);
            await ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
