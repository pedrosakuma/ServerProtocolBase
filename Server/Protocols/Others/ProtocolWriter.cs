namespace ServerProtocol.Server.Protocols.Others
{
    public class ProtocolWriter : IDisposable
    {
        private readonly MemoryStream _memoryStream;
        private readonly BinaryWriter _binaryWriter;

        public ProtocolWriter()
        {
            _memoryStream = new MemoryStream();
            _binaryWriter = new BinaryWriter(_memoryStream);
        }

        public void WriteByte(byte value)
        {
            _binaryWriter.Write(value);
        }

        public void WriteUShort(ushort value)
        {
            _binaryWriter.Write(value);
        }

        public byte[] GetBytes()
        {
            return _memoryStream.ToArray();
        }

        public int Length
        {
            get { return (int)_memoryStream.Length; }
        }

        public void Dispose()
        {
            _binaryWriter.Dispose();
            _memoryStream.Dispose();
        }
    }
}
