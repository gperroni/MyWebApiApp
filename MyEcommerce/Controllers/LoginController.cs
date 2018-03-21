using Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyEcommerce.Controllers
{
    /// <summary>
    /// Classe controladora para realizar autenticação
    /// </summary>
    public class LoginController : ApiController
    {
        /// <summary>
        /// Realiza a busca do CLIENTE por e-mail e senha, retornando o CPF do mesmo
        /// </summary>
        /// <param name="email">E-mail do cliente</param>
        /// <param name="senha">Senha do cliente</param>
        /// <returns>CPF do cliente ou mensagem de erro caso não seja encontrado</returns>
        [HttpGet]
        public HttpResponseMessage Autenticar(string email, string senha)
        {
            var savedCpf = new ClienteBusiness().BuscarCpfPorEmailEhSenha(email, senha);

            // Retorna status 200 e o CPF encontrado ou 403 caso não tenha sido possível encontrar
            var response = savedCpf == null
                ? Request.CreateResponse(HttpStatusCode.Forbidden)
                : Request.CreateResponse(HttpStatusCode.OK, savedCpf);

            return response;
        }
    }
}
