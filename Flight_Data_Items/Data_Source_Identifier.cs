using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Flight_Data_Items
{
    internal class Data_Source_Identifier
    {
        public string SAC;

        public string SIC;

        public List<string> Data_Source_ID_Table = new List<string>();

        public Data_Source_Identifier()
        {
            this.SAC = "N/A";
            this.SIC = "N/A";
        }

        public Data_Source_Identifier(List<string> Bin_Data_Source_ID)
        {
            this.SAC = Convert.ToString(Convert.ToInt32(string.Join("", Bin_Data_Source_ID.GetRange(1, 8)), 2), 10);
            this.SIC = Convert.ToString(Convert.ToInt32(string.Join("", Bin_Data_Source_ID.GetRange(8, 8)), 2), 10);
        }
    }
}
