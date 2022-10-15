using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Class_Library;
using System.Globalization;

namespace ClassLibrary
{
    public class AsterixFile

    {
        public string path { set; get; }
        public string name { set; get; }

        public AsterixFile()
        {

        }
        public AsterixFile(string path, string name)
        {
            this.path = path;
            this.name = name;
        }
        List<CAT10> CAT10_Messages_List = new List<CAT10>();  //Initialize the list of CAT10 messages
        List<CAT21> CAT21_Messages_List = new List<CAT21>();  //Initialize the list of CAT21 messages
        DataTable CAT10_Table = new DataTable();     //Initialize the datatables of CAT10 messages
        DataTable CAT21_Table = new DataTable();     //Initialize the datatables of CAT21 messages
        public int CAT10_Number_Messages = 0;
        public int CAT21_Number_Messages = 0;
        string Proccess_message;
        readonly DecodeLibrary Library = new DecodeLibrary();

        public string ReadFile(string path, string name)
        {
            try
            {
                byte[] File_Bytes = File.ReadAllBytes(path);
                int Lenght_File_Bytes = File_Bytes.Length;
                List<byte[]> Raw_messages = new List<byte[]>();
                int i = 0;
                while (File_Bytes.Length - 2 > i)
                {
                    int Len_Message = File_Bytes[i + 1] + File_Bytes[i + 2];
                    byte[] Message = new byte[Len_Message];
                    int j = 0;
                    while (Len_Message > j)
                    {
                        Message[j] = File_Bytes[i];
                        j++;
                        i++;

                    }
                    Raw_messages.Add(Message);
                    Console.WriteLine(i);
                }
                i = 0;
                int Lenght_Raw_messages = Raw_messages.Count;
                List<string[]> Raw_messages_Bin = new List<string[]>();
                while (Lenght_Raw_messages > i)
                {
                    byte[] Raw_Single_Message_Byte = Raw_messages[i];
                    string[] Raw_Single_message_Bin = new string[Raw_messages[i].Count()];
                    int j = 0;
                    while (Raw_messages[i].Count() > j)
                    {
                        Raw_Single_message_Bin[j] = Convert.ToString(Raw_Single_Message_Byte[j],2);
                        j++;
                    }
                    Raw_messages_Bin.Add(Raw_Single_message_Bin);
                    i++;
                }
                i = 0;
                List<string[]> Raw_messages_Str = new List<string[]>();
                while (Lenght_Raw_messages > i)
                {
                    byte[] Raw_Single_Message_Byte = Raw_messages[i];
                    string[] Raw_Single_message_Str = new string[Raw_messages[i].Count()];
                    int j = 0;
                    while (Raw_messages[i].Count() > j)
                    {
                        Raw_Single_message_Str[j] = Raw_Single_Message_Byte[j].ToString(); ;
                        j++;
                    }
                    Raw_messages_Str.Add(Raw_Single_message_Str);
                    i++;
                }
                i = 0;
                //int Lenght_Raw_messages = Raw_messages.Count;
                while (Raw_messages_Str.Count() > i)
                {
                    if (Raw_messages_Str[i][0] == "10") //CAT10
                    {
                        CAT10_Number_Messages++;
                        CAT10 newCAT10_message = new CAT10(Raw_messages_Bin[i], Library);
                        CAT10_Messages_List.Add(newCAT10_message);
                    } 
                    if (Raw_messages_Str[i][0] == "21") //CAT21
                    {
                        CAT21_Number_Messages++;
                        CAT21 newCAT21_message = new CAT21(Raw_messages_Bin[i], Library);
                        CAT21_Messages_List.Add(newCAT21_message);
                    }
                    i++;
                }
                return "1";
            }   
            catch (Exception e)
            {
                string Error_Text_User = "The binary file" + name + "could not be read";
                string Error_Message = e.Message;
                return Error_Message; //Cambiar a Text user
            }
        }

    }
}

