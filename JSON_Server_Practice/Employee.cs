using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JSON_Server_Practice
{
    public class Employee
    {
        [JsonProperty("id")]
        public string EmployeeId { get; set; }
        [JsonProperty("name")]
        public string EmployeeName { get; set; }
        [JsonProperty("salary")]
        public string Salary { get; set; }

    }
}
