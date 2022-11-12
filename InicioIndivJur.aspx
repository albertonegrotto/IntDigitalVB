<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="InicioIndivJur.aspx.vb" Inherits="INTeatroDig.InicioIndivJur" 
    title="Alta de Personas Jurídicas" Culture="es-AR" MaintainScrollPositionOnPostBack="True"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Alta de Datos Individual de Personas Jurídicas</h2>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/index.aspx" CssClass="linksBold">Volver</asp:HyperLink>
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-10 col-centered Texto1" style="text-align: center">
           Declaro CONOCER y ACEPTAR en su totalidad los términos y condiciones de la reglamentación vigente, la<br />
           <asp:HyperLink ID="HyperLink1" runat="server" 
                NavigateUrl="http://inteatro.gob.ar/intdigital/reglamentacion_registro.htm" CssClass="links" Target="_blank">
                Reglamentación del Registro Nacional del Teatro Independiente
           </asp:HyperLink>
            , la 
           <asp:HyperLink ID="HyperLink5" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/40000-44999/42762/norma.htm" CssClass="links" Target="_blank">
                Ley Nº 24.800
           </asp:HyperLink>
           y su 
           <asp:HyperLink ID="HyperLink6" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/45000-49999/46047/norma.htm" CssClass="links" Target="_blank">
                    Decreto Reglamentario Nº 991/97
           </asp:HyperLink><br />
           <asp:CheckBox ID="AceptoDJ" runat="server" Text="Acepto" AutoPostBack="True" /> &nbsp;&nbsp;<asp:Label ID="Label8" runat="server" style="color: red" Text="[*]"></asp:Label> 
           <asp:Label ID="Label3" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row rowCentered">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
          <asp:Label ID="Label1" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>

<div id="tablaDatos" runat="server" visible="false" class="container-fluid">
    <br />
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
          <label>Tipo de Persona: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
          <asp:DropDownList ID="ddlPersona" runat="server" CssClass="form-control"></asp:DropDownList>
       </div>
       <div class="col-md-4 col-left">
          <%--<asp:Label ID="Label9" runat="server" style="color: red" Text="[*]"></asp:Label>--%>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br /> 
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Entidad/Sociedad: </label>
       </div>
       <div class="col-md-4 col-centered" style="text-align: left">
           <asp:DropDownList ID="ddlEntSoc" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
       </div>
       <div class="col-md-3 col-left">
          <%--<asp:Label ID="Label4" runat="server" style="color: red" Text="[*]"></asp:Label>--%>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Tipo de Entidad/Sociedad: </label>
       </div>
       <div class="col-md-4 col-centered" style="text-align: left">
           <asp:DropDownList ID="ddlCat" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
       </div>
       <div class="col-md-3 col-left">
          <%--<asp:Label ID="Label5" runat="server" style="color: red" Text="[*]"></asp:Label>--%>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Nº de CUIT / CUIL: </label>
       </div>
       <div class="col-md-2 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxCUIT" runat="server"  CssClass="form-control"></asp:TextBox>
           <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
               TargetControlID="TextBoxCUIT" 
               Mask="99 99999999 9"
               MessageValidatorTip="true" 
               MaskType="Number" 
               AcceptNegative="Left" 
               ErrorTooltipEnabled="True">
           </asp:MaskedEditExtender>
       </div>
       <div class="col-md-4 text-left">
           <asp:Label ID="Label30" runat="server" style="color: red" Text="[*]"></asp:Label>
           <asp:Label ID="lblErrorTextBoxCUIT" runat="server" CssClass="text-danger"></asp:Label>
       </div>
       <div class="col-md-2 col-left">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-8 col-center">
           <asp:label ID="Label2" runat="server" CssClass="observacion">Consignar la denominación completa y exacta de la Entidad, sin abreviaturas ni comillas ni caracteres especiales.</asp:label>
       </div>
       <div class="col-md-2 col-left">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
            <label>Denominación Completa:</label>
       </div>
       <div class="col-md-4 col-centered" style="text-align: left">
            <asp:TextBox ID="TextBoxDenomina" runat="server" Width="450px" CssClass="form-control" onkeypress="return validate(event)"></asp:TextBox>
       </div>
       <div class="col-md-3 col-left">
          <asp:Label ID="Label6" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
            <label>Personería Jurídica Nº/Matrícula:</label>
       </div>
       <div class="col-md-1 col-centered" style="text-align: left">
            <asp:TextBox ID="TextBoxPersoner" runat="server" Width="100px" CssClass="form-control" ></asp:TextBox>
       </div>
       <div class="col-md-6 col-left">
          <asp:Label ID="Label7" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Provincia:  </label>
       </div>
       <div class="col-md-4 col-centered" style="text-align: left">
           <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
       </div>
       <div class="col-md-3 col-left">
          <asp:Label ID="Label10" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Localidad: </label>
       </div>
       <div class="col-md-4 col-centered" style="text-align: left">
           <asp:DropDownList ID="DdlLocalidad" runat="server" CssClass="form-control" ></asp:DropDownList>
           <asp:Label ID="lblErrorDdlLocalidad" runat="server" CssClass="text-danger"></asp:Label>
       </div>
       <div class="col-md-3 col-left">
          <asp:Label ID="Label11" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
          <label>Código Postal: </label>
       </div>
       <div class="col-md-1 col-centered" style="text-align: left">
          <asp:TextBox ID="TextBoxCopost" runat="server" Width="100px" CssClass="form-control" ></asp:TextBox>
       </div>
       <div class="col-md-6 col-left">
          <asp:Label ID="Label12" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
          <label>Domicilio Constituido: </label>
       </div>
       <div class="col-md-4 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxDomicilio" runat="server" Width="450px" CssClass="form-control"></asp:TextBox>
       </div>
       <div class="col-md-3 col-left">
          <asp:Label ID="Label13" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Teléfono de la Entidad: </label>
        </div>
        <div class="col-md-1 text-center" style="width: 30px">
            <label for="TextBoxPrefTelPart">0</label> 
        </div>
        <div class="col-md-6 col-centered" style="text-align: left"> 
            &nbsp;
            <asp:TextBox ID="TextBoxPrefTelPart" placeholder="Prefijo" runat="server" CssClass="form-control pull-left" Width="90px" MaxLength="3" onkeypress="return validatel(event)"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="TextBoxTelPart" placeholder="Teléfono" runat="server" Width="230px" MaxLength="8" CssClass="form-control pull-left" onkeypress="return validatel(event)"></asp:TextBox>
            <asp:Label ID="lblErrorTelefono" runat="server" CssClass="text-danger"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
    </div>
    <br />
    <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-9 col-centered" style="text-align: center">
         <asp:label CssClass="observacion" runat="server">En la cuenta de correo electrónico que consigne a continuación recibirá la confirmación de procesamiento de este trámite, y desde ella deberá confirmar su validación, por tal considere informar una cuenta 'personal' y de chequeo habitual permanente.</asp:label>
      </div>
      <div class="col-md-1 text-right">
      </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
          <label>Correo Electrónico: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxMail" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
           <asp:Label ID="lblErrorTextBoxMail" runat="server" CssClass="text-danger"></asp:Label>
       </div>
       <div class="col-md-4 col-left">
          <asp:Label ID="Label14" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
          <label>Confirmar Correo Electrónico: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxConfMail" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
       </div>
       <div class="col-md-4 col-left">
          <asp:Label ID="Label15" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
         <label class="observacion">(consignar 8 caracteres con por lo menos un número)</label>
      </div>
      <div class="col-md-2 text-right">
      </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Ingreso de Contraseña: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxContra" runat="server" TextMode="Password"  CssClass="form-control" MaxLength="8"></asp:TextBox>
           <asp:Label ID="lblErrorTextBoxContra" runat="server" CssClass="text-danger"></asp:Label>
       </div>
       <div class="col-md-4 col-left">
          <asp:Label ID="Label16" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Reiteración de Contraseña:: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxReContra" runat="server"  CssClass="form-control" TextMode="Password" MaxLength="8"></asp:TextBox>
       </div>
       <div class="col-md-4 text-left">
          <asp:Label ID="Label17" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-8 col-centered" style="text-align: center">
           <label class="observacion">Elija una pregunta de seguridad para la cual le resulte sencillo recordar la respuesta, ya que ese dato se le pedirá cuando a futuro necesite recuperar su contraseña</label>
       </div>
       <div class="col-md-2 text-right">
       </div>
    </div>
    <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
         <label>Recuperación de Contraseña: </label>
      </div>
      <div class="col-md-4 col-centered" style="text-align: left">
         <asp:DropDownList ID="DdlPregunta" runat="server" CssClass="form-control"></asp:DropDownList>
         <asp:Label ID="lblErrorDdlPregunta" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-3 col-left">
           <asp:Label ID="Label18" runat="server" style="color: red" Text="[*]"></asp:Label>
      </div>
      <div class="col-md-1 text-right">
      </div>
    </div>
    <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
         <label>Respuesta: </label>
      </div>
      <div class="col-md-3 col-centered" style="text-align: left">
         <asp:TextBox ID="TextBoxRespuesta" runat="server" Width="270px" CssClass="form-control"></asp:TextBox>
         <asp:Label ID="lblErrorTextBoxRespuesta" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-4 col-left">
          <asp:Label ID="Label19" runat="server" style="color: red" Text="[*]"></asp:Label>
      </div>
      <div class="col-md-1 text-right">
      </div>
    </div>
    <br />
    <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-9 col-centered" style="text-align: center">
         <asp:label CssClass="observacion" runat="server">Los datos consignados tienen carácter de Declaración Jurada y deberán actualizarse toda vez que produzca cualquier cambio en relación a lo declarado precedentemente.</asp:label>
         <br />
         <asp:CheckBox ID="CheckBoxAcepto" runat="server" Text="Acepto" />
         &nbsp;&nbsp;<asp:Label ID="Label27" runat="server" style="color: red" Text="[*]"></asp:Label>
         <asp:Label ID="lblErrorCheckBoxAcepto" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-1 text-right">
      </div>
    </div>
    <br />
    <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-9 col-centered" style="text-align: center">
          <asp:Label ID="FailureText" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-1 text-right">
      </div>
    </div>        
    <br />
    <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-9 col-centered" style="text-align: center">
          <asp:Button ID="BtnEnviar" runat="server" Text="Confirmar Alta Individual" CssClass="btn btn-success" />        
          <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-warning"  /> &nbsp;&nbsp;<asp:Label ID="Label4" runat="server" style="color: red" Text="[*]"></asp:Label>   
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

    <script type="text/javascript">
        $(document).ready(function() {
            $("#aspnetForm").validate({
                rules: {
                    <%=TextBoxCUIT.UniqueID %>: {
                        required: true
                    },
                     <%=TextBoxDenomina.UniqueID %>: {                       
                        required: true
                    },
                     <%=TextBoxPersoner.UniqueID %>: {                       
                        required: true,
                        range: [1 , 9999999999]
                    },
                     <%=TextBoxCopost.UniqueID %>: {                       
                        required: true,
                        range: [1000 , 9999]
                    },
                     <%=TextBoxDomicilio.UniqueID %>: {                       
                        required: true
                    },
                     <%=TextBoxMail.UniqueID %>: {                       
                        required: true,
                        email: true
                    },
                     <%=TextBoxConfMail.UniqueID %>: {                       
                        required: true,
                        email: true
                    },
                     <%=TextBoxContra.UniqueID %>: {                       
                        required: true
                    },
                     <%=TextBoxReContra.UniqueID %>: {                       
                        required: true
                    },
                     <%=TextBoxRespuesta.UniqueID %>: {                       
                        required: true
                    }
                }, messages: {
                    <%=TextBoxCUIT.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=TextBoxDenomina.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=TextBoxPersoner.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        range:  "<span  style='color:red'>* Debe ingresar entre 1 y 9999999999 *</span>"
                    },
                    <%=TextBoxCopost.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        range:  "<span  style='color:red'>* Debe ingresar entre 1000 y 9999 *</span>"
                    },
                    <%=TextBoxDomicilio.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=TextBoxMail.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        email: "<span  style='color:red'>Debe ingresar un correo válido</span>"
                    },
                    <%=TextBoxConfMail.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        email: "<span  style='color:red'>Debe ingresar un correo válido</span>"
                    },
                    <%=TextBoxContra.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=TextBoxReContra.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=TextBoxRespuesta.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    }
               }
            });
        });
    </script>

    <script type="text/javascript">
        function validate(key) {
        var keycode = (key.which) ? key.which : key.keyCode;
         if ((keycode > 64 && keycode < 91) || (keycode > 96 && keycode < 123) || keycode == 8 || keycode == 241 || keycode == 209  || keycode == 32 || (keycode >= 48 && keycode <= 57)) {
            return true;
            }
        else
            {
            return false;
            }
        }
    </script>

    <script type="text/javascript">
    //Function to allow only numbers to textbox
    function validatel(key) {
        var keycode = (key.which) ? key.which : key.keyCode;
        if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
            return false;
        }
        else {
                return true;
            }
        }
   </script>

</asp:Content>
