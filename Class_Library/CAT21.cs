using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Text;

namespace Class_Library
{
    public class CAT21
    {
        public string[] FSPEC_Return;

        public string[] CAT21_Message;   //Objeto tipo lista de bytes (mensage CAT10)
        readonly DecodeLibrary Library;
        public CAT21(string[] CAT21_Message, DecodeLibrary Library)
        {
            this.CAT21_Message = CAT21_Message;
            this.Library = Library;


            FSPEC_Return = Library.obtainFSPEC(CAT21_Message);
            int Position = Convert.ToInt32(FSPEC_Return[0]) + 1;
            string FSPEC = FSPEC_Return[1];
            {
                if (FSPEC[0] == '1') { Position = this.Decode_Aircraft_Operation_Status(Position, CAT21_Message); }
                if (FSPEC[1] == '1') { Position = this.Decode_Data_Source_Identifier(Position, CAT21_Message); }
                if (FSPEC[2] == '1') { Position = this.Decode_Service_Identification(Position, CAT21_Message); }
                if (FSPEC[3] == '1') { Position = this.Decode_Service_Management(Position, CAT21_Message); }
                if (FSPEC[4] == '1') { Position = this.Decode_Emitter_Category(Position, CAT21_Message); }
                if (FSPEC[5] == '1') { Position = this.Decode_Target_Report_Descriptor(Position, CAT21_Message); }
                if (FSPEC[6] == '1') { Position = this.Decode_Mode_3A(Position, CAT21_Message); }

                if (FSPEC[8] == '1') { Position = this.Decode_Target_Report_Descriptor(Position, CAT21_Message); }
                if (FSPEC[9] == '1') { Position = this.Decode_ToA_Position(Position, CAT21_Message); }
                if (FSPEC[10] == '1') { Position = this.Decode_ToA_Velocity(Position, CAT21_Message); }
                if (FSPEC[11] == '1') { Position = this.Decode_ToMR_Position(Position, CAT21_Message); }
                if (FSPEC[12] == '1') { Position = this.Decode_ToMR_Position_HP(Position, CAT21_Message); }
                if (FSPEC[13] == '1') { Position = this.Decode_ToMR_Velocity(Position, CAT21_Message); }
                if (FSPEC[14] == '1') { Position = this.Decode_ToMR_Velocity_HP(Position, CAT21_Message); }

                if (FSPEC[16] == '1') { Position = this.Decode_ToART(Position, CAT21_Message); }
                if (FSPEC[17] == '1') { Position = this.Decode_Target_Address(Position, CAT21_Message); }
                if (FSPEC[18] == '1') { Position = this.Decode_Quality_Indicators(Position, CAT21_Message); }
                if (FSPEC[19] == '1') { Position = this.Decode_Trajectory_Intent(Position, CAT21_Message); }
                if (FSPEC[20] == '1') { Position = this.Decode_Position_WGS(Position, CAT21_Message); }
                if (FSPEC[21] == '1') { Position = this.Decode_Position_WGS84(Position, CAT21_Message); }
                if (FSPEC[22] == '1') { Position = this.Decode_Message_Amplitude(Position, CAT21_Message); }

                if (FSPEC[24] == '1') { Position = this.Decode_Geometric_Height(Position, CAT21_Message); }
                if (FSPEC[25] == '1') { Position = this.Decode_Flight_Level(Position, CAT21_Message); }
                if (FSPEC[26] == '1') { Position = this.Decode_Selected_Altitude(Position, CAT21_Message); }
                if (FSPEC[27] == '1') { Position = this.Decode_Final_State_Selected_Altitude(Position, CAT21_Message); }
                if (FSPEC[28] == '1') { Position = this.Decode_Air_Speed(Position, CAT21_Message); }
                if (FSPEC[29] == '1') { Position = this.Decode_True_Airspeed(Position, CAT21_Message); }
                if (FSPEC[30] == '1') { Position = this.Decode_Magnetic_Heading(Position, CAT21_Message); }

                if (FSPEC[32] == '1') { Position = this.Decode_Barometric_Vertical_Rate(Position, CAT21_Message); }
                if (FSPEC[33] == '1') { Position = this.Decode_Geometric_Vertical_Rate(Position, CAT21_Message); }
                if (FSPEC[34] == '1') { Position = this.Decode_Airborne_Ground_Vector(Position, CAT21_Message); }
                if (FSPEC[35] == '1') { Position = this.Decode_Track_Number(Position, CAT21_Message); }
                if (FSPEC[36] == '1') { Position = this.Decode_Track_Angle_Rate(Position, CAT21_Message); }
                if (FSPEC[37] == '1') { Position = this.Decode_Target_Identification(Position, CAT21_Message); }
                if (FSPEC[38] == '1') { Position = this.Decode_Target_Status(Position, CAT21_Message); }

                if (FSPEC[40] == '1') { Position = this.Decode_MOPS_Version(Position, CAT21_Message); }
                if (FSPEC[41] == '1') { Position = this.Decode_Met_Information(Position, CAT21_Message); }
                if (FSPEC[42] == '1') { Position = this.Decode_Roll_Angle(Position, CAT21_Message); }
                if (FSPEC[43] == '1') { Position = this.Decode_Mode_S_MB_Data(Position, CAT21_Message); }
                if (FSPEC[44] == '1') { Position = this.Decode_ACAS_RAR(Position, CAT21_Message); }
                if (FSPEC[45] == '1') { Position = this.Decode_Surface_Capabilities_Characteristics(Position, CAT21_Message); }
                if (FSPEC[46] == '1') { Position = this.Decode_Data_Ages(Position, CAT21_Message); }
            }
        }

        #region Item I021/008 Aircraft Operational Status

        public string RA; public string TC; public string TS;
        public string ARV; public string CDTI_A; public string not_TCAS; public string SA;
        private int Decode_Aircraft_Operation_Status(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][0] == '0') { RA = "TCAS II or ACAS RA not active"; }
            if (CAT21_Message[Position][0] == '1') { RA = "TCAS RA active"; }

            string TC_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][1]) + Convert.ToString(CAT21_Message[Position][2])));
            if (TC_int == "0") { TC = "No Capability for Trajectory Change Reports"; }
            if (TC_int == "1") { TC = "Support for TC + 0 reports only"; }
            if (TC_int == "2") { TC = "Support for multiple TC reports"; }
            if (TC_int == "3") { TC = "reserved"; }

            if (CAT21_Message[Position][3] == '0') { TS = "No Capability to support Target State Reports"; }
            if (CAT21_Message[Position][3] == '1') { TS = "Capable of supporting Target State Reports"; }

            if (CAT21_Message[Position][4] == '0') { ARV = "No Capability to generate ARV Reports"; }
            if (CAT21_Message[Position][4] == '1') { ARV = "Capable of generating ARV Reports"; }

            if (CAT21_Message[Position][5] == '0') { CDTI_A = "CDTI Not Operational"; }
            if (CAT21_Message[Position][5] == '1') { CDTI_A = "CDTI Operational"; }

            if (CAT21_Message[Position][6] == '0') { not_TCAS = "TCAS Operational"; }
            if (CAT21_Message[Position][6] == '1') { not_TCAS = "TCAS Not Operational"; }

            if (CAT21_Message[Position][7] == '0') { SA = "Antenna Diversity"; }
            if (CAT21_Message[Position][7] == '1') { SA = "Single Antenna Only"; }

            Position++;

            return Position;
        }
        #endregion

        #region Item I021/010 Data Source Identification
        public string SIC;
        public string SAC;
        private int Decode_Data_Source_Identifier(int Position, string[] CAT21_Message)
        {
            int Data_Source_Identifier_byte_SAC = Convert.ToInt32(CAT21_Message[Position], 2);
            SAC = Convert.ToString(Data_Source_Identifier_byte_SAC);
            Position++;
            int Data_Source_Identifier_byte_SIC = Convert.ToInt32(CAT21_Message[Position], 2);
            SIC = Convert.ToString(Data_Source_Identifier_byte_SIC);
            Position++;

            return Position;
        }
        #endregion

        #region Item I021/015 Service Identification

        public string SID;

        private int Decode_Service_Identification(int Position, string[] CAT21_Message)
        {
            SID = Convert.ToString(Convert.ToInt32(CAT21_Message[Position]));

            Position++;

            return Position;
        }

        #endregion

        #region Item I021/016 Service Management

        public string RP;

        private int Decode_Service_Management(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][-1] == '0') { RP = "Data Driven Mode"; }

            else
            {
                double RP_double = Convert.ToInt32(CAT21_Message[Position]) * 0.5 - 0.5;

                if (RP_double >= 127.5) { RP = "127.5 seconds or more"; }

                else { RP = Convert.ToString(RP_double) + " seconds"; }
            }

            Position++;

            return Position;
        }

        #endregion

        #region Item I021/020 Emitter Category

        public string ECAT;

        private int Decode_Emitter_Category(int Position, string[] CAT21_Message)
        {
            string ECAT_int = Convert.ToString(Convert.ToInt32(CAT21_Message[Position]));

            if (ECAT_int == "0") { ECAT = "No ADS-B Emitter Category Information"; }
            if (ECAT_int == "1") { ECAT = "Light Aircraft <= 15500 lbs"; }
            if (ECAT_int == "2") { ECAT = "15500 lbs < Small Aircraft < 75000 lbs"; }
            if (ECAT_int == "3") { ECAT = "75000 lbs < Medium Aircraft < 300000 lbs"; }
            if (ECAT_int == "4") { ECAT = "High Vortex Large"; }
            if (ECAT_int == "5") { ECAT = "300000 lbs <= Heavy Aircraft"; }
            if (ECAT_int == "6") { ECAT = "Highly Maneouvrable (5g Acceleration Capability) and high speed (>400 kts cruise)"; }

            if (ECAT_int == "10") { ECAT = "Rotocraft"; }
            if (ECAT_int == "11") { ECAT = "Glider / Sailplane"; }
            if (ECAT_int == "12") { ECAT = "Lighter-Than-Air"; }
            if (ECAT_int == "13") { ECAT = "Unmanned Aerial Vehicle"; }
            if (ECAT_int == "14") { ECAT = "Space / Transatmospheric Vehicle"; }
            if (ECAT_int == "15") { ECAT = "Ultralight / Handglider / Paraglider"; }
            if (ECAT_int == "16") { ECAT = "Parachutist / Skydiver"; }

            if (ECAT_int == "20") { ECAT = "Surface Emergency Vehicle"; }
            if (ECAT_int == "21") { ECAT = "Surface Service Vehicle"; }
            if (ECAT_int == "22") { ECAT = "Fixed Ground or Tethered Obstruction"; }
            if (ECAT_int == "23") { ECAT = "Cluster Obstacle"; }
            if (ECAT_int == "24") { ECAT = "Line Obstacle"; }

            if (ECAT_int == "7" || ECAT_int == "8" || ECAT_int == "9" || ECAT_int == "17" || ECAT_int == "18" || ECAT_int == "19") { ECAT = "Reserved"; }

            Position++;

            return Position;
        }

        #endregion

        #region Item I021/040 Target Report Descriptor

        //Main structure data
        public string ATP; public string ARC; public string RC; public string RAB;

        //First Extension Data
        public string DCR; public string GBS; public string SIM; public string TST; public string SAA; public string CL;

        //Second Extension Data
        public string IPC; public string NOGO; public string CPR; public string LDPJ; public string RCF;

        private int Decode_Target_Report_Descriptor(int Position, string[] CAT21_Message)
        {
            string ATP_int = Convert.ToString(Convert.ToInt32(CAT21_Message[Position][0] + CAT21_Message[Position][1] + CAT21_Message[Position][2]));

            if (ATP_int == "0") { ATP = "24-Bit ICAO Address"; }
            if (ATP_int == "1") { ATP = "Duplicate Address"; }
            if (ATP_int == "2") { ATP = "Surface Vehicle Address"; }
            if (ATP_int == "3") { ATP = "Anonymous Address"; }
            if (ATP_int == "4" | ATP_int == "5" | ATP_int == "6" | ATP_int == "7") { ATP = "Reserved for Future Use"; }

            string ARC_int = Convert.ToString(Convert.ToInt32(CAT21_Message[Position][3] + CAT21_Message[Position][4]));

            if (ARC_int == "0") { ARC = "25 ft"; }
            if (ARC_int == "1") { ARC = "100 ft"; }
            if (ARC_int == "2") { ARC = "Unknown"; }
            if (ARC_int == "3") { ARC = "Invalid"; }

            if (CAT21_Message[Position][5] == '0') { RC = "Default"; }
            if (CAT21_Message[Position][5] == '1') { RC = "Range Check Passed, CPR Validation Pending"; }

            if (CAT21_Message[Position][6] == '0') { RAB = "Report from Targeet Transponder"; }
            if (CAT21_Message[Position][6] == '1') { RAB = "Report from Field Monitor (Fixed Transponder)"; }

            Position++;

            while (CAT21_Message[Position - 1].Last() == '1')
            {
                if (CAT21_Message[Position][0] == '0') { DCR = "No Differential Correction (ADS-B)"; }
                if (CAT21_Message[Position][0] == '1') { DCR = "Differential Correction (ADS-B)"; }

                if (CAT21_Message[Position][1] == '0') { GBS = "Ground Bit Not Set"; }
                if (CAT21_Message[Position][1] == '1') { GBS = "Ground Bit Set"; }

                if (CAT21_Message[Position][2] == '0') { SIM = "Actual Target Report"; }
                if (CAT21_Message[Position][2] == '1') { SIM = "Simulated Target Report"; }

                if (CAT21_Message[Position][3] == '0') { TST = "Default"; }
                if (CAT21_Message[Position][3] == '1') { TST = "Test Target"; }

                if (CAT21_Message[Position][4] == '0') { SAA = "Equipment Capable of Providing Selected Altitude"; }
                if (CAT21_Message[Position][4] == '1') { SAA = "Equipment Not Capable of Providing Selected Altitude"; }

                string CL_int = Convert.ToString(Convert.ToInt32(CAT21_Message[Position][5] + CAT21_Message[Position][6]));

                if (CL_int == "0") { CL = "Report Valid"; }
                if (CL_int == "1") { CL = "Report Suspect"; }
                if (CL_int == "2") { CL = "No Information"; }
                if (CL_int == "3") { CL = "Reserved for Future Use"; }

                Position++;

                while (CAT21_Message[Position - 1].Last() == '1')
                {
                    if (CAT21_Message[Position][2] == '0') { IPC = "Default (No Independent Position Check)"; }
                    if (CAT21_Message[Position][2] == '1') { IPC = "Independent Position Check Failed"; }

                    if (CAT21_Message[Position][3] == '0') { NOGO = "NOGO Bit Not Set"; }
                    if (CAT21_Message[Position][3] == '1') { NOGO = "NOGO Bit Set"; }

                    if (CAT21_Message[Position][4] == '0') { CPR = "CPR Validation Correct"; }
                    if (CAT21_Message[Position][4] == '1') { CPR = "CPR Validation Failed"; }

                    if (CAT21_Message[Position][5] == '0') { LDPJ = "LDPJ Not Detected"; }
                    if (CAT21_Message[Position][5] == '1') { LDPJ = "LDPJ Detected"; }

                    if (CAT21_Message[Position][6] == '0') { RCF = "Default"; }
                    if (CAT21_Message[Position][6] == '1') { RCF = "Range Check Failed"; }

                    Position++;
                }
            }


            return Position;
        }

        #endregion

        #region Item I021/070  Mode-3/A Code in Octal Representation

        public string Mode3_A_reply;
        private int Decode_Mode_3A(int Position, string[] CAT21_Message)
        {
            //Binary to octet transformation:
            string Full_Reply_bin = Convert.ToString(CAT21_Message[Position]) + Convert.ToString(CAT21_Message[Position + 1]);
            int Octet_A_bin = Convert.ToInt32(Convert.ToString(Full_Reply_bin[4] + Full_Reply_bin[5] + Full_Reply_bin[6]), 2);
            int Octet_B_bin = Convert.ToInt32(Convert.ToString(Full_Reply_bin[7] + Full_Reply_bin[8] + Full_Reply_bin[9]), 2);
            int Octet_C_bin = Convert.ToInt32(Convert.ToString(Full_Reply_bin[10] + Full_Reply_bin[11] + Full_Reply_bin[12]), 2);
            int Octet_D_bin = Convert.ToInt32(Convert.ToString(Full_Reply_bin[13] + Full_Reply_bin[14] + Full_Reply_bin[15]), 2);

            Mode3_A_reply = Convert.ToString(Octet_A_bin + Octet_B_bin + Octet_C_bin + Octet_D_bin);
            Position++;

            return Position;
        }

        #endregion

        #region Item I021/071 Time of Aplicability for Position

        public string ToA_Position;

        private int Decode_ToA_Position(int Position, string[] CAT21_Message)
        {
            float ToA_Position_seconds = Convert.ToInt32(CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2]) * 1 / 128;

            double ToA_Position_minutes = ToA_Position_seconds / 60;

            int ToA_Position_hours = Convert.ToInt32(ToA_Position_minutes / 60);

            ToA_Position_minutes = ToA_Position_minutes % 60;

            ToA_Position_seconds = ToA_Position_seconds % 60;

            ToA_Position = Convert.ToString(ToA_Position_hours + ":" + ToA_Position_minutes + ":" + ToA_Position_seconds + " UTC");

            Position = Position + 3;

            return Position;
        }

        #endregion

        #region Item I021/072 Time of Aplicability for Velocity

        public string ToA_Velocity;

        private int Decode_ToA_Velocity(int Position, string[] CAT21_Message)
        {
            float ToA_Velocity_seconds = Convert.ToInt32(CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2]) * 1 / 128;

            double ToA_Velocity_minutes = ToA_Velocity_seconds / 60;

            int ToA_Velocity_hours = Convert.ToInt32(ToA_Velocity_minutes / 60);

            ToA_Velocity_minutes = ToA_Velocity_minutes % 60;

            ToA_Velocity_seconds = ToA_Velocity_seconds % 60;

            ToA_Velocity = Convert.ToString(ToA_Velocity_hours + ":" + ToA_Velocity_minutes + ":" + ToA_Velocity_seconds + " UTC");

            Position = Position + 3;

            return Position;

        }

        #endregion

        #region Item I021/073 Time of Message Reception for Position

        public string ToMR_Position; public int ToMR_Position_FS;

        private int Decode_ToMR_Position(int Position, string[] CAT21_Message)
        {
            float ToMR_Position_seconds = Convert.ToInt32(CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2]) * 1 / 128;

            double ToMR_Position_minutes = ToMR_Position_seconds / 60;

            int ToMR_Position_hours = Convert.ToInt32(ToMR_Position_minutes / 60);

            ToMR_Position_minutes = ToMR_Position_minutes % 60;

            ToMR_Position_seconds = ToMR_Position_seconds % 60;

            ToMR_Position_FS = (Convert.ToInt32(ToMR_Position_seconds));

            ToMR_Position = Convert.ToString(ToMR_Position_hours + ":" + ToMR_Position_minutes + ":" + ToMR_Position_seconds + " UTC");

            Position = Position + 3;

            return Position;

        }

        #endregion

        #region Item I021/074 Time of Message Reception of Position - High Precision

        public string FSI_Position; public string ToMR_Position_HP;

        private int Decode_ToMR_Position_HP(int Position, string[] CAT21_Message)
        {
            string FSI_bin = Convert.ToString(Convert.ToInt32(CAT21_Message[Position][0] + CAT21_Message[Position][1]));

            if (FSI_bin == "00") { FSI_Position = Convert.ToString(ToMR_Position_FS); }
            if (FSI_bin == "01") { FSI_Position = Convert.ToString(ToMR_Position_FS + 1); }
            if (FSI_bin == "10") { FSI_Position = Convert.ToString(ToMR_Position_FS - 1); }
            if (FSI_bin == "11") { FSI_Position = "Reserved"; }

            string ToMR_Position_HP_bin = Convert.ToString(CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2] + CAT21_Message[Position + 3]);

            string ToMR_Fract_bin = "";

            for (int i = 2; i < ToMR_Position_HP_bin.LongCount(); i++)
            {
                ToMR_Fract_bin = ToMR_Fract_bin + Convert.ToString(ToMR_Position_HP_bin[i]);
            }

            ToMR_Position_HP = FSI_Position + ":" + Convert.ToString(Convert.ToDouble(ToMR_Fract_bin) * 0.9313e-6) + " [seconds:miliseconds]";

            Position = Position + 4;

            return Position;
        }

        #endregion

        #region Item I021/075 Time of Message Reception for Velocity

        public string ToMR_Velocity; public int ToMR_Velocity_FS;
        private int Decode_ToMR_Velocity(int Position, string[] CAT21_Message)
        {
            float ToMR_Velocity_seconds = Convert.ToInt32(CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2]) * 1 / 128;

            double ToMR_Velocity_minutes = ToMR_Velocity_seconds / 60;

            int ToMR_Velocity_hours = Convert.ToInt32(ToMR_Velocity_minutes / 60);

            ToMR_Velocity_minutes = ToMR_Velocity_minutes % 60;

            ToMR_Velocity_seconds = ToMR_Velocity_seconds % 60;

            ToMR_Velocity_FS = (Convert.ToInt32(ToMR_Velocity_seconds));

            ToMR_Velocity = Convert.ToString(ToMR_Velocity_hours + ":" + ToMR_Velocity_minutes + ":" + ToMR_Velocity_seconds + " UTC");

            Position = Position + 3;

            return Position;
        }

        #endregion

        #region Item I021/076 Time of Message Reception of Velocity - High Precision

        public string FSI_Velocity; public string ToMR_Velocity_HP;

        private int Decode_ToMR_Velocity_HP(int Position, string[] CAT21_Message)
        {
            string FSI_bin = Convert.ToString(Convert.ToInt32(CAT21_Message[Position][0] + CAT21_Message[Position][1]));

            if (FSI_bin == "00") { FSI_Velocity = Convert.ToString(ToMR_Velocity_FS); }
            if (FSI_bin == "01") { FSI_Velocity = Convert.ToString(ToMR_Velocity_FS + 1); }
            if (FSI_bin == "10") { FSI_Velocity = Convert.ToString(ToMR_Velocity_FS - 1); }
            if (FSI_bin == "11") { FSI_Velocity = "Reserved"; }

            string ToMR_Velocity_HP_bin = Convert.ToString(CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2] + CAT21_Message[Position + 3]);

            string ToMR_Fract_bin = "";

            for (int i = 2; i < ToMR_Velocity_HP_bin.LongCount(); i++)
            {
                ToMR_Fract_bin = ToMR_Fract_bin + Convert.ToString(ToMR_Velocity_HP_bin[i]);
            }

            ToMR_Velocity_HP = FSI_Velocity + ":" + Convert.ToString(Convert.ToDouble(ToMR_Fract_bin) * 0.9313e-6) + " [seconds:miliseconds]";

            Position = Position + 4;

            return Position;
        }

        #endregion

        #region Item I021/077 Time of ASTERIX Report Transmission

        public string ToART;
        private int Decode_ToART(int Position, string[] CAT21_Message)
        {
            float ToART_seconds = Convert.ToInt32(CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2]) * 1 / 128;

            double ToART_minutes = ToART_seconds / 60;

            int ToART_hours = Convert.ToInt32(ToART_minutes / 60);

            ToART_minutes = ToART_minutes % 60;

            ToART_seconds = ToART_seconds % 60;

            ToART = Convert.ToString(ToART_hours + ":" + ToART_minutes + ":" + ToART_seconds + " UTC");

            Position = Position + 3;

            return Position;
        }

        #endregion

        #region Item I021/080 Target Address

        public string Target_Address;

        private int Decode_Target_Address(int Position, string[] CAT21_Message)
        {
            string Target_Address_bin = CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2];

            Target_Address = Convert.ToString(Convert.ToInt32(Target_Address_bin));

            Position = Position + 3;

            return Position;
        }

        #endregion

        #region Item I021/090 Quality Indicators

        //Main Structure Data
        public string NUCr_or_NACv; public string NUCp_or_NIC;

        //First Extension Data
        public string NIC_baro; public string SIL; public string NACp;

        //Second Extension Data
        public string SIL_supplement; public string SDA; public string GVA;

        //Third Extension Data
        public string PIC;
        private int Decode_Quality_Indicators(int Position, string[] CAT21_Message)
        {
            string NUCr_or_NACv_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][0] + CAT21_Message[Position][1] + CAT21_Message[Position][2]), 2));

            if (NUCr_or_NACv_int == "0") { NUCr_or_NACv = "Horizontal Velocity Error ≥ 10 m/s"; }
            if (NUCr_or_NACv_int == "1") { NUCr_or_NACv = "Horizontal Velocity Error < 10 m/s"; }
            if (NUCr_or_NACv_int == "2") { NUCr_or_NACv = "Horizontal Velocity Error < 3 m/s"; }
            if (NUCr_or_NACv_int == "3") { NUCr_or_NACv = "Horizontal Velocity Error < 1 m/s"; }
            if (NUCr_or_NACv_int == "4") { NUCr_or_NACv = "Horizontal Velocity Error < 0.3 m/s"; }

            string NUCp_or_NIC_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][3] + CAT21_Message[Position][4] + CAT21_Message[Position][5] + CAT21_Message[Position][6]), 2));

            if (NUCp_or_NIC_int == "0") { NUCp_or_NIC = "Rc Unknown"; }
            if (NUCp_or_NIC_int == "1") { NUCp_or_NIC = "Rc < 20 NM (37.04 km)"; }
            if (NUCp_or_NIC_int == "2") { NUCp_or_NIC = "Rc < 8 NM (14.816 km)"; }
            if (NUCp_or_NIC_int == "3") { NUCp_or_NIC = "Rc < 4 NM (7.408 km)"; }
            if (NUCp_or_NIC_int == "4") { NUCp_or_NIC = "Rc < 2 NM (3.704 km)"; }
            if (NUCp_or_NIC_int == "5") { NUCp_or_NIC = "Rc < 1 NM (1852 m)"; }
            if (NUCp_or_NIC_int == "6") { NUCp_or_NIC = "Rc < 0.5 NM (926 m)"; }
            if (NUCp_or_NIC_int == "7") { NUCp_or_NIC = "Rc < 0.2 NM (370.4 m)"; }
            if (NUCp_or_NIC_int == "8") { NUCp_or_NIC = "Rc < 0.1 NM (185.2 m)"; }
            if (NUCp_or_NIC_int == "9") { NUCp_or_NIC = "Rc < 75 m"; }
            if (NUCp_or_NIC_int == "10") { NUCp_or_NIC = "Rc < 25 m"; }
            if (NUCp_or_NIC_int == "11") { NUCp_or_NIC = "Rc < 7.5 m"; }

            Position++;

            while (CAT21_Message[Position - 1].Last() == '1')
            {
                NIC_baro = Convert.ToString(Convert.ToInt32(CAT21_Message[Position][0]));

                string SIL_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][1]) + Convert.ToString(CAT21_Message[Position][2]) , 2));
                
                if (SIL_int == "0") { SIL = "SIL 0, Probability of exceeding the NIC Containment Radius: Unknown or > 1 x 10e-3 per flight hour or per sample"; }
                if (SIL_int == "1") { SIL = "SIL 1, Probability of exceeding the NIC Containment Radius: ≤ 1 x 10e-3 per flight hour or per sample"; }
                if (SIL_int == "2") { SIL = "SIL 2, Probability of exceeding the NIC Containment Radius: ≤ 1 x 10e-5 per flight hour or per sample"; }
                if (SIL_int == "3") { SIL = "SIL 3, Probability of exceeding the NIC Containment Radius: ≤ 1 x 10e-7 per flight hour or per sample"; }

                string NACp_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][3] + CAT21_Message[Position][4] + CAT21_Message[Position][5] + CAT21_Message[Position][6]), 2));

                if (NACp_int == "0") { NACp = "95% Horizontal Accuracy Bounds (EPU) ≥ 18.52 km (≥10NM); Unknown Accuracy"; }
                if (NACp_int == "1") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 18.52 km (10NM); RNP-10 Accuracy"; }
                if (NACp_int == "2") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 7.408 km (4NM); RNP-4 Accuracy"; }
                if (NACp_int == "3") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 3.704 km (2NM); RNP-2 Accuracy"; }
                if (NACp_int == "4") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 1852 m (1NM); RNP-1 Accuracy"; }
                if (NACp_int == "5") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 926 m (0.5NM); RNP-0.5 Accuracy"; }
                if (NACp_int == "6") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 555.6 m (0.3); RNP-0.3 Accuracy"; }
                if (NACp_int == "7") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 185.2 m (0.1NM); RNP-0.1 Accuracy"; }
                if (NACp_int == "8") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 92.6 m (0.05NM); e.g., GPS (with SA on)"; }
                if (NACp_int == "9") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 30 m; e.g., GPS (SA off)"; }
                if (NACp_int == "10") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 10 m; e.g., WAAS"; }
                if (NACp_int == "11") { NACp = "95% Horizontal Accuracy Bounds (EPU) < 3 m; e.g., LAAS"; }
                if (NACp_int == "12") { NACp = "Reserved"; }
                if (NACp_int == "13") { NACp = "Reserved"; }
                if (NACp_int == "14") { NACp = "Reserved"; }
                if (NACp_int == "15") { NACp = "Reserved"; }

                Position++;

                while (CAT21_Message[Position - 1].Last() == '1')
                {
                    if (CAT21_Message[Position][0] == '0') { SIL_supplement = "Measured per Flight Hour"; }
                    if (CAT21_Message[Position][1] == '1') { SIL_supplement = "Measured per Sample"; }

                    string SDA_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][3] + CAT21_Message[Position][4]), 2));

                    if (SDA_int == "0") { SDA = "SDA Level: N/A, Supported Failed Condition: Unknown/No Safety Effect; Probability of Undetected Fault causing transmission of False or Misleading Information: > 1 x 10e-3 per Flight Hour or Unknown"; }
                    if (SDA_int == "1") { SDA = "SDA Level: D, Supported Failed Condition: Minor; Probability of Undetected Fault causing transmission of False or Misleading Information: ≤ 1 x 10e-3 per Flight Hour"; }
                    if (SDA_int == "2") { SDA = "SDA Level: C, Supported Failed Condition: Major; Probability of Undetected Fault causing transmission of False or Misleading Information: ≤ 1 x 10e-5 per Flight Hour"; }
                    if (SDA_int == "3") { SDA = "SDA Level: B, Supported Failed Condition: Hazardous; Probability of Undetected Fault causing transmission of False or Misleading Information: ≤ 1 x 10e-7 per Flight Hour"; }

                    string GVA_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][5] + CAT21_Message[Position][6]), 2));

                    if (GVA_int == "0") { GVA = "Geometric Vertical Accuracy: Unknown or > 150 meters"; }
                    if (GVA_int == "1") { GVA = "Geometric Vertical Accuracy: ≤ 150 meters"; }
                    if (GVA_int == "2") { GVA = "Geometric Vertical Accuracy: ≤ 45 meters"; }
                    if (GVA_int == "3") { GVA = "Geometric Vertical Accuracy: Reserved"; }

                    Position++;

                    if (CAT21_Message[Position - 1].Last() == '1')
                    {
                        string PIC_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][0] + CAT21_Message[Position][1] + CAT21_Message[Position][2] + CAT21_Message[Position][3]), 2));

                        if (PIC_int == "0") { PIC = "Integrity Containment Bound: No Integrity (or > 20.0 NM) ; NUCp ED102/DO260: 0; NIC (+ suppl.) DO260A: 0; NIC (+ suppl.'s) ED102A/DO260B: 0"; }
                        if (PIC_int == "1") { PIC = "Integrity Containment Bound < 20.0 NM; NUCp ED102/DO260: 1; NIC (+ suppl.) DO260A: 1; NIC (+ suppl.'s) ED102A/DO260B: 1"; }
                        if (PIC_int == "2") { PIC = "Integrity Containment Bound < 10.0 NM; NUCp ED102/DO260: 2; NIC (+ suppl.) DO260A: -; NIC (+ suppl.'s) ED102A/DO260B: -"; }
                        if (PIC_int == "3") { PIC = "Integrity Containment Bound < 8.0 NM; NUCp ED102/DO260: -; NIC (+ suppl.) DO260A: 2; NIC (+ suppl.'s) ED102A/DO260B: 2"; }
                        if (PIC_int == "4") { PIC = "Integrity Containment Bound < 4.0 NN; NUCp ED102/DO260: - ; NIC (+ suppl.) DO260A: 3; NIC (+ suppl.'s) ED102A/DO260B: 3"; }
                        if (PIC_int == "5") { PIC = "Integrity Containment Bound < 2.0 NM; NUCp ED102/DO260: 3; NIC (+ suppl.) DO260A: 4; NIC (+ suppl.'s) ED102A/DO260B: 4"; }
                        if (PIC_int == "6") { PIC = "Integrity Containment Bound < 1.0 NM; NUCp ED102/DO260: 4; NIC (+ suppl.) DO260A: 5; NIC (+ suppl.'s) ED102A/DO260B: 5"; }
                        if (PIC_int == "7") { PIC = "Integrity Containment Bound < 0.6 NM; NUCp ED102/DO260: -; NIC (+ suppl.) DO260A: 6 (+ 1); NIC (+ suppl.'s) ED102A/DO260B: 6 (+ 1/1)"; }
                        if (PIC_int == "8") { PIC = "Integrity Containment Bound < 0.5 NM; NUCp ED102/DO260: 5; NIC (+ suppl.) DO260A: 6 (+ 0); NIC (+ suppl.'s) ED102A/DO260B: 6 (+ 0/0)"; }
                        if (PIC_int == "9") { PIC = "Integrity Containment Bound < 0.3 NM; NUCp ED102/DO260: -; NIC (+ suppl.) DO260A: -; NIC (+ suppl.'s) ED102A/DO260B: 6 (+ 0/1)"; }
                        if (PIC_int == "10") { PIC = "Integrity Containment Bound < 0.2 NM; NUCp ED102/DO260: 6; NIC (+ suppl.) DO260A: 7; NIC (+ suppl.'s) ED102A/DO260B: 7"; }
                        if (PIC_int == "11") { PIC = "Integrity Containment Bound < 0.1 NM; NUCp ED102/DO260: 7; NIC (+ suppl.) DO260A: 8; NIC (+ suppl.'s) ED102A/DO260B: 8"; }
                        if (PIC_int == "12") { PIC = "Integrity Containment Bound < 0.04 NM; NUCp ED102/DO260: -; NIC (+ suppl.) DO260A: 9; NIC (+ suppl.'s) ED102A/DO260B: 9"; }
                        if (PIC_int == "13") { PIC = "Integrity Containment Bound < 0.013 NM; NUCp ED102/DO260 8; NIC (+ suppl.) DO260A: 10; NIC (+ suppl.'s) ED102A/DO260B: 10"; }
                        if (PIC_int == "14") { PIC = "Integrity Containment Bound < 0.004 NM; NUCp ED102/DO260: 9; NIC (+ suppl.) DO260A: 11; NIC (+ suppl.'s) ED102A/DO260B: 11"; }
                        if (PIC_int == "15") { PIC = "Not Defined"; }

                        Position++;
                    }
                }

            }

            return Position;
        }

        #endregion

        #region Item I021/110 Trajectory Intent (PENDIENTE)

        //Main Structure Data

        //N/A

        //First Subfield Data

        public string NAV; public string NVB;

        //Second Subfield Data

        public string REP_TI;

        public string[] TI_Points = new string[] {};

        private int Decode_Trajectory_Intent(int Position, string[] CAT21_Message)
        {
            string TIS_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][0], 2)));

            string TID_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][1], 2)));

            Position++;

            while (CAT21_Message[Position - 1].Last() == '1')
               
            {
                if (TIS_int == "1")
                {
                    if (CAT21_Message[Position][0] == '0') { NAV = "Trajectory Intent Data is Available for This Aircraft"; }
                    if (CAT21_Message[Position][0] == '1') { NAV = "Trajectory Intent Data is Not Available for This Aircraft"; }

                    if (CAT21_Message[Position][1] == '0') { NVB = "Trajectory Intent Data is Valid"; }
                    if (CAT21_Message[Position][1] == '1') { NVB = "Trajectory Intent Data is Not Valid"; } 
                }

                if (TID_int == "1" || TIS_int == "1")
                {

                }
            }

            return Position;
        }

        #endregion

        #region Item I021/130 Position in WGS-84 Co-ordinates

        public string Lat_WGS84; public string Lon_WGS84;

        private int Decode_Position_WGS(int Position, string[] CAT21_Message)
        {
            string Lat_WGS84_bin = Convert.ToString(CAT21_Message[Position] + CAT21_Message[Position + 1] + CAT21_Message[Position + 2]);

            string Lon_WGS84_bin = Convert.ToString(CAT21_Message[Position + 3] + CAT21_Message[Position + 4] + CAT21_Message[Position + 5]);

            if (Lat_WGS84_bin[0] == '0') { Lat_WGS84 = Convert.ToString(Convert.ToInt32(Lat_WGS84_bin)); }
            if (Lat_WGS84_bin[0] == '1')
            {
                Lat_WGS84 = Convert.ToString(Library.twos_complement(Lat_WGS84_bin) * (180 / Math.Pow(2, 23)));
            }
            if (Lon_WGS84_bin[0] == '0') { Lon_WGS84 = Convert.ToString(Convert.ToInt32(Lon_WGS84_bin)); }
            if (Lon_WGS84_bin[0] == '1')
            {
                Lon_WGS84 = Convert.ToString(Library.twos_complement(Lon_WGS84_bin) * (180 / Math.Pow(2, 23)));
            }

            Position = Position + 6;

            return Position;
        }

        #endregion

        #region Item I021/131 High-Resolution Position in WGS-84 Co-ordinates

        public string Lat_WGS84_HP; public string Lon_WGS84_HP;
        private int Decode_Position_WGS84(int Position, string[] CAT10_Message)
        {
            string Lat_WGS84_HP_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1] + CAT10_Message[Position + 2] + CAT10_Message[Position + 3]);
            Position = Position + 4;
            string Lon_WGS84_HP_bin = Convert.ToString(CAT10_Message[Position] + CAT10_Message[Position + 1] + CAT10_Message[Position + 2] + CAT10_Message[Position + 3]);
            Position = Position + 4;

            if (Lat_WGS84_HP_bin[0] == '0') { Lat_WGS84_HP = Convert.ToString(Convert.ToInt32(Lat_WGS84_HP_bin)); }
            if (Lat_WGS84_HP_bin[0] == '1')
            {
                Lat_WGS84_HP = Convert.ToString(Library.twos_complement(Lat_WGS84_HP_bin) * (180 / Math.Pow(2, 30)));
            }
            if (Lon_WGS84_HP_bin[0] == '0') { Lon_WGS84_HP = Convert.ToString(Convert.ToInt32(Lon_WGS84_HP_bin)); }
            if (Lon_WGS84_HP_bin[0] == '1')
            {
                Lon_WGS84_HP = Convert.ToString(Library.twos_complement(Lon_WGS84_HP_bin) * (180 / Math.Pow(2, 30)));
            }
            return Position;
        }

        #endregion

        #region Item I021/132 Message Amplitude

        public string MAM;

        private int Decode_Message_Amplitude(int Position, string[] CAT21_Message)
        {
            MAM = Convert.ToString(Convert.ToInt32(CAT21_Message[Position])) + " dBm";

            Position++;

            return Position;
        }

        #endregion

        #region Item I021/140 Geometric Height

        public string GH;

        private int Decode_Geometric_Height(int Position, string[] CAT21_Message)
        {
            string GH_bin = Convert.ToString(CAT21_Message[Position] + CAT21_Message[Position + 1]);

            if (GH_bin[0] == '0') 
            {
                string GH_int = Convert.ToString(Convert.ToInt32(GH_bin) * 6.25);

                if (GH_bin == "0111111111111111") { GH = "Greater Than" + GH_int + " ft"; }

                else { GH = GH_int + "ft"; }
            }

            if (GH_bin[0] == '1') { GH = Convert.ToString(Library.twos_complement(GH_bin) * 6.25); }

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/145 Flight Level

        public string FL;

        private int Decode_Flight_Level(int Position, string[] CAT21_Message)
        {
            string FL_bin = Convert.ToString(CAT21_Message[Position] + CAT21_Message[Position + 1]);

            if (FL_bin[0] == '0') { FL = Convert.ToString(Convert.ToInt32(FL_bin) * 0.25); }

            if (FL_bin[0] == '1') { FL = Convert.ToString(Library.twos_complement(FL_bin) * 0.25); }

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/146 Selected Altitude

        public string SAS; public string Source; public string Altitude;

        private int Decode_Selected_Altitude(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][0] == '0') { SAS = "No Source Information Provided"; }
            if (CAT21_Message[Position][0] == '1') { SAS = "Source Information Provided"; }

            string Source_bin = Convert.ToString(CAT21_Message[Position][1]) + Convert.ToString(CAT21_Message[Position][2]);

            if (Source_bin == "00") { Source = "Unknown"; }
            if (Source_bin == "01") { Source = "Aircraft Altitude (Holding Altitude)"; }
            if (Source_bin == "10") { Source = "MCP/FCU Selected Altitude"; }
            if (Source_bin == "11") { Source = "FMS Selected Altitude"; }

            string Selected_Altitude_bin = Convert.ToString(CAT21_Message[Position] + CAT21_Message[Position + 1]);
            string Altitude_bin = "";

            for (int i = 2; i < Selected_Altitude_bin.LongCount(); i++)
            {
                Altitude_bin = Altitude_bin + Selected_Altitude_bin[i];
            }

            if (Altitude_bin[0] == '0') { Altitude = Convert.ToString(Convert.ToInt32(Altitude_bin) * 25); }
            if (Altitude_bin[0] == '1') { Altitude = Convert.ToString(Library.twos_complement(Altitude_bin) * 25); }

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/148 Final State Selected Altitude

        public string MV; public string AH; public string AM; public string FSS_Altitude;
        private int Decode_Final_State_Selected_Altitude(int Postion, string[] CAT21_Message)
        {
            if (CAT21_Message[Postion][0] == '0') { MV = "Not Active or Unknown"; }
            if (CAT21_Message[Postion][0] == '1') { MV = "Active"; }

            if (CAT21_Message[Postion][1] == '0') { AH = "Not Active or Unknown"; }
            if (CAT21_Message[Postion][1] == '1') { AH = "Active"; }

            if (CAT21_Message[Postion][2] == '0') { AM = "Not Active or Unknown"; }
            if (CAT21_Message[Postion][2] == '1') { AM = "Active"; }

            string FSS_Altitude_bin = "";
            string FS_Selected_Altitude_bin = Convert.ToString(CAT21_Message[Postion] + CAT21_Message[Postion + 1]);

            for (int i = 3; i < FS_Selected_Altitude_bin.LongCount(); i++)
            {
                FSS_Altitude_bin = FSS_Altitude_bin + FS_Selected_Altitude_bin[i];
            }

            if (FSS_Altitude_bin[0] == '0') { FSS_Altitude = Convert.ToString(Convert.ToInt32(FSS_Altitude_bin) * 25); }
            if (FSS_Altitude_bin[0] == '1') { FSS_Altitude = Convert.ToString(Library.twos_complement(FSS_Altitude_bin) * 25); }

            Postion = Postion + 2;

            return Postion;
        }

        #endregion

        #region Item I021/150 Air Speed

        public string IM; public string Air_Speed;

        private int Decode_Air_Speed(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][0] == '0') { IM = "IAS"; }
            if (CAT21_Message[Position][0] == '1') { IM = "Mach"; }

            double LSB = 0;
            string units = "";

            if (IM == "IAS") { LSB = Math.Pow(2, -14); units = "NM/s"; }
            if (IM == "Mach") { LSB = 0.001; units = "Mach"; }

            string Air_Speed_bin = "";
            string Message_bin = Convert.ToString(CAT21_Message[Position] + CAT21_Message[Position + 1]);

            for (int i = 1; i < Message_bin.LongCount(); i++)
            {
                Air_Speed_bin = Air_Speed_bin + Message_bin[i];
            }

            Air_Speed = Convert.ToString(Convert.ToInt32(Air_Speed_bin) * LSB) + units;

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/151 True Airspeed

        public string TAS; public string RE;

        private int Decode_True_Airspeed(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][0] == '0') { RE = "Value in Defined Range"; }
            if (CAT21_Message[Position][0] == '1') { RE = "Value Exceeds Defined Range"; }

            string Message_bin = CAT21_Message[Position] + CAT21_Message[Position + 1];

            string TAS_bin = "";

            for (int i = 1; i < Message_bin.LongCount(); i++)
            {
                TAS_bin = TAS_bin + Message_bin[i];
            }

            TAS = Convert.ToString(Convert.ToInt32(TAS_bin)) + " kts";

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/152 Magnetic Heading

        public string MH;

        private int Decode_Magnetic_Heading(int Position, string[] CAT21_Message)
        {
            string MH_bin = CAT21_Message[Position] + CAT21_Message[Position + 1];

            MH = Convert.ToString(Convert.ToInt32(MH_bin) * (360 / Math.Pow(2, 16))) + "º";

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/155 Barometric Vertical Rate

        public string RE_BVR; public string BVR;

        private int Decode_Barometric_Vertical_Rate(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][0] == '0') { RE_BVR = "Value in Defined Range"; }
            if (CAT21_Message[Position][0] == '1') { RE_BVR = "Value Exceeds Defined Range"; }

            string Message_bin = CAT21_Message[Position] + CAT21_Message[Position + 1];

            string BVR_bin = "";

            for (int i = 1; i < Message_bin.LongCount(); i++)
            {
                BVR_bin = BVR_bin + Message_bin[i];
            }

            if (BVR_bin[0] == '0') { BVR = Convert.ToString(Convert.ToInt32(BVR_bin) * 6.25); }
            if (BVR_bin[0] == '1') { BVR = Convert.ToString(Library.twos_complement(BVR_bin) * 6.25); }

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/157 Geometric Vertical Rate

        public string RE_GVR; public string GVR;

        private int Decode_Geometric_Vertical_Rate(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][0] == '0') { RE_GVR = "Value in Defined Range"; }
            if (CAT21_Message[Position][0] == '1') { RE_GVR = "Value Exceeds Defined Range"; }

            string Message_bin = CAT21_Message[Position] + CAT21_Message[Position + 1];

            string GVR_bin = "";

            for (int i = 1; i < Message_bin.LongCount(); i++)
            {
                GVR_bin = GVR_bin + Message_bin[i];
            }

            if (GVR_bin[0] == '0') { BVR = Convert.ToString(Convert.ToInt32(GVR_bin) * 6.25); }
            if (GVR_bin[0] == '1') { BVR = Convert.ToString(Library.twos_complement(GVR_bin) * 6.25); }

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/160 Airborne Ground Vector

        public string RE_AGV; public string GS; public string TA;

        private int Decode_Airborne_Ground_Vector(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][0] == '0') { RE_AGV = "Value in Defined Range"; }
            if (CAT21_Message[Position][0] == '1') { RE_AGV = "Value Exceeds Defined Range"; }

            string First_Octet_Pair_bin = CAT21_Message[Position] + CAT21_Message[Position + 1];

            string GS_bin = "";

            for (int i = 1; i < First_Octet_Pair_bin.LongCount(); i++)
            {
                GS_bin = GS_bin + First_Octet_Pair_bin[i];
            }

            GS = Convert.ToString(Convert.ToInt32(GS_bin) * (Math.Pow(2, -14))) + " kts";

            string TA_bin = CAT21_Message[Position + 2] + CAT21_Message[Position + 3];

            TA = Convert.ToString(Convert.ToInt32(TA_bin) * (360 / Math.Pow(2, 16))) + "º";

            Position = Position + 4;

            return Position;
        }

        #endregion

        #region Item I021/161 Track Number

        public string TN;

        private int Decode_Track_Number(int Position, string[] CAT21_Message)
        {
            string Message_bin = CAT21_Message[Position] + CAT21_Message[Position + 1];

            string TN_bin = "";

            for (int i = 4; i < Message_bin.LongCount(); i++)
            {
                TN_bin = TN_bin + Message_bin[i];
            }

            TN = Convert.ToString(Convert.ToInt32(TN_bin));

            Position = Position + 2;

            return Position;
        }

        #endregion

        #region Item I021/165 Track Angle Rate

        public string TAR;

        private int Decode_Track_Angle_Rate(int Position, string[] CAT21_Message)
        {
            string Message_bin = CAT21_Message[Position] + CAT21_Message[Position + 1];

            string TAR_bin = "";

            for (int i = 5; i < Message_bin.LongCount(); i++)
            {
                TAR_bin = TAR_bin + Message_bin[i];
            }

            if (TAR_bin[0] == '0') { TAR = Convert.ToString(Convert.ToInt32(TAR_bin) * (1 / 32)); }
            if (TAR_bin[0] == '1') { TAR = Convert.ToString(Library.twos_complement(TAR_bin) * (1 / 32)); }

            return Position;
        }

        #endregion

        #region Item I021/170 Target Identification (RESCATAR CAT 10)

        private int Decode_Target_Identification(int Position, string[] CAT21_Message)
        {
            return Position;
        }

        #endregion

        #region Item I021/200 Target Status

        public string ICF; public string LNAV; public string PS; public string SS;

        private int Decode_Target_Status(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][0] == '0') { ICF = "No Intent Change Active"; }
            if (CAT21_Message[Position][0] == '1') { ICF = "Intent Change Flag Raised"; }

            if (CAT21_Message[Position][1] == '0') { LNAV = "LNAV Mode Engaged"; }
            if (CAT21_Message[Position][1] == '1') { LNAV = "LNAV Mode Not Engaged"; }

            string PS_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][3]) + Convert.ToString(CAT21_Message[Position][4]) + Convert.ToString(CAT21_Message[Position][5])));

            if (PS_int == "0") { PS = "No Emergency / Not Reported"; }
            if (PS_int == "1") { PS = "General Emergency"; }
            if (PS_int == "2") { PS = "Lifeguard / Medical Emergency"; }
            if (PS_int == "3") { PS = "Minimum Fuel"; }
            if (PS_int == "4") { PS = "No Communications"; }
            if (PS_int == "5") { PS = "Unlawful Interference"; }
            if (PS_int == "6") { PS = "'Downed' Aircraft"; }

            string SS_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][6]) + Convert.ToString(CAT21_Message[Position][7])));

            if (SS_int == "0") { SS = "No Condition Reported"; }
            if (SS_int == "1") { SS = "Permanent Alert (Emergency Condition)"; }
            if (SS_int == "2") { SS = "Temporary Alert (Change in Mode 3/A Code Other Than Emergency"; }
            if (SS_int == "3") { SS = "SPI Set"; }

            Position++;

            return Position;

        }

        #endregion

        #region Item I021/210 MOPS Version

        public string VNS; public string VN; public string LTT;

        private int Decode_MOPS_Version(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][1] == '0') { VNS = "The MOPS Version is Supported by the GS"; }
            if (CAT21_Message[Position][1] == '1') { VNS = "The MOPS Version is NOT Supported by the GS"; }

            string VN_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][2]) + Convert.ToString(CAT21_Message[Position][3]) + Convert.ToString(CAT21_Message[Position][4])));

            if (VN_int == "0") { VN = "ED102/DO-260 [Ref. 8]"; }
            if (VN_int == "1") { VN = "DO-260A [Ref. 9]"; }
            if (VN_int == "2") { VN = "ED102A/DO-260B [Ref. 11]"; }

            string LTT_int = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][5]) + Convert.ToString(CAT21_Message[Position][6]) + Convert.ToString(CAT21_Message[Position][7])));

            if (LTT_int == "0") { LTT = "Other"; }
            if (LTT_int == "1") { LTT = "UAT"; }
            if (LTT_int == "2") { LTT = "1090 ES"; }
            if (LTT_int == "3") { LTT = "VDL 4"; }
            if (LTT_int == "4" || LTT_int == "5" || LTT_int == "6" || LTT_int == "7") { LTT = "Not Assigned"; }

            Position++;

            return Position;
        }

        #endregion

        #region Item I021/220 Met Information (ACABAR LSB)

        public string Wind_Speed; public string Wind_Direction; public string Temperature; public string Turbulence;

        private int Decode_Met_Information(int Position, string[] CAT21_Message)
        {
            string WS = Convert.ToString(CAT21_Message[Position][0]);

            string WD = Convert.ToString(CAT21_Message[Position][1]);

            string TMP = Convert.ToString(CAT21_Message[Position][2]);

            string TRB = Convert.ToString(CAT21_Message[Position][3]);

            if (CAT21_Message[Position].Last() == '1')
            {
                if (WS == "1")
                {
                    Wind_Speed = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 1] + CAT21_Message[Position + 2]));
                }

                if (WD == "1")
                {
                    if (WS == "0") { Wind_Direction = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 1] + CAT21_Message[Position + 2])); }
                    if (WS == "1") { Wind_Direction = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 3] + CAT21_Message[Position + 4])); }
                }

                if (TMP == "1")
                {
                    if (WS == "0" && WD == "0") { Temperature = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 1] + CAT21_Message[Position + 2])); }
                    if ((WS == "0" && WD == "1") || (WS == "1" && WD == "0")) { Temperature = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 3] + CAT21_Message[Position + 4])); }
                    if (WS == "1" && WD == "1") { Temperature = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 5] + CAT21_Message[Position + 6])); }
                } 
                
                if (TRB == "1")
                {
                    if (WS == "0" && WD == "0" && TMP == "0") { Turbulence = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 1])); }
                    if (WS == "0" && WD == "0" && TMP == "1" || WS == "0" && TMP == "0" && WD == "11" || WD == "0" && TMP == "0" && WS == "1") { Turbulence = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 3])); }
                    if (WS == "1" && WD == "1" && TMP == "0" || WS == "1" && TMP == "1" && WD == "0" || WD == "1" && TMP == "1" && WS == "0") { Turbulence = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 5])); }
                    if (WS == "1" && WD == "1" && TMP == "1") { Turbulence = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 7])); }
                }
            }

            return Position;
        }

        #endregion

        #region Item I021/230 Roll Angle

        public string Roll;

        private int Decode_Roll_Angle(int Position, string[] CAT21_Message)
        {
            Roll = Convert.ToString(Convert.ToInt32(CAT21_Message[Position] + CAT21_Message[Position + 1]) * 0.01);

            Position += 2;

            return Position;
        }

        #endregion

        #region Item I021/250 Mode S MB Data (RESCATAR CAT10)

        public string REP;

        private int Decode_Mode_S_MB_Data(int Position, string[] CAT21_Message)
        {
            return Position;
        }

        #endregion

        #region Item I021/260 ACAS Resolution Advisory Report

        public string TYP; public string STYP; public string ARA;

        public string RAC; public string RAT; public string MTE;

        public string TTI; public string TRID;

        private int Decode_ACAS_RAR(int Position, string[] CAT21_Message)
        {
            TYP = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][0]) + Convert.ToString(CAT21_Message[Position][1]) + Convert.ToString(CAT21_Message[Position][2]) + Convert.ToString(CAT21_Message[Position][3]) + Convert.ToString(CAT21_Message[Position][4])));

            STYP = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][5]) + Convert.ToString(CAT21_Message[Position][6]) + Convert.ToString(CAT21_Message[Position][7])));

            ARA = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position + 1]) + Convert.ToString(CAT21_Message[Position + 2][0]) + Convert.ToString(CAT21_Message[Position + 2][1]) + Convert.ToString(CAT21_Message[Position + 2][2]) + Convert.ToString(CAT21_Message[Position + 2][3]) + Convert.ToString(CAT21_Message[Position + 2][4]) + Convert.ToString(CAT21_Message[Position + 2][5])));

            RAC = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position + 2][6]) + Convert.ToString(CAT21_Message[Position + 2][7]) + Convert.ToString(CAT21_Message[Position + 3][0]) + Convert.ToString(CAT21_Message[Position + 3][1])));

            RAT = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 3][2]));

            MTE = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + 3][3]));

            TTI = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position + 3][4]) + Convert.ToString(CAT21_Message[Position + 3][5])));

            TID = Convert.ToString(Convert.ToInt32(Convert.ToSingle(CAT21_Message[Position + 3][6]) + Convert.ToString(CAT21_Message[Position + 3][7]) + CAT21_Message[Position + 4] + CAT21_Message[Position + 5] + CAT21_Message[Position + 6]));

            Position += 7;

            return Position;
        }

        #endregion

        #region Item I021/271 Surface Capabilities and Characteristics

        public string POA; public string CDTI_S; public string B2_LOW;

        public string RAS; public string IDENT; public string L_W;

        private int Decode_Surface_Capabilities_Characteristics(int Position, string[] CAT21_Message)
        {
            if (CAT21_Message[Position][2] == '0') { POA = "Position Transmitted is Not ADS-B Position Reference Point"; }
            if (CAT21_Message[Position][2] == '1') { POA = "Position Transmitted is ADS-B Position Reference Point"; }

            if (CAT21_Message[Position][3] == '0') { CDTI_S = "CDTI Not Operational"; }
            if (CAT21_Message[Position][3] == '1') { CDTI_S = "CDTI Operational"; }

            if (CAT21_Message[Position][4] == '0') { B2_LOW = "≥ 70 Watts"; }
            if (CAT21_Message[Position][4] == '1') { B2_LOW = "< 70 Watts"; }

            if (CAT21_Message[Position][5] == '0') { RAS = "Aircraft Not Receiving ATC Services"; }
            if (CAT21_Message[Position][5] == '1') { RAS = "Aircraft Receiving Services"; }

            if (CAT21_Message[Position][6] == '0') { IDENT = "IDENT Switch Not Active"; }
            if (CAT21_Message[Position][6] == '1') { IDENT = "IDENT Switch Active"; }

            Position++;

            if (CAT21_Message[Position].Last() == '1')
            {
                string L_W_bin = Convert.ToString(Convert.ToInt32(Convert.ToString(CAT21_Message[Position][4]) + Convert.ToString(CAT21_Message[Position][5]) + Convert.ToString(CAT21_Message[Position][6]) + Convert.ToString(CAT21_Message[Position][7])));

                if (L_W_bin == "0") { L_W = "L < 15 [m]; W < 11.5 [m]"; }
                if (L_W_bin == "1") { L_W = "L < 15 [m]; W < 23 [m]"; }
                if (L_W_bin == "2") { L_W = "L < 25 [m]; W < 28.5 [m]"; }
                if (L_W_bin == "3") { L_W = "L < 25 [m]; W < 34 [m]"; }
                if (L_W_bin == "4") { L_W = "L < 35 [m]; W < 33 [m]"; }
                if (L_W_bin == "5") { L_W = "L < 35 [m]; W < 38 [m]"; }
                if (L_W_bin == "6") { L_W = "L < 45 [m]; W < 39.5 [m]"; }
                if (L_W_bin == "7") { L_W = "L < 45 [m]; W < 45 [m]"; }
                if (L_W_bin == "8") { L_W = "L < 55 [m]; W < 45 [m]"; }
                if (L_W_bin == "9") { L_W = "L < 55 [m]; W < 52 [m]"; }
                if (L_W_bin == "10") { L_W = "L < 65 [m]; W < 59.5 [m]"; }
                if (L_W_bin == "11") { L_W = "L < 65 [m]; W < 67 [m]"; }
                if (L_W_bin == "12") { L_W = "L < 75 [m]; W < 72.5 [m]"; }
                if (L_W_bin == "13") { L_W = "L < 75 [m]; W < 80 [m]"; }
                if (L_W_bin == "14") { L_W = "L < 85 [m]; W < 80 [m]"; }
                if (L_W_bin == "15") { L_W = "L > 85 [m] or W > 80 [m]"; }

                Position++;

            }

            return Position;
        }

        #endregion

        #region Item I021/295 Data Ages

        public string AOS_Age; public string TRD_Age; public string M3A_Age; public string QI_Age;

        public string TI_Age; public string MAM_Age; public string GH_Age; public string FL_Age;

        public string ISA_Age; public string FSA_Age; public string AS_Age; public string TAS_Age;

        public string MH_Age; public string BVR_Age; public string GVR_Age; public string GV_Age;

        public string TAR_Age; public string TID_Age; public string TS_Age; public string MET_Age;

        public string ROA_Age; public string ARA_Age; public string SCC_Age;


        string[] Fields;

        string[] Field_Presence = new string[] { };


        private int Decode_Data_Ages(int Position, string[] CAT21_Message)
        {
            string AOS_ = Convert.ToString(CAT21_Message[Position][0]); Field_Presence.Append(AOS_);

            string TRD_ = Convert.ToString(CAT21_Message[Position][1]); Field_Presence.Append(TRD_);

            string M3A_ = Convert.ToString(CAT21_Message[Position][2]); Field_Presence.Append(M3A_);

            string QI_ = Convert.ToString(CAT21_Message[Position][3]); Field_Presence.Append(QI_);

            string TI_ = Convert.ToString(CAT21_Message[Position][4]); Field_Presence.Append(TI_);

            string MAM_ = Convert.ToString(CAT21_Message[Position][5]); Field_Presence.Append(MAM_);

            string GH_ = Convert.ToString(CAT21_Message[Position][6]); Field_Presence.Append(GH_);

            Position++;

            while (CAT21_Message[Position - 1].Last() == '1')
            {
                string FL_ = Convert.ToString(CAT21_Message[Position][0]); Field_Presence.Append(FL_);

                string ISA_ = Convert.ToString(CAT21_Message[Position][1]); Field_Presence.Append(ISA_);

                string FSA_ = Convert.ToString(CAT21_Message[Position][2]); Field_Presence.Append(FSA_);

                string AS_ = Convert.ToString(CAT21_Message[Position][3]); Field_Presence.Append(AS_);

                string TAS_ = Convert.ToString(CAT21_Message[Position][4]); Field_Presence.Append(TAS_);

                string MH_ = Convert.ToString(CAT21_Message[Position][5]); Field_Presence.Append(MH_);

                string BVR_ = Convert.ToString(CAT21_Message[Position][6]); Field_Presence.Append(BVR_);

                Position++;

                while (CAT21_Message[Position - 1].Last() == '1')
                {
                    string GVR_ = Convert.ToString(CAT21_Message[Position][0]); Field_Presence.Append(GVR_);

                    string GV_ = Convert.ToString(CAT21_Message[Position][1]); Field_Presence.Append(GV_);

                    string TAR_ = Convert.ToString(CAT21_Message[Position][2]); Field_Presence.Append(TAR_);

                    string TID_ = Convert.ToString(CAT21_Message[Position][3]); Field_Presence.Append(TID_);

                    string TS_ = Convert.ToString(CAT21_Message[Position][4]); Field_Presence.Append(TS_);

                    string MET_ = Convert.ToString(CAT21_Message[Position][5]); Field_Presence.Append(MET_);

                    string ROA_ = Convert.ToString(CAT21_Message[Position][6]); Field_Presence.Append(ROA_);

                    Position++;

                    if (CAT21_Message[Position - 1].Last() == '1')
                    {
                        string ARA_ = Convert.ToString(CAT21_Message[Position][0]); Field_Presence.Append(ARA_);

                        string SCC_ = Convert.ToString(CAT21_Message[Position][1]); Field_Presence.Append(SCC_);

                        Position++;
                    }
                }
            }


            Fields = new string[] { AOS_Age, TRD_Age, M3A_Age, QI_Age, TI_Age, MAM_Age, GH_Age };

            int Count = 0;

            for (int i = 0; i < Field_Presence.Length; i++)
            {
                if (Field_Presence[i] == "1") { Fields[i] = Convert.ToString(Convert.ToInt32(CAT21_Message[Position + Count]) * 0.1); Count++; }

                if (Field_Presence[i] == "0") { Fields[i] = "N/A"; }
            }

            Position = Position + Count;

            return Position;
        }

        #endregion
    }
}
