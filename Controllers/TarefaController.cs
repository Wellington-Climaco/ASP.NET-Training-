using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreinandoApi.Data;
using TreinandoApi.Models;
using TreinandoApi.Repository;
using TreinandoApi.Repository.Interface;
using TreinandoApi.ViewModels.Tarefa2;

namespace TreinandoApi.Controllers
{
    [ApiController]

    public class TarefaController: ControllerBase
    {
        private readonly ITarefaRepository _TarefaRepository;

        public TarefaController(ITarefaRepository repository)
        {
            _TarefaRepository = repository;
        }

        [HttpGet("v1/tarefas")]
        public async Task<IActionResult> ListaTarefa()
        {            
            var tarefas = await _TarefaRepository.BuscarTudo();

            if(tarefas == null) return NotFound("Não existe tarefas pendentes!!");
            return Ok(tarefas);
        }

        [HttpGet("v1/tarefas/{id:int}")]
        public async Task<IActionResult> ListarPorId([FromRoute] int id)
        {
            try
            {
                var tarefa = await _TarefaRepository.BuscarPorId(id);
                if (tarefa == null) return NotFound("Tarefa não encontrada!!");
                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "0765X - Erro interno");
            }
        }

        [HttpPost("v1/tarefas/criando")]
        public async Task<IActionResult> CriandoTarefa([FromBody] CriarTarefasViewModel tarefaPersonalizada)
        {
            try
            {
                var identificacao = await _TarefaRepository.ValidarUsuario(tarefaPersonalizada);
                if (identificacao == null) return NotFound("seu usuario nao foi encontrado");
                if (identificacao.ListaTarefas.Count >= 2) return StatusCode(500, "Usuario ja possui duas tarefas pendentes");

                var Tarefa = new Tarefa { NomeTarefa = tarefaPersonalizada.NomeTarefa, Descricao = tarefaPersonalizada.Descricao, Usuario = identificacao };
                var task = await _TarefaRepository.Adicionar(Tarefa, tarefaPersonalizada);
                return Ok($"Tarefa criada com sucesso!!");
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

        [HttpPut("v1/tarefas/Atualizando")]
        public async Task<IActionResult> AtualizarTarefas([FromBody]TarefaViewModel tarefaViewModel)
        {
            try
            {
                var validacao = await _TarefaRepository.BuscarPorId(tarefaViewModel.Id);
                if (validacao == null) return NotFound($"Tarefa com Id: {tarefaViewModel.Id} não encontrado!!");

                var Tarefa = await _TarefaRepository.Atualizar(tarefaViewModel);

                return Ok(Tarefa);
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

        [HttpDelete("v1/tarefas/remover/{Id:int}")]
        public async Task<IActionResult> RemoverTarefa([FromRoute] int Id)
        {
            var tarefa = await _TarefaRepository.BuscarPorId(Id);
            if (tarefa == null) return NotFound("Tarefa não encontrada!!");

            var resultado = await _TarefaRepository.Deletar(tarefa);
            return Ok(resultado);           
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
