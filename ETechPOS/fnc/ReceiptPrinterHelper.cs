using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Data;
using Cobainsoft.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using ETech.cls;

namespace ETech.fnc
{
    public class ReceiptPrinterHelper
    {
        /// <summary>
        /// Get or sets the maximum possible number of characters that can be printed per line.
        /// </summary>
        public int StringFullWidth { get; set; }
        /// <summary>
        /// Gets or sets the number of characters that will actually be printed per line.
        /// </summary>
        public int StringWidth { get; set; }
        /// <summary>
        /// Gets or sets the number of whitespaces to be inserted in front of every line.
        /// </summary>
        public int StringBufferWidth { get; set; }
        public string PrinterName { get; set; }

        public ReceiptPrinterHelper(int stringWidth)
            : this(stringWidth, new PrintDocument().PrinterSettings.PrinterName) { }
        public ReceiptPrinterHelper(int stringWidth, string printerName)
        {
            this.StringWidth = stringWidth;
            this.StringFullWidth = this.StringWidth;
            this.PrinterName = printerName;
            this.buffer = new byte[] { };
            this.setSpacingForText();
            this.NormalFont();
            this.CPI12();
        }

        public void ActivateCutter()
        {
            RawPrinterHelper.SendStringToPrinter(this.PrinterName, "\n\r");
            byte[] bytes = new byte[] { 0x1D, 0x56, 0x42, 0x00 };
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, unmanagedPointer, bytes.Length);
            RawPrinterHelper.SendBytesToPrinter(this.PrinterName, unmanagedPointer, bytes.Length);
            Marshal.FreeHGlobal(unmanagedPointer);
        }
        public void CPI12()
        {
            addBytesToBuffer(new byte[] { 0x1B, 0x4D });
            // Dont know why this seems necessary:
            addStringToBuffer("1");
        }
        public string GetRepeatingCharacter(char character, int numberOfRepeats)
        {
            string result = "";
            for (int i = 0; i < numberOfRepeats; i++)
                result += character;
            return result;
        }
        public void LargeFont()
        {
            byte[] bytes = new byte[] { 0x1B, 0x21, 0x10 };
            addBytesToBuffer(bytes);
        }
        public void NormalFont()
        {
            byte[] bytes = new byte[] { 0x1B, 0x21, 0x00 };
            addBytesToBuffer(bytes);
        }
        public void OpenCashDrawer()
        {
            byte[] DrawerOpen = cls_globalvariables.OpenDrawerBytes;
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(DrawerOpen.Length);
            Marshal.Copy(DrawerOpen, 0, unmanagedPointer, DrawerOpen.Length);
            RawPrinterHelper.SendBytesToPrinter(PrinterName, unmanagedPointer, DrawerOpen.Length);
            Marshal.FreeHGlobal(unmanagedPointer);
        }

        public void WriteBarcode(string barcode)
        {
            WriteBarcode(barcode, BarcodeType.CODE128B);
        }
        public void WriteBarcode(string barcode, BarcodeType barcodeType)
        {
            BarcodeControl BCctrl = new BarcodeControl();
            BCctrl.TextPosition = BarcodeTextPosition.NotShown;
            BCctrl.HorizontalAlignment = BarcodeHorizontalAlignment.Center;
            BCctrl.Location = new Point(0, 0);
            BCctrl.BarcodeType = barcodeType;
            BCctrl.CopyRight = "";
            BCctrl.Size = new Size(200, 40);
            BCctrl.Data = barcode;
            Rectangle rect = new Rectangle(0, 0, BCctrl.Width, BCctrl.Height);
            Bitmap bitmap = new Bitmap(BCctrl.Width, BCctrl.Height);

            BCctrl.DrawToBitmap(bitmap, rect);
            WriteImage(bitmap);
        }
        public void WriteImage(Image image)
        {
            if (image.PixelFormat != PixelFormat.Format32bppArgb)
                throw new Exception("Pixel Format not yet supported.");

            Image tempImage = image;
            float scale = tempImage.Height / tempImage.Width;
            if (tempImage.Width > 255)
                tempImage = new Bitmap(image, new Size(255, (int)(255 / scale)));
            if (tempImage.Height > 255)
                tempImage = new Bitmap(image, new Size((int)(scale / 255), 255));
            image = tempImage;

            Bitmap bmp = new Bitmap(image);
            Rectangle rect = new Rectangle(0, 0, image.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            List<int[]> tempList = new List<int[]>();
            List<int> tempList2 = new List<int>();
            int nonzero = 0;
            for (int i = 0; i < rgbValues.Length; )
            {
                int byte1 = (((int)rgbValues[i]) << 24);
                int byte2 = (((int)rgbValues[i + 1]) << 16);
                int byte3 = (((int)rgbValues[i + 1]) << 8);
                int byte4 = (((int)rgbValues[i + 1]));
                int finalInt = byte1 | byte2 | byte3 | byte4;
                if (finalInt != -1)
                    nonzero++;
                tempList2.Add(finalInt);
                if (((i += 4) % bmpData.Stride) == 0)
                {
                    tempList.Add(tempList2.ToArray());
                    tempList2 = new List<int>();
                }
            }
            int[][] thefinalarray = tempList.ToArray();

            int[][] anotherarray = (int[][])thefinalarray.Clone();
            for (int i = 0; i < anotherarray.Length; i++)
            {
                for (int j = 0; j < anotherarray[i].Length; j++)
                {
                    if (anotherarray[i][j] != -1)
                        anotherarray[i][j] = 1;
                    else
                        anotherarray[i][j] = 0;
                }
            }
            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            betterPrintImage(anotherarray);
        }
        public void WriteLines(string text)
        {
            WriteLines(text, StringAlignment.Center);
        }
        public void WriteLines(string text, StringAlignment alignment)
        {
            text = text.Replace('\t', ' ');
            text = getFormattedString(text, alignment, this.StringWidth);
            List<string> tempStringList = text.Split('\n', '\r').ToList();
            tempStringList.RemoveAll(x => x == "");
            if (tempStringList.Count > 0)
            {
                tempStringList.ForEach(x => this.addStringToBuffer(GetRepeatingCharacter(' ', this.StringBufferWidth) + x
                    + ((x.Length + this.StringBufferWidth < this.StringFullWidth) ? "\n\r" : "")));
            }
        }
        public void WriteRow(string[] strings, StringAlignment[] alignments, int[] columnWidths)
        {
            if (strings.Length != alignments.Length || strings.Length != columnWidths.Length)
                throw new Exception("All arrays must have the same lengths.");

            columnWidths[columnWidths.Length - 1] += this.StringWidth - columnWidths.Sum();
            List<List<string>> stringListToPrint = new List<List<string>>();
            for (int i = 0; i < strings.Length; i++)
            {
                string temptmeptmep = getFormattedString(strings[i], alignments[i], columnWidths[i]);
                List<string> tempStringList = temptmeptmep.Replace("\n\r", "\0").Split('\0').ToList();
                tempStringList.RemoveAll(x => x == "");
                stringListToPrint.Add(tempStringList);
            }
            int numberOfLines = stringListToPrint.Max(x => x.Count);
            for (int i = 0; i < numberOfLines; i++)
            {
                string line = "";
                for (int j = 0; j < stringListToPrint.Count; j++)
                {

                    if (stringListToPrint[j].Count > i)
                        line += stringListToPrint[j][i];
                    else
                        line += GetRepeatingCharacter(' ', columnWidths[j]);
                }
                WriteLines(line);
            }
        }
        public void WriteRepeatingCharacterLine(char character)
        {
            WriteRepeatingCharacterLine(character, this.StringWidth, StringAlignment.Near);
        }
        public void WriteRepeatingCharacterLine(char character, int numberOfRepeats, StringAlignment alignment)
        {
            WriteLines(GetRepeatingCharacter(character, numberOfRepeats), alignment);
        }
        public void WriteTable(string[][] strings, StringAlignment[] alignments, int[] columnWidths)
        {
            for (int i = 0; i < strings.Length; i++)
            {
                WriteRow(strings[i], alignments, columnWidths);
            }
        }
        public void WriteTable(DataTable data, StringAlignment[] alignments, int[] columnWidths)
        {
            foreach (DataRow row in data.Rows)
            {
                List<string> stringList = new List<string>();
                foreach (object obj in row.ItemArray)
                    stringList.Add(obj.ToString());
                WriteRow(stringList.ToArray(), alignments, columnWidths);
            }
        }
        public void Print()
        {
            if (this.PrinterName == "Microsoft XPS Document Writer")
            {
                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings.PrinterName = this.PrinterName;
                doc.PrintPage += (sender, e) =>
                {
                    int y = 0;
                    SolidBrush solidBrush = new SolidBrush(Color.Black);
                    Font font = new Font(FontFamily.GenericMonospace, 12f);
                    int yMultiplier = (int)e.Graphics.MeasureString("Og", font).Height;
                    string text = "";
                    List<byte> byteListTemp = new List<byte>();
                    foreach (byte cmd in this.buffer)
                    {
                        // Command bytes
                        if (cmd == 0x1B || byteListTemp.Count > 0)
                        {
                            byteListTemp.Add(cmd);
                            byte[] array = byteListTemp.ToArray();

                            // CPI12()
                            if (array.Count() == 3 && array[0] == 0x1B && array[1] == 0x4D)
                                byteListTemp.Clear();
                            // NormalFont(), LargeFont()
                            else if (array.Count() == 3 && array[0] == 0x1B && array[1] == 0x21)
                                byteListTemp.Clear();
                            else if (array.Count() == 3 && array[0] == 0x1B && array[1] == 0x33)
                                byteListTemp.Clear();
                        }
                        // Text
                        else
                        {
                            text += (char)cmd;
                            if (text.Contains("\n\r") || text.Length == this.StringFullWidth)
                            {
                                e.Graphics.DrawString(text, font, solidBrush, new PointF(0, y * yMultiplier));
                                y++;
                                text = "";
                            }
                        }
                    }
                };
                try
                {
                    doc.Print();
                }
                catch (InvalidPrinterException e)
                {

                }
                return;
            }
            IntPtr unmanagedPointer = Marshal.AllocHGlobal(buffer.Length);
            Marshal.Copy(buffer, 0, unmanagedPointer, buffer.Length);
            RawPrinterHelper.SendBytesToPrinter(this.PrinterName, unmanagedPointer, buffer.Length);
            Marshal.FreeHGlobal(unmanagedPointer);
            buffer = new byte[] { };
        }

        private void betterPrintImage(int[][] pixels)
        {
            int height = pixels.Length;
            int width = pixels[0].Length;
            setSpacingForImage();
            byte[] lengthByte = new byte[] { 0x1B, 0x2A, 0x00, (byte)(0x00FF & width), (byte)((0xFF00 & width) >> 8) };

            for (int y = 0; y < height; y += 8)
            {
                // Select image mode. (Needs to be done per line)
                addBytesToBuffer(new byte[] { 0x1B, 0x2A, 33 });
                // Send nL and nH
                addBytesToBuffer(lengthByte);

                // Process one block of 8x8
                for (int x = 0; x < width; x += 8)
                    addBytesToBuffer(process8x8(pixels, x, y));

                addStringToBuffer("\n\r");
            }
            setSpacingForText();
        }
        private byte[] process8x8(int[][] pixels, int x, int y)
        {
            int yEnd = Math.Min(pixels.Length, y + 8);
            int xEnd = Math.Min(pixels[0].Length, x + 8);
            List<byte> result = new List<byte>();

            for (int j = x; j < xEnd; j++)
            {
                byte temp = 0;
                for (int i = y; i < yEnd; i++)
                {
                    temp <<= 1;
                    if (pixels[i][j] != 0)
                        temp |= 0x01;
                }
                result.Add(temp);
            }
            return result.ToArray();
        }

        private void setSpacingForText()
        {
            // Special case where spacing becomes very small for EPSON TM-T82 (Thermal Printer)
            if (StringFullWidth == 64)
                addBytesToBuffer(new byte[] { 0x1B, 0x33, 50 });
            else
                addBytesToBuffer(new byte[] { 0x1B, 0x33, 20 });
        }
        private void setSpacingForImage()
        {
            addBytesToBuffer(new byte[] { 0x1B, 0x33, 8 });
        }
        private string getFormattedString(string text, StringAlignment alignment, int maxLineLength)
        {
            AlignmentMethod alignmentMethod = leftAlignString;
            switch (alignment)
            {
                case StringAlignment.Center:
                    alignmentMethod = centerAlignString;
                    break;
                case StringAlignment.Far:
                    alignmentMethod = rightAlignString;
                    break;
                case StringAlignment.Near:
                    alignmentMethod = leftAlignString;
                    break;
            }
            if (text.Length > maxLineLength)
            {
                string tempString1 = "";
                string tempString2 = "";
                string[] stringArray = text.Split(' ');
                text = "";
                for (int i = 0; i < stringArray.Length; i++)
                {
                    string textIterator = stringArray[i];
                    tempString1 += ' ' + textIterator;
                    if (textIterator.Length > maxLineLength)
                    {
                        int truncateIndex = maxLineLength - tempString2.Length - 1;
                        string partsOfIterator = textIterator.Substring(0, truncateIndex);

                        if (tempString2 != "")
                            tempString2 = tempString2.Substring(1, tempString2.Length - 1);
                        text += alignmentMethod(tempString2 + ' ' + partsOfIterator, maxLineLength) + "\n\r";

                        for (; truncateIndex < textIterator.Length; truncateIndex += maxLineLength)
                        {
                            if (truncateIndex + maxLineLength <= textIterator.Length)
                                text += textIterator.Substring(truncateIndex, maxLineLength) + "\n\r";
                            else
                                text += textIterator.Substring(truncateIndex, textIterator.Length - truncateIndex);
                        }
                        tempString1 = "";
                        tempString2 = "";
                    }
                    // Minus 1 is for the extra leading space
                    else if (tempString1.Length - 1 > maxLineLength)
                    {
                        if (tempString2 != "")
                            tempString2 = tempString2.Substring(1, tempString2.Length - 1);
                        text += alignmentMethod(tempString2, maxLineLength) + "\n\r";
                        tempString1 = ' ' + textIterator;
                        tempString2 = tempString1;
                    }
                    else if (i != stringArray.Length - 1)
                    {
                        tempString2 = tempString1;
                    }
                    else
                    {
                        if (tempString1 != "")
                            text += alignmentMethod(tempString1.Substring(1, tempString1.Length - 1), maxLineLength);
                        tempString1 = "";
                        tempString2 = "";
                    }
                }
                if (tempString1 != "")
                    text += alignmentMethod(tempString1.Substring(1, tempString1.Length - 1), maxLineLength);
            }
            else
            {
                text = alignmentMethod(text, maxLineLength);
            }
            List<string> padderListString = text.Split('\n', '\r').ToList();
            padderListString.RemoveAll(x => x == "");
            text = "";
            for (int i = 0; i < padderListString.Count; i++)
            {
                string tempText = padderListString[i];
                int length = maxLineLength - tempText.Length;
                for (int j = 0; j < length; j++)
                    tempText += " ";
                text += tempText;
                if (i != padderListString.Count - 1)
                    text += "\n\r";
            }

            return text;
        }
        private string centerAlignString(string text)
        {
            return centerAlignString(text, this.StringWidth);
        }
        private string centerAlignString(string text, int maxLength)
        {
            int spaceleft = maxLength - text.Length;
            int leftindent = spaceleft / 2;
            int totallength = leftindent + text.Length;
            return String.Format("{0, " + totallength + "}", text);
        }
        private string rightAlignString(string text)
        {
            return rightAlignString(text, this.StringWidth);
        }
        private string rightAlignString(string text, int maxLength)
        {
            return String.Format("{0, " + (maxLength) + "}", text);
        }
        private string leftAlignString(string text)
        {
            return leftAlignString(text, this.StringWidth);
        }
        private string leftAlignString(string text, int maxLength)
        {
            return text;
        }

        private void addBytesToBuffer(byte[] bytes)
        {
            List<byte> tempBuffer = this.buffer.ToList();
            foreach (byte x in bytes)
                tempBuffer.Add(x);
            this.buffer = tempBuffer.ToArray();
        }
        private void addStringToBuffer(string text)
        {
            List<byte> tempBuffer = this.buffer.ToList();
            foreach (char x in text)
                tempBuffer.Add((byte)x);
            this.buffer = tempBuffer.ToArray();
        }

        private delegate string AlignmentMethod(string text, int maxLength);

        private byte[] buffer;
    }
}
