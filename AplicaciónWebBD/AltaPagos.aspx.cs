using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AltaPagos : System.Web.UI.Page
{

    //Variables de clase.
    GestorBD.GestorBD GestorBD;
    string rfc, cadSql;
    DataSet Dsgeneral = new DataSet(), DsPedidos = new DataSet();
    DataSet DsPag = new DataSet(), DsPagos = new DataSet(), DsAlta = new DataSet();
    DataRow Fila;
    Comunes común = new Comunes();
    const int OK = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Recupera valores de Session.
            GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
            rfc = Session["rfc"].ToString();

            //Lee los datos de los pedidos que ha hecho el empleado
            //y carga los Nombres de los clientes en el DDL.
            cadSql = "select Nombre from PCUsuarios where RFC in (select RFCC from PCPedidos p where p.RFCE='" + rfc + "'";
            GestorBD.consBD(cadSql, DsPedidos, "Clientes");
            común.cargaDDL(DDLNombres, DsPedidos, "Clientes", "Nombre");
            Session["DsPedidos"] = DsPedidos;
        }
    }



    protected void DDLNombres_SelectedIndexChanged(object sender, EventArgs e)
    {
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        //Lee los datos de los pedidos que ha hecho el empleado
        //y carga los Nombres de los clientes en el DDL.
        cadSql = "select FolioP from PCPedidos where RFCC=(select RFC from PCUsuarios where nombre='"+DDLNombres.SelectedValue+"')";
        GestorBD.consBD(cadSql, Dsgeneral, "Folios");
        común.cargaDDL(DropDownList2, Dsgeneral, "Folios", "FolioP");
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        //Muestra los otros elementos
        TblPagos.Visible = true;
        BtModi.Visible = true;
        GVPagos.Visible = true;

        //obtiene la informacion del pedido seleccionado y llena la tabla y el grid view
        cadSql = "select fechaPed, monto, saldocli, saldofacs from PCPedidos where folioP='" + DropDownList2.SelectedValue + "'";
        GestorBD.consBD(cadSql, DsPagos, "InfoPed");
        Fila = DsPagos.Tables["InfoPed"].Rows[0];
        TblPagos.Rows[1].Cells[0].Text = Fila["FechaPed"].ToString();
        TblPagos.Rows[1].Cells[1].Text = Fila["Monto"].ToString();
        TblPagos.Rows[1].Cells[2].Text = ((double)Fila["Monto"] - (double)Fila["SaldoCli"]).ToString();
        TblPagos.Rows[1].Cells[2].Text = Fila["SaldoCli"].ToString();

  
        cadSql = "select IdPago, Fecha, Monto from PCPagos where FolioP = '" + DropDownList2.SelectedValue + "'";
        GestorBD.consBD(cadSql, DsPag, "PagosPed");
        GVPagos.DataSource = DsPag.Tables["PagosPed"];  //Muestra resultados.
        GVPagos.DataBind();

    }

    protected void BtModi_Click(object sender, EventArgs e)
    {
        BtBajaP.Visible = true;
        BtModi.Visible = true;
        BtModP.Visible = true;
    }

    protected void BtBajaP_Click(object sender, EventArgs e)
    {
        //Agarra todos los Id´s de los pagos y los mete a un DDL
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        DDLBaja.Visible = true;
        BtEjecutarBaja.Visible = true;
        cadSql = "Select * from PCPagos where FolioP = '" + DropDownList2.SelectedValue + "'";
        GestorBD.consBD(cadSql, DsPag, "Pagos");
        común.cargaDDL(DDLBaja, DsPag, "Pagos", "IdPago");
    }

    protected void BtEjecutarBaja_Click(object sender, EventArgs e)
    {
        //Da de baja el pago seleccionado
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        cadSql = "delete from PCPagos where IdPago=" + DDLBaja.SelectedValue + "";
        if (GestorBD.bajaBD(cadSql) == OK)
            Response.Write("Eliminación exitosa en Usuarios");
        else
            Response.Write("Error de eliminación del Pago");
    }

    protected void BtAltaPago_Click(object sender, EventArgs e)
    {
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        LbAlta.Visible = true;
        TxBMonto.Visible = true;
        BtEjecutarAlta.Visible = true;
        //Genera un Id para el nuevo pago
        cadSql = "select max(IdPago) into MId from PCPagos";
        GestorBD.consBD(cadSql, DsPag, "MaxId");
        int idtemp;
        Fila = DsPag.Tables["MaxiD"].Rows[0];
        idtemp = (int)Fila["MId"];
        idtemp = idtemp + 1;

        //Crear el pago
        string mon = TxBMonto.Text;
        cadSql = "Insert into PCPagos values ('" + DropDownList2.SelectedValue + "', " + idtemp + ", " + DateTime.Now.ToString() +", "+ mon +")";
        if (GestorBD.altaBD(cadSql) == OK)
            Response.Write("Pago dado de Alta");
        else
            Response.Write("No se pudo dar de alta el pago");

    }
}