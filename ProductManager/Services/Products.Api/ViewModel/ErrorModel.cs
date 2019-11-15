using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Api.ViewModel
{
    public class ErrorModel
    {
        public string Message { get; set; }

        public ErrorModel(string message)
        {
            Message = message;
        }
    }
}
