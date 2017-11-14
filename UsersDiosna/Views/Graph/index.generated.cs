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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Graph/index.cshtml")]
    public partial class _Views_Graph_index_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
        public _Views_Graph_index_cshtml()
        {
        }
        public override void Execute()
        {
WriteLiteral(" <div");

WriteLiteral(" id=\"graph\"");

WriteLiteral(" onLoad=\"init()\"");

WriteLiteral(" scroll=\"no\"");

WriteLiteral(" style=\"overflow: hidden;\"");

WriteLiteral(">\r\n      <link");

WriteLiteral(" href=\"./css/style_01.css\"");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(" />\r\n      <script");

WriteLiteral(" src=\"./js/graphControler.js\"");

WriteLiteral("></script>\r\n\r\n    <div");

WriteLiteral(" id=\"top_menu\"");

WriteLiteral(">\r\n      <input");

WriteLiteral(" id=\"backShift\"");

WriteLiteral(" style=\"height: 26px; width: 50px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"<--\"");

WriteLiteral(" onClick=\"backShift()\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"fwdShift\"");

WriteLiteral(" style=\"height: 26px; width: 50px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"-->\"");

WriteLiteral(" onClick=\"fwdShift()\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"narrow\"");

WriteLiteral(" style=\"height: 26px; width: 45px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"-><-\"");

WriteLiteral(" onClick=\"narrow()\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"extend\"");

WriteLiteral("style=\"height: 26px; width: 45px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"<->\"");

WriteLiteral(" onClick=\"extend()\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"backDay\"");

WriteLiteral("style=\"height: 26px; width: 50px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"<-day\"");

WriteLiteral(" onClick=\"backDay()\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"fwdDay\"");

WriteLiteral("style=\"height: 26px; width: 50px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"day->\"");

WriteLiteral(" onClick=\"fwdDay()\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"zoom\"");

WriteLiteral(" style=\"height: 26px; width: 50px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"zoom\"");

WriteLiteral(" onClick=\"zoomSignal(value)\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"date\"");

WriteLiteral("style=\"height: 26px; width: 50px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"date\"");

WriteLiteral(" onClick=\"calendar();\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"reset\"");

WriteLiteral(" style=\"height: 26px; width: 50px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\">0<\"");

WriteLiteral(" onClick=\"resetValue(value)\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"refresh\"");

WriteLiteral("style=\"height: 26px; width: 60px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"refresh\"");

WriteLiteral(" onClick=\"refresh()\"");

WriteLiteral("/>\r\n      <label");

WriteLiteral(" for=\"group\"");

WriteLiteral(">group:</label>\r\n      <!--[if IE]>\r\n      <select id=\"group\" style=\"height: 26px" +
"; width: 210px;\" onChange=\"changeGroup(-1)\" tabindex=\"1\" method=\"get\">\r\n      </" +
"select>\r\n      <![endif]-->\r\n      <select");

WriteLiteral(" id=\"group\"");

WriteLiteral(" style=\"height: 26px; width: 210px;\"");

WriteLiteral(" onChange=\"changeGroup(value)\"");

WriteLiteral(" tabindex=\"1\"");

WriteLiteral(" method=\"get\"");

WriteLiteral(">\r\n      </select>\r\n      <input");

WriteLiteral(" id=\"lang\"");

WriteLiteral(" style=\"height: 26px; width: 75px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"lang:EN\"");

WriteLiteral(" onClick=\"changeLang()\"");

WriteLiteral("/>\r\n      <input");

WriteLiteral(" id=\"zone\"");

WriteLiteral(" style=\"text-align: left; height: 26px; width: 120px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"zone:CET\"");

WriteLiteral(" onClick=\"changeZone(value)\"");

WriteLiteral("/>\r\n      <!-- <input id=\"mode\" style=\"height: 26px; width: 50px;\" type=\"button\" " +
"value=\"mode\" onClick=\"chartMode()\"/> -->\r\n      <input");

WriteLiteral(" id=\"settings\"");

WriteLiteral(" style=\"height: 26px; width: 70px;\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"settings\"");

WriteLiteral(" onClick=\"settings(value)\"");

WriteLiteral("/>      \r\n      <input");

WriteLiteral(" id=\"default\"");

WriteLiteral(" style=\"height: 26px; width: 70px; visibility: hidden;\"");

WriteLiteral(" type=button");

WriteLiteral(" value=\"Default\"");

WriteLiteral(" onClick=\"defaultLayout()\"");

WriteLiteral("/>\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"calendar\"");

WriteLiteral(" style=\"position: absolute; top: 105px; left: 350px; background-color: #F0F0F0; z" +
"-index: 0; visibility: hidden\"");

WriteLiteral(">    \r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"settingsWin\"");

WriteLiteral(" style=\"position: absolute; top: 105px; left: 250px; background-color: #F0F0F0; z" +
"-index: 0; visibility: hidden\"");

WriteLiteral(">\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"message\"");

WriteLiteral(" style=\"position: absolute; top: 86px; left: 400px; height: 19px; width: 700px; b" +
"ackground-color: #F0F0F0; z-index: 50;\"");

WriteLiteral(">\r\n      <span");

WriteLiteral(" style=\"text-align: left; font-weight: 700; color:#FF0000; margin-left: 15px;\"");

WriteLiteral("></span>\r\n      <input");

WriteLiteral(" id=\"reload\"");

WriteLiteral(" style=\"float: right; height: 19px; width: 50px; font-size: 9pt\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(" value=\"reload\"");

WriteLiteral(" onClick=\"reload()\"");

WriteLiteral("/>\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"graph_content\"");

WriteLiteral(" style=\"position: absolute;\"");

WriteLiteral(">\r\n      <canvas");

WriteLiteral(" id=\"active_canvas\"");

WriteLiteral(" width=\"1600\"");

WriteLiteral(" height=\"900\"");

WriteLiteral(" style=\"position: absolute; z-index: -1;\"");

WriteLiteral("></canvas>\r\n      <canvas");

WriteLiteral(" id=\"front_canvas\"");

WriteLiteral(" width=\"1600\"");

WriteLiteral(" height=\"900\"");

WriteLiteral(" style=\"position: absolute; z-index: -2;\"");

WriteLiteral("></canvas>\r\n      <canvas");

WriteLiteral(" id=\"signal_canvas\"");

WriteLiteral(" width=\"1600\"");

WriteLiteral(" height=\"900\"");

WriteLiteral(" style=\"position: absolute; z-index: -3;\"");

WriteLiteral("></canvas>\r\n      <canvas");

WriteLiteral(" id=\"back_canvas\"");

WriteLiteral(" width=\"1600\"");

WriteLiteral(" height=\"900\"");

WriteLiteral(" style=\"position: absolute; z-index: -4;\"");

WriteLiteral("></canvas>   \r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"loading\"");

WriteLiteral(" style=\"display: block; width: 102px; height: 19px; position: absolute; top: 110p" +
"x; left: 610px; z-index: 0;\"");

WriteLiteral(">\r\n       <div");

WriteLiteral(" style=\"z-index: 0; width: 102px; height: 19px\"");

WriteLiteral(" id=\"fountainG\"");

WriteLiteral(">\r\n  \t     <div");

WriteLiteral(" id=\"fountainG_1\"");

WriteLiteral(" class=\"fountainG\"");

WriteLiteral("></div>\r\n  \t     <div");

WriteLiteral(" id=\"fountainG_2\"");

WriteLiteral(" class=\"fountainG\"");

WriteLiteral("></div>\r\n  \t     <div");

WriteLiteral(" id=\"fountainG_3\"");

WriteLiteral(" class=\"fountainG\"");

WriteLiteral("></div>\r\n  \t     <div");

WriteLiteral(" id=\"fountainG_4\"");

WriteLiteral(" class=\"fountainG\"");

WriteLiteral("></div>\r\n  \t     <div");

WriteLiteral(" id=\"fountainG_5\"");

WriteLiteral(" class=\"fountainG\"");

WriteLiteral("></div>\r\n  \t     <div");

WriteLiteral(" id=\"fountainG_6\"");

WriteLiteral(" class=\"fountainG\"");

WriteLiteral("></div>\r\n  \t     <div");

WriteLiteral(" id=\"fountainG_7\"");

WriteLiteral(" class=\"fountainG\"");

WriteLiteral("></div>\r\n  \t     <div");

WriteLiteral(" id=\"fountainG_8\"");

WriteLiteral(" class=\"fountainG\"");

WriteLiteral("></div>\r\n       </div>\r\n    </div>\r\n\r\n    <div");

WriteLiteral(" id=\"sources\"");

WriteLiteral(">\r\n     <!-- graphicList -->\r\n     <img");

WriteLiteral(" id=\"status\"");

WriteLiteral(" style=\"display: none;\"");

WriteLiteral(" src=\"/img/status.png\"");

WriteLiteral(" />\r\n     <!-- event icons -->\r\n     <img");

WriteLiteral(" id=\"close\"");

WriteLiteral(" style=\"display: none;\"");

WriteLiteral(" src=\"/img/close.png\"");

WriteLiteral(" />\r\n     <img");

WriteLiteral(" id=\"signal_visible\"");

WriteLiteral(" style=\"display: none;\"");

WriteLiteral(" src=\"/img/signal_visible.png\"");

WriteLiteral(" />\r\n     <img");

WriteLiteral(" id=\"event_point\"");

WriteLiteral(" style=\"display: none;\"");

WriteLiteral(" src=\"/img/event_point.png\"");

WriteLiteral(" />\r\n    </div>\r\n  \r\n    <script");

WriteLiteral(" src=\"/js/jquery-latest.js\"");

WriteLiteral("></script>\r\n    <!--[if lt IE 9]>\r\n      <script src=\"excanvas.js\"></script>\r\n   " +
" <![endif]-->\r\n    <script");

WriteLiteral(" src=\"/lib/pktime.js\"");

WriteLiteral("></script>    \r\n  \r\n  </div>  ");

        }
    }
}
#pragma warning restore 1591
