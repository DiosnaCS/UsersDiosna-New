using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UsersDiosna.GraphReport.Models
{   
    public class GraphReportForm
    {
        public DateTime beginDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public MultiSelectList tagList { get; set; }
        public List<string> tags { get; set; }
        public int GraphsCount { get; set; }
    }
    public class GraphReportResponse
    {
          public List<double> datasets { get; set; }
          public List<string> labels { get; set; }
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
        public string label { get; set; }
    }

    public enum RequestType
    {
        batches,
        frequency,
        differences,
        absoulteScale
    }
    public enum GraphType
    {

    }
}