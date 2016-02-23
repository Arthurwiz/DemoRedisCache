<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCachePage.aspx.cs" Inherits="DemoRedisCache.TestCachePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        <br />
        <br />
        Delete Cache<br />
        <asp:Button ID="ButDelete" runat="server" OnClick="ButDelete_Click" Text="Delete Cache" />
    </div>
    </form>
</body>
</html>

