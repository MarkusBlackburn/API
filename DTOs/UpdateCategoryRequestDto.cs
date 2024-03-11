using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UpdateCategoryRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string CategoryUrl { get; set; } = string.Empty;
    }
}