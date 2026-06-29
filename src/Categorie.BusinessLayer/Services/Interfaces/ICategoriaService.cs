using Categorie.BusinessLayer.Models;

namespace Categorie.BusinessLayer.Services.Interfaces;

public interface ICategoriaService
{
    Task<Result<IEnumerable<CategoriaModel>>> GetCategorieAsync(CancellationToken cancellationToken);
    Task<Result<CategoriaModel>> GetCategoriaByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<CategoriaModel>> CreateCategoriaAsync(CategoriaCreateModel model, CancellationToken cancellationToken);
    Task<Result<CategoriaModel>> UpdateCategoriaAsync(int id, CategoriaUpdateModel model, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteCategoriaAsync(int id, CancellationToken cancellationToken);
}