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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Admin/RemoveRole.cshtml")]
    public partial class _Views_Admin_RemoveRole_cshtml : System.Web.Mvc.WebViewPage<UsersDiosna.Admin.Models.AdminRemoveRoleModel>
    {
        public _Views_Admin_RemoveRole_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Admin\RemoveRole.cshtml"
  
    ViewBag.Title = "RemoveRole";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Remove Role</h2>\r\nExstining roles:<br>\r\n");

            
            #line 8 "..\..\Views\Admin\RemoveRole.cshtml"
 foreach (String role in ViewBag.Roles) {
    
            
            #line default
            #line hidden
            
            #line 9 "..\..\Views\Admin\RemoveRole.cshtml"
Write(role);

            
            #line default
            #line hidden
WriteLiteral("<br>\r\n");

            
            #line 10 "..\..\Views\Admin\RemoveRole.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"message\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 13 "..\..\Views\Admin\RemoveRole.cshtml"
Write(ViewBag.message);

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n");

            
            #line 15 "..\..\Views\Admin\RemoveRole.cshtml"
 using (Html.BeginForm("RemoveRoleForm", "Admin", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 18 "..\..\Views\Admin\RemoveRole.cshtml"
   Write(Html.LabelFor(m => m.Role, new { @class = "col-md-2 control-label" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 20 "..\..\Views\Admin\RemoveRole.cshtml"
       Write(Html.TextBoxFor(m => m.Role, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-offset-2 col-md-10\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Send\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n");

            
            #line 28 "..\..\Views\Admin\RemoveRole.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
