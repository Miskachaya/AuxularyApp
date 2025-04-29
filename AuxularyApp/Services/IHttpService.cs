using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxularyApp.Services
{
    public interface IHttpService
    {
        Task<string> GetDataAsync();
    }
}
