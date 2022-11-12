<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="confirmaMail.aspx.vb" Inherits="INTeatroDig.confirmaMail" 
    title="Correo electrónico enviado" Culture="es-AR"%>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 <div id="tablaDatos" runat="server" class="container-fluid">

   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Confirmación de Envío de Correo Electrónico</h2>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/Index.aspx" CssClass="linksBold">Volver</asp:HyperLink>
      </div>
   </div>
   <br />
   <br />
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
          <asp:Label ID="lblResultado" runat="server" Text="Se ha recibido correctamente su consulta y se procederá a responderla a la brevedad." CssClass="observacion"></asp:Label>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <br />
   <br />
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
          Gracias por contactarse con INTeatro Digital.
          Importante: para evitar que los correos electrónicos que se le envíen desde esta plataforma sean considerados como “spam” o “correo no deseado”, se le sugiere que incorpore a la libreta de direcciones (o a la lista segura de remitentes) de su cuenta de correo consignada, las direcciones <b>intdigital@inteatro.gob.ar</b> e <b>info.intdigital@inteatro.gob.ar</b><br />
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <br />
   <br /> 
   <br />
   <div class="row">
     <div class="col-md-2 col-left">
     </div>
     <div class="col-md-8 col-centered" style="text-align: center">
         <asp:Button ID="BtnEnviar" runat="server" Text="Finalizar" CssClass="btn btn-primary" />
     </div>
     <div class="col-md-2 text-right">
     </div>
   </div>     

  </div>
</asp:Content>
