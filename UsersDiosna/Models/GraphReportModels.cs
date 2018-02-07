using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web.Mvc;
using UsersDiosna.Report.Models;

namespace UsersDiosna.GraphReport.Models
{   
    public class GraphReportForm
    {
        public DateTime beginDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public MultiSelectList tagList { get; set; }
        public List<string> tags { get; set; }
        public int graphsCount { get; set; }
    }
    public class GraphReportResponse
    {
          public List<DataSet> datasets { get; set; }
          public List<string> labels { get; set; }
    }
    public class DataSet
    {
        public string backgroundColor { get; set; }
        //public string fillColor { get; set; }
        //public string strokeColor { get; set; }
        //public string highlightColor { get; set; }
        //public string highlightStroker { get; set; }
        public string label { get; set; }
        public List<double> data { get; set; }
    }
    public class DataRequest
    {
        public long beginTime { get; set; } //in pkTime
        public long endTime { get; set; } //in pkTime
        public RequestType requestType { get; set; }
        //public int viewLength;
        public List<Tag> definition { get; set; }
    }

    public class Tag
    {
        public string table { get; set; }
        public string column { get; set; }
        public Operations operation { get; set; }
        public string label { get; set; }
    }

    public enum RequestType
    {
        batches,
        frequency,
        differences,
        absoulteScale
    }
    //public enum GraphType
    //{
       
    //}
}