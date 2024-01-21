using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class BeerUpdateValidator :AbstractValidator<BeerUpdateDto>
    {
        public BeerUpdateValidator() 
        {
            RuleFor(x => x.Id).NotNull().WithMessage("El id debe existir");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre no puede estar vacío");
            RuleFor(x => x.Name).Length(2, 25).WithMessage("El nombre debe tener entre 2 y 25 caracteres");
            RuleFor(x => x.BrandID).NotNull().WithMessage("La marca es obligatoria");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage("La marca debe existir");
        }
    }
}
