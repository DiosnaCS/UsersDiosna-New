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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Menu/_Menu.cshtml")]
    public partial class _Views_Menu__Menu_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Menu__Menu_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"sidenav a\"");

WriteLiteral(">\r\n");

            
            #line 2 "..\..\Views\Menu\_Menu.cshtml"
  
    ViewBag.Names = Session["names"];
    ViewBag.plc = Session["plc"];
    ViewBag.id = Session["id"];
    ViewBag.types = Session["types"];
    ViewBag.ProjectName = Session["ProjectName"];
    int i = 0;
    int j = 0;

            
            #line default
            #line hidden
WriteLiteral("\r\n<a");

WriteLiteral(" href=\"/Home/Homepage\"");

WriteLiteral(" class=\"projectName\"");

WriteLiteral(" style=\"color:white;\"");

WriteLiteral(">\r\n");

            
            #line 12 "..\..\Views\Menu\_Menu.cshtml"
    
            
            #line default
            #line hidden
            
            #line 12 "..\..\Views\Menu\_Menu.cshtml"
     if (ViewBag.ProjectName != null) {
        
            
            #line default
            #line hidden
            
            #line 13 "..\..\Views\Menu\_Menu.cshtml"
   Write(ViewBag.ProjectName.ToString());

            
            #line default
            #line hidden
            
            #line 13 "..\..\Views\Menu\_Menu.cshtml"
                                       
    }
            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 15 "..\..\Views\Menu\_Menu.cshtml"
 foreach (String type in ViewBag.types)
{
    if (type.Contains("plc"))    {

            
            #line default
            #line hidden
WriteLiteral("        <a");

WriteLiteral(" class=\"plc\"");

WriteLiteral(">");

            
            #line 18 "..\..\Views\Menu\_Menu.cshtml"
                  Write(ViewBag.plc[i]);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 19 "..\..\Views\Menu\_Menu.cshtml"
        i++;
    } else {
        if (i == 0)
        {

            
            #line default
            #line hidden
WriteLiteral("            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 620), Tuple.Create("\"", 677)
, Tuple.Create(Tuple.Create("", 627), Tuple.Create("/", 627), true)
            
            #line 23 "..\..\Views\Menu\_Menu.cshtml"
, Tuple.Create(Tuple.Create("", 628), Tuple.Create<System.Object, System.Int32>(type
            
            #line default
            #line hidden
, 628), false)
, Tuple.Create(Tuple.Create("", 633), Tuple.Create("?&name=", 633), true)
            
            #line 23 "..\..\Views\Menu\_Menu.cshtml"
, Tuple.Create(Tuple.Create("", 640), Tuple.Create<System.Object, System.Int32>(ViewBag.names[j]
            
            #line default
            #line hidden
, 640), false)
, Tuple.Create(Tuple.Create("", 657), Tuple.Create("&plc=", 657), true)
            
            #line 23 "..\..\Views\Menu\_Menu.cshtml"
, Tuple.Create(Tuple.Create("", 662), Tuple.Create<System.Object, System.Int32>(ViewBag.plc[i]
            
            #line default
            #line hidden
, 662), false)
);

WriteLiteral(">");

            
            #line 23 "..\..\Views\Menu\_Menu.cshtml"
                                                                    Write(ViewBag.names[j]);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 24 "..\..\Views\Menu\_Menu.cshtml"
        }
        else
        {
            i--;

            
            #line default
            #line hidden
WriteLiteral("            <a");

WriteAttribute("href", Tuple.Create(" href=\"", 770), Tuple.Create("\"", 826)
, Tuple.Create(Tuple.Create("", 777), Tuple.Create("/", 777), true)
            
            #line 28 "..\..\Views\Menu\_Menu.cshtml"
, Tuple.Create(Tuple.Create("", 778), Tuple.Create<System.Object, System.Int32>(type
            
            #line default
            #line hidden
, 778), false)
, Tuple.Create(Tuple.Create("", 783), Tuple.Create("?name=", 783), true)
            
            #line 28 "..\..\Views\Menu\_Menu.cshtml"
, Tuple.Create(Tuple.Create("", 789), Tuple.Create<System.Object, System.Int32>(ViewBag.names[j]
            
            #line default
            #line hidden
, 789), false)
, Tuple.Create(Tuple.Create("", 806), Tuple.Create("&plc=", 806), true)
            
            #line 28 "..\..\Views\Menu\_Menu.cshtml"
, Tuple.Create(Tuple.Create("", 811), Tuple.Create<System.Object, System.Int32>(ViewBag.plc[i]
            
            #line default
            #line hidden
, 811), false)
);

WriteLiteral(">");

            
            #line 28 "..\..\Views\Menu\_Menu.cshtml"
                                                                   Write(ViewBag.names[j]);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 29 "..\..\Views\Menu\_Menu.cshtml"
            i++;
        }
        j++;
    }
}

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591