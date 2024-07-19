using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiCrud.Estudantes;
public static class EstudantesRotas
{
  public static void AddRotasEstudantes(this WebApplication app)
  {
    var rotasEstudantes = app.MapGroup("estudantes");

    rotasEstudantes.MapPost("", async (AddEstudanteRequest request, AppDbContext context) =>
    {
      var existeEstudante = await context.Estudantes.AnyAsync(estudante => estudante.Nome == request.Nome);

      if (!existeEstudante)
      {
        var novoEstudante = new Estudante(request.Nome);
        await context.Estudantes.AddAsync(novoEstudante);
        await context.SaveChangesAsync();
      }
    });

    //rotasEstudantes.MapGet("", () => "Hello estudante!");

    //app.MapPost("estudantes", () =>)  
    //app.MapGet("estudantes", () => new Estudante("Claudinho"));
  }
}