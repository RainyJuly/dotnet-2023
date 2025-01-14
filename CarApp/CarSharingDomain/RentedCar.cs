﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSharingDomain;
/// <summary>
/// this class describes car that've been rented and was or was not returned, also this class connects other three classes
/// </summary>
public class RentedCar
{
    /// <summary>
    /// connection to other class, represents a client, who rented a car
    /// </summary>
    public Client? Client { get; set; }
    /// <summary>
    /// id of client, who rented a car
    /// </summary>
    [ForeignKey("Client")]
    [Column("clientId")]
    public int ClientId { get; set; }
    /// <summary>
    ///connection to other class, represents a point, where client rented a car
    /// </summary>
    public RentalPoint? Point { get; set; }
    /// <summary>
    /// id of rental point, where a car was rented
    /// </summary>
    [ForeignKey("RentalPoint")]
    [Column("pointId")]
    public int RentalPointId { get; set; }
    /// <summary>
    /// connection to other class, represents a car rented by the client
    /// </summary>
    public Car? Car { get; set; }
    /// <summary>
    /// id of model of rented car
    /// </summary>
    [ForeignKey("Car")]
    [Column("carId")]
    public int CarId { get; set; }
    /// <summary>
    /// id of rented car
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }
    /// <summary>
    /// date and time when client rented a car
    /// </summary>
    [Column("timeOfRent")]
    public DateTime TimeOfRent { get; set; } = DateTime.MinValue;
    /// <summary>
    /// period of time when the client can use a rented car 
    /// </summary>
    [Column("rentPeriod")]
    public int RentPeriod { get; set; } = 0;
    /// <summary>
    /// a day when car must be returned to the rental point
    /// </summary>
    
    [Column("timeOfReturn")]
    public DateTime TimeOfReturn { get; set; } = DateTime.MinValue;
    /// <summary>
    /// Default constructor
    /// </summary>
    public RentedCar() { }
    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="id"></param>
    /// <param name="client"></param>
    /// <param name="point"></param>
    /// <param name="car"></param>
    /// <param name="timeOfRent"></param>
    /// <param name="rentPeriod"></param>
    /// <param name="timeOfReturn"></param>
    public RentedCar(int id, Client client, RentalPoint point, Car car, DateTime timeOfRent,int rentPeriod, DateTime timeOfReturn)
    {
        Id = id;
        Client = client;
        Point = point;
        Car = car;
        TimeOfRent = timeOfRent;
        RentPeriod = rentPeriod;
        TimeOfReturn = timeOfReturn;
    }
}
