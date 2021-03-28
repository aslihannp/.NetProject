using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)//params verince run içerisine istediğimiz kadar IResult verebiliyoruz parametre olarak
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;
                }

            }
            return null;
        }
    }
}
