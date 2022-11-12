<%@ Page Title="Alta de Personas Físicas" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="InicioIndivFis.aspx.vb" Inherits="INTeatroDig.InicioIndivFis" 
    Culture="es-AR" MaintainScrollPositionOnPostBack="True"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
    <style type="text/css">
        .left_align{
           text-align:left;
           direction:ltr;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Alta de Datos Individual de Personas Humanas</h2>
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
           <asp:HyperLink ID="HyperLinkReglamentacion" runat="server" 
                NavigateUrl="http://inteatro.gob.ar/intdigital/reglamentacion_registro.htm" CssClass="links" Target="_blank">
                Reglamentación del Registro Nacional del Teatro Independiente
           </asp:HyperLink>
            , la 
           <asp:HyperLink ID="HyperLinkLey24800" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/40000-44999/42762/norma.htm" CssClass="links" Target="_blank">
                Ley Nº 24.800
           </asp:HyperLink>
           y su 
           <asp:HyperLink ID="HyperLink4" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/45000-49999/46047/norma.htm" CssClass="links" Target="_blank">
                    Decreto Reglamentario Nº 991/97
           </asp:HyperLink><br />
           <asp:CheckBox ID="AceptoDJ" runat="server" Text=" Acepto" AutoPostBack="True" /> &nbsp;&nbsp;<asp:Label ID="Label8" runat="server" style="color: red" Text="[*]"></asp:Label>          
           <asp:Label ID="lblErrorCheckBoxAcepto" runat="server" CssClass="TextoError"></asp:Label>
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-9 col-centered" style="text-align: center">
          <asp:Label ID="Label1" runat="server" CssClass="TextoError"></asp:Label>
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>  
   <div id="tablaDatos" runat="server" visible="false" class="container-fluid">
    <br />
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Tipo de Persona:</label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:DropDownList ID="ddlPersona" runat="server" CssClass="form-control"></asp:DropDownList>
       </div>
       <div class="col-md-4 col-left">
         <%-- <asp:Label ID="Label9" runat="server" style="color: red" Text="[*]"></asp:Label>--%>
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
          <%-- <asp:Label ID="Label30" runat="server" style="color: red" Text="[*]"></asp:Label>--%>
           <asp:Label ID="lblErrorTextBoxCUIT" runat="server" CssClass="TextoError"></asp:Label>
       </div>
       <div class="col-md-2 col-left">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Fecha de Nacimiento:</label>
       </div>
       <div class="col-md-4 col-centered" style="text-align: left">
             <asp:DropDownList ID="ddlDia" runat="server" Width="100px"  CssClass="form-control pull-left" ></asp:DropDownList>
             &nbsp;
             <asp:DropDownList ID="ddlMes" runat="server" Width="160px"  CssClass="form-control pull-left" ></asp:DropDownList>
             &nbsp;
             <asp:DropDownList ID="ddlAnio" runat="server" Width="90px"  CssClass="form-control pull-left" ></asp:DropDownList>        
             <asp:Label ID="lblErrorFechaNacimiento" runat="server" CssClass="TextoError"></asp:Label>
       </div>
       <div class="col-md-3 text-left">
           <asp:Label ID="Label10" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <br />
    <div class="row">
       <div class="col-md-2 col-left">
       </div>
       <div class="col-md-9 col-center">
           <asp:label ID="Label2" runat="server" CssClass="TextoComentario">Los menores de 18 años con carácter de EMANCIPADOS deberán presentar ante el I.N.T. copia autenticada de la documentación respectiva por la cual ha sido otorgada la emancipación. </asp:label>
       </div>
       <div class="col-md-1 col-left">
       </div>
    </div>
    <div class="row">
       <div class="col-md-5 col-left">
       </div>
       <div class="col-md-4 col-center">
           <asp:CheckBox ID="CheckBoxEdad" runat="server" Text="Acepto" />
           <asp:Label ID="lblErrorCheckBoxEdad" runat="server" CssClass="TextoError"></asp:Label>
       </div>
       <div class="col-md-3 col-left">
       </div>
    </div> 
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Nombre/s Completo/s Real: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxNombre" runat="server" Width="350px"  CssClass="form-control" onkeypress="return validaten(event)" ></asp:TextBox>
           <asp:Label ID="Label4" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
       </div>
       <div class="col-md-4 text-left">
           <asp:Label ID="Label11" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
            <label>Apellido/s Completo/s Real: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
            <asp:TextBox ID="TextBoxApellido" runat="server" Width="350px"  CssClass="form-control"  onkeypress="return validate(event)" ></asp:TextBox>
            <asp:Label ID="Label5" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
       </div>
       <div class="col-md-4 text-left">
           <asp:Label ID="Label12" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Género: </label>
       </div>
       <div class="col-md-4 col-centered" style="text-align: left">
           <asp:DropDownList ID="ddlSexo" runat="server" Width="400px" CssClass="form-control" onchange="EvalSexo()" ></asp:DropDownList>          
           <asp:Label ID="lblErrorddlSexo" runat="server" CssClass="TextoError"></asp:Label>
           <asp:TextBox ID="TextBoxDsexo"  runat="server" placeholder="Descripción" CssClass="form-control pull-left" Width="200px" MaxLength="50"></asp:TextBox> 
       </div>
       <div class="col-md-4 text-left">
           <asp:Label ID="Label13" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>
    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Nacionalidad: </label>
       </div>
       <div class="col-md-3 col-centered" style="text-align: left">
           <asp:DropDownList ID="DdlNacional" runat="server"  Width="200px"  CssClass="form-control" ></asp:DropDownList>           
           <asp:Label ID="lblErrorDdlNacional" runat="server" CssClass="TextoError"></asp:Label>
       </div>
       <div class="col-md-4 text-left">
           <asp:Label ID="Label14" runat="server" style="color: red" Text="[*]"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-3 col-centered" style="text-align: center">
           <label>Años de Residencia: </label>
       </div>
       <div class="col-md-7 col-centered" style="text-align: left">
           <asp:TextBox ID="TextBoxresid" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>&nbsp;
           <asp:Label ID="Label44" runat="server" Text="Completar solo extranjeros" CssClass="TextoComentario"></asp:Label>
           <asp:Label ID="lblErrorTextBoxresid" runat="server" CssClass="TextoError"></asp:Label>
       </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <br />
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Provincia: </label>
        </div>
        <div class="col-md-4 col-centered" style="text-align: left">
            <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-control"
                AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-md-3 col-left">
            <asp:Label ID="Label16" runat="server" style="color: red" Text="[*]"></asp:Label>
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
            <asp:Label ID="lblErrorDdlLocalidad" runat="server" CssClass="TextoError"></asp:Label>
        </div>
        <div class="col-md-3 col-left">
            <asp:Label ID="Label17" runat="server" style="color: red" Text="[*]"></asp:Label>
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
           <asp:TextBox ID="TextBoxCopost" runat="server" Width="80px"  CssClass="form-control" ></asp:TextBox>
           <asp:Label ID="lblErrorCPOSTAL" runat="server" CssClass="TextoError"></asp:Label>
       </div>
        <div class="col-md-6 col-left">
            <asp:Label ID="Label15" runat="server" style="color: red" Text="[*]"></asp:Label>
        </div>
       <div class="col-md-1 text-right">
       </div>
     </div>
     <br />
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Domicilio Particular: </label>
        </div>
        <div class="col-md-4 col-centered" style="text-align: left">
            <asp:TextBox ID="TextBoxDompar" runat="server" Width="450px"  CssClass="form-control" ></asp:TextBox>
            <asp:Label ID="lblErrorTextBoxDompar" runat="server" CssClass="TextoError"></asp:Label>
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
            <label>Teléfono Particular: </label>
        </div>
        <div class="col-md-1 text-center" style="width: 30px">
            <label for="TextBoxPrefTelPart">0</label> 
        </div>
        <div class="col-md-6 col-centered" style="text-align: left"> 
            &nbsp;
            <asp:TextBox ID="TextBoxPrefTelPart" placeholder="Prefijo" runat="server" CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="TextBoxTelPart" placeholder="Teléfono" runat="server" Width="230px" MaxLength="8" CssClass="form-control pull-left"></asp:TextBox>
            <asp:Label ID="lblErrorTelefono" runat="server" CssClass="TextoError"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Teléfono Celular: </label>
        </div>
        <div class="col-md-1 text-left" style="width: 30px">
            <label for="TextBoxPrefCelu" >549</label> 
        </div>
        <div class="col-md-1 col-centered" style="text-align: left">
            <asp:TextBox ID="TextBoxPrefCelu" placeholder="Prefijo" runat="server"  CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>                
        </div>
        <div class="col-md-1 text-right" style="width: 10px">
            <label for="TextBoxCelular" > 15 </label> 
        </div>
        <div class="col-md-2 text-right">
            <asp:TextBox ID="TextBoxCelular" placeholder="Teléfono" runat="server" CssClass="form-control pull-left" Width="230px" MaxLength="8"></asp:TextBox>
            <asp:Label ID="lblErrorCelular" runat="server" CssClass="TextoError"></asp:Label>
        </div>
        <div class="col-md-2 col-left">
            <asp:Label ID="Label19" runat="server" style="color: red" Text="[*]"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <br />
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Tipo de Formación: </label>
        </div>
        <div class="col-md-4 col-centered" style="text-align: left">
            <asp:DropDownList ID="DdlFormacion" runat="server" CssClass="form-control" onchange="EvalFormacion()"></asp:DropDownList>
            <asp:Label ID="LabelErrorFormacion" runat="server" CssClass="TextoError"></asp:Label>
        </div>
        <div class="col-md-3 col-left">
            <asp:Label ID="Label20" runat="server" style="color: red" Text="[*]"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Título Alcanzado: </label>
        </div>
        <div class="col-md-4 col-centered" style="text-align: left">
            <asp:DropDownList ID="DdlTitulo" runat="server" CssClass="form-control"></asp:DropDownList>
            <asp:Label ID="LabelErrorTitulo" runat="server" CssClass="TextoError"></asp:Label>
        </div>
        <div class="col-md-3 col-left">
            <asp:Label ID="Label21" runat="server" style="color: red" Text="[*]"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <br />
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
           <label>Currículum Vitae: </label>
        </div>
        <div class="col-md-3 col-left">
            <asp:FileUpload ID="UploadImporta" runat="server" CssClass="form-control" onchange="EvalDocumento()"/>
            <asp:Label ID="Label7" runat="server" Text="Puede adjuntar un archivo en formato .doc .docx o .pdf de hasta 10 Mb." CssClass="TextoComentario"></asp:Label>
        </div>
        <div class="col-md-2 col-left">
            <asp:Label ID="LabelNombreUpload" runat="server" CssClass="form-control" Height="50px"></asp:Label>
        </div>
        <div class="col-md-2 col-left">
            <asp:Button ID="BtnVisualiza" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
        </div>
        <div class="col-md-1 col-left">
        </div>
     </div>
     <br />
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
           <label>WhatsApp: </label>
        </div>
        <div class="col-md-2 col-centered" style="text-align: left">
           <asp:DropDownList ID="DDlWhatsApp" runat="server" CssClass="form-control"> </asp:DropDownList>
        </div>
        <div class="col-md-5 text-left">
           <asp:Label ID="Label6" runat="server" Text="¿Desea consignar si utiliza esa aplicación y quiere ser contactado por el INT por ese medio?" CssClass="TextoComentario"></asp:Label>
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
            <asp:Checkbox ID="facebook" runat="server" Visible="true" Checked="false" Text="Facebook" CssClass="form-control" Width="200px"/><br />
            <asp:CheckBox ID="instagram" runat="server" Visible="true" Checked="false"  Text="Instagram" CssClass="form-control" Width="200px"/><br />
            <asp:CheckBox ID="twiter" runat="server" Visible="true" Checked="false"  Text="Twiter" CssClass="form-control" Width="200px"/><br />
            <asp:CheckBox ID="youtube" runat="server" Visible="true" Checked="false"  Text="Youtube" CssClass="form-control" Width="200px"/><br />
        </div>
        <div class="col-md-1 text-right">
        </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">
               <label>Profesión/es - Oficio/s: (seleccione al menos una)</label>
               &nbsp;&nbsp;<asp:Label ID="Label23" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:Checkbox ID="chkActividad1" runat="server" Visible="true" Checked="false" Text="Actuación / Interpretación / Performer" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad2" runat="server" Visible="true" Checked="false"  Text="Dirección de actores" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad3" runat="server" Visible="true" Checked="false"  Text="Asistencia de dirección" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad4" runat="server" Visible="true" Checked="false"  Text="Puesta en escena" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad5" runat="server" Visible="true" Checked="false"  Text="Dramaturgia" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad6" runat="server" Visible="true" Checked="false"  Text="Investigación" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad7" runat="server" Visible="true" Checked="false"  Text="Producción" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad8" runat="server" Visible="true" Checked="false"  Text="Asistencia de producción" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad9" runat="server" Visible="true" Checked="false"  Text="Diseño lumínico/sonido" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad10" runat="server" Visible="true" Checked="false"  Text="Operación técnica de luces y/o sonido" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad11" runat="server" Visible="true" Checked="false"  Text="Diseño sonoro" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad12" runat="server" Visible="true" Checked="false"  Text="Ejecución musical" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad13" runat="server" Visible="true" Checked="false"  Text="Realización de Escenografía" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad14" runat="server" Visible="true" Checked="false"  Text="Diseño escenoplástico" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad15" runat="server" Visible="true" Checked="false"  Text="Maquinista" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad16" runat="server" Visible="true" Checked="false"  Text="Escenotecnia" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad17" runat="server" Visible="true" Checked="false"  Text="Utilería" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad18" runat="server" Visible="true" Checked="false"  Text="Vestuario" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad19" runat="server" Visible="true" Checked="false"  Text="Maquillaje /Caracterizaciones / Peinados" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad20" runat="server" Visible="true" Checked="false"  Text="Gestión Cultural" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad21" runat="server" Visible="true" Checked="false"  Text="Comunicación / Divulgación" CssClass="form-control" Width="350px"/><br />
              <asp:CheckBox ID="chkActividad22" runat="server" Visible="true" Checked="false"  Text="Mediación de espectadores" CssClass="form-control" Width="350px"/><br />
              <br /> 
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
             <asp:Label ID="txtErrorActividad" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">
               <label>Lenguaje - Disciplina artística en la que se desarrolla: (seleccione al menos una)</label>
               &nbsp;&nbsp;<asp:Label ID="Label24" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:Checkbox ID="ChkDiscipli1" runat="server" Visible="true" Checked="false" Text="Teatro" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli2" runat="server" Visible="true" Checked="false"  Text="Máscara" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli3" runat="server" Visible="true" Checked="false"  Text="Danza" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli4" runat="server" Visible="true" Checked="false"  Text="Danza Contemporánea" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli5" runat="server" Visible="true" Checked="false"  Text="Danza Teatro" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli6" runat="server" Visible="true" Checked="false"  Text="Títeres" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli7" runat="server" Visible="true" Checked="false"  Text="Marionetas" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli8" runat="server" Visible="true" Checked="false"  Text="Mimo" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli9" runat="server" Visible="true" Checked="false"  Text="Circo" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli10" runat="server" Visible="true" Checked="false"  Text="Clown" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli11" runat="server" Visible="true" Checked="false"  Text="Payaso/a" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli12" runat="server" Visible="true" Checked="false"  Text="Magia" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli13" runat="server" Visible="true" Checked="false"  Text="Murga" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli14" runat="server" Visible="true" Checked="false"  Text="Murga Teatro" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli15" runat="server" Visible="true" Checked="false"  Text="Performance" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli16" runat="server" Visible="true" Checked="false"  Text="Teatro de objetos" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli17" runat="server" Visible="true" Checked="false"  Text="Teatro Comunitario" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli18" runat="server" Visible="true" Checked="false"  Text="Teatro en Oscuridad" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli19" runat="server" Visible="true" Checked="false"  Text="Teatro Físico" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli20" runat="server" Visible="true" Checked="false"  Text="Teatro Aéreo " CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli21" runat="server" Visible="true" Checked="false"  Text="Teatro de Imagen" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli22" runat="server" Visible="true" Checked="false"  Text="Tecnovivio" CssClass="form-control" Width="200px"/><br />
              <asp:Checkbox ID="ChkDiscipli23" runat="server" Visible="true" Checked="false"  Text="Teatro de sombras" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli24" runat="server" Visible="true" Checked="false"  Text="Teatro Musical" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli25" runat="server" Visible="true" Checked="false"  Text="Teatro Negro" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli26" runat="server" Visible="true" Checked="false"  Text="Teatro Ciego" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli27" runat="server" Visible="true" Checked="false"  Text="Teatro de Revista" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli28" runat="server" Visible="true" Checked="false"  Text="Varieté" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli29" runat="server" Visible="true" Checked="false"  Text="Café Concert" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli30" runat="server" Visible="true" Checked="false"  Text="Improvisación" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli31" runat="server" Visible="true" Checked="false"  Text="Stand Up" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli32" runat="server" Visible="true" Checked="false"  Text="Ópera" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli33" runat="server" Visible="true" Checked="false"  Text="Monólogos" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli34" runat="server" Visible="true" Checked="false"  Text="Radioteatro" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkDiscipli35" runat="server" Visible="true" Checked="false"  Text="Cuentacuentos" CssClass="form-control" Width="200px"/><br />
              <br /> 
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
             <asp:Label ID="txtErrorDisciplina" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center;">
             <asp:label CssClass="observacion" runat="server">En la cuenta de correo electrónico que consigne a continuación recibirá la confirmación de procesamiento de este trámite, y desde ella deberá confirmar su validación, por tal considere informar una cuenta 'personal' y de chequeo habitual permanente.</asp:label>
             <br />
             <asp:CheckBox ID="CheckBox1" runat="server" Text="Acepto" Width="80px" />
             <asp:Label ID="lblErrorCheckBox1" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Correo Electrónico: </label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="TextBoxMail" runat="server" Width="270px"  CssClass="form-control" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
              <asp:Label ID="lblErrorTextBoxMail" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label22" runat="server" style="color: red" Text="[*]"></asp:Label>
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
             <asp:TextBox ID="TextBoxConfMail" runat="server" Width="270px"  CssClass="form-control" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label26" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
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
             <asp:TextBox ID="TextBoxContra" runat="server" Width="260px" TextMode="Password" MaxLength="8" CssClass="form-control pull-left" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>                            
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label31" runat="server" style="color: red" Text="[*]"></asp:Label>
              <asp:Label ID="lblErrorTextBoxContra" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-1 text-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Reiteración de Contraseña: </label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
               <asp:TextBox ID="TextBoxReContra" runat="server" Width="260px" TextMode="Password" MaxLength="8" CssClass="form-control" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label25" runat="server" style="color: red" Text="[*]"></asp:Label>
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
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="DdlPregunta" runat="server" Width="260px" CssClass="form-control"></asp:DropDownList>
              <asp:Label ID="lblErrorDdlPregunta" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
               <asp:Label ID="Label28" runat="server" style="color: red" Text="[*]"></asp:Label>
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
          <div class="col-md-4 col-centered" style="text-align: left">
             <asp:TextBox ID="TextBoxRespuesta" runat="server" CssClass="form-control"></asp:TextBox>
             <asp:Label ID="lblErrorTextBoxRespuesta" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-3 col-left">
               <asp:Label ID="Label29" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:label CssClass="observacion" runat="server">Los datos consignados tienen carácter de Declaración Jurada y deberán actualizarse toda vez que produzca cualquier cambio en relación a lo declarado precedentemente.</asp:label>
             <br />
             <asp:CheckBox ID="CheckBoxAcepto" runat="server" Text="Acepto" />
             &nbsp;&nbsp;<asp:Label ID="Label27" runat="server" style="color: red" Text="[*]"></asp:Label>
             <asp:Label ID="Label3" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />       
       <!-- Palabras a publicar -->
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:label CssClass="observacion" runat="server">Autorizo al Instituto Nacional del Teatro a que los datos consignados sean de acceso público (en virtud de lo establecido por la Ley 25.326 de Protección de Datos Personales) y sirvan de utilidad para uso estadístico y para la implementación de políticas públicas para el sector cultural</asp:label>
             <br />
             <asp:CheckBox ID="checkAutorizoPublicar" runat="server" Text="Acepto" Visible="True" /> &nbsp;&nbsp;<asp:Label ID="Label32" runat="server" style="color: red" Text="[*]"></asp:Label>
             <asp:Label ID="lblErrorcheckAutorizoPublicar" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <!-- End of Palabras a publicar -->
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:Label ID="FailureText" runat="server" CssClass="TextoError"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
              <asp:Button ID="BtnEnviar" runat="server" Text="Confirmar Alta Individual" CssClass="btn btn-success" /> 
              <asp:Button ID="btnCancelar" runat="server" class="btn btn-warning" Text="Cancelar" /> &nbsp;&nbsp;<asp:Label ID="Label33" runat="server" style="color: red" Text="[*]"></asp:Label>   
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
        $(document).ready(function () {
            EvalSexo();
            $("#aspnetForm").validate({
                rules: {
                    <%=TextBoxApellido.UniqueID %>: {
                        required: true
                    },
                     <%=TextBoxNombre.UniqueID %>: {                       
                        required: true
                    },
                     <%=TextBoxCopost.UniqueID %>: {                       
                        required: true,
                        range: [1000 , 99999]
                    },
                     <%=TextBoxDompar.UniqueID %>: {                       
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
                    <%=TextBoxApellido.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=TextBoxNombre.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=TextBoxCopost.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        range:  "<span  style='color:red'>* Debe ingresar entre 1000 y 9999 *</span>"
                    },
                    <%=TextBoxDompar.UniqueID %>:{
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
         if ((keycode > 64 && keycode < 91) || (keycode > 96 && keycode < 123) || keycode == 8 || keycode == 241 || keycode == 209  || keycode == 32  || keycode == 34  || keycode == 39) {
            return true;
            }
        else
            {
            return false;
            }
        }
   </script>

   <script type="text/javascript">
        function validaten(key) {
        var keycode = (key.which) ? key.which : key.keyCode;
         if ((keycode > 64 && keycode < 91) || (keycode > 96 && keycode < 123) || keycode == 8 || keycode == 241 || keycode == 209  || keycode == 32 ) {
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

   <script type="text/javascript">
        function EvalSexo() {
            var e = document.getElementById("<%=ddlSexo.ClientID %>");
            var desc = document.getElementById("<%=TextBoxDsexo.ClientID %>");
            var sCodigo = e.options[e.selectedIndex].value;
            if (sCodigo == 6) {
                desc.style.display = "block";
            }
            else {
                desc.value = "";
                desc.style.display = "none";
            }
        }
   </script>

   <script type="text/javascript">
       function EvalFormacion() {
            var e = document.getElementById("<%=DdlFormacion.ClientID %>");
            var desc = document.getElementById("<%=DdlTitulo.ClientID %>");
            var sCodigo = e.options[e.selectedIndex].value;
            if (sCodigo == 1) {
                desc.disabled = false;
            }
            else {
                desc.value = 0;
                desc.disabled = true;
            }
        }
   </script>

   <script type="text/javascript">
        function EvalDocumento() {
          var sfile =document.getElementById("<%=UploadImporta.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelNombreUpload.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
   </script>


</asp:Content>
