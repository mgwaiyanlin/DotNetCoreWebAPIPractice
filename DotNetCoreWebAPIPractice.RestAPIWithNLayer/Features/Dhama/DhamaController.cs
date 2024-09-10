using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNetCoreWebAPIPractice.RestAPIWithNLayer.Features.Dhama
{
    [Route("api/[controller]")]
    [ApiController]
    public class DhamaController : ControllerBase
    {
        private async Task<List<Dhama>> GetDhamaAsync()
        {
            string jsonString = await System.IO.File.ReadAllTextAsync("DhamaData.json");
            var model = JsonConvert.DeserializeObject<List<Dhama>>(jsonString);

            return model!;
        }

        [HttpGet("dhama_titles")]
        public async Task<IActionResult> GetDhamaTitles()
        {
            var data = await GetDhamaAsync();

            return Ok(data[0].Data[0].Title);
        }
    }

    public class Dhama
    {
        public int GroupId { get; set; }
        public string Title { get; set; }
        public List<DhamaData> Data { get; set; }
    }

    public class DhamaData
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
    }

}
