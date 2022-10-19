using System;
using System.Collections.Generic;
using System.Text;

namespace Flight_Data_Items
{
    public class Target_Report_Descriptor
    {
        public string TYP;

        public string DCR;

        public string CHN;

        public string GBS;

        public string CRT;

        public string FX;


        public Target_Report_Descriptor()
        {
            this.TYP = "N/A";
            this.DCR = "N/A";
            this.CHN = "N/A";
            this.GBS = "N/A";
            this.CRT = "N/A";
            this.FX = "N/A";
        }
        
        public Target_Report_Descriptor(List<string> Bin_Target_Report_Descrptor)
        {

        }
    }
}
