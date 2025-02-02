﻿namespace CarSharingServer.Dto;
/// <summary>
/// RentedCarPostDto for HTTP POST request
/// </summary>
public class RentedCarPostDto
{
    /// <summary>
    /// id of client, who rented a car
    /// </summary>
    public int ClientId { get; set; }
    /// <summary>
    /// id of rental point, where a car was rented
    /// </summary>
    public int RentalPointId { get; set; }
    /// <summary>
    /// id of model of rented car
    /// </summary>
    public int CarId { get; set; }
    /// <summary>
    /// date and time when client rented a car
    /// </summary>
    public DateTime TimeOfRent { get; set; } = DateTime.MinValue;
    /// <summary>
    /// period of time when the client can use a rented car 
    /// </summary>
    public int RentPeriod { get; set; } = 0;
    /// <summary>
    /// a day when car must be returned to the rental point
    /// </summary>
    public DateTime TimeOfReturn { get; set; } = DateTime.MinValue;
}
