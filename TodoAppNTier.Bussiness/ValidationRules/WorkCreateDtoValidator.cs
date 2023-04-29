using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNTier.Dtos.WorkDtos;

namespace TodoAppNTier.Bussiness.ValidationRules
{
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().When(x=>x.IsCompleted).WithMessage("Definition is required").Must(NotBeYavuz).WithMessage("Definition Yavuz Olamaz.");
        }
        //When şart belirtebiliyor. Mesela iscomplated true ise definition boş olamaz.

        //Eğer definition da yavuz yazmıyorsa geçerlidir. Özel isteklerde bulunabiliriz Must ile.
        private bool NotBeYavuz(string arg)
        {
            return arg != "Yavuz" && arg != "yavuz";
        }
    }
}
