using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {

  //Variables de clase.
  GestorBD.GestorBD GestorBD;
  DataSet DsGeneral = new DataSet();
    DataRow Fila;
  string cadSql;

  //Acciones iniciales.
  protected void Page_Load(object sender, EventArgs e) {
    Label1.Text = DateTime.Now.ToString();

    //La propiedad, de la página, IsPostBack da False la 1a. vez que se carga la págiona.
    //Después siempre da True.
    if (!IsPostBack) {
      GestorBD = new GestorBD.GestorBD("MSDAORA", "bdalumno", "estudia", "oracle");
      Session["GestorBD"] = GestorBD;
    }
  }

  //Verifica que el usuario exista.
  protected void Login1_Authenticate(object sender, AuthenticateEventArgs e) {
    string tipo;
    GestorBD = (GestorBD.GestorBD)Session["GestorBD"];  //Recupera la conexión a la BD.
    cadSql = "select * from PCUsuarios where RFC='" + Login1.UserName +
      "' and Passw='" + Login1.Password + "'";
    GestorBD.consBD(cadSql, DsGeneral, "Usuario");
    //Verifica si el usuario existe en la BD.
    if (DsGeneral.Tables["Usuario"].Rows.Count != 0) {
      Session["rfc"] = Login1.UserName;     //Si existe, guarda el RFC del usuario.
      Fila = DsGeneral.Tables["Usuario"].Rows[0];
      tipo = Fila["Tipo"].ToString();
      if (tipo == "Emp")
          Server.Transfer("MenuOpciones.aspx");
      else
          Server.Transfer("ListaPedidos.aspx");
    }
  }
}