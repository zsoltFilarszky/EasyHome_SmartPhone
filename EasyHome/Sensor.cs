﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyHome
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

        public override string ToString()
        {
            return "Id: " + Id + " Type: " + Type;
        }
    }


}