using Dominio.Entidades;
using Dominio.Interfaces.Dados;
using Dominio.Interfaces.Servicos;
using Dominio.ViewModels;

namespace Servicos
{
    public class ClientesServico : IClientesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Cliente> _clientesRepo;

        public ClientesServico(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _clientesRepo = _unitOfWork.GetRepository<Cliente>();
        }

        public void Alterar(ClienteViewModel clienteVM)
        {
            var cliente = _clientesRepo.FindBy(c => c.Id == clienteVM.Id);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            var dataNascimento = DateTime.Parse(clienteVM.DataNascimento);

            cliente.Cidade = clienteVM.Cidade;
            cliente.Estado = clienteVM.Estado;
            cliente.Sexo = clienteVM.Sexo;
            cliente.Endereco = clienteVM.Endereco;
            cliente.Nome = clienteVM.Nome;
            cliente.DataNascimento = dataNascimento;
            cliente.Documento = clienteVM.Documento;

            _clientesRepo.Update(cliente);
            _unitOfWork.SaveChanges().Wait();

        }

        public ClienteViewModel Criar(ClienteViewModel clienteVM)
        {
            var clienteExistente = _clientesRepo.FindBy(c => c.Documento == clienteVM.Documento);

            if (clienteExistente != null)
            {
                throw new Exception("Cliente já existente com este documento");
            }

            Cliente cliente = new();

            var dataNascimento = DateTime.Parse(clienteVM.DataNascimento);

            cliente.Documento = clienteVM.Documento;
            cliente.Cidade = clienteVM.Cidade;
            cliente.Estado = clienteVM.Estado;
            cliente.Sexo = clienteVM.Sexo;
            cliente.Endereco = clienteVM.Endereco;
            cliente.Nome = clienteVM.Nome;
            cliente.DataNascimento = dataNascimento;

            _clientesRepo.Create(cliente);
            _unitOfWork.SaveChanges().Wait();

            clienteVM.Id = cliente.Id;

            return clienteVM;

        }

        public void Excluir(long id)
        {
            var cliente = _clientesRepo.FindBy(c => c.Id == id);

            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }

            _clientesRepo.Delete(cliente);
            _unitOfWork.SaveChanges().Wait();

        }

        public List<ClienteViewModel> Listar()
        {
            return _clientesRepo
                .GetBy(c => c.Id >= 0)
                .Select(c => new ClienteViewModel
                {
                    Id = c.Id,
                    Cidade = c.Cidade,
                    DataNascimento = c.DataNascimento.ToString("dd/MM/yyyy"),
                    Documento = c.Documento,
                    Endereco = c.Endereco,
                    Estado = c.Estado,
                    Nome = c.Nome,
                    Sexo = c.Sexo
                })
                .ToList();
        }

    }

}