
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
//builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "customizada",
    defaults: new { controller = "Home", action = "Index" },
    pattern: "minha-area/{id?}"
//pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.MapControllerRoute(
    name: "default",
    defaults: new {controller="Home", action="Index" },
    pattern: "{controller}/{action}/{id?}"
    //pattern: "{controller=Home}/{action=Index}/{id?}"
); 

app.Run();







//app.MapGet("/", () =>
//{
//    var pessoa = new Pessoa() { Nome="Fabio"};
//    return pessoa;
//});

[Serializable]
public class Pessoa
{
    public string Nome { get; set; }
}

public class Pais
{
    public string Nome { get; set; }
}


public class HomeViewModel
{
    //public Pais PaisSelecionado { get; set; }
    public List<Pais> Paises { get; set; }
    public List<Pessoa> Pessoas { get; set; }
}