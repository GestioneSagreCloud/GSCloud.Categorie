using System.Net.Mime;
using AppEngine.Validations.DependencyInjection;
using Categorie.BusinessLayer.Models;

namespace Categorie.Api.Endpoints;

public class CategoriaEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var versionedApi = endpoints.NewVersionedApi().ReportApiVersions();

        var versionNeutralApi = versionedApi.MapGroup("categoria").IsApiVersionNeutral();
        var categoriaGroup = versionNeutralApi.WithTags("Categorie Endpoints");

        categoriaGroup.MapGet(string.Empty, async (HttpContext httpContext, ICategoriaService categoriaService, CancellationToken cancellationToken) =>
        {
            var result = await categoriaService.GetCategorieAsync(cancellationToken);
            var response = httpContext.CreateResponse(result);

            return response;
        })
        .Produces<IEnumerable<CategoriaModel>>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
        .WithName("GetCategorie")
        .WithDescription("Recupera tutte le categorie disponibili.")
        .WithSummary("Recupera tutte le categorie disponibili.");

        categoriaGroup.MapGet("{id:int}", async (HttpContext httpContext, ICategoriaService categoriaService, int id, CancellationToken cancellationToken) =>
        {
            var result = await categoriaService.GetCategoriaByIdAsync(id, cancellationToken);
            var response = httpContext.CreateResponse(result);

            return response;
        })
        .Produces<CategoriaModel>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
        .WithName("GetCategoriaById")
        .WithDescription("Recupera una categoria specifica per ID.")
        .WithSummary("Recupera una categoria specifica per ID.");

        categoriaGroup.MapPost(string.Empty, async (HttpContext httpContext, CategoriaCreateModel model, ICategoriaService categoriaService, CancellationToken cancellationToken) =>
        {
            var result = await categoriaService.CreateCategoriaAsync(model, cancellationToken);
            var response = httpContext.CreateResponse(result);

            return response;
        })
        .Produces<CategoriaModel>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status422UnprocessableEntity, MediaTypeNames.Application.Json)
        .WithName("CreateCategoria")
        .WithDescription("Crea una nuova categoria.")
        .WithSummary("Crea una nuova categoria.")
        .WithValidation<CategoriaCreateModel>();

        categoriaGroup.MapPut("{id:int}", async (HttpContext httpContext, int id, CategoriaUpdateModel model, ICategoriaService categoriaService, CancellationToken cancellationToken) =>
        {
            var result = await categoriaService.UpdateCategoriaAsync(id, model, cancellationToken);
            var response = httpContext.CreateResponse(result);

            return response;
        })
        .Produces<CategoriaModel>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status422UnprocessableEntity, MediaTypeNames.Application.Json)
        .WithName("UpdateCategoria")
        .WithDescription("Aggiorna una categoria esistente.")
        .WithSummary("Aggiorna una categoria esistente.")
        .WithValidation<CategoriaUpdateModel>();

        categoriaGroup.MapDelete("{id:int}", async (HttpContext httpContext, int id, ICategoriaService categoriaService, CancellationToken cancellationToken) =>
        {
            var result = await categoriaService.DeleteCategoriaAsync(id, cancellationToken);
            var response = httpContext.CreateResponse(result);

            return response;
        })
        .Produces<CategoriaModel>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)
        .ProducesProblem(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)
        .WithName("DeleteCategoria")
        .WithDescription("Elimina una categoria esistente.")
        .WithSummary("Elimina una categoria esistente.");
    }
}
