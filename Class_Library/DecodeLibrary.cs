using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
