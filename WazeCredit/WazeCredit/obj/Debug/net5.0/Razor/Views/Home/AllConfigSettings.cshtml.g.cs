#pragma checksum "C:\Users\shafe\Projects\WazeCredit\WazeCredit\Views\Home\AllConfigSettings.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "bab418b8cb683602ba346a7541cd9f521ed41b62a269047e1db0d2acffa5bc95"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_AllConfigSettings), @"mvc.1.0.view", @"/Views/Home/AllConfigSettings.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\shafe\Projects\WazeCredit\WazeCredit\Views\_ViewImports.cshtml"
using WazeCredit;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\shafe\Projects\WazeCredit\WazeCredit\Views\_ViewImports.cshtml"
using WazeCredit.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"bab418b8cb683602ba346a7541cd9f521ed41b62a269047e1db0d2acffa5bc95", @"/Views/Home/AllConfigSettings.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"f5fec276bfd8516b69629c414c04442ae6ff0ac98ebfb76ea4e497a5ad2d9c02", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_AllConfigSettings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<string>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\shafe\Projects\WazeCredit\WazeCredit\Views\Home\AllConfigSettings.cshtml"
  
    ViewData["Title"] = "Config Settings";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"p-4\">\r\n    <h3>The following are configurations from AppSettings:</h3>\r\n    <ul>\r\n");
#nullable restore
#line 10 "C:\Users\shafe\Projects\WazeCredit\WazeCredit\Views\Home\AllConfigSettings.cshtml"
         foreach(var message in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <li>");
#nullable restore
#line 12 "C:\Users\shafe\Projects\WazeCredit\WazeCredit\Views\Home\AllConfigSettings.cshtml"
           Write(message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n");
#nullable restore
#line 13 "C:\Users\shafe\Projects\WazeCredit\WazeCredit\Views\Home\AllConfigSettings.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </ul>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<string>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
