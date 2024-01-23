using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using TreinandoApi.Data;
using TreinandoApi.Models;
using TreinandoApi.Repository.Interface;
using TreinandoApi.ViewModels.Tarefa2;

namespace TreinandoApi.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly  DbContexto _contexto;
        public TarefaRepository(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<Tarefa> Adicionar(Tarefa tarefa, CriarTarefasViewModel criarTarefas)
        {
            await _contexto.Tarefas.AddAsync(tarefa);
            _contexto.SaveChanges();
            return tarefa;
        }

        public async Task<Tarefa> Atualizar(TarefaViewModel tarefaViewModel)
        {
            var Tarefa = await _contexto.Tarefas.FirstOrDefaultAsync(x => x.Id == tarefaViewModel.Id);

            Tarefa.NomeTarefa = tarefaViewModel.NomeTarefa;
            Tarefa.Descricao = tarefaViewModel.Descricao;

            _contexto.Tarefas.Update(Tarefa);
            await _contexto.SaveChangesAsync();
            return Tarefa;
            
        }

        public async Task<ListaTarefasViewModel> BuscarPorId(int id)
        {
            var tarefa = await _contexto.Tarefas.AsNoTracking().Include(x=>x.Usuario).Select(x=> new ListaTarefasViewModel
            {Id = x.Id,NomeTarefa= x.NomeTarefa,Descricao = x.Descricao,DataCriacao =x.DataCriacao, Usuario = x.Usuario.Nome}).FirstOrDefaultAsync(x=> x.Id == id);
            return tarefa;              
        }

        public async Task<List<ListaTarefasViewModel>> BuscarTudo()
        {
            return await _contexto.Tarefas.AsNoTracking().Include(x=>x.Usuario).Select(x=> new ListaTarefasViewModel 
            { Id = x.Id,NomeTarefa = x.NomeTarefa ,Descricao = x.Descricao,DataCriacao=x.DataCriacao,Usuario = x.Usuario.Nome}).ToListAsync();
        }

        public async Task<string> Deletar(ListaTarefasViewModel tarefa)
        {
            var Tarefa = await _contexto.Tarefas.FirstOrDefaultAsync(x=>x.Id == tarefa.Id);
            _contexto.Remove(Tarefa);
            await _contexto.SaveChangesAsync();
            return ("Tarefa excluida com sucesso!!");
        }

        public Task<Usuarios> ValidarUsuario(CriarTarefasViewModel criarTarefas)
        {
            var IdentificacaoUsuario = _contexto.Usuarios.Include(x => x.ListaTarefas).FirstOrDefaultAsync(x => x.Id == criarTarefas.Id_Usuario);
            return IdentificacaoUsuario;
        }




    }
}
