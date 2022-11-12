<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.master" CodeBehind="AltaIni.aspx.vb" Inherits="INTeatroDig.AltaIni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div id="mensaje" runat="server" class="container-fluid">
        <br />
        <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <img src="images/textoHomepage.gif" width="663px" height="349" alt="" />
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <asp:HyperLink ID="HyperLinkReglamentacion" runat="server" 
                  NavigateUrl="http://inteatro.gob.ar/intdigital/reglamentacion_registro.htm" CssClass="links" Target="_blank">
                 Ver Reglamentación del Registro Nacional del Teatro Independiente
              </asp:HyperLink>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
    </div>  

</asp:Content>
