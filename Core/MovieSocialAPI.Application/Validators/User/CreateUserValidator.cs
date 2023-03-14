using FluentValidation;
using MovieSocialAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MovieSocialAPI.Application.Validators
{
    //public class CreateUserValidator:AbstractValidator<User>
    //{
    //    public CreateUserValidator()
    //    {
    //        //Normalde kullanıcıdan post için veriyi entity nesnesi ile yakalamayız, onun yerine app. katmanına 
    //        //view-model açarız ve mesela create için kullanıcıdan ne alacaksak onu doldururuz. Burası da view-model olur ama bu denemelik.
    //        RuleFor(x => x.Name)
    //            .NotEmpty()
    //            .NotNull()
    //            .WithMessage("Lütfen isim giriniz.")
    //            .MinimumLength(5)
    //            .WithMessage("Lütfen 5 karakterden fazla isim giriniz.");

    //        RuleFor(x => x.Surname)
    //            .NotNull()
    //            .NotEmpty()
    //            .WithMessage("Lütfen soyadınızı boş geçmeyin.");
    //        //sayısal değerler için must kullanılır.Must(s=> s>=0) 0 dan büyük olmalı gibi.
                
    //    }
    
}
