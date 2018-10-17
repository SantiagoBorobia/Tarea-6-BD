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
    DataSet DsArtículos = new DataSet(), DsPagos = new DataSet();
    DataRow Fila;
    Comunes común = new Comunes();

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
       
        //Lee los datos de los pedidos que ha hecho el empleado
        //y carga los Nombres de los clientes en el DDL.
        cadSql = "select FolioP from PCPedidos where RFCC=(select RFC from PCUsuarios where nombre='"+DDLNombres.SelectedValue+"')";
        GestorBD.consBD(cadSql, Dsgeneral, "Folios");
        común.cargaDDL(DropDownList2, Dsgeneral, "Folios", "FolioP");
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        cadSql = "select fechaPed, monto, saldocli, saldofacs from PCPedidos where folioP='" + DropDownList2.SelectedValue + "'";
        GestorBD.consBD(cadSql, DsPagos, "InfoPed");

    }
}