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
        <asp:Table ID="TblPagos" runat="server" style="z-index: 1; left: 484px; top: 125px; position: absolute; height: 25px; width: 468px" GridLines="Both" Visible="False">
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
    
        <asp:GridView ID="GVPagos" runat="server" style="z-index: 1; left: 487px; top: 218px; position: absolute; height: 133px; width: 474px" Visible="False">
        </asp:GridView>
        <asp:Button ID="BtModi" runat="server" OnClick="BtModi_Click" style="z-index: 1; left: 76px; top: 220px; position: absolute" Text="Modificaciones" />
        <asp:DropDownList ID="DDLBaja" runat="server" style="z-index: 1; left: 71px; top: 329px; position: absolute; margin-bottom: 0px" Visible="False">
        </asp:DropDownList>
        <asp:Button ID="BtBajaP" runat="server" OnClick="BtBajaP_Click" style="z-index: 1; left: 74px; top: 270px; position: absolute; width: 90px" Text="Baja Pago" Visible="False" />
        <asp:Button ID="BtEjecutarBaja" runat="server" OnClick="BtEjecutarBaja_Click" style="z-index: 1; left: 193px; top: 327px; position: absolute; width: 87px" Text="Dar de Baja" Visible="False" />
        <asp:Button ID="BtAltaPago" runat="server" OnClick="BtAltaPago_Click" style="z-index: 1; left: 192px; top: 271px; position: absolute" Text="Alta Pago" Visible="False" />
        <asp:Button ID="BtModP" runat="server" style="z-index: 1; left: 304px; top: 273px; position: absolute; width: 97px" Text="Modificar Pago" Visible="False" OnClick="BtModP_Click" />
        <asp:Label ID="LbAlta" runat="server" style="z-index: 1; left: 74px; top: 338px; position: absolute" Text="Monto" Visible="False"></asp:Label>
        <asp:TextBox ID="TxBMonto" runat="server" style="z-index: 1; left: 183px; top: 335px; position: absolute" Visible="False"></asp:TextBox>
        <asp:DropDownList ID="DDLModi" runat="server" style="z-index: 1; left: 184px; top: 326px; position: absolute" Visible="False">
            <asp:ListItem>Monto</asp:ListItem>
            <asp:ListItem>Fecha</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="TxBModi" runat="server" style="z-index: 1; left: 269px; top: 327px; position: absolute" Visible="False"></asp:TextBox>
        <asp:Button ID="BtnRegistrarModi" runat="server" style="z-index: 1; left: 284px; top: 378px; position: absolute" Text="Realizar Modificación" Visible="False" />
        <asp:Button ID="BtEjecutarAlta" runat="server" style="z-index: 1; left: 287px; top: 379px; position: absolute" Text="Ejecutar Alta" Visible="False" OnClick="BtEjecutarAlta_Click" />
    
    </div>
    </form>
</body>
</html>
