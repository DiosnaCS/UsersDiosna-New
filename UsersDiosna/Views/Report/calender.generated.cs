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
    
    #line 1 "..\..\Views\Report\calender.cshtml"
    using UsersDiosna.Report.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Report/calender.cshtml")]
    public partial class _Views_Report_calender_cshtml : System.Web.Mvc.WebViewPage<UsersDiosna.Report.Models.DataReportModel>
    {
        public _Views_Report_calender_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Report\calender.cshtml"
  
    ViewBag.Title = "calender";
    string name = string.Empty;
    int count = 0;
    int year = ViewBag.year;
    int monthLess = ViewBag.Month - 1;
    int monthMore = ViewBag.Month + 1;
    if (monthLess < 1) {
        year--;
        monthLess = 12;
    }
    if (monthMore > 12) {
        year++;
        monthMore = 1;
    }
    DateTime monthDT = new DateTime(year, ViewBag.month, 1);
    string monthName = monthDT.ToString("MMMM");

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h4>calender</h4>\r\n<div");

WriteLiteral(" class=\" col-md-10\"");

WriteLiteral(">\r\n    <a");

WriteLiteral(" class=\"btn-primary btn-xs\"");

WriteAttribute("href", Tuple.Create(" href=\"", 631), Tuple.Create("\"", 668)
, Tuple.Create(Tuple.Create("", 638), Tuple.Create("/Report/Month/", 638), true)
            
            #line 25 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 652), Tuple.Create<System.Object, System.Int32>(monthLess
            
            #line default
            #line hidden
, 652), false)
, Tuple.Create(Tuple.Create("", 662), Tuple.Create("/", 662), true)
            
            #line 25 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 663), Tuple.Create<System.Object, System.Int32>(year
            
            #line default
            #line hidden
, 663), false)
);

WriteLiteral(">-1 month</a> Month: ");

            
            #line 25 "..\..\Views\Report\calender.cshtml"
                                                                                       Write(monthName);

            
            #line default
            #line hidden
WriteLiteral(" <a");

WriteLiteral(" class=\"btn-primary btn-xs\"");

WriteAttribute("href", Tuple.Create(" href=\"", 730), Tuple.Create("\"", 767)
, Tuple.Create(Tuple.Create("", 737), Tuple.Create("/Report/Month/", 737), true)
            
            #line 25 "..\..\Views\Report\calender.cshtml"
                                                                          , Tuple.Create(Tuple.Create("", 751), Tuple.Create<System.Object, System.Int32>(monthMore
            
            #line default
            #line hidden
, 751), false)
, Tuple.Create(Tuple.Create("", 761), Tuple.Create("/", 761), true)
            
            #line 25 "..\..\Views\Report\calender.cshtml"
                                                                                     , Tuple.Create(Tuple.Create("", 762), Tuple.Create<System.Object, System.Int32>(year
            
            #line default
            #line hidden
, 762), false)
);

WriteLiteral(">+1 month</a>\r\n    <p></p>\r\n</div>\r\n<table");

WriteLiteral(" class=\"table-bordered table-condensed table-hover\"");

WriteLiteral(">\r\n    <thead>\r\n    <tr>\r\n        <th>Day | Hours</th>\r\n");

            
            #line 32 "..\..\Views\Report\calender.cshtml"
        
            
            #line default
            #line hidden
            
            #line 32 "..\..\Views\Report\calender.cshtml"
         for (int hours = 0;hours<24;hours++) {

            
            #line default
            #line hidden
WriteLiteral("            <th>");

            
            #line 33 "..\..\Views\Report\calender.cshtml"
           Write(hours);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n");

            
            #line 34 "..\..\Views\Report\calender.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        <th>Batches count</th>\r\n    </tr>\r\n    </thead>\r\n");

            
            #line 38 "..\..\Views\Report\calender.cshtml"
    
            
            #line default
            #line hidden
            
            #line 38 "..\..\Views\Report\calender.cshtml"
     for (int day = 1; day <= DateTime.DaysInMonth(year, ViewBag.month); day++)
    {
        
            
            #line default
            #line hidden
            
            #line 40 "..\..\Views\Report\calender.cshtml"
           DateTime dayDT = new DateTime(year, ViewBag.month, day); string sDay = dayDT.DayOfWeek.ToString(); 
            
            #line default
            #line hidden
            
            #line 40 "..\..\Views\Report\calender.cshtml"
                                                                                                               

            
            #line default
            #line hidden
WriteLiteral("        <tr>\r\n            <td>");

            
            #line 42 "..\..\Views\Report\calender.cshtml"
           Write(day);

            
            #line default
            #line hidden
WriteLiteral(". ");

            
            #line 42 "..\..\Views\Report\calender.cshtml"
                 Write(sDay);

            
            #line default
            #line hidden
WriteLiteral("</td>            \r\n");

            
            #line 43 "..\..\Views\Report\calender.cshtml"
           
            
            #line default
            #line hidden
            
            #line 43 "..\..\Views\Report\calender.cshtml"
            for (int hour = 0; hour < 24; hour++)
           {
               List<ViewHeaderBatch> batches = new List<ViewHeaderBatch>();
               int latestBatchNo = 0;
               name = string.Empty;
               if (Model.Data.Exists(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour))
               {
                   var data = Model.Data.Where(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour);
                   foreach (var batch in data)
                   {
                       if (batch.RecordNo != latestBatchNo)
                       {
                           batches.Add(new ViewHeaderBatch() { Name = batch.Destination, BatchNo = batch.BatchNo });
                           count++;
                       }
                       latestBatchNo = batch.RecordNo;
                   }

               }

            
            #line default
            #line hidden
WriteLiteral("                <td>\r\n");

            
            #line 63 "..\..\Views\Report\calender.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 63 "..\..\Views\Report\calender.cshtml"
                     foreach (var batch in batches) {

            
            #line default
            #line hidden
WriteLiteral("                        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 2301), Tuple.Create("\"", 2338)
, Tuple.Create(Tuple.Create("", 2308), Tuple.Create("/Report/Detail/", 2308), true)
            
            #line 64 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 2323), Tuple.Create<System.Object, System.Int32>(batch.BatchNo
            
            #line default
            #line hidden
, 2323), false)
, Tuple.Create(Tuple.Create(" ", 2337), Tuple.Create("", 2337), true)
);

WriteLiteral(">");

            
            #line 64 "..\..\Views\Report\calender.cshtml"
                                                            Write(batch.Name);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 65 "..\..\Views\Report\calender.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                </td>                \r\n");

            
            #line 67 "..\..\Views\Report\calender.cshtml"
           }

            
            #line default
            #line hidden
WriteLiteral("            <td>");

            
            #line 68 "..\..\Views\Report\calender.cshtml"
           Write(count);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 69 "..\..\Views\Report\calender.cshtml"
            
            
            #line default
            #line hidden
            
            #line 69 "..\..\Views\Report\calender.cshtml"
              count = 0;
            
            #line default
            #line hidden
WriteLiteral("\r\n        </tr>\r\n");

            
            #line 71 "..\..\Views\Report\calender.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</table>");

        }
    }
}
#pragma warning restore 1591