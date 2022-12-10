using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary;
using Class_Library;
using System.Drawing.Design;

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
            int Position = Convert.ToInt32(FSPEC_Return[0]) + 1;
            string FSPEC = FSPEC_Return[1];

            if (FSPEC[0] == '1') { Position = this.Decode_Data_Source_Identifier(Position, CAT10_Message); }
            if (FSPEC[1] == '1') { Position = this.Decode_MessageType(Position, CAT10_Message); }
            if (FSPEC[2] == '1') { Position = this.Decode_Target_Report_Descriptor(Position, CAT10_Message); }
            if (FSPEC[3] == '1') { Position = this.Decode_Time_of_Day(Position, CAT10_Message); }
            if (FSPEC[4] == '1') { Position = this.Decode_Position_WGS84(Position, CAT10_Message); }
            if (FSPEC[5] == '1') { Position = this.Decode_Measured_Position_Polar(Position, CAT10_Message); }
            if (FSPEC[6] == '1') { Position = this.Decode_Cartesian_Position(Position, CAT10_Message); }

            if (FSPEC.Length > 8)
            {
                if (FSPEC[8] == '1') { Position = this.Decode_Track_Velocity_Polar_Coordinates(Position, CAT10_Message); }
                if (FSPEC[9] == '1') { Position = this.Decode_Track_Velocity_Cartesian_Coordinates(Position, CAT10_Message); }
                if (FSPEC[10] == '1') { Position = this.Decode_Track_Number(Position, CAT10_Message); }
                if (FSPEC[11] == '1') { Position = this.Decode_Track_Status(Position, CAT10_Message); }
                if (FSPEC[12] == '1') { Position = this.Decode_Mode_3A(Position, CAT10_Message); }
                if (FSPEC[13] == '1') { Position = this.Decode_Target_Address(Position, CAT10_Message); }
                if (FSPEC[14] == '1') { Position = this.Decode_Target_Identification(Position, CAT10_Message); }

                if (FSPEC.Length > 16)
                {
                    if (FSPEC[16] == '1') { Position = this.Decode_Mode_S_MB_Data(Position, CAT10_Message); }
                    if (FSPEC[17] == '1') { Position = this.Decode_Vehicle_Fleet_Identification(Position, CAT10_Message); }
                    if (FSPEC[18] == '1') { Position = this.Decode_Flight_Level(Position, CAT10_Message); }
                    if (FSPEC[19] == '1') { Position = this.Decode_Measured_Height(Position, CAT10_Message); }
                    if (FSPEC[20] == '1') { Position = this.Decode_Size_Orientation(Position, CAT10_Message); }
                    if (FSPEC[21] == '1') { Position = this.Decode_System_Status(Position, CAT10_Message); }
                    if (FSPEC[22] == '1') { Position = this.Decode_Pre_Programmed_Message(Position, CAT10_Message); }

                    if (FSPEC.Length > 24)
                    {
                        if (FSPEC[24] == '1') { Position = this.Decode_Standard_deviation_of_position(Position, CAT10_Message); }
                        if (FSPEC[25] == '1') { Position = this.Decode_Presence(Position, CAT10_Message); }
                        if (FSPEC[26] == '1') { Position = this.Decode_PAM(Position, CAT10_Message); }
                        if (FSPEC[27] == '1') { Position = this.Decode_Calculated_Acceleration(Position, CAT10_Message); }
                    }
                }
            }
        }

        public string CAT = "10";

        #region Item I010/000 Message Type
        public string Message_type;
        private int Decode_MessageType(int Position, string[] CAT10_Message)
        {
            int Message_Type_byte_Int = Convert.ToInt32(CAT10_Message[Position], 2);
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

        #region Item I010/020 Target Report Descriptor
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
            if (CHN_bin == "0") { CHN = "Chain 1"; }
            if (CHN_bin == "1") { CHN = "Chain 2"; }

            string GBS_bin = Convert.ToString(CAT10_Message[Position][5]);
            if (GBS_bin == "0") { GBS = "Transponder Ground bit not set"; }
            if (GBS_bin == "1") { GBS = "Transponder Ground bit set"; }

            string CRT_bin = Convert.ToString(CAT10_Message[Position][6]);
            if (CRT_bin == "0") { CRT = "No Corrupted reply in multilateration"; }
            if (CRT_bin == "1") { CRT = "Corrupted replies in multilateration"; }

            Position++;
            if (Convert.ToString(CAT10_Message[Position - 1].Last()) == "1")
            {
                string SIM_bin = Convert.ToString(CAT10_Message[Position][0]);
                if (SIM_bin == "0") { SIM = "Actual target report"; }
                if (SIM_bin == "1") { SIM = "Simulated target report"; }

                string TST_bin = Convert.ToString(CAT10_Message[Position][1]);
                if (TST_bin == "0") { TST = "Default"; }
                if (TST_bin == "1") { TST = "Test target"; }

                string RAB_bin = Convert.ToString(CAT10_Message[Position][2]);
                if (RAB_bin == "0") { RAB = "Report from target transponder"; }
                if (RAB_bin == "1") { RAB = "Report from field monitor (fixed transponder)"; }

                string LOP_bin = Convert.ToString(Convert.ToString(CAT10_Message[Position][3]) + Convert.ToString(CAT10_Message[Position][4]));
                if (LOP_bin == "00") { LOP = "Undetermined"; }
                if (LOP_bin == "01") { LOP = "Loop start"; }
                if (LOP_bin == "10") { LOP = "Loope finish"; }

                string TOT_bin = Convert.ToString(Convert.ToString(CAT10_Message[Position][5]) + Convert.ToString(CAT10_Message[Position][6]));
                if (TOT_bin == "00") { TOT = "Undetermined"; }
                if (TOT_bin == "01") { TOT = "Aircraft"; }
                if (TOT_bin == "10") { TOT = "Ground vehicle"; }
                if (TOT_bin == "11") { TOT = "Helicopter"; }

                Position++;

                if (Convert.ToString(CAT10_Message[Position - 1].Last()) == "1")
                {
                    string SPI_bin = Convert.ToString(CAT10_Message[Position][0]);
                    if (SPI_bin == "0") { SPI = "Absence of SPI"; }
                    if (SPI_bin == "1") { SPI = "Special position identification"; }
                    Position++;
                }
            }

            return Position;
        }
        #endregion

        #region Item I010/040 Measured Position in Polar Coordinates

        public string RHO; public string THETA;

        private int Decode_Measured_Position_Polar(int Position, string[] CAT10_Message)
        {
            int Measured_Rho = Convert.ToInt32(Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]), 2);
            RHO = Convert.ToString(Measured_Rho);
            Position = Position + 2;   //Deberia estar esto?
            double Measured_Theta = Convert.ToInt32(Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]), 2) * 0.0055;//(360/(Math.Pow(2, 16)));
            THETA = Convert.ToString(Measured_Theta);
            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I010/041 Position in WGS-84 Coordinates

        public string Lat_WGS84; public string Lon_WGS84;


        private int Decode_Position_WGS84(int Position, string[] CAT10_Message)
        {
            string Lat_WGS84_bin = String.Concat(CAT10_Message[Position] , CAT10_Message[Position + 1] , CAT10_Message[Position + 2] , CAT10_Message[Position + 3]);
            Position = Position + 4;
            string Lon_WGS84_bin = String.Concat(CAT10_Message[Position] , CAT10_Message[Position + 1] , CAT10_Message[Position + 2] , CAT10_Message[Position + 3]);
            Position = Position + 4;

            if (Lat_WGS84_bin[0] == '0') { Lat_WGS84 = Convert.ToString(Convert.ToInt32(Lat_WGS84_bin, 2) * (180 / Math.Pow(2, 32))); }
            if (Lat_WGS84_bin[0] == '1') { Lat_WGS84 = Convert.ToString("-" + Library.twos_complement(Lat_WGS84_bin) * (180 / Math.Pow(2, 32))); }
            if (Lon_WGS84_bin[0] == '0') { Lon_WGS84 = Convert.ToString(Convert.ToInt32(Lon_WGS84_bin, 2) * (180 / Math.Pow(2, 32))); }
            if (Lon_WGS84_bin[0] == '1') { Lon_WGS84 = Convert.ToString("-" + Library.twos_complement(Lon_WGS84_bin) * (180 / Math.Pow(2, 32))); }

            return Position;
        }

        #endregion

        #region Item I010/042 Position in Cartesian Co-ordinates

        public string X_Component; public string Y_Component;

        private int Decode_Cartesian_Position(int Position, string[] CAT10_Message)
        {
            string X_Component_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]);
            Position = Position + 2;
            string Y_Component_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]);
            Position = Position + 2;

            if (X_Component_bin[0] == '0') { X_Component = Convert.ToString(Convert.ToInt32(X_Component_bin, 2)); }
            if (X_Component_bin[0] == '1') { X_Component = Convert.ToString("-" + Library.twos_complement(X_Component_bin)); }
            if (Y_Component_bin[0] == '0') { Y_Component = Convert.ToString(Convert.ToInt32(Y_Component_bin, 2)); }
            if (Y_Component_bin[0] == '1') { Y_Component = Convert.ToString("-" + Library.twos_complement(Y_Component_bin)); }


            return Position;
        }

        #endregion

        # region Item I010/060 Mode-3/A Code in Octal Representation

        public string V; public string G; public string L; public string Mode3_A_reply;
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
            int Octet_A_bin = Convert.ToInt32(string.Concat(Full_Reply_bin[4], Full_Reply_bin[5], Full_Reply_bin[6]), 2);
            int Octet_B_bin = Convert.ToInt32(string.Concat(Full_Reply_bin[7], Full_Reply_bin[8], Full_Reply_bin[9]), 2);
            int Octet_C_bin = Convert.ToInt32(string.Concat(Full_Reply_bin[10], Full_Reply_bin[11], Full_Reply_bin[12]), 2);
            int Octet_D_bin = Convert.ToInt32(string.Concat(Full_Reply_bin[13], Full_Reply_bin[14], Full_Reply_bin[15]), 2);

            Mode3_A_reply = Convert.ToString(Octet_A_bin) + Convert.ToString(Octet_B_bin) + Convert.ToString(Octet_C_bin) + Convert.ToString(Octet_D_bin);
            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I010/090 Flight Level in Binary Representation

        public string Validated; public string Garbled; public string FL;

        private int Decode_Flight_Level(int Position, string[] CAT10_Message)
        {
            if (CAT10_Message[Position][0] == '1') { Validated = "Code Validated"; }
            if (CAT10_Message[Position][0] == '0') { Validated = "Code Not Validated"; }
            if (CAT10_Message[Position][1] == '1') { Garbled = "Garbled Code"; }
            if (CAT10_Message[Position][1] == '0') { Garbled = "Default"; }

            string Flight_level_bin = Convert.ToString(String.Concat(CAT10_Message[Position][2], CAT10_Message[Position][3], CAT10_Message[Position][4], CAT10_Message[Position][5], CAT10_Message[Position][6], CAT10_Message[Position][7], CAT10_Message[Position + 1]));
            int FL_int = 0;
            if (Convert.ToString(Flight_level_bin[0]) == "0") { FL_int = Convert.ToInt32(Flight_level_bin, 2); }
            if (Convert.ToString(Flight_level_bin[0]) == "1") { FL_int = Library.twos_complement(Flight_level_bin) * -1; }

            FL = Convert.ToString(FL_int * 0.25);

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

        public string PAM;

        private int Decode_PAM(int Position, string[] CAT10_Message)
        {
            PAM = Convert.ToString(Convert.ToInt32(CAT10_Message[Position]), 2);

            Position++;

            return Position;
        }

        #endregion

        #region Item I010/140 Time of Day

        public string ToD; public double ToD_seconds;

        private int Decode_Time_of_Day(int Position, string[] CAT10_Message)
        {
            //string Probe = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1] + CAT10_Message[Position + 2]);
            double ToD_seconds_int = Convert.ToInt32(String.Concat(CAT10_Message[Position],CAT10_Message[Position + 1],CAT10_Message[Position + 2]), 2);

            ToD_seconds = (ToD_seconds_int / 128);

            ToD = Library.ToD_Calc(ToD_seconds);

            Position = Position + 3;

            return Position;
        }

        #endregion

        #region Item I010/161 Track Number

        public string Track_Number;

        private int Decode_Track_Number(int Position, string[] CAT10_Message)
        {
            //Bits 16 to 13 set to 0
            string bin_Track_Number = Convert.ToString(String.Concat(CAT10_Message[Position][4], CAT10_Message[Position][5], CAT10_Message[Position][6], CAT10_Message[Position][7]) + CAT10_Message[Position + 1]);
            Track_Number = Convert.ToString(Convert.ToInt32(bin_Track_Number, 2));

            Position = Position + 2;
            return Position;
        }

        #endregion

        #region Item I010/161 Track Status

        //Main structure data
        public string CNF; public string TRE; public string CST; public string MAH; public string TCC; public string STH;
        //First octet extent data
        public string TOM; public string DOU; public string MRS;
        //Second octet extent data
        public string GHO;

        private int Decode_Track_Status(int Position, string[] CAT10_Message)
        {

            string CNF_bin = Convert.ToString(CAT10_Message[Position][0]);
            if (CNF_bin == "0") { CNF = "Confirmed track"; }
            if (CNF_bin == "1") { CNF = "Track initalisation phase"; }

            string TRE_bin = Convert.ToString(CAT10_Message[Position][1]);
            if (TRE_bin == "0") { TRE = "Default"; }
            if (TRE_bin == "1") { TRE = "Last report track"; }

            string CST_bin = Convert.ToString(Convert.ToString(CAT10_Message[Position][2]) + Convert.ToString(CAT10_Message[Position][3]));
            if (CST_bin == "00") { CST = "No extrapolation"; }
            if (CST_bin == "01") { CST = "Predictable extrapolation due to sensor refresh period (see NOTE)"; }
            if (CST_bin == "10") { CST = "Predictable extrapolation in masked area"; }
            if (CST_bin == "11") { CST = "Extrapolation due to unpredictable absence of detection"; }

            string MAH_bin = Convert.ToString(CAT10_Message[Position][4]);
            if (MAH_bin == "0") { MAH = "Default"; }
            if (MAH_bin == "1") { MAH = "Horizontal manoeuvre"; }

            string TCC_bin = Convert.ToString(CAT10_Message[Position][5]);
            if (TCC_bin == "0") { TCC = "Tracking performed in 'Sensor Plane', i.e. neither slant range correction nor projection was applied. "; }
            if (TCC_bin == "1") { TCC = "Slant range correction and a suitable projection technique are used to track in a 2D.reference plane, tangential to the earth model at the Sensor Site co-ordinates."; }

            string STH_bin = Convert.ToString(CAT10_Message[Position][6]);
            if (STH_bin == "0") { STH = "Measured position"; }
            if (STH_bin == "1") { STH = "Smoothed position"; }

            Position++;
            if (Convert.ToString(CAT10_Message[Position - 1].Last()) == "1")
            {
                string TOM_bin = Convert.ToString(String.Concat(CAT10_Message[Position][0] + CAT10_Message[Position][1]));
                if (TOM_bin == "00") { TOM = "Unknown type of movement"; }
                if (TOM_bin == "01") { TOM = "Taking-off"; }
                if (TOM_bin == "10") { TOM = "Landing"; }
                if (TOM_bin == "11") { TOM = "Other types of movement"; }

                string DOU_bin = Convert.ToString(String.Concat(CAT10_Message[Position][2] + CAT10_Message[Position][3] + CAT10_Message[Position][4]));
                if (DOU_bin == "000") { DOU = "No doubt"; }
                if (DOU_bin == "001") { DOU = "Doubtful correlation (undetermined reason)"; }
                if (DOU_bin == "010") { DOU = "Doubtful correlation in clutter"; }
                if (DOU_bin == "011") { DOU = "Loss of accuracy"; }
                if (DOU_bin == "100") { DOU = "Loss of accuracy in clutter"; }
                if (DOU_bin == "101") { DOU = "Unstable track"; }
                if (DOU_bin == "110") { DOU = " Previously coasted"; }

                string MRS_bin = Convert.ToString(String.Concat(CAT10_Message[Position][5] + CAT10_Message[Position][6]));
                if (MRS_bin == "00") { MRS = "Merge or split indication undetermined"; }
                if (MRS_bin == "01") { MRS = "Track merged by association to plot"; }
                if (MRS_bin == "10") { MRS = "Track merged by non-association to plot"; }
                if (MRS_bin == "11") { MRS = "Split track"; }

                Position++;

                if (Convert.ToString(CAT10_Message[Position - 1].Last()) == "1")
                {
                    string GHO_bin = Convert.ToString(CAT10_Message[Position][0]);
                    if (GHO_bin == "0") { GHO = "Default"; }
                    if (GHO_bin == "1") { GHO = "Ghost track"; }
                    Position++;
                }
            }
            return Position;
        }

        #endregion

        #region Item I010/200 Calculated Track Velocity in Polar Co-ordinates

        public string Ground_Speed; public string Track_Angle;

        private int Decode_Track_Velocity_Polar_Coordinates(int Position, string[] CAT10_Message)
        {
            string Ground_Speed_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]);
            Ground_Speed = Convert.ToString(Convert.ToInt32(Ground_Speed_bin, 2) * 0.22); //kt
            Position = Position + 2;
            string Track_Angle_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1]);
            Track_Angle = Convert.ToString(Convert.ToInt32(Track_Angle_bin, 2) * 0.0055); //º
            Position = Position + 2;

            return Position;
        }
        #endregion

        #region Item I010/202 Calculated Track Velocity in Cartesian Co-ordinates

        public string Vx; public string Vy;

        private int Decode_Track_Velocity_Cartesian_Coordinates(int Position, string[] CAT10_Message)
        {
            string Ground_Speed_bin = Convert.ToString(String.Concat(CAT10_Message[Position] + CAT10_Message[Position + 1]));
            Position = Position + 2;
            string Track_Angle_bin = Convert.ToString(String.Concat(CAT10_Message[Position] + CAT10_Message[Position + 1]));
            Position = Position + 2;

            if (Ground_Speed_bin[0] == '0') { Vx = Convert.ToString(Convert.ToInt32(Ground_Speed_bin, 2) * (0.25)); }
            if (Ground_Speed_bin[0] == '1') { Vx = "-" + Convert.ToString(Library.twos_complement(Ground_Speed_bin) * (0.25)); }
            if (Track_Angle_bin[0] == '0') { Vy = Convert.ToString(Convert.ToInt32(Track_Angle_bin, 2) * (0.25)); }
            if (Track_Angle_bin[0] == '1') { Vy = "-" + Convert.ToString(Library.twos_complement(Track_Angle_bin) * (0.25)); }


            return Position;
        }
        #endregion

        #region Item I010/210 Calculated Acceleration

        public string Ax; public string Ay;

        private int Decode_Calculated_Acceleration(int Position, string[] CAT10_Message)
        {
            string Ax_bin = Convert.ToString(CAT10_Message[Position]);
            Position = Position + 1;
            string Ay_bin = Convert.ToString(CAT10_Message[Position]);
            Position = Position + 1;

            if (Ax_bin[0] == '0') { Ax = Convert.ToString(Convert.ToInt32(Ax_bin, 2) * (0.25)); } //m/s^2
            if (Ax_bin[0] == '1') { Ax = Convert.ToString("-" + Library.twos_complement(Ax_bin) * (0.25)); }
            if (Ay_bin[0] == '0') { Ay = Convert.ToString(Convert.ToInt32(Ay_bin, 2) * (0.25)); }
            if (Ay_bin[0] == '1') { Ay = Convert.ToString("-" + Library.twos_complement(Ay_bin) * (0.25)); }


            return Position;
        }
        #endregion

        #region Item I010/220 Target Address

        public string Target_Address;

        private int Decode_Target_Address(int Position, string[] CAT10_Message)
        {
            string Probe = String.Concat(CAT10_Message[Position], CAT10_Message[Position + 1], CAT10_Message[Position + 2]);
            int TargetAddress_int = Convert.ToInt32(String.Concat(CAT10_Message[Position], CAT10_Message[Position + 1], CAT10_Message[Position + 2]), 2);
            string hexValue = TargetAddress_int.ToString("X");
            Position = Position + 3;

            //Target_Address = Convert.ToString(int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber));
            Target_Address = "0x" + hexValue;
            return Position;
        }
        #endregion

        #region Item I010/245 Target identification

        public string STI; public string Target_Identification;
        public string Char1; public string Char2; public string Char3; public string Char4; public string Char5; public string Char6; public string Char7; public string Char8;
        private int Decode_Target_Identification(int Position, string[] CAT10_Message)
        {
            string STI_bin = String.Concat(Convert.ToString(CAT10_Message[Position][0]) + Convert.ToString(CAT10_Message[Position][1]));
            if (STI_bin == "00") { STI = "Callsign or registration downlinked from transponder"; }
            if (STI_bin == "01") { STI = "Callsign not downlinked from transponder"; }
            if (STI_bin == "10") { STI = "Registration not downlinked from transponder"; }
            Position = Position + 1;

            string Six1 = String.Concat(Convert.ToString(CAT10_Message[Position][0]) + Convert.ToString(CAT10_Message[Position][1]) + Convert.ToString(CAT10_Message[Position][2]) + Convert.ToString(CAT10_Message[Position][3]) + Convert.ToString(CAT10_Message[Position][4]) + Convert.ToString(CAT10_Message[Position][5]));
            Char1 = Library.Target_Identification_Coding(Six1);
            Position = Position + 1;
            string Six2 = String.Concat(Convert.ToString(CAT10_Message[Position - 1][6]) + Convert.ToString(CAT10_Message[Position - 1][7]) + Convert.ToString(CAT10_Message[Position][0]) + Convert.ToString(CAT10_Message[Position][1]) + Convert.ToString(CAT10_Message[Position][2]) + Convert.ToString(CAT10_Message[Position][3]));
            Char2 = Library.Target_Identification_Coding(Six2);
            Position = Position + 1;
            string Six3 = String.Concat(Convert.ToString(CAT10_Message[Position - 1][4]) + Convert.ToString(CAT10_Message[Position - 1][5]) + Convert.ToString(CAT10_Message[Position - 1][6]) + Convert.ToString(CAT10_Message[Position - 1][7]) + Convert.ToString(CAT10_Message[Position][0]) + Convert.ToString(CAT10_Message[Position][1]));
            Char3 = Library.Target_Identification_Coding(Six3);
            Position = Position + 1;
            string Six4 = String.Concat(Convert.ToString(CAT10_Message[Position - 1][2]) + Convert.ToString(CAT10_Message[Position - 1][3]) + Convert.ToString(CAT10_Message[Position - 1][4]) + Convert.ToString(CAT10_Message[Position - 1][5]) + Convert.ToString(CAT10_Message[Position - 1][6]) + Convert.ToString(CAT10_Message[Position - 1][7]));
            Char4 = Library.Target_Identification_Coding(Six4);
            string Six5 = String.Concat(Convert.ToString(CAT10_Message[Position][0]) + Convert.ToString(CAT10_Message[Position][1]) + Convert.ToString(CAT10_Message[Position][2]) + Convert.ToString(CAT10_Message[Position][3]) + Convert.ToString(CAT10_Message[Position][4]) + Convert.ToString(CAT10_Message[Position][5]));
            Char5 = Library.Target_Identification_Coding(Six5);
            Position = Position + 1;
            string Six6 = String.Concat(Convert.ToString(CAT10_Message[Position - 1][6]) + Convert.ToString(CAT10_Message[Position - 1][7]) + Convert.ToString(CAT10_Message[Position][0]) + Convert.ToString(CAT10_Message[Position][1]) + Convert.ToString(CAT10_Message[Position][2]) + Convert.ToString(CAT10_Message[Position][3]));
            Char6 = Library.Target_Identification_Coding(Six6);
            Position = Position + 1;
            string Six7 = String.Concat(Convert.ToString(CAT10_Message[Position - 1][4]) + Convert.ToString(CAT10_Message[Position - 1][5]) + Convert.ToString(CAT10_Message[Position - 1][6]) + Convert.ToString(CAT10_Message[Position - 1][7]) + Convert.ToString(CAT10_Message[Position][0]) + Convert.ToString(CAT10_Message[Position][1]));
            Char7 = Library.Target_Identification_Coding(Six7);
            Position = Position + 1;
            string Six8 = String.Concat(Convert.ToString(CAT10_Message[Position - 1][2]) + Convert.ToString(CAT10_Message[Position - 1][3]) + Convert.ToString(CAT10_Message[Position - 1][4]) + Convert.ToString(CAT10_Message[Position - 1][5]) + Convert.ToString(CAT10_Message[Position - 1][6]) + Convert.ToString(CAT10_Message[Position - 1][7]));
            Char8 = Library.Target_Identification_Coding(Six8);

            Target_Identification = Convert.ToString(Char1 + Char2 + Char3 + Char4 + Char5 + Char6 + Char7 + Char8);

            return Position;
        }
        #endregion

        #region Item I010/250 Mode S MB Data

        public List<string> DataList;

        private int Decode_Mode_S_MB_Data(int Position, string[] CAT10_Message)
        {
            int rep = Convert.ToInt32(CAT10_Message[Position], 2);
            int i = 0;
            string BDS1;
            string BDS2;
            BDS1 = String.Concat(CAT10_Message[Position + 8][0], CAT10_Message[Position + 8][1], CAT10_Message[Position + 8][2], CAT10_Message[Position + 8][3]); 
            BDS2 = String.Concat(CAT10_Message[Position + 8][4], CAT10_Message[Position + 8][5], CAT10_Message[Position + 8][6], CAT10_Message[Position + 8][7]); 
            while (rep > i)
            {

                if (Convert.ToString(BDS1) + Convert.ToString(BDS2) == "00")
                {
                    DataList.Add("Data");
                }

                if (Convert.ToString(BDS1) + Convert.ToString(BDS2) == "01")
                {
                    DataList.Add("Data");
                }
                Position = Position + 7;
                i++;
            }

            return Position;
        }
        #endregion

        #region Item I010/270 Target Size & Orientation

        public string Lenght; public string Orientation; public string Width;

        private int Decode_Size_Orientation(int Position, string[] CAT10_Message)
        {
            string Lenght_bin = Convert.ToString(CAT10_Message[Position]);
            Lenght = Convert.ToString(Convert.ToInt32(Lenght_bin.Remove(Lenght_bin.Length - 1, 1), 2)); //1m
            Position = Position + 1;
            if (Lenght_bin[7] == '1')
            {
                string Orientation_bin = Convert.ToString(CAT10_Message[Position]);
                Orientation = Convert.ToString(Convert.ToInt32(Orientation_bin.Remove(Orientation_bin.Length - 1, 1), 2) * 2.81); //º
                Position = Position + 1;

                if (Orientation_bin[7] == '1')
                {
                    string Width_bin = Convert.ToString(CAT10_Message[Position]);
                    Width = Convert.ToString(Convert.ToInt32(Width_bin.Remove(Width_bin.Length - 1, 1), 2)); //1m
                    Position = Position + 1;
                }
            }
            return Position;
        }
        #endregion

        #region Item I010/280 Presence

        public string REP; public string DRHO; public string DTHETA;

        private int Decode_Presence(int Position, string[] CAT10_Message)
        {
            string Lenght_bin = Convert.ToString(CAT10_Message[Position]);
            string Lenght = Convert.ToString(Convert.ToInt32(Lenght_bin.Remove(Lenght_bin.Length - 1, 1), 2));
            Position++;

            return Position;
        }
        #endregion

        #region Item I010/280 Vehicle Fleet Identification

        public string VFI;

        private int Decode_Vehicle_Fleet_Identification(int Position, string[] CAT10_Message)
        {
            string VFI_bin = Convert.ToString(CAT10_Message[Position]);
            int VFI_Int = Convert.ToInt32(VFI_bin, 2);

            if (VFI_Int == 0) { VFI = "Unknown"; }
            if (VFI_Int == 1) { VFI = "ATC equipment maintenance"; }
            if (VFI_Int == 2) { VFI = "Airport maintenance"; }
            if (VFI_Int == 3) { VFI = "Fire"; }
            if (VFI_Int == 4) { VFI = "Bird scarer"; }
            if (VFI_Int == 5) { VFI = "Snow plough"; }
            if (VFI_Int == 6) { VFI = "Runway sweeper"; }
            if (VFI_Int == 7) { VFI = "Emergency"; }
            if (VFI_Int == 8) { VFI = "Police"; }
            if (VFI_Int == 9) { VFI = "Bus"; }
            if (VFI_Int == 10) { VFI = "Tug (push/tow)"; }
            if (VFI_Int == 11) { VFI = "Grass cutter"; }
            if (VFI_Int == 12) { VFI = "Fuel"; }
            if (VFI_Int == 13) { VFI = "Baggage"; }
            if (VFI_Int == 14) { VFI = "Catering"; }
            if (VFI_Int == 15) { VFI = "Aircraft maintenance"; }
            if (VFI_Int == 16) { VFI = "Flyco (follow me)"; }

            Position = Position + 1;
            return Position;
        }
        #endregion

        #region Item I010/280 Pre-programmed Message

        public string TRB; public string MSG;

        private int Decode_Pre_Programmed_Message(int Position, string[] CAT10_Message)
        {
            string TRB_bin = Convert.ToString(CAT10_Message[Position][0]);
            if (TRB_bin == "0") { TRB = "Default"; }
            if (TRB_bin == "1") { TRB = "In Trouble"; }

            int MSG_bin = Convert.ToInt32(CAT10_Message[Position].Remove(0, 1), 2);
            if (MSG_bin == 1) { MSG = "Towing aircraft"; }
            if (MSG_bin == 2) { MSG = "“Follow me” operation "; }
            if (MSG_bin == 3) { MSG = "Runway check"; }
            if (MSG_bin == 4) { MSG = "Emergency operation (fire, medical…)"; }
            if (MSG_bin == 5) { MSG = "Work in progress (maintenance, birds scarer, sweepers…)"; }
            Position = Position + 1;

            return Position;
        }
        #endregion

        #region Item I010/280  Standard Deviation of Position

        public string Ox; public string Oy; public string Oxy;

        private int Decode_Standard_deviation_of_position(int Position, string[] CAT10_Message)
        {
            string Ox_bin = Convert.ToString(CAT10_Message[Position]);
            string Ox = Convert.ToString(Convert.ToInt32(Ox_bin, 2));

            string Oy_bin = Convert.ToString(CAT10_Message[Position + 1]);
            string Oy = Convert.ToString(Convert.ToInt32(Oy_bin, 2));

            string Oxy_bin = Convert.ToString(CAT10_Message[Position + 2] + CAT10_Message[Position + 3]);
            string Oxy = Convert.ToString(Convert.ToInt32(Oxy_bin, 2));

            Position = Position + 4;

            return Position;
        }
        #endregion

        #region Item I010/280  System Status

        public string NOGO; public string OVL; public string TSV; public string DIV; public string TTF;

        private int Decode_System_Status(int Position, string[] CAT10_Message)
        {
            string NOGO_bin = Convert.ToString(CAT10_Message[Position][0] + CAT10_Message[Position][1]);
            if (NOGO_bin == "00") { NOGO = "Operational"; }
            if (NOGO_bin == "01") { NOGO = "Degraded"; }
            if (NOGO_bin == "10") { NOGO = "NOGO"; }

            string OVL_bin = Convert.ToString(CAT10_Message[Position][2]);
            if (OVL_bin == "0") { OVL = "No overloadd"; }
            if (OVL_bin == "1") { OVL = "Overload"; }

            string TSV_bin = Convert.ToString(CAT10_Message[Position][3]);
            if (TSV_bin == "0") { TSV = "Valid"; }
            if (TSV_bin == "1") { TSV = "Invalid"; }

            string DIV_bin = Convert.ToString(CAT10_Message[Position][4]);
            if (DIV_bin == "0") { DIV = "Normal Operation"; }
            if (DIV_bin == "1") { DIV = "Diversity degraded"; }

            string TTF_bin = Convert.ToString(CAT10_Message[Position][5]);
            if (TTF_bin == "0") { TTF = "Test Target Operative"; }
            if (TTF_bin == "1") { TTF = "Test Target Failure"; }

            Position = Position + 1;

            return Position;
        }
        #endregion

        public int Num_Message;
        public void set_Message_Num(int Message_Num)
        {
            this.Num_Message = Message_Num;
        }
    }
}
