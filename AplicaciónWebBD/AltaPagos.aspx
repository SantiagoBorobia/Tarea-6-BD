<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AltaPagos.aspx.cs" Inherits="AltaPagos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 403px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color: #009933">
    <div style="background-color: #008000; height: 555px;">
    
        <asp:DropDownList ID="DDLNombres" runat="server" style="z-index: 1; left: 195px; top: 110px; position: absolute" OnSelectedIndexChanged="DDLNombres_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" ForeColor="White" style="z-index: 1; left: 451px; top: 38px; position: absolute" Text="Página para alta de pagos."></asp:Label>
        <asp:Label ID="Label2" runat="server" ForeColor="White" style="z-index: 1; left: 75px; top: 112px; position: absolute" Text="Nombre cliente:"></asp:Label>
        <asp:Label ID="Label3" runat="server" ForeColor="White" style="z-index: 1; left: 75px; top: 156px; position: absolute" Text="Clave pedido:"></asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server" style="z-index: 1; left: 195px; top: 157px; position: absolute" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Table ID="TblPagos" runat="server" style="z-index: 1; left: 484px; top: 125px; position: absolute; height: 25px; width: 468px" GridLines="Both">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Fecha pedido</asp:TableCell>
                <asp:TableCell runat="server">Total pagar</asp:TableCell>
                <asp:TableCell runat="server">Total pagos realizados</asp:TableCell>
                <asp:TableCell runat="server">Saldo a cubrir</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    
    </div>
    </form>
</body>
</html>
