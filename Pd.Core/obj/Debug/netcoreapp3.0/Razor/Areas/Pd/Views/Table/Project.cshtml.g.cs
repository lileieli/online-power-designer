#pragma checksum "E:\项目\个人\在线表设计\Pd.Core.Solution\Pd.Core\Areas\Pd\Views\Table\Project.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ec4bfccc8464ee803481ed817b1cae2e11251b7f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Pd_Views_Table_Project), @"mvc.1.0.view", @"/Areas/Pd/Views/Table/Project.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Areas/Pd/Views/Table/Project.cshtml", typeof(AspNetCore.Areas_Pd_Views_Table_Project))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec4bfccc8464ee803481ed817b1cae2e11251b7f", @"/Areas/Pd/Views/Table/Project.cshtml")]
    public class Areas_Pd_Views_Table_Project : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "E:\项目\个人\在线表设计\Pd.Core.Solution\Pd.Core\Areas\Pd\Views\Table\Project.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(56, 1029, true);
            WriteLiteral(@"<div class=""modal-header"">

    <button type=""button""id=""close"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>

    <h4 class=""modal-title"">新增项目</h4>

</div>
<div class=""modal-body"">
    <div class=""form-group"">
        <label for=""name"">项目名称</label>
        <input type=""text"" class=""form-control"" id=""name"" placeholder=""请输入项目名称"">
    </div>
    <button id=""submit"" class=""btn btn-block"">提交</button>
</div>
<script>
    $(document).ready(function () {

        $(document).on(""click"", ""#submit"", function () {
           
            $.ajax({
                type: ""post"",
                url: ""/Pd/Table/AddProject"",
                data: { ""name"": $(""#name"").val() },
                success: function (data) {
                    if (data.success == 1) {
                        alert(""保存成功!"");
                        //$(""#close"").click();//关闭模态框
                    }
                }
            })

        })
    })

</scri");
            WriteLiteral("pt>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
