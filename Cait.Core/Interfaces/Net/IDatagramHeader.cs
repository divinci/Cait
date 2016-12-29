using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cait.Core.Interfaces.Net
{
    public interface IDatagramHeader
    {
        byte[] GetBytes();
        byte[] GetBytes(IDatagramPayload header);
    }
}
