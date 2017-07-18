using System;
using System.Collections.Generic;

namespace UsersDiosna.Report.Models
{
    public class ColumnReportModel
    {
        
        public int  RecordNo { get; set; }

        public int  RecordType { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public int  BatchNo { get; set; }

        public string  Destination { get; set; }

        public int  Need { get; set; }

        public int  Actual { get; set; }

        public int Variant1 { get; set; }
        public int Variant2 { get; set; }
        public int Variant3 { get; set; }
        public int Variant4 { get; set; }


        /*
         public int diRecNo  { get; set; }
         public int iRecType  { get; set; }
         public int diTimestamp  { get; set; }
         public int diBatchNo  { get; set; }
         public int iSrc  { get; set; }
         public int iDest  { get; set; }
         public int iFlags  { get; set; }
         public int iRcpNo  { get; set; }
         public int diSetpoint  { get; set; }
         public int diActual  { get; set; }
         public int iUserID  { get; set; }
         public int iParam1  { get; set; }
         public int iParam2  { get; set; }
         public int diMatCode  { get; set; }
         public int diDoseSetPoint  { get; set; }
         public int iSPinfo  { get; set; }
         public int iOrgUnit  { get; set; }
         */

        /*Maybe could be some dynamic to reed it from xml file*/
    }

    public class OperationReportModel
    {
        //Recipe operations
        public static int RecipeStart = 10;
        public static int Interrupt = 11;
        public static int Continue = 12;
        public static int StepSkip = 13;
        public static int RecipeEnd = 14;

        //Rawmaterial operations
    }

    public class DataReportModel
    {
        public List<ColumnReportModel> Data { get; set;}
    }
}