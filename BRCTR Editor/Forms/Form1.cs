using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Nintendo.MKW.BRCTR;

namespace BRCTR_Editor
{
    public partial class Form1 : Form
    {
        BRCTR BRCTR;
        string FilePath = "";
        bool saved = true;
        const string ProgramVersion = "v1.0.1.1";
        const string thisName = "BRCTR Position Editor | " + ProgramVersion + " | By Wexos";
        Graphics g;
        float[] trans;
        bool IsMoving = false;
        int Point = -1;
        public Form1()
        {
            InitializeComponent();
            BoolThings(false);
            this.Text = thisName;
            this.Size = new System.Drawing.Size(800, 500);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveToolStripMenuItem.Enabled == true)
            {
                if (saved == false)
                {
                    DialogResult d = MessageBox.Show("Do you wanna save before opening a new file?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    switch (d)
                    {
                        case DialogResult.Yes:
                            if (FilePath == "")
                            {
                                if (saveBRCTR.ShowDialog() == DialogResult.OK)
                                {
                                    WriteBRCTR(saveBRCTR.FileName);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                WriteBRCTR(FilePath);
                            }
                            break;
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
            }
            if (openBRCTR.ShowDialog() == DialogResult.OK)
            {
                PreRead(openBRCTR.FileName);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilePath == "")
            {
                if (saveBRCTR.ShowDialog() == DialogResult.OK)
                {
                    WriteBRCTR(saveBRCTR.FileName);
                }
            }
            else
            {
                WriteBRCTR(FilePath);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveBRCTR.ShowDialog() == DialogResult.OK)
            {
                WriteBRCTR(saveBRCTR.FileName);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is made by Wexos. It allows you to edit the positions in BRCTR files (only found in MKW so far). Thanks to Atlas/AtlasOmegaAlpha for the icon." + Environment.NewLine + "This is version " + ProgramVersion.Replace("v", "") + Environment.NewLine + Environment.NewLine + "© Wexos 2016", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://wiki.tockdom.com/wiki/BRCTR_Position_Editor");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saveToolStripMenuItem.Enabled == true)
            {
                if (saved == false)
                {
                    DialogResult d = MessageBox.Show("Do you wanna save before exiting?", "Save?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    switch (d)
                    {
                        case DialogResult.Yes:
                            if (FilePath == "")
                            {
                                if (saveBRCTR.ShowDialog() == DialogResult.OK)
                                {
                                    FilePath = saveBRCTR.FileName;
                                    WriteBRCTR(FilePath);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                WriteBRCTR(FilePath);
                            }
                            return;
                        case DialogResult.No:
                            return;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                        case DialogResult.None:
                            e.Cancel = true;
                            return;
                    }
                }
            }
        }

        public void PreRead(string FileName)
        {
            BoolThings(false);
            saved = true;
            Clear();
            try
            {
                ReadBRCTR(FileName);
            }
            catch (Exception Ex)
            {
                Clear();
                BoolThings(false);
                FilePath = "";
                this.Text = thisName;
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(Ex, true);
                throw new Exception(Ex.Message + Environment.NewLine + "Class Name: " + trace.GetFrame(0).GetMethod().ReflectedType.FullName + " Line: " + trace.GetFrame(0).GetFileLineNumber() + " Column: " + trace.GetFrame(0).GetFileColumnNumber());
            }
        }
        public void ReadBRCTR(string FileName)
        {
            Clear();
            BRCTR = new Nintendo.MKW.BRCTR.BRCTR(File.ReadAllBytes(FileName));
            for (int i = 0; i < BRCTR._Section3.Count; i++)
            {
                dgwSection3.Rows.Add(HexUtil.Hex8(i), "FFFF", BRCTR._Section3[i].TranslationWS[0], BRCTR._Section3[i].TranslationWS[1], BRCTR._Section3[i].TranslationWS[2], BRCTR._Section3[i].ScaleWS[0], BRCTR._Section3[i].ScaleWS[1], BRCTR._Section3[i].Translation[0], BRCTR._Section3[i].Translation[1], BRCTR._Section3[i].Translation[2], BRCTR._Section3[i].Scale[0], BRCTR._Section3[i].Scale[1]);
                for (ushort j = 0; j < BRCTR._NameTable.Names.Count; j++)
                {
                    if (BRCTR._Section3[i].NameOffset == 0)
                    {
                        break;
                    }
                    if (BRCTR._Section3[i].NameOffset == BRCTR._NameTable.Offsets[j])
                    {
                        dgwSection3.Rows[i].Cells[1].Value = BRCTR._NameTable.Names[j];
                        break;
                    }
                    else
                    {
                        if (j == BRCTR._NameTable.Names.Count - 1)
                        {
                            MessageBox.Show("The entry " + HexUtil.ConvertToHex(i) + " (Name 0) refers to a not existing name. It will be replaced with 0xFFFF");
                        }
                    }
                }
            }
            BoolThings(true);
            saved = true;
            FilePath = FileName;
        }
        private void WriteBRCTR(string FileName)
        {
            for (int i = 0; i < dgwSection3.Rows.Count; i++)
            {
                BRCTR._Section3[i].TranslationWS = new float[3] { Convert.ToSingle(dgwSection3.Rows[i].Cells[2].Value.ToString()), Convert.ToSingle(dgwSection3.Rows[i].Cells[3].Value.ToString()), Convert.ToSingle(dgwSection3.Rows[i].Cells[4].Value.ToString()) };
                BRCTR._Section3[i].ScaleWS = new float[2] { Convert.ToSingle(dgwSection3.Rows[i].Cells[5].Value.ToString()), Convert.ToSingle(dgwSection3.Rows[i].Cells[6].Value.ToString()) };
                BRCTR._Section3[i].Translation = new float[3] { Convert.ToSingle(dgwSection3.Rows[i].Cells[7].Value.ToString()), Convert.ToSingle(dgwSection3.Rows[i].Cells[8].Value.ToString()), Convert.ToSingle(dgwSection3.Rows[i].Cells[9].Value.ToString()) };
                BRCTR._Section3[i].ScaleWS = new float[2] { Convert.ToSingle(dgwSection3.Rows[i].Cells[10].Value.ToString()), Convert.ToSingle(dgwSection3.Rows[i].Cells[11].Value.ToString()) };
            }
            File.WriteAllBytes(FileName, BRCTR.Write());
            saved = true;
        }

        private void Clear()
        {
            dgwSection3.Rows.Clear();
            FilePath = "";
        }
        private void BoolThings(bool b)
        {
            dgwSection3.Enabled = b;
            saveToolStripMenuItem.Enabled = b;
            saveAsToolStripMenuItem.Enabled = b;
        }

        private void dgwSection3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            saved = false;
            if (dgwSection3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                dgwSection3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            }
            else
            {
                try
                {
                    float f = Convert.ToSingle(dgwSection3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                catch
                {
                    MessageBox.Show("Invalid value!", "Invalid value!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9)
                    {
                        dgwSection3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    }
                    else
                    {
                        dgwSection3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    }
                }
            }
        }
        private void dgw_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            saved = false;
            DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
            tb.KeyPress += new KeyPressEventHandler(dgw_KeyPress);
            e.Control.KeyPress += new KeyPressEventHandler(dgw_KeyPress);

        }
        private void dgw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(".") || e.KeyChar == Convert.ToChar(",") || e.KeyChar == Convert.ToChar("-"))
            {

            }
            else if (char.IsNumber(e.KeyChar))
            {

            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
            }
        }

        private void File_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (FileList != null)
            {
                PreRead(FileList[0]);
            }
        }
        private void File_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }        
    }
}