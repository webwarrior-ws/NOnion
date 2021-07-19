﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using NOnion.Cells;
using NOnion.Utility;

namespace DotNetOnion.Cells
{
    public class CellRelayEncrypted : ICell
    {
        public byte[] EncryptedData { get; set; }

        public byte Command => 0x03;

        public void Deserialize(BinaryReader reader)
        {
            EncryptedData = reader.ReadBytes(Constants.FixedPayloadLength);
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(EncryptedData);
        }
    }

    public class CellRelayPlain
    {
        public RelayCommand RelayCommand { get; set; }
        public ushort Recognized { get; set; }
        public ushort StreamId { get; set; }
        public byte[] Digest { get; set; }
        public byte[] Data { get; set; }
        private byte[] padding;

        private void InitializePadding()
        {
            if (padding == null)
            {
                padding = new byte[Constants.FixedPayloadLength - 11 - Data.Length];
                RandomNumberGenerator.Create().GetNonZeroBytes(padding);
                Array.Clear(padding, 0, Math.Min(padding.Length, 4));
            }
        }

        public void FromBytes(byte[] bytes)
        {
            using MemoryStream memStream = new (bytes);
            using BinaryReader reader = new (memStream);
            RelayCommand = (RelayCommand)reader.ReadByte();
            Recognized = BinaryIO.ReadBigEndianUInt16(reader);
            StreamId = BinaryIO.ReadBigEndianUInt16(reader);
            Digest = reader.ReadBytes(4);
            Data = reader.ReadBytes(BinaryIO.ReadBigEndianUInt16(reader));
            padding = reader.ReadBytes(Constants.FixedPayloadLength - 11 - Data.Length);
        }

        public byte[] ToBytes(bool emptyDigest = false)
        {
            InitializePadding();
            using MemoryStream memStream = new(Constants.FixedPayloadLength);
            using BinaryWriter writer = new(memStream);
            writer.Write((byte)RelayCommand);
            BinaryIO.WriteUInt16BigEndian(writer, Recognized);
            BinaryIO.WriteUInt16BigEndian(writer, StreamId);
            writer.Write(emptyDigest ? new byte[4] : Digest);
            BinaryIO.WriteUInt16BigEndian(writer, (ushort)Data.Length);
            writer.Write(Data);
            writer.Write(padding);
            return memStream.ToArray();
        }
    }

    public enum RelayCommand
    {
        BEGIN = 1,
        DATA = 2,
        END = 3,
        CONNECTED = 4,
        SENDME = 5,
        EXTEND = 6,
        EXTENDED = 7,
        TRUNCATE = 8,
        TRUNCATED = 9,
        DROP = 10,
        RESOLVE = 11,
        RESOLVED = 12,
        BEGIN_DIR = 13,
        EXTEND2 = 14,
        EXTENDED2 = 15,
    }
}
