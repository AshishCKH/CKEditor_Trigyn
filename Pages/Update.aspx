<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Update.aspx.cs"
    Inherits="XMLEditor.Pages.Update" ValidateRequest="true" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../ckeditor/adapters/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var count = 0;
           
                           
            $("#btnJSBack").click(function () {                
                var isTextChanged = '<%=Session["IsTextChanged"] %>';
                var url = '<%=ConfigurationSettings.AppSettings["RediretionURL"].ToString()%>';                

                if ($("#hdnXmlChanged").val() == "true" || isTextChanged == "true") {
                    var retVal = confirm("You made changes to the XML file. Are you sure you want to leave this page?");
                    if (retVal == true) {
                        window.location = url + "/" + "XMLGetAll.aspx";
                        //window.location = "XMLGetAll.aspx";
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    window.location = url + "/" + "XMLGetAll.aspx";
                    //window.location = "XMLGetAll.aspx";
                }
            });

            CKEDITOR.on('instanceCreated', function (e) {
                e.editor.on('change', function (ev) {
                    count++;
                    //By default on page load the onchange event is called twice. So ignore the first two calls.
                    if (count > 2) {
                        $("#hdnXmlChanged").val(true);
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <input type="button" value="Back" id="btnJSBack" />
        <asp:Button ID="btnCheck" runat="server" Text="Preview" OnClick="btnCheck_Click" />&nbsp;
        <input type="hidden" id="hdnXmlChanged" />
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:HiddenField ID="hdnPath" runat="server" />
        <asp:HiddenField ID="hdnFileName" runat="server" />
    </div>
    <div>
        &nbsp;
        <CKEditor:CKEditorControl ID="txtXML" runat="server" height="400" width="900"></CKEditor:CKEditorControl>
    </div>
    <div>
    </div>
</asp:Content>
