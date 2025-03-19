using FluentValidation;
using MailWarehouse.Application.Models;

namespace MailWarehouse.Application.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(user => user.FirstName).NotEmpty().WithMessage("Ім'я обов'язкове для заповнення.").MaximumLength(50).WithMessage("Ім'я не може бути довшим за 50 символів.");
        RuleFor(user => user.LastName).NotEmpty().WithMessage("Прізвище обов'язкове для заповнення.").MaximumLength(50).WithMessage("Прізвище не може бути довшим за 50 символів.");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email обов'язковий для заповнення.").EmailAddress().WithMessage("Некоректний формат email.");
        RuleFor(user => user.PhoneNumber).NotEmpty().WithMessage("Номер телефону обов'язковий для заповнення.").WithMessage("Некоректний формат номера телефону.");
    }
}
