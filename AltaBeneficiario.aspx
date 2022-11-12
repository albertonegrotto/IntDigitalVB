<%@ Page Title="Datos de Alta de Beneficiario de Pagos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="AltaBeneficiario.aspx.vb" 
    Inherits="INTeatroDig.AltaBeneficiario"  MaintainScrollPositionOnPostBack="True" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Datos del Alta de Beneficiario</h2>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/menuFinal.aspx" CssClass="linksBold">Volver</asp:HyperLink>
      </div>
   </div>
   <div id="tablaDatos" runat="server" visible="false" class="container-fluid">
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Código :</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxente" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-4 col-left">
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Denominación :</label>
       </div>
       <div class="col-md-7 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxdenominacion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Actividad :</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxActividad" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-4 col-left">
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Fecha de Alta :</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxAlta" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-4 col-left">
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Inhibido :</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxad_sb" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-4 col-left">
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Inactivo :</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxInactivo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-4 col-left">
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Domicilio :</label>
       </div>
       <div class="col-md-7 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxdomicilio" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
        <div class="col-md-2 col-left">
        </div>
        <div class="col-md-2 col-centered" style="text-align: center">
            <label>Provincia: </label>
        </div>
        <div class="col-md-7 col-centered" style="text-align: left">
            <asp:TextBox ID="TextBoxDesprovi" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Localidad :</label>
       </div>
       <div class="col-md-7 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxlocalidad" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Código Postal :</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxcopost" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-4 col-left">
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Fecha de Baja :</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxBaja" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
       </div>
       <div class="col-md-4 col-left">
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <br />
     <br />
     <br />
     <br />
   </div>

   <div id="tablaDatos2" runat="server" visible="false" class="container-fluid">
     <br/>
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-8 col-centered" style="text-align: center">
           <label>Su CUIL/CUIT no posee “Alta de Beneficiario de Pagos” en el Sistema Integrado de Administración Financiera del Ministerio de Economía y Finanzas Públicas</label>
       </div>
       <div class="col-md-2 text-right">
       </div>
     </div>
     <br />
     <br />
     <br />
     <br />
     <br />
     <br />
     <br />
     <br />
   </div>

</asp:Content>
