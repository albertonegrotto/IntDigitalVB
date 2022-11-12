<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="cambioDeClave.aspx.vb" Inherits="INTeatroDig.cambioDeClave" 
    Culture="es-AR" title="Cambio de Clave"%>

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
              <h2>Cambio de Clave</h2>
          </div>
           <div class="col-md-2 col-centered" style="text-align: left">
              <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/menuFinal.aspx" CssClass="linksBold">Volver</asp:HyperLink>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
              <label>Clave actual:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <asp:TextBox ID="txtClaveActual" runat="server" CssClass="form-control" Width="190px" TextMode="Password"></asp:TextBox>
          </div>
          <div class="col-md-4 text-left">
              <asp:Label ID="Label1" runat="server" style="color: red" Text="[*]"></asp:Label> 
              <asp:RequiredFieldValidator ID="RequiredFieldValidatorClaveActual" runat="server" 
                  ControlToValidate="txtClaveActual" ErrorMessage="Debe ingresar la clave actual" 
                  ValidationGroup="ValidDatos" Font-Names="Arial" Font-Size="13px"></asp:RequiredFieldValidator>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
              <label>Nueva clave:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <asp:TextBox ID="txtNuevaClave" runat="server" CssClass="form-control" Width="190px" MaxLength="8" TextMode="Password" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
          </div>
          <div class="col-md-4 text-left">
              <asp:Label ID="Label2" runat="server" style="color: red" Text="[*]"></asp:Label> 
             <label class="observacion"> (consignar 8 caracteres con por lo menos un número)</label>
          </div>
          <div class="col-md-2 text-left">
              <asp:RequiredFieldValidator ID="RequiredFieldValidatorClaveNueva" runat="server" 
                  ControlToValidate="txtNuevaClave" ErrorMessage="Ingrese nueva clave" 
                  ValidationGroup="ValidDatos" Font-Names="Arial" Font-Size="13px"></asp:RequiredFieldValidator>
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
              <label>Confirmación:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
               <asp:TextBox ID="txtConfirmacionClave" runat="server" CssClass="form-control" Width="190px" MaxLength="8" TextMode="Password" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label3" runat="server" style="color: red" Text="[*]"></asp:Label> 
              <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirmacion" runat="server" 
                  ControlToValidate="txtConfirmacionClave" ErrorMessage="Debe confirmar la nueva clave" 
                  ValidationGroup="ValidDatos" Font-Names="Arial" Font-Size="13px"></asp:RequiredFieldValidator>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
               <label>Pregunta:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="DdlPregunta" runat="server" CssClass="form-control"></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label4" runat="server" style="color: red" Text="[*]"></asp:Label> 
              <label class="observacion">Elija una pregunta de seguridad para la cual le resulte sencillo recordar la respuesta, ya que ese dato se le pedirá cuando a futuro necesite recuperar su contraseña</label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
             <label>Respuesta:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
             <asp:TextBox ID="TextBoxRespuesta" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
          <div class="col-md-4 col-center">              
              <asp:Label ID="Label5" runat="server" style="color: red" Text="[*]"></asp:Label> 
          </div>
          <div class="col-md-1 text-left">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                ControlToValidate="TextBoxRespuesta" ErrorMessage="*"
                ValidationGroup="ValidDatos" Font-Names="Arial" Font-Size="13px">
             </asp:RequiredFieldValidator>
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-2 col-centered" style="text-align: center">
          </div>
          <div class="col-md-5 col-centered" style="text-align: left">
              <asp:CompareValidator ID="CompareValidator1" runat="server" 
                  ErrorMessage="La clave nueva y su confirmación no coinciden" 
                  ControlToCompare="txtNuevaClave" 
                  ControlToValidate="txtConfirmacionClave" Font-Names="Arial" Font-Size="13px"></asp:CompareValidator>
          </div>
          <div class="col-md-2 col-left">
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

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        EnableScriptLocalization="true" 
        EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>  

</asp:Content>
