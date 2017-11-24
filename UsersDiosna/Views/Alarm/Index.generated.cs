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
    
    #line 1 "..\..\Views\Alarm\Index.cshtml"
    using UsersDiosna.Handlers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Alarm/Index.cshtml")]
    public partial class _Views_Alarm_Index_cshtml : System.Web.Mvc.WebViewPage<List<AlarmHelper.alarm>>
    {
        public _Views_Alarm_Index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

            
            #line 5 "..\..\Views\Alarm\Index.cshtml"
  
    ViewBag.Title = "Alarms";
    int pageLess = ViewBag.page - 1;
    int pageMore = ViewBag.page + 1;
    List<int> currentAlarmIDs = new List<int>();
    ViewBag.filtered = Session["filtered"];

            
            #line default
            #line hidden
WriteLiteral("\r\n<h4>Alarms</h4>\r\n<div>\r\n");

            
            #line 14 "..\..\Views\Alarm\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 14 "..\..\Views\Alarm\Index.cshtml"
     if (pageLess >= 0)
    {

            
            #line default
            #line hidden
WriteLiteral("        <a");

WriteLiteral(" class=\"btn-sm btn-primary\"");

WriteAttribute("href", Tuple.Create(" href=\"", 367), Tuple.Create("\"", 396)
, Tuple.Create(Tuple.Create("", 374), Tuple.Create("/Alarm/Page/", 374), true)
            
            #line 16 "..\..\Views\Alarm\Index.cshtml"
, Tuple.Create(Tuple.Create("", 386), Tuple.Create<System.Object, System.Int32>(pageLess
            
            #line default
            #line hidden
, 386), false)
, Tuple.Create(Tuple.Create("", 395), Tuple.Create("/", 395), true)
);

WriteLiteral(">&lArr;</a>\r\n");

            
            #line 17 "..\..\Views\Alarm\Index.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    Page: ");

            
            #line 18 "..\..\Views\Alarm\Index.cshtml"
     Write(ViewBag.page);

            
            #line default
            #line hidden
WriteLiteral("\r\n    <a");

WriteLiteral(" class=\"btn-sm btn-primary\"");

WriteAttribute("href", Tuple.Create(" href=\"", 475), Tuple.Create("\"", 504)
, Tuple.Create(Tuple.Create("", 482), Tuple.Create("/Alarm/Page/", 482), true)
            
            #line 19 "..\..\Views\Alarm\Index.cshtml"
, Tuple.Create(Tuple.Create("", 494), Tuple.Create<System.Object, System.Int32>(pageMore
            
            #line default
            #line hidden
, 494), false)
, Tuple.Create(Tuple.Create("", 503), Tuple.Create("/", 503), true)
);

WriteLiteral(">&rArr;</a>\r\n");

            
            #line 20 "..\..\Views\Alarm\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 20 "..\..\Views\Alarm\Index.cshtml"
     if (ViewBag.filtered != null)
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"right\"");

WriteLiteral(">\r\n            <br>\r\n");

WriteLiteral("            ");

            
            #line 24 "..\..\Views\Alarm\Index.cshtml"
       Write(ViewBag.filtered);

            
            #line default
            #line hidden
WriteLiteral("\r\n            <a");

WriteLiteral(" class=\"btn-sm btn-info\"");

WriteLiteral(" href=\"/Alarm/CancelFilter/\"");

WriteLiteral(">Reset current filter on alarms</a>\r\n        </div>\r\n");

            
            #line 27 "..\..\Views\Alarm\Index.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 28 "..\..\Views\Alarm\Index.cshtml"
     if (Session["AlarmDB"].ToString() != "")
    {

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"btn-toolbar\"");

WriteLiteral(">\r\n        <br>\r\n        <p>\r\n        <a");

WriteLiteral(" class=\"btn-sm btn-primary\"");

WriteAttribute("href", Tuple.Create(" href=\"", 914), Tuple.Create("\"", 1009)
, Tuple.Create(Tuple.Create("", 921), Tuple.Create("/AlarmNotification/All?name=", 921), true)
            
            #line 33 "..\..\Views\Alarm\Index.cshtml"
, Tuple.Create(Tuple.Create("", 949), Tuple.Create<System.Object, System.Int32>(Request.QueryString["name"]
            
            #line default
            #line hidden
, 949), false)
, Tuple.Create(Tuple.Create("", 977), Tuple.Create("&plc=", 977), true)
            
            #line 33 "..\..\Views\Alarm\Index.cshtml"
                          , Tuple.Create(Tuple.Create("", 982), Tuple.Create<System.Object, System.Int32>(Request.QueryString["plc"]
            
            #line default
            #line hidden
, 982), false)
);

WriteLiteral(">Notification from all alarms</a>\r\n        <a");

WriteLiteral(" class=\"btn-sm btn-primary\"");

WriteLiteral(" href=\"/Alarm/FilterAll\"");

WriteLiteral(">Filter from all alarms</a>\r\n        <a");

WriteLiteral(" class=\"btn-sm btn-primary\"");

WriteLiteral(" href=\"/Alarm/FilterCurrent\"");

WriteLiteral(">Filter from current alarms</a>\r\n        <a");

WriteLiteral(" class=\"btn-sm btn-primary\"");

WriteAttribute("href", Tuple.Create(" href=\"", 1270), Tuple.Create("\"", 1373)
, Tuple.Create(Tuple.Create("", 1277), Tuple.Create("/AlarmNotification/FromCurrent?name=", 1277), true)
            
            #line 36 "..\..\Views\Alarm\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1313), Tuple.Create<System.Object, System.Int32>(Request.QueryString["name"]
            
            #line default
            #line hidden
, 1313), false)
, Tuple.Create(Tuple.Create("", 1341), Tuple.Create("&plc=", 1341), true)
            
            #line 36 "..\..\Views\Alarm\Index.cshtml"
                                 , Tuple.Create(Tuple.Create("", 1346), Tuple.Create<System.Object, System.Int32>(Request.QueryString["plc"]
            
            #line default
            #line hidden
, 1346), false)
);

WriteLiteral(">Notification from current alarms</a>\r\n        </p>\r\n    </div>\r\n");

            
            #line 39 "..\..\Views\Alarm\Index.cshtml"
    }
    else
    {

            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"btn-toolbar\"");

WriteLiteral(">\r\n            <a");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" href=\"/Alarm/Filter\"");

WriteLiteral(">Filter from all alarms</a>\r\n            <a");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" href=\"/Alarm/Filter\"");

WriteLiteral(">Filter from current alarms</a>\r\n        </div>\r\n");

            
            #line 46 "..\..\Views\Alarm\Index.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n<table");

WriteLiteral(" class=\"table table-striped table-bordered table-condensed table-hover\"");

WriteLiteral(">\r\n    <tr>\r\n        <th>Id</th>\r\n        <th>Name of alarm</th>\r\n        <th>Ori" +
"gin</th>\r\n        <th>Expiry</th>\r\n    </tr>\r\n");

            
            #line 55 "..\..\Views\Alarm\Index.cshtml"
    
            
            #line default
            #line hidden
            
            #line 55 "..\..\Views\Alarm\Index.cshtml"
     for (int i = 0; i < Model.Count; i++)
    {

            
            #line default
            #line hidden
WriteLiteral("        <tr>\r\n            <td>");

            
            #line 58 "..\..\Views\Alarm\Index.cshtml"
           Write(Model[i].id);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n");

            
            #line 59 "..\..\Views\Alarm\Index.cshtml"
            
            
            #line default
            #line hidden
            
            #line 59 "..\..\Views\Alarm\Index.cshtml"
             if (!currentAlarmIDs.Exists(p => p == Model[i].id)) {
                currentAlarmIDs.Add(Model[i].id);
            }

            
            #line default
            #line hidden
WriteLiteral("            <td>");

            
            #line 62 "..\..\Views\Alarm\Index.cshtml"
           Write(Model[i].title);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            <td");

WriteAttribute("id", Tuple.Create(" id=\"", 2198), Tuple.Create("\"", 2210)
, Tuple.Create(Tuple.Create("", 2203), Tuple.Create("date_", 2203), true)
            
            #line 63 "..\..\Views\Alarm\Index.cshtml"
, Tuple.Create(Tuple.Create("", 2208), Tuple.Create<System.Object, System.Int32>(i
            
            #line default
            #line hidden
, 2208), false)
);

WriteLiteral(">");

            
            #line 63 "..\..\Views\Alarm\Index.cshtml"
                        Write(Model[i].originTime);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            <td");

WriteAttribute("id", Tuple.Create(" id=\"", 2254), Tuple.Create("\"", 2269)
, Tuple.Create(Tuple.Create("", 2259), Tuple.Create("expDate_", 2259), true)
            
            #line 64 "..\..\Views\Alarm\Index.cshtml"
, Tuple.Create(Tuple.Create("", 2267), Tuple.Create<System.Object, System.Int32>(i
            
            #line default
            #line hidden
, 2267), false)
);

WriteLiteral(">");

            
            #line 64 "..\..\Views\Alarm\Index.cshtml"
                           Write(Model[i].expiryTime);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n        </tr>\r\n");

            
            #line 66 "..\..\Views\Alarm\Index.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    ");

            
            #line 67 "..\..\Views\Alarm\Index.cshtml"
      Session["alarmIDs"] = currentAlarmIDs;
            
            #line default
            #line hidden
WriteLiteral(@"
</table>
<script>
        function DateTime(date){
                var offset = date.getTimezoneOffset();
                var offseted_miliseconds = date.getTime() - (offset*60000);
                var DateTime = new Date(offseted_miliseconds);
                var localDateTime = DateTime.toLocaleString();
                return localDateTime;
            }
</script>
");

            
            #line 78 "..\..\Views\Alarm\Index.cshtml"
 for (int i = 0; i < Model.Count; i++)
{

            
            #line default
            #line hidden
WriteLiteral("    <script>\r\n        var year = ");

            
            #line 81 "..\..\Views\Alarm\Index.cshtml"
              Write(Model[i].originTime.Year);

            
            #line default
            #line hidden
WriteLiteral(";\r\n        var month = ");

            
            #line 82 "..\..\Views\Alarm\Index.cshtml"
               Write(Model[i].originTime.Month);

            
            #line default
            #line hidden
WriteLiteral(";\r\n        var day = ");

            
            #line 83 "..\..\Views\Alarm\Index.cshtml"
             Write(Model[i].originTime.Day);

            
            #line default
            #line hidden
WriteLiteral(";\r\n        var hour = ");

            
            #line 84 "..\..\Views\Alarm\Index.cshtml"
              Write(Model[i].originTime.Hour);

            
            #line default
            #line hidden
WriteLiteral(";\r\n        var minute = ");

            
            #line 85 "..\..\Views\Alarm\Index.cshtml"
                Write(Model[i].originTime.Minute);

            
            #line default
            #line hidden
WriteLiteral(";\r\n        var second = ");

            
            #line 86 "..\..\Views\Alarm\Index.cshtml"
                Write(Model[i].originTime.Second);

            
            #line default
            #line hidden
WriteLiteral(";\r\n        var date = new Date(year, month, day, hour, minute, second, 0);\r\n     " +
"   console.log(date);\r\n                var yearExp = ");

            
            #line 89 "..\..\Views\Alarm\Index.cshtml"
                         Write(Model[i].expiryTime.Year);

            
            #line default
            #line hidden
WriteLiteral(";\r\n                var monthExp = ");

            
            #line 90 "..\..\Views\Alarm\Index.cshtml"
                          Write(Model[i].expiryTime.Month);

            
            #line default
            #line hidden
WriteLiteral(";\r\n                var dayExp = ");

            
            #line 91 "..\..\Views\Alarm\Index.cshtml"
                        Write(Model[i].expiryTime.Day);

            
            #line default
            #line hidden
WriteLiteral(";\r\n                var hourExp = ");

            
            #line 92 "..\..\Views\Alarm\Index.cshtml"
                         Write(Model[i].expiryTime.Hour);

            
            #line default
            #line hidden
WriteLiteral(";\r\n                var minuteExp = ");

            
            #line 93 "..\..\Views\Alarm\Index.cshtml"
                           Write(Model[i].expiryTime.Minute);

            
            #line default
            #line hidden
WriteLiteral(";\r\n                var secondExp = ");

            
            #line 94 "..\..\Views\Alarm\Index.cshtml"
                           Write(Model[i].expiryTime.Second);

            
            #line default
            #line hidden
WriteLiteral(";\r\n                var expDate = new Date(yearExp, monthExp, dayExp, hourExp, min" +
"uteExp, secondExp, 0);\r\n\r\n            var localDateTime = DateTime(date);\r\n     " +
"       document.getElementById(\"date_\" + \"");

            
            #line 98 "..\..\Views\Alarm\Index.cshtml"
                                          Write(i);

            
            #line default
            #line hidden
WriteLiteral("\").innerHTML = localDateTime;\r\n            var localExpDateTime = DateTime(expDat" +
"e);\r\n            document.getElementById(\"expDate_\" + \"");

            
            #line 100 "..\..\Views\Alarm\Index.cshtml"
                                             Write(i);

            
            #line default
            #line hidden
WriteLiteral("\").innerHTML = localExpDateTime;\r\n    </script>\r\n");

            
            #line 102 "..\..\Views\Alarm\Index.cshtml"
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
