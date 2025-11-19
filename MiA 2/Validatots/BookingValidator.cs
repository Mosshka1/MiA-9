using FluentValidation;
using Models;

namespace Validators;

public class BookingValidator : AbstractValidator<Booking>
{
    public BookingValidator()
    {
        RuleFor(x => x.RoomId).GreaterThan(0);
        RuleFor(x => x.StartUtc).GreaterThan(DateTime.UtcNow.AddMinutes(-1));
        RuleFor(x => x.Players).InclusiveBetween(1, 10);
        RuleFor(x => x.CustomerName).NotEmpty().MinimumLength(3);

        RuleFor(x => x.CustomerPhone)
            .NotEmpty()
            .Matches(@"^\+380\d{9}$")
            .WithMessage("Phone must be like +380XXXXXXXXX");
    }
}
