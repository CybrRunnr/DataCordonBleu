using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataCordonBleu_Framework.Models {
    public static class BitBlock {

        #region Public Methods
        /// <summary>
        /// Converts a string into a jagged array of integer corresponding to the binary value of the given characters.
        /// </summary>
        /// <param name="blockSize">Size of binary block</param>
        /// <param name="message">Message to be converted</param>
        /// <returns>Jagged array of integers where each array of integers represents the binary at a charter in the message</returns>
        public static int[][] MessageToBinary(string message, int blockSize) {
            //make arrays the same size always
            int[] msgInAscii = GetASCIIArr(message);
            List<int[]> retList = new List<int[]>();
            foreach (int item in msgInAscii) {
                retList.Add(IntToBitBlocks(item, blockSize));
            }
            return retList.ToArray();
        }

        /// <summary>
        /// Converts a jagged array of integer corresponding to the binary value of the given characters into a string.
        /// </summary>
        /// <param name="jag">Jagged 2D array to be converted</param>
        /// <param name="blockSize">Size of binary block</param>
        /// <returns>Message the array held</returns>
        public static string BinaryToMessage(int[][] jag, int blockSize) {
            string retString = "";
            foreach (int[] arr in jag) {
                char temp = (char)BinaryArrayToInt(arr, blockSize);
                retString += temp;
            }
            return retString;
        }

        /// <summary>
        /// Shifts a number by the given block size to create space (zeros) at the end of the binary number
        /// </summary>
        /// <param name="num">Number to be shifted</param>
        /// <param name="blockSize">Size of the shift</param>
        /// <returns>Integer number that when converted to binary will have [BlockSize] number of zeros at the end</returns>
        public static int MakeStorageSpace(int num, int blockSize) {
            int retNum = RightShift(num, blockSize);
            retNum = LeftShift(retNum, blockSize);
            return retNum;
        }

        /// <summary>
        /// Gets the remained of a number when it is divided by 2^[blockSize]
        /// </summary>
        /// <param name="num">Number to be modded</param>
        /// <param name="blockSize">Size of binary block</param>
        /// <returns>remained of a number when it is divided by 2^[blockSize]</returns>
        public static int GetMod(int num, int blockSize) {
            int ret = (byte)(num % Math.Pow(2, blockSize));
            return ret;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Converts an integer into a list of integers corresponding to the binary value of the given number.
        /// </summary>
        /// <param name="num">Number to convert</param>
        /// <param name="blockSize">Size of binary block</param>
        /// <returns>Array of integers represent the binary at that block</returns>
        private static int[] IntToBitBlocks(int num, int blockSize) {
            List<int> numList = new List<int>();
            int remainder;
            Double arrSize = 16 / blockSize;
            arrSize = Math.Ceiling(arrSize);
            do {
                int mod = GetMod(num, blockSize);
                remainder = num - mod;
                numList.Add(mod);
                num = RightShift(num, blockSize);
            } while (remainder != 0);
            numList.Reverse();
            while (numList.Count < arrSize) {
                numList.Insert(0, 0);
            }
            return numList.ToArray();
        }

        /// <summary>
        /// Converts an integer array corresponding to the binary value of a number to that number.
        /// </summary>
        /// <param name="arr">Array containing binary values</param>
        /// <param name="blockSize"></param>
        /// <returns>Size of binary block</returns>
        private static int BinaryArrayToInt(int[] arr, int blockSize) {
            int retNum = 0;
            int arrayLength = arr.Length;
            for (int ndx = 0; ndx < arrayLength; ndx++) {
                int valInArr = arr[ndx];
                int pwr = (int)Math.Pow(Math.Pow(2, blockSize), arrayLength - ndx - 1);
                int numToBeAdded = (pwr * valInArr);
                retNum += numToBeAdded;
            }
            return retNum;
        }

        /// <summary>
        /// Shifts a number to the right
        /// </summary>
        /// <param name="num">Number to shift</param>
        /// <param name="shift">Amount to shift by</param>
        /// <returns>A number that has been shifted right by [shift] places</returns>
        private static int RightShift(int num, int shift) {
            int ret = num >> shift;
            return ret;
        }

        /// <summary>
        /// Shifts a number to the left
        /// </summary>
        /// <param name="num">Number to shift</param>
        /// <param name="shift">Amount to shift by</param>
        /// <returns>A number that has been shifted left by [shift] places</returns>
        private static int LeftShift(int num, int shift) {
            int ret = num << shift;
            return ret;
        }

        /// <summary>
        /// Converts a string into an array of ASCII values
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <returns>Array of ASCII values</returns>
        private static int[] GetASCIIArr(string str) {
            List<int> values = new List<int>();
            foreach (char chr in str) {
                values.Add(chr);
            }
            return values.ToArray();
        }
        #endregion
    }
}