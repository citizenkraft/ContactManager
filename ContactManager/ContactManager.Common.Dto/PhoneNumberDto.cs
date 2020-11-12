using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Common.Dto
{
    public class PhoneNumberDto
    {
        public int PhoneNumberId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
