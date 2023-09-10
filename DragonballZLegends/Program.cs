using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace DragonballZLegends
{
    class Program
    {
        static void Main(string[] args)
        {
            Edit1();
            EditATB();
            EditBandai();
            EditEnd();
            EditInf();
            EditOrv();
            EditSelect();
            EditTitle();

            EditWar();

        }

        static void Edit1()
        {
            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "1"));
            ApplyLegends(bin, 0x1B1C0); //Done
        }
        static void EditATB()
        {
            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "ATB", "AP_ATB.BIN"));

        }
        static void EditBandai()
        {
            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Bandai", "AP_BND.BIN"));
            ApplyLegends(bin, 0x1F228); //Done
        }
        static void EditEnd()
        {
            var images = ExtractImage("End", "GD_END.BIN");
            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "End", "AP_END.BIN"));
            ApplyLegends(bin, 0x1C460);
            var palettes = new List<List<Color>>();

            for (var p = 0; p < 0x2E00; p += 0x200)
            {
                palettes.Add(ExtractPalette(bin.Skip(0x1d218 + p).Take(0x200).ToArray()));
                System.Diagnostics.Debug.WriteLine($"{0x1d218 + p:X5}");
            }


            palettes.Add(ExtractPalette(bin.Skip(0x1d218 + 0x2E00).Take(0x20).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x1d218 + 0x2E00:X5}");
            palettes.Add(ExtractPalette(bin.Skip(0x1d218 + 0x2E20).Take(0x200).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x1d218 + 0x2E20:X5}");
            palettes.Add(ExtractPalette(bin.Skip(0x1d218 + 0x3020).Take(0x200).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x1d218 + 0x3020:X5}");

            

            var i = 0;
            foreach (var image in images)
            {
               
                var palette = (i < 23) ? palettes[i] : palettes[23];
                palette = (i == 215) ? palettes[24] : (i == 216) ? palettes[24] : (i == 217) ? palettes[25] : palette;
                var sbmp = image.Is16Color ? ToBitmap16(image.Width, image.Height, image.Data, palette) : ToBitmap(image.Width, image.Height, image.Data, palette);
                sbmp.Save(Path.Combine(Directory.GetCurrentDirectory(), "End", $"GD_END_{i++}.png"), ImageFormat.Png);

            }
        }
        static void EditInf()
        {
            var images = ExtractImage("Inf", "GD_INF.BIN");

            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Inf", "AP_INF.BIN"));
            bin[0X399F] = 0x05;//Why?
            ApplyLegends(bin, 0x23DB4); //Done

            var palettes = new List<List<Color>>();
            var p = 0;
            var o = p + 0x200 * 44;
            for (p = 0; p < o; p += 0x200)
            {
                System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
                palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x200).ToArray()));
            }
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x20).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x20;
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x200).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x200;
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x20).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x20;
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x200).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x200;
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x200).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x200;
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x20).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x20;
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x20).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x20;
            o = p+ 0x200 * 25;
            for (; p <o ; p += 0x200)
            {
                System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
                palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x200).ToArray()));
            }
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x20).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x20;
            o = p + 0x200 * 3;
            for (; p < o; p += 0x200)
            {
                System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
                palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x200).ToArray()));
            }
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x20).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x20;
            palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x200).ToArray()));
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
            p += 0x200;
            o = p + 0x20 * 36;
            for (; p < o; p += 0x20)
            {
                System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");
                palettes.Add(ExtractPalette(bin.Skip(0x24D18 + p).Take(0x20).ToArray()));
            }
            System.Diagnostics.Debug.WriteLine($"{0x24D18 + p:X5}");


            var i = 0;
            foreach (var image in images)
            {
                var palette = palettes[0];
                if (i < 43) palette = palettes[i];  //Portraits
                else if (i < 63) palette = palettes[43]; //Text
                //else if (i == 63) palette = palettes[44];
                else if (i < 72) palette = palettes[80];
                else if (i < 96) palette = palettes[(i - 72) + 52 ];
                //else if (i < 106) palette = palettes[76];
               // else if (i < 117) palette = palettes[50];
               // else if (i < 125) palette = palettes[81];
               // else if (i < 130) palette = palettes[76];
                else if (i == 130) palette = palettes[77];
                else if (i == 131) palette = palettes[78];
                else if (i == 132) palette = palettes[78];
                else if (i == 133) palette = palettes[79];
               // else if (i < 130) palette = palettes[80];
                else if(i < 167) palette = palettes[81];
                else if (i < 169) palette = palettes[82]; //Goku
                else if (i < 171) palette = palettes[83]; //Goku
                else if (i < 173) palette = palettes[84]; //Goku
                else if (i < 175) palette = palettes[85]; //Gohan
                else if (i < 177) palette = palettes[86]; //Gohan
                else if (i < 179) palette = palettes[87]; //Gohan
                else if (i < 181) palette = palettes[88]; //Gohan
                else if (i < 183) palette = palettes[89]; //Gohan
                else if (i < 185) palette = palettes[90]; //Picolo
                else if (i < 187) palette = palettes[91]; //Krilin
                else if (i < 189) palette = palettes[92]; //Trunks
                else if (i < 191) palette = palettes[93]; //Trunks
                else if (i < 193) palette = palettes[94]; //Trunks
                else if (i < 195) palette = palettes[95]; //Vegeta
                else if (i < 197) palette = palettes[96]; //Vegeta
                else if (i < 199) palette = palettes[97]; //Vegeta

                else if (i < 201) palette = palettes[98]; //Nappa
                else if (i < 203) palette = palettes[99]; //Ginyu 1
                else if (i < 205) palette = palettes[100]; //Ginyu 2
                else if (i < 207) palette = palettes[101]; //Ginyu 3
                else if (i < 209) palette = palettes[102]; //Ginyu 4
                else if (i < 211) palette = palettes[103]; //Ginyu 5
                else if (i < 213) palette = palettes[104]; //Freeza
                else if (i < 215) palette = palettes[105]; //Android 16
                else if (i < 217) palette = palettes[106]; //Android 17
                else if (i < 219) palette = palettes[107]; //Android 18
                else if (i < 221) palette = palettes[108]; //??? 
                else if (i < 223) palette = palettes[109]; //???
                else if (i < 225) palette = palettes[110]; //Cell
                else if (i < 227) palette = palettes[111]; //Cell minion
                else if (i < 229) palette = palettes[112]; //???
                else if (i < 231) palette = palettes[113]; //Buu 1
                else if (i < 233) palette = palettes[114]; //Buu 2
                else if (i < 235) palette = palettes[115]; //Buu 3
                else if (i < 237) palette = palettes[116]; //Goten 
                else if (i < 241) palette = palettes[117]; //Goten
               // else if (i < 251) palette = palettes[80]; //Goten 
                else if (i < 254) palette = palettes[80]; //Goten 

                var col = image.Is16Color ? 16 : 256;



                var sbmp = image.Is16Color ? ToBitmap16(image.Width, image.Height, image.Data, palette) : ToBitmap(image.Width, image.Height, image.Data, palette);
                sbmp.Save(Path.Combine(Directory.GetCurrentDirectory(), "Inf", $"GD_INF_{i++}_{col}.png"), ImageFormat.Png);

            }


        }
        static void EditOrv()
        {
            var images = ExtractImage("Orv", "GD_ORV.BIN");
            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "1"));

            var palettes = new List<List<Color>>();

            for (var p = 0; p < 0x400; p += 0x200)
            {
                palettes.Add(ExtractPalette(bin.Skip(0x0 + p).Take(0x200).ToArray()));
            }




            var i = 0;
            foreach (var image in images)
            {
                var palette = (i < 1) ? palettes[i] : palettes[1];
                var col = image.Is16Color ? 16 : 256;
                var sbmp = image.Is16Color ? ToBitmap16(image.Width, image.Height, image.Data, palette) : ToBitmap(image.Width, image.Height, image.Data, palette);
                  sbmp.Save(Path.Combine(Directory.GetCurrentDirectory(), "Orv", $"GD_ORV_{col}_{i++}.png"), ImageFormat.Png);
            }
        }
        static void EditSelect()
        {
            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Select", "AP_SEL.BIN"));
            ApplyLegends(bin, 0x24d10);
            var palettes = new List<List<Color>>();

            for (var p = 0; p < 0x5000; p += 0x200)
            {
                palettes.Add(ExtractPalette(bin.Skip(0x27ea4 + p).Take(0x200).ToArray()));
            }

            var images = ExtractImage("Select", "GD_SEL.BIN");
            var i = 0;
            var bigImage = images.First();
            var bmp = ToBitmap(bigImage.Width, bigImage.Height, bigImage.Data, palettes[i++]);
            bmp.Save(Path.Combine(Directory.GetCurrentDirectory(), "Select", $"GD_SEL_{i++}.png"), ImageFormat.Png);

            images.Remove(images.First());
            foreach (var image in images)
            {
                var palette = (i < 40) ? palettes[i] : palettes[39];

                var sbmp = image.Is16Color ? ToBitmap16(image.Width, image.Height, image.Data, palette) : ToBitmap(image.Width, image.Height, image.Data, palette);
                sbmp.Save(Path.Combine(Directory.GetCurrentDirectory(), "Select", $"GD_SEL_{i++}.png"), ImageFormat.Png);

            }
        }
        static void EditTitle()
        {
            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Title", "AP_TLP.BIN"));
            ApplyLegends(bin, 0x1F97C);

            var images = ExtractImage("Title", "GD_TLP.BIN");

            var palettes = new List<List<Color>> {
                ExtractPalette(bin.Skip(0x201e4).Take(0x200).ToArray()),
                ExtractPalette(bin.Skip(0x1FFE4).Take(0x200).ToArray())
            };


            var i = 0;
            foreach (var image in images)
            {
                var palette = (i < 2) ? palettes[i] : palettes[1];

                var sbmp = image.Is16Color ? ToBitmap16(image.Width, image.Height, image.Data, palette) : ToBitmap(image.Width, image.Height, image.Data, palette);
                sbmp.Save(Path.Combine(Directory.GetCurrentDirectory(), "Title", $"GD_TLP_{i++}.png"), ImageFormat.Png);

            }
        }
        static void EditWar()
        {
            var images = ExtractImage("War", "GD_WAR.BIN");
            var bin = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "1"));

            var palettes = new List<List<Color>> {
                ExtractPalette(bin.Skip(0x1BDF4).Take(0x200).ToArray()),
                ExtractPalette(bin.Skip(0x1BFF4).Take(0x200).ToArray()),
                ExtractPalette(bin.Skip(0x1C1F4).Take(0x200).ToArray())
            };


            var i = 0;
            foreach (var image in images)
            {
                var palette = (i < 3) ? palettes[i] : palettes[2];

                var sbmp = image.Is16Color ? ToBitmap16(image.Width, image.Height, image.Data, palette) : ToBitmap(image.Width, image.Height, image.Data, palette);
                sbmp.Save(Path.Combine(Directory.GetCurrentDirectory(), "War", $"GD_WAR_{i++}.png"), ImageFormat.Png);

            }


        }


        static void ApplyLegends(byte[] data, int address)
        {
            var legends = Encoding.ASCII.GetBytes("LEGENDS   ");
            Array.Copy(legends, 0, data, address, legends.Length);
        }
        static Bitmap ToBitmap(int width, int height, byte[] data, List<Color> palette)
        {
            using var bmp = new Bitmap(width, height);

            var x = 0;
            var y = 0;
            foreach (var p in data)
            {
                bmp.SetPixel(x, y, palette[p]);


                x++;
                if (x < width) continue;
                y++;
                x = 0;

            }
            return new Bitmap(bmp);
        }
        static Bitmap ToBitmap16(int width, int height, byte[] data, List<Color> palette)
        {

            using var bmp = new Bitmap(width, height);

            var x = 0;
            var y = 0;
            foreach (var p in data)
            {
                bmp.SetPixel(x++, y, palette[p >> 4]);
                bmp.SetPixel(x++, y, palette[p & 0xF]);

                if (x < width) continue;
                y++;
                x = 0;

            }
            return new Bitmap(bmp);
        }
        static List<Color> ExtractPalette(byte[] paletteBytes)
        {

            var palette = new List<System.Drawing.Color>();
            //var palette16 = new List<ushort>();
            for (var pl = 0; pl < paletteBytes.Length; pl += 2)
            {
                var p = (ushort)(paletteBytes[pl] << 8 | paletteBytes[pl + 1]);
                //palette16.Add(p);
                var r = ((p >> 10) & 0x1F) << 3;
                var g = ((p >> 5) & 0x1F) << 3;
                var b = ((p >> 0) & 0x1F) << 3;
                palette.Add(System.Drawing.Color.FromArgb(255, b, g, r));
            }
            palette[0] = Color.Transparent;
            return palette;
        }
        //static List<Color> ExtractPalette16(byte[] paletteBytes)
        //{

        //    var palette = new List<System.Drawing.Color>();
        //    //var palette16 = new List<ushort>();
        //    for (var pl = 0; pl < paletteBytes.Length; pl += 2)
        //    {
        //        var p = (ushort)(paletteBytes[pl] << 8 | paletteBytes[pl + 1]);
        //        //palette16.Add(p);
        //        var r = ((p >> 10) & 0x1F) << 3;
        //        var g = ((p >> 5) & 0x1F) << 3;
        //        var b = ((p >> 0) & 0x1F) << 3;
        //        palette.Add(System.Drawing.Color.FromArgb(255, r, g, b));
        //    }
        //    palette[0] = Color.Transparent;
        //    return palette;
        //}
        static List<Image> ExtractImage(string path, string filename)
        {
            var images = new List<Image>();
            var image = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), path, filename));
            var ms = new MemoryStream(image);
            var br = new BinaryReaderBigEndian(ms);
            var headerSize = br.ReadUInt32();


            var addresses = new List<uint>();
            do
            {
                addresses.Add(br.ReadUInt32());
            } while (br.Position() < addresses.First());
            addresses.Add(headerSize);


            for (var a = 0; a < addresses.Count - 1; a++)
            {
                var imagesData = new List<(uint addr, int size)>();

                br.Position(addresses[a]);
                while (br.Position() < addresses[a + 1])
                {
                    imagesData.Add((br.ReadUInt32(), br.ReadInt32()));
                    if (imagesData.Last().addr == 0)
                    {
                        imagesData.RemoveAt(imagesData.Count() - 1);
                        break;
                    }
                }

                foreach (var imageData in imagesData)
                {
                    br.Position(imageData.addr);
                    var bytes = br.ReadBytes(imageData.size);
                    if (bytes[0] == 0x06 && bytes[1] == 0x0F)
                        continue;
                    else if (bytes[0] + bytes[1] != 0)
                        images.Add(ExtractLargeImage(bytes));
                    else
                        images.AddRange(ExtractSmallImages(bytes));
                }
            }
            return images;
        }

        static Image ExtractLargeImage(byte[] data)
        {
            var bgimageMS = new MemoryStream(data);
            var bgimageBR = new BinaryReaderBigEndian(bgimageMS);
            var width = bgimageBR.ReadInt16();
            var height = bgimageBR.ReadInt16();
            var bpp8 = bgimageBR.ReadBytes(width * height);
            return new Image() { Width = width, Height = height, Data = bpp8 };
        }

        static List<Image> ExtractSmallImages(byte[] data)
        {
            var images = new List<Image>();
            var imagesMS = new MemoryStream(data);
            var imagesBR = new BinaryReaderBigEndian(imagesMS);

            var numberOfImages = imagesBR.ReadUInt32();
            var sizes = new List<(ushort addr, byte width, byte height, ushort size)>();
            for (var n = 0; n < numberOfImages; n++)
            {
                var addr = imagesBR.ReadUInt16();
                var width = imagesBR.ReadByte();

                var height = imagesBR.ReadByte();
                var size = (ushort)(imagesBR.ReadUInt16() - addr);

                sizes.Add((addr, width, height, size));
                imagesBR.Position(imagesBR.Position() - 2);
            }
            var last = sizes.Last();
            last.size = (ushort)(data.Length - sizes.Last().addr);
            sizes[^1] = last;
            foreach (var size in sizes)
            {
                imagesBR.Position(size.addr);
                images.Add(new Image()
                {
                    Width = size.width,
                    Height = size.height,
                    Data = imagesBR.ReadBytes(size.size),
                    Is16Color = ((size.width * size.height) > size.size)
                });
            }

            return images;

        }

    }
}
