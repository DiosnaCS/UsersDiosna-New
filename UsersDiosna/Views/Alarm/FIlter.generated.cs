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
    
    #line 1 "..\..\Views\Alarm\FIlter.cshtml"
    using UsersDiosna.Handlers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Alarm/FIlter.cshtml")]
    public partial class _Views_Alarm_FIlter_cshtml : System.Web.Mvc.WebViewPage<List<AlarmHelper.alarm_texts>>
    {
        public _Views_Alarm_FIlter_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Alarm\FIlter.cshtml"
  
    ViewBag.Title = "FIlter from alarms";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Filter from alarms</h2>\r\n<div");

WriteLiteral(" class=\"bottom\"");

WriteLiteral(">\r\n    Check checkbox of deisired alarms to filter them\r\n\r\n");

            
            #line 11 "..\..\Views\Alarm\FIlter.cshtml"
    
            
            #line default
            #line hidden
            
            #line 11 "..\..\Views\Alarm\FIlter.cshtml"
     using (Html.BeginForm("FilterFromAlarms", "Alarm", new { ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
    {

            
            #line default
            #line hidden
WriteLiteral("        <table");

WriteLiteral(" class=\"table table-striped table-bordered table-condensed table-hover\"");

WriteLiteral(">\r\n            <tr>\r\n                <th></th>\r\n                <th>Id</th>\r\n    " +
"            <th>Alarm Label</th>\r\n            </tr>\r\n            <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-md-2\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-primary btn-sm\"");

WriteLiteral(" value=\"Create new filter\"");

WriteLiteral(" />\r\n                </div>\r\n                <div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n                    <a");

WriteLiteral(" href=\"/Alarm/\"");

WriteLiteral(" class=\"btn btn-success btn-sm\"");

WriteLiteral(">Back</a>\r\n                </div>\r\n            </div>\r\n");

            
            #line 27 "..\..\Views\Alarm\FIlter.cshtml"
            
            
            #line default
            #line hidden
            
            #line 27 "..\..\Views\Alarm\FIlter.cshtml"
             foreach (AlarmHelper.alarm_texts Alarm in Model)
            {

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td><input");

WriteLiteral(" type=\"checkbox\"");

WriteAttribute("name", Tuple.Create(" name=\"", 1131), Tuple.Create("\"", 1147)
            
            #line 30 "..\..\Views\Alarm\FIlter.cshtml"
, Tuple.Create(Tuple.Create("", 1138), Tuple.Create<System.Object, System.Int32>(Alarm.id
            
            #line default
            #line hidden
, 1138), false)
);

WriteLiteral(">");

WriteLiteral("</td>\r\n                    <td>");

            
            #line 31 "..\..\Views\Alarm\FIlter.cshtml"
                   Write(Alarm.id);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td>");

            
            #line 32 "..\..\Views\Alarm\FIlter.cshtml"
                   Write(Alarm.title);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                </tr>\r\n");

            
            #line 34 "..\..\Views\Alarm\FIlter.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n        </table>\r\n");

WriteLiteral("            <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n                <div");

WriteLiteral(" class=\"col-md-2\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-primary btn-sm\"");

WriteLiteral("\" value=\"Create new filter\" />\r\n                </div>\r\n                <div clas" +
"s=\"col-md-10\">\r\n                    <a href=\"/Alarm/\"");

WriteLiteral(" class=\"btn btn-success btn-sm\"");

WriteLiteral(">Back</a>\r\n");

WriteLiteral("                </div>\r\n");

WriteLiteral("            </div>\r\n");

            
            #line 45 "..\..\Views\Alarm\FIlter.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591
