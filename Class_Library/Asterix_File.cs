using System;
using System.Collections.Generic;
using System.Data;

namespace Class_Library
{
    public class Asterix_File
    {
        public string path = null;

        //public List<CAT10> listCAT10 = new List<CAT10>();

        //public List<CAT21> listCAT21 = new List<CAT21>();

        DataTable tabla_CAT10 = new DataTable();

        DataTable tabla_CAT21 = new DataTable();

        public Asterix_File(string path)
        {
            this.path = path;
        }
    }
}
