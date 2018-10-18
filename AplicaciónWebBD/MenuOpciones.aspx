<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuOpciones.aspx.cs" Inherits="MenuOpciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="background-color: #0099FF">
    <div style="background-color: #0066CC; height: 470px;">
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="z-index: 1; left: 279px; top: 137px; position: absolute" Text="Lista Pedidos" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" style="z-index: 1; top: 137px; position: absolute; left: 538px; margin-bottom: 4px" Text="Admin Usuario" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" style="z-index: 1; top: 138px; position: absolute; left: 763px" Text="Alta Pagos" />
        <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" style="z-index: 1; left: 450px; top: 44px; position: absolute" Text="Selecciona una opcion:"></asp:Label>
    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" style="z-index: 1; left: 288px; top: 265px; position: absolute">Menu Principal</asp:HyperLink>
    
    </div>
    </form>
</body>
</html>
