using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Class_Library;

namespace ClassLibrary
{
    public class AsterixFile

    {

        public List<string> Files_Names = new List<string>();
        public List<string> Files_Paths = new List<string>();
        public int Number_files_loaded = 0;

        public AsterixFile(List<string> File_Names, List<string> Files_Paths, int Number_files_loaded)
        {
            this.Files_Names = File_Names;
            this.Files_Paths = Files_Paths;
            this.Number_files_loaded = Number_files_loaded;
        }

        List<CAT10> CAT10_List = new List<CAT10>();  //Initialize the list of CAT10 messages
        //List<CAT21> CAT21_List = new List<CAT21>();  //Initialize the list of CAT21 messages
        DataTable CAT10_Table = new DataTable();     //Initialize the datatables of CAT10 messages
        DataTable CAT21_Table = new DataTable();     //Initialize the datatables of CAT21 messages
        public int CAT10_Number_Messages = 0;
        public int CAT21_Number_Messages = 0;
        

        public string ReadFile(string path)
        {
            try
            {
                byte[] File_Bytes = File.ReadAllBytes(path);
                List<byte[]> Bytes_List = new List<byte[]>();
                int i = 0;
                while (File_Bytes.Length > i)
                {
                    byte[] Octet_array = new byte[i];
                    int j = 0;
                    while (Octet_array.Length> j)
                    {
                        Octet_array[j] = File_Bytes[i];
                        j++;
                    }
                    Bytes_List.Add(Octet_array);
                    i++;
                }
                return "1";

            }
            catch (Exception e)
            {
                string Error_Text_User = "The binary file could not be read:";
                string Error_Message = e.Message;
                return Error_Text_User;
            }


        }

    }
}
}
