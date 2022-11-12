<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="CambioResponsable.aspx.vb" Inherits="INTeatroDig.CambioResponsable"
    Title="Página sin título" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="style.css" />
    <link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css' />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 <div id="Datos" runat="server" class="container-fluid">

    <div class="row">
       <div class="col-md-1 col-left">
       </div>
       <div class="col-md-6 col-centered" style="text-align: center">
           <h2>Cambio de Responsable</h2>
       </div>
       <div class="col-md-4 col-left">
           <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/menuFinal.aspx"
              CssClass="linksBold">Volver</asp:HyperLink>
       </div>
       <div class="col-md-1 text-right">
       </div>
    </div>

    <div id="CambioResponsable" class="preguntaR" style="margin: 20px">
        Para realizar un cambio de responsable, se deberán seguir los siguientes pasos:<br />
        1.- El nuevo responsable debe realizar el Alta de Datos Individual<br />
        2.- El actual responsable y el nuevo responsable deben redactar una nota donde soliciten el cambio, aclarando que el nuevo responsable ya realizó su Alta de Datos Individual, y donde figuren los siguientes datos:<br />
        <ul style="list-style-type: disc; list-style-position: inside; margin-left: 15px">
            <li><b>Nombre de la entidad</b></li>
            <li><b>Nº de Registro</b></li>
            <li><b>Nombre y CUIL/CUIT del actual responsable</b></li>
            <li><b>Nombre y CUIL/CUIT del nuevo responsable</b></li>
        </ul>
        3.- El actual responsable, el nuevo responsable y los demás integrantes cuando corresponda, deben firmar la nota (firma y aclaración).<br />
        4.- El nuevo responsable, desde su casilla de correo electrónico – la declarada en su Alta de Datos Individual – debe enviar scaneado la nota y copia de su DNI a info.intdigital@inteatro.gob.ar.<br />
        5.- El nuevo responsable recibirá un mail a la casilla de correo declarada, con los pasos a seguir para continuar con la tramitación pertinente.<br />

        Importante: El actual responsable y los demás integrantes que firmen la nota, deben coincidir en un 100 % con lo declarado en el actual Registro. De no ser así, no se autorizará el cambio de responsable solicitado.
  
    </div>
  </div>

</asp:Content>
