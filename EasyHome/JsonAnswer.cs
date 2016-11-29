using Newtonsoft.Json;

namespace EasyHome
{
    public class JsonAnswer
    {
        public JsonAnswer(string answer)
        {
            Answer = answer;
        }
        public string Answer { get; set; }
    }
}