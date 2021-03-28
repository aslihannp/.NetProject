using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
 


        public Result(bool success, string message):this(success) //böylecek success de set eglirmiş olur
        {
            Message = message;
        }
        public Result(bool success) //overloading
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; } //setter'ı yok
        //NOT: getter read only dir. Read only'ler constructor da set edilebilir.
        //Burada constructor dışında set etmeyeceğimiz için setter yazmamaıza gerek kalmadı.
    }
}
