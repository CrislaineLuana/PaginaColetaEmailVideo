﻿using Microsoft.AspNetCore.Mvc;
using PaginaColetaEmailVideo.Filtros;
using PaginaColetaEmailVideo.Models;
using PaginaColetaEmailVideo.Services.EmailService;

namespace PaginaColetaEmailVideo.Controllers
{
    [UsuarioLogado]
    public class EmailController : Controller
    {
        private readonly IEmailInterface _emailInterface;
        public EmailController(IEmailInterface emailInterface)
        {
            _emailInterface = emailInterface;
        }

        public async Task<ActionResult<List<EmailModel>>> Index(string? pesquisar)
        {
            if(pesquisar != null)
            {
                var resgistrosEmailsFiltro = await _emailInterface.ListarEmails(pesquisar);
                return View(resgistrosEmailsFiltro);
            }


            var registrosEmails = await _emailInterface.ListarEmails();
            return View(registrosEmails);
        }


        [HttpGet]
        public async Task<ActionResult<EmailModel>> DetalhesEmail(int id)
        {
            var registroEmail = await _emailInterface.ListarEmailPorId(id);
            return View(registroEmail);
        }



        [HttpPost]
        public async Task<ActionResult<EmailModel>> EnviarEmail(string enderecoEmail, string textoEmail, string assuntoEmail, int id)
        {

            var email = await _emailInterface.ListarEmailPorId(id);

            if(email.Status == false)
            {
                TempData["MensagemErro"] = "Não é possível encaminhar email para um registro inativo!";
                return View("DetalhesEmail", email);
            }

            if(textoEmail == null || assuntoEmail == null)
            {

                TempData["MensagemErro"] = "Insira um assunto e um corpo para o email!";
                return View("DetalhesEmail", email);
            }


            bool resultado = _emailInterface.EnviarEmail(enderecoEmail, textoEmail, assuntoEmail);

            if(resultado == true)
            {
                TempData["MensagemSucesso"] = "Email encaminhado com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um problema no envio do email!";

            }


            return RedirectToAction("Index");
        }
    }
}
