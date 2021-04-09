using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataCordonBleu_Framework.Models {
    public static class Shifter {

        /// <summary>
        /// Converts an integer into a list of integers corresponding to the binary value of the given number.
        /// </summary>
        /// <param name="num">Number to convert</param>
        /// <param name="blockSize">Size of binary block</param>
        /// <returns>List of integers represent the binary at that block</returns>
        public static int[] IntToBitBlocks(int num, int blockSize) {
            List<int> numList = new List<int>();
            int remainder;
            do {
                int mod = GetMod(num, blockSize);
                remainder = num - mod;
                numList.Add(mod);
                num = GetShift(num, blockSize);
            } while (remainder != 0);
            numList.Reverse();
            return numList.ToArray();
        }

        //make arrays the same size always

        public static int BinaryArrayToInt(int[] arr) {
            int retNum = 0;
            for (int ndx = 0; ndx < arr.Length; ndx++) {
                retNum += (int)Math.Pow(4, ndx) * arr[ndx];
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
    }
}