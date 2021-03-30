using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataCordonBleu_Framework.Models {
    public static class Shifter {

        public static List<int> IntToBitBlocks(int num, int shift) {
            List<int> numList = new List<int>();
            int remainder;
            do {
                int mod = GetMod(num, shift);
                remainder = num - mod;
                numList.Add(mod);;
                num = GetShift(num, shift);
            } while (remainder != 0);
            numList.Reverse();
            return numList;
        }

        private static int GetMod(int num, int shift) {
            int ret = (byte)(num % Math.Pow(2, shift));
            return ret;
        }

        private static int GetShift(int num, int shift) {
            int ret = num >> shift;
            return ret;
        }
    }
}