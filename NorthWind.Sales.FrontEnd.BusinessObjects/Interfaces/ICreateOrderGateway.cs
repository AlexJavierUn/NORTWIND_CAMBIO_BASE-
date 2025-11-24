namespace NorthWind.Sales.FrontEnd.BusinessObjects.Interfaces;

//Esta interface permitira implementar una clase para encapsular el codigo cliente que 
//Consuma la Web API y crear la orden desde Blazor.
public interface ICreateOrderGateway
{
    Task<int> CreateOrderAsync(CreateOrderDto order);


}
