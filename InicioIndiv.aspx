<%@ Page Title="Alta Individual" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.master" CodeBehind="InicioIndiv.aspx.vb" Inherits="INTeatroDig.InicioIndiv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="tablaDatos" runat="server" class="container-fluid">
     <div class="row">
        <div class="col-md-2 col-left">
        </div>
        <div class="col-md-8 col-centered page-institucional" style="text-align: center">
             <h2>Alta de Datos Individual</h2>
        </div>
        <div class="col-md-2 text-right">
           <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/index.aspx" CssClass="linksBold">Volver</asp:HyperLink>
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-md-2 col-left">
        </div>
        <div class="col-md-8 col-centered page-institucional" style="text-align: center">
            <h4>Importante</h4>
        </div>
        <div class="col-md-2 text-right">
        </div>
    </div>
    <div class="row">
        <div class="col-md-2 col-left">
        </div>
        <div class="col-md-8 col-centered page-institucional" style="text-align: center">
            <p>
            Al realizar su “Alta de Datos Individual” usted deberá consignar una cuenta de correo electrónico
            “personal” y de chequeo habitual y permanente, ya que en ella recibirá la confirmación del
            procesamiento de todos los trámites que gestione a través de INTeatro Digital, y desde esa misma
            cuenta deberá confirmar sus respectivas validaciones.
            Si su proveedor de correo es <strong>“gmail”</strong> o <strong>"hotmail"</strong>, <strong>ANTES DE REALIZAR ESTE TRÁMITE DE ALTA</strong>, tenga la
            previsión de incorporar a la libreta de contactos (o a la lista segura de remitentes), las direcciones <b>intdigital@inteatro.gob.ar</b> e <b>info.intdigital@inteatro.gob.ar</b> para que ningún mensaje proveniente de esta plataforma sea considerado como “spam” por su proveedor de correo.
            </p>
        </div>
        <div class="col-md-2 text-right">
        </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
          <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-control" Visible="False">
          </asp:DropDownList>
       </div>
       <div class="col-md-4 col-left">
           <asp:Label ID="LabelProvincia" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
          <label>Tipo de Persona:</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
          <asp:DropDownList ID="ddlPersona" runat="server" CssClass="form-control" AutoPostBack="True">
          </asp:DropDownList>
       </div>
       <div class="col-md-4 col-left">
           <asp:Label ID="LabelPersona" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
  </div>
  <div id="DatosSoc" runat="server" visible="false" class="container-fluid">
     <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Entidad/Sociedad: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:DropDownList ID="ddlEntidadSociedad" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
       </div>
       <div class="col-md-4 col-left">
           <asp:Label ID="LabelEntidad" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-2 col-centered" style="text-align: center">
           <label>Tipo: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
       </div>
       <div class="col-md-4 col-left">
           <asp:Label ID="LabelCategoria" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <br />
  </div>
  <div id="DivEnviar" runat="server" class="container-fluid">
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-7 col-centered" style="text-align: center">
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:Button ID="BtnEnviar" runat="server" Text="Continuar" CssClass="btn btn-primary" />
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>      
    <br />
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-10 col-centered" style="text-align: center">
          <asp:Label ID="FailureText" runat="server" CssClass="text-danger"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
  </div>
</asp:Content>
