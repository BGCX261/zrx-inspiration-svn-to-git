using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
namespace Inspiration.MvcWeb.Validators
{
    public class GroupValidator : AbstractValidator<Model.Group>
    {
        public GroupValidator()
        {
            //RuleFor(x => x.ID)
            //    .NotNull();
            RuleFor(x => x.GroupName)
                .NotNull()
                .WithMessage("角色名称不能为空");

        }
    }
}