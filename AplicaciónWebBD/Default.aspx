<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Aplicación Web - BD</title>
</head>
<body style="height: 553px">
    <form id="form1" runat="server" style="background-color: #FFCC99; height: 546px;">
    <div style="height: 528px; background-color: #FFCC66;">
    
        <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate" style="z-index: 1; left: 338px; top: 80px; position: absolute; height: 147px; width: 359px">
        </asp:Login>
    
        <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 1014px; top: 32px; position: absolute" Text="Label"></asp:Label>
    
    </div>
    </form>
</body>
</html>
