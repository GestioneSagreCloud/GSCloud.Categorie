namespace Categorie.BusinessLayer.Models;

public class CategoriaCreateModel
{
    public int IdFesta { get; set; }
    public string Categoria_Video { get; set; } = null!;
    public string Categoria_Stampa { get; set; } = null!;
}