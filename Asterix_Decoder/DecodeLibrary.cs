using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Class_Library
{
    public class DecodeLibrary
    {

        public string[] obtainFSPEC(string[] BinMessage)
        {
            //First we must check the lenght of FSPEC camp
            int FSPEC_Bytes_Num = 1;
            int FSPEC_Camp_Num = 3;
            int LenghtBin = BinMessage[3].Count();
            string FSPEC = BinMessage[FSPEC_Camp_Num];


            while (BinMessage[FSPEC_Camp_Num].Last() == '1')
            {
                FSPEC_Bytes_Num++;
                FSPEC_Camp_Num++;
                FSPEC = FSPEC + BinMessage[FSPEC_Camp_Num];
            }
            string[] Return_FSPEC = new string[2];
            Return_FSPEC[0] = Convert.ToString(FSPEC_Camp_Num);
            Return_FSPEC[1] = FSPEC;

            return Return_FSPEC;
        }

        static char flip(char c)
        {
            return (c == '0') ? '1' : '0';
        }

        public int twos_complement(string BinMessage)
        {
            string ones = "", twos = "";
            ones = twos = "";

            int i;
            int n = BinMessage.Length;

            for (i = 0; i < n; i++)
            {
                ones += flip(BinMessage[i]);
            }

            twos = ones;

            for (i = n - 1; i >= 0; i--)
            {
                if (ones[i] == '1')
                {
                    twos = twos.Substring(0, i) + '0' + twos.Substring(i + 1, twos.Length - (i + 1));
                }
                else
                {
                    twos = twos.Substring(0, i) + '1' + twos.Substring(i + 1, twos.Length - (i + 1));
                    break;
                }
            }

            if (i == -1)
            {
                twos = '1' + twos;
            }

            return Convert.ToInt32(twos, 2);
        }

        public string Target_Identification_Coding(string Six_bit_Message)
        {
            string Character = "";
            string bit1 = Convert.ToString(Six_bit_Message[5]);
            string bit2 = Convert.ToString(Six_bit_Message[4]);
            string bit3 = Convert.ToString(Six_bit_Message[3]);
            string bit4 = Convert.ToString(Six_bit_Message[2]);
            string bit5 = Convert.ToString(Six_bit_Message[1]);
            string bit6 = Convert.ToString(Six_bit_Message[0]);

            string probe = String.Concat(bit4, bit3, bit2, bit1, bit6, bit5);

            #region Column 1

            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "000000") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "000100") { Character = "A"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "001000") { Character = "B"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "001100") { Character = "C"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "010000") { Character = "D"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "010100") { Character = "E"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "011000") { Character = "F"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "011100") { Character = "G"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "100000") { Character = "H"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "100100") { Character = "I"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "101000") { Character = "J"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "101100") { Character = "K"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "110000") { Character = "L"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "110100") { Character = "M"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "111000") { Character = "N"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "111100") { Character = "O"; }

            #endregion

            #region Column 2

            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "000001") { Character = "P"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "000101") { Character = "Q"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "001001") { Character = "R"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "001101") { Character = "S"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "010001") { Character = "T"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "010101") { Character = "U"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "011001") { Character = "V"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "011101") { Character = "W"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "100001") { Character = "X"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "100101") { Character = "Y"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "101001") { Character = "Z"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "101101") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "110001") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "110101") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "111001") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "111101") { Character = ""; }

            #endregion

            #region Column 3

            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "000010") { Character = " "; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "000110") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "001010") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "001110") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "010010") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "010110") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "011010") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "011110") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "100010") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "100110") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "101010") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "101110") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "110010") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "110110") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "111010") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "111110") { Character = ""; }

            #endregion

            #region Column 3

            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "000011") { Character = "0"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "000111") { Character = "1"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "001011") { Character = "2"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "001111") { Character = "3"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "010011") { Character = "4"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "010111") { Character = "5"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "011011") { Character = "6"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "011111") { Character = "7"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "100011") { Character = "8"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "100111") { Character = "9"; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "101011") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "101111") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "110011") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "110111") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "111011") { Character = ""; }
            if (bit4 + bit3 + bit2 + bit1 + bit6 + bit5 == "111111") { Character = ""; }

            #endregion

            return Character;
        }

        public string ToD_Calc(int ToD_seconds)
        {
            int ToD_minutes; int ToD_hours; int sec;

            ToD_hours = ToD_seconds / 3600;

            ToD_minutes = ToD_hours % 60;

            sec = ToD_seconds % 60;

            string Tod_string = ToD_hours + ":" + ToD_minutes + ":" + sec +"UTC";
            return Tod_string;
        }
    }

}
