using FluentValidation;
using HitecService.Core.Services.IServices;
using HitecService.Models.Requests.Privileges;

namespace HitecService.Core.Validators.Privilege;

public class RequestCreatePrivilegeValidator : AbstractValidator<RequestPrivilege>
{
    private readonly IMenuConfigurationService _menuConfigurationService;
    private readonly IRoleService              _roleService;

    public RequestCreatePrivilegeValidator(IRoleService roleService, IMenuConfigurationService menuConfigurationService)
    {
        _roleService              = roleService;
        _menuConfigurationService = menuConfigurationService;
        SetRules();
    }

    private void SetRules()
    {
        RuleFor(x => x.RolId)
            .NotEmpty()
            .WithMessage("The rolId field is required")
            .Must(IsExistRol)
            .WithMessage("The role_id doesn't exist.");

        RuleFor(x => x.MenuConfigurationId)
            .NotEmpty()
            .WithMessage("The menu configuration is field is required")
            .Must(IsExistMenuConfiguration)
            .WithMessage("The menu_configuration_id doesn't exist.");
    }

    private bool IsExistRol(int id)
    {
        var isExist = _roleService.IsExistRoleById(id).Result;

        return isExist;
    }

    private bool IsExistMenuConfiguration(int id)
    {
        var isExist = _menuConfigurationService.IsExistMenuConfigurationById(id).Result;

        return isExist;
    }
}