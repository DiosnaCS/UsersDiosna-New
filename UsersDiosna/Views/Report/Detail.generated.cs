﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using UsersDiosna;
    using UsersDiosna.Controllers;
    
    #line 3 "..\..\Views\Report\Detail.cshtml"
    using UsersDiosna.Handlers;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Report\Detail.cshtml"
    using UsersDiosna.Report.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Report/Detail.cshtml")]
    public partial class _Views_Report_Detail_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<UsersDiosna.Report.Models.ColumnReportModel>>
    {
        public _Views_Report_Detail_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 5 "..\..\Views\Report\Detail.cshtml"
  
    ViewBag.Title = "Detail";
    int BatchNo = ViewBag.BatchNo;
    int needSum = 0;
    int doneSum = 0;
    string Variant1, Variant2, Variant3, Varaiant4;
    string need;
    string done;
    string dynamicRowStyle = "";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h4>Batch Detail</h4>\r\n\r\n<p>\r\n    <a");

WriteLiteral(" class=\"btn-primary btn-xs\"");

WriteAttribute("href", Tuple.Create(" href=\"", 436), Tuple.Create("\"", 472)
, Tuple.Create(Tuple.Create("", 443), Tuple.Create("/Report/GetPrevBatch/", 443), true)
            
            #line 19 "..\..\Views\Report\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 464), Tuple.Create<System.Object, System.Int32>(BatchNo
            
            #line default
            #line hidden
, 464), false)
);

WriteLiteral(">Previouse batch</a> Batch number: ");

            
            #line 19 "..\..\Views\Report\Detail.cshtml"
                                                                                                    Write(BatchNo);

            
            #line default
            #line hidden
WriteLiteral(" <a");

WriteLiteral(" class=\"btn-primary btn-xs\"");

WriteAttribute("href", Tuple.Create(" href=\"", 546), Tuple.Create("\"", 582)
, Tuple.Create(Tuple.Create("", 553), Tuple.Create("/Report/GetNextBatch/", 553), true)
            
            #line 19 "..\..\Views\Report\Detail.cshtml"
                                                                                            , Tuple.Create(Tuple.Create("", 574), Tuple.Create<System.Object, System.Int32>(BatchNo
            
            #line default
            #line hidden
, 574), false)
);

WriteLiteral(">Next batch</a>\r\n    <br>\r\n    <p>\r\n        <h4>Batch info</h4>\r\n        <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">Destination:</div> ");

            
            #line 23 "..\..\Views\Report\Detail.cshtml"
                                            Write(ViewBag.Destination);

            
            #line default
            #line hidden
WriteLiteral("<br>\r\n        <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">Batch number:</div> ");

            
            #line 24 "..\..\Views\Report\Detail.cshtml"
                                             Write(ViewBag.BatchNo);

            
            #line default
            #line hidden
WriteLiteral("<br>\r\n        <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">Number of interrupts:</div> ");

            
            #line 25 "..\..\Views\Report\Detail.cshtml"
                                                     Write(ViewBag.InteruptedCounts);

            
            #line default
            #line hidden
WriteLiteral("<br>\r\n        <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">Number of skips:</div> ");

            
            #line 26 "..\..\Views\Report\Detail.cshtml"
                                                Write(ViewBag.NumberOfStepsSkipped);

            
            #line default
            #line hidden
WriteLiteral("<br>\r\n        <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">Started:</div> ");

            
            #line 27 "..\..\Views\Report\Detail.cshtml"
                                        Write(Model.Min(p => p.TimeStart.ToShortDateString()));

            
            #line default
            #line hidden
WriteLiteral("<br />\r\n        <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">Ended:</div> ");

            
            #line 28 "..\..\Views\Report\Detail.cshtml"
                                      Write(Model.Max(p => p.TimeStart.ToShortDateString()));

            
            #line default
            #line hidden
WriteLiteral("<br />\r\n    </p>\r\n</p>\r\n<div");

WriteLiteral(" class=\"table-responsive\"");

WriteLiteral(">\r\n<table");

WriteLiteral(" class=\"table table-responsive table-condensed\"");

WriteLiteral(@">
    <tr>
        <th>
            Operation
        </th>
        <th>
            Start time 
        </th>
        <th>
            End
        </th>
        <th>
            Need
        </th>
        <th>
            Actual
        </th>
        <th>
            Info
        </th>
    </tr>

");

            
            #line 54 "..\..\Views\Report\Detail.cshtml"
 foreach (var item in Model) {
        
            
            #line default
            #line hidden
            
            #line 55 "..\..\Views\Report\Detail.cshtml"
           dynamicRowStyle = ""; 
            
            #line default
            #line hidden
            
            #line 55 "..\..\Views\Report\Detail.cshtml"
                                  
        
            
            #line default
            #line hidden
            
            #line 56 "..\..\Views\Report\Detail.cshtml"
         if ((int)item.RecordType > 9 && (int)item.RecordType < 15)
        {
            
            
            #line default
            #line hidden
            
            #line 58 "..\..\Views\Report\Detail.cshtml"
             if (item.RecordType == Operations.StatusInfo || item.RecordType == Operations.Continue || item.RecordType == Operations.Interrupt)
            {
                dynamicRowStyle = "<tr>";
                
            
            #line default
            #line hidden
            
            #line 61 "..\..\Views\Report\Detail.cshtml"
                 if (item.RecordType == Operations.Continue || item.RecordType == Operations.Interrupt)
                {
                    dynamicRowStyle = "<tr style=\"background-color: beige\">";
                }
            
            #line default
            #line hidden
            
            #line 64 "..\..\Views\Report\Detail.cshtml"
                 
                
            
            #line default
            #line hidden
            
            #line 65 "..\..\Views\Report\Detail.cshtml"
           Write(Html.Raw(dynamicRowStyle));

            
            #line default
            #line hidden
            
            #line 65 "..\..\Views\Report\Detail.cshtml"
                                          
            }
            else
            {
                dynamicRowStyle = "<tr style=\"background-color: darkseagreen\"><i>";
                
            
            #line default
            #line hidden
            
            #line 70 "..\..\Views\Report\Detail.cshtml"
           Write(Html.Raw(dynamicRowStyle));

            
            #line default
            #line hidden
            
            #line 70 "..\..\Views\Report\Detail.cshtml"
                                          
            }
            
            #line default
            #line hidden
            
            #line 71 "..\..\Views\Report\Detail.cshtml"
             
        } else
        {
            
            
            #line default
            #line hidden
            
            #line 74 "..\..\Views\Report\Detail.cshtml"
       Write(Html.Raw("<tr>"));

            
            #line default
            #line hidden
            
            #line 74 "..\..\Views\Report\Detail.cshtml"
                             ;
        }
            
            #line default
            #line hidden
            
            #line 75 "..\..\Views\Report\Detail.cshtml"
         

            
            #line default
            #line hidden
WriteLiteral("        <td>\r\n");

            
            #line 77 "..\..\Views\Report\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 77 "..\..\Views\Report\Detail.cshtml"
             if ((int)item.RecordType > 9 && (int)item.RecordType < 15)
            {
                
            
            #line default
            #line hidden
            
            #line 79 "..\..\Views\Report\Detail.cshtml"
           Write(Html.Raw("\t"));

            
            #line default
            #line hidden
            
            #line 79 "..\..\Views\Report\Detail.cshtml"
                               ;
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 81 "..\..\Views\Report\Detail.cshtml"
       Write(Html.DisplayFor(modelItem => item.RecordType));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n");

WriteLiteral("        <td>\r\n");

            
            #line 84 "..\..\Views\Report\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 84 "..\..\Views\Report\Detail.cshtml"
              string timeStartForView = item.TimeStart.ToShortTimeString();
            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 85 "..\..\Views\Report\Detail.cshtml"
       Write(timeStartForView);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n");

WriteLiteral("        <td>\r\n");

            
            #line 88 "..\..\Views\Report\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 88 "..\..\Views\Report\Detail.cshtml"
             if (item.TimeEnd.Ticks> 630822816000000000) {

            
            #line default
            #line hidden
WriteLiteral("                <span>");

            
            #line 89 "..\..\Views\Report\Detail.cshtml"
                 Write(item.TimeEnd.ToShortTimeString());

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");

            
            #line 90 "..\..\Views\Report\Detail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </td>\r\n");

WriteLiteral("        <td>\r\n");

            
            #line 93 "..\..\Views\Report\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 93 "..\..\Views\Report\Detail.cshtml"
             if ((int)item.RecordType > 31 && (int)item.RecordType < 39) {
                TimeSpan spanN = new TimeSpan(((long)((long)item.Need * (long)10000000)));
                need = (int)spanN.TotalHours + " h " + (int)spanN.Minutes +" m " + (int)spanN.Seconds + " s";
            } else {
                if ((int)item.RecordType > 21 && (int)item.RecordType < 30)
                {
                    needSum += item.Need;
                }
                need = (item.Need / 1000) + "kg";
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 103 "..\..\Views\Report\Detail.cshtml"
             if (item.Need != 0)
            {
                
            
            #line default
            #line hidden
            
            #line 105 "..\..\Views\Report\Detail.cshtml"
           Write(need);

            
            #line default
            #line hidden
            
            #line 105 "..\..\Views\Report\Detail.cshtml"
                     
            }

            
            #line default
            #line hidden
WriteLiteral("        </td>\r\n");

WriteLiteral("        <td>\r\n");

            
            #line 109 "..\..\Views\Report\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 109 "..\..\Views\Report\Detail.cshtml"
             if ((int)item.RecordType > 31 && (int)item.RecordType < 39)
            {
                TimeSpan spanD = new TimeSpan(((long)((long)item.Actual * (long)10000000)));
                done = (int)spanD.TotalHours + " h " + (int)spanD.Minutes + " m " + (int)spanD.Seconds + " s";
            }
            else
            {
                if ((int)item.RecordType > 19 && (int)item.RecordType < 30)
                {
                    doneSum += item.Actual;
                }
                done = (item.Actual / 1000) + "kg";
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 122 "..\..\Views\Report\Detail.cshtml"
             if (item.Actual != 0)
            {

            
            #line default
            #line hidden
WriteLiteral("                <b>");

            
            #line 124 "..\..\Views\Report\Detail.cshtml"
              Write(done);

            
            #line default
            #line hidden
WriteLiteral("</b>\r\n");

            
            #line 125 "..\..\Views\Report\Detail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </td>\r\n");

            
            #line 127 "..\..\Views\Report\Detail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("        <td>\r\n");

            
            #line 129 "..\..\Views\Report\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 129 "..\..\Views\Report\Detail.cshtml"
              string info = ReportHandler.getInfoColumn(item);
            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 130 "..\..\Views\Report\Detail.cshtml"
       Write(info);

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n");

            
            #line 132 "..\..\Views\Report\Detail.cshtml"

        
            
            #line default
            #line hidden
            
            #line 133 "..\..\Views\Report\Detail.cshtml"
         if (item.RecordType == Operations.RecipeStart || item.RecordType == Operations.RecipeEnd)
        {
            dynamicRowStyle = "</i></tr>";
        }
        else {
            dynamicRowStyle = "</tr>";
        }
            
            #line default
            #line hidden
            
            #line 139 "..\..\Views\Report\Detail.cshtml"
         
        
            
            #line default
            #line hidden
            
            #line 140 "..\..\Views\Report\Detail.cshtml"
   Write(Html.Raw(dynamicRowStyle));

            
            #line default
            #line hidden
            
            #line 140 "..\..\Views\Report\Detail.cshtml"
                                  
}

            
            #line default
            #line hidden
WriteLiteral("</table>\r\n</div>\r\n<p>\r\n    <h4>Batch summary</h4>\r\n    <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">RCP duration:</div> ");

            
            #line 146 "..\..\Views\Report\Detail.cshtml"
                                                
        var startTime = Model.Min(p => p.TimeStart.Ticks);
        var endTime = Model.Max(p => p.TimeEnd.Ticks);
        var diff = endTime - startTime;
        TimeSpan duration = TimeSpan.FromTicks(diff);
        int durationDays = (int)duration.Days;
        int durationHours = (int)duration.Hours;
        int durationMinutes = (int)duration.Minutes;
    
            
            #line default
            #line hidden
WriteLiteral("\r\n    <b>");

            
            #line 155 "..\..\Views\Report\Detail.cshtml"
  Write(durationDays);

            
            #line default
            #line hidden
WriteLiteral(" d ");

            
            #line 155 "..\..\Views\Report\Detail.cshtml"
                  Write(durationHours);

            
            #line default
            #line hidden
WriteLiteral(" h ");

            
            #line 155 "..\..\Views\Report\Detail.cshtml"
                                   Write(durationMinutes);

            
            #line default
            #line hidden
WriteLiteral(" m</b><br />\r\n    <div");

WriteLiteral(" class=\"col-md-3\"");

WriteLiteral(">Total amount dosed: </div>");

            
            #line 156 "..\..\Views\Report\Detail.cshtml"
                                                Write(doneSum/1000);

            
            #line default
            #line hidden
WriteLiteral(" kg\r\n</p>\r\n<p>\r\n    <a");

WriteLiteral(" class=\"btn-primary btn-sm\"");

WriteLiteral(" href=\"/Report/\"");

WriteLiteral(">Back to calendar</a>\r\n</p>");

        }
    }
}
#pragma warning restore 1591
