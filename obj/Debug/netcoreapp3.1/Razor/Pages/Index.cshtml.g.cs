#pragma checksum "/Users/sahilbhanderi/Documents/Senior Year/CMPSC 483/myWebApp/Pages/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2a38321cd51612f1ddb5dbc1260acbe159c2c108"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(HousePointsApp.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace HousePointsApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/sahilbhanderi/Documents/Senior Year/CMPSC 483/myWebApp/Pages/_ViewImports.cshtml"
using HousePointsApp;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2a38321cd51612f1ddb5dbc1260acbe159c2c108", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cca0c942a96c73cf96f970c167bf05d28d2fffc0", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "/Users/sahilbhanderi/Documents/Senior Year/CMPSC 483/myWebApp/Pages/Index.cshtml"
  
    ViewData["Title"] = "House Points";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""text-center"">
    <h1 class=""display-4"">Welcome</h1>
    <p>Learn about <a href=""https://docs.microsoft.com/aspnet/core"">building Web apps with ASP.NET Core</a>.</p>
</div>

<!-- Navigation -->
<nav class=""navbar navbar-expand-lg navbar-dark bg-dark fixed-top"">
    <div class=""container"">
        <a class=""navbar-brand"" href=""#"">House Points</a>
        <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target=""#navbarResponsive"" aria-controls=""navbarResponsive"" aria-expanded=""false"" aria-label=""Toggle navigation"">
            <span class=""navbar-toggler-icon""></span>
        </button>
        <div class=""collapse navbar-collapse"" id=""navbarResponsive"">
            <ul class=""navbar-nav ml-auto"">
                <li class=""nav-item active"">
                    <a class=""nav-link"" href=""#"">
                        Home
                        <span class=""sr-only"">(current)</span>
                    </a>
                </li>
                <li class=""n");
            WriteLiteral(@"av-item"">
                    <a class=""nav-link"" href=""#"">About</a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" href=""#"">Services</a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link"" href=""#"">Contact</a>
                </li>
            </ul>
        </div>
    </div>
</nav>

<!-- Header - set the background image for the header in the line below -->
<header class=""py-5 bg-image-full"" style=""background-image: url('https://unsplash.it/1900/1080?image=1076');"">
    <img class=""img-fluid d-block mx-auto"" src=""http://placehold.it/200x200&text=Logo""");
            BeginWriteAttribute("alt", " alt=\"", 1773, "\"", 1779, 0);
            EndWriteAttribute();
            WriteLiteral(@">
</header>

<!-- Content section -->
<section class=""py-5"">
    <div class=""container"">
        <h1>Section Heading</h1>
        <p class=""lead"">Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>
        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aliquid, suscipit, rerum quos facilis repellat architecto commodi officia atque nemo facere eum non illo voluptatem quae delectus odit vel itaque amet.</p>
    </div>
</section>

<!-- Image Section - set the background image for the header in the line below -->
<section class=""py-5 bg-image-full"" style=""background-image: url('https://unsplash.it/1900/1080?image=1081');"">
    <!-- Put anything you want here! There is just a spacer below for demo purposes! -->
    <div style=""height: 200px;""></div>
</section>

<!-- Content section -->
<section class=""py-5"">
    <div class=""container"">
        <h1>Section Heading</h1>
        <p class=""lead"">Lorem ipsum dolor sit amet, consectetur adipisicing elit.</p>
        <p>Lore");
            WriteLiteral(@"m ipsum dolor sit amet, consectetur adipisicing elit. Aliquid, suscipit, rerum quos facilis repellat architecto commodi officia atque nemo facere eum non illo voluptatem quae delectus odit vel itaque amet.</p>
    </div>
</section>

<!-- Footer -->
<footer class=""py-5 bg-dark"">
    <div class=""container"">
        <p class=""m-0 text-center text-white"">Copyright &copy; Your Website 2019</p>
    </div>
    <!-- /.container -->
</footer>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591