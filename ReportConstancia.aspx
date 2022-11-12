<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ReportConstancia.aspx.vb" Inherits="INTeatroDig.ReportConstancia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

 <div id="tablaDatos" runat="server" class="container-fluid">
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
         <h2>Impresión de Constancia en el Registro Nacional del Teatro Independiente</h2>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
         <h3>Para la Impresión de la Constancia :</h3>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-7 col-left" style="text-align: left">
         <h4>I. Presionar el botón Constancia</h4>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-3 col-left">
      </div>
      <div class="col-md-7 col-left" style="text-align: left">
         <h4>II. Seguidamente la Planilla se generará en archivo .pdf por lo que para su visualización</h4>
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
      <div class="col-md-3 col-center">
      </div>
      <div class="col-md-7 col-left" style="text-align: left">
         <h4>III. Al obtener la previsualización del formulario 
               la impresión final se hace con el botón Constancia</h4>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <br />
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-center" style="text-align: center">
          <asp:Button ID="BtnConstancia" runat="server" Text="Constancia" CssClass="btn btn-warning" />
      </div>
      <div class="col-md-2 col-left">
      </div>
   </div>  
   <br />
   <div class="row">
      <div class="col-md-2 col-center">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
          <asp:Label ID="LabelIntegrantes" runat="server" Text="Integrantes sin Verificar" CssClass="text-danger" Font-Bold="True"></asp:Label>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-center">
         <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered text-danger" CellPadding="2" AutoGenerateColumns="False">
            <Columns>
              <asp:BoundField DataField="cuil" HeaderText="CUIL" InsertVisible="False" ReadOnly="True" SortExpression="cuil"/>
              <asp:BoundField DataField="apellido" HeaderText="Apellido" SortExpression="apellido" />
              <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
            </Columns>
         </asp:GridView>
      </div>
      <div class="col-md-2 col-left">
      </div>
   </div>  
   <br />     
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-7 col-left">
      </div>
      <div class="col-md-3 col-left">
         <asp:Button ID="BtonVolver" runat="server" Text="Volver" CssClass="btn btn-primary" />
      </div>
      <div class="col-md-1 col-left">
      </div>
   </div>  
   <br />
   <br />
   <br />

 </div>

</asp:Content>
