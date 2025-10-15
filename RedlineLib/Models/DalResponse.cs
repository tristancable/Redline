using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedlineLib.Models
{
    public class DalResponse<T>
    {
        public ResponseCodes Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}