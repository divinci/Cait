using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cait.Core.Interfaces.Net
{
    public interface IDatagramPayload
    {
        byte[] GetBytes();
        byte[] GetBytes(IDatagramHeader header);
    }
}
