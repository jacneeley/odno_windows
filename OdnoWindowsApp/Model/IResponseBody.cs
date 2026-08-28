using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdnoWindowsApp.Model
{
    internal interface IResponseBody
    {
        Task<ResponseBody> get();
        void reset();
    }
}
