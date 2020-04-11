using System;
using System.Collections.Generic;

namespace System.IO
{
    public class BigEndianWriter : BinaryWriter //By Wexos
    {
        public BigEndianWriter(Stream stream) : base(stream) { }
        public void WriteBoolean(Boolean Value)
        {
            base.Write(Value);
        }
        public void WriteByte(byte Value)
        {
            base.Write(Value);
        }
        public void WriteBytes(byte[] Value)
        {
            foreach (byte b in Value)
            {
                base.Write(b);
            }
        }
        public void WriteBytes(List<byte> Value)
        {
            foreach (byte b in Value)
            {
                base.Write(b);
            }
        }
        public void WriteSByte(sbyte Value)
        {
            base.Write(Value);
        }
        public void WriteSBytes(sbyte[] Value)
        {
            foreach (sbyte b in Value)
            {
                base.Write(b);
            }
        }
        public void WriteSBytes(List<sbyte> Value)
        {
            foreach (sbyte b in Value)
            {
                base.Write(b);
            }
        }
        public void WriteUInt16(UInt16 Value)
        {
            byte[] Value2 = BitConverter.GetBytes(Value);
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteUInt16s(UInt16[] Value)
        {
            foreach (UInt16 UInt in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(UInt);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteUInt16s(List<UInt16> Value)
        {
            foreach (UInt16 UInt in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(UInt);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteInt16(Int16 Value)
        {
            byte[] Value2 = BitConverter.GetBytes(Value);
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteInt16s(Int16[] Value)
        {
            foreach (Int16 Int in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(Int);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteInt16s(List<Int16> Value)
        {
            foreach (Int16 Int in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(Int);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteUInt32(UInt32 Value)
        {
            byte[] Value2 = BitConverter.GetBytes(Value);
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteUInt32s(UInt32[] Value)
        {
            foreach (UInt32 UInt in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(UInt);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteUInt32s(List<UInt32> Value)
        {
            foreach (UInt32 UInt in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(UInt);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteInt32(Int32 Value)
        {
            byte[] Value2 = BitConverter.GetBytes(Value);
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteInt32s(Int32[] Value)
        {
            foreach (Int32 Int in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(Int);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteInt32s(List<Int32> Value)
        {
            foreach (Int32 Int in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(Int);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteUInt64(UInt64 Value)
        {
            byte[] Value2 = BitConverter.GetBytes(Value);
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteUInt64s(UInt64[] Value)
        {
            foreach (UInt64 UInt in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(UInt);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteUInt64s(List<UInt64> Value)
        {
            foreach (UInt64 UInt in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(UInt);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteInt64(Int64 Value)
        {
            byte[] Value2 = BitConverter.GetBytes(Value);
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteInt64s(Int64[] Value)
        {
            foreach (Int64 Int in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(Int);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteInt64s(List<Int64> Value)
        {
            foreach (Int64 Int in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(Int);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteSingle(float Value)
        {
            byte[] Value2 = BitConverter.GetBytes(Value);
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteSingles(float[] Value)
        {
            foreach (float fl in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(fl);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteSingles(List<float> Value)
        {
            foreach (float fl in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(fl);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteDecimal(decimal Value)
        {
            throw new NotImplementedException();
            byte[] Value2 = BitConverter.GetBytes(Convert.ToDouble(Value));
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteDecimals(decimal[] Value)
        {
            throw new NotImplementedException();
            foreach (decimal de in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(Convert.ToInt64(de));
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteDecimals(List<decimal> Value)
        {
            throw new NotImplementedException();
            foreach (decimal de in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(Convert.ToInt64(de));
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteString(string Value)
        {
            base.Write(Value);
        }
        public void WriteChar(char Value)
        {
            byte[] Value2 = BitConverter.GetBytes(Value);
            Array.Reverse(Value2);
            base.Write(Value2);
        }
        public void WriteChars(char[] Value)
        {
            foreach (char ch in Value)
            {
                byte[] Value2 = BitConverter.GetBytes(ch);
                Array.Reverse(Value2);
                base.Write(Value2);
            }
        }
        public void WriteChars(char[] Value, int index, int count)
        {
            base.Write(Value, index, count);
        }
        public void WriteChars(List<char> Value, int index, int count)
        {
            base.Write(Value.ToArray(), index, count);
        }
    }
}
