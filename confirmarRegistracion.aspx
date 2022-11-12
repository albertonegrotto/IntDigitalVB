<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="confirmarRegistracion.aspx.vb" Inherits="INTeatroDig.confirmarRegistracion" 
    title="Confirmación de registro" Culture="es-AR"%>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 <div id="Datos" runat="server" class="container-fluid">

    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-8 col-centered" style="text-align: center">
           <asp:Label ID="lblTitulo" runat="server" Text="Label" Font-Names="Times New Roman" Font-Size="X-Large"></asp:Label>           
      </div>
       </div>
       <div class="col-md-2 text-right">
       </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-8 col-centered" style="text-align: center">
           <asp:Label ID="lblRegistro" runat="server" CssClass="observacion"></asp:Label>
       </div>
       <div class="col-md-2 text-right">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-8 col-centered" style="text-align: center">
           <asp:Label ID="lblResultado" runat="server" CssClass="observacion"></asp:Label>
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
            <asp:Button ID="BtnEnviar" runat="server" Text="Finalizar" CssClass="btn btn-primary" />
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <br />
 </div>
</asp:Content>
