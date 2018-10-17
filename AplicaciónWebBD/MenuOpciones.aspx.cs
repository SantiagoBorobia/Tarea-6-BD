using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MenuOpciones : System.Web.UI.Page
{
    GestorBD.GestorBD GestorBD;
    string rfc;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Recupera valores de Session.
            GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
            rfc = Session["rfc"].ToString();
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
          //Recupera valores de Session.
            GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
            rfc = Session["rfc"].ToString();
            Server.Transfer("ListaPedidos.aspx");
        
        
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        
            //Recupera valores de Session.
            GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
            rfc = Session["rfc"].ToString();
            Server.Transfer("AdminUsuarios.aspx");

        
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
       
            //Recupera valores de Session.
            GestorBD = (GestorBD.GestorBD)Session["GestorBD"];
            rfc = Session["rfc"].ToString();
            Server.Transfer("AltaPagos.aspx");
        
        
    }
}