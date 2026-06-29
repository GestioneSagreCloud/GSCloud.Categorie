using Categorie.BusinessLayer.Models;
using FluentValidation;

namespace Categorie.BusinessLayer.Validations;

public class CategoriaUpdateValidator : AbstractValidator<CategoriaUpdateModel>
{
    public CategoriaUpdateValidator()
    {
        RuleFor(x => x.IdFesta)
            .GreaterThan(0).WithMessage("IdFesta must be greater than 0.");

        RuleFor(x => x.Categoria_Video)
            .NotEmpty().WithMessage("La categoria video è obbligatoria.")
            .MaximumLength(100).WithMessage("La categoria video non può superare i 100 caratteri.");

        RuleFor(x => x.Categoria_Stampa)
            .NotEmpty().WithMessage("La categoria stampa è obbligatoria.")
            .MaximumLength(100).WithMessage("La categoria stampa non può superare i 100 caratteri.");
    }
}