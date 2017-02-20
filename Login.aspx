<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="XMLEditor.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
<div>
    <span class="maroonhead">*</span><strong> </strong>
    <span style="font-size: 11px; color: black;">denotes required fields</span>            
</div>
<div style="float:left; margin-top:10px; margin-left:15px;">        
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
    <tr>
        <td colspan="2" align="left">
            <asp:ValidationSummary ID="val" runat="server" ValidationGroup="log" />
        </td>
    </tr>
    <tr>
        <td align="right" nowrap="noWrap">
            <span class="maroonhead">* &nbsp;</span><asp:Label ID="lblUserid" runat="server"
                Text="SVN User Id :" Style="position: static" />
        </td>
        <td align="left">
            <asp:TextBox CssClass="login" ID="txtUser" runat="server" Width="180px" Style="position: static"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rv1" runat="server" ControlToValidate="txtUser" Display="None"
                ErrorMessage="Enter SVN User Id" ValidationGroup="log"></asp:RequiredFieldValidator>
        </td>
    </tr>  
    <tr>
        <td align="right" nowrap="nowrap">
            <span class="maroonhead">*&nbsp;</span><asp:Label ID="lblPwd" runat="server"
                Text="SVN Password :" Style="position: static"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtPass" runat="server" CssClass="login" TextMode="Password"
                Width="180px" Style="position: static"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="rv2" runat="server" ControlToValidate="txtPass" Display="None" ErrorMessage="Enter SVN Password"
                ValidationGroup="log"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="dotted_lines" colspan="2" style="padding-left:103px;">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 30px;padding-left:103px;">                                         
            <asp:Button ID="btnSubmit" runat="server" CssClass="ButtonMedium" OnClick="btnSubmit_Click"
                Text="Submit" ValidationGroup="log" />
            <asp:Button ID="btnClear" runat="server" CssClass="ButtonMedium" OnClick="btnClear_Click"
                Text="Clear" />
        </td>
    </tr>
    </table>      
</div>
</asp:content>
