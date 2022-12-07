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
using Newtonsoft.Json.Converters;

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
        public DataTable CATAll_Table = new DataTable();
        public List<DataTable> DataTables_List = new List<DataTable>();
        public int CAT10_Number_Messages = 0;
        public int CAT21_Number_Messages = 0;
        public int FileSize;
        public int Loading_Process = 0;
        public ProgressBar LoadingFile_ProgressBar = new ProgressBar();


        readonly DecodeLibrary Library = new DecodeLibrary();

        public List<DataTable> ReadFile(string path, string name)
        {
            try
            {
                Set_DataTable_Columns_CAT10(CAT10_Table);
                Set_Data_Table_Columns_CAT21(CAT21_Table);
                Set_Data_Table_Columns_CATAll(CATAll_Table);
                byte[] File_Bytes = File.ReadAllBytes(path);
                int Lenght_File_Bytes = File_Bytes.Length;
                LoadingFile_ProgressBar.Maximum = File_Bytes.Length;
                LoadingFile_ProgressBar.Minimum = 0;
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
                        Add_Row_Message_CAT10(newCAT10_message);
                        Add_Row_Message_CATAll(newCAT10_message);
                    }
                    if (Raw_messages_Str[i][0] == "21") //CAT21
                    {
                        CAT21_Number_Messages++;
                        CAT21 newCAT21_message = new CAT21(Raw_messages_Bin[i], Library);
                        newCAT21_message.set_Message_Num(CAT21_Number_Messages);
                        CAT21_Messages_List.Add(newCAT21_message);
                        Add_Row_Message_CAT21(newCAT21_message);
                        Add_Row_Message_CATAll(newCAT21_message);
                    }
                    //Loading_Process++;
                    //set_ProgressBar_Value(LoadingFile_ProgressBar, Loading_Process);
                    //LoadingFile_ProgressBar.Visible = true;
                    i++;
                }
                DataTables_List.Add(CAT10_Table);
                DataTables_List.Add(CAT21_Table);
                DataTables_List.Add(CATAll_Table);

                return DataTables_List;

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
        private void set_ProgressBar_Value(ProgressBar P, int value)
        {
            P.Value = value;
        }
        public int get_LoadingProcess()
        {
            return this.Loading_Process;
        }

        public List<CAT10> get_CAT10_List()
        {
            return this.CAT10_Messages_List;
        }

        public List<CAT21> get_CAT21_List()
        {
            return this.CAT21_Messages_List;
        }

        public int get_FileSize()
        {
            return FileSize;
        }
        public int set_FileRead_Progress()
        {
            return Loading_Process;
        }

        public List<CAT10> get_CAT10List()
        {
            return CAT10_Messages_List;
        }

        public List<CAT21> get_CAT21List()
        {
            return CAT21_Messages_List;
        }

        private void Set_DataTable_Columns_CAT10(DataTable T)
        {
            T.Columns.Add("Number", typeof(Int32));
            T.Columns.Add("Category", typeof(String));
            T.Columns.Add("SAC", typeof(String));
            T.Columns.Add("SIC", typeof(String));
            T.Columns.Add("TargetID", typeof(String));
            T.Columns.Add("Track Number", typeof(String));
            T.Columns.Add("Target Report\nDescriptor", typeof(String));
            T.Columns.Add("Flight Level", typeof(String));
            T.Columns.Add("Position in WGS-84\nCo-ordinates", typeof(String));
            T.Columns.Add("Mode-3A Code", typeof(String));
            T.Columns.Add("Mode-S-MB Data", typeof(String));
            T.Columns.Add("Target Address", typeof(String));

            T.Columns.Add("Message Type", typeof(String));
            T.Columns.Add("Time of Day", typeof(String));
            T.Columns.Add("Track Status", typeof(String));
            T.Columns.Add("Position in Cartesian\nCo-ordinates", typeof(String));
            T.Columns.Add("Position in Polar\nCo-ordinates", typeof(String));
            T.Columns.Add("Track velocity in\npolar Co-ordinates", typeof(String));
            T.Columns.Add("Track velocity in\nCartesian Co-ordinates", typeof(String));
            T.Columns.Add("Target size and\norientation", typeof(String));
            T.Columns.Add("System Status", typeof(String));
            T.Columns.Add("Vehicle fleet ID", typeof(String));
            T.Columns.Add("Pre-programmed\nMessage", typeof(String));
            T.Columns.Add("Measured Height", typeof(String));
            T.Columns.Add("Standard Deviation\nof Position", typeof(String));
            T.Columns.Add("Presence", typeof(String));
            T.Columns.Add("Amplitude of\nPrimary Plot", typeof(String));
            T.Columns.Add("Calculated\nAcceleration", typeof(String));
        }

        private void Set_Data_Table_Columns_CAT21(DataTable T)
        {
            T.Columns.Add("Number", typeof(Int32));
            T.Columns.Add("Category", typeof(String));
            T.Columns.Add("SAC", typeof(String));
            T.Columns.Add("SIC", typeof(String));
            T.Columns.Add("TargetID", typeof(String));
            T.Columns.Add("Track Number", typeof(String));
            T.Columns.Add("Target Report\nDescriptor", typeof(String));
            T.Columns.Add("Flight Level", typeof(String));
            T.Columns.Add("Position in WGS-84\nCo-ordinates", typeof(String));
            T.Columns.Add("Mode-3A Code", typeof(String));
            T.Columns.Add("Mode-S-MB Data", typeof(String));
            T.Columns.Add("Target Address", typeof(String));

            T.Columns.Add("Aircraft Operational Status", typeof(String));
            T.Columns.Add("Service Identification", typeof(String));
            T.Columns.Add("Service Management", typeof(String));
            T.Columns.Add("Emitter Category", typeof(String));
            T.Columns.Add("ToA for Position", typeof(String));
            T.Columns.Add("ToA for Velocity", typeof(String));
            T.Columns.Add("Time of Message Reception\nfor Position", typeof(String));
            T.Columns.Add("Time of Message Reception\nfor Position - High Precision", typeof(String));
            T.Columns.Add("Time of Message Reception\nfor Velocity", typeof(String));
            T.Columns.Add("Time of Message Reception\nfor Velocity - High Precision", typeof(String));
            T.Columns.Add("Time of Report Transmission", typeof(String));
            T.Columns.Add("Quality Indicators", typeof(String));
            T.Columns.Add("Trajectory Intent", typeof(String));
            T.Columns.Add("Position in WGS-84\nCo-ordinates, High Precision", typeof(String));
            T.Columns.Add("Message Amplitude", typeof(String));
            T.Columns.Add("Geometric Height", typeof(String));
            T.Columns.Add("Selected Altitude", typeof(String));
            T.Columns.Add("Final State Selected Altitude", typeof(String));
            T.Columns.Add("Air Speed", typeof(String));
            T.Columns.Add("True Airspeed", typeof(String));
            T.Columns.Add("Magnetic Heading", typeof(String));
            T.Columns.Add("Barometric Vertical Rate", typeof(String));
            T.Columns.Add("Geometric Vertical Rate", typeof(String));
            T.Columns.Add("Airborne Ground Vector", typeof(String));
            T.Columns.Add("Track Angle Rate", typeof(String));
            T.Columns.Add("Target Status", typeof(String));
            T.Columns.Add("MOPS Version", typeof(String));
            T.Columns.Add("Met Information", typeof(String));
            T.Columns.Add("Roll Angle", typeof(String));
            T.Columns.Add("ACAS Resolution Advisory\nReport", typeof(String));
            T.Columns.Add("Surface Capabilities\nand Characteristics", typeof(String));
            T.Columns.Add("Data Ages", typeof(String));
            T.Columns.Add("Receiver ID", typeof(String));
        }

        private void Add_Row_Message_CAT10(CAT10 Message)
        {
            var row = CAT10_Table.NewRow();
            row["Number"] = Message.Num_Message;
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
            if (Message.TYP != null) { row["Target Report\nDescriptor"] = "TYP: " + Message.TYP + "\nDCR: " + Message.DCR + "\nCHN: " + Message.CHN + "\nGBS: " + Message.GBS + "\nCRT: " + Message.CRT; }
            else { row["Target Report\nDescriptor"] = "No Data"; }
            if (Message.Message_type != null) { row["Message Type"] = Message.Message_type; }
            else { row["Message Type"] = "No Data"; }
            if (Message.FL != null) { row["Flight Level"] = Message.FL; }
            else { row["Flight Level"] = "No Data"; }
            if (Message.Track_Number != null) { row["Track Number"] = Message.Track_Number; }
            else { row["Track Number"] = "No Data"; }
            if (Message.ToD != null) { row["Time of Day"] = Message.ToD; }
            else { row["Time of Day"] = "No Data"; }
            if (Message.CNF != null)
            {
                row["Track Status"] = "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH;
                if (Message.TOM != null && Message.DOU != null && Message.MRS != null)
                {
                    row["Track Status"] = "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH + "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH + "\nTOM: " + Message.TOM + "\nDOU: " + Message.MRS;
                    if (Message.GHO != null)
                    {
                        row["Track Status"] = "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH + "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH + "\nTOM: " + Message.TOM + "\nDOU: " + Message.MRS + "\nGHO: " + Message.GHO;
                    }
                }
            }
            else { row["Track Status"] = "No Data"; }
            if (Message.Lat_WGS84 != null && Message.Lon_WGS84 != null) { row["Position in WGS-84\nCo-ordinates"] = "Lat: " + Message.Lat_WGS84 + "\nLon: " + Message.Lon_WGS84; }
            else { row["Position in WGS-84\nCo-ordinates"] = "No Data"; }
            if (Message.X_Component != null && Message.Y_Component != null) { row["Position in Cartesian\nCo-ordinates"] = "X: " + Message.X_Component + "m" + "\nY:" + Message.Y_Component + "m"; }
            else { row["Position in Cartesian\nCo-ordinates"] = "No Data"; }
            if (Message.RHO != null && Message.THETA != null) { row["Position in Polar\nCo-ordinates"] = "RHO: " + Message.RHO + "º" + "\nTHETA: " + Message.THETA + "º"; }
            else { row["Position in Polar\nCo-ordinates"] = "No Data"; }
            if (Message.Ground_Speed != null && Message.Track_Angle != null) { row["Track velocity in\nCartesian Co-ordinates"] = "Gs: " + Message.Ground_Speed + "kt" + "\nTa: " + Message.Track_Angle + "º"; }
            else { row["Track velocity in\nCartesian Co-ordinates"] = "No Data"; }
            if (Message.Vx != null && Message.Vy != null) { row["Track velocity in\nCartesian Co-ordinates"] = "Vx: " + Message.Vx + "m/s" + "\nVy: " + Message.Vy + "m/s"; }
            else { row["Track velocity in\nCartesian Co-ordinates"] = "No Data"; }
            if (Message.Lenght != null && Message.Orientation != null && Message.Width != null) { row["Target size and\norientation"] = "Lenght: " + Message.Lenght + "\nOrientation: " + Message.Orientation + "\nWidth: " + Message.Width; }
            else { row["Target size and\norientation"] = "No Data"; }

            if (Message.Target_Address != null) { row["Target Address"] = Message.Target_Address; }
            else { row["Target Address"] = "No Data"; }
            if (Message.NOGO != null && Message.OVL != null && Message.TSV != null && Message.DIV != null && Message.TTF != null) { row["System Status"] = "NOGO: " + Message.NOGO + "\nOVL: " + Message.OVL + "\nTSV: " + Message.TSV + "\nDIV: " + Message.DIV + "\nTTF: " + Message.TTF; }
            else { row["System Status"] = "No Data"; }
            if (Message.VFI != null) { row["Vehicle fleet ID"] = Message.VFI; }
            else { row["Vehicle fleet ID"] = "No Data"; }
            if (Message.TRB != null && Message.MSG != null) { row["Pre-programmed\nMessage"] = "TRB: " + Message.TRB + "\nMSG: " + Message.MSG; }
            else { row["Pre-programmed\nMessage"] = "No Data"; }
            if (Message.Measured_Height != null) { row["Measured Height"] = Message.Measured_Height; }
            else { row["Measured Height"] = "No Data"; }
            if (Message.Mode3_A_reply != null && Message.V != null && Message.G != null && Message.L != null) { row["Mode-3A Code"] = "3/A reply: " + Message.Mode3_A_reply + "\nV: " + Message.V + "\nG: " + Message.G + "\nL: " + Message.L; }
            else { row["Mode-3A Code"] = "No Data"; }
            if (Message.DataList != null) { row["Mode-S-MB Data"] = "No data"; }
            else { row["Mode-S-MB Data"] = "No Data"; }
            if (Message.Ox != null && Message.Oy != null && Message.Oxy != null) { row["Standard Deviation\nof Position"] = "Ox: " + Message.Ox + "m" + "\nOy: " + Message.Oy + "m" + "\nOxy: " + Message.Oxy + "m"; }
            else { row["Standard Deviation\nof Position"] = "No Data"; }
            if (Message.REP != null && Message.DRHO != null && Message.DTHETA != null) { row["Presence"] = "REP: " + Message.REP + "\nDRHO: " + Message.DRHO + "m" + "\nDTHETA: " + Message.DTHETA + "º"; }
            else { row["Presence"] = "No Data"; }
            if (Message.PAM != null) { row["Amplitude of\nPrimary Plot"] = Message.PAM; }
            else { row["Amplitude of\nPrimary Plot"] = "No Data"; }
            if (Message.Ax != null && Message.Ay != null) { row["Calculated\nAcceleration"] = "Ax: " + Message.Ax + "m/s\xB2" + "\nAy: " + Message.Ay + "m/s\xB2"; }
            else { row["Calculated\nAcceleration"] = "No Data"; }

            CAT10_Table.Rows.Add(row);


        }

        private void Add_Row_Message_CAT21(CAT21 Message)
        {
            var row = CAT21_Table.NewRow();
            row["Number"] = Message.Num_Message;
            if (Message.CAT != null) { row["Category"] = Message.CAT; }
            else { row["Category"] = "No Data"; }
            if (Message.SAC != null) { row["SAC"] = Message.SAC; }
            else { row["SAC"] = "No Data"; }
            if (Message.SIC != null) { row["SIC"] = Message.SIC; }
            else { row["SIC"] = "No Data"; }
            if (Message.Target_Identification != null) { row["TargetID"] = Message.Target_Identification; }
            else { row["TargetID"] = "No Data"; }
            if (Message.ATP != null)
            {
                row["Target Report\nDescriptor"] = "ATP: " + Message.ATP + "\nARC: " + Message.ARC + "\nRC: " + Message.RC + "\nRAB: " + Message.RAB;
                if (Message.DCR != null)
                {
                    row["Target Report\nDescriptor"] = "ATP: " + Message.ATP + "\nARC: " + Message.ARC + "\nRC: " + Message.RC + "\nRAB: " + Message.RAB + "\nDCR: " + Message.DCR + "\nGBS: " + Message.GBS + "\nSIM: " + Message.SIM + "\nTST: " + Message.TST + "\nSAA: " + Message.SAA + "\nCL: " + Message.CL;
                    if (Message.IPC != null)
                    {
                        row["Target Report\nDescriptor"] = "ATP: " + Message.ATP + "\nARC: " + Message.ARC + "\nRC: " + Message.RC + "\nRAB: " + Message.RAB + "\nDCR: " + Message.DCR + "\nGBS: " + Message.GBS + "\nSIM: " + Message.SIM + "\nTST: " + Message.TST + "\nSAA: " + Message.SAA + "\nCL: " + Message.CL + "\nIPC: " + Message.IPC + "\nNOGO: " + Message.NOGO + "\nCPR: " + Message.CPR + "\nLDPJ: " + Message.LDPJ + "\nRCF: " + Message.RCF;
                    }
                }

            }
            else { row["Target Report\nDescriptor"] = "No Data"; }
            if (Message.TN != null) { row["Track Number"] = Message.TN; }
            else { row["Track Number"] = "No Data"; }
            if (Message.SID != null) { row["Service Identification"] = Message.SID; }
            else { row["Service Identification"] = "No Data"; }
            if (Message.ToA_Position != null) { row["ToA for Position"] = Message.ToA_Position; }
            else { row["ToA for Position"] = "No Data"; }
            if (Message.Lat_WGS84 != null && Message.Lon_WGS84 != null) { row["Position in WGS-84\nCo-ordinates"] = Message.Lat_WGS84 + ", " + Message.Lon_WGS84; }
            else { row["Position in WGS-84\nCo-ordinates"] = "No Data"; }
            if (Message.Lat_WGS84_HP != null && Message.Lon_WGS84_HP != null) { row["Position in WGS-84\nCo-ordinates, High Precision"] = Message.Lat_WGS84_HP + ", " + Message.Lon_WGS84_HP; }
            else { row["Position in WGS-84\nCo-ordinates, High Precision"] = "No Data"; }
            if (Message.ToA_Velocity != null) { row["ToA for Velocity"] = Message.ToA_Velocity; }
            else { row["ToA for Velocity"] = "No Data"; }
            if (Message.Air_Speed != null) { row["Air Speed"] = Message.Air_Speed; }
            else { row["Air Speed"] = "No Data"; }
            if (Message.TAS != null) { row["True Airspeed"] = Message.TAS; }
            else { row["True Airspeed"] = "No Data"; }
            if (Message.Target_Address != null) { row["Target Address"] = Message.Target_Address; }
            else { row["Target Address"] = "No Data"; }
            if (Message.ToMR_Position != null) { row["Time of Message Reception\nfor Position"] = Message.ToMR_Position; }
            else { row["Time of Message Reception\nfor Position"] = "No Data"; }
            if (Message.ToMR_Velocity != null) { row["Time of Message Reception\nfor Velocity"] = Message.ToMR_Velocity; }
            else { row["Time of Message Reception\nfor Velocity"] = "No Data"; }
            if (Message.GH != null) { row["Geometric Height"] = Message.GH; }
            else { row["Geometric Height"] = "No Data"; }
            if (Message.NUCr_or_NACv != null)
            {
                row["Quality Indicators"] = "NUCr or NACv: " + Message.NUCr_or_NACv + "\nNUCp or NIC: " + Message.NUCp_or_NIC;
                if (Message.NIC_baro != null)
                {
                    row["Quality Indicators"] = "NUCr or NACv: " + Message.NUCr_or_NACv + "\nNUCp or NIC: " + Message.NUCp_or_NIC + "\nNIC bar: " + Message.NIC_baro + "\nSIL: " + Message.SIL + "\nNACp: " + Message.NACp;
                    if (Message.SIL_supplement != null)
                    {
                        row["Quality Indicators"] = "NUCr or NACv: " + Message.NUCr_or_NACv + "\nNUCp or NIC: " + Message.NUCp_or_NIC + "\nNIC bar: " + Message.NIC_baro + "\nSIL: " + Message.SIL + "\nNACp: " + Message.NACp + "\nSIL Supplement: " + Message.SIL_supplement + "\nSDA: " + Message.SDA + "\nGVA: " + Message.GVA;
                        if (Message.PIC != null)
                        {
                            row["Quality Indicators"] = "NUCr or NACv: " + Message.NUCr_or_NACv + "\nNUCp or NIC: " + Message.NUCp_or_NIC + "\nNIC bar: " + Message.NIC_baro + "\nSIL: " + Message.SIL + "\nNACp: " + Message.NACp + "\nSIL Supplement: " + Message.SIL_supplement + "\nSDA: " + Message.SDA + "\nGVA: " + Message.GVA + "\nPIC: " + Message.PIC;
                        }
                    }

                }
            }
            else { row["Quality Indicators"] = "No Data"; }
            if (Message.VNS != null) { row["MOPS Version"] = "MOPS Version: " + Message.VN + "\nTecnology Type: " + Message.LTT + "\nCompatibility: " + Message.VNS; }
            else { row["MOPS Version"] = "No Data"; }
            if (Message.Mode3_A_reply != null) { row["Mode-3A Code"] = Message.Mode3_A_reply; }
            else { row["Mode-3A Code"] = "No Data"; }
            if (Message.Roll != null) { row["Roll Angle"] = Message.Roll; }
            else { row["Roll Angle"] = "No Data"; }
            if (Message.FL != null) { row["Flight Level"] = Message.FL; }
            else { row["Flight Level"] = "No Data"; }
            if (Message.MH != null) { row["Magnetic Heading"] = Message.MH; }
            else { row["Magnetic Heading"] = "No Data"; }
            if (Message.ICF != null)
            {
                row["Target Status"] = "ICF: " + Message.ICF + "\nLNAV: " + Message.LNAV + "\nPS: " + Message.PS + "\nSS: " + Message.SS;
            }
            else { row["Target Status"] = "No Data"; }
            if (Message.BVR != null) { row["Barometric Vertical Rate"] = Message.BVR; }
            else { row["Barometric Vertical Rate"] = "No Data"; }
            if (Message.GVR != null) { row["Geometric Vertical Rate"] = Message.GVR; }
            else { row["Geometric Vertical Rate"] = "No Data"; }
            if (Message.RE_AGV != null) { row["Airborne Ground Vector"] = "[" + Message.GS + "; " + Message.TA + "]\n" + Message.RE_AGV; }
            else { row["Airborne Ground Vector"] = "No Data"; }
            if (Message.TAR != null) { row["Track Angle Rate"] = Message.TAR; }
            else { row["Track Angle Rate"] = "No Data"; }
            if (Message.ToART != null) { row["Time of Report Transmission"] = Message.ToART; }
            else { row["Time of Report Transmission"] = "No Data"; }
            if (Message.ECAT != null) { row["Emitter Category"] = Message.ECAT; }
            else { row["Emitter Category"] = "No Data"; }
            if (Message.Wind_Speed != null)
            {
                row["Met Information"] = "Wind Speed: " + Message.Wind_Speed + "\nWind Direction: " + Message.Wind_Direction + "\nTemperature: " + Message.Temperature + "\nTurbulence: " + Message.Turbulence;
            }
            else { row["Met Information"] = "No Data"; }
            if (Message.SAS != null) { row["Selected Altitude"] = Message.SAS + "\nSource: " + Message.Source + "\nAltitude: " + Message.Altitude; }
            else { row["Selected Altitude"] = "No Data"; }
            if (Message.MV != null)
            {
                row["Final State Selected Altitude"] = "Manage Vertical Mode: " + Message.MV + "\nAltitude Hold Mode: " + Message.AH + "\nApproach Mode: " + Message.AM + "\nFSS Altitude: " + Message.FSS_Altitude;
            }
            else { row["Final State Selected Altitude"] = "No Data"; }
            if (Message.TI_Points != null)
            {
                for (int i = 0; i < Message.TI_Points.Length; i++)
                {
                    row["Trajectory Intent"] = Message.TI_Points[i] + "\n";
                }
            }
            else { row["Trajectory Intent"] = "No Data"; }
            if (Message.RP != null) { row["Service Management"] = Message.RP; }
            else { row["Service Management"] = "No Data"; }
            if (Message.RA != null) { row["Aircraft Operational Status"] = "RA: " + Message.RA + "\nTC: " + Message.TC + "\nTS: " + Message.TS + "\nARV: " + Message.ARV + "\nCDTI/A: " + Message.CDTI_A + "\nnot TCAS: " + Message.not_TCAS + "\nSA: " + Message.SA; }
            else { row["Aircraft Operational Status"] = "No Data"; }
            if (Message.POA != null)
            {
                row["Surface Capabilities\nand Characteristics"] = "POA: " + Message.POA + "\nCDTI/S: " + Message.CDTI_S + "\nB2 low: " + Message.B2_LOW + "\nRAS: " + Message.RAS + "\nIDENT: " + Message.IDENT;
                if (Message.L_W != null)
                {
                    row["Surface Capabilities\nand Characteristics"] = "POA: " + Message.POA + "\nCDTI/S: " + Message.CDTI_S + "\nB2 low: " + Message.B2_LOW + "\nRAS: " + Message.RAS + "\nIDENT: " + Message.IDENT + "\nLenght/Width aircraft: " + Message.L_W;
                }
            }
            else { row["Surface Capabilities\nand Characteristics"] = "No Data"; }
            if (Message.MAM != null) { row["Message Amplitude"] = Message.MAM; }
            else { row["Message Amplitude"] = "No Data"; }
            row["Mode-S-MB Data"] = "No Data";
            if (Message.TYP != null) { row["ACAS Resolution Advisory\nReport"] = "TYP: " + Message.TYP + "\nSTYP: " + Message.STYP + "\nARA: " + Message.ARA + "\nRAC: " + Message.RAC + "\nRAT: " + Message.RAT + "\nMTE: " + Message.MTE + "\nTTI: " + Message.TTI + "\nTID: " + Message.TRID; }
            else { row["ACAS Resolution Advisory\nReport"] = "No Data"; }
            if (Message.RID != null) { row["Receiver ID"] = Message.RID; }
            else { row["Receiver ID"] = "No Data"; }
            if (Message.AOS_Age != null)
            {
                row["Data Ages"] = "AOS: " + Message.AOS_Age + "\nTRD: " + Message.TRD_Age + "\nM3A: " + Message.M3A_Age + "\nQI: " + Message.QI_Age + "\nTI: " + Message.TI_Age + "\nMAM: " + Message.MAM_Age + "\nGH: " + Message.GH_Age;
                if (Message.FL != null)
                {
                    row["Data Ages"] = "AOS: " + Message.AOS_Age + "\nTRD: " + Message.TRD_Age + "\nM3A: " + Message.M3A_Age + "\nQI: " + Message.QI_Age + "\nTI: " + Message.TI_Age + "\nMAM: " + Message.MAM_Age + "\nGH: " + Message.GH_Age + "\nFL: " + Message.FL_Age + "\nISA: " + Message.ISA_Age + "\nFSA: " + Message.FSA_Age + "\nAS: " + Message.AS_Age + "\nTAS: " + Message.TAS_Age + "\nMH: " + Message.MH_Age + "\nBVR: " + Message.BVR_Age;
                    if (Message.GVR != null)
                    {
                        row["Data Ages"] = "AOS: " + Message.AOS_Age + "\nTRD: " + Message.TRD_Age + "\nM3A: " + Message.M3A_Age + "\nQI: " + Message.QI_Age + "\nTI: " + Message.TI_Age + "\nMAM: " + Message.MAM_Age + "\nGH: " + Message.GH_Age + "\nFL: " + Message.FL_Age + "\nISA: " + Message.ISA_Age + "\nFSA: " + Message.FSA_Age + "\nAS: " + Message.AS_Age + "\nTAS: " + Message.TAS_Age + "\nMH: " + Message.MH_Age + "\nBVR: " + Message.BVR_Age + "\nGVR: " + Message.GVR_Age + "\nGV: " + Message.GV_Age + "\nTAR: " + Message.TAR_Age + "\nTI: " + Message.TI_Age + "\nTS: " + Message.TS_Age + "\nMET: " + Message.MET_Age + "\nROA: " + Message.ROA_Age;
                        if (Message.ARA_Age != null)
                        {
                            row["Data Ages"] = "AOS: " + Message.AOS_Age + "\nTRD: " + Message.TRD_Age + "\nM3A: " + Message.M3A_Age + "\nQI: " + Message.QI_Age + "\nTI: " + Message.TI_Age + "\nMAM: " + Message.MAM_Age + "\nGH: " + Message.GH_Age + "\nFL: " + Message.FL_Age + "\nISA: " + Message.ISA_Age + "\nFSA: " + Message.FSA_Age + "\nAS: " + Message.AS_Age + "\nTAS: " + Message.TAS_Age + "\nMH: " + Message.MH_Age + "\nBVR: " + Message.BVR_Age + "\nGVR: " + Message.GVR_Age + "\nGV: " + Message.GV_Age + "\nTAR: " + Message.TAR_Age + "\nTI: " + Message.TI_Age + "\nTS: " + Message.TS_Age + "\nMET: " + Message.MET_Age + "\nROA: " + Message.ROA_Age + "\nARA: " + Message.ARA_Age + "\nSCC: " + Message.SCC_Age;
                        }
                    }
                }
            }

            else { row["Data Ages"] = "No Data"; }
            CAT21_Table.Rows.Add(row);


        }

        private void Set_Data_Table_Columns_CATAll(DataTable T)
        {
            //CAT ALL --------------------------------------------------------------------
            T.Columns.Add("Number", typeof(Int32));
            T.Columns.Add("Category", typeof(String));
            T.Columns.Add("SAC", typeof(String));
            T.Columns.Add("SIC", typeof(String));
            T.Columns.Add("TargetID", typeof(String));
            T.Columns.Add("Track Number", typeof(String));
            T.Columns.Add("Target Report\nDescriptor", typeof(String));
            T.Columns.Add("Flight Level", typeof(String));
            T.Columns.Add("Position in WGS-84\nCo-ordinates", typeof(String));
            T.Columns.Add("Mode-3A Code", typeof(String));
            T.Columns.Add("Mode-S-MB Data", typeof(String));
            T.Columns.Add("Target Address", typeof(String));
            //CAT 10 --------------------------------------------------------------------
            T.Columns.Add("Message Type", typeof(String));
            T.Columns.Add("Time of Day", typeof(String));
            T.Columns.Add("Track Status", typeof(String));
            T.Columns.Add("Position in Cartesian\nCo-ordinates", typeof(String));
            T.Columns.Add("Position in Polar\nCo-ordinates", typeof(String));
            T.Columns.Add("Track velocity in\npolar Co-ordinates", typeof(String));
            T.Columns.Add("Track velocity in\nCartesian Co-ordinates", typeof(String));
            T.Columns.Add("Target size and\norientation", typeof(String));
            T.Columns.Add("System Status", typeof(String));
            T.Columns.Add("Vehicle fleet ID", typeof(String));
            T.Columns.Add("Pre-programmed\nMessage", typeof(String));
            T.Columns.Add("Measured Height", typeof(String));
            T.Columns.Add("Standard Deviation\nof Position", typeof(String));
            T.Columns.Add("Presence", typeof(String));
            T.Columns.Add("Amplitude of\nPrimary Plot", typeof(String));
            T.Columns.Add("Calculated\nAcceleration", typeof(String));
            //CAT 21 --------------------------------------------------------------------
            T.Columns.Add("Aircraft Operational Status", typeof(String));
            T.Columns.Add("Service Identification", typeof(String));
            T.Columns.Add("Service Management", typeof(String));
            T.Columns.Add("Emitter Category", typeof(String));
            T.Columns.Add("ToA for Position", typeof(String));
            T.Columns.Add("ToA for Velocity", typeof(String));
            T.Columns.Add("Time of Message Reception\nfor Position", typeof(String));
            T.Columns.Add("Time of Message Reception\nfor Position - High Precision", typeof(String));
            T.Columns.Add("Time of Message Reception\nfor Velocity", typeof(String));
            T.Columns.Add("Time of Message Reception\nfor Velocity - High Precision", typeof(String));
            T.Columns.Add("Time of Report Transmission", typeof(String));
            T.Columns.Add("Quality Indicators", typeof(String));
            T.Columns.Add("Trajectory Intent", typeof(String));
            T.Columns.Add("Position in WGS-84\nCo-ordinates, High Precision", typeof(String));
            T.Columns.Add("Message Amplitude", typeof(String));
            T.Columns.Add("Geometric Height", typeof(String));
            T.Columns.Add("Selected Altitude", typeof(String));
            T.Columns.Add("Final State Selected Altitude", typeof(String));
            T.Columns.Add("Air Speed", typeof(String));
            T.Columns.Add("True Airspeed", typeof(String));
            T.Columns.Add("Magnetic Heading", typeof(String));
            T.Columns.Add("Barometric Vertical Rate", typeof(String));
            T.Columns.Add("Geometric Vertical Rate", typeof(String));
            T.Columns.Add("Airborne Ground Vector", typeof(String));
            T.Columns.Add("Track Angle Rate", typeof(String));
            T.Columns.Add("Target Status", typeof(String));
            T.Columns.Add("MOPS Version", typeof(String));
            T.Columns.Add("Met Information", typeof(String));
            T.Columns.Add("Roll Angle", typeof(String));
            T.Columns.Add("ACAS Resolution Advisory\nReport", typeof(String));
            T.Columns.Add("Surface Capabilities\nand Characteristics", typeof(String));
            T.Columns.Add("Data Ages", typeof(String));
            T.Columns.Add("Receiver ID", typeof(String));
        }

        private void Add_Row_Message_CATAll(CAT10 Message)
        {
            var row = CATAll_Table.NewRow();
            row["Number"] = Message.Num_Message;
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
            if (Message.TYP != null) { row["Target Report\nDescriptor"] = "TYP: " + Message.TYP + "\nDCR: " + Message.DCR + "\nCHN: " + Message.CHN + "\nGBS: " + Message.GBS + "\nCRT: " + Message.CRT; }
            else { row["Target Report\nDescriptor"] = "No Data"; }
            if (Message.Message_type != null) { row["Message Type"] = Message.Message_type; }
            else { row["Message Type"] = "No Data"; }
            if (Message.FL != null) { row["Flight Level"] = Message.FL; }
            else { row["Flight Level"] = "No Data"; }
            if (Message.Track_Number != null) { row["Track Number"] = Message.Track_Number; }
            else { row["Track Number"] = "No Data"; }
            if (Message.ToD != null) { row["Time of Day"] = Message.ToD; }
            else { row["Time of Day"] = "No Data"; }
            if (Message.CNF != null)
            {
                row["Track Status"] = "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH;
                if (Message.TOM != null && Message.DOU != null && Message.MRS != null)
                {
                    row["Track Status"] = "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH + "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH + "\nTOM: " + Message.TOM + "\nDOU: " + Message.MRS;
                    if (Message.GHO != null)
                    {
                        row["Track Status"] = "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH + "CNF: " + Message.CNF + "\nTRE: " + Message.TRE + "\nCST: " + Message.CST + "\nMAH: " + Message.MAH + "\nTCC: " + Message.TCC + "\nSTH: " + Message.STH + "\nTOM: " + Message.TOM + "\nDOU: " + Message.MRS + "\nGHO: " + Message.GHO;
                    }
                }
            }
            else { row["Track Status"] = "No Data"; }
            if (Message.Lat_WGS84 != null && Message.Lon_WGS84 != null) { row["Position in WGS-84\nCo-ordinates"] = "Lat: " + Message.Lat_WGS84 + "\nLon: " + Message.Lon_WGS84; }
            else { row["Position in WGS-84\nCo-ordinates"] = "No Data"; }
            if (Message.X_Component != null && Message.Y_Component != null) { row["Position in Cartesian\nCo-ordinates"] = "X: " + Message.X_Component + "m" + "\nY:" + Message.Y_Component + "m"; }
            else { row["Position in Cartesian\nCo-ordinates"] = "No Data"; }
            if (Message.RHO != null && Message.THETA != null) { row["Position in Polar\nCo-ordinates"] = "RHO: " + Message.RHO + "º" + "\nTHETA: " + Message.THETA + "º"; }
            else { row["Position in Polar\nCo-ordinates"] = "No Data"; }
            if (Message.Ground_Speed != null && Message.Track_Angle != null) { row["Track velocity in\nCartesian Co-ordinates"] = "Gs: " + Message.Ground_Speed + "kt" + "\nTa: " + Message.Track_Angle + "º"; }
            else { row["Track velocity in\nCartesian Co-ordinates"] = "No Data"; }
            if (Message.Vx != null && Message.Vy != null) { row["Track velocity in\nCartesian Co-ordinates"] = "Vx: " + Message.Vx + "m/s" + "\nVy: " + Message.Vy + "m/s"; }
            else { row["Track velocity in\nCartesian Co-ordinates"] = "No Data"; }
            if (Message.Lenght != null && Message.Orientation != null && Message.Width != null) { row["Target size and\norientation"] = "Lenght: " + Message.Lenght + "\nOrientation: " + Message.Orientation + "\nWidth: " + Message.Width; }
            else { row["Target size and\norientation"] = "No Data"; }

            if (Message.Target_Address != null) { row["Target Address"] = Message.Target_Address; }
            else { row["Target Address"] = "No Data"; }
            if (Message.NOGO != null && Message.OVL != null && Message.TSV != null && Message.DIV != null && Message.TTF != null) { row["System Status"] = "NOGO: " + Message.NOGO + "\nOVL: " + Message.OVL + "\nTSV: " + Message.TSV + "\nDIV: " + Message.DIV + "\nTTF: " + Message.TTF; }
            else { row["System Status"] = "No Data"; }
            if (Message.VFI != null) { row["Vehicle fleet ID"] = Message.VFI; }
            else { row["Vehicle fleet ID"] = "No Data"; }
            if (Message.TRB != null && Message.MSG != null) { row["Pre-programmed\nMessage"] = "TRB: " + Message.TRB + "\nMSG: " + Message.MSG; }
            else { row["Pre-programmed\nMessage"] = "No Data"; }
            if (Message.Measured_Height != null) { row["Measured Height"] = Message.Measured_Height; }
            else { row["Measured Height"] = "No Data"; }
            if (Message.Mode3_A_reply != null && Message.V != null && Message.G != null && Message.L != null) { row["Mode-3A Code"] = "3/A reply: " + Message.Mode3_A_reply + "\nV: " + Message.V + "\nG: " + Message.G + "\nL: " + Message.L; }
            else { row["Mode-3A Code"] = "No Data"; }
            if (Message.DataList != null) { row["Mode-S-MB Data"] = "No data"; }
            else { row["Mode-S-MB Data"] = "No Data"; }
            if (Message.Ox != null && Message.Oy != null && Message.Oxy != null) { row["Standard Deviation\nof Position"] = "Ox: " + Message.Ox + "m" + "\nOy: " + Message.Oy + "m" + "\nOxy: " + Message.Oxy + "m"; }
            else { row["Standard Deviation\nof Position"] = "No Data"; }
            if (Message.REP != null && Message.DRHO != null && Message.DTHETA != null) { row["Presence"] = "REP: " + Message.REP + "\nDRHO: " + Message.DRHO + "m" + "\nDTHETA: " + Message.DTHETA + "º"; }
            else { row["Presence"] = "No Data"; }
            if (Message.PAM != null) { row["Amplitude of\nPrimary Plot"] = Message.PAM; }
            else { row["Amplitude of\nPrimary Plot"] = "No Data"; }
            if (Message.Ax != null && Message.Ay != null) { row["Calculated\nAcceleration"] = "Ax: " + Message.Ax + "m/s\xB2" + "\nAy: " + Message.Ay + "m/s\xB2"; }
            else { row["Calculated\nAcceleration"] = "No Data"; }

            CATAll_Table.Rows.Add(row);
        }

        private void Add_Row_Message_CATAll(CAT21 Message)
        {
            var row = CAT21_Table.NewRow();
            row["Number"] = Message.Num_Message;
            if (Message.CAT != null) { row["Category"] = Message.CAT; }
            else { row["Category"] = "No Data"; }
            if (Message.SAC != null) { row["SAC"] = Message.SAC; }
            else { row["SAC"] = "No Data"; }
            if (Message.SIC != null) { row["SIC"] = Message.SIC; }
            else { row["SIC"] = "No Data"; }
            if (Message.Target_Identification != null) { row["TargetID"] = Message.Target_Identification; }
            else { row["TargetID"] = "No Data"; }
            if (Message.ATP != null)
            {
                row["Target Report\nDescriptor"] = "ATP: " + Message.ATP + "\nARC: " + Message.ARC + "\nRC: " + Message.RC + "\nRAB: " + Message.RAB;
                if (Message.DCR != null)
                {
                    row["Target Report\nDescriptor"] = "ATP: " + Message.ATP + "\nARC: " + Message.ARC + "\nRC: " + Message.RC + "\nRAB: " + Message.RAB + "\nDCR: " + Message.DCR + "\nGBS: " + Message.GBS + "\nSIM: " + Message.SIM + "\nTST: " + Message.TST + "\nSAA: " + Message.SAA + "\nCL: " + Message.CL;
                    if (Message.IPC != null)
                    {
                        row["Target Report\nDescriptor"] = "ATP: " + Message.ATP + "\nARC: " + Message.ARC + "\nRC: " + Message.RC + "\nRAB: " + Message.RAB + "\nDCR: " + Message.DCR + "\nGBS: " + Message.GBS + "\nSIM: " + Message.SIM + "\nTST: " + Message.TST + "\nSAA: " + Message.SAA + "\nCL: " + Message.CL + "\nIPC: " + Message.IPC + "\nNOGO: " + Message.NOGO + "\nCPR: " + Message.CPR + "\nLDPJ: " + Message.LDPJ + "\nRCF: " + Message.RCF;
                    }
                }

            }
            else { row["Target Report\nDescriptor"] = "No Data"; }
            if (Message.TN != null) { row["Track Number"] = Message.TN; }
            else { row["Track Number"] = "No Data"; }
            if (Message.SID != null) { row["Service Identification"] = Message.SID; }
            else { row["Service Identification"] = "No Data"; }
            if (Message.ToA_Position != null) { row["ToA for Position"] = Message.ToA_Position; }
            else { row["ToA for Position"] = "No Data"; }
            if (Message.Lat_WGS84 != null && Message.Lon_WGS84 != null) { row["Position in WGS-84\nCo-ordinates"] = Message.Lat_WGS84 + ", " + Message.Lon_WGS84; }
            else { row["Position in WGS-84\nCo-ordinates"] = "No Data"; }
            if (Message.Lat_WGS84_HP != null && Message.Lon_WGS84_HP != null) { row["Position in WGS-84\nCo-ordinates, High Precision"] = Message.Lat_WGS84_HP + ", " + Message.Lon_WGS84_HP; }
            else { row["Position in WGS-84\nCo-ordinates, High Precision"] = "No Data"; }
            if (Message.ToA_Velocity != null) { row["ToA for Velocity"] = Message.ToA_Velocity; }
            else { row["ToA for Velocity"] = "No Data"; }
            if (Message.Air_Speed != null) { row["Air Speed"] = Message.Air_Speed; }
            else { row["Air Speed"] = "No Data"; }
            if (Message.TAS != null) { row["True Airspeed"] = Message.TAS; }
            else { row["True Airspeed"] = "No Data"; }
            if (Message.Target_Address != null) { row["Target Address"] = Message.Target_Address; }
            else { row["Target Address"] = "No Data"; }
            if (Message.ToMR_Position != null) { row["Time of Message Reception\nfor Position"] = Message.ToMR_Position; }
            else { row["Time of Message Reception\nfor Position"] = "No Data"; }
            if (Message.ToMR_Velocity != null) { row["Time of Message Reception\nfor Velocity"] = Message.ToMR_Velocity; }
            else { row["Time of Message Reception\nfor Velocity"] = "No Data"; }
            if (Message.GH != null) { row["Geometric Height"] = Message.GH; }
            else { row["Geometric Height"] = "No Data"; }
            if (Message.NUCr_or_NACv != null)
            {
                row["Quality Indicators"] = "NUCr or NACv: " + Message.NUCr_or_NACv + "\nNUCp or NIC: " + Message.NUCp_or_NIC;
                if (Message.NIC_baro != null)
                {
                    row["Quality Indicators"] = "NUCr or NACv: " + Message.NUCr_or_NACv + "\nNUCp or NIC: " + Message.NUCp_or_NIC + "\nNIC bar: " + Message.NIC_baro + "\nSIL: " + Message.SIL + "\nNACp: " + Message.NACp;
                    if (Message.SIL_supplement != null)
                    {
                        row["Quality Indicators"] = "NUCr or NACv: " + Message.NUCr_or_NACv + "\nNUCp or NIC: " + Message.NUCp_or_NIC + "\nNIC bar: " + Message.NIC_baro + "\nSIL: " + Message.SIL + "\nNACp: " + Message.NACp + "\nSIL Supplement: " + Message.SIL_supplement + "\nSDA: " + Message.SDA + "\nGVA: " + Message.GVA;
                        if (Message.PIC != null)
                        {
                            row["Quality Indicators"] = "NUCr or NACv: " + Message.NUCr_or_NACv + "\nNUCp or NIC: " + Message.NUCp_or_NIC + "\nNIC bar: " + Message.NIC_baro + "\nSIL: " + Message.SIL + "\nNACp: " + Message.NACp + "\nSIL Supplement: " + Message.SIL_supplement + "\nSDA: " + Message.SDA + "\nGVA: " + Message.GVA + "\nPIC: " + Message.PIC;
                        }
                    }

                }
            }
            else { row["Quality Indicators"] = "No Data"; }
            if (Message.VNS != null) { row["MOPS Version"] = "MOPS Version: " + Message.VN + "\nTecnology Type: " + Message.LTT + "\nCompatibility: " + Message.VNS; }
            else { row["MOPS Version"] = "No Data"; }
            if (Message.Mode3_A_reply != null) { row["Mode-3A Code"] = Message.Mode3_A_reply; }
            else { row["Mode-3A Code"] = "No Data"; }
            if (Message.Roll != null) { row["Roll Angle"] = Message.Roll; }
            else { row["Roll Angle"] = "No Data"; }
            if (Message.FL != null) { row["Flight Level"] = Message.FL; }
            else { row["Flight Level"] = "No Data"; }
            if (Message.MH != null) { row["Magnetic Heading"] = Message.MH; }
            else { row["Magnetic Heading"] = "No Data"; }
            if (Message.ICF != null)
            {
                row["Target Status"] = "ICF: " + Message.ICF + "\nLNAV: " + Message.LNAV + "\nPS: " + Message.PS + "\nSS: " + Message.SS;
            }
            else { row["Target Status"] = "No Data"; }
            if (Message.BVR != null) { row["Barometric Vertical Rate"] = Message.BVR; }
            else { row["Barometric Vertical Rate"] = "No Data"; }
            if (Message.GVR != null) { row["Geometric Vertical Rate"] = Message.GVR; }
            else { row["Geometric Vertical Rate"] = "No Data"; }
            if (Message.RE_AGV != null) { row["Airborne Ground Vector"] = "[" + Message.GS + "; " + Message.TA + "]\n" + Message.RE_AGV; }
            else { row["Airborne Ground Vector"] = "No Data"; }
            if (Message.TAR != null) { row["Track Angle Rate"] = Message.TAR; }
            else { row["Track Angle Rate"] = "No Data"; }
            if (Message.ToART != null) { row["Time of Report Transmission"] = Message.ToART; }
            else { row["Time of Report Transmission"] = "No Data"; }
            if (Message.ECAT != null) { row["Emitter Category"] = Message.ECAT; }
            else { row["Emitter Category"] = "No Data"; }
            if (Message.Wind_Speed != null)
            {
                row["Met Information"] = "Wind Speed: " + Message.Wind_Speed + "\nWind Direction: " + Message.Wind_Direction + "\nTemperature: " + Message.Temperature + "\nTurbulence: " + Message.Turbulence;
            }
            else { row["Met Information"] = "No Data"; }
            if (Message.SAS != null) { row["Selected Altitude"] = Message.SAS + "\nSource: " + Message.Source + "\nAltitude: " + Message.Altitude; }
            else { row["Selected Altitude"] = "No Data"; }
            if (Message.MV != null)
            {
                row["Final State Selected Altitude"] = "Manage Vertical Mode: " + Message.MV + "\nAltitude Hold Mode: " + Message.AH + "\nApproach Mode: " + Message.AM + "\nFSS Altitude: " + Message.FSS_Altitude;
            }
            else { row["Final State Selected Altitude"] = "No Data"; }
            if (Message.TI_Points != null)
            {
                for (int i = 0; i < Message.TI_Points.Length; i++)
                {
                    row["Trajectory Intent"] = Message.TI_Points[i] + "\n";
                }
            }
            else { row["Trajectory Intent"] = "No Data"; }
            if (Message.RP != null) { row["Service Management"] = Message.RP; }
            else { row["Service Management"] = "No Data"; }
            if (Message.RA != null) { row["Aircraft Operational Status"] = "RA: " + Message.RA + "\nTC: " + Message.TC + "\nTS: " + Message.TS + "\nARV: " + Message.ARV + "\nCDTI/A: " + Message.CDTI_A + "\nnot TCAS: " + Message.not_TCAS + "\nSA: " + Message.SA; }
            else { row["Aircraft Operational Status"] = "No Data"; }
            if (Message.POA != null)
            {
                row["Surface Capabilities\nand Characteristics"] = "POA: " + Message.POA + "\nCDTI/S: " + Message.CDTI_S + "\nB2 low: " + Message.B2_LOW + "\nRAS: " + Message.RAS + "\nIDENT: " + Message.IDENT;
                if (Message.L_W != null)
                {
                    row["Surface Capabilities\nand Characteristics"] = "POA: " + Message.POA + "\nCDTI/S: " + Message.CDTI_S + "\nB2 low: " + Message.B2_LOW + "\nRAS: " + Message.RAS + "\nIDENT: " + Message.IDENT + "\nLenght/Width aircraft: " + Message.L_W;
                }
            }
            else { row["Surface Capabilities\nand Characteristics"] = "No Data"; }
            if (Message.MAM != null) { row["Message Amplitude"] = Message.MAM; }
            else { row["Message Amplitude"] = "No Data"; }
            row["Mode-S-MB Data"] = "No Data";
            if (Message.TYP != null) { row["ACAS Resolution Advisory\nReport"] = "TYP: " + Message.TYP + "\nSTYP: " + Message.STYP + "\nARA: " + Message.ARA + "\nRAC: " + Message.RAC + "\nRAT: " + Message.RAT + "\nMTE: " + Message.MTE + "\nTTI: " + Message.TTI + "\nTID: " + Message.TRID; }
            else { row["ACAS Resolution Advisory\nReport"] = "No Data"; }
            if (Message.RID != null) { row["Receiver ID"] = Message.RID; }
            else { row["Receiver ID"] = "No Data"; }
            if (Message.AOS_Age != null)
            {
                row["Data Ages"] = "AOS: " + Message.AOS_Age + "\nTRD: " + Message.TRD_Age + "\nM3A: " + Message.M3A_Age + "\nQI: " + Message.QI_Age + "\nTI: " + Message.TI_Age + "\nMAM: " + Message.MAM_Age + "\nGH: " + Message.GH_Age;
                if (Message.FL != null)
                {
                    row["Data Ages"] = "AOS: " + Message.AOS_Age + "\nTRD: " + Message.TRD_Age + "\nM3A: " + Message.M3A_Age + "\nQI: " + Message.QI_Age + "\nTI: " + Message.TI_Age + "\nMAM: " + Message.MAM_Age + "\nGH: " + Message.GH_Age + "\nFL: " + Message.FL_Age + "\nISA: " + Message.ISA_Age + "\nFSA: " + Message.FSA_Age + "\nAS: " + Message.AS_Age + "\nTAS: " + Message.TAS_Age + "\nMH: " + Message.MH_Age + "\nBVR: " + Message.BVR_Age;
                    if (Message.GVR != null)
                    {
                        row["Data Ages"] = "AOS: " + Message.AOS_Age + "\nTRD: " + Message.TRD_Age + "\nM3A: " + Message.M3A_Age + "\nQI: " + Message.QI_Age + "\nTI: " + Message.TI_Age + "\nMAM: " + Message.MAM_Age + "\nGH: " + Message.GH_Age + "\nFL: " + Message.FL_Age + "\nISA: " + Message.ISA_Age + "\nFSA: " + Message.FSA_Age + "\nAS: " + Message.AS_Age + "\nTAS: " + Message.TAS_Age + "\nMH: " + Message.MH_Age + "\nBVR: " + Message.BVR_Age + "\nGVR: " + Message.GVR_Age + "\nGV: " + Message.GV_Age + "\nTAR: " + Message.TAR_Age + "\nTI: " + Message.TI_Age + "\nTS: " + Message.TS_Age + "\nMET: " + Message.MET_Age + "\nROA: " + Message.ROA_Age;
                        if (Message.ARA_Age != null)
                        {
                            row["Data Ages"] = "AOS: " + Message.AOS_Age + "\nTRD: " + Message.TRD_Age + "\nM3A: " + Message.M3A_Age + "\nQI: " + Message.QI_Age + "\nTI: " + Message.TI_Age + "\nMAM: " + Message.MAM_Age + "\nGH: " + Message.GH_Age + "\nFL: " + Message.FL_Age + "\nISA: " + Message.ISA_Age + "\nFSA: " + Message.FSA_Age + "\nAS: " + Message.AS_Age + "\nTAS: " + Message.TAS_Age + "\nMH: " + Message.MH_Age + "\nBVR: " + Message.BVR_Age + "\nGVR: " + Message.GVR_Age + "\nGV: " + Message.GV_Age + "\nTAR: " + Message.TAR_Age + "\nTI: " + Message.TI_Age + "\nTS: " + Message.TS_Age + "\nMET: " + Message.MET_Age + "\nROA: " + Message.ROA_Age + "\nARA: " + Message.ARA_Age + "\nSCC: " + Message.SCC_Age;
                        }
                    }
                }
            }

            else { row["Data Ages"] = "No Data"; }
            CAT21_Table.Rows.Add(row);


        }
    }
}

