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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Notification/Index.cshtml")]
    public partial class _Views_Notification_Index_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<UsersDiosna.Controllers.Notification>>
    {
        public _Views_Notification_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Notification\Index.cshtml"
  
    ViewBag.Title = "Index";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h4>Notifications center</h4>\r\n<div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n    <p");

WriteLiteral(" class=\"col-md-4 col-md-offset-2\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"btn btn-default\"");

WriteAttribute("href", Tuple.Create(" href=\"", 225), Tuple.Create("\"", 320)
, Tuple.Create(Tuple.Create("", 232), Tuple.Create("/AlarmNotification/All?name=", 232), true)
            
            #line 10 "..\..\Views\Notification\Index.cshtml"
, Tuple.Create(Tuple.Create("", 260), Tuple.Create<System.Object, System.Int32>(Request.QueryString["name"]
            
            #line default
            #line hidden
, 260), false)
, Tuple.Create(Tuple.Create("", 288), Tuple.Create("&plc=", 288), true)
            
            #line 10 "..\..\Views\Notification\Index.cshtml"
                       , Tuple.Create(Tuple.Create("", 293), Tuple.Create<System.Object, System.Int32>(Request.QueryString["plc"]
            
            #line default
            #line hidden
, 293), false)
);

WriteLiteral(">Notification alarms</a>\r\n    </p><p");

WriteLiteral(" class=\"col-md-4 col-md-offset-2\"");

WriteLiteral(">\r\n        <a");

WriteLiteral(" class=\"btn btn-default\"");

WriteAttribute("href", Tuple.Create(" href=\"", 427), Tuple.Create("\"", 522)
, Tuple.Create(Tuple.Create("", 434), Tuple.Create("/GraphNotification/All?name=", 434), true)
            
            #line 12 "..\..\Views\Notification\Index.cshtml"
, Tuple.Create(Tuple.Create("", 462), Tuple.Create<System.Object, System.Int32>(Request.QueryString["name"]
            
            #line default
            #line hidden
, 462), false)
, Tuple.Create(Tuple.Create("", 490), Tuple.Create("&plc=", 490), true)
            
            #line 12 "..\..\Views\Notification\Index.cshtml"
                       , Tuple.Create(Tuple.Create("", 495), Tuple.Create<System.Object, System.Int32>(Request.QueryString["plc"]
            
            #line default
            #line hidden
, 495), false)
);

WriteLiteral(">Notification PLC tags</a>\r\n    </p>\r\n</div>\r\n<table");

WriteLiteral(" class=\"table col-md-1\"");

WriteLiteral(">\r\n    <tr>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 18 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.ProjectName));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 21 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.BakeryID));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 24 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Type));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 27 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Definition));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 30 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.TimestampCreated));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 33 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Active));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 36 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Detail));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 39 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Occurred));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>\r\n");

WriteLiteral("            ");

            
            #line 42 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.Status));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>Actions</th>\r\n    </tr>\r\n\r\n");

            
            #line 47 "..\..\Views\Notification\Index.cshtml"
 foreach (var item in Model) {

            
            #line default
            #line hidden
WriteLiteral("    <tr>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 50 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.ProjectName));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 53 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.BakeryID));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 56 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.Type));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 59 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.Definition));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 62 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.TimestampCreated));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 65 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.Active));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 68 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.Detail));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 71 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.Occurred));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 74 "..\..\Views\Notification\Index.cshtml"
       Write(Html.DisplayFor(modelItem => item.Status));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n        <td>\r\n");

WriteLiteral("            ");

            
            #line 77 "..\..\Views\Notification\Index.cshtml"
       Write(Html.ActionLink("Edit", "Edit", new { id=item.Id }));

            
            #line default
            #line hidden
WriteLiteral(" |\r\n");

WriteLiteral("            ");

            
            #line 78 "..\..\Views\Notification\Index.cshtml"
       Write(Html.ActionLink("Delete", "Delete", new { id=item.Id }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </td>\r\n    </tr>\r\n");

            
            #line 81 "..\..\Views\Notification\Index.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n</table>\r\n");

        }
    }
}
#pragma warning restore 1591