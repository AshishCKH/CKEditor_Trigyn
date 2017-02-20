<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Confirm.aspx.cs"
    Inherits="XMLEditor.Pages.Confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/styles.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="~/Styles/font-awesome.css" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(
            function () {
                var active = true;
                $('#collapse-init').click(function () {
                    if (active) {
                        active = false;
                        $('.panel-collapse').collapse('show');
                        $('.panel-title').attr('data-toggle', '');
                        $(this).text('Collapse All');
                    }
                    else {
                        active = true;
                        $('.panel-collapse').collapse('hide');
                        $('.panel-title').attr('data-toggle', 'collapse');
                        $(this).text('Expand All');
                    }
                    return false;
                });
                $('#accordion').on('show.bs.collapse', function () {
                    if (active) $('#accordion .in').collapse('hide');
                });
            });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />&nbsp;
        <asp:Button ID="btnConfirm" runat="server" Text="Save XML In File" OnClick="btnConfirm_Click" />&nbsp;                
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:HiddenField ID="hdnPath" runat="server" />
        <asp:HiddenField ID="hdnFileName" runat="server" />
    </div>
    <div id="divXmlContent" runat="server" style="width: 730px; border: 1px solid grey;
        padding-left: 2px; padding-right: 2px; margin-top: 20px; ">
    </div>
    <script type="text/javascript">
        $(function () {
            $('a[class=]:not(".ctl00_NavigationMenu_1")').click(function (e) {
                e.preventDefault();
            });
        });
    </script>
</asp:Content>
