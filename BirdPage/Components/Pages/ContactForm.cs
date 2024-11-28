using System.ComponentModel.DataAnnotations;

namespace BirdPage.Components.Pages;

public class ContactForm
{
    [Required]
    [EmailAddress] public string Email { get; set; }

    [Required]
    [StringLength(2048, ErrorMessage = "This is too long of a message")]
    public string Message { get; set; } = "Hi\nI saw your pictures and I want to know more about them...";
}