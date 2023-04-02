﻿namespace CarSharingServer.Dto;

public class CarPostDto
{
    /// <summary>
    /// model of the car
    /// </summary>
    public string Model { get; set; } = string.Empty;
    /// <summary>
    /// colour of the car
    /// </summary>
    public string Colour { get; set; } = string.Empty;
    /// <summary>
    /// number of the car
    /// </summary>
    public string Number { get; set; } = string.Empty;
}
