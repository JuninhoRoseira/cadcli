using Dominio.ViewModels;

namespace Dominio.Interfaces.Servicos
{
    public interface IClientesService
    {
        List<ClienteViewModel> Listar();
        ClienteViewModel Criar(ClienteViewModel clienteVM);
        void Alterar(ClienteViewModel clienteVM);
        void Excluir(long id);
    }
}
