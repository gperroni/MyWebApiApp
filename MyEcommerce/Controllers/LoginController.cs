using MyEcommerce.Models.Repositories;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyEcommerce.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Autenticar(string email, string senha)
        {
            var savedCliente = new ClienteRepository().BuscarPorEmailEhSenha(email, senha);
            var response = savedCliente == null 
                ? Request.CreateResponse(HttpStatusCode.Forbidden, "Cliente não encontrado") 
                :  Request.CreateResponse(HttpStatusCode.OK, savedCliente);

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }
    }
}
