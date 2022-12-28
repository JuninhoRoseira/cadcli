using Dominio.Interfaces.Servicos;
using Dominio.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CadCliWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientesService _clientesServico;

        public HomeController(IClientesService clientesServico)
        {
            _clientesServico = clientesServico;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/ObterClientes/ 
        public JsonResult ObterClientes()
        {
            return Json(_clientesServico.Listar());
        }

        // GET: /Home/ExcluirCliente/ 
        public JsonResult ExcluirCliente(string id)
        {
            try
            {
                _clientesServico.Excluir(long.Parse(id));

                return Json(new
                {
                    Mensagem = "Cliente excluído com sucesso.",
                    Erro = false
                });

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Mensagem = ex.Message,
                    Erro = true
                });
            }

        }

        [HttpPost]
        public JsonResult SalvarCliente(ClienteViewModel cliente)
        {
            if (cliente.Id > 0)
            {
                _clientesServico.Alterar(cliente);

                return Json(cliente);

            }

            var clienteNovo = _clientesServico.Criar(cliente);

            return Json(clienteNovo);

        }

    }

}
