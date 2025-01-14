﻿namespace CarSharingTests;
public class CarSharingQueries : IClassFixture<CarFixture>
{
    private readonly CarFixture _fixture;
    public CarSharingQueries(CarFixture fixture)
    {
        _fixture = fixture;
    }
    /// <summary>
    /// first request - info about all cars
    /// </summary>
    [Fact]
    public void AllCars()
    {
        var carList = _fixture.FixtureCar;
        var result = (from car in carList select car).ToList().Count;
        Assert.Equal(5, result);
    }
    /// <summary>
    /// second request - all clients who rented requested car - Rolls-Royce
    /// </summary>
    [Fact]
    public void AllClientsRented()
    {
        var rentedCarList = _fixture.FixtureRentedCar.ToList();
        var result = (from car in rentedCarList where car.Car.Model == "Rolls-Royce Boat Tail" select car.Client.FirstName).ToList();
        Assert.Equal(result[0], rentedCarList[1].Client.FirstName);
    }
    ///<summary>
    ///third request - info about cars which are in rent now
    ///</summary>
    [Fact]
    public void AllCarsInRent()
    {
        var carsInRent = _fixture.FixtureRentedCar.ToList();
        var result = (from car in carsInRent where car.TimeOfReturn < DateTime.Now select car.Car.Model).ToList();
        Assert.Equal(carsInRent[0].Car.Model, result[0]);
    }
    ///<summary>
    ///fourth request - top five most rented cars 
    ///</summary>
    [Fact]
    public void TopFive()
    {
        var rentedCars = _fixture.FixtureRentedCar.ToList();
        var counter = (from cartop in rentedCars
                       group cartop by cartop.Car.Id into carGroup
                       select new
                       {
                           carmodel = carGroup.Key,
                           count = carGroup.Count()
                       }).ToList();
        var result = (from carcounter in counter orderby carcounter.count descending select carcounter).Take(5).ToList();
        Assert.Equal(5, result.Count);
    }
    ///<summary>
    ///fifth request - number of rents for each car
    ///</summary>
    [Fact]
    public void NumberOfRents()
    {
        var rentInfo = _fixture.FixtureRentedCar.ToList();
        var result = (from rent in rentInfo
                          group rent by rent.Car.Model into carGroup
                      select new
                          {
                              model = carGroup.Key,
                              counter = carGroup.Distinct().Count()
                          }).ToList();
        Assert.Equal(4, result[0].counter);
    }
    ///<summary>
    ///sixth request - rental point where the most cars were rented
    ///</summary>
    [Fact]
    public void RentalPointStatistics()
    {
        var rentalPoint = _fixture.FixtureRentedCar.ToList();
        var rentNumber = (from rentalPointInfo in rentalPoint
                          orderby rentalPointInfo.Point.PointName
                          group rentalPointInfo by rentalPointInfo.Point.PointName into rentalPointGroup
                          select new
                          {
                              name = rentalPointGroup.Key,
                              counter = rentalPointGroup.Distinct().Count()
                          }).ToList();
        var maxRent = (from point in rentNumber where (point.counter == rentNumber.Max(rents => rents.counter)) select point.name).ToList();
        Assert.Equal("Kchau", maxRent[0]);
    }
}
