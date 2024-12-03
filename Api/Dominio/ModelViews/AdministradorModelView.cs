namespace MinimalApi.Dominio.ModelViews;
public record AdministradorModelView
{
    public string Email { get; set; }
    public string Perfil { get; set; }
    public string Token { get; set; }
}