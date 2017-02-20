<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewPage.aspx.cs"
    Inherits="XMLEditor.Pages.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.loadmask.min.js" type="text/javascript"></script>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">   
   <div id="divResult">
    <div>
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />&nbsp;
        <asp:Button ID="btnTransfer" runat="server" Text="Transfer XML File To Production"
            OnClick="btnTransfer_Click" Width="220px" onclientclick="Loader()" />&nbsp;        
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:HiddenField ID="hdnPath" runat="server" />
        <asp:HiddenField ID="hdnFileName" runat="server" />
    </div>
    <div id="stagingDiv" runat="server" visible="false" class="divStyle">
        <span class="msg">The xml file is saved successfully on staging site. Please click on
            the below link to see the changes</span>
        <br />
        <asp:HyperLink ID="stagingLink" runat="server" Target="_blank" Style="color: #034af3!important">Staging</asp:HyperLink>
    </div>
    <div id="productionDiv" runat="server" visible="false" class="divStyle">
        <span class="msg" id="spnMessage" runat="server"></span><br />
        <span class="errormsg" id="spnErrorMessage" runat="server"></span><br />
        <asp:HyperLink ID="productionLink" runat="server" Target="_blank" Style="color: #034af3!important"></asp:HyperLink>
    </div>     
    </div>
    <script type="text/javascript">
        function Loader() {
            $("form").mask("Loading");
        }

        $(document).ready(function () {
            $("form").unmask();
        });
    </script>
</asp:content>
