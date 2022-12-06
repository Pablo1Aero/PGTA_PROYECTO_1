 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Class_Library;
using System.Globalization;
using Asterix_Decoder;
using System.Diagnostics.Tracing;
using System.Windows.Forms;
using System.Xml;

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

        public List<CAT10> CAT10_Messages_List = new List<CAT10>();  //Initialize the list of CAT10 messages
        public List<CAT21> CAT21_Messages_List = new List<CAT21>();  //Initialize the list of CAT21 messages
        public DataTable CAT10_Table = new DataTable();     //Initialize the datatables of CAT10 messages
        public DataTable CAT21_Table = new DataTable();     //Initialize the datatables of CAT21 messages
        public int CAT10_Number_Messages = 0;
        public int CAT21_Number_Messages = 0;
        public int FileSize;
        
        readonly DecodeLibrary Library = new DecodeLibrary();

        public DataTable ReadFile(string path, string name)
        {
            try
            {
                Set_DataTable_Columns_CAT10(CAT10_Table);
                byte[] File_Bytes = File.ReadAllBytes(path);
                int Lenght_File_Bytes = File_Bytes.Length;
                FileSize = Lenght_File_Bytes;
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
                        Raw_Single_message_Bin[j] = Convert.ToString(Raw_Single_Message_Byte[j], 2);
                        while (Raw_Single_message_Bin[j].Count() < 8)
                        {
                            Raw_Single_message_Bin[j] = 0 + Raw_Single_message_Bin[j];
                        }
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
                //Set_DataTable_Columns_CAT10(CAT10_Table);
                while (Raw_messages_Str.Count() > i)
                {
                    if (Raw_messages_Str[i][0] == "10") //CAT10
                    {
                        CAT10_Number_Messages++;                       
                        CAT10 newCAT10_message = new CAT10(Raw_messages_Bin[i], Library);
                        newCAT10_message.set_Message_Num(CAT10_Number_Messages);
                        CAT10_Messages_List.Add(newCAT10_message);
                        Add_Row_Message(newCAT10_message);
                    }
                    if (Raw_messages_Str[i][0] == "21") //CAT21
                    {
                        CAT21_Number_Messages++;
                        CAT21 newCAT21_message = new CAT21(Raw_messages_Bin[i], Library);
                        CAT21_Messages_List.Add(newCAT21_message);
                    }
                    i++;
                }
                
                return CAT10_Table ;

            }

            catch (Exception e)
            {
               string Error_Text_User = "The binary file" + name + "could not be read";
               string Error_Message = e.Message;
               MessageBox.Show(Convert.ToString(CAT10_Number_Messages));
                return null;
               //return Error_Message; //Cambiar a Text user
            }
           
        }
        public List<CAT10> get_CAT10_List()
        {
            return this.CAT10_Messages_List;
        }

        public int get_FileSize()
        {
            return FileSize;
        }
        public int set_FileRead_Progress(int i)
        {
            return i;
        }

        public List<CAT10> get_CAT10List()
        {
            return CAT10_Messages_List;
        }

        private void Set_DataTable_Columns_CAT10(DataTable T)
        {
            T.Columns.Add("Number", typeof(Int32));
            T.Columns.Add("Category", typeof(String));
            T.Columns.Add("SAC", typeof(String));
            T.Columns.Add("SIC", typeof(String));
            T.Columns.Add("TargetID", typeof(String));
            T.Columns.Add("Track Number", typeof(String));
            T.Columns.Add("Target Report Descriptor", typeof(String));
            T.Columns.Add("Message Type", typeof(String));
            T.Columns.Add("Flight Level", typeof(String));
            T.Columns.Add("Time of Day", typeof(String));
            T.Columns.Add("Track Status", typeof(String));
            T.Columns.Add("Position in WGS-84 Co-ordinates", typeof(String));
            T.Columns.Add("Position in Cartesian Co-ordinates", typeof(String));
            T.Columns.Add("Position in Polar Co-ordinates", typeof(String));
            T.Columns.Add("Track velocity in polar Co-ordinates", typeof(String));
            T.Columns.Add("Track velocity in Cartesian Co-ordinates", typeof(String));
            T.Columns.Add("Target size and orientation", typeof(String));
            T.Columns.Add("Target Address", typeof(String));
            T.Columns.Add("System Status", typeof(String));
            T.Columns.Add("Vehicle fleet ID", typeof(String));
            T.Columns.Add("Pre-programmed Message", typeof(String));
            T.Columns.Add("Measured Height", typeof(String));
            T.Columns.Add("Mode-3A Code", typeof(String));
            T.Columns.Add("Mode-S-MB Data", typeof(String));
            T.Columns.Add("Standard Deviation of Position", typeof(String));
            T.Columns.Add("Presence", typeof(String));
            T.Columns.Add("Amplitude of Primary Plot", typeof(String));
            T.Columns.Add("Calculated Acceleration", typeof(String));
        }

        private void Add_Row_Message(CAT10 Message)
        {
            var row = CAT10_Table.NewRow();
            row["Number"] = Message.Num_Message;
            row["Category"] = Message.CAT;
            if (Message.CAT != null) { row["Category"] = Message.CAT; }
            else { row["Category"] = "No Data"; }
            if (Message.SAC != null) { row["SAC"] = Message.SAC; }
            else { row["SAC"] = "No Data"; }
            if (Message.SIC != null) { row["SIC"] = Message.SIC; }
            else { row["SIC"] = "No Data"; }
            if (Message.Target_Identification != null)
            {
                if (Message.Target_Identification.Replace(" ", "") != "") { row["TargetID"] = Message.Target_Identification; }
                else { row["TargetID"] = "No Data"; }
            }
            else { row["TargetID"] = "No Data"; }
            if (Message.TYP != null) { row["Target Report Descriptor"] = "Click to more info"; }
            else { row["Target Report Descriptor"] = "No Data"; }
            if (Message.Message_type != null) { row["Message Type"] = Message.Message_type; }
            else { row["Message Type"] = "No Data"; }
            if (Message.FL != null) { row["Flight Level"] = Message.FL; }
            else { row["Flight Level"] = "No Data"; }
            if (Message.Track_Number != null) { row["Track Number"] = Message.Track_Number; }
            else { row["Track Number"] = "No Data"; }
            if (Message.ToD != null) { row["Time of Day"] = Message.ToD; }
            else { row["Time of Day"] = "No Data"; }
            if (Message.CNF != null) { row["Track Status"] = "Click to expand"; }
            else { row["Track Status"] = "No Data"; }
            if (Message.Lat_WGS84 != null && Message.Lon_WGS84 != null) { row["Position in WGS-84 Co-ordinates"] = Message.Lat_WGS84 + ", " + Message.Lon_WGS84; }
            else { row["Position in WGS-84 Co-ordinates"] = "No Data"; }
            if (Message.X_Component != null && Message.Y_Component != null) { row["Position in Cartesian Co-ordinates"] = Message.X_Component + ", " + Message.Y_Component; }
            else { row["Position in Cartesian Co-ordinates"] = "No Data"; }            
            if (Message.RHO != null && Message.THETA != null) { row["Position in Polar Co-ordinates"] = "RHO: " + Message.RHO + "  THETA: " + Message.THETA; }
            else { row["Position in Polar Co-ordinates"] = "No Data"; }
            if (Message.Ground_Speed != null && Message.Track_Angle != null) { row["Track velocity in polar Co-ordinates"] = "Ground Speed: " + Message.Ground_Speed + "  Track Angle: " + Message.Track_Angle; }
            else { row["Track velocity in polar Co-ordinates"] = "No Data"; }
            if (Message.Vx != null && Message.Vy != null) { row["Track velocity in Cartesian Co-ordinates"] = "Vx: " + Message.Vx + "  Vy: " + Message.Vy; }
            else { row["Track velocity in Cartesian Co-ordinates"] = "No Data"; }
            if (Message.Lenght != null && Message.Orientation != null && Message.Width != null) { row["Target size and orientation"] = "Lenght: " + Message.Lenght + " Orientation: "+ Message.Orientation + " Width: "+ Message.Width; }
            else { row["Target size and orientation"] = "No Data"; }
            
            if (Message.Target_Address != null) { row["Target Address"] = "Target Address: " + Message.Target_Address ; }
            else { row["Target Address"] = "No Data"; }
            if (Message.NOGO != null && Message.OVL != null && Message.TSV != null && Message.DIV != null && Message.TTF != null) { row["System Status"] = "NOGO: "+ Message.NOGO + " OVL: "+ Message.OVL + " TSV: "+ Message.TSV + " DIV: "+ Message.DIV + " TTF: " + Message.TTF; }
            else { row["System Status"] = "No Data"; }
            if (Message.VFI != null) { row["Vehicle fleet ID"] = Message.VFI; }
            else { row["Vehicle fleet ID"] = "No Data"; }
            if (Message.TRB != null && Message.MSG != null) { row["Pre-programmed Message"] = "TRB: " + Message.TRB + " MSG: " + Message.MSG ; }
            else { row["Pre-programmed Message"] = "No Data"; }
            if (Message.Measured_Height != null) { row["Measured Height"] = Message.Measured_Height; }
            else { row["Measured Height"] = "No Data"; }
            if (Message.Mode3_A_reply != null && Message.V != null && Message.G != null && Message.L != null) { row["Mode-3A Code"] = "Mode 3A reply: " + Message.Mode3_A_reply + " V: " + Message.V + " G: " + Message.G + " L: "+ Message.L; }
            else { row["Mode-3A Code"] = "No Data"; }
            if (Message.DataList != null) { row["Mode-S-MB Data"] = "No data"; }
            else { row["Mode-S-MB Data"] = "No Data"; }
            if (Message.Ox != null && Message.Oy != null && Message.Oxy != null) { row["Standard Deviation of Position"] = "Ox: " + Message.Ox + " Oy: " + Message.Oy + " Oxy: "+ Message.Oxy; }
            else { row["Standard Deviation of Position"] = "No Data"; }
            if (Message.REP != null && Message.DRHO != null && Message.DTHETA != null) { row["Presence"] = "REP: " + Message.REP + " DRHO: "+Message.DRHO+" DTHETA: "+ Message.DTHETA; }
            else { row["Presence"] = "No Data"; }
            if (Message.PAM != null) { row["Amplitude of Primary Plot"] = Message.PAM; }
            else { row["Amplitude of Primary Plot"] = "No Data"; }
            if (Message.Ax != null && Message.Ay != null) { row["Calculated Acceleration"] = "Ax: " + Message.Ax + " Ay: "+ Message.Ay; }
            else { row["Calculated Acceleration"] = "No Data"; }
            
            CAT10_Table.Rows.Add(row);


        }

    }
}

