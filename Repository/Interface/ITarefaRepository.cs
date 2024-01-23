using System.Collections;
using TreinandoApi.Models;
using TreinandoApi.ViewModels.Tarefa2;

namespace TreinandoApi.Repository.Interface
{
    public interface ITarefaRepository
    {
        Task<List<ListaTarefasViewModel>> BuscarTudo();

        Task<ListaTarefasViewModel> BuscarPorId(int id);

        Task<Tarefa> Adicionar(Tarefa tarefa, CriarTarefasViewModel criarTarefas);

        Task<Usuarios> ValidarUsuario(CriarTarefasViewModel criarTarefas);

        Task<Tarefa> Atualizar(TarefaViewModel tarefaViewModel);

        Task<string> Deletar(ListaTarefasViewModel tarefa);


    }
}
