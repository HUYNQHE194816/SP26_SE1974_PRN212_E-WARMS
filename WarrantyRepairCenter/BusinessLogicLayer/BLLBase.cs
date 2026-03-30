using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarrantyRepairCenter.Authentication;

namespace WarrantyRepairCenter.BusinessLogicLayer
{
    internal class BLLBase
    {
        protected static void CheckAuth()
        {
            if (!AuthHelper.IsAuthenticated())
                throw new UnauthenticatedException();
        }
    }
}
