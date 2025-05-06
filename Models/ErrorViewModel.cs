using SinemaArsivSitesi.Data;
using SinemaArsivSitesi.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.Rendering;
namespace SinemaArsivSitesi.Models;

public class ErrorViewModel :BaseEntity
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
