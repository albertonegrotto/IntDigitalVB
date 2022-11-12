<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ConfirmaIndiv.aspx.vb" Inherits="INTeatroDig.ConfirmaIndiv" 
    title="Correo electrónico enviado" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="Datos" runat="server" class="container-fluid">

    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-8 col-centered" style="text-align: center">
           <h2>Confirmación de Envío de Datos Individuales</h2>
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
           Solicitud Nº
           <asp:Label ID="Label7" runat="server" Text="" CssClass="Texto"></asp:Label>
       </div>
       <div class="col-md-2 text-right">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-8 col-centered" style="text-align: center">
            Recibirá en la cuenta de correo electrónico consignada un mail
            conteniendo la información ingresada, y las debidas instrucciones para finalizar 
            este trámite de &quot;Alta de Datos Individual&quot; y concluir así con la ACTIVACIÓN DE 
            SU CUENTA DE USUARIO.
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
  
  </div> 

</asp:Content>
