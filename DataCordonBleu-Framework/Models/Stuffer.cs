using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using DataCordonBleu_Framework.Models;

namespace DataCordonBleu_Framework.Models {
    public class Stuffer {
        #region Private Properties
        private string _Message;
        private string _Password;
        private string _FilePath;
        private int _BlockSize = 2;
        private Bitmap _ImageBMP;
        private int[][] _MessageArray;
        #endregion

        #region Constructors
        public Stuffer() { }

        public Stuffer(string msg, Bitmap bmp, int blockSize) {
            Message = msg;
            ImageBMP = bmp;
            BlockSize = blockSize;
        }
        #endregion

        #region Properities
        [Display(Name = "Message")]
        [Required]
        public string Message {
            get {
                if (string.IsNullOrEmpty(_Message)) {
                    _Message = "";
                    return _Message;
                } else {
                    return _Message;
                }
            }

            set { _Message = value; }
        }

        [Display(Name = "Password to encode")]
        [Required]
        public string Password {
            get { return _Password; }
            set { _Password = value; }
        }

        public string FilePath {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        public int BlockSize {
            get { return _BlockSize; }
            set { _BlockSize = value; }
        }

        public Bitmap ImageBMP {
            get { return _ImageBMP; }
            set { _ImageBMP = value; }
        }

        public int[][] MessageArray {
            get {
                if (_MessageArray == null) {
                    _MessageArray = BitBlock.MessageToBinary(Message, BlockSize);
                    return _MessageArray;
                } else {
                    return _MessageArray;
                }

            }
            set {
                _MessageArray = value;
            }
        }
        #endregion

        #region Public Methods
        public void InsertMessage() {
            //int[] colorArr = GetArrayOfColors();
            int count = 0;
            int[] mess = ToOneDemArray(MessageArray);
            for (int x = 0; x < ImageBMP.Width; x++) {
                for (int y = 0; y < ImageBMP.Height; y++) {
                    Color pixelColor = ImageBMP.GetPixel(x, y);
                    Color newColor = MakeSpaceInPixel(pixelColor);
                    int red, green, blue;
                    if (count < mess.Length) {
                        red = newColor.R + mess[count];
                    } else {
                        red = newColor.R;
                    }
                    count++;
                    if (count < mess.Length) {
                        green = newColor.G + mess[count];
                    } else {
                        green = newColor.G;
                    }
                    count++;
                    if (count < mess.Length) {
                        blue = newColor.B + mess[count];
                    } else {
                        blue = newColor.B;
                    }
                    count++;
                    newColor = Color.FromArgb(red, green, blue);
                    ImageBMP.SetPixel(x, y, newColor);
                }
            }
        }

        public void ExtractMessage() {
            List<int> messNums = new List<int>();
            for (int x = 0; x < ImageBMP.Width; x++) {
                for (int y = 0; y < ImageBMP.Height; y++) {
                    Color pixelColor = ImageBMP.GetPixel(x, y);
                    messNums.Add(BitBlock.GetMod(pixelColor.R, BlockSize));
                    messNums.Add(BitBlock.GetMod(pixelColor.G, BlockSize));
                    messNums.Add(BitBlock.GetMod(pixelColor.B, BlockSize));
                }

            }
            MessageArray = ToTwoDemArray(messNums.ToArray());
            Message = BitBlock.BinaryToMessage(MessageArray, BlockSize);
        }
        #endregion

        #region Private Methods
        private int[] ToOneDemArray(int[][] twoD) {
            List<int> retList = new List<int>();
            for (int ndx = 0; ndx < MessageArray.Length; ndx++) {
                int[] arrForOneChar = MessageArray[ndx];
                for (int i = 0; i < arrForOneChar.Length; i++) {
                    retList.Add(arrForOneChar[i]);
                }
            }
            return retList.ToArray();
        }

        private int[][] ToTwoDemArray(int[] oneD) {
            List<int[]> retList = new List<int[]>();
            for (int ndx = 0; ndx < oneD.Length; ndx += 4) {
                IEnumerable<int> tempList = oneD.Skip(ndx).Take(4);
                int[] tempArr = tempList.ToArray();
                if (tempArr.Sum() == 0) {
                    return retList.ToArray();
                }
                retList.Add(tempArr);
            }
            return retList.ToArray();
        }

        private Color MakeSpaceInPixel(Color pixel) {
            int red = BitBlock.MakeStorageSpace(pixel.R, BlockSize);
            int green = BitBlock.MakeStorageSpace(pixel.G, BlockSize);
            int blue = BitBlock.MakeStorageSpace(pixel.B, BlockSize);
            Color newColor = Color.FromArgb(red, green, blue);
            return newColor;
        }

        //private Color EncodePixel() { }
        #endregion

        #region Old Stuff
        private int[] GetArrayOfColors() {
            List<Color> pixelList = new List<Color>();
            List<int> colorList = new List<int>();
            for (int x = 0; x < ImageBMP.Width; x++) {
                //Gets X coordinate
                for (int y = 0; y < ImageBMP.Height; y++) {
                    pixelList.Add(ImageBMP.GetPixel(x, y));
                }
            }
            foreach (Color pixel in pixelList) {
                colorList.Add(pixel.R);
                colorList.Add(pixel.G);
                colorList.Add(pixel.B);
            }
            return colorList.ToArray();
        }

        private List<Color> ArrayOfColorsToPixels(int[] arr) {
            List<Color> pixelList = new List<Color>();
            for (int ndx = 0; ndx < arr.Length; ndx += 3) {
                int red = arr[ndx];
                int green = arr[ndx + 1];
                int blue = arr[ndx + 2];
                Color temp = Color.FromArgb(red, green, blue);
                pixelList.Add(temp);
            }
            return pixelList;
        }

        //private Color GetPixel(int xCoord, int yCoord) {
        //    for (int x = xCoord; x < ImageBMP.Width; x++) {
        //        //Gets X coordinate
        //        for (int y = yCoord; y < ImageBMP.Height; y++) {
        //            //Color: Struct that hold the ARGB values of a pixel
        //            return ImageBMP.GetPixel(x, y);
        //        }
        //    }
        //}
    }
    #endregion
}