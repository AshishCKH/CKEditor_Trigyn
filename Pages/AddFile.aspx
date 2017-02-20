<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddFile.aspx.cs"
   Inherits="XMLEditor.Pages.AddFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div>
      <table width="600px">
         <tr>
            <td align="left">
               Add Detail
            </td>           
         </tr>
         <tr>
            <td colspan="2">
               &nbsp;
            </td>
         </tr>
         <tr>
            <td>
               Enter Path :
            </td>
            <td>
               <asp:TextBox ID="txtPath" runat="server" Width="350px"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td>
               Enter File Name:
            </td>
            <td>
               <asp:TextBox ID="txtName" runat="server" Width="350px"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td>
               Enter Page Name:
            </td>
            <td>
               <asp:TextBox ID="txtPage" runat="server" Width="350px"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td>
               Enter Cached File Name:
            </td>
            <td>
               <asp:TextBox ID="txtCachedName" runat="server" Width="350px"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td colspan="2">
               <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;
               <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
               <asp:Label ID="lblMSG" runat="server" Text="" ForeColor="Red"></asp:Label>
               <asp:HiddenField ID="hdnID" runat="server" />
            </td>
         </tr>
      </table>
   </div>
</asp:Content>
