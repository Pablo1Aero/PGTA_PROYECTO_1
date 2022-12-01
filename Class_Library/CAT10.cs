using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary;
using Class_Library;

namespace Class_Library
{
    public class CAT10
    {
        public string[] FSPEC_Return;

        public string[] CAT10_Message;   //Objeto tipo lista de bytes (mensage CAT10)
        readonly DecodeLibrary Library;
        public CAT10(string[] CAT10_Message, DecodeLibrary Library)
        {
            this.CAT10_Message = CAT10_Message;
            this.Library = Library;


            FSPEC_Return = Library.obtainFSPEC(CAT10_Message);
            int Position = Convert.ToInt32(FSPEC_Return[0])+1;
            string FSPEC = FSPEC_Return[1];
            {
                if (FSPEC[0] == '1') { Position = this.Decode_Data_Source_Identifier(Position, CAT10_Message); }
                if (FSPEC[1] == '1') { Position = this.Decode_MessageType(Position, CAT10_Message); }
                if (FSPEC[2] == '1') { Position = this.Decode_Target_Report_Descriptor(Position, CAT10_Message); }
                if (FSPEC[3] == '1') { Position = this.Decode_Measured_Position_Polar(Position, CAT10_Message); }
                if (FSPEC[4] == '1') { Position = this.Decode_Position_WGS84(Position, CAT10_Message); }
                if (FSPEC[5] == '1') { Position = this.Decode_Cartesian_Position(Position, CAT10_Message); }
                if (FSPEC[6] == '1') { Position = this.Decode_Mode_3A(Position, CAT10_Message); }
            }
        }


        # region Item I010/000 Message Type
        public string Message_type;
        private int Decode_MessageType(int Position, string[] CAT10_Message)
        {   
            int Message_Type_byte_Int = Convert.ToInt32(CAT10_Message[Position]);
            if (Message_Type_byte_Int == 1) { Message_type = "Target Report"; }
            if (Message_Type_byte_Int == 2) { Message_type = "Start of Update Cycle"; }
            if (Message_Type_byte_Int == 3) { Message_type = "Periodic Status Message"; }
            if (Message_Type_byte_Int == 4) { Message_type = "Event-triggered Status Message"; }
            Position++;

            return Position;
        }
        #endregion

        #region Item I010/010 Data Source Identifier
        public string SIC;
        public string SAC;
        private int Decode_Data_Source_Identifier(int Position, string[] CAT10_Message)
        {
            int Data_Source_Identifier_byte_SAC = Convert.ToInt32(CAT10_Message[Position], 2);
            SAC = Convert.ToString(Data_Source_Identifier_byte_SAC);
            Position++;
            int Data_Source_Identifier_byte_SIC = Convert.ToInt32(CAT10_Message[Position], 2);
            SIC = Convert.ToString(Data_Source_Identifier_byte_SIC);
            Position++;

            return Position;
        }
        #endregion

        #region Item I010/020  Target Report Descriptor
        //Main structure data
        public string TYP; public string DCR; public string CHN; public string GBS; public string CRT;
        //First octet data
        public string SIM; public string TST; public string RAB; public string LOP; public string TOT;
        //Second octet data
        public string SPI;

        private int Decode_Target_Report_Descriptor(int Position, string[] CAT10_Message)
        {
            
            string Main_structure_byte = CAT10_Message[Position];
            string TYP_bin = Convert.ToString(Convert.ToString(Main_structure_byte[0]) + Convert.ToString(Main_structure_byte[1]) + Convert.ToString(Main_structure_byte[2]));
            if (TYP_bin == "000") { TYP = "SSR Multilateration"; }
            if (TYP_bin == "001") { TYP = "Mode S Multilateration"; }
            if (TYP_bin == "010") { TYP = "ADS-B"; }
            if (TYP_bin == "011") { TYP = "PSR"; }
            if (TYP_bin == "100") { TYP = "Magnetic Loop System"; }
            if (TYP_bin == "101") { TYP = "HF multilateration"; }
            if (TYP_bin == "110") { TYP = "Not defined"; }
            if (TYP_bin == "111") { TYP = "Other types"; }

            string DCR_bin = Convert.ToString(CAT10_Message[Position][3]);
            if (DCR_bin == "0") { DCR = "No differential correction (ADS-B)"; }
            if (DCR_bin == "1") { DCR = "Differential correction (ADS-B)"; }

            string CHN_bin = Convert.ToString(CAT10_Message[Position][4]);
            if (DCR_bin == "0") { CHN = "Chain 1"; }
            if (DCR_bin == "1") { CHN = "Chain 2"; }

            string GBS_bin = Convert.ToString(CAT10_Message[Position][5]);
            if (DCR_bin == "0") { GBS = "Transponder Ground bit not set"; }
            if (DCR_bin == "1") { GBS = "Transponder Ground bit set"; }

            string CRT_bin = Convert.ToString(CAT10_Message[Position][6]);
            if (DCR_bin == "0") { CRT = "No Corrupted reply in multilateration"; }
            if (DCR_bin == "1") { CRT = "Corrupted replies in multilateration"; }

            Position++;
            while (CAT10_Message[Position-1].Last() == 1)
            {
                string SIM_bin = Convert.ToString(CAT10_Message[Position][0]);
                if (SIM_bin == "0") { SIM = "Actual target report"; }
                if (SIM_bin == "1") { SIM = "Simulated target report"; }

                string TST_bin = Convert.ToString(CAT10_Message[Position][1]);
                if (TST_bin == "0") { SIM = "Default"; }
                if (TST_bin == "1") { SIM = "Test target"; }

                string RAB_bin = Convert.ToString(CAT10_Message[Position][2]);
                if (RAB_bin == "0") { SIM = "Report from target transponder"; }
                if (RAB_bin == "1") { SIM = "Report from field monitor (fixed transponder)"; }

                string LOP_bin = Convert.ToString(Convert.ToString(CAT10_Message[Position][3]) + Convert.ToString(CAT10_Message[Position][4])) ;
                if (LOP_bin == "00") { LOP = "Undetermined"; }
                if (LOP_bin == "01") { LOP = "Loop start"; }
                if (LOP_bin == "10") { LOP = "Loope finish"; }

                string TOT_bin = Convert.ToString(Convert.ToString(CAT10_Message[Position][5]) + Convert.ToString(CAT10_Message[Position][6]));
                if (TOT_bin == "00") { LOP = "Undetermined"; }
                if (TOT_bin == "01") { LOP = "Aircraft"; }
                if (TOT_bin == "10") { LOP = "Ground vehicle"; }
                if (TOT_bin == "11") { LOP = "Helicopter"; }

                Position++;

                if (CAT10_Message[Position - 1].Last() == 1)
                {
                    string SPI_bin = Convert.ToString(CAT10_Message[Position][0]);
                    if (SPI_bin == "0") { SIM = "Absence of SPI"; }
                    if (SIM_bin == "1") { SIM = "Special position identification"; }
                }
            }
            
            return Position;
        }
        #endregion

        #region Item I010/040   Measured Position in Polar Coordinates
        
        public string RHO; public string THETA;

        private int Decode_Measured_Position_Polar(int Position, string[] CAT10_Message)
        {
            int Measured_Rho = Convert.ToInt32(Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]), 2);
            RHO = Convert.ToString(Measured_Rho);
            Position = Position ++;
            double Measured_Theta = Convert.ToDouble(Convert.ToInt32(Convert.ToString(CAT10_Message[Position + 1] + CAT10_Message[Position + 2]), 2)) * 0.0055;//(360/(Math.Pow(2, 16)));
            THETA = Convert.ToString(Measured_Theta);
            Position = Position + 3;

            return Position;
        }

        #endregion

        #region Item I010/041    Position in WGS-84 Coordinates

        public string Lat_WGS84; public string Lon_WGS84;


        private int Decode_Position_WGS84(int Position, string[] CAT10_Message)
        {
            string Lat_WGS84_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1] + CAT10_Message[Position + 2] + CAT10_Message[Position + 3]);
            Position = Position + 4;
            string Lon_WGS84_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1] + CAT10_Message[Position + 2] + CAT10_Message[Position + 3]);
            Position = Position + 4;

            if (Lat_WGS84_bin[0] == '0') { Lat_WGS84 = Convert.ToString(Convert.ToInt32(Lat_WGS84_bin)); }
            if (Lat_WGS84_bin[0] == '1')
            {
                Lat_WGS84 = Convert.ToString(Library.twos_complement(Lat_WGS84_bin) * (180/Math.Pow(2, 31)));
            }
            if (Lon_WGS84_bin[0] == '0') { Lon_WGS84 = Convert.ToString(Convert.ToInt32(Lon_WGS84_bin)); }
            if (Lon_WGS84_bin[0] == '1')
            {
                Lon_WGS84 = Convert.ToString(Library.twos_complement(Lon_WGS84_bin) * (180/Math.Pow(2, 31)));
            }
            return Position;
        }

        #endregion

        #region Item I010/042   Position in Cartesian Co-ordinates
        
        public string X_Component; public string Y_Component;

        private int Decode_Cartesian_Position(int Position, string[] CAT10_Message)
        {
            string X_Component_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]);
            Position = Position + 2;
            string Y_Component_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]);
            Position = Position + 2;

            //LSB falta implementar

            return Position;
        }

        #endregion

        # region Item I010/060  Mode-3/A Code in Octal Representation

        string V; string G; string L; string Mode3_A_reply;
        private int Decode_Mode_3A(int Position, string[] CAT10_Message)
        {
            int Mode_3A_byte_Int = Convert.ToInt32(CAT10_Message[Position]);
            if (Convert.ToString(CAT10_Message[Position][0]) == "0") { V = "Code validated"; }
            if (Convert.ToString(CAT10_Message[Position][0]) == "1") { V = "Code not validated"; }
            if (Convert.ToString(CAT10_Message[Position][1]) == "0") { G = "Default"; }
            if (Convert.ToString(CAT10_Message[Position][1]) == "1") { G = "Garlbed code"; }
            if (Convert.ToString(CAT10_Message[Position][2]) == "0") { L = "Mode-3/A code derived from the reply of the transponder"; }
            if (Convert.ToString(CAT10_Message[Position][2]) == "1") { L = "Mode-3/A code not extracted during the last scan"; }

            //Binary to octet transformation:
            string Full_Reply_bin = Convert.ToString(CAT10_Message[Position]) + Convert.ToString(CAT10_Message[Position + 1]);
            int Octet_A_bin = Convert.ToInt32(Convert.ToString(Full_Reply_bin[4] + Full_Reply_bin[5] + Full_Reply_bin[6]),2);
            int Octet_B_bin = Convert.ToInt32(Convert.ToString(Full_Reply_bin[7] + Full_Reply_bin[8] + Full_Reply_bin[9]),2);
            int Octet_C_bin = Convert.ToInt32(Convert.ToString(Full_Reply_bin[10] + Full_Reply_bin[11] + Full_Reply_bin[12]),2);
            int Octet_D_bin = Convert.ToInt32(Convert.ToString(Full_Reply_bin[13] + Full_Reply_bin[14] + Full_Reply_bin[15]),2);

            Mode3_A_reply = Convert.ToString(Octet_A_bin + Octet_B_bin + Octet_C_bin + Octet_D_bin);
            Position++;

            return Position;
        }

        #endregion

        #region Item I010/090 Flight Level in Binary Representation

        public string Validated = ""; public string Garbled = ""; public string FL = "";
        
        private int Decode_Flight_Level(int Position, string[] CAT10_Message)
        {
            if (CAT10_Message[Position][0] == '1') { Validated = "Code Validated"; }
            if (CAT10_Message[Position][0] == '0') { Validated = "Code Not Validated"; }
            if (CAT10_Message[Position][1] == '1') { Garbled = "Garbled Code"; }
            if (CAT10_Message[Position][1] == '0') { Garbled = "Default"; }

            string FL_bin = "";
            
            for (int i = 2; i < (CAT10_Message[Position] + CAT10_Message[Position+1]).Length; i++)
            {
                FL_bin += (CAT10_Message[Position] + CAT10_Message[Position + 1])[i];
            }

            FL = Convert.ToString(Convert.ToInt32(FL_bin, 2) * 0.25);

            if (FL.Length == 1) { FL = "00" + FL; }
            if (FL.Length == 2) { FL = "0" + FL; }

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I010/091 Measured Height

        public string Measured_Height;

        private int Decode_Measured_Height(int Position, string[] CAT10_Message)
        {
            Measured_Height = Convert.ToString(Library.twos_complement(CAT10_Message[Position] + CAT10_Message[Position + 1]) * 6.25);

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I010/131 Amplitude of Primary Plot

        public string PAM = "";

        private int Decode_PAM(int Position, string[] CAT10_Message)
        {
            PAM = Convert.ToString(Convert.ToInt32(CAT10_Message[Position]));

            Position++;

            return Position;
        }

        #endregion

        #region Item I010/140 Time of Day

        public string ToD = "";

        private int Decode_Time_of_Day(int Position, string[] CAT10_Message)
        {
            double ToD_seconds = Convert.ToInt32(CAT10_Message[Position] + CAT10_Message[Position + 1] + CAT10_Message[Position + 2]) * (1.0/128.0);

            double ToD_minutes = ToD_seconds / 60;

            double ToD_hours = ToD_minutes / 60;

            ToD_minutes = (ToD_minutes % 60);

            ToD_seconds = (ToD_seconds % 60);

            ToD = Convert.ToString(ToD_hours) + ':' + Convert.ToString(ToD_minutes) + ':' + Convert.ToString(ToD_seconds) +  "UTC";

            Position = Position + 3;

            return Position;
        }

        #endregion
    }
}
