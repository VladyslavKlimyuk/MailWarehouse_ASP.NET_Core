using FluentValidation;
using MailWarehouse.Application.Models;

namespace MailWarehouse.Application.Validators;

public class PackageValidator : AbstractValidator<PackageDto>
{
    public PackageValidator()
    {
        RuleFor(package => package.TrackingNumber).NotEmpty().WithMessage("Трек-номер обов'язковий для заповнення.");
        RuleFor(package => package.SenderId).NotEmpty().WithMessage("Відправник обов'язковий для заповнення.");
        RuleFor(package => package.RecipientId).NotEmpty().WithMessage("Отримувач обов'язковий для заповнення.");
        RuleFor(package => package.SenderAddress).NotEmpty().WithMessage("Адреса відправника обов'язкова для заповнення.");
        RuleFor(package => package.RecipientAddress).NotEmpty().WithMessage("Адреса отримувача обов'язкова для заповнення.");
        RuleFor(package => package.Weight).NotEmpty().WithMessage("Вага обов'язкова для заповнення.").GreaterThan(0).WithMessage("Вага повинна бути більшою за 0.");
        RuleFor(package => package.Price).NotEmpty().WithMessage("Ціна обов'язкова для заповнення.").GreaterThanOrEqualTo(0).WithMessage("Ціна не може бути від'ємною.");
        RuleFor(package => package.Status).NotEmpty().WithMessage("Статус обов'язковий для заповнення.");
        RuleFor(package => package.CreatedAt).NotEmpty().WithMessage("Дата створення обов'язкова для заповнення.");
    }
}
