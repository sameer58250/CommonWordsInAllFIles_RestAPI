using CommonWordsInAllFilesAPI.Services;
using System.Web.Http;

namespace CommonWordsInAllFilesAPI.Controllers
{
    public class CommonWordsController : ApiController
    {
        private readonly WordHelper wordHelper;
        CommonWordsController()
        {
            this.wordHelper = new WordHelper();
        }
        [Route("GetCommonWords")]
        [HttpPost]
        public string GetCommonWords([FromBody]string[] filePaths)
        {
            return wordHelper.ExtractCommonWords(filePaths);
        }
    }
}