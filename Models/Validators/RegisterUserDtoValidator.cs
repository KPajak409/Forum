using FluentValidation;
using Forum.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(ForumDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(25);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty();

            RuleFor(x => x.DateOfBirth.Year)
                .LessThanOrEqualTo(DateTime.Now.Year - 13);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(15);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

            RuleFor(x => x.Name)
                .MaximumLength(40);

            RuleFor(x => x.SurName)
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                        context.AddFailure("Email", "That email is taken");
                });

            /*RuleFor(x => x.Username)
                .Custom((value, context) =>
                {
                    var UsernameInUse = dbContext.Users.Any(u => u.Username == value);
                    if (UsernameInUse)
                        context.AddFailure("Username", "That username is taken");
                });*/
        }
    }
}
