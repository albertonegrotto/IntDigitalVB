<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="LoginInicio.aspx.vb" Inherits="INTeatroDig.LoginInicio" 
    Culture="es-AR" validateRequest="false"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 <div id="Datos" runat="server" class="container-fluid">

      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-6 col-centered" style="text-align: center">
              <asp:Label ID="lblTitulo" runat="server" Text="Actualización de Datos Individuall"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
              <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/AltaIni.aspx" CssClass="linksBold">Volver</asp:HyperLink>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nº de CUIT / CUIL:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:TextBox ID="Username" runat="server" Width="90px" Text="" CssClass="form-control"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                    TargetControlID="Username" 
                    Mask="99 99999999 9"
                    MessageValidatorTip="true" 
                    MaskType="Number" 
                    AcceptNegative="Left" 
                    ErrorTooltipEnabled="True">
                </asp:MaskedEditExtender>
                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                    ControlExtender="MaskedEditExtender1"  
                    ControlToValidate="Username"   
                    IsValidEmpty="False"   
                    EmptyValueMessage="Debe ingresar el CUIT o CUIL"  
                    InvalidValueMessage="CUIT / CUIL inválido"  
                    EmptyValueBlurredText=""   
                    InvalidValueBlurredMessage=""   
                    MaximumValueBlurredMessage=""   
                    MinimumValueBlurredText=""  
                    Display="Dynamic"   
                    TooltipMessage="Ingrese su CUIT o CUIL sin espacios ni caracteres especiales, solo números" >
                </asp:MaskedEditValidator>                                 
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
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                 ControlToValidate="Username" ErrorMessage="" 
                 ValidationGroup="changepassword" 
                 Width="72px">
             </asp:RequiredFieldValidator>
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
              <label>Contraseña:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="90px"  CssClass="form-control"
                    Text="" MaxLength="6" ></asp:TextBox>
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
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="Password" ErrorMessage="" 
                ValidationGroup="changepassword" Width="72px">
            </asp:RequiredFieldValidator>
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
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
             <asp:CheckBox ID="Remember" runat="server" Text="Recordar" CssClass="form-control" Width="70px" />
          </div>
          <div class="col-md-4 col-left">
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
               <asp:Button ID="BtnEnviar" runat="server" Text="Ingresar" CssClass="btn btn-success" />
          </div>
          <div class="col-md-4 col-left">
               <asp:Button ID="BtnRecupera" runat="server" Text="Recuperar Contraseña" CssClass="btn btn-warning" />
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
