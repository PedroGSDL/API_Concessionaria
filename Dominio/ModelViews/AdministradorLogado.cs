namespace MinimalApi.Dominio.ModelViews;
public record AdministradorLogado

{
    public string Email { get; set; }
    public string Perfil { get; set; }
    public string Token { get; set; }
}