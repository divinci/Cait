namespace Cait.Core.Interfaces.Net
{
    public interface IDatagramPayload
    {
        byte[] GetBytes(IDatagramHeader header);
    }
}