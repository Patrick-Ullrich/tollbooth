using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ApiKey
    {
        public int ApiKeyId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
