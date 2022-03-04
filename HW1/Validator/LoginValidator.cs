using FluentValidation;
using HW1.Models;

namespace LogoBootcampHW1.Validator
{
    public class LoginValidator : AbstractValidator<Login>
    {
        // fluent validation rules for login model
        public LoginValidator()
        {
            RuleFor(x=>x.Ad)
                .NotNull()
                .WithMessage("Ad boş olamaz");

            RuleFor(x=>x.Soyad)
                .NotNull()
                .WithMessage("Soyad boş olamaz");

            RuleFor(x=>x.Eposta)
                .NotNull()
                .Matches("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$")
                  .WithMessage("Lütfen geçerli bir email girin");

            RuleFor(x => x.Şifre)
                   .MaximumLength(8)
                   .NotNull()
                   .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{0,8}$")
                   .WithMessage("Şifre bir büyük harf, 1 sayı içermeli ve en fazla 8 karakter olmalıdır");
        }
    }
}
