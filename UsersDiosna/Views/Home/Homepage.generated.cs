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
    
    #line 2 "..\..\Views\Home\Homepage.cshtml"
    using System.Web.Mvc;
    
    #line default
    #line hidden
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using UsersDiosna;
    
    #line 1 "..\..\Views\Home\Homepage.cshtml"
    using UsersDiosna.Controllers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Home/Homepage.cshtml")]
    public partial class _Views_Home_Homepage_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Home_Homepage_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<h2>Welcome to Diosna cloud</h2>\r\n<div");

WriteLiteral(" class=\"jumbotron\"");

WriteLiteral(">\r\n");

            
            #line 6 "..\..\Views\Home\Homepage.cshtml"
    
            
            #line default
            #line hidden
            
            #line 6 "..\..\Views\Home\Homepage.cshtml"
      int index = 1;
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 7 "..\..\Views\Home\Homepage.cshtml"
    
            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Home\Homepage.cshtml"
     for (int idx = 1; idx <= (ViewBag.Count / 3); idx++)
    {

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"btn-group btn-group-justified\"");

WriteLiteral(">\r\n");

            
            #line 10 "..\..\Views\Home\Homepage.cshtml"
        
            
            #line default
            #line hidden
            
            #line 10 "..\..\Views\Home\Homepage.cshtml"
         for (int i = 1; i <= 3 && (i*idx) < ViewBag.Count; i++)
        {

            
            #line default
            #line hidden
WriteLiteral("            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 345), Tuple.Create("\"", 384)
, Tuple.Create(Tuple.Create("", 352), Tuple.Create("/Menu?id=", 352), true)
            
            #line 12 "..\..\Views\Home\Homepage.cshtml"
, Tuple.Create(Tuple.Create("", 361), Tuple.Create<System.Object, System.Int32>(ViewBag.Numbers[index]
            
            #line default
            #line hidden
, 361), false)
);

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(">");

            
            #line 12 "..\..\Views\Home\Homepage.cshtml"
                                                                          Write(ViewBag.Text[index]);

            
            #line default
            #line hidden
WriteLiteral("&rAarr;</a>\r\n");

            
            #line 13 "..\..\Views\Home\Homepage.cshtml"
            index++;
        }

            
            #line default
            #line hidden
WriteLiteral("    </div>\r\n");

WriteLiteral("    <p></p>\r\n");

            
            #line 17 "..\..\Views\Home\Homepage.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"btn-group btn-group-justified\"");

WriteLiteral(">\r\n        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 567), Tuple.Create("\"", 602)
, Tuple.Create(Tuple.Create("", 574), Tuple.Create("/Menu?id=", 574), true)
            
            #line 19 "..\..\Views\Home\Homepage.cshtml"
, Tuple.Create(Tuple.Create("", 583), Tuple.Create<System.Object, System.Int32>(ViewBag.Numbers[0]
            
            #line default
            #line hidden
, 583), false)
);

WriteLiteral(" class=\"btn btn-primary\"");

WriteLiteral(">");

            
            #line 19 "..\..\Views\Home\Homepage.cshtml"
                                                                  Write(ViewBag.Text[0]);

            
            #line default
            #line hidden
WriteLiteral("&rAarr;</a>\r\n    </div>\r\n    \r\n</div>");

        }
    }
}
#pragma warning restore 1591
