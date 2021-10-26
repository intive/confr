using System;
using System.Collections.Generic;
using System.Text;

namespace Intive.ConfR.Domain.Entities
{
    public class GraphListResponse<T>
    {
        public List<T> Value { get; set; }
    }
}
