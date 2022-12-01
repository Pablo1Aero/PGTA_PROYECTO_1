using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Class_Library
{
    public class CAT21
    {
        public string[] CAT21_Message;
        readonly DecodeLibrary Library;
        public CAT21(string[] CAT21_Message, DecodeLibrary Library)
        {
            this.CAT21_Message = CAT21_Message;
            this.Library = Library;
            try
            {
                string[] FSPEC_Return = Library.obtainFSPEC(CAT21_Message);
                int Position = Convert.ToInt32(FSPEC_Return[0]);
                string FSPEC = FSPEC_Return[1];
                {
                    if (FSPEC[0] == '1') { Position = this.Decode_Aircraft_Operation_Status(Position, CAT21_Message[Position]); }
                }
            }
            catch
            {
                string ErrorMessage = "Error decoding CAT21 message";
            }
        }



        //I021/008 Aircraft Operational Status
        #region Aircraft Operational Status
        public string Aircraft_Operation_Status;
        private int Decode_Aircraft_Operation_Status(int Position, string Message_Type_byte)
        {
            if (Convert.ToInt32(Message_Type_byte[0]) == 0) { Aircraft_Operation_Status = "TCAS II or ACAS RA not active "; }
            if (Convert.ToInt32(Message_Type_byte[0]) == 1)  { Aircraft_Operation_Status = "TCAS RA active "; }

            Position++;

            return Position;
        }
        #endregion

    }
}
