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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Manage/ChangeTheme.cshtml")]
    public partial class _Views_Manage_ChangeTheme_cshtml : System.Web.Mvc.WebViewPage<UsersDiosna.Models.ChangeThemeModel>
    {
        public _Views_Manage_ChangeTheme_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Manage\ChangeTheme.cshtml"
  
    ViewBag.Title = "ChangeTheme";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>");

            
            #line 6 "..\..\Views\Manage\ChangeTheme.cshtml"
Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n");

            
            #line 7 "..\..\Views\Manage\ChangeTheme.cshtml"
 using (Html.BeginForm("ChangeTheme", "Manage", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"message\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 10 "..\..\Views\Manage\ChangeTheme.cshtml"
   Write(ViewBag.message);

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n");

WriteLiteral("    <hr />\r\n");

WriteLiteral("    <div");

WriteLiteral(" class=\"col\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 14 "..\..\Views\Manage\ChangeTheme.cshtml"
   Write(Html.LabelFor(m => m.Theme, new { @class = "col-md-2 control-label" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"col-md-offset-2 col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 16 "..\..\Views\Manage\ChangeTheme.cshtml"
       Write(Html.ListBoxFor(m => m.Theme, Model.ThemesList, new { @class = "col-md-2" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-md-offset-2 col-md-10\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Change Theme\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n");

            
            #line 22 "..\..\Views\Manage\ChangeTheme.cshtml"
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
