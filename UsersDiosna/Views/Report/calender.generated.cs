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
    
    #line 2 "..\..\Views\Report\calender.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    
    #line 1 "..\..\Views\Report\calender.cshtml"
    using System.Threading;
    
    #line default
    #line hidden
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
    
    #line 3 "..\..\Views\Report\calender.cshtml"
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
            
            #line 6 "..\..\Views\Report\calender.cshtml"
  
    ViewBag.Title = "calender";
    string name = string.Empty;
    int EndType = 0;
    int amntTotal = 0;
    int amntOverall= 0;
    int amnt = 0;
    string colourRow = "white";
    int count = 0;
    int countOverall = 0;
    int dosingOutCount = 0;
    int dosingOutAmountSum = 0;
    int rowDOcount = 0;
    int rowDOamount = 0;
    int rowDOcountOverall = 0;
    int rowDOamountOverall = 0;
    int cleaningCount = 0;
    /*
    int rowDOcount = 0;
    int rowDOamount = 0;
    int rowDOcountOverall = 0;
    int rowDOamountOverall = 0;
    */
    int year = ViewBag.year;
    int yearOnButtonLeft = year;
    int yearOnButtonRight = year;
    int monthLess = ViewBag.Month - 1;
    int monthMore = ViewBag.Month + 1;
    if (monthLess < 1) {
        yearOnButtonLeft--;
        monthLess = 12;
    }
    if (monthMore > 12) {
        yearOnButtonRight++;
        monthMore = 1;
    }
    ViewHeaderDosingOut dosingOut = null;
    ViewHeaderCleaning cleaning = null;
    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
    DateTime monthDT = new DateTime(year, ViewBag.month, 1);
    string monthName = monthDT.ToString("MMMM");


            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h4>Calender</h4>\r\n<div");

WriteLiteral(" class=\" col-md-12\"");

WriteLiteral(">\r\n    <a");

WriteLiteral(" class=\"btn-primary btn-xs\"");

WriteAttribute("href", Tuple.Create(" href=\"", 1424), Tuple.Create("\"", 1473)
, Tuple.Create(Tuple.Create("", 1431), Tuple.Create("/Report/Month/", 1431), true)
            
            #line 52 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 1445), Tuple.Create<System.Object, System.Int32>(monthLess
            
            #line default
            #line hidden
, 1445), false)
, Tuple.Create(Tuple.Create("", 1455), Tuple.Create("/", 1455), true)
            
            #line 52 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 1456), Tuple.Create<System.Object, System.Int32>(yearOnButtonLeft
            
            #line default
            #line hidden
, 1456), false)
);

WriteLiteral(">-1 month</a> Month: ");

            
            #line 52 "..\..\Views\Report\calender.cshtml"
                                                                                                   Write(monthName);

            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 52 "..\..\Views\Report\calender.cshtml"
                                                                                                              Write(year);

            
            #line default
            #line hidden
WriteLiteral(" <a");

WriteLiteral(" class=\"btn-primary btn-xs\"");

WriteAttribute("href", Tuple.Create(" href=\"", 1541), Tuple.Create("\"", 1591)
, Tuple.Create(Tuple.Create("", 1548), Tuple.Create("/Report/Month/", 1548), true)
            
            #line 52 "..\..\Views\Report\calender.cshtml"
                                                                                           , Tuple.Create(Tuple.Create("", 1562), Tuple.Create<System.Object, System.Int32>(monthMore
            
            #line default
            #line hidden
, 1562), false)
, Tuple.Create(Tuple.Create("", 1572), Tuple.Create("/", 1572), true)
            
            #line 52 "..\..\Views\Report\calender.cshtml"
                                                                                                      , Tuple.Create(Tuple.Create("", 1573), Tuple.Create<System.Object, System.Int32>(yearOnButtonRight
            
            #line default
            #line hidden
, 1573), false)
);

WriteLiteral(">+1 month</a>\r\n    <p></p>\r\n</div>\r\n<div");

WriteLiteral(" class=\" col-md-12\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"checkbox-inline big-checkbox\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" onclick=\"hideShowBatches()\"");

WriteLiteral("/>Hide/Show Batches\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"checkbox-inline big-checkbox\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" onclick=\"hideShowDosing()\"");

WriteLiteral(" checked/>Hide/Show Dosing\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"checkbox-inline big-checkbox\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"checkbox\"");

WriteLiteral(" onclick=\"hideShowCleaning()\"");

WriteLiteral(" checked/>Hide/Show Cleaning\r\n    </div>\r\n</div>\r\n<table");

WriteLiteral(" class=\"table-bordered table-condensed\"");

WriteLiteral(">\r\n    <thead>\r\n    <tr>\r\n        <th>Day | Hours</th>\r\n");

            
            #line 70 "..\..\Views\Report\calender.cshtml"
        
            
            #line default
            #line hidden
            
            #line 70 "..\..\Views\Report\calender.cshtml"
         for (int hours = 0;hours<24;hours++) {

            
            #line default
            #line hidden
WriteLiteral("            <th");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral(">");

            
            #line 71 "..\..\Views\Report\calender.cshtml"
                             Write(hours);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n");

            
            #line 72 "..\..\Views\Report\calender.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        <th");

WriteLiteral(" style=\"background-color: lightcyan;\"");

WriteLiteral(" title=\"Batches Count\"");

WriteLiteral(">B. cnt.</th>\r\n        <th");

WriteLiteral(" style=\"background-color: lightcyan;\"");

WriteLiteral(" title=\"Dosing Count\"");

WriteLiteral(">DO. cnt.</th>\r\n        <th");

WriteLiteral(" style=\"background-color: lightgray;\"");

WriteLiteral(" title=\"Batches amount\"");

WriteLiteral(">B. amt.</th>       \r\n        <th");

WriteLiteral(" style=\"background-color: lightgray;\"");

WriteLiteral(" title=\"Dosing amount\"");

WriteLiteral(">DO. amt.</th>\r\n    </tr>\r\n    </thead>\r\n");

            
            #line 79 "..\..\Views\Report\calender.cshtml"
    
            
            #line default
            #line hidden
            
            #line 79 "..\..\Views\Report\calender.cshtml"
     for (int day = 1; day <= DateTime.DaysInMonth(year, ViewBag.month); day++)
    {
        
            
            #line default
            #line hidden
            
            #line 81 "..\..\Views\Report\calender.cshtml"
           DateTime dayDT = new DateTime(year, ViewBag.month, day); string sDay = dayDT.DayOfWeek.ToString();  
            
            #line default
            #line hidden
            
            #line 81 "..\..\Views\Report\calender.cshtml"
                                                                                                                

            
            #line default
            #line hidden
WriteLiteral("        <tr>\r\n");

            
            #line 83 "..\..\Views\Report\calender.cshtml"
            
            
            #line default
            #line hidden
            
            #line 83 "..\..\Views\Report\calender.cshtml"
             if (dayDT.DayOfWeek == DayOfWeek.Saturday || dayDT.DayOfWeek == DayOfWeek.Sunday)
            {
                colourRow = "lightgoldenrodyellow";

            
            #line default
            #line hidden
WriteLiteral("                <td>");

            
            #line 86 "..\..\Views\Report\calender.cshtml"
               Write(day);

            
            #line default
            #line hidden
WriteLiteral(". ");

            
            #line 86 "..\..\Views\Report\calender.cshtml"
                     Write(sDay);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 87 "..\..\Views\Report\calender.cshtml"
            } else
            {
                colourRow = "white";

            
            #line default
            #line hidden
WriteLiteral("                <td>");

            
            #line 90 "..\..\Views\Report\calender.cshtml"
               Write(day);

            
            #line default
            #line hidden
WriteLiteral(". ");

            
            #line 90 "..\..\Views\Report\calender.cshtml"
                     Write(sDay);

            
            #line default
            #line hidden
WriteLiteral("</td>            \r\n");

            
            #line 91 "..\..\Views\Report\calender.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("           ");

            
            #line 92 "..\..\Views\Report\calender.cshtml"
            for (int hour = 0; hour < 24; hour++)
           {
               List<ViewHeaderBatch> batches = new List<ViewHeaderBatch>();
               int latestBatchNo = 0;
               name = string.Empty;
               if (Model.Data.Exists(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour))
               {
                   var data = Model.Data.Where(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour && p.RecordType != Operations.DosingOut);
                   foreach (var batch in data)
                   {
                       if (batch.Need != 0)
                       {
                           amnt = batch.Need / 1000;
                           amntTotal += amnt;
                       }
                       if (batch.BatchNo != latestBatchNo && (batch.RecordType == Operations.RecipeStart))
                       {

                           batches.Add(new ViewHeaderBatch() { Name = batch.Destination, BatchNo = batch.BatchNo, AmntTotal = amnt, RecipeNo = batch.Variant1});
                           count++;
                       }
                       latestBatchNo = batch.BatchNo;
                   }
               }
               if (Model.Data.Exists(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour && p.RecordType == Operations.DosingOut))
               {
                   dosingOutCount = Model.Data.Where(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour && p.RecordType == Operations.DosingOut).Count();
                   dosingOutAmountSum = Model.Data.Where(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour && p.RecordType == Operations.DosingOut).Sum(p => p.Actual);
                   dosingOut = new ViewHeaderDosingOut { hour = hour, count = dosingOutCount, amountSum = dosingOutAmountSum};
               }
               if (Model.Data.Exists(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour && (p.RecordType == Operations.FermenterCleaning || p.RecordType == Operations.PipWorkCleaning || p.RecordType == Operations.YeastCleaning)))
               {
                   var cleanings = Model.Data.Where(p => p.TimeStart.Day == day && p.TimeStart.Hour == hour && (p.RecordType == Operations.FermenterCleaning || p.RecordType == Operations.PipWorkCleaning || p.RecordType == Operations.YeastCleaning));
                   foreach (var cleanOp in cleanings) {
                       if (cleanOp.RecordType == Operations.Pigging) {
                           cleaning = new ViewHeaderCleaning { cleaning = cleanOp.RecordType.ToString(), destination = cleanOp.Destination, BatchNo = cleanOp.BatchNo, ClnType = ClnType.none, StartedBy = (StartedBy)cleanOp.Variant2 };
                       }
                       else {
                           cleaning = new ViewHeaderCleaning { cleaning = cleanOp.RecordType.ToString(), destination = cleanOp.Destination, BatchNo = cleanOp.BatchNo, ClnType = (ClnType)cleanOp.Variant1, StartedBy = (StartedBy)cleanOp.Variant2 };
                       }
                   }
               }

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteAttribute("style", Tuple.Create(" style=\"", 6337), Tuple.Create("\"", 6373)
, Tuple.Create(Tuple.Create("", 6345), Tuple.Create("background-color:", 6345), true)
            
            #line 134 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create(" ", 6362), Tuple.Create<System.Object, System.Int32>(colourRow
            
            #line default
            #line hidden
, 6363), false)
);

WriteLiteral(">\r\n");

            
            #line 135 "..\..\Views\Report\calender.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 135 "..\..\Views\Report\calender.cshtml"
                     foreach (var batch in batches)
                    {
                        if (Model.Data.Exists(p => p.BatchNo == batch.BatchNo && p.RecordType == Operations.RecipeEnd))
                        {
                            EndType = Model.Data.Single(p => p.BatchNo == batch.BatchNo && p.RecordType == Operations.RecipeEnd).Variant2; // takes endType
                        }
                        batch.Status = Convert.ToBoolean(EndType);
                        if (batch.Status != false)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <b><a");

WriteAttribute("href", Tuple.Create(" href=\"", 6965), Tuple.Create("\"", 7001)
, Tuple.Create(Tuple.Create("", 6972), Tuple.Create("/Report/Detail/", 6972), true)
            
            #line 144 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 6987), Tuple.Create<System.Object, System.Int32>(batch.BatchNo
            
            #line default
            #line hidden
, 6987), false)
);

WriteLiteral(" class=\"batch\"");

WriteLiteral(" style=\"color: darkgreen;\"");

WriteAttribute("title", Tuple.Create(" title=\"", 7042), Tuple.Create("\"", 7131)
, Tuple.Create(Tuple.Create("", 7050), Tuple.Create("Batch:", 7050), true)
            
            #line 144 "..\..\Views\Report\calender.cshtml"
                                            , Tuple.Create(Tuple.Create(" ", 7056), Tuple.Create<System.Object, System.Int32>(batch.BatchNo
            
            #line default
            #line hidden
, 7057), false)
, Tuple.Create(Tuple.Create("  ", 7071), Tuple.Create("Recipe:", 7073), true)
            
            #line 144 "..\..\Views\Report\calender.cshtml"
                                                                    , Tuple.Create(Tuple.Create(" ", 7080), Tuple.Create<System.Object, System.Int32>(batch.RecipeNo
            
            #line default
            #line hidden
, 7081), false)
, Tuple.Create(Tuple.Create(" ", 7096), Tuple.Create("Filled", 7097), true)
, Tuple.Create(Tuple.Create(" ", 7103), Tuple.Create("amount:", 7104), true)
            
            #line 144 "..\..\Views\Report\calender.cshtml"
                                                                                                   , Tuple.Create(Tuple.Create(" ", 7111), Tuple.Create<System.Object, System.Int32>(batch.AmntTotal
            
            #line default
            #line hidden
, 7112), false)
, Tuple.Create(Tuple.Create(" ", 7128), Tuple.Create("kg", 7129), true)
);

WriteLiteral(">");

            
            #line 144 "..\..\Views\Report\calender.cshtml"
                                                                                                                                                                                                    Write(batch.Name);

            
            #line default
            #line hidden
WriteLiteral("</a></b>\r\n");

            
            #line 145 "..\..\Views\Report\calender.cshtml"
                        }
                        else
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <b><a");

WriteAttribute("href", Tuple.Create(" href=\"", 7271), Tuple.Create("\"", 7307)
, Tuple.Create(Tuple.Create("", 7278), Tuple.Create("/Report/Detail/", 7278), true)
            
            #line 148 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 7293), Tuple.Create<System.Object, System.Int32>(batch.BatchNo
            
            #line default
            #line hidden
, 7293), false)
);

WriteLiteral(" class=\"batch\"");

WriteAttribute("title", Tuple.Create(" title=\"", 7322), Tuple.Create("\"", 7411)
, Tuple.Create(Tuple.Create("", 7330), Tuple.Create("Batch:", 7330), true)
            
            #line 148 "..\..\Views\Report\calender.cshtml"
                  , Tuple.Create(Tuple.Create(" ", 7336), Tuple.Create<System.Object, System.Int32>(batch.BatchNo
            
            #line default
            #line hidden
, 7337), false)
, Tuple.Create(Tuple.Create("  ", 7351), Tuple.Create("Recipe:", 7353), true)
            
            #line 148 "..\..\Views\Report\calender.cshtml"
                                          , Tuple.Create(Tuple.Create(" ", 7360), Tuple.Create<System.Object, System.Int32>(batch.RecipeNo
            
            #line default
            #line hidden
, 7361), false)
, Tuple.Create(Tuple.Create(" ", 7376), Tuple.Create("Filled", 7377), true)
, Tuple.Create(Tuple.Create(" ", 7383), Tuple.Create("amount:", 7384), true)
            
            #line 148 "..\..\Views\Report\calender.cshtml"
                                                                         , Tuple.Create(Tuple.Create(" ", 7391), Tuple.Create<System.Object, System.Int32>(batch.AmntTotal
            
            #line default
            #line hidden
, 7392), false)
, Tuple.Create(Tuple.Create(" ", 7408), Tuple.Create("kg", 7409), true)
);

WriteLiteral(">");

            
            #line 148 "..\..\Views\Report\calender.cshtml"
                                                                                                                                                                          Write(batch.Name);

            
            #line default
            #line hidden
WriteLiteral("</a></b>\r\n");

            
            #line 149 "..\..\Views\Report\calender.cshtml"
                        }
                    }

            
            #line default
            #line hidden
WriteLiteral("                    ");

            
            #line 151 "..\..\Views\Report\calender.cshtml"
                     if (dosingOut != null)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 7578), Tuple.Create("\"", 7628)
, Tuple.Create(Tuple.Create("", 7585), Tuple.Create("/ReportDosing/Day/", 7585), true)
            
            #line 153 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 7603), Tuple.Create<System.Object, System.Int32>(day
            
            #line default
            #line hidden
, 7603), false)
, Tuple.Create(Tuple.Create("", 7607), Tuple.Create("/", 7607), true)
            
            #line 153 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 7608), Tuple.Create<System.Object, System.Int32>(ViewBag.Month
            
            #line default
            #line hidden
, 7608), false)
, Tuple.Create(Tuple.Create("", 7622), Tuple.Create("/", 7622), true)
            
            #line 153 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 7623), Tuple.Create<System.Object, System.Int32>(year
            
            #line default
            #line hidden
, 7623), false)
);

WriteLiteral(" class=\"dosing\"");

WriteAttribute("title", Tuple.Create(" title=\"", 7644), Tuple.Create("\"", 7696)
, Tuple.Create(Tuple.Create("", 7652), Tuple.Create("Total", 7652), true)
, Tuple.Create(Tuple.Create(" ", 7657), Tuple.Create("Amount:", 7658), true)
            
            #line 153 "..\..\Views\Report\calender.cshtml"
                                  , Tuple.Create(Tuple.Create(" ", 7665), Tuple.Create<System.Object, System.Int32>(dosingOut.amountSum/1000
            
            #line default
            #line hidden
, 7666), false)
, Tuple.Create(Tuple.Create(" ", 7693), Tuple.Create("kg", 7694), true)
);

WriteLiteral(">(<b>");

            
            #line 153 "..\..\Views\Report\calender.cshtml"
                                                                                                                                                 Write(dosingOut.count);

            
            #line default
            #line hidden
WriteLiteral("</b>)</a>\r\n");

            
            #line 154 "..\..\Views\Report\calender.cshtml"
                        rowDOcount += dosingOut.count;
                        rowDOamount += (dosingOut.amountSum/1000);
                        dosingOut = null;
                    }

            
            #line default
            #line hidden
WriteLiteral("                    ");

            
            #line 158 "..\..\Views\Report\calender.cshtml"
                     if (cleaning != null)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 8012), Tuple.Create("\"", 8064)
, Tuple.Create(Tuple.Create("", 8019), Tuple.Create("/ReportCleaning/Day/", 8019), true)
            
            #line 160 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 8039), Tuple.Create<System.Object, System.Int32>(day
            
            #line default
            #line hidden
, 8039), false)
, Tuple.Create(Tuple.Create("", 8043), Tuple.Create("/", 8043), true)
            
            #line 160 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 8044), Tuple.Create<System.Object, System.Int32>(ViewBag.Month
            
            #line default
            #line hidden
, 8044), false)
, Tuple.Create(Tuple.Create("", 8058), Tuple.Create("/", 8058), true)
            
            #line 160 "..\..\Views\Report\calender.cshtml"
, Tuple.Create(Tuple.Create("", 8059), Tuple.Create<System.Object, System.Int32>(year
            
            #line default
            #line hidden
, 8059), false)
);

WriteLiteral(" class=\"cleaning\"");

WriteAttribute("title", Tuple.Create(" title=\"", 8082), Tuple.Create("\"", 8202)
, Tuple.Create(Tuple.Create("", 8090), Tuple.Create("Cleaning:", 8090), true)
            
            #line 160 "..\..\Views\Report\calender.cshtml"
                                 , Tuple.Create(Tuple.Create(" ", 8099), Tuple.Create<System.Object, System.Int32>(cleaning.cleaning
            
            #line default
            #line hidden
, 8100), false)
, Tuple.Create(Tuple.Create(" ", 8118), Tuple.Create("Clean", 8119), true)
, Tuple.Create(Tuple.Create(" ", 8124), Tuple.Create("Type:", 8125), true)
            
            #line 160 "..\..\Views\Report\calender.cshtml"
                                                                , Tuple.Create(Tuple.Create(" ", 8130), Tuple.Create<System.Object, System.Int32>(cleaning.ClnType.ToString()
            
            #line default
            #line hidden
, 8131), false)
, Tuple.Create(Tuple.Create(" ", 8159), Tuple.Create("Started", 8160), true)
, Tuple.Create(Tuple.Create(" ", 8167), Tuple.Create("By:", 8168), true)
            
            #line 160 "..\..\Views\Report\calender.cshtml"
                                                                                                         , Tuple.Create(Tuple.Create(" ", 8171), Tuple.Create<System.Object, System.Int32>(cleaning.StartedBy.ToString()
            
            #line default
            #line hidden
, 8172), false)
);

WriteLiteral(">[<b>");

            
            #line 160 "..\..\Views\Report\calender.cshtml"
                                                                                                                                                                                                                         Write(cleaning.destination);

            
            #line default
            #line hidden
WriteLiteral("</b>]</a>\r\n");

            
            #line 161 "..\..\Views\Report\calender.cshtml"
                        cleaning = null;
                    }

            
            #line default
            #line hidden
WriteLiteral("                </td>\r\n");

            
            #line 164 "..\..\Views\Report\calender.cshtml"
           }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 165 "..\..\Views\Report\calender.cshtml"
             if (count != 0)
            {

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral("  style=\"background-color: lightcyan; text-align: center;\"");

WriteLiteral(">");

            
            #line 167 "..\..\Views\Report\calender.cshtml"
                                                                         Write(count);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 168 "..\..\Views\Report\calender.cshtml"
            } else {

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral(" style=\"background-color: lightcyan; text-align: center;\"");

WriteLiteral("></td>\r\n");

            
            #line 170 "..\..\Views\Report\calender.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 171 "..\..\Views\Report\calender.cshtml"
              countOverall += count; count = 0; 
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 172 "..\..\Views\Report\calender.cshtml"
            
            
            #line default
            #line hidden
            
            #line 172 "..\..\Views\Report\calender.cshtml"
             if (rowDOcount != 0)
            {

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral(" style=\"background-color: lightcyan; text-align: center; \"");

WriteLiteral(">");

            
            #line 174 "..\..\Views\Report\calender.cshtml"
                                                                         Write(rowDOcount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 175 "..\..\Views\Report\calender.cshtml"
            }
            else
            {

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral(" style=\"background-color: lightcyan; text-align: center;\"");

WriteLiteral("></td>\r\n");

            
            #line 179 "..\..\Views\Report\calender.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 180 "..\..\Views\Report\calender.cshtml"
               rowDOcountOverall += rowDOcount; rowDOcount = 0; 
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 181 "..\..\Views\Report\calender.cshtml"
            
            
            #line default
            #line hidden
            
            #line 181 "..\..\Views\Report\calender.cshtml"
             if (amntTotal != 0)
            {

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral(" style=\"background-color: lightgray; text-align: center; \"");

WriteLiteral(">");

            
            #line 183 "..\..\Views\Report\calender.cshtml"
                                                                         Write(amntTotal);

            
            #line default
            #line hidden
WriteLiteral(" kg</td>\r\n");

            
            #line 184 "..\..\Views\Report\calender.cshtml"
            } else {

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral(" style=\"background-color: lightgray; text-align: center;\"");

WriteLiteral("></td>\r\n");

            
            #line 186 "..\..\Views\Report\calender.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 187 "..\..\Views\Report\calender.cshtml"
              amntOverall += amntTotal; amntTotal = 0; 
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 188 "..\..\Views\Report\calender.cshtml"
            
            
            #line default
            #line hidden
            
            #line 188 "..\..\Views\Report\calender.cshtml"
             if (rowDOamount != 0)
            {

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral(" style=\"background-color: lightgray; text-align: center; \"");

WriteLiteral(">");

            
            #line 190 "..\..\Views\Report\calender.cshtml"
                                                                         Write(rowDOamount);

            
            #line default
            #line hidden
WriteLiteral(" kg</td>\r\n");

            
            #line 191 "..\..\Views\Report\calender.cshtml"
            }
            else
            {

            
            #line default
            #line hidden
WriteLiteral("                <td");

WriteLiteral(" style=\"background-color: lightgray; text-align: center;\"");

WriteLiteral("></td>\r\n");

            
            #line 195 "..\..\Views\Report\calender.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 196 "..\..\Views\Report\calender.cshtml"
               rowDOamountOverall += rowDOamount; rowDOamount = 0; 
            
            #line default
            #line hidden
WriteLiteral("\r\n        </tr>\r\n");

            
            #line 198 "..\..\Views\Report\calender.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    <tr>\r\n        <td><b>Total</b></td>\r\n");

            
            #line 201 "..\..\Views\Report\calender.cshtml"
        
            
            #line default
            #line hidden
            
            #line 201 "..\..\Views\Report\calender.cshtml"
         for (int hours = 0; hours < 24; hours++)
        {

            
            #line default
            #line hidden
WriteLiteral("            <td");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral("></td>\r\n");

            
            #line 204 "..\..\Views\Report\calender.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        <td");

WriteLiteral(" style=\"text-align: center;\"");

WriteLiteral("><b>");

            
            #line 205 "..\..\Views\Report\calender.cshtml"
                                      Write(countOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n        <td");

WriteLiteral(" style=\"text-align: center;\"");

WriteLiteral("><b>");

            
            #line 206 "..\..\Views\Report\calender.cshtml"
                                      Write(rowDOcountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n        <td");

WriteLiteral(" style=\"text-align: center;\"");

WriteLiteral("><b>");

            
            #line 207 "..\..\Views\Report\calender.cshtml"
                                      Write(amntOverall);

            
            #line default
            #line hidden
WriteLiteral(" kg</b></td>\r\n        <td");

WriteLiteral(" style=\"text-align: center;\"");

WriteLiteral("><b>");

            
            #line 208 "..\..\Views\Report\calender.cshtml"
                                      Write(rowDOamountOverall);

            
            #line default
            #line hidden
WriteLiteral(" kg</b></td>\r\n    </tr>\r\n</table>");

        }
    }
}
#pragma warning restore 1591
