public class ProtocolReader : IDisposable
{
    private readonly BinaryReader _binaryReader;

    public ProtocolReader(byte[] data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        _binaryReader = new BinaryReader(new MemoryStream(data));
    }

    public byte ReadByte()
    {
        return _binaryReader.ReadByte();
    }
    public ushort ReadUShort()
    {
        return _binaryReader.ReadUInt16();
    }


    public void Dispose()
    {
        _binaryReader.Dispose();
    }
}