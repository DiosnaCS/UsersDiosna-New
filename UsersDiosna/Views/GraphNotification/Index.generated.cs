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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/GraphNotification/Index.cshtml")]
    public partial class _Views_GraphNotification_Index_cshtml : System.Web.Mvc.WebViewPage<IEnumerable<UsersDiosna.Graph.Models.NameDef>>
    {
        public _Views_GraphNotification_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\GraphNotification\Index.cshtml"
  
    ViewBag.Title = "Index";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Index</h2>\r\n\r\n\r\n<table");

WriteLiteral(" class=\"table table-striped table-sm table-bordered table-condensed table-hover\"");

WriteLiteral(">\r\n    <tr>\r\n        <th>\r\n            Select requested tags\r\n        </th>\r\n    " +
"    <th>\r\n");

WriteLiteral("            ");

            
            #line 16 "..\..\Views\GraphNotification\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.column));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </th>\r\n        <th>Condition</th>\r\n        <th>Value</th>\r\n    </tr>\r\n");

            
            #line 21 "..\..\Views\GraphNotification\Index.cshtml"
   int i = 0; 
            
            #line default
            #line hidden
WriteLiteral("\r\n        \r\n");

            
            #line 23 "..\..\Views\GraphNotification\Index.cshtml"
 using (Html.BeginForm("NewNotification", "GraphNotification", new { ReturnUrl = ViewBag.ReturnUrl }, FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
{
    foreach (var item in Model) {

            
            #line default
            #line hidden
WriteLiteral("        <input");

WriteLiteral(" type=\"hidden\"");

WriteAttribute("name", Tuple.Create(" name=\"", 692), Tuple.Create("\"", 707)
, Tuple.Create(Tuple.Create("", 699), Tuple.Create("table_", 699), true)
            
            #line 26 "..\..\Views\GraphNotification\Index.cshtml"
, Tuple.Create(Tuple.Create("", 705), Tuple.Create<System.Object, System.Int32>(i
            
            #line default
            #line hidden
, 705), false)
);

WriteAttribute("value", Tuple.Create(" value=\"", 708), Tuple.Create("\"", 727)
            
            #line 26 "..\..\Views\GraphNotification\Index.cshtml"
, Tuple.Create(Tuple.Create("", 716), Tuple.Create<System.Object, System.Int32>(item.table
            
            #line default
            #line hidden
, 716), false)
);

WriteLiteral(">\r\n");

WriteLiteral("        <tr>\r\n            <td>\r\n                <input");

WriteLiteral(" type=\"checkbox\"");

WriteAttribute("name", Tuple.Create(" name=\"", 801), Tuple.Create("\"", 820)
            
            #line 29 "..\..\Views\GraphNotification\Index.cshtml"
, Tuple.Create(Tuple.Create("", 808), Tuple.Create<System.Object, System.Int32>(item.column
            
            #line default
            #line hidden
, 808), false)
);

WriteLiteral(">\r\n            </td>\r\n            <td>\r\n");

WriteLiteral("                ");

            
            #line 32 "..\..\Views\GraphNotification\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.column));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </td>\r\n            <td>\r\n                <input");

WriteLiteral(" list=\"operators\"");

WriteAttribute("name", Tuple.Create(" name=\"", 997), Tuple.Create("\"", 1015)
, Tuple.Create(Tuple.Create("", 1004), Tuple.Create("operator_", 1004), true)
            
            #line 35 "..\..\Views\GraphNotification\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1013), Tuple.Create<System.Object, System.Int32>(i
            
            #line default
            #line hidden
, 1013), false)
);

WriteLiteral(">\r\n                <datalist");

WriteLiteral(" id=\"operators\"");

WriteLiteral(">\r\n                    <option");

WriteLiteral(" value=\">\"");

WriteLiteral(">More than</option>\r\n                    <option");

WriteLiteral(" value=\">=\"");

WriteLiteral(">Equals and more than</option>\r\n                    <option");

WriteLiteral(" value=\"<=\"");

WriteLiteral(">Equals and less than</option>\r\n                    <option");

WriteLiteral(" value=\"<\"");

WriteLiteral(">Less then</option>\r\n                </datalist>\r\n            </td>\r\n            " +
"<td>    \r\n                <div");

WriteLiteral(" class=\"form-group-sm\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"text\"");

WriteAttribute("name", Tuple.Create(" name=\"", 1471), Tuple.Create("\"", 1485)
, Tuple.Create(Tuple.Create("", 1478), Tuple.Create("text_", 1478), true)
            
            #line 45 "..\..\Views\GraphNotification\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1483), Tuple.Create<System.Object, System.Int32>(i
            
            #line default
            #line hidden
, 1483), false)
);

WriteLiteral(">\r\n                </div>\r\n                \r\n            </td>\r\n        </tr>\r\n");

            
            #line 50 "..\..\Views\GraphNotification\Index.cshtml"
        i++;
    }

            
            #line default
            #line hidden
WriteLiteral("    <p>\r\n            <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(" value=\"Create new notification\"");

WriteLiteral(" />\r\n    </p>\r\n");

            
            #line 55 "..\..\Views\GraphNotification\Index.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("</table>\r\n");

        }
    }
}
#pragma warning restore 1591
