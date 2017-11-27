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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Admin/AddUser.cshtml")]
    public partial class _Views_Admin_AddUser_cshtml : System.Web.Mvc.WebViewPage<UsersDiosna.Admin.Models.AdminAddUserModel>
    {
        public _Views_Admin_AddUser_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Admin\AddUser.cshtml"
  
    ViewBag.Title = "AddUser";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Add user to role</h2>\r\n");

            
            #line 7 "..\..\Views\Admin\AddUser.cshtml"
Write(Html.Partial("_Search"));

            
            #line default
            #line hidden
WriteLiteral("\r\n<div");

WriteLiteral(" class=\"message\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 9 "..\..\Views\Admin\AddUser.cshtml"
Write(ViewBag.message);

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>\r\n");

            
            #line 11 "..\..\Views\Admin\AddUser.cshtml"
 using (Html.BeginForm("AddUserToRole", "Admin", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 14 "..\..\Views\Admin\AddUser.cshtml"
   Write(Html.LabelFor(m => m.User, new { @class = "col-md-2 control-label" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 16 "..\..\Views\Admin\AddUser.cshtml"
       Write(Html.TextBoxFor(m => m.User, new { @class = "form-control" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 20 "..\..\Views\Admin\AddUser.cshtml"
   Write(Html.LabelFor(m => m.Role, new { @class = "col-md-2 control-label" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 22 "..\..\Views\Admin\AddUser.cshtml"
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

WriteLiteral(" class=\"btn btn-success\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n");

            
            #line 30 "..\..\Views\Admin\AddUser.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("Existining users:<br>\r\n<ul");

WriteLiteral(" id=\"myUL\"");

WriteLiteral(">\r\n");

            
            #line 33 "..\..\Views\Admin\AddUser.cshtml"
    
            
            #line default
            #line hidden
            
            #line 33 "..\..\Views\Admin\AddUser.cshtml"
     foreach (var rolesForUser in Model.RolesForUsers)
    {

            
            #line default
            #line hidden
WriteLiteral("        <li>\r\n            <b><a");

WriteLiteral(" class=\"header\"");

WriteLiteral(">");

            
            #line 36 "..\..\Views\Admin\AddUser.cshtml"
                            Write(rolesForUser.user);

            
            #line default
            #line hidden
WriteLiteral("</a></b>\r\n");

            
            #line 37 "..\..\Views\Admin\AddUser.cshtml"
            
            
            #line default
            #line hidden
            
            #line 37 "..\..\Views\Admin\AddUser.cshtml"
             foreach (var user in rolesForUser.Roles)
                {

            
            #line default
            #line hidden
WriteLiteral("                <a");

WriteLiteral(" class=\"ul-item\"");

WriteLiteral(">");

            
            #line 39 "..\..\Views\Admin\AddUser.cshtml"
                              Write(user);

            
            #line default
            #line hidden
WriteLiteral("</a>\r\n");

            
            #line 40 "..\..\Views\Admin\AddUser.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </li>\r\n");

            
            #line 42 "..\..\Views\Admin\AddUser.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</ul>");

        }
    }
}
#pragma warning restore 1591
