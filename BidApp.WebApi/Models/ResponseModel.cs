using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidApp.WebApi.Models
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public int StatusCode { get; set; }


        public void SetOk(T result)
        {
            this.Result = result;
            this.StatusCode = 200;
            this.IsSuccess = true;
        }
        public void SetNok()
        {

            this.StatusCode = 200;
            this.IsSuccess = false;
        }
        
    }
}
