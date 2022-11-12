<%@ Page Title="Actualización de Adjuntos de Salas" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="ActuDocSala.aspx.vb"
    Inherits="INTeatroDig.ActuDocSala" MaintainScrollPositionOnPostBack="True" %>

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
             <h2>Actualización Documentación Salas o Espacios Teatrales</h2>
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
      <div id="DivTablaPeritaje" runat="server" class="container-fluid">
	   <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <label>Documentación para el Peritaje</label>
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
          </div>
          <div class="col-md-1 text-right">
          </div>                
       </div>
       <div class="row" id="Fotos" runat="server">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fotos : </label>
          </div>
          <div class="col-md-3 col-left">
             <asp:FileUpload ID="UploadImportaFotos" runat="server" CssClass="form-control"/>
             <asp:Label ID="Label9" runat="server" Text="Puede adjuntar archivos en formato .jpg o .jpeg de hasta 2 Mb. cada uno en calidad de 150 DPI de: fachada, ingreso, boletería, sala de espera, platea, escenario, camarines,
                   sanitarios, cabina técnica y parrilla, espacios de apoyo, salas auxiliares y espacios de alojamiento para elencos. Las fotos deberán estar bien iluminadas y encuadradas para poder apreciar los espacios en su totalidad
                   y de la manera más clara posible. Esta documentación será utilizada por el cuerpo de Peritos de Salas del INT" CssClass="TextoComentario"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Button ID="BtnVisualizaFotos" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
          </div>
          <div class="col-md-2 col-left">
             <asp:Button ID="BtnAgregaFotos" runat="server" Text="Agregar" CssClass="btn btn-success"  /><br /> 
             <asp:Label ID="LblErrorFotos" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 col-left">
          </div>
          <br />
       </div>
       <div class="row">
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-5 col-left" style="text-align: center">
              <asp:GridView ID="GridView2" runat="server" 
                  AllowSorting="True" AutoGenerateColumns="False"
                  CssClass="table table-striped"
                  DataKeyNames="identidad">
                  <Columns>
                    <asp:BoundField DataField="identidad" HeaderText=" " 
                        SortExpression="identidad" ReadOnly="True"/>
                    <asp:BoundField DataField="documento" HeaderText="Foto" 
                        SortExpression="documento" ReadOnly="True" />
                    <asp:BoundField DataField="filepath" HeaderText="filepath" 
                        SortExpression="filepath" ReadOnly="True" visible="false">
                       <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False">
                      <ItemTemplate>
                         <asp:Button ID="BtnBorrar" runat="server" CausesValidation="false" CommandName="" CssClass="btn btn-danger"
                             onclick="Borra_Foto_Click_Event" Text="Borrar"/>
                      </ItemTemplate>
                    </asp:TemplateField>
                  </Columns>
              </asp:GridView>
          </div>
          <div class="col-md-4 col-left">
          </div>
      </div>
      <br />
      <div class="row" id="Planos" runat="server">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Planos : </label>
          </div>
          <div class="col-md-3 col-left">
             <asp:FileUpload ID="UploadImportaPlanos" runat="server" CssClass="form-control"/>
             <asp:Label ID="Label10" runat="server" Text="Puede adjuntar archivos en formato .jpg, .jpeg o .pdf de hasta 10 Mb. cada uno de: “Planta” (todo el edificio) y “Cortes” (los necesarios), principalmente uno que atraviese
                   platea y escenario. Esta documentación será utilizada por el cuerpo de Peritos de Salas del INT" CssClass="TextoComentario"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Button ID="BtnVisualizaPlanos" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
          </div>
          <div class="col-md-2 col-left">
             <asp:Button ID="BtnAgregaPlanos" runat="server" Text="Agregar" CssClass="btn btn-success"  /><br /> 
             <asp:Label ID="LblErrorPlanos" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 col-left">
          </div>
         <br />
       </div>
       <div class="row">
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-5 col-left" style="text-align: center">
              <asp:GridView ID="GridView3" runat="server" 
                  AllowSorting="True" AutoGenerateColumns="False"
                  CssClass="table table-striped"
                  DataKeyNames="identidad">
                  <Columns>
                    <asp:BoundField DataField="identidad" HeaderText=" " 
                        SortExpression="identidad" ReadOnly="True"/>
                    <asp:BoundField DataField="documento" HeaderText="Plano" 
                        SortExpression="documento" ReadOnly="True" />
                    <asp:BoundField DataField="filepath" HeaderText="filepath" 
                        SortExpression="filepath" ReadOnly="True" visible="false">
                       <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="False">
                      <ItemTemplate>
                         <asp:Button ID="BtnBorrar" runat="server" CausesValidation="false" CommandName="" CssClass="btn btn-danger"
                             onclick="Borra_Plano_Click_Event" Text="Borrar"/>
                      </ItemTemplate>
                    </asp:TemplateField>
                  </Columns>
              </asp:GridView>
          </div>
          <div class="col-md-4 col-left">
          </div>
        </div>
      </div>
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
      <div class="row">
        <div class="col-md-4 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: left">
            <label>Espacios</label>
        </div>
        <div class="col-md-2 text-right">
        </div>
        <div class="col-md-2 text-right">
        </div>
        <div class="col-md-1 text-right">
        </div>
      </div>
      <div id="TablaEspacio1" runat="server" class="container-fluid">
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-2 col-centered" style="text-align: left">
                <label>Espacio 1</label>
            </div>
            <div class="col-md-4 text-right">
                <asp:Label ID="LblErrorEspacio1" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row" id="PlanoEscena1" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Plano Escénico : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadPlanoEscena1" runat="server" CssClass="form-control" onchange="EvalPlanoEscena1()"/>
               <asp:Label ID="Label15" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg o .pdf de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelPlanoEscena1" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnPlanoEscena1" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>
         <div class="row" id="PlantaLuz1" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Planta de Luz : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadPlantaLuz1" runat="server" CssClass="form-control" onchange="EvalplantaLuz1()"/>
               <asp:Label ID="Label17" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg o .pdf de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelPlantaLuz1" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnPlantaLuz1" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>
         <div class="row" id="FotoPlanoEscena1" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Foto del Plano Escénico : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadFotoEscena1" runat="server" CssClass="form-control" onchange="EvalFotoEscena1()"/>
               <asp:Label ID="Label16" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelFotoEscena1" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnFotoPlanoEscena1" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>
       </div>
       <br />
       <div id="TablaEspacio2" runat="server" class="container-fluid">
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-2 col-centered" style="text-align: left">
                <label>Espacio 2</label>
            </div>
            <div class="col-md-4 text-right">
                <asp:Label ID="LblErrorEspacio2" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row" id="PlanoEscena2" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Plano Escénico : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadPlanoEscena2" runat="server" CssClass="form-control" onchange="EvalPlanoEscena2()"/>
               <asp:Label ID="Label12" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg o .pdf de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelPlanoEscena2" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnPlanoEscena2" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>
         <div class="row" id="PlantaLuz2" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Planta de Luz : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadPlantaLuz2" runat="server" CssClass="form-control" onchange="EvalplantaLuz2()"/>
               <asp:Label ID="Label20" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg o .pdf de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelPlantaLuz2" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnPlantaLuz2" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>
         <div class="row" id="FotoPlanoEscena2" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Foto del Plano Escénico : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadFotoEscena2" runat="server" CssClass="form-control" onchange="EvalFotoEscena2()"/>
               <asp:Label ID="Label22" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelFotoEscena2" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnFotoPlanoEscena2" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>     
       </div>
       <br /> 
       <div id="TablaEspacio3" runat="server" class="container-fluid">
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-2 col-centered" style="text-align: left">
                <label>Espacio 3</label>
            </div>
            <div class="col-md-4 text-right">
                <asp:Label ID="LblErrorEspacio3" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row" id="PlanoEscena3" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Plano Escénico : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadPlanoEscena3" runat="server" CssClass="form-control" onchange="EvalPlanoEscena3()"/>
               <asp:Label ID="Label19" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg o .pdf de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelPlanoEscena3" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnPlanoEscena3" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>
         <div class="row" id="PlantaLuz3" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Planta de Luz : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadPlantaLuz3" runat="server" CssClass="form-control" onchange="EvalplantaLuz3()"/>
               <asp:Label ID="Label23" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg o .pdf de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelPlantaLuz3" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnPlantaLuz3" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>
         <div class="row" id="FotoPlanoEscena3" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Foto del Plano Escénico : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadFotoEscena3" runat="server" CssClass="form-control" onchange="EvalFotoEscena3()"/>
               <asp:Label ID="Label25" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelFotoEscena3" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnFotoPlanoEscena3" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
         </div>
      </div>
      <br /> 
      <div id="TablaEspacio4" runat="server" class="container-fluid">
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-2 col-centered" style="text-align: left">
                <label>Espacio 4</label>
            </div>
            <div class="col-md-4 text-right">
                <asp:Label ID="LblErrorEspacio4" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row" id="PlanoEscena4" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Plano Escénico : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadPlanoEscena4" runat="server" CssClass="form-control" onchange="EvalPlanoEscena4()"/>
               <asp:Label ID="Label21" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg o .pdf de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelPlanoEscena4" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnPlanoEscena4" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
            <br />
         </div>
         <div class="row" id="PlantaLuz4" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Planta de Luz : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadPlantaLuz4" runat="server" CssClass="form-control" onchange="EvalplantaLuz4()"/>
               <asp:Label ID="Label26" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg o .pdf de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelPlantaLuz4" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnPlantaLuz4" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
            <br />
         </div>
         <div class="row" id="FotoPlanoEscena4" runat="server">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Foto del Plano Escénico : </label>
            </div>
            <div class="col-md-3 col-left">
               <asp:FileUpload ID="FileUploadFotoEscena4" runat="server" CssClass="form-control" onchange="EvalFotoEscena4()"/>
               <asp:Label ID="Label28" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jpeg de hasta 10 Mb" CssClass="TextoComentario"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Label ID="LabelFotoEscena4" runat="server" CssClass="form-control" Height="50px"></asp:Label>
            </div>
            <div class="col-md-2 col-left">
               <asp:Button ID="BtnFotoPlanoEscena4" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
            </div>
            <div class="col-md-1 col-left">
            </div>
            <br /> 
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
        function EvalPlanoEscena1() {
          var sfile =document.getElementById("<%=FileUploadPlanoEscena1.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelPlanoEscena1.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalPlanoEscena12) {
          var sfile =document.getElementById("<%=FileUploadPlanoEscena2.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelPlanoEscena2.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalPlanoEscena3() {
          var sfile =document.getElementById("<%=FileUploadPlanoEscena3.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelPlanoEscena3.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalPlanoEscena4() {
          var sfile =document.getElementById("<%=FileUploadPlanoEscena4.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelPlanoEscena4.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalplantaLuz1() {
          var sfile =document.getElementById("<%=FileUploadPlantaLuz1.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelPlantaLuz1.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalplantaLuz2() {
          var sfile =document.getElementById("<%=FileUploadPlantaLuz2.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelPlantaLuz2.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalplantaLuz3() {
          var sfile =document.getElementById("<%=FileUploadPlantaLuz3.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelPlantaLuz3.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalplantaLuz4() {
          var sfile =document.getElementById("<%=FileUploadPlantaLuz4.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelPlantaLuz4.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalFotoEscena1() {
          var sfile =document.getElementById("<%=FileUploadFotoEscena1.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelFotoEscena1.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>


    <script type="text/javascript">
        function EvalFotoEscena2() {
          var sfile =document.getElementById("<%=FileUploadFotoEscena2.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelFotoEscena2.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalFotoEscena3() {
          var sfile =document.getElementById("<%=FileUploadFotoEscena3.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelFotoEscena3.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

    <script type="text/javascript">
        function EvalFotoEscena4() {
          var sfile =document.getElementById("<%=FileUploadFotoEscena4.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelFotoEscena4.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

</asp:Content>
