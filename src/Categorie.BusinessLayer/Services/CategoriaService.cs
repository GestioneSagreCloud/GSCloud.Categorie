using AppEngine.EFCore.Repositories.Interfaces;
using AppEngine.EFCore.UoW.Interfaces;
using Categorie.BusinessLayer.Models;
using Categorie.BusinessLayer.Services.Interfaces;
using Categorie.DataAccessLayer.Entities;

namespace Categorie.BusinessLayer.Services;

public class CategoriaService(ILogger<CategoriaService> logger, IRepository<Categoria, int> repository, IUnitOfWork unitOfWork) : ICategoriaService
{
    public async Task<Result<IEnumerable<CategoriaModel>>> GetCategorieAsync(CancellationToken cancellationToken)
    {
        var query = await repository.GetAllAsync(cancellationToken);

        if (query is null)
        {
            logger.LogWarning("No Categorie found in the database.");
            return Result.Fail(FailureReasons.ItemNotFound);
        }

        var result = query.Select(x => new CategoriaModel
        {
            Id = x.Id,
            IdFesta = x.IdFesta,
            Categoria_Video = x.Categoria_Video,
            Categoria_Stampa = x.Categoria_Stampa,
        }).ToList();

        return result;
    }

    public async Task<Result<CategoriaModel>> GetCategoriaByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = await repository.GetByIdAsync(id, cancellationToken);

        if (query is null)
        {
            logger.LogWarning("Categoria with ID {Id} not found.", id);
            return Result.Fail(FailureReasons.ItemNotFound);
        }

        var result = new CategoriaModel
        {
            Id = query.Id,
            IdFesta = query.IdFesta,
            Categoria_Video = query.Categoria_Video,
            Categoria_Stampa = query.Categoria_Stampa
        };

        return result;
    }

    public async Task<Result<CategoriaModel>> CreateCategoriaAsync(CategoriaCreateModel model, CancellationToken cancellationToken)
    {
        var entity = new Categoria
        {
            IdFesta = model.IdFesta,
            Categoria_Video = model.Categoria_Video,
            Categoria_Stampa = model.Categoria_Stampa
        };

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error creating Categoria");

            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Fail(CustomFailureReasons.InvalidRequest);
        }

        var createdModel = new CategoriaModel
        {
            Id = entity.Id,
            IdFesta = entity.IdFesta,
            Categoria_Video = entity.Categoria_Video,
            Categoria_Stampa = entity.Categoria_Stampa
        };

        return createdModel;
    }

    public async Task<Result<CategoriaModel>> UpdateCategoriaAsync(int id, CategoriaUpdateModel model, CancellationToken cancellationToken)
    {
        var query = await repository.GetByIdAsync(id, cancellationToken);

        if (query is null)
        {
            logger.LogWarning("Categoria with ID {Id} not found for update.", id);
            return Result.Fail(FailureReasons.ItemNotFound);
        }

        query.IdFesta = model.IdFesta;
        query.Categoria_Video = model.Categoria_Video;
        query.Categoria_Stampa = model.Categoria_Stampa;

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            repository.Update(query);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error updating Categoria with id {Id}", id);

            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Fail(CustomFailureReasons.InvalidRequest);
        }

        var updatedModel = new CategoriaModel
        {
            Id = query.Id,
            IdFesta = query.IdFesta,
            Categoria_Video = query.Categoria_Video,
            Categoria_Stampa = query.Categoria_Stampa
        };

        return updatedModel;
    }

    public async Task<Result<bool>> DeleteCategoriaAsync(int id, CancellationToken cancellationToken)
    {
        var query = await repository.GetByIdAsync(id, cancellationToken);

        if (query is null)
        {
            logger.LogWarning("Categoria with ID {Id} not found for deletion.", id);
            return Result.Fail(FailureReasons.ItemNotFound);
        }

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            repository.Delete(query);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error deleting Categoria with id {Id}", id);

            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            return Result.Fail(FailureReasons.InvalidContent);
        }

        return true;
    }
}