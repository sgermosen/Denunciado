using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;

namespace Denounces.Web.Helpers
{
    public interface ITranslator
    {
        string TranslateText(string text, string language);
        string TranslateHtml(string text, string language);
    }
    public class Translator : ITranslator
    {
        private readonly TranslationClient client;
        public Translator()
        {
            client = TranslationClient.Create();
        }
        public string TranslateHtml(string text, string language)
        {
            var response = client.TranslateHtml(text, language);
            return response.TranslatedText;
        }
        public string TranslateText(string text, string language)
        {
            var response = client.TranslateText(text, language);
            return response.TranslatedText;
        }
    }

}
