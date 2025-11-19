using FluentValidation;
using Models;

namespace Validators;

public class GameSessionValidator : AbstractValidator<GameSession>
{
    public GameSessionValidator()
    {
        RuleFor(x => x.BookingId).GreaterThan(0);
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Notes).MaximumLength(500);
    }
}
