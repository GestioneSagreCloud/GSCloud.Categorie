namespace Categorie.DataAccessLayer.Entities;

public class Categoria : BaseEntity<int>
{
    public int IdFesta { get; set; }
    public string Categoria_Video { get; set; } = null!;
    public string Categoria_Stampa { get; set; } = null!;
}