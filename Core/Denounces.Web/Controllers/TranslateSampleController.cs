using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Denounces.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Denounces.Web.Controllers
{
    public class TranslateSampleController : Controller
    {
        private readonly ITranslator _translator;

        public TranslateSampleController(ITranslator translator)
        {
            _translator = translator;


        }
        public ActionResult Index()
        {
            var text = "Translate using Google";
            var translatedText = _translator.TranslateText(text, "fr");
            ViewBag.Msg = translatedText;
            return View("Index", translatedText);
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