namespace LogTailer.Ui.Extensions
{
    using System.IO;
    using System.Reflection;

    public static class StreamReaderExtensions
    {
        private static readonly FieldInfo? CharPosField = typeof(StreamReader).GetField("charPos", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        private static readonly FieldInfo? ByteLenField = typeof(StreamReader).GetField("byteLen", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        private static readonly FieldInfo? CharBufferField = typeof(StreamReader).GetField("charBuffer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        public static long GetPosition(this StreamReader reader)
        {
            // shift position back from BaseStream.Position by the number of bytes read
            // into internal buffer.
            var byteLenObject = ByteLenField?.GetValue(reader);
            if (byteLenObject is not int byteLen)
            {
                return 0;
            }

            var position = reader.BaseStream.Position - byteLen;

            // if we have consumed chars from the buffer we need to calculate how many
            // bytes they represent in the current encoding and add that to the position.
            var charposObject = CharPosField?.GetValue(reader);
            if (charposObject is not int charPos)
            {
                return 0;
            }

            if (charPos <= 0)
            {
                return position;
            }

            var charobject = CharBufferField?.GetValue(reader);
            if (charobject is not char[] charBuffer)
            {
                return position;
            }

            var encoding = reader.CurrentEncoding;
            var bytesConsumed = encoding.GetBytes(charBuffer, 0, charPos).Length;
            position += bytesConsumed;

            return position;

        }

        public static void SetPosition(this StreamReader reader, long position)
        {
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(position, SeekOrigin.Begin);
        }
    }
}
