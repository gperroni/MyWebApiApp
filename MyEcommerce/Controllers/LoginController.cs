using Business;
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
            var savedCpf = new ClienteBusiness().BuscarCpfPorEmailEhSenha(email, senha);
            var response = savedCpf == null
                ? Request.CreateResponse(HttpStatusCode.Forbidden)
                : Request.CreateResponse(HttpStatusCode.OK, savedCpf);

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }
    }
}
