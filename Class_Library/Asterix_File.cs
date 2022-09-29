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

        public string path { set; get; }
        public string name { set; get; }

        public AsterixFile(string path, string name)
        {
            this.path = path;
            this.name = name;
        }


        List<CAT10> CAT10_List = new List<CAT10>();  //Initialize the list of CAT10 messages
        //List<CAT21> CAT21_List = new List<CAT21>();  //Initialize the list of CAT21 messages
        DataTable CAT10_Table = new DataTable();     //Initialize the datatables of CAT10 messages
        DataTable CAT21_Table = new DataTable();     //Initialize the datatables of CAT21 messages
        public int CAT10_Number_Messages = 0;
        public int CAT21_Number_Messages = 0;
        List<List<byte>> Raw_messages = new List<List<byte>>();

        //CAT number in byte
        byte CAT10_ID = 10;

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
                        Octet_array[j] = Bytes_List[i];
                        j++;
                    }
                    Bytes_List.Add(Octet_array);
                    i++;
                }
                i = 1;
                //Bucle that groups bytes into messages and clasify them by CAT
                while (Bytes_List.Count > i)
                {

                    int Len_Message = (BitConverter.ToInt32(Bytes_List[i],0) + BitConverter.ToInt32(Bytes_List[i+1],0));
                    int j = 0;
                    Raw_messages.Add(Bytes_List[i-1]);
                    while (Len_Message > j)
                    {
                        Raw_messages.Add(Bytes_List[i]);
                    }
                       
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

