using System;
using System.Collections.Generic;

namespace Flight_Data_Items
{
    public class Message_Type
    {
        public string type;

        public List<string> Table_type = new List<string>();

        public Message_Type()
        {
            this.type = "N/A";
        }

        public Message_Type(List<string> Bin_Message_Type)
        {
            List<string> default_Message_Type = new List<string>() {"Target Report" , "Start of Update Cycle" , "Periodic Status Message" , "Event-Triggered Status Message" };

            this.type = default_Message_Type[Convert.ToInt32(Bin_Message_Type[0], 2)];
        }
    }
}
