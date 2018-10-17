using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ListaPedidos : System.Web.UI.Page {

  //Variables de clase.
  GestorBD.GestorBD GestorBD;
  string rfc, cadSql;
  DataSet Dsgeneral = new DataSet(), DsPedidos = new DataSet();
  DataSet DsArtículos = new DataSet(), DsPagos = new DataSet();
  DataRow Fila;
  Comunes común = new Comunes();

  //Acciones iniciales.
  protected void Page_Load(object sender, EventArgs e) {

    if (!IsPostBack) {
      //Recupera valores de Session.
      GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
      rfc = Session["rfc"].ToString();

      //Lee los datos del usuario.
      cadSql = "select * from PCUsuarios u, PCClientes c where u.RFC='" + rfc +
        "' and u.RFC=c.RFC";
      GestorBD.consBD(cadSql, Dsgeneral, "Usuario");

      //Despliega los datos en la tabla.
      Fila = Dsgeneral.Tables["Usuario"].Rows[0];
      TblUsuario.Rows[1].Cells[0].Text = rfc;
      TblUsuario.Rows[1].Cells[1].Text = Fila["Nombre"].ToString();
      TblUsuario.Rows[1].Cells[2].Text = Fila["Domicilio"].ToString();

      //Lee los pedidos del cliente y carga los folios en el DDLPedidos.
      cadSql = "select * from PCPedidos where RFCC='" + rfc + "'";
      GestorBD.consBD(cadSql, DsPedidos, "Pedidos");
      común.cargaDDL(DDLPedidos, DsPedidos, "Pedidos", "FolioP");
      Session["DsPedidos"] = DsPedidos;
    }
  }

  //Muestra los datos del pedido, más nombre, cant. pedida y cant. entregada 
  //de los artículos, más los pagos hechos al pedido.
  protected void DDLPedidos_SelectedIndexChanged(object sender, EventArgs e) {
    //DataRow[] filas;

    GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
    //DsPedidos = (DataSet)Session["DsPedidos"];

    //Primera alternativa: consultando de nuevo a la BD (puede ser costoso, aunque con
    //datos actuales).
    rfc = Session["rfc"].ToString();
    cadSql = "select * from PCPedidos where RFCC='" + rfc + "' and FolioP=" + DDLPedidos.Text;
    GestorBD.consBD(cadSql, DsPedidos, "Pedidos");
    Fila = DsPedidos.Tables["Pedidos"].Rows[0];

    //Segunda alternativa: usando la información que ya está en el DataSet (más eficiente,
    //pero puede tener datos desactualizados).
    //filas = (DataRow[])DsPedidos.Tables["Pedidos"].Select("FolioP=" + DdlPedidos.Text);
    //fila = filas[0];

    TblPedido.Rows[1].Cells[0].Text = Fila["FolioP"].ToString();
    TblPedido.Rows[1].Cells[1].Text = Fila["FechaPed"].ToString();
    TblPedido.Rows[1].Cells[2].Text = Fila["Monto"].ToString();
    TblPedido.Rows[1].Cells[3].Text = Fila["SaldoCli"].ToString();
    TblPedido.Rows[1].Cells[4].Text = Fila["SaldoFacs"].ToString();

    //Muestra los artículos que ampara el pedido.
    cadSql = "select Nombre, CantPed, CantEnt from PCArtículos a, PCDetalle d where FolioP=" +
                DDLPedidos.Text + " and d.IdArt=a.IdArt";
    GestorBD.consBD(cadSql, DsArtículos, "Artículos");
    GrdArtículos.DataSource = DsArtículos.Tables["Artículos"];  //Muestra resultados.
    GrdArtículos.DataBind();

    //Muestra los pagos realizados para el pedido seleccionado.
    cadSql = "select * from PCPagos where FolioP=" + DDLPedidos.Text;
    GestorBD.consBD(cadSql, DsPagos, "Pagos");
    GrdPagos.DataSource = DsPagos.Tables["Pagos"];  //Muestra resultados.
    GrdPagos.DataBind();

  }
}











