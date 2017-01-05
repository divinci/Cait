using Cait.Core.Interfaces.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
