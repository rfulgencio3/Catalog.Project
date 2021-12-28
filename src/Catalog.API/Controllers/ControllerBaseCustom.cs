using Catalog.API.Extensions;
using Catalog.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    public abstract class ControllerBaseCustom : ControllerBase
    {
        protected ControllerBaseCustom()
        {
        }

        private ICollection<string> Errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation()) return Ok(GenerateResponseSuccess(result));
            return BadRequest(GenerateResponseError());
        }

        protected ActionResult CustomResponse(ResponseBase result)
        {
            if (result.Errors.Any()) AddErrors(result.Errors);
            return CustomResponse();
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {

            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(string[] erros)
        {

            foreach (var erro in erros)
            {
                AddError(erro);
            }

            return CustomResponse();

        }

        private object GenerateResponseSuccess(object result)
        {
            // Coloque aqui o padrão de resposta de sucesso
            // Exemplo de objeto de retorno:
            //var response = new
            //{
            //    Data = result
            //};

            //return response;

            return result;

        }

        private List<object> GenerateResponseError()
        {
            List<object> responseError = new List<object>();

            foreach (var value in Errors)
            {
                responseError.Add(CreateErrorResponse(value));
            }

            return responseError;
        }

        protected bool ValidOperation()
        {
            return !Errors.Any();
        }

        protected void AddError(string erro)
        {
            Errors.Add(erro);
        }

        protected void AddErrors(ICollection<string> erros)
        {
            foreach (var erro in erros)
            {
                AddError(erro);
            }
        }

        protected void ClearErrors()
        {
            Errors.Clear();
        }

        public static object CreateErrorResponse(string value, int StatusCode = 400)
        {

            return new
            {
                status = StatusCode,
                code = value.GetHashCode(),
                error = value,
                localizedMessage = string.Empty,
                timestamp = DateTime.UtcNow.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
                path = MyHttpContext.AppFullUrl()
            };

        }

    }
}