<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="contacto.aspx.vb" Inherits="INTeatroDig.contacto" 
    title="Formulario de Contacto" Culture="es-AR"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 <div id="tablaDatos" runat="server" class="container-fluid">

   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Envío de Correo Electrónico</h2>
      </div>
      <div class="col-md-2 text-left">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
      </div>
      <div class="col-md-5 col-centered" style="text-align: left">
      </div>
      <div class="col-md-2 col-left">
          <asp:Button ID="BtnVolver" runat="server" class="btn btn-primary" Text="Volver" />
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
         <label>Nombre/s:</label>
      </div>
      <div class="col-md-5 col-centered" style="text-align: left">
         <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Columns="30" CssClass="form-control" ToolTip="Nombre/s completo/s"></asp:TextBox>
         <asp:Label ID="txtErrorNombre" runat="server" CssClass="text-danger"></asp:Label>                
      </div>
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
         <label>Apellido/s:</label>
      </div>
      <div class="col-md-5 col-centered" style="text-align: left">
         <asp:TextBox ID="txtApellido" runat="server" MaxLength="50" Columns="30" CssClass="form-control" ToolTip="Apellido/s completo/s"></asp:TextBox>
         <asp:Label ID="txtErrorApellido" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
         <label>Correo Electrónico:</label>
      </div>
      <div class="col-md-4 col-centered" style="text-align: left">
         <asp:TextBox ID="txtFrom" runat="server" MaxLength="100" Columns="40" CssClass="form-control" ToolTip="Ingrese su cuenta de correo electrónico"></asp:TextBox>
      </div>
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
          <label>Asunto:</label>
      </div>
      <div class="col-md-3 col-centered" style="text-align: left">
         <asp:TextBox ID="txtSubject" runat="server" MaxLength="50" Columns="30" CssClass="form-control"
           ToolTip="Ingrese el asunto del correo electrónico"></asp:TextBox>
         <asp:Label ID="txtErrorSubject" runat="server" CssClass="text-danger"></asp:Label>                
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
          <label>Consignar Nº de CUIT/CUIL y descripción breve (hasta 1000 caracteres):</label>
      </div>
      <div class="col-md-7 col-centered" style="text-align: left">
          <asp:TextBox ID="txtBody" runat="server"  TextMode="multiline" MaxLength="1000" Columns="65" Rows="8" CssClass="form-control"            
            ToolTip="Ingrese el texto del correo electrónico, el mismo no debe contener mas de 1000 caracteres" 
            Width="696px"></asp:TextBox>
          <asp:Label ID="txtErrorBody" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <br />
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-center" style="text-align: center">
         <label for="FileUpload1">Adjunto</label>
      </div>
      <div class="col-md-4 col-left" style="text-align: left">
         <asp:FileUpload ID="UploadImporta" runat="server" CssClass="form-control"/>
         <asp:Label ID="LabelErrorAdjunto" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-3 text-center">
          <label class="observacion">Puede adjuntar archivos en formato .doc .docx .jpg .jpeg o .pdf de hasta 10 Mb. cada uno</label>
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
      <div class="col-md-3 col-centered" style="text-align: left">
          <asp:Button ID="btnEnviar" runat="server" Text="Enviar" CssClass="btn btn-success" />
          <asp:Button ID="btnCancelar" runat="server" class="btn btn-warning" Text="Cancelar" />
      </div>
      <div class="col-md-4 col-left">
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <br />
   <br />

  </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#aspnetForm").validate({
                rules: {
                    <%=txtSubject.UniqueID %>: {
                        required: true
                    },
                     <%=txtFrom.UniqueID %>: {                       
                        required: true,
                        email:true
                    },
                     <%=txtBody.UniqueID %>: {                       
                        required: true
                    }
                }, messages: {
                    <%=txtSubject.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtFrom.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        email: "<span  style='color:red'>Debe ingresar un correo válido</span>"
                    },
                    <%=txtBody.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    }
               }
            });
        });
    </script>
    <script type="text/javascript">
        $("#MenuContacto").addClass("active");
    </script>
</asp:Content>
