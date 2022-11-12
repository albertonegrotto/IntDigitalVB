<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="confirmaRegistro.aspx.vb" Inherits="INTeatroDig.confirmaRegistro" 
    title="Correo electrónico enviado" 
    Culture="es-AR"
    Debug="true"
%>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="Datos" runat="server" class="container-fluid">
      <div class="row row-centered">
         <div class="col-md-2 col-left">
         </div>
         <div class="col-md-8 col-centered" style="text-align: center">
             <asp:Label ID="lblTitulo" runat="server" Text="Label" CssClass="TextoTitulo" Font-Size="15px"></asp:Label>
         </div>
         <div class="col-md-2 text-right">
         </div>
      </div>
      <br />
      <br />
      <br />
      <br />
      <div class="row row-centered">
         <div class="col-md-2 col-left">
         </div>
         <div class="col-md-8 col-centered" style="text-align: center">
             <asp:Label runat="server" Text="Envío:" CssClass="Texto" Font-Size="15px"></asp:Label>&nbsp;
             <asp:Label ID="lblResultado" runat="server" Text="" CssClass="Texto" Font-Size="15px"></asp:Label>
         </div>
         <div class="col-md-2 text-right">
         </div>
      </div>
      <br />
      <br />
      <div class="row row-centered">
         <div class="col-md-2 col-left">
         </div>
         <div class="col-md-8 col-centered" style="text-align: center">
             <asp:Label ID="lblMensaje" runat="server" CssClass="Texto" Font-Size="15px"></asp:Label>
         </div>
         <div class="col-md-2 text-right">
         </div>
      </div>
      <br />
      <br />
      <div class="row row-centered">
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
