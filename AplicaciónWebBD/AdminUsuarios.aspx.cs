using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminUsuarios : System.Web.UI.Page
{
  //Variables generales de la página.
  DataSet DsGeneral= new DataSet(); DataRow fila;
  GestorBD.GestorBD GestorBD;         //Para manejar la BD.
  Comunes comunes= new Comunes();     //Para manejar las rutinas de uso común.
  String cadSql;
  const int OK = 1;

  //Acciones iniciales.
  protected void Page_Load(object sender, EventArgs e) {

  }

  //========================================================================
  //Parte 1a: acciones relacionadas con el alta de usuarios.
  //Muestra/deshabilita los controles asociados al alta.
  protected void BtnAlta_Click(object sender, EventArgs e) {

    TxtRFC.Visible = true; TxtNombre.Visible = true;
    TxtPassw.Visible = true; DDLTipo.Visible = true;
    BtnBaja.Enabled = false; BtnCambio.Enabled = false;
    LblMensaje.Text = "Operación: Alta";
    Session["Operación"] = "Alta";
  }

  //Muestra controles adicionales según el tipo de usuario a dar de alta.
  protected void DDLTipo_SelectedIndexChanged(object sender, EventArgs e) {
    String tipo;

    tipo = DDLTipo.SelectedValue;
    LblMensaje.Text = "Tipo de usuario: " + tipo;
    if (DDLTipo.Text != "Tipo de usuario") {
      if (DDLTipo.SelectedValue == "Cli") {
        TxtDomicilio.Visible = true; TxtCat.Visible = false;
      }
      else {
        TxtDomicilio.Visible = false; TxtCat.Visible = true;
        if (DDLTipo.SelectedValue == "Emp")
          TxtCat.Text = "Base";          //Tipo de empleado.
        else
          TxtCat.Text = "Gerente";
      }
      BtnEjecuta.Enabled = true;
    }
    else {
      TxtDomicilio.Visible = false; TxtCat.Visible = false; BtnEjecuta.Enabled = false;
    }

  }

  //===============================================================
  //Alta de un nuevo usuario:
  //primeramente lo da de alta en la tabla de Usuarios, verificando antes que no exista 
  //el RFC. Después da de alta en las tablas de Clientes o Empleados, según el tipo de
  //usuario de que se trate.
  public void alta() {
    //Parte 1b: da de alta al nuevo usuario.
    GestorBD = (GestorBD.GestorBD)Session["GestorBD"];

    //Verifica que el RFC no exista en la BD.
    cadSql = "select * from PCUsuarios where RFC='" + TxtRFC.Text + "'";
    GestorBD.consBD(cadSql, DsGeneral, "Usuario");
    if (DsGeneral.Tables["Usuario"].Rows.Count == 0) {
      //Si el RFC no existe, entonces inserta en la tabla de Usuarios.
      cadSql = "insert into PCUsuarios values('" + TxtRFC.Text + "','" + TxtNombre.Text +
                "','" + TxtPassw.Text + "','" + DDLTipo.SelectedValue.ToString() + "')";

      if (GestorBD.altaBD(cadSql) == OK) {
        if (DDLTipo.SelectedValue.ToString() == "Cli") {
          //============================================
          //Parte 1c: alta de un cliente.
          cadSql = "insert into PCClientes values('" + TxtRFC.Text +
                    "','" + TxtDomicilio.Text + "')";
          if (GestorBD.altaBD(cadSql) == OK)
            LblMensaje.Text = "Inserción exitosa en Usuarios y Clientes";
          else
            LblMensaje.Text = "Error de inserción en la tabla Clientes";
        }
        else {
          //============================================
          //Parte 1d: alta de un empleado.
          cadSql = "insert into PCEmpleados values('" + TxtRFC.Text + "','" + TxtCat.Text + "')";
          if (GestorBD.altaBD(cadSql) == OK)
            LblMensaje.Text = "Inserción exitosa en Usuarios y Empleados";
          else
            LblMensaje.Text = "Error de inserción en la tabla Empleados";
        }
      }
      else
        LblMensaje.Text = "Error de inserción en la tabla Usuarios";
    }
    else
      LblMensaje.Text = "Error: este RFC ya existe en la BD";
  }

  //=====================================================================
  //Parte 4: acciones relacionadas con la ejecución de la operación elegida.
  protected void BtnEjecuta_Click(object sender, EventArgs e) {
    String oper;

    oper = Session["Operación"].ToString();
    switch (oper) {
      case "Alta":
        alta();
        break;
      case "Baja":
        baja();
        break;
      case "Cambio":
        //cambio();
        break;
    }

    TxtRFC.Visible = false; TxtNombre.Visible = false;
    TxtPassw.Visible = false;
    //TxtPassw.TextMode = TextBoxMode.Password;   //Para que aparezcan asteriscos al dar la contra.
    TxtDomicilio.Visible = false; TxtCat.Visible = false;
    DDLTipo.Text = "Tipo de usuario"; DDLTipo.Visible = false; DDLUsuarios.Visible = false;
    BtnAlta.Enabled = true; BtnBaja.Enabled = true; BtnCambio.Enabled = true;
    BtnEjecuta.Enabled = false; LblMensaje.Text = "En espera";

  }

  protected void TxtRFC_TextChanged(object sender, EventArgs e) {

  }

  //=================================================================
  //Parte 2a: acciones relacionadas con la baja de un usuario.
  protected void BtnBaja_Click(object sender, EventArgs e) {

    //Lee los datos de los usuarios y muestra sus RFC en DDLUsuarios.
    leeUsuarios();

    //Oculta/deshabilita controles.
    DDLUsuarios.Visible = true;
    BtnAlta.Enabled = false; BtnBaja.Enabled = false; BtnCambio.Enabled = false;
    LblMensaje.Text = "Operación: Baja";
    Session["Operación"] = "Baja";
  }

  //Lee los datos de los usuarios y muestra sus RFC en el DDL de usuarios.
  //Esta rutina es usada tanto en baja, como en cambio de datos de usuarios.
  protected void leeUsuarios() {
    GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
    cadSql = "select * from PCUsuarios";
    GestorBD.consBD(cadSql, DsGeneral, "DatosUsuarios");
    comunes.cargaDDL(DDLUsuarios, DsGeneral, "DatosUsuarios", "RFC");
    Session["DsGeneral"] = DsGeneral;
  }

  //Parte 2b: da de baja al usuario elegido en DDLUsuarios.
  public void baja() {
    string rfc;

    //Da de baja en Usuarios. Por el trigger se da de baja automáticamente en Clientes o Empleados.
    rfc = DDLUsuarios.SelectedValue;
    GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
    cadSql = "delete from PCUsuarios where RFC='" + rfc + "'";
    if (GestorBD.bajaBD(cadSql) == OK)
      Response.Write("Eliminación exitosa en Usuarios");
    else
      LblMensaje.Text = "Error de eliminación en la tabla Usuarios";
  }

  //En la baja, este método habilita el botón de ejecutar operación.
  protected void DDLUsuarios_SelectedIndexChanged(object sender, EventArgs e) {

    if (DDLUsuarios.Text != " ")
      BtnEjecuta.Enabled = true;
    else
      BtnEjecuta.Enabled = false;
  }
}