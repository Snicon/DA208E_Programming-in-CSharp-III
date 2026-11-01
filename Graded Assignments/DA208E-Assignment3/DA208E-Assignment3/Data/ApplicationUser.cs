// Sixten Peterson (AQ9300) 2026-11-01
using Microsoft.AspNetCore.Identity;

namespace DA208E_Assignment3.Data;

/// <summary>
/// Custom user type for Identity. Could be used to extend the IdentityUser with additional user-related properties in
/// the future if needed.
/// 
/// Note to self: p.581 in ASP.NET Core In Action (Third edition) by Andrew Lock, p.10 in Assignment 3 - Help document
/// from Canvas.
/// </summary>
public class ApplicationUser : IdentityUser
{
    
}