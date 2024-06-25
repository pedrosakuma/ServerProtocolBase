﻿
using ServerConsole.Core;
using System.Text;

namespace ServerConsole
{
    internal class EchoMessage : IMessage, IFormattableMessage
    {
        private readonly string text;

        public string Text => text;

        public int MessageType => 1;

        public EchoMessage(string text)
        {
            this.text = text;
        }

        public int FormatMessage(Span<byte> bytes)
        {
            return Encoding.UTF8.GetBytes(text, bytes);
        }
    }
}