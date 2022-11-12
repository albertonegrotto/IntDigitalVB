<%@ Page Title="Actualiza CV Persona" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ActualizaCVPersona.aspx.vb" 
    Inherits="INTeatroDig.ActualizaCVPersona" Culture="es-AR" MaintainScrollPositionOnPostBack="True"%>

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
           <h2>Actualización del Currículum Vitae</h2>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/AdjuntosLista.aspx" CssClass="linksBold">Volver</asp:HyperLink>
      </div>
    </div>
     <br />
     <div class="row">
        <div class="col-md-2 col-left">
        </div>
        <div class="col-md-2 col-centered" style="text-align: center">
           <label>Currículum Vitae: </label>
        </div>
        <div class="col-md-3 col-left">
            <asp:FileUpload ID="UploadImporta" runat="server" CssClass="form-control" onchange="EvalDocumento()"/>
            <asp:Label ID="Label7" runat="server" Text="Puede adjuntar un archivo en formato .doc .docx o .pdf de hasta 10 Mb." CssClass="TextoComentario"></asp:Label>
        </div>
        <div class="col-md-2 col-left">
            <asp:Label ID="LabelNombreUpload" runat="server" CssClass="form-control" Height="75px"></asp:Label>
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
        <div class="col-md-10 col-centered" style="text-align: center">
           <asp:Label ID="FailureText" runat="server" CssClass="text-danger"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <br />
     <br />
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-10 col-centered" style="text-align: center">
            <asp:Button ID="BtnEnviar" runat="server" Text="Confirmar Actualización" CssClass="btn btn-success" /> 
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="btn btn-warning" />
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <br />
     <br />

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
