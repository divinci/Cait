namespace Cait.Core.Interfaces.Net
{
    public interface IDatagramHeader
    {
        byte[] GetBytes(IDatagramPayload header);
    }
}