<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ActuDocGrupo.aspx.vb" 
    Inherits="INTeatroDig.ActuDocGrupo" MaintainScrollPositionOnPostBack="True" %>

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
             <h2>Actualización Documentación Grupos de Teatro</h2>
         </div>
         <div class="col-md-2 text-left">
             <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/AdjuntosLista.aspx" CssClass="linksBold">Volver</asp:HyperLink>
         </div>
      </div>
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <h2 id="LblDenominacion" runat="server"></h2>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
          </div>
      </div>
      <br />
      <br />
      <div id="DivTablaEquipamiento" runat="server" class="container-fluid">
        <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Listado Total del Equipamiento : </label>
          </div>
          <div class="col-md-3 col-left">
             <asp:FileUpload ID="FileUploadEquipa" runat="server" CssClass="form-control" onchange="EvalEquipa()"/>
             <asp:Label ID="Label11" runat="server" Text="Debe adjuntar un archivo en formato .doc .docx o .pdf de hasta 10 Mb. que incluya la descripción de todo el equipamiento técnico de la Sala, detallando Luz, Sonido y
                     Climatización prioritariamente" CssClass="TextoComentario"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Label ID="LabelUploadEquipa" runat="server" CssClass="form-control" Height="50px"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Button ID="BtnVisualizaEquipa" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
         <div class="col-md-4 col-left">
         </div>
         <div class="col-md-7 col-centered page-institucional" style="text-align: center">
            <asp:Label ID="txtErrorEquipamiento" runat="server" CssClass="text-danger"></asp:Label>
         </div>
         <div class="col-md-1 text-left">
         </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Equipamiento Adquirido con Subsidios del INT : </label>
          </div>
          <div class="col-md-3 col-left">
             <asp:FileUpload ID="FileUploadEquipaSub" runat="server" CssClass="form-control" onchange="EvalEquipaSub()"/>
             <asp:Label ID="Label13" runat="server" Text="Puede adjuntar un archivo en formato .doc .docx o .pdf de hasta 10 Mb. que incluya la descripción del equipamiento de Luz, Sonido o Climatización
                 adquirido por la Sala con Subsidios del INT y el año de adquisición de cada uno" CssClass="TextoComentario"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Label ID="LabelUploadEquipaSub" runat="server" CssClass="form-control" Height="50px"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Button ID="BtnVisualizaEquipaSub" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <br />
      </div>      
      <div id="DivTablaTrayectoria" runat="server" class="container-fluid">
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Trayectoria del Grupo : </label>
          </div>
          <div class="col-md-3 col-left">
             <asp:FileUpload ID="FileUploadTrayectoria" runat="server" CssClass="form-control" onchange="EvalTrayectoria()"/>
             <asp:Label ID="Label3" runat="server" Text="Puede adjuntar un archivo en formato .doc .docx o .pdf de hasta 10 Mb. que incluya la descripción de espectáculos, premios, menciones, notas de prensa, etc."
                 CssClass="TextoComentario"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Label ID="LabelUploadTrayectoria" runat="server" CssClass="form-control" Height="50px"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Button ID="BtnVisualizaTrayectoria" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
        <div class="row">
         <div class="col-md-2 col-left">
         </div>
         <div class="col-md-8 col-centered" style="text-align: center">
            <asp:Label ID="txtErrorTrayectoria" runat="server" CssClass="text-danger"></asp:Label>
         </div>
         <div class="col-md-2 text-right">
         </div>
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
        function EvalEquipa() {
          var sfile =document.getElementById("<%=FileUploadEquipa.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelUploadEquipa.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalEquipaSub() {
          var sfile =document.getElementById("<%=FileUploadEquipaSub.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelUploadEquipaSub.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalTrayectoria() {
          var sfile =document.getElementById("<%=FileUploadTrayectoria.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelUploadTrayectoria.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>


</asp:Content>
