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
    [EnableCors(origins: "http://localhost:8100", headers: "*", methods: "*")]
    public class ClientesController : ApiController
    {
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

            var response = msgError == null
                ? Request.CreateResponse(HttpStatusCode.OK, savedCliente)
                : Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(msgError));

            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }

        public HttpResponseMessage PostCliente([FromBody] Cliente cliente)
        {
            try
            {
                var clienteBusiness = new ClienteBusiness();
                var errorMsg = clienteBusiness.CadastrarCliente(cliente);

                return errorMsg == null
                    ? Request.CreateResponse(HttpStatusCode.OK, cliente)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(errorMsg));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ErrorMsgs.Get("ERRO_CADASTRO_CLIENTE", ex.ToString(), cliente.Cpf)));
            }

        }

        [Route("api/Clientes/{cpfCliente}")]
        [HttpPut]
        public HttpResponseMessage PutCliente(string cpfCliente, Cliente cliente)
        {
            try
            {


                var clienteBusiness = new ClienteBusiness();
                var errorMsg = clienteBusiness.AtualizarCliente(cliente);

                return errorMsg == null
                    ? Request.CreateResponse(HttpStatusCode.OK, cliente)
                    : Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(errorMsg));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(ErrorMsgs.Get("ERRO_ATUALIZACAO_CLIENTE", ex.ToString(), cliente.Cpf)));
            }
        }

        [Route("api/Clientes/{cpfCliente}")]
        [HttpDelete]
        public HttpResponseMessage DeleteCliente(string cpfCliente)
        {
            string msgError = "";
            try
            {
                var clienteBusiness = new ClienteBusiness();
                msgError = clienteBusiness.ExcluirCliente(cpfCliente);

                return msgError == null
                    ? Request.CreateResponse(HttpStatusCode.OK)
                : Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(msgError));
            }
            catch (Exception ex)
            {
                msgError = string.Format(ErrorMsgs.Get("ERRO_BUSCAR_CLIENTE", ex.ToString(), cpfCliente));
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(msgError));
            }
        }
    }
}
