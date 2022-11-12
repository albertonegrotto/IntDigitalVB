﻿<%@ Page Title="Confirma Recuperación de Contraseña" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ConfirmRecup.aspx.vb" Inherits="INTeatroDig.ConfirmRecup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="tablaDatos" runat="server" class="container-fluid">

   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Confirmación de Envío de Recupero de Contraseña</h2>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/Index.aspx" CssClass="linksBold">Volver</asp:HyperLink>
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
          <asp:Label ID="Label31" runat="server" Text="La palabra clave ingresada es correcta, recibirá en la cuenta de correo electrónico consignada un mail conteniendo su contraseña." CssClass="TextoComentario"></asp:Label><br />
      </div>
      <div class="col-md-2 col-centered" style="text-align: left">
      </div>
   </div>
   <br />
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
  </div>
</asp:Content>
