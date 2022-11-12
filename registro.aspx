<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="registro.aspx.vb" Inherits="INTeatroDig.registro" 
    title="Registro" Culture="es-AR"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 <div id="Datos" runat="server" class="container-fluid">

   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
          <asp:Label ID="lblTitulo" runat="server" Text=" " CssClass="TextoSubtitulo">Registro / Actualización de Registro</asp:Label>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/AltaIni.aspx" CssClass="linksBold">Volver</asp:HyperLink>
      </div>
   </div>
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center" id="divBannerTop" runat="server">
          <asp:Label runat="server" CssClass="TextoComentario">Este trámite debe ser realizado únicamente por el RESPONSABLE (ya sea persona física o jurídica), el cual, al igual que cada integrante a vincular, debe haber previamente tramitado y confirmado su ALTA INDIVIDUAL en el sistema INTeatro Digital</asp:Label>
      </div>
      <div class="col-md-2 text-left">
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
      <div class="col-md-7 col-centered" style="text-align: left">
          <asp:TextBox ID="txtCUIT" runat="server" Width="100px" Text="" CssClass="form-control"></asp:TextBox>
          <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
              TargetControlID="txtCUIT" 
              Mask="99 99999999 9"
              MessageValidatorTip="true" 
              MaskType="Number" 
              AcceptNegative="Left" 
              ErrorTooltipEnabled="True">
          </asp:MaskedEditExtender>
          <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
              ControlExtender="MaskedEditExtender1"  
              ControlToValidate="txtCUIT"   
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
     <div class="col-md-1 text-right">
     </div>
   </div>    
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
      </div>
      <div class="col-md-7 col-centered" style="text-align: left">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
              ControlToValidate="txtCUIT" ErrorMessage="" 
              ValidationGroup="changepassword" 
              Width="72px">
          </asp:RequiredFieldValidator>
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
      <div class="col-md-7 col-centered" style="text-align: left">
          <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="100px" 
                  MaxLength="6" Cssclass="form-control">
          </asp:TextBox>
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
      </div>
      <div class="col-md-7 col-centered" style="text-align: left">
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtPassword" ErrorMessage="" 
            ValidationGroup="changepassword" Width="72px">
         </asp:RequiredFieldValidator>
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>     
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-9 col-centered" style="text-align: center" id="divBannerMiddle" runat="server">
          Declaro CONOCER y ACEPTAR en su totalidad los términos y condiciones de la Reglamentación vigente para el trámite de "SOLICITUD DE REGISTRO o ACTUALIZACION en el REGISTRO NACIONAL DEL TEATRO INDEPENDIENTE&nbsp;
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-10 col-centered Texto1" style="text-align: center" id="divBannerBottom" runat="server">
           <asp:HyperLink ID="HyperLink2" runat="server" 
                NavigateUrl="http://inteatro.gob.ar/intdigital/reglamentacion_registro.htm" CssClass="links" Target="_blank">
                Reglamentación del Registro Nacional del Teatro Independiente
           </asp:HyperLink>
            , la 
           <asp:HyperLink ID="HyperLink3" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/40000-44999/42762/norma.htm" CssClass="links" Target="_blank">
                Ley Nº 24.800
           </asp:HyperLink>
           y su 
           <asp:HyperLink ID="HyperLink4" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/45000-49999/46047/norma.htm" CssClass="links" Target="_blank">
                    Decreto Reglamentario Nº 991/97
           </asp:HyperLink><br />
           <asp:CheckBox ID="CheckBoxAcepto" runat="server" Text="Acepto" AutoPostBack="True" />
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
          <asp:Button ID="BtnEnviar" runat="server" Text="Ingresar" CssClass="btn btn-primary" />
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
