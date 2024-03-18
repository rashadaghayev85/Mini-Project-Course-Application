using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class HelperService:IHelperService
    {
        public bool CheckTryCount(ref int num)
        {
            num++;
            if (num > 3)
                return true;
            return false;

        }
    }
}
