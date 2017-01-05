using Cait.Bitcoin.Net.Constants;
using Cait.Core.Extensions;
using System;
using System.Linq;

namespace Cait.Bitcoin.Net.Extensions
{
    public static class ServiceExtensions
    {
        public static int AsBitfield(this ServiceFlag[] services)
        {
            return services.Select(service => (Enum)service).ToArray().CreateFlagsBitfield();
        }
    }
}