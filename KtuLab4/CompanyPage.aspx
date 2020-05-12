<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyPage.aspx.cs" Inherits="KtuLab4.CompanyPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>L4</title>
    <link href="../Content/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="SiteName" runat="server" Text="OOP L4"></asp:Label><br />
            <asp:Label ID="BranchSelectorLabel" runat="server" Text="Select which company files to use: "></asp:Label>
            <asp:DropDownList ID="BranchDDL" runat="server">
            </asp:DropDownList><br />
            <asp:Button ID="ExecButton" runat="server" Text="Run" OnClick="ExecButton_Click" /><br />
            <asp:Panel ID="DataPanel" runat="server"></asp:Panel>
            <asp:Panel ID="ResultPanel" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
