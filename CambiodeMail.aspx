<%@ Page Title="Cambio de Mail" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="CambiodeMail.aspx.vb" Inherits="INTeatroDig.CambiodeMail" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <div id="Datos" runat="server" class="container-fluid">
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-8 col-centered page-institucional" style="text-align: center">
              <h2>Cambio de Mail</h2>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/menuFinal.aspx" CssClass="linksBold">Volver</asp:HyperLink>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <br />
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
              <label>Mail actual:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtMailActual" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
          </div>
          <div class="col-md-4 text-right">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
              <label>Nuevo Mail:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="TextBoxNuevoMail" runat="server" CssClass="form-control" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
          </div>
          <div class="col-md-4 text-left">
              <asp:Label ID="Label8" runat="server" style="color: red" Text="[*]"></asp:Label> 
              <label class="observacion">Por favor chequee frecuentemente esta cuenta de correo ya que en ella recibirá información del INT y todas las notificaciones de gestiones que Ud. realice en INTeatroDigital</label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
              <label>Confirmación:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="TextBoxConfirma" runat="server" CssClass="form-control" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
          </div>
          <div class="col-md-4 text-left">
              <asp:Label ID="Label1" runat="server" style="color: red" Text="[*]"></asp:Label> 
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
             <asp:Label ID="FailureText" runat="server" CssClass="TextoError"  Width="324px" ></asp:Label>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
          </div>
          <div class="col-md-4 col-left">
             <asp:Button ID="BtnEnviar" runat="server" Text="Modificar" CssClass="btn btn-success" />
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <br /> 
   </div>
</asp:Content>
