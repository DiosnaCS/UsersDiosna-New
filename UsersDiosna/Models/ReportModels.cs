using System;
using System.Collections.Generic;

namespace UsersDiosna.Report.Models
{
    public  class ColumnReportModel
    {
        
        public int  RecordNo { get; set; }

        public Operations  RecordType { get; set; }

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

        /*Maybe could be some dynamic to reed it from xml file*/
    }

    public class FilterReportModel {

    }
    
    public class OverviewReportDataModel {
        public int day { get; set; }
        //Columns of the table daily consumption
        public int MotherCultureAmnt { get; set; }
        public int MotherCultureBatchCount { get; set; }

        public int FlourAmnt { get; set; }
        public int FlourBatchCount { get; set; }

        public int WaterAmnt { get; set; }
        public int WaterBatchCount { get; set; }

        public int OldBreadAmnt { get; set; }
        public int OldBreadBatchCount { get; set; }

        public int LiquidYeastAmnt { get; set; }
        public int LiquidYeastBatchCount { get; set; }

        public int MixtureAmnt { get; set; }
        public int MixtureBatchCount { get; set; }

        public int GenericAmnt { get; set; }
        public int GenericBatchCount { get; set; }
    }

    public class OverviewReportModel {
        public List<OverviewReportDataModel> Data { get; set; }
    }

    public class ViewHeaderBatch
    {
        public string Name;
        public int BatchNo;
        public int AmntTotal;
        public int RecipeNo;
        public bool Status;
    }

    public class ViewHeaderDosingOut
    {
        public int hour;
        public int count;
        public int amountSum;
    }

    public class ViewHeaderCleaning
    {
        public string cleaning;
        public string destination;
        public int BatchNo;
        public ClnType ClnType;
        public StartedBy StartedBy;
    }

    public enum Operations
    {
        //Recipe operations
        RecipeStart = 10,
        Interrupt = 11,
        Continue = 12,
        StepSkip = 13,
        RecipeEnd = 14,
        //Rawmaterial operations
        FillingMotherCulture = 20,
        FillingFlour = 21,
        FillingWater = 22,
        FillingOldBread = 23,
        FillingLiquidYeast =  24,
        FillingGenericComponent = 25,
        //RecipeSteps
        FillingMixture = 31,
        Soaking = 32,
        Mixing = 33,
        Fermentation = 35,
        Cooling = 36,
        Storing = 37,
        StatusInfo = 39,
        //Auxiliary operations
        Repumping = 40,
        DosingOut = 44,
        Pigging = 45,
        FermenterCleaning = 46,
        PipWorkCleaning = 47,
        YeastCleaning = 48,
        //OperatorActions
        OperatorLogin = 50,
        OperatorLogout = 51
    }

    public enum StartedBy {
        automatic = 0,
        manual = 1
    }

    public enum FillType {
        automatic = 0,
        manual = 1,
        byRest = 2
    }

    public enum WaterSource {
        WMU = 0,
        cleaningWater = 1,
    }

    public enum RcpType {
        predough = 0,
        motherdough = 1
    }

    public enum Cause {
        userInterrupt = 0,
        alarm = 1
    }

    public enum ClnType {
        none = -1,
        automatic = 0,
        manual = 1,
        showering = 2,
        chemical = 3
    }
    public class DataReportModel
    {
        public List<ColumnReportModel> Data { get; set;}
    }

    public class ReportDosing {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public List<Dosing> Batches { get; set; }
    }

    public class Dosing {
        public int RecordNo { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string Destination { get; set; }

        public int Actual { get; set; }
        /// <summary>
        /// Variant 1 column
        /// </summary>
        public string SrcTank { get; set; }
        /// <summary>
        /// Variant 2 column
        /// </summary>
        public StartedBy StartedBy { get; set; }
        /// <summary>
        /// Variant3 column
        /// </summary>
        public int SrcBatchNo { get; set; }

    }
    public class ColumnGraphModel
    {

        public int RecordNo { get; set; }

        public int RecordType { get; set; }

        public int TimeStart { get; set; }

        public int TimeEnd { get; set; }

        public int BatchNo { get; set; }

        public string Destination { get; set; }

        public int Need { get; set; }

        public int Actual { get; set; }

        public int Variant1 { get; set; }
        public int Variant2 { get; set; }
        public int Variant3 { get; set; }
        public int Variant4 { get; set; }

        /*Maybe could be some dynamic to reed it from xml file*/
    }

    public class GraphEventsData
    {
        public List<ColumnGraphModel> events { get; set; }
    }
}