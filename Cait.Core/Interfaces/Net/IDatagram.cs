namespace Cait.Core.Interfaces.Net
{
    public interface IDatagram
    {
        IDatagramHeader DatagramHeader { get; }
        IDatagramPayload DatagramPayload { get; }

        byte[] GetBytes();
    }
}