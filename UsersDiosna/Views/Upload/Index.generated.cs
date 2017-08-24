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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Upload/Index.cshtml")]
    public partial class _Views_Upload_Index_cshtml : System.Web.Mvc.WebViewPage<UsersDiosna.Upload.Models.UploadFile>
    {
        public _Views_Upload_Index_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\Upload\Index.cshtml"
  
    ViewBag.Title = "Index";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<h2>Index of Upload System</h2>\r\n");

            
            #line 7 "..\..\Views\Upload\Index.cshtml"
Write(ViewBag.message);

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 8 "..\..\Views\Upload\Index.cshtml"
 using (Html.BeginForm("UploadFile", "Upload", FormMethod.Post, new { @class = "form-horizontal", role = "form", enctype = "multipart/form-data" }))
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 11 "..\..\Views\Upload\Index.cshtml"
   Write(Html.TextBoxFor(m => m.File, new { type = "file" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        <div");

WriteLiteral(" class=\"col-md-10\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 13 "..\..\Views\Upload\Index.cshtml"
       Write(Html.ValidationMessageFor(m => m.File));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 14 "..\..\Views\Upload\Index.cshtml"
       Write(Html.HiddenFor(m => m.plcName, Request.QueryString["plc"]));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n");

WriteLiteral("    <div");

WriteLiteral(" class=\"form-group\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-md-offset-2 col-md-10\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" class=\"btn btn-default\"");

WriteLiteral(" value=\"Upload File\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n");

            
            #line 22 "..\..\Views\Upload\Index.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral(@"<script>
    function show() {

        //var filePath = document.getElementById(""file2"").file[0].fileName;
        var file = document.getElementById(""file2"").value;
        //document.getElementById(""filePathToView2"").innerHTML = filePath;
        document.getElementById(""filePathToView"").innerHTML = file;
}
</script>
<div");

WriteLiteral(" id=\"filePathToView\"");

WriteLiteral(">\r\n</div>\r\n<div");

WriteLiteral(" id=\"filePathToView2\"");

WriteLiteral(">\r\n</div>\r\n<h3>9_Public</h3>\r\n");

            
            #line 37 "..\..\Views\Upload\Index.cshtml"
  
    int i = 0;

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 40 "..\..\Views\Upload\Index.cshtml"
 if (ViewBag.fileList != null)
{
    foreach (string file in ViewBag.fileList)
    {

            
            #line default
            #line hidden
WriteLiteral("            <div");

WriteLiteral(" class=\"list-group-item-text\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 45 "..\..\Views\Upload\Index.cshtml"
           Write(file);

            
            #line default
            #line hidden
WriteLiteral("\r\n                <a");

WriteAttribute("href", Tuple.Create(" href=\"", 1380), Tuple.Create("\"", 1424)
, Tuple.Create(Tuple.Create("", 1387), Tuple.Create("/Download/downloadFile?nameFile=", 1387), true)
            
            #line 46 "..\..\Views\Upload\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1419), Tuple.Create<System.Object, System.Int32>(file
            
            #line default
            #line hidden
, 1419), false)
);

WriteLiteral(">Download</a>\r\n");

            
            #line 47 "..\..\Views\Upload\Index.cshtml"
                
            
            #line default
            #line hidden
            
            #line 47 "..\..\Views\Upload\Index.cshtml"
                 if (file.Contains(".pdf") || file.Contains(".txt"))
                {

            
            #line default
            #line hidden
WriteLiteral("                    <a");

WriteAttribute("href", Tuple.Create(" href=\"", 1551), Tuple.Create("\"", 1602)
, Tuple.Create(Tuple.Create("", 1558), Tuple.Create("/Download/downloadFile?nameFile=", 1558), true)
            
            #line 49 "..\..\Views\Upload\Index.cshtml"
, Tuple.Create(Tuple.Create("", 1590), Tuple.Create<System.Object, System.Int32>(file
            
            #line default
            #line hidden
, 1590), false)
, Tuple.Create(Tuple.Create("", 1595), Tuple.Create("&View=1", 1595), true)
);

WriteLiteral(">View</a>");

WriteLiteral("<br>\r\n");

            
            #line 50 "..\..\Views\Upload\Index.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                    <br>\r\n");

            
            #line 54 "..\..\Views\Upload\Index.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </div>\r\n");

            
            #line 56 "..\..\Views\Upload\Index.cshtml"
        i++;
    }
}
else {

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"has-error\"");

WriteLiteral(">No files has been found</div>\r\n");

            
            #line 61 "..\..\Views\Upload\Index.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
