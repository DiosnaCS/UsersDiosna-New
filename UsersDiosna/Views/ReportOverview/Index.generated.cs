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
    
    #line 2 "..\..\Views\ReportOverview\Index.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    
    #line 1 "..\..\Views\ReportOverview\Index.cshtml"
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ReportOverview/Index.cshtml")]
    public partial class _Views_ReportOverview_Index_cshtml : System.Web.Mvc.WebViewPage<UsersDiosna.Report.Models.OverviewReportModel>
    {
        public _Views_ReportOverview_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 5 "..\..\Views\ReportOverview\Index.cshtml"
  
    ViewBag.Title = "Index";
    string name = string.Empty;
    int EndType = 0;
    int amntTotal = 0;
    int MotherAmntOverall = 0;
    int MotherCountOverall = 0;
    int FlourAmountOverall = 0;
    int FlourCountOverall = 0;
    int WaterAmountOverall = 0;
    int WaterCountOverall = 0;
    int OldBreadAmountOverall = 0;
    int OldBreadCountOverall = 0;
    int LiquidYeastAmountOverall = 0;
    int LiquidYeastCountOverall = 0;
    int MixtureAmountOverall = 0;
    int MixtureCountOverall = 0;
    int GenericAmountOverall = 0;
    int GenericCountOverall = 0;
    int amnt = 0;
    bool colourRow;
    int countHeades = 0;
    int countOverall = 0;
    int year = ViewBag.year;
    int monthLess = ViewBag.Month - 1;
    int monthMore = ViewBag.Month + 1;
    if (monthLess < 1)
    {
        year--;
        monthLess = 12;
    }
    if (monthMore > 12)
    {
        year++;
        monthMore = 1;
    }
    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
    DateTime monthDT = new DateTime(year, ViewBag.month, 1);
    string monthName = monthDT.ToString("MMMM");


            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Cunsumption Report</h2>\r\n\r\n<div");

WriteLiteral(" class=\" col-md-10\"");

WriteLiteral(">\r\n    <a");

WriteLiteral(" class=\"btn-primary btn-xs\"");

WriteAttribute("href", Tuple.Create(" href=\"", 1347), Tuple.Create("\"", 1392)
, Tuple.Create(Tuple.Create("", 1354), Tuple.Create("/ReportOverview/Month/", 1354), true)
            
            #line 50 "..\..\Views\ReportOverview\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1376), Tuple.Create<System.Object, System.Int32>(monthLess
            
            #line default
            #line hidden
, 1376), false)
, Tuple.Create(Tuple.Create("", 1386), Tuple.Create("/", 1386), true)
            
            #line 50 "..\..\Views\ReportOverview\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1387), Tuple.Create<System.Object, System.Int32>(year
            
            #line default
            #line hidden
, 1387), false)
);

WriteLiteral(">-1 month</a> Month: ");

            
            #line 50 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                               Write(monthName);

            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 50 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                          Write(year);

            
            #line default
            #line hidden
WriteLiteral(" <a");

WriteLiteral(" class=\"btn-primary btn-xs\"");

WriteAttribute("href", Tuple.Create(" href=\"", 1460), Tuple.Create("\"", 1505)
, Tuple.Create(Tuple.Create("", 1467), Tuple.Create("/ReportOverview/Month/", 1467), true)
            
            #line 50 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                               , Tuple.Create(Tuple.Create("", 1489), Tuple.Create<System.Object, System.Int32>(monthMore
            
            #line default
            #line hidden
, 1489), false)
, Tuple.Create(Tuple.Create("", 1499), Tuple.Create("/", 1499), true)
            
            #line 50 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                          , Tuple.Create(Tuple.Create("", 1500), Tuple.Create<System.Object, System.Int32>(year
            
            #line default
            #line hidden
, 1500), false)
);

WriteLiteral(">+1 month</a>\r\n    <p></p>\r\n</div>\r\n<table");

WriteLiteral(" class=\"table-bordered table-condensed table-hover\"");

WriteLiteral(">\r\n    <thead>\r\n        <tr>\r\n            <th>Day</th>\r\n");

            
            #line 57 "..\..\Views\ReportOverview\Index.cshtml"
            
            
            #line default
            #line hidden
            
            #line 57 "..\..\Views\ReportOverview\Index.cshtml"
             if (Model.Data.Max(p => p.MotherCultureBatchCount > 0)) {

            
            #line default
            #line hidden
WriteLiteral("                <th>Mother culture</th>\r\n");

WriteLiteral("                <td");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral("></td>\r\n");

            
            #line 60 "..\..\Views\ReportOverview\Index.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 61 "..\..\Views\ReportOverview\Index.cshtml"
             if (Model.Data.Max(p => p.FlourBatchCount > 0)) {

            
            #line default
            #line hidden
WriteLiteral("                <th>Flour</th>\r\n");

WriteLiteral("                <td");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral("></td>\r\n");

            
            #line 64 "..\..\Views\ReportOverview\Index.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 65 "..\..\Views\ReportOverview\Index.cshtml"
             if (Model.Data.Max(p => p.WaterBatchCount > 0)) {

            
            #line default
            #line hidden
WriteLiteral("                <th>Water</th>\r\n");

WriteLiteral("                <td");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral("></td>\r\n");

            
            #line 68 "..\..\Views\ReportOverview\Index.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 69 "..\..\Views\ReportOverview\Index.cshtml"
             if (Model.Data.Max(p => p.OldBreadBatchCount > 0)) {

            
            #line default
            #line hidden
WriteLiteral("                <th>Old Bread</th>\r\n");

WriteLiteral("                <td");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral("></td>\r\n");

            
            #line 72 "..\..\Views\ReportOverview\Index.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 73 "..\..\Views\ReportOverview\Index.cshtml"
             if (Model.Data.Max(p => p.LiquidYeastBatchCount > 0)) {

            
            #line default
            #line hidden
WriteLiteral("                <th>Liquid yeast</th>\r\n");

WriteLiteral("                <td");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral("></td>\r\n");

            
            #line 76 "..\..\Views\ReportOverview\Index.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 77 "..\..\Views\ReportOverview\Index.cshtml"
             if (Model.Data.Max(p => p.MixtureBatchCount > 0)) {

            
            #line default
            #line hidden
WriteLiteral("                <th>Mixture</th>\r\n");

WriteLiteral("                <td");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral("></td>\r\n");

            
            #line 80 "..\..\Views\ReportOverview\Index.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            ");

            
            #line 81 "..\..\Views\ReportOverview\Index.cshtml"
             if (Model.Data.Max(p => p.GenericBatchCount > 0)) {

            
            #line default
            #line hidden
WriteLiteral("                <th>Generic component</th>\r\n");

WriteLiteral("                <td");

WriteLiteral(" class=\"th-Report\"");

WriteLiteral("></td>\r\n");

            
            #line 84 "..\..\Views\ReportOverview\Index.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </tr>\r\n    </thead>\r\n");

            
            #line 87 "..\..\Views\ReportOverview\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 87 "..\..\Views\ReportOverview\Index.cshtml"
     for (int day = 1; day <= DateTime.DaysInMonth(year, ViewBag.month); day++)
    {
        
            
            #line default
            #line hidden
            
            #line 89 "..\..\Views\ReportOverview\Index.cshtml"
           DateTime dayDT = new DateTime(year, ViewBag.month, day); string sDay = dayDT.DayOfWeek.ToString();  
            
            #line default
            #line hidden
            
            #line 89 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n");

            
            #line 91 "..\..\Views\ReportOverview\Index.cshtml"
                
            
            #line default
            #line hidden
            
            #line 91 "..\..\Views\ReportOverview\Index.cshtml"
                 if (dayDT.DayOfWeek == DayOfWeek.Saturday || dayDT.DayOfWeek == DayOfWeek.Sunday)
                {
                    colourRow = true;

            
            #line default
            #line hidden
WriteLiteral("                    <td");

WriteLiteral(" style=\"background-color: lightgoldenrodyellow\"");

WriteLiteral(">");

            
            #line 94 "..\..\Views\ReportOverview\Index.cshtml"
                                                                  Write(day);

            
            #line default
            #line hidden
WriteLiteral(". ");

            
            #line 94 "..\..\Views\ReportOverview\Index.cshtml"
                                                                        Write(sDay);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 95 "..\..\Views\ReportOverview\Index.cshtml"
                }
                else
                {
                    colourRow = false;

            
            #line default
            #line hidden
WriteLiteral("                    <td>");

            
            #line 99 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(day);

            
            #line default
            #line hidden
WriteLiteral(". ");

            
            #line 99 "..\..\Views\ReportOverview\Index.cshtml"
                         Write(sDay);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 100 "..\..\Views\ReportOverview\Index.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("                ");

            
            #line 101 "..\..\Views\ReportOverview\Index.cshtml"
                 if (Model.Data.Exists(p => p.day == day))
                {
                    
            
            #line default
            #line hidden
            
            #line 103 "..\..\Views\ReportOverview\Index.cshtml"
                       int? MotherAmount = Model.Data.Single(p => p.day == day).MotherCultureAmnt / 1000;
                        if (MotherAmount != null) {
                            MotherAmntOverall += (int)MotherAmount; }
            
            #line default
            #line hidden
            
            #line 105 "..\..\Views\ReportOverview\Index.cshtml"
                                                                      
                    
            
            #line default
            #line hidden
            
            #line 106 "..\..\Views\ReportOverview\Index.cshtml"
                       int? MotherCount = Model.Data.Single(p => p.day == day).MotherCultureBatchCount;
                        if (MotherCount != null)
                        {
                            MotherCountOverall += (int)MotherCount;
                        } 
            
            #line default
            #line hidden
            
            #line 110 "..\..\Views\ReportOverview\Index.cshtml"
                           
                    
            
            #line default
            #line hidden
            
            #line 111 "..\..\Views\ReportOverview\Index.cshtml"
                      int? FlourAmount = Model.Data.Single(p => p.day == day).FlourAmnt / 1000; if (FlourAmount != null)
                        {
                            FlourAmountOverall += (int)FlourAmount;
                        } 
            
            #line default
            #line hidden
            
            #line 114 "..\..\Views\ReportOverview\Index.cshtml"
                           
                    
            
            #line default
            #line hidden
            
            #line 115 "..\..\Views\ReportOverview\Index.cshtml"
                      int? FlourCount = Model.Data.Single(p => p.day == day).FlourBatchCount; if (FlourCount != null)
                        {
                            FlourCountOverall += (int)FlourCount;
                        } 
            
            #line default
            #line hidden
            
            #line 118 "..\..\Views\ReportOverview\Index.cshtml"
                           
                    
            
            #line default
            #line hidden
            
            #line 119 "..\..\Views\ReportOverview\Index.cshtml"
                      int? WaterAmount = Model.Data.Single(p => p.day == day).WaterAmnt / 1000; if (WaterAmount != null)
                            {
                                WaterAmountOverall += (int)WaterAmount;
                            }
            
            #line default
            #line hidden
            
            #line 122 "..\..\Views\ReportOverview\Index.cshtml"
                              
                    
            
            #line default
            #line hidden
            
            #line 123 "..\..\Views\ReportOverview\Index.cshtml"
                      int? WaterCount = Model.Data.Single(p => p.day == day).WaterBatchCount; if (WaterCount != null) { WaterCountOverall += (int)WaterCount; } 
            
            #line default
            #line hidden
            
            #line 123 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                 
                    
            
            #line default
            #line hidden
            
            #line 124 "..\..\Views\ReportOverview\Index.cshtml"
                      int? OldBreadAmount = Model.Data.Single(p => p.day == day).OldBreadAmnt / 1000; if (OldBreadAmount != null) { OldBreadAmountOverall += (int)OldBreadAmount; } 
            
            #line default
            #line hidden
            
            #line 124 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                                     
                    
            
            #line default
            #line hidden
            
            #line 125 "..\..\Views\ReportOverview\Index.cshtml"
                      int? OldBreadCount = Model.Data.Single(p => p.day == day).OldBreadBatchCount; if (OldBreadCount != null) { OldBreadCountOverall += (int)OldBreadCount; } 
            
            #line default
            #line hidden
            
            #line 125 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                                
                    
            
            #line default
            #line hidden
            
            #line 126 "..\..\Views\ReportOverview\Index.cshtml"
                      int? LiquidYeastAmount = Model.Data.Single(p => p.day == day).LiquidYeastAmnt / 1000; if (LiquidYeastAmount != null) { LiquidYeastAmountOverall += (int)LiquidYeastAmount; } 
            
            #line default
            #line hidden
            
            #line 126 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                                                    
                    
            
            #line default
            #line hidden
            
            #line 127 "..\..\Views\ReportOverview\Index.cshtml"
                      int? LiquidYeastCount = Model.Data.Single(p => p.day == day).LiquidYeastBatchCount; if (LiquidYeastCount != null) { LiquidYeastCountOverall += (int)LiquidYeastCount; } 
            
            #line default
            #line hidden
            
            #line 127 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                                               
                    
            
            #line default
            #line hidden
            
            #line 128 "..\..\Views\ReportOverview\Index.cshtml"
                      int? MixtureAmount = Model.Data.Single(p => p.day == day).MixtureAmnt / 1000; if (MixtureAmount != null) { MixtureAmountOverall += (int)MixtureAmount; } 
            
            #line default
            #line hidden
            
            #line 128 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                                
                    
            
            #line default
            #line hidden
            
            #line 129 "..\..\Views\ReportOverview\Index.cshtml"
                      int? MixtureCount = Model.Data.Single(p => p.day == day).MixtureBatchCount; if (MixtureCount != null) { MixtureCountOverall += (int)MixtureCount; } 
            
            #line default
            #line hidden
            
            #line 129 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                           
                    
            
            #line default
            #line hidden
            
            #line 130 "..\..\Views\ReportOverview\Index.cshtml"
                      int? GenericAmount = Model.Data.Single(p => p.day == day).GenericAmnt / 1000; if (GenericAmount != null) { GenericAmountOverall += (int)GenericAmount; } 
            
            #line default
            #line hidden
            
            #line 130 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                                
                    
            
            #line default
            #line hidden
            
            #line 131 "..\..\Views\ReportOverview\Index.cshtml"
                      int? GenericCount = Model.Data.Single(p => p.day == day).GenericBatchCount; if (GenericCount != null) { GenericCountOverall += (int)GenericCount; } 
            
            #line default
            #line hidden
            
            #line 131 "..\..\Views\ReportOverview\Index.cshtml"
                                                                                                                                                                           

            
            #line default
            #line hidden
WriteLiteral("                    <td>");

            
            #line 132 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(MotherAmount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 133 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(MotherCount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 134 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(FlourAmount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 135 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(FlourCount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 136 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(WaterAmount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 137 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(WaterCount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 138 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(OldBreadAmount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 139 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(OldBreadCount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 140 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(LiquidYeastAmount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 141 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(LiquidYeastCount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 142 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(MixtureAmount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 143 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(MixtureCount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 144 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(GenericAmount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

WriteLiteral("                    <td>");

            
            #line 145 "..\..\Views\ReportOverview\Index.cshtml"
                   Write(GenericCount);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 146 "..\..\Views\ReportOverview\Index.cshtml"
                }
               
            
            #line default
            #line hidden
            
            #line 163 "..\..\Views\ReportOverview\Index.cshtml"
                   

            
            #line default
            #line hidden
WriteLiteral("            </tr>\r\n");

            
            #line 165 "..\..\Views\ReportOverview\Index.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("    <tr>\r\n        <td><b>Summary</b></td>\r\n");

            
            #line 168 "..\..\Views\ReportOverview\Index.cshtml"
        
            
            #line default
            #line hidden
            
            #line 168 "..\..\Views\ReportOverview\Index.cshtml"
         if (MotherAmntOverall != 0) {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 169 "..\..\Views\ReportOverview\Index.cshtml"
              Write(MotherAmntOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 170 "..\..\Views\ReportOverview\Index.cshtml"
        } else {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 172 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 173 "..\..\Views\ReportOverview\Index.cshtml"
         if (MotherCountOverall != 0) {

            
            #line default
            #line hidden
WriteLiteral("        <td><b>");

            
            #line 174 "..\..\Views\ReportOverview\Index.cshtml"
          Write(MotherCountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 175 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 178 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 179 "..\..\Views\ReportOverview\Index.cshtml"
         if (FlourAmountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 180 "..\..\Views\ReportOverview\Index.cshtml"
              Write(FlourAmountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 181 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 184 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 185 "..\..\Views\ReportOverview\Index.cshtml"
         if (FlourCountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 186 "..\..\Views\ReportOverview\Index.cshtml"
              Write(FlourCountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 187 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 190 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 191 "..\..\Views\ReportOverview\Index.cshtml"
         if (WaterAmountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 192 "..\..\Views\ReportOverview\Index.cshtml"
              Write(WaterAmountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 193 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 196 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 197 "..\..\Views\ReportOverview\Index.cshtml"
         if (WaterCountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 198 "..\..\Views\ReportOverview\Index.cshtml"
              Write(MotherCountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 199 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 202 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 203 "..\..\Views\ReportOverview\Index.cshtml"
         if (OldBreadAmountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 204 "..\..\Views\ReportOverview\Index.cshtml"
              Write(OldBreadAmountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 205 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 208 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 209 "..\..\Views\ReportOverview\Index.cshtml"
         if (OldBreadCountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 210 "..\..\Views\ReportOverview\Index.cshtml"
              Write(OldBreadCountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 211 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 214 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 215 "..\..\Views\ReportOverview\Index.cshtml"
         if (LiquidYeastAmountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 216 "..\..\Views\ReportOverview\Index.cshtml"
              Write(LiquidYeastAmountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 217 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 220 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 221 "..\..\Views\ReportOverview\Index.cshtml"
         if (LiquidYeastCountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 222 "..\..\Views\ReportOverview\Index.cshtml"
              Write(LiquidYeastCountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 223 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 226 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 227 "..\..\Views\ReportOverview\Index.cshtml"
         if (MixtureAmountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 228 "..\..\Views\ReportOverview\Index.cshtml"
              Write(MixtureAmountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 229 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 232 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 233 "..\..\Views\ReportOverview\Index.cshtml"
         if (MixtureCountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 234 "..\..\Views\ReportOverview\Index.cshtml"
              Write(MixtureCountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 235 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 238 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 239 "..\..\Views\ReportOverview\Index.cshtml"
         if (GenericAmountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 240 "..\..\Views\ReportOverview\Index.cshtml"
              Write(GenericAmountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 241 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 244 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        ");

            
            #line 245 "..\..\Views\ReportOverview\Index.cshtml"
         if (GenericCountOverall != 0)        {

            
            #line default
            #line hidden
WriteLiteral("            <td><b>");

            
            #line 246 "..\..\Views\ReportOverview\Index.cshtml"
              Write(GenericCountOverall);

            
            #line default
            #line hidden
WriteLiteral("</b></td>\r\n");

            
            #line 247 "..\..\Views\ReportOverview\Index.cshtml"
        }
        else        {

            
            #line default
            #line hidden
WriteLiteral("            <td></td>\r\n");

            
            #line 250 "..\..\Views\ReportOverview\Index.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </tr>\r\n</table>");

        }
    }
}
#pragma warning restore 1591
