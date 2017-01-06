using Cait.Core.Interfaces.Net;
using System.Diagnostics.CodeAnalysis;

namespace Cait.Bitcoin.NetTests
{
    public class DummyIDatagramHeader : IDatagramHeader
    {
        [ExcludeFromCodeCoverage()]
        public byte[] GetBytes(IDatagramPayload header)
        {
            return new byte[0];
        }
    }
}