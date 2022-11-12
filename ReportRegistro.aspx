<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ReportRegistro.aspx.vb" 
     Inherits ="INTeatroDig.ReportRegistro" Title="Registros" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

 <div id="tablaDatos" runat="server" class="container-fluid">
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
         <h2>Impresión de Planillas de Confirmación de Datos</h2>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
         <h3>Para la Impresión de las Planillas :</h3>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-7 col-left" style="text-align: left">
         <h4>Se generarán en archivo .pdf , por lo tanto para su visualización </h4>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-4 col-left" style="text-align: left">
         <h4>deberá tener instalado el software Adobe Acrobat</h4>
      </div>
      <div class="col-md-3 col-left" style="text-align: left">
         <asp:HyperLink ID="HyperLink5" runat="server" CssClass="TextoComentario"
               NavigateUrl="http://get.adobe.com/es/reader/?no_ab=1">(Descarga del Software)</asp:HyperLink>
      </div>
      <div class="col-md-2 col-left">
      </div>
   </div>     
   <div class="row">
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-7 col-left" style="text-align: left">
         <asp:Label ID="lblAclaracion1" runat="server"></asp:Label>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-left" style="text-align: left">
      </div>
      <div class="col-md-3 col-left" style="text-align: left">
         <img id="imgPointingHand1" runat="server" alt="Click ahí" src="images/pointingHand.gif" width="15" height="22" border="0" />
      </div>
      <div class="col-md-4 col-left">
      </div>
      <div class="col-md-1 col-left">
      </div>
   </div>  
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-left" style="text-align: left">
      </div>
      <div class="col-md-3 col-left" style="text-align: left">
          <asp:Button ID="BtnRegistro" runat="server" Text="Responsable" CssClass="btn btn-warning" />
      </div>
      <div class="col-md-4 col-left">
      </div>
      <div class="col-md-1 col-left">
      </div>
   </div>  
   <div class="row">
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-7 col-left" style="text-align: left">
         <asp:Label ID="lblAclaracion2" runat="server"></asp:Label>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-3 col-left">
         <img id="imgPointingHand2" runat="server" alt="Click ahí" src="images/pointingHand.gif" width="15" height="22" border="0" />
      </div>
      <div class="col-md-4 col-left">
      </div>
      <div class="col-md-1 col-left">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-3 col-left">
          <asp:Button ID="BtnIntegrantes" runat="server" Text="Integrantes" CssClass="btn btn-warning" />
      </div>
      <div class="col-md-4 col-left">
      </div>
      <div class="col-md-1 col-left">
      </div>
   </div>
   <br />
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-center" style="text-align: center">
          <asp:label runat="server" CssClass="TextoComentario">La generación de las planillas en formato .pdf puede demorar unos minutos, no cierre esta pantalla y aguarde hasta que se visualicen las planillas correspondientes, para su posterior impresión</asp:label>
      </div>
      <div class="col-md-2 col-left">
      </div>
   </div> 
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-4 col-left">
          <asp:Button ID="BtonVolver" runat="server" Text="Volver" CssClass="btn btn-primary" />
      </div>
      <div class="col-md-1 col-left">
      </div>
   </div> 
   <br />
   <br />
   <br />
   <br />
   <br />
   <br />

 </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"
        EnableScriptLocalization="true"
        EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>

</asp:Content>
