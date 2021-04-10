using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataCordonBleu_Framework.Models {
    public static class BitBlock {

        /// <summary>
        /// Converts a string into a jagged array of integer corresponding to the binary value of the given character.
        /// </summary>
        /// <param name="blockSize">Size of binary block</param>
        /// <param name="message">Message to be converted</param>
        /// <returns>Jagged array of integers where each array of integers represents the binary at a charter in the message</returns>
        public static int[][] MessageToBinary(int blockSize, string message) {
            //make arrays the same size always
            int[] msgInAscii = GetASCIIArr(message);
            List<int[]> retList = new List<int[]>();
            foreach (int item in msgInAscii) {
                retList.Add(IntToBitBlocks(item, blockSize));
            }
            return retList.ToArray();
        }

        public static string BinaryToMessage(int[][] jag) {
            string retString = "";
            foreach (int[] arr in jag) {
                char temp = (char)BinaryArrayToInt(arr);
                retString += temp;
            }
            return retString;
        }

        /// <summary>
        /// Converts an integer into a list of integers corresponding to the binary value of the given number.
        /// </summary>
        /// <param name="num">Number to convert</param>
        /// <param name="blockSize">Size of binary block</param>
        /// <returns>Array of integers represent the binary at that block</returns>
        private static int[] IntToBitBlocks(int num, int blockSize) {
            List<int> numList = new List<int>();
            int remainder;
            do {
                int mod = GetMod(num, blockSize);
                remainder = num - mod;
                numList.Add(mod);
                num = GetShift(num, blockSize);
            } while (remainder != 0);
            numList.Reverse();
            while (numList.Count < 4) {
                numList.Insert(0, 0);
            }
            return numList.ToArray();
        }

        private static int BinaryArrayToInt(int[] arr) {
            int retNum = 0;
            int arrayLength = arr.Length;
            for (int ndx = 0; ndx < arrayLength; ndx++) {
                int valInArr = arr[ndx];
                int pwr = (int)Math.Pow(4, arrayLength - ndx - 1);
                int numToBeAdded = (pwr * valInArr);
                retNum += numToBeAdded;
            }
            return retNum;
        }

        private static int GetMod(int num, int blockSize) {
            int ret = (byte)(num % Math.Pow(2, blockSize));
            return ret;
        }

        private static int GetShift(int num, int shift) {
            int ret = num >> shift;
            return ret;
        }

        private static int[] GetASCIIArr(string str) {
            List<int> values = new List<int>();
            foreach (char chr in str) {
                values.Add(chr);
            }
            return values.ToArray();
        }
    }
}