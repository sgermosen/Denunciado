using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Denounces.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 

namespace Denounces.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslatorController : ControllerBase
    {
        private readonly ITranslator _translator;
        public TranslatorController(ITranslator translator)
        {
            _translator = translator;
        }

        [HttpGet]
        [Route("Translate")]
        public ActionResult Translate(string text, string language)
        {
            var translatedText = _translator.TranslateText(text, language);
            return Ok(translatedText);
        }

        [HttpGet]
        [Route("TranslateHtml")]
        public ActionResult TranslateHtml(string html, string language)
        {
            var translatedText = _translator.TranslateHtml(html, language);
            return Ok(translatedText);
        }

    }
}