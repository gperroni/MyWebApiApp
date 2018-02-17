using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyEcommerce.Models;
using MyEcommerce.Models.Repositories;
using System.Web.Http.Cors;

namespace MyEcommerce.Controllers
{
    [EnableCors(origins: "http://localhost:8100", headers: "*", methods: "*")]
    public class ClientesController : ApiController
    {
        private static readonly Dictionary<string, string> ERROR_MSGS = new Dictionary<string, string>
        {
            {"CPF_CADASTRADO", "CPF já cadastrado" },
            {"EMAIL_CADASTRADO", "E-mail já cadastrado" },
            {"ERRO_CADASTRO_CLIENTE", "Erro {0} ao criar cliente com CPF {1}"},
            {"CLIENTE_CPF_NAO_ECONTRADO", "Cliente com CPF: {0} não foi encontrado"},
            {"ERRO_ATUALIZACAO_CLIENTE", "Erro {0} ao atualizar cliente com CPF {1}" },
            {"ERRO_BUSCAR_CLIENTE", "Erro {0} ao buscar cliente com CPF {1}" }
        };

        [Route("api/Clientes/{cpfCliente}")]
        [HttpGet]
        public HttpResponseMessage GetCliente(string cpfCliente)
        {
            string msgError = null;
            Cliente savedCliente = null;
            try
            {
                savedCliente = new ClienteRepository().BuscarPorCpf(cpfCliente);

                if (savedCliente == null)
                    msgError = string.Format(ERROR_MSGS["CLIENTE_CPF_NAO_ECONTRADO"], cpfCliente);

            }
            catch (Exception ex)
            {
                msgError = string.Format(ERROR_MSGS["ERRO_BUSCAR_CLIENTE"], ex.ToString(), cpfCliente);
            }

            var response = msgError == null
                ? Request.CreateResponse(HttpStatusCode.OK, savedCliente)
                : Request.CreateResponse(HttpStatusCode.NotFound, msgError);

            response.Headers.Add("Access-Control-Allow-Origin", "*");

            return response;
        }

        public HttpResponseMessage PostCliente([FromBody] Cliente cliente)
        {
            try
            {
                var clienteRepository = new ClienteRepository();

                var errorValidacaoDados = cliente.ValidarDados();
                if (!string.Empty.Equals(errorValidacaoDados))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errorValidacaoDados);

                if (clienteRepository.CpfJaCadastrado(cliente.Cpf))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ERROR_MSGS["CPF_CADASTRADO"]);

                if (clienteRepository.EmailJaCadastrado(cliente.Email))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ERROR_MSGS["EMAIL_CADASTRADO"]);


                clienteRepository.Create(cliente);

                return Request.CreateResponse(HttpStatusCode.OK, cliente);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(ERROR_MSGS["ERRO_CADASTRO_CLIENTE"], ex.ToString(), cliente.Cpf));
            }

        }

        [Route("api/Clientes/{cpfCliente}")]
        [HttpPut]
        public HttpResponseMessage PutCliente(string cpfCliente, Cliente cliente)
        {
            try
            {
                var clienteRepository = new ClienteRepository();
                var savedCliente = clienteRepository.BuscarPorCpf(cpfCliente);
                if (savedCliente == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, string.Format(ERROR_MSGS["CLIENTE_CPF_NAO_ECONTRADO"], cliente.Cpf));

                var errorValidacaoDados = cliente.ValidarDados();
                if (!string.Empty.Equals(errorValidacaoDados))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, errorValidacaoDados);

                if (cliente.Email != savedCliente.Email && clienteRepository.EmailJaCadastrado(cliente.Email))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ERROR_MSGS["EMAIL_CADASTRADO"]);

                savedCliente.AtualizarCadastro(cliente);
                clienteRepository.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, savedCliente);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(ERROR_MSGS["ERRO_ATUALIZACAO_CLIENTE"], ex.ToString(), cliente.Cpf));
            }
        }

        [Route("api/Clientes/{cpfCliente}")]
        [HttpDelete]
        public HttpResponseMessage DeleteCliente(string cpfCliente)
        {
            string msgError = "";
            try
            {
                var clienteRepository = new ClienteRepository();
                var savedCliente = clienteRepository.BuscarPorCpf(cpfCliente);
                if (savedCliente != null)
                {
                    clienteRepository.Delete(savedCliente);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                msgError = string.Format(ERROR_MSGS["CLIENTE_CPF_NAO_ECONTRADO"], cpfCliente);
            }
            catch (Exception ex)
            {
                msgError = string.Format(ERROR_MSGS["ERRO_BUSCAR_CLIENTE"], ex.ToString(), cpfCliente);
            }

            var error = new HttpError(msgError);
            return Request.CreateResponse(HttpStatusCode.NotFound, error);
        }
    }
}
