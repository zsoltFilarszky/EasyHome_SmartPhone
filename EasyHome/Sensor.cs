using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace api_tester
{
    public class Sensor
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "changeable")]
        public Changable Changeable { get; set; }

        public Sensor(int id, string type, Changable changeable)
        {
            Id = id;
            Type = type;
            Changeable = changeable;
        }
    }


}