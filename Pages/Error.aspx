<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs"
    Inherits="XMLEditor.Pages.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    <div>
        <span id="lblErrmsg" runat="server" class="ErrorPageMessage" style="font-size: Small;
            font-weight: normal; color: Red">An error has occured. We are sorry for the inconvenience.</span>
            <br />
            <asp:Label runat="server" ID="lblErrorDetails" />
    </div>
</asp:content>
