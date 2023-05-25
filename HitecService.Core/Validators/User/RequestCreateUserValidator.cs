using FluentValidation;
using HitecService.Core.Services.IServices;
using HitecService.Models.Requests.User;

namespace HitecService.Core.Validators.User;

public class RequestCreateUserValidator : AbstractValidator<RequestCreateUser>
{
    private readonly IRoleService _roleService;

    public RequestCreateUserValidator(IRoleService roleService)
    {
        _roleService = roleService;
        SetRules();
    }

    private void SetRules()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("The email field is required");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("The password field is required");

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("The user field is required");

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("The status field is required");

        RuleFor(x => x.RolId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("The rol_id field is required")
            .Must(IsExistRol)
            .WithMessage("The role_id doesn't exist.");
    }

    private bool IsExistRol(int id)
    {
        var isExist = _roleService.IsExistRoleById(id).Result;

        return isExist;
    }
}