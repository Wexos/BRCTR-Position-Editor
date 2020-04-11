using System;
using System.Collections.Generic;
using System.IO;

namespace Nintendo.MKW.BRCTR
{
    public class BRCTR
    {
        public static Header _Header;
        public static SubHeader1 _SubHeader1;
        public static SubHeader2 _SubHeader2;
        public static List<Section1> _Section1;
        public static List<Section2> _Section2;
        public static List<Section3> _Section3;
        public static List<Section4> _Section4;
        public static List<Section5> _Section5;
        public static NameTable _NameTable;

        public BRCTR(byte[] Data)
        {
            InitializeNewLists();
            BigEndianReader Reader = new BigEndianReader(new MemoryStream(Data));
            _Header = new Header(Reader);
            Reader.BaseStream.Position = _Header.SubHeader1Offset;
            _SubHeader1 = new SubHeader1(Reader);
            Reader.BaseStream.Position = _SubHeader1.Section1Offset + _Header.SubHeader1Offset;
            for (int i = 0; i < _SubHeader1.NrSection1; i++)
            {
                _Section1.Add(new Section1(Reader));
            }
            Reader.BaseStream.Position = _SubHeader1.Section2Offset + _Header.SubHeader1Offset;
            for (int i = 0; i < _SubHeader1.NrSection2; i++)
            {
                _Section2.Add(new Section2(Reader));
            }
            Reader.BaseStream.Position = _Header.SubHeader2Offset;
            _SubHeader2 = new SubHeader2(Reader);
            Reader.BaseStream.Position = _SubHeader2.Section3Offset + _Header.SubHeader2Offset; ;
            for (int i = 0; i < _SubHeader2.NrSection3; i++)
            {
                _Section3.Add(new Section3(Reader));
            }
            Reader.BaseStream.Position = _SubHeader2.Section4Offset + _Header.SubHeader2Offset; ;
            for (int i = 0; i < _SubHeader2.NrSection4; i++)
            {
                _Section4.Add(new Section4(Reader));
            }
            Reader.BaseStream.Position = _SubHeader2.Section5Offset + _Header.SubHeader2Offset; ;
            for (int i = 0; i < _SubHeader2.NrSection5; i++)
            {
                _Section5.Add(new Section5(Reader));
            }
            Reader.BaseStream.Position = _Header.NameTableOffset;
            _NameTable = new NameTable(Reader);
        }
        public BRCTR()
        {

        }
        public byte[] Write()
        {
            MemoryStream m = new MemoryStream();
            BigEndianWriter Writer = new BigEndianWriter(m);
            _Header.Write(Writer);
            _SubHeader1.Write(Writer);
            for (int i = 0; i < _Section1.Count; i++)
            {
                _Section1[i].Write(Writer);
            }
            for (int i = 0; i < _Section2.Count; i++)
            {
                _Section2[i].Write(Writer);
            }
            _SubHeader2.Write(Writer);
            for (int i = 0; i < _Section3.Count; i++)
            {
                _Section3[i].Write(Writer);
            }
            for (int i = 0; i < _Section4.Count; i++)
            {
                _Section4[i].Write(Writer);
            }
            for (int i = 0; i < _Section5.Count; i++)
            {
                _Section5[i].Write(Writer);
            }
            _NameTable.Write(Writer);
            return m.ToArray();
        }
        public void InitializeNewLists()
        {
            _Section1 = new List<Section1>();
            _Section2 = new List<Section2>();
            _Section3 = new List<Section3>();
            _Section4 = new List<Section4>();
            _Section5 = new List<Section5>();
            if (_NameTable != null)
            {
                _NameTable.Names = new List<string>();
                _NameTable.Offsets = new List<long>();
            }
        }

        public class Header
        {
            public Header(BigEndianReader Reader)
            {
                Magic = Reader.ReadASCII(4); if (Magic != "bctr") { throw new WrongMagicException(Magic, "bctr", Reader.BaseStream.Position - 4); }
                NrSubHeader = Reader.ReadUInt16(); if (NrSubHeader != 2) { System.Windows.Forms.MessageBox.Show("Please give this file to Wexos. There is no problem with but he is interested in it (the importing will continue)"); }
                NrNameTable = Reader.ReadUInt16(); if (NrNameTable != 1) { System.Windows.Forms.MessageBox.Show("Please give this file to Wexos. There is no problem with but he is interested in it (the importing will continue)"); }
                Unknown1 = Reader.ReadUInt16();
                Unknown2 = Reader.ReadUInt16();
                SubHeader1Offset = Reader.ReadUInt16();
                SubHeader2Offset = Reader.ReadUInt16();
                NameTableOffset = Reader.ReadUInt16();
                Unknown3 = Reader.ReadUInt16();
            }
            public Header()
            {

            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteChars("bctr".ToCharArray(), 0, 4);
                Writer.WriteUInt16(NrSubHeader);
                Writer.WriteUInt16(NrNameTable);
                Writer.WriteUInt16(Unknown1);
                Writer.WriteUInt16(Unknown2);
                Writer.WriteUInt16(SubHeader1Offset);
                Writer.WriteUInt16(SubHeader2Offset);
                Writer.WriteUInt16(NameTableOffset);
                Writer.WriteUInt16(Unknown3);
            }

            public string Magic { get; set; }
            public UInt16 NrSubHeader { get; set; }
            public UInt16 NrNameTable { get; set; }
            public UInt16 Unknown1 { get; set; }
            public UInt16 Unknown2 { get; set; }
            public UInt16 SubHeader1Offset { get; set; }
            public UInt16 SubHeader2Offset { get; set; }
            public UInt16 NameTableOffset { get; set; }
            public UInt16 Unknown3 { get; set; }
        }
        public class SubHeader1
        {
            public SubHeader1(BigEndianReader Reader)
            {
                Section1Offset = Reader.ReadUInt16();
                NrSection1 = Reader.ReadUInt16();
                Section2Offset = Reader.ReadUInt16();
                NrSection2 = Reader.ReadUInt16();
            }
            public SubHeader1()
            {

            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteUInt16(Section1Offset);
                Writer.WriteUInt16(NrSection1);
                Writer.WriteUInt16(Section2Offset);
                Writer.WriteUInt16(NrSection2);
            }
            public UInt16 Section1Offset { get; set; }
            public UInt16 NrSection1 { get; set; }
            public UInt16 Section2Offset { get; set; }
            public UInt16 NrSection2 { get; set; }
        }
        public class Section1
        {
            public Section1(BigEndianReader Reader)
            {
                NameOffsets = Reader.ReadUInt16s(2);
                Section2ID = Reader.ReadUInt16();
                NrSection2 = Reader.ReadUInt16();
            }
            public Section1()
            {

            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteUInt16s(NameOffsets);
                Writer.WriteUInt16(Section2ID);
                Writer.WriteUInt16(NrSection2);
            }

            public UInt16[] NameOffsets { get; set; } //UInt16[2]
            public UInt16 Section2ID { get; set; }
            public UInt16 NrSection2 { get; set; }
        }
        public class Section2
        {
            public Section2(BigEndianReader Reader)
            {
                NameOffsets = Reader.ReadUInt16s(4);
                Unknown = Reader.ReadSingle();
            }
            public Section2()
            {

            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteUInt16s(NameOffsets);
                Writer.WriteSingle(Unknown);
            }

            public UInt16[] NameOffsets { get; set; } //UInt16[4]
            public float Unknown { get; set; }
        }
        public class SubHeader2
        {
            public SubHeader2(BigEndianReader Reader)
            {
                Section3Offset = Reader.ReadUInt16();
                NrSection3 = Reader.ReadUInt16();
                Section4Offset = Reader.ReadUInt16();
                NrSection4 = Reader.ReadUInt16();
                Section5Offset = Reader.ReadUInt16();
                NrSection5 = Reader.ReadUInt16();
            }
            public SubHeader2()
            {

            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteUInt16(Section3Offset);
                Writer.WriteUInt16(NrSection3);
                Writer.WriteUInt16(Section4Offset);
                Writer.WriteUInt16(NrSection4);
                Writer.WriteUInt16(Section5Offset);
                Writer.WriteUInt16(NrSection5);
            }

            public UInt16 Section3Offset { get; set; }
            public UInt16 NrSection3 { get; set; }
            public UInt16 Section4Offset { get; set; }
            public UInt16 NrSection4 { get; set; }
            public UInt16 Section5Offset { get; set; }
            public UInt16 NrSection5 { get; set; }
        }
        public class Section3
        {
            public Section3(BigEndianReader Reader)
            {
                NameOffset = Reader.ReadUInt16();
                Alpha = Reader.ReadUInt16();
                Animation = Reader.ReadUInt16();
                Padding = Reader.ReadUInt16();
                Delay = Reader.ReadSingle();
                TranslationWS = Reader.ReadSingles(3);
                ScaleWS = Reader.ReadSingles(2);
                Translation = Reader.ReadSingles(3);
                Scale = Reader.ReadSingles(2);
                Section4ID = Reader.ReadUInt16();
                NrSection4 = Reader.ReadUInt16();
                Section5ID = Reader.ReadUInt16();
                NrSection5 = Reader.ReadUInt16();
            }
            public Section3()
            {

            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteUInt16(NameOffset);
                Writer.WriteUInt16(Alpha);
                Writer.WriteUInt16(Animation);
                Writer.WriteUInt16(Padding);
                Writer.WriteSingle(Delay);
                Writer.WriteSingles(TranslationWS);
                Writer.WriteSingles(ScaleWS);
                Writer.WriteSingles(Translation);
                Writer.WriteSingles(Scale);
                Writer.WriteUInt16(Section4ID);
                Writer.WriteUInt16(NrSection4);
                Writer.WriteUInt16(Section5ID);
                Writer.WriteUInt16(NrSection5);
            }

            public UInt16 NameOffset { get; set; }
            public UInt16 Alpha { get; set; }
            public UInt16 Animation { get; set; }
            public UInt16 Padding { get; set; }
            public float Delay { get; set; }
            public float[] TranslationWS { get; set; } //float[3]
            public float[] ScaleWS { get; set; } //float[2]
            public float[] Translation { get; set; } //float[3]
            public float[] Scale { get; set; } //float[2]
            public UInt16 Section4ID { get; set; }
            public UInt16 NrSection4 { get; set; }
            public UInt16 Section5ID { get; set; }
            public UInt16 NrSection5 { get; set; }
        }
        public class Section4
        {
            public Section4(BigEndianReader Reader)
            {
                Unknown1 = Reader.ReadUInt16();
                Unknown2 = Reader.ReadUInt16();
                MessageID = Reader.ReadUInt32();
            }
            public Section4()
            {

            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteUInt16(Unknown1);
                Writer.WriteUInt16(Unknown2);
                Writer.WriteUInt32(MessageID);
            }

            public UInt16 Unknown1 { get; set; }
            public UInt16 Unknown2 { get; set; }
            public UInt32 MessageID { get; set; }
        }
        public class Section5
        {
            public Section5(BigEndianReader Reader)
            {
                NameOffsets = Reader.ReadUInt16s(2);
            }
            public Section5()
            {

            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteUInt16s(NameOffsets);
            }

            public UInt16[] NameOffsets { get; set; } //UInt16[2]
        }
        public class NameTable
        {
            public NameTable(BigEndianReader Reader)
            {
                Names = new List<string>();
                Offsets = new List<long>();
                Reader.BaseStream.Position++;
                while (Reader.BaseStream.Position < Reader.BaseStream.Length)
                {
                    List<byte> l = new List<byte>();
                    long o = Reader.BaseStream.Position - _Header.NameTableOffset;
                    do
                    {
                        l.Add(Reader.ReadByte());
                    }
                    while (l[l.Count - 1] != 0);
                    l.RemoveAt(l.Count - 1);
                    Names.Add(System.Text.Encoding.ASCII.GetString(l.ToArray()));
                    Offsets.Add(o);
                }
            }
            public NameTable()
            {
                Names = new List<string>();
            }
            public void Write(BigEndianWriter Writer)
            {
                Writer.WriteByte(0);
                for (int i = 0; i < Names.Count; i++)
                {
                    Writer.WriteChars(Names[i].ToCharArray(), 0, Names[i].ToCharArray().Length);
                    Writer.WriteByte(0);
                }
            }

            public List<string> Names { get; set; }
            public List<long> Offsets { get; set; }
        }
    }
}
