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
            Label3.Text = DateTime.Today.ToString();
            //Recupera valores de Session.
            GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
            rfc = Session["rfc"].ToString();

      //Lee los datos de los pedidos que ha hecho el empleado
      //y carga los Nombres de los clientes en el DDL.
            cadSql = "select Nombre from PCUsuarios where RFC in (select RFCC from PCPedidos p where p.RFCE='" + rfc + "')";
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
        cadSql = "select FolioP from PCPedidos where RFCC=(select RFC from PCUsuarios where nombre='"+DDLNombres.Text+"')";
        Response.Write("Cadena:" + DDLNombres.Text);
        GestorBD.consBD(cadSql, Dsgeneral, "Folios");
        común.cargaDDL(DropDownList2, Dsgeneral, "Folios", "FolioP");

    //Muestra/Oculta controles
    LbClave.Visible = true;
    DropDownList2.Visible = true;
    BtAltaPago.Visible = false;
    BtBajaP.Visible = false;
    BtEjecutarAlta.Visible = false;
    BtEjecutarBaja.Visible = false;
    BtModi.Visible = false;
    BtModP.Visible = false;
    BtnRegistrarModi.Visible = false;
    Label4.Visible = false;
    DDLBaja.Visible = false;
    DDLModi.Visible = false;
    TxBModi.Visible = false;
    TxBMonto.Visible = false;
    GVPagos.Visible = false;
    TblPagos.Visible = false;
    

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pagsRealizados;

        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        //Muestra los otros elementos
        TblPagos.Visible = true;
        BtModi.Visible = true;
        GVPagos.Visible = true;

        //obtiene la informacion del pedido seleccionado y llena la tabla y el grid view
        cadSql = "select fechaPed, monto, saldocli, saldofacs from PCPedidos where folioP=" + DropDownList2.Text + "";
        GestorBD.consBD(cadSql, DsPagos, "InfoPed");
        Fila = DsPagos.Tables["InfoPed"].Rows[0];
        Label3.Text= Fila["Monto"].ToString();

        TblPagos.Rows[1].Cells[0].Text = Fila["FechaPed"].ToString();
        TblPagos.Rows[1].Cells[1].Text = Fila["Monto"].ToString();
        TblPagos.Rows[1].Cells[2].Text = "";
        TblPagos.Rows[1].Cells[3].Text = Fila["SaldoCli"].ToString();

  
        cadSql = "select distinct IdPago, Fecha, Monto from PCPagos where FolioP = " + DropDownList2.Text ;
        GestorBD.consBD(cadSql, DsPag, "PagosPed");
        GVPagos.DataSource = DsPag.Tables["PagosPed"];  //Muestra resultados.
        GVPagos.DataBind();

    }

    protected void BtModi_Click(object sender, EventArgs e)
    {
        BtBajaP.Visible = true;
        BtAltaPago.Visible = true;
        BtModP.Visible = true;
    }

    protected void BtBajaP_Click(object sender, EventArgs e)
    {
        
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];

        //Muestra/Oculta controles
        DDLBaja.Visible = true;
        BtEjecutarBaja.Visible = true;
        LbAlta.Visible = false;
        TxBMonto.Visible = false;
        BtEjecutarAlta.Visible = false;
        DDLModi.Visible = false;
        TxBModi.Visible = false;
        BtnRegistrarModi.Visible = false;
        Label4.Visible = true;

        //Agarra todos los Id´s de los pagos y los mete a un DDL
        cadSql = "Select * from PCPagos where FolioP = " + DropDownList2.SelectedValue + "";
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

        //Oculta los controles
        DDLBaja.Visible = false;
        BtEjecutarBaja.Visible = false;
        Label4.Visible = false;

        //Actualiza los datos de la Tabla y el DataGrid:
        cadSql = "select fechaPed, monto, saldocli, saldofacs from PCPedidos where folioP=" + DropDownList2.Text + "";
        GestorBD.consBD(cadSql, DsPagos, "InfoPed");
        Fila = DsPagos.Tables["InfoPed"].Rows[0];
        Label3.Text = Fila["Monto"].ToString();

        TblPagos.Rows[1].Cells[0].Text = Fila["FechaPed"].ToString();
        TblPagos.Rows[1].Cells[1].Text = Fila["Monto"].ToString();
        TblPagos.Rows[1].Cells[2].Text = "";
        TblPagos.Rows[1].Cells[3].Text = Fila["SaldoCli"].ToString();


        cadSql = "select IdPago, Fecha, Monto from PCPagos where FolioP = '" + DropDownList2.Text + "'";
        GestorBD.consBD(cadSql, DsPag, "PagosPed");
        GVPagos.DataSource = DsPag.Tables["PagosPed"];  //Muestra resultados.
        GVPagos.DataBind();
    }

    protected void BtAltaPago_Click(object sender, EventArgs e)
    {
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        //Muestra/Oculta controles 
        LbAlta.Visible = true;
        TxBMonto.Visible = true;
        BtEjecutarAlta.Visible = true;
        DDLBaja.Visible = false;
        BtEjecutarBaja.Visible = false;
        DDLModi.Visible = false;
        TxBModi.Visible = false;
        BtnRegistrarModi.Visible = false;
        Label4.Visible = false;

  }

  protected void BtEjecutarAlta_Click(object sender, EventArgs e)
    {
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
        //Genera un Id para el nuevo pago
        cadSql = "select max(IdPago) from PCPagos where folioP="+DropDownList2.Text;
        GestorBD.consBD(cadSql, DsPag, "MaxId");
        int idtemp;
        Fila = DsPag.Tables["MaxId"].Rows[0];
        idtemp =  int.Parse(Fila["max(IdPago)"].ToString());
        idtemp = idtemp + 1;

        //Crea el pago y lo inserta
        string mon = TxBMonto.Text;
        cadSql = "Insert into PCPagos values (" + DropDownList2.Text + ", " + idtemp + ", '" + DateTime.Today.ToString() + "', " + mon + ")";
        if (GestorBD.altaBD(cadSql) == OK)
          Response.Write("Pago dado de Alta");
        else
          Response.Write("No se pudo dar de alta el pago");

        //Oculta los controles
        LbAlta.Visible = false;
        TxBMonto.Visible = false;
        BtEjecutarAlta.Visible = false;

        //Actualiza datos en la tabla y el dataGrid:
        cadSql = "select fechaPed, monto, saldocli, saldofacs from PCPedidos where folioP=" + DropDownList2.Text + "";
        GestorBD.consBD(cadSql, DsPagos, "InfoPed");
        Fila = DsPagos.Tables["InfoPed"].Rows[0];
        Label3.Text = Fila["Monto"].ToString();

        TblPagos.Rows[1].Cells[0].Text = Fila["FechaPed"].ToString();
        TblPagos.Rows[1].Cells[1].Text = Fila["Monto"].ToString();
        TblPagos.Rows[1].Cells[2].Text = "";
        TblPagos.Rows[1].Cells[3].Text = Fila["SaldoCli"].ToString();


        cadSql = "select IdPago, Fecha, Monto from PCPagos where FolioP = '" + DropDownList2.Text + "'";
        GestorBD.consBD(cadSql, DsPag, "PagosPed");
        GVPagos.DataSource = DsPag.Tables["PagosPed"];  //Muestra resultados.
        GVPagos.DataBind();

    }

  protected void BtModP_Click(object sender, EventArgs e)
    {
        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];

        //Muestra/Oculta controles
        DDLBaja.Visible = true;
        DDLModi.Visible = true;
        TxBModi.Visible = true;
        BtnRegistrarModi.Visible = true;
        BtEjecutarBaja.Visible = false;
        LbAlta.Visible = false;
        TxBMonto.Visible = false;
        BtEjecutarAlta.Visible = false;
        BtEjecutarBaja.Visible = false;
        Label4.Visible = true;

        //Llena el DDL con los id de los pagos
        cadSql = "Select * from PCPagos where FolioP = " + DropDownList2.Text + "";
        GestorBD.consBD(cadSql, DsPag, "Pagos");
        común.cargaDDL(DDLBaja, DsPag, "Pagos", "IdPago");

        
  }



    protected void BtnRegistrarModi_Click(object sender, EventArgs e)
    {
        //Realiza la modificacion

        GestorBD = (GestorBD.GestorBD)Session["GestorBD"];

        cadSql = "Update PCPagos set '" + DDLModi.Text + "' = '" + TxBModi.Text + "' where IdPago = " + DDLBaja.Text + "";
        if (GestorBD.cambiaBD(cadSql) == OK)
            Response.Write("Se ha realizado la modificación");
        else
            Response.Write("No se pudo realizar la modificación");
        
    }
}