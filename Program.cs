
using fiap2022.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
//builder.Services.AddControllers();

var app = builder.Build();

#region middlewares-ex
//app.Use(async (context, next) =>
//{
//    //logica
//    await next.Invoke();

//});

//app.Use(async (context, next) =>
//{
//    //logica

//    context.Response.Headers.Add("x-header", "valuedoheader");
//    await next.Invoke();

//});

//app.Map("/admin", myApp =>
//{
//    myApp.Run(async (context) =>
//    {
//        await context.Response.WriteAsync("Admin Area");

//    });
//});

//app.MapWhen(context => context.Request.Query.ContainsKey("fiap"),
//    myApp =>
//    {
//        myApp.Run(async (context) =>
//        {
//            await context.Response.WriteAsync("Quesristring Fiap");

//        });
//    });

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Ola");

//});

#endregion



//app.UseMiddleware<MeuMiddleware>();
app.UseMeuMiddlwareDeLogs();




#region mvc
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("ErrorProd");
}

app.UseStaticFiles();


app.UseRouting();
app.MapControllerRoute(
    name: "customizada",
    defaults: new { controller = "Home", action = "Index" },
    pattern: "minha-area/{id?}"
//pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.MapControllerRoute(
    name: "default",
    defaults: new { controller = "Home", action = "Index" },
    pattern: "{controller}/{action}/{id?}"
//pattern: "{controller=Home}/{action=Index}/{id?}"
);
#endregion

app.Run();







//app.MapGet("/", () =>
//{
//    var pessoa = new Pessoa() { Nome="Fabio"};
//    return pessoa;
//});


namespace ViewModels
{
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
}