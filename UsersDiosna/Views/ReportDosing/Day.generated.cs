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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/ReportDosing/Day.cshtml")]
    public partial class _Views_ReportDosing_Day_cshtml : System.Web.Mvc.WebViewPage<UsersDiosna.Report.Models.ReportDosing>
    {
        public _Views_ReportDosing_Day_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\ReportDosing\Day.cshtml"
  
    ViewBag.Title = "One dosing day";
    DateTime thisDay = new DateTime(Model.Year, Model.Month, Model.Day);

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h4>Day of dosings</h4>\r\n\r\n<p>\r\n    <a");

WriteLiteral(" class=\"btn-sm btn-primary\"");

WriteAttribute("href", Tuple.Create(" href=\"", 236), Tuple.Create("\"", 299)
, Tuple.Create(Tuple.Create("", 243), Tuple.Create("/ReporDosing/Day/", 243), true)
            
            #line 11 "..\..\Views\ReportDosing\Day.cshtml"
, Tuple.Create(Tuple.Create("", 260), Tuple.Create<System.Object, System.Int32>(Model.Day-1
            
            #line default
            #line hidden
, 260), false)
, Tuple.Create(Tuple.Create("", 274), Tuple.Create("/", 274), true)
            
            #line 11 "..\..\Views\ReportDosing\Day.cshtml"
, Tuple.Create(Tuple.Create("", 275), Tuple.Create<System.Object, System.Int32>(Model.Month
            
            #line default
            #line hidden
, 275), false)
, Tuple.Create(Tuple.Create("", 287), Tuple.Create("/", 287), true)
            
            #line 11 "..\..\Views\ReportDosing\Day.cshtml"
      , Tuple.Create(Tuple.Create("", 288), Tuple.Create<System.Object, System.Int32>(Model.Year
            
            #line default
            #line hidden
, 288), false)
);

WriteLiteral(">&lArr; Day</a> \r\n");

WriteLiteral("    ");

            
            #line 12 "..\..\Views\ReportDosing\Day.cshtml"
Write(thisDay.DayOfWeek);

            
            #line default
            #line hidden
WriteLiteral(" ");

            
            #line 12 "..\..\Views\ReportDosing\Day.cshtml"
                  Write(thisDay.ToShortDateString());

            
            #line default
            #line hidden
WriteLiteral("\r\n    <a");

WriteLiteral(" class=\"btn-sm btn-primary\"");

WriteAttribute("href", Tuple.Create(" href=\"", 404), Tuple.Create("\"", 467)
, Tuple.Create(Tuple.Create("", 411), Tuple.Create("/ReporDosing/Day/", 411), true)
            
            #line 13 "..\..\Views\ReportDosing\Day.cshtml"
, Tuple.Create(Tuple.Create("", 428), Tuple.Create<System.Object, System.Int32>(Model.Day+1
            
            #line default
            #line hidden
, 428), false)
, Tuple.Create(Tuple.Create("", 442), Tuple.Create("/", 442), true)
            
            #line 13 "..\..\Views\ReportDosing\Day.cshtml"
, Tuple.Create(Tuple.Create("", 443), Tuple.Create<System.Object, System.Int32>(Model.Month
            
            #line default
            #line hidden
, 443), false)
, Tuple.Create(Tuple.Create("", 455), Tuple.Create("/", 455), true)
            
            #line 13 "..\..\Views\ReportDosing\Day.cshtml"
      , Tuple.Create(Tuple.Create("", 456), Tuple.Create<System.Object, System.Int32>(Model.Year
            
            #line default
            #line hidden
, 456), false)
);

WriteLiteral(">Day &rArr;</a> \r\n</p>\r\n<table");

WriteLiteral(" class=\"table table-condensed table-condensed table-hover\"");

WriteLiteral(@">
    <tr>
        <th>
            Record id
        </th>
        <th>
            Started at
        </th>
        <th>
            Ended at
        </th>
        <th>
            Destination
        </th>
        <th>
            Actual amount dosed
        </th>
        <th>
            Source Tank
        </th>
        <th>
            Source batch number
        </th>
    </tr>
");

            
            #line 39 "..\..\Views\ReportDosing\Day.cshtml"
 foreach (var item in Model.Batches) {

            
            #line default
            #line hidden
WriteLiteral("    <tr>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 42 "..\..\Views\ReportDosing\Day.cshtml"
       Write(Html.DisplayFor(modelItem => item.RecordNo));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 45 "..\..\Views\ReportDosing\Day.cshtml"
       Write(Html.DisplayFor(modelItem => item.TimeStart));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 48 "..\..\Views\ReportDosing\Day.cshtml"
       Write(Html.DisplayFor(modelItem => item.TimeEnd));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 51 "..\..\Views\ReportDosing\Day.cshtml"
       Write(Html.DisplayFor(modelItem => item.Destination));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 54 "..\..\Views\ReportDosing\Day.cshtml"
       Write(Html.DisplayFor(modelItem => item.Actual));

            
            #line default
            #line hidden
WriteLiteral(" g\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 57 "..\..\Views\ReportDosing\Day.cshtml"
       Write(Html.DisplayFor(modelItem => item.SrcTank));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 60 "..\..\Views\ReportDosing\Day.cshtml"
       Write(Html.DisplayFor(modelItem => item.SrcBatchNo));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n    </tr>\r\n");

            
            #line 63 "..\..\Views\ReportDosing\Day.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n</table>\r\n");

        }
    }
}
#pragma warning restore 1591
