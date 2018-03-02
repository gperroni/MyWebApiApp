using Business;
using Model;
using Model.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MyEcommerce.Controllers
{

    /// <summary>
    /// Classe controladora para Buscar, Criar, Atualizar e Excluir cliente
    /// </summary>
    [EnableCors(origins: "http://localhost:8100", headers: "*", methods: "*")]
    public class ClientesController : ApiController
    {


        /// <summary>
        /// Busca dados do cliente através de seu CPF.
        /// </summary>
        /// <param name="cpfCliente">CPF do cliente</param>
        /// <returns>Retorna dados do Cliente encontrado ou mensagem de alerta caso não seja ou erro</returns>
        [Route("api/Clientes/{cpfCliente}")]
        [HttpGet]
        public HttpResponseMessage GetCliente(string cpfCliente)
        {
            string msgError = null;
            Cliente savedCliente = null;
            try
            {
                savedCliente = new ClienteBusiness().BuscarPorCpf(cpfCliente);

                if (savedCliente == null)
                    msgError = ErrorMsgs.Get("CLIENTE_CPF_NAO_ECONTRADO", cpfCliente);

            }
            catch (Exception ex)
            {
                msgError = ErrorMsgs.Get("ERRO_BUSCAR_CLIENTE", ex.ToString(), cpfCliente);
            }

            // Retorna 404 caso não encontre o cliente ou haja algum erro.
            // Retorna 200 caso encontre o cliente
            var response = msgError == null
                ? Request.CreateResponse(HttpStatusCode.OK, savedCliente)
                : Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(msgError));

            return response;
        }


        /// <summary>
        /// Realiza a criação do CLIENTE, retornando os dados do cliente recém gravado
        /// </summary>
        /// <param name="cliente">ENTIDADE Cliente a ser gravada</param>
        /// <returns>Retorna dados do cliente ou mensagem de erro caso tenha tido algum problema na criação</returns>
        public HttpResponseMessage PostCliente([FromBody] Cliente cliente)
        {
            try
            {
                var clienteBusiness = new ClienteBusiness();
                var errorMsg = clienteBusiness.CadastrarCliente(cliente);

                // Caso não tenha retornado nenhuma mensagem de erro, retorna status code 200 e os dados do cliente
                // Caso contrário, retorna 400
                return errorMsg == null
                    ? Request.CreateResponse(HttpStatusCode.OK, cliente)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(errorMsg));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ErrorMsgs.Get("ERRO_CADASTRO_CLIENTE", ex.ToString(), cliente.Cpf)));
            }

        }


        /// <summary>
        /// Realiza a atualização do CLIENTE, retornando os dados do cliente recém atualizado
        /// </summary>
        /// <param name="cpfCliente">CPF do cliente a ser atualizado</param>
        /// <param name="cliente">Dados do cliente para atualzação</param>
        /// <returns>Retorna os dados do cliente atualizados ou mensagem de erro caso tenha tido algum problema na atualização</returns>
        [Route("api/Clientes/{cpfCliente}")]
        [HttpPut]
        public HttpResponseMessage PutCliente(string cpfCliente, Cliente cliente)
        {
            try
            {


                var clienteBusiness = new ClienteBusiness();
                var errorMsg = clienteBusiness.AtualizarCliente(cliente);

                // Caso não tenha retornado nenhuma mensagem de erro, retorna status code 200 e os dados do cliente
                // Caso contrário, retorna 400 e a mensagem de erro
                return errorMsg == null
                    ? Request.CreateResponse(HttpStatusCode.OK, cliente)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(errorMsg));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ErrorMsgs.Get("ERRO_ATUALIZACAO_CLIENTE", ex.ToString(), cliente.Cpf)));
            }
        }


        /// <summary>
        /// Realiza e exclusão do CLIENTE
        /// </summary>
        /// <param name="cpfCliente">CPF do cliente a ser excluído</param>
        /// <returns>Retorna mensagem de erro caso a exclusão não tenha ocorrido</returns>
        [Route("api/Clientes/{cpfCliente}")]
        [HttpDelete]
        public HttpResponseMessage DeleteCliente(string cpfCliente)
        {
            string msgError = "";
            try
            {
                var clienteBusiness = new ClienteBusiness();
                msgError = clienteBusiness.ExcluirCliente(cpfCliente);

                // Caso não tenha retornado nenhuma mensagem de erro, retorna status code 200 
                // Caso contrário, retorna 404 e a mensagem de erro
                return msgError == null
                    ? Request.CreateResponse(HttpStatusCode.OK)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(msgError));
            }   
            catch (Exception ex)
            {
                msgError = string.Format(ErrorMsgs.Get("ERRO_BUSCAR_CLIENTE", ex.ToString(), cpfCliente));
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(msgError));
            }
        }
    }
}
