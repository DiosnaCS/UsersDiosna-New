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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/CMS/DeleteArticle.cshtml")]
    public partial class _Views_CMS_DeleteArticle_cshtml : System.Web.Mvc.WebViewPage<UsersDiosna.Models.Article>
    {
        public _Views_CMS_DeleteArticle_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\CMS\DeleteArticle.cshtml"
  
    ViewBag.Title = "DeleteArticle";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>DeleteArticle</h2>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<di" +
"v>\r\n    <h4>Article</h4>\r\n    <hr />\r\n    <dl");

WriteLiteral(" class=\"dl-horizontal\"");

WriteLiteral(">\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 15 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.bakeryId));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 19 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.bakeryId));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 23 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.DateTime));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 27 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.DateTime));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 31 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.Author));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 35 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.Author));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 39 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.Header));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 43 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.Header));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 47 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.Text));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 51 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.Text));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 55 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.Amount));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 59 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.Amount));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 63 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.HoursSpend));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 67 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.HoursSpend));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 71 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.Attachment));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 75 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.Attachment));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 79 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.Description));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 83 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.Description));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n        <dt>\r\n");

WriteLiteral("            ");

            
            #line 87 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayNameFor(model => model.SectionId));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dt>\r\n\r\n        <dd>\r\n");

WriteLiteral("            ");

            
            #line 91 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.DisplayFor(model => model.SectionId));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dd>\r\n\r\n    </dl>\r\n\r\n");

            
            #line 96 "..\..\Views\CMS\DeleteArticle.cshtml"
    
            
            #line default
            #line hidden
            
            #line 96 "..\..\Views\CMS\DeleteArticle.cshtml"
     using (Html.BeginForm()) {
        
            
            #line default
            #line hidden
            
            #line 97 "..\..\Views\CMS\DeleteArticle.cshtml"
   Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
            
            #line 97 "..\..\Views\CMS\DeleteArticle.cshtml"
                                


            
            #line default
            #line hidden
WriteLiteral("        <div");

WriteLiteral(" class=\"form-actions no-color\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Delete\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" /> |\r\n");

WriteLiteral("            ");

            
            #line 101 "..\..\Views\CMS\DeleteArticle.cshtml"
       Write(Html.ActionLink("Back to List", "Index"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n");

            
            #line 103 "..\..\Views\CMS\DeleteArticle.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

        }
    }
}
#pragma warning restore 1591