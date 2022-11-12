<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="RecupContra.aspx.vb" Inherits="INTeatroDig.RecupContra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="Datos" runat="server" class="container-fluid">
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
             <h3 align="center">Recuperación de contraseña</h3>                
          </div>
          <div class="col-md-2 col-left">
             <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/index.aspx" CssClass="linksBold">Volver</asp:HyperLink>
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nº de CUIT / CUIL:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="Username" runat="server" Width="150px" CssClass="form-control pull-left" Enabled="false"></asp:TextBox>&nbsp;
             <%-- <asp:Button ID="btnVerificar" runat="server" Text="Verificar" CssClass="btn btn-success" />--%>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
               <label>Pregunta:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="DdlPregunta" runat="server" CssClass="form-control"></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
             <label>Respuesta:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
             <asp:TextBox ID="TextBoxRespuesta" runat="server" CssClass="form-control"></asp:TextBox>
          </div>
          <div class="col-md-4 col-center">
              <label class="observacion">Para recuperar su contraseña, por favor ingrese la respuesta a esta pregunta, que usted consignó al momento de realizar su alta individual</label>
          </div>
          <div class="col-md-1 text-left">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                ControlToValidate="TextBoxRespuesta" ErrorMessage="*"
                ValidationGroup="ValidDatos" Font-Names="Arial" Font-Size="13px">
             </asp:RequiredFieldValidator>
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>MAIL CONSIGNADO EN SU ALTA INDIVIDUAL</label>
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
              <asp:Label id="LabelEmail" runat="server" CssClass="form-control" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
          </div>
          <div class="col-md-3 col-left">
              <asp:Button ID="BtnMailActual" runat="server" Text="Este no es mi mail actual" CssClass="btn btn-warning" />
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <asp:Label ID="FailureText" runat="server" CssClass="text-danger" Width="324px"></asp:Label>
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
              <asp:Button ID="BtnEnviar" runat="server" Text="Enviar" CssClass="btn btn-primary" />
          </div>
          <div class="col-md-4 col-left">
              <asp:Button ID="BtnNorecuerdo" runat="server" Text="No recuerdo la respuesta" CssClass="btn btn-warning" />
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div id="tablaDatos" runat="server" visible="false" class="container-fluid">
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center">
                <asp:Label runat="server" Text="Lamentablemente no ha podido validar su identidad como usuario de INTeatroDigital." CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center">
                <asp:Label runat="server" Text="Por favor contáctese con el sector REGISTRO del INT para solicitar el reseteo de su contraseña." CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center">
                <asp:Label runat="server" Text="Puede hacerlo enviando un mail a " CssClass="observacion"></asp:Label>
                <a href="mailto:info.intdigital@inteatro.gob.ar" class="links" style="font-size: small; font-weight: bold;">info.intdigital@inteatro.gob.ar</a>
                <asp:Label runat="server" Text=" (adjuntando copia de su DNI)" CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center">
                <asp:Label runat="server" Text=" indicando a qué mail desea que le llegue el reseteo de clave" CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center">
                <asp:Label runat="server" Text="o escribiendo en el " CssClass="observacion"></asp:Label>
                <asp:Label runat="server" Text=" FORMULARIO DE CONTACTO " CssClass="observacion"></asp:Label>
                <asp:Label runat="server" Text=" que figura en la barra superior de esta Plataforma." CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
       </div>
       <div id="TablaDatos2" runat="server" visible="false" class="container-fluid">
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center">
                <asp:Label runat="server" Text="Si éste no es su mail actual, por favor contáctese con el sector REGISTRO del INT" CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center">
                <asp:Label runat="server" Text="para solicitar el cambio de su cuenta de correo electrónico registrada en INTeatroDigital." CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center;">
                <asp:Label runat="server" Text="Puede hacerlo enviando un mail a " CssClass="observacion"></asp:Label>
                <a href="mailto:info.intdigital@inteatro.gob.ar" class="links" style="font-size: small; font-weight: bold;">info.intdigital@inteatro.gob.ar</a>
                <asp:Label runat="server" Text=" (adjuntando copia de su DNI) " CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-2 col-left">
            </div>
            <div class="col-md-8 col-centered" style="text-align: center">
                <asp:Label runat="server" Text="o escribiendo en el " CssClass="observacion"></asp:Label>
                <asp:Label runat="server" Text=" FORMULARIO DE CONTACTO " CssClass="observacion"></asp:Label>
                <asp:Label runat="server" Text=" que figura en la barra superior de esta Plataforma." CssClass="observacion"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
       </div>
       <br />
       <br />
     </div>

</asp:Content>
