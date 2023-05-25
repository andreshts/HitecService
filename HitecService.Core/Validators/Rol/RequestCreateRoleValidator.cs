using FluentValidation;
using HitecService.Models.Requests.Rol;

namespace HitecService.Core.Validators.Rol;

public class RequestCreateRoleValidator : AbstractValidator<RequestRole>
{
    public RequestCreateRoleValidator()
    {
        SetRules();
    }


    private void SetRules()
        => RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The name field is required");
}