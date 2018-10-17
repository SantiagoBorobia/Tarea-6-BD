<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaPedidos.aspx.cs" Inherits="ListaPedidos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 566px;
        }
        .auto-style1 {
            width: 100%;
            z-index: 1;
            left: 11px;
            top: 323px;
            position: absolute;
            height: 28px;
        }
        .auto-style2 {
            position: absolute;
            left: 12px;
            top: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="background-color: #FF9966">
    <div style="background-color: #FFCC66; height: 552px;">
    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" style="z-index: 1; left: 23px; top: 501px; position: absolute">Página principal</asp:HyperLink>
    
        <table class="auto-style1">
            <caption>
                Artículos y pagos del pedido</caption>
            <tr style="z-index: 1">
                <td class="auto-style2">
                    <asp:GridView ID="GrdArtículos" runat="server">
                    </asp:GridView>
                </td>
                <td class="auto-style2">
                    <asp:GridView ID="GrdPagos" runat="server" style="z-index: 1; left: 523px; top: 31px; position: absolute; height: 152px; width: 375px">
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
        <asp:Table ID="TblUsuario" runat="server" BorderStyle="Outset" GridLines="Both" style="z-index: 1; left: 287px; top: 90px; position: absolute; height: 54px; width: 441px">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">RFC</asp:TableCell>
                <asp:TableCell runat="server">Nombre</asp:TableCell>
                <asp:TableCell runat="server">Domicilio</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="TblPedido" runat="server" BorderStyle="Double" GridLines="Both" style="z-index: 1; left: 211px; top: 223px; position: absolute; height: 54px; width: 579px">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">Pedido no.</asp:TableCell>
                <asp:TableCell runat="server">Fecha</asp:TableCell>
                <asp:TableCell runat="server">Total a pagar</asp:TableCell>
                <asp:TableCell runat="server">Saldo del cliente</asp:TableCell>
                <asp:TableCell runat="server">Saldo en facturas</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
                <asp:TableCell runat="server"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 375px; top: 51px; position: absolute; width: 262px" Text="Datos de los pedidos del cliente"></asp:Label>
    
        <asp:Label ID="Label2" runat="server" style="z-index: 1; left: 355px; top: 175px; position: absolute" Text="Pedidos:"></asp:Label>
        <asp:DropDownList ID="DDLPedidos" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLPedidos_SelectedIndexChanged" style="z-index: 1; left: 474px; top: 172px; position: absolute">
        </asp:DropDownList>
    
    </div>
    </form>
</body>
</html>
