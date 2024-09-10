using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNetCoreWebAPIPractice.RestAPIWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {

        private async Task<LatHtaukBayDin> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonStr);

            return model!;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var model = await GetDataAsync();

            return Ok(model.questions);
        }

        [HttpGet("number_list")]
        public async Task<IActionResult> GetNumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.numberList);
        }

        [HttpGet("{questionId}/{answerId}")]
        public async Task<IActionResult> GetQuestionAndAnswer(int questionId, int answerId)
        {
            var model = await GetDataAsync();
            return Ok(model.answers.FirstOrDefault(x => x.questionNo == questionId && x.answerNo == answerId));
        }

    }


    public class LatHtaukBayDin
    {
        public Question[] questions { get; set; }
        public Answer[] answers { get; set; }
        public string[] numberList { get; set; }
    }

    public class Question
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answer
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }


}
