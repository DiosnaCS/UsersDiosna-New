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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Alarm/_Content.cshtml")]
    public partial class _Views_Alarm__Content_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Alarm__Content_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 1 "..\..\Views\Alarm\_Content.cshtml"
   
    if (ViewBag.NumberOfRecords == null)
    {
        ViewBag.NumberOfRecords = 20;
    }

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"bottom\"");

WriteLiteral(">\r\n    <table");

WriteLiteral(" class=\"tg\"");

WriteLiteral(">\r\n        <tr>\r\n            <th");

WriteLiteral(" class=\"tg-yw4l\"");

WriteLiteral(">Id</th>\r\n            <th");

WriteLiteral(" class=\"tg-yw42\"");

WriteLiteral(">Alarm Label</th>\r\n            <th");

WriteLiteral(" class=\"tg-yw43\"");

WriteLiteral(">Origin Time</th>\r\n            <th");

WriteLiteral(" class=\"tg-yw44\"");

WriteLiteral(">ExpiryTime</th>\r\n        </tr>\r\n\r\n\r\n");

            
            #line 17 "..\..\Views\Alarm\_Content.cshtml"
        
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\Alarm\_Content.cshtml"
         for (int i = 0; i < ViewBag.NumberOfRecords; i++)
        {

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td");

WriteLiteral(" class=\"tg-yw4l\"");

WriteLiteral(">");

            
            #line 20 "..\..\Views\Alarm\_Content.cshtml"
                               Write(ViewBag.Id[i]);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td");

WriteLiteral(" class=\"tg-yw42\"");

WriteLiteral(">");

            
            #line 21 "..\..\Views\Alarm\_Content.cshtml"
                               Write(ViewBag.Label[i]);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td");

WriteLiteral(" class=\"tg-yw43\"");

WriteAttribute("id", Tuple.Create(" id=\"", 613), Tuple.Create("\"", 625)
, Tuple.Create(Tuple.Create("", 618), Tuple.Create("date_", 618), true)
            
            #line 22 "..\..\Views\Alarm\_Content.cshtml"
, Tuple.Create(Tuple.Create("", 623), Tuple.Create<System.Object, System.Int32>(i
            
            #line default
            #line hidden
, 623), false)
);

WriteLiteral(">");

            
            #line 22 "..\..\Views\Alarm\_Content.cshtml"
                                            Write(ViewBag.originTime[i]);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td");

WriteLiteral(" class=\"tg-yw44\"");

WriteAttribute("id", Tuple.Create(" id=\"", 691), Tuple.Create("\"", 706)
, Tuple.Create(Tuple.Create("", 696), Tuple.Create("expDate_", 696), true)
            
            #line 23 "..\..\Views\Alarm\_Content.cshtml"
, Tuple.Create(Tuple.Create("", 704), Tuple.Create<System.Object, System.Int32>(i
            
            #line default
            #line hidden
, 704), false)
);

WriteLiteral(">");

            
            #line 23 "..\..\Views\Alarm\_Content.cshtml"
                                               Write(ViewBag.expTime[i]);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");

            
            #line 25 "..\..\Views\Alarm\_Content.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral(@"    </table>
    <script>
        function DateTime(date){
                var d = new Date(date);
                var offset = d.getTimezoneOffset();
                var offseted_miliseconds = d.getTime() - (offset*60000);
                var DateTime = new Date(offseted_miliseconds);
                var localDateTime = DateTime.toLocaleString();
                console.log(date);
                console.log(localDateTime);
                return localDateTime;
            }
    </script>
");

            
            #line 39 "..\..\Views\Alarm\_Content.cshtml"
    
            
            #line default
            #line hidden
            
            #line 39 "..\..\Views\Alarm\_Content.cshtml"
     for (int i = 0; i < ViewBag.NumberOfRecords; i++)
    {

            
            #line default
            #line hidden
WriteLiteral("        <script>\r\n            var date = \"");

            
            #line 42 "..\..\Views\Alarm\_Content.cshtml"
                   Write(ViewBag.originTime[i]);

            
            #line default
            #line hidden
WriteLiteral("\";\r\n            var expDate = \"");

            
            #line 43 "..\..\Views\Alarm\_Content.cshtml"
                      Write(ViewBag.expTime[i]);

            
            #line default
            #line hidden
WriteLiteral("\";\r\n\r\n            var localDateTime = DateTime(date);\r\n            document.getEl" +
"ementById(\"date_\" + \"");

            
            #line 46 "..\..\Views\Alarm\_Content.cshtml"
                                          Write(i);

            
            #line default
            #line hidden
WriteLiteral("\").innerHTML = localDateTime;\r\n            var localExpDateTime = DateTime(expDat" +
"e);\r\n            document.getElementById(\"expDate_\" + \"");

            
            #line 48 "..\..\Views\Alarm\_Content.cshtml"
                                             Write(i);

            
            #line default
            #line hidden
WriteLiteral("\").innerHTML = localExpDateTime;\r\n        </script>\r\n");

            
            #line 50 "..\..\Views\Alarm\_Content.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591