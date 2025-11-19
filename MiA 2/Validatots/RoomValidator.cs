using FluentValidation;
using Models;

namespace Validators;

public class RoomValidator : AbstractValidator<Room>
{
    public RoomValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Capacity).InclusiveBetween(1, 10);
    }
}
