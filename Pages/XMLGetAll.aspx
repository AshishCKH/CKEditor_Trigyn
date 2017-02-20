<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="XMLGetAll.aspx.cs"
    Inherits="XMLEditor.Pages.XMLGetAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">

        function ShowConfirmBox(xmlFile) {           
            var retVal = confirm("Are you sure you want to revert the changes in \"" + xmlFile + "\" file?");
            if (retVal == true) {
                return true;
            }
            else {
                event.preventDefault;
                return false;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="margtop01">
        <table width="100%">
            <tr>
                <td align="left">
                    <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click">Add</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <div class="margtop01">
        <asp:Label ID="lblNoofRecords" runat="server" Text=""></asp:Label>
        <asp:GridView ID="gvXML" runat="server" AutoGenerateColumns="False" SkinID="Main"
            OnRowCommand="gvXML_RowCommand" EmptyDataText="No Records Found." OnRowDataBound="gvXML_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Update XML" SortExpression="ID" HeaderStyle-CssClass="gvMainHead"
                    ItemStyle-Width="125px" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="View" Text='Update XML Data'
                            CssClass="padLeft3" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="XMLPath" HeaderStyle-CssClass="gvMainHead" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="350px" ItemStyle-Height="25px">
                    <ItemTemplate>
                        <span class="padLeft3">
                            <%# Eval("XMLPath") != null ? Server.HtmlEncode(Eval("XMLPath").ToString()) : Eval("XMLPath") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FileName" HeaderStyle-CssClass="gvMainHead" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="350px">
                    <ItemTemplate>
                        <span class="padLeft3">
                            <%# Eval("FileName") != null ? Server.HtmlEncode(Eval("FileName").ToString()) : Eval("FileName") %></span>
                        <asp:Image ImageUrl="~/Images/warning.png" ToolTip="The file has been modified."
                            runat="server" ID="imgWarning" ImageAlign="Middle" />
                        <asp:LinkButton ID="lnkRevert" runat="server" Text="Revert" CommandArgument='<%# Eval("XMLPath")+"\\"+Eval("FileName") %>'
                            OnClick="lnkRevert_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit Location" SortExpression="ID" HeaderStyle-CssClass="gvMainHead"
                    ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" Text='Edit' CommandArgument='<%# Eval("ID") %>'
                            CssClass="padLeft3"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
