using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreinandoApi.Data;
using TreinandoApi.Models;
using TreinandoApi.ViewModels.Tarefa2;

namespace TreinandoApi.Controllers
{
    [ApiController]

    public class TarefaController: ControllerBase
    {

        [HttpGet("v1/tarefas")]
        public async Task<IActionResult> ListarTodasTarefas([FromServices] DbContexto contexto)
        {
            try
            {
                var Tarefas = await contexto.Tarefas.AsNoTracking().Include(x => x.Usuario).Select(x => new ListaTarefasViewModel
                {
                    Id = x.Id,
                    NomeTarefa = x.NomeTarefa,
                    Descricao = x.Descricao,
                    DataCriacao = x.DataCriacao,
                    Usuario = x.Usuario.Nome
                }).ToListAsync();

                return Ok(Tarefas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno");
            }

        }

        [HttpGet("v1/tarefas/{Id:int}")]
        public async Task <IActionResult> ListarTarefa([FromRoute] int Id, [FromServices] DbContexto contexto)
        {
            try
            {
                var tarefa = await contexto.Tarefas.AsNoTracking().Include(x=>x.Usuario).Select(x=> new ListaTarefasViewModel
                {
                    Id = x.Id,
                    NomeTarefa = x.NomeTarefa,
                    Descricao = x.Descricao,
                    DataCriacao = x.DataCriacao,
                    Usuario = x.Usuario.Nome
                }).FirstOrDefaultAsync(x => x.Id == Id);
                                
                if (tarefa == null) return NotFound("Tarefa nao encontrada!!");

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno");

            }
        }
        
        [HttpPost("v1/tarefas/criar")]
        public async Task<IActionResult> CriarTarefa([FromBody] CriarTarefasViewModel tarefa, [FromServices] DbContexto contexto)
        {
            try
            {
                var IdentificacaoUsuario = await contexto.Usuarios.Include(x=>x.ListaTarefas).FirstOrDefaultAsync(x => x.Id == tarefa.Id_Usuario);

                if (IdentificacaoUsuario == null) return NotFound("seu usuario nao foi encontrado");
                if (IdentificacaoUsuario.ListaTarefas.Count >= 2) return StatusCode(500,"Usuario já possui duas tarefas pendentes!!");
                
                var Tarefa = new Tarefa { NomeTarefa = tarefa.NomeTarefa, Descricao = tarefa.Descricao,Usuario=IdentificacaoUsuario};
                
                await contexto.Tarefas.AddAsync(Tarefa);
                await contexto.SaveChangesAsync();
                return Ok(Tarefa);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possivel criar Tarefa");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno");

            }
        }

        [HttpPut("v1/tarefas/update")]
        public async Task<IActionResult> AtualizarTarefa([FromBody] TarefaViewModel tarefa, [FromServices] DbContexto contexto)
        {
            var tarefaAtualizada = contexto.Tarefas.FirstOrDefault(x => x.Id == tarefa.Id);
            if (tarefaAtualizada == null) return NotFound("Tarefa não encontrada!");

            try
            {
                tarefaAtualizada.NomeTarefa = tarefa.NomeTarefa;
                tarefaAtualizada.Descricao = tarefa.Descricao;

                contexto.Update(tarefaAtualizada);
                await contexto.SaveChangesAsync();

                return Ok(tarefaAtualizada);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possivel atualizar usuario");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno");

            }
        }

        [HttpDelete("v1/tarefas/delete/{id:int}")]
        public async Task<IActionResult> DeletarTarefa([FromRoute] int id, [FromServices] DbContexto contexto)
        {
            try
            {
                var tarefa = contexto.Tarefas.FirstOrDefault(x => x.Id == id);

                if (tarefa == null) return NotFound("Tarefa nao encontrada");

                contexto.Remove(tarefa);
                await contexto.SaveChangesAsync();

                return Ok("Conteudo excluido com sucesso!!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno");

            }

        }


        


    }
}
