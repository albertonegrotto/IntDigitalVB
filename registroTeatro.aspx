<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="registroTeatro.aspx.vb" Inherits="INTeatroDig.registroTeatro" 
    title="Registro Teatro Independiente" Culture="es-AR" MaintainScrollPositionOnPostBack="True"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Registro de Sala o Espacio Teatral</h2>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/menuFinal.aspx" CssClass="linksBold">Volver</asp:HyperLink>
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-10 col-centered Texto1" style="text-align: center">
           Declaro CONOCER y ACEPTAR en su totalidad los términos y condiciones de la reglamentación vigente, la<br />
           <asp:HyperLink ID="HyperLink3" runat="server" 
                NavigateUrl="http://inteatro.gob.ar/intdigital/reglamentacion_registro.htm" CssClass="links" Target="_blank">
                Reglamentación del Registro Nacional del Teatro Independiente
           </asp:HyperLink>
            , la 
           <asp:HyperLink ID="HyperLink4" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/40000-44999/42762/norma.htm" CssClass="links" Target="_blank">
                Ley Nº 24.800
           </asp:HyperLink>
           y su 
           <asp:HyperLink ID="HyperLink5" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/45000-49999/46047/norma.htm" CssClass="links" Target="_blank">
                    Decreto Reglamentario Nº 991/97
           </asp:HyperLink><br />
           <asp:CheckBox ID="AceptoDJ" runat="server" Text="Acepto" AutoPostBack="True" /> &nbsp;&nbsp;<asp:Label ID="Label6" runat="server" style="color: red" Text="[*]"></asp:Label>           
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
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
              <label>Tipo de Registro:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="ddlSectores" runat="server" Enabled ="false" CssClass="form-control"></asp:DropDownList>
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
              <label>Denominación completa: </label>
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
              <asp:TextBox ID="txtDenominacion" runat="server" MaxLength="200" Columns="40" CssClass="form-control"
                ToolTip="Ingrese la Denominación de la Sala o Espacio Teatral" onkeypress="return validate(event)"></asp:TextBox>
          </div>
          <div class="col-md-3 col-left">
             <asp:Label ID="Label24" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
      <div class="row">
          <div class="col-md-4 text-right">
          </div>
          <div class="col-md-7 col-center">
              <asp:label ID="Label2" runat="server" CssClass="TextoComentario">No se permite consignar letras acentuadas ni símbolos ni caracteres especiale</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Provincia:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:DropDownList ID="ddlProvincias" runat="server" CssClass="form-control"
                    AutoPostBack="True"></asp:DropDownList>
                <asp:Label ID="txtErrorProvincia1" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label27" runat="server" style="color: red" Text="[*]"></asp:Label>
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
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="ddlLocalidades" runat="server" CssClass="form-control" ></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label29" runat="server" style="color: red" Text="[*]"></asp:Label>
              <asp:Label ID="txtErrorLocalidades" runat="server" CssClass="text-danger"></asp:Label>            
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Código Postal:</label>
          </div>
          <div class="col-md-1 col-centered" style="text-align: left">
              <asp:TextBox ID="txtCP" runat="server" MaxLength="4" Columns="4" CssClass="form-control"
                       ToolTip="Ingrese el Código Postal"></asp:TextBox>
          </div>
          <div class="col-md-6 col-left">
              <asp:Label ID="Label30" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Domicilio de la Sala:</label>
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
              <asp:TextBox ID="txtDomicilio" runat="server" MaxLength="100" Columns="40" CssClass="form-control"
                ToolTip="Ingrese el domicilio de la sala"></asp:TextBox>
          </div>
          <div class="col-md-3 col-left">
              <asp:Label ID="Label31" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
      <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Teléfono: </label>
        </div>
        <div class="col-md-1 text-center" style="width: 30px">
            <label for="txtPrefijo1">0</label> 
        </div>
        <div class="col-md-6 col-centered" style="text-align: left"> 
            &nbsp;
            <asp:TextBox ID="txtPrefijo" placeholder="Prefijo" runat="server" CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="txtNumero" placeholder="Teléfono" runat="server" Width="230px" MaxLength="8" CssClass="form-control pull-left"></asp:TextBox>
        </div>
        <div class="col-md-1 text-right">
        </div>
      </div>
      <div class="row">
          <div class="col-md-4 text-right">
          </div>
          <div class="col-md-7 col-center">
              <asp:label ID="Label3" runat="server" CssClass="TextoComentario">Completar únicamente si difiere del teléfono del responsable</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
      </div>
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Correo Electrónico:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtMail" runat="server" MaxLength="100" Columns="40" CssClass="form-control"
                  ToolTip="Ingrese el Correo electrónico"></asp:TextBox>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
      <div class="row">
          <div class="col-md-4 text-right">
          </div>
          <div class="col-md-7 col-center">
              <asp:label ID="Label18" runat="server" CssClass="TextoComentario">Completar únicamente si difiere del correo electrónico del responsable</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
      </div>
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Página web: (http://)</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtWeb" runat="server" MaxLength="100" Columns="40" CssClass="form-control"
                ToolTip="Ingrese la dirección de la página web"></asp:TextBox>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-center" style="text-align: center">
              <label>Fecha de Inauguraciónde la Sala:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker1" >
                    <input type="text" class="form-control" id="TextDesde" placeholder="Inicio" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaInicio" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-5 col-left">
              <asp:Label ID="Label32" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:label ID="Label53" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>              
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-center" style="text-align: center">
              <label>Fecha Inicio de Gestión Actual:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker2" >
                    <input type="text" class="form-control" id="TextGestion" placeholder="Gestion" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaGestion" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-5 col-left">
              <asp:Label ID="Label33" runat="server" style="color: red" Text="[*]"></asp:Label>
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
             <asp:label ID="Label52" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>              
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
      <div class="row">
          <div class="col-md-4 text-right">
          </div>
          <div class="col-md-7 col-center">
              <asp:label ID="Label7" runat="server" CssClass="TextoComentario">Completar con la fecha en que el Responsable ACTUAL comenzó a gestionar la sala</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
      </div>
      <br />
      <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
           <label>¿La sala de teatro ha funcionado en otro edificio previamente?: </label>
        </div>
        <div class="col-md-2 col-centered" style="text-align: left">
              <asp:RadioButton ID="RadioButtonEdificio1" runat="server" Text="Si" GroupName="grupoEdificio" CssClass="form-control" Width="200px" onchange="Mudanza()"/>
              <asp:RadioButton ID="RadioButtonEdificio2" runat="server" Text="No" Checked="false" GroupName="grupoEdificio" CssClass="form-control" Width="200px" onchange="NoMudanza()"/>
              <asp:Label ID="txtErrorAnterior" runat="server" CssClass="text-danger"></asp:Label>
        </div>
        <div class="col-md-5 text-left">
            <asp:Label ID="Label34" runat="server" style="color: red" Text="[*]"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
      </div>
      <div id="DivTablaMudanza" runat="server" class="container-fluid">
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Año de Mudanza:</label>
          </div>
          <div class="col-md-1 col-centered" style="text-align: left">
              <asp:TextBox ID="TextBoxAnioMudanza" runat="server" MaxLength="4" Columns="3" CssClass="form-control" onkeypress="return validatenumber(event)"></asp:TextBox>
          </div>
          <div class="col-md-6 col-left">
             <asp:Label ID="Label8" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Domicilio Anterior:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="TextBoxDomiAnterior" runat="server" MaxLength="100" Columns="40" CssClass="form-control"
                ToolTip="Ingrese el domicilio anterior de la sala"></asp:TextBox>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
      </div>
      <br />
	  <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Situación de Uso:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="ddlEspacios" runat="server" CssClass="form-control" ></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label35" runat="server" style="color: red" Text="[*]"></asp:Label>
              <asp:Label ID="TxtErrorEspacios" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
	  <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Cuenta con la documentación pertinente vigente para funcionar como sala o espacio teatral.</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="DdlDocumentacion" runat="server" CssClass="form-control" ></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label36" runat="server" style="color: red" Text="[*]"></asp:Label>
              <asp:Label ID="txtErrorDocumentacion" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
      <div id="DivTablaPeritaje" runat="server" class="container-fluid">
	   <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <label>Documentación para el Peritaje</label>
              <asp:Label ID="txtErrorEspacioEscenico" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
              <asp:TextBox ID="txtEspacioEscenico" runat="server" Visible="false"  
                MaxLength="200" Columns="35" TextMode="MultiLine" Rows="4" CssClass="form-control"
                ToolTip="Ingrese una descripción de las características del espacio escénico" ></asp:TextBox>
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
          <br />
      </div>
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
        <br />
      </div>
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>¿Cuenta con Equipamiento Técnico?”.</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <asp:TextBox ID="txtEquipamiento" runat="server" visible="false" MaxLength="200" Columns="35" TextMode="MultiLine" Rows="4" CssClass="form-control" ></asp:TextBox>
              <asp:RadioButton ID="RadioButtonEquipa1" runat="server" Text="Si" GroupName="grupoEquipa" CssClass="form-control" Width="200px" onchange="TieneEquipo()" />
              <asp:RadioButton ID="RadioButtonEquipa2" runat="server" Text="No" Checked="false" GroupName="grupoEquipa" CssClass="form-control" Width="200px" onchange="NoTieneEquipo()"/>
          </div>
          <div class="col-md-5 col-left">
              <asp:Label ID="Label37" runat="server" style="color: red" Text="[*]"></asp:Label>
              <asp:Label ID="txtErrorEquipamiento" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
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
              <asp:TextBox ID="txtLocalidades" runat="server" MaxLength="3" Columns="3" CssClass="form-control" Visible="false"
                ToolTip="Cantidad de localidades, máximo 300"></asp:TextBox>
          </div>
          <div class="col-md-2 text-right">
              <asp:TextBox ID="txtComentariosEspacio" runat="server" 
                MaxLength="200" Columns="35" TextMode="MultiLine" Rows="4" CssClass="form-control" Visible="false"
                ToolTip="Ingrese una descripción del tipo de espacio" ></asp:TextBox>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Cantidad de Espacios (Sala principal + anexos)</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
             <asp:DropDownList ID="DdlCantidadEspacios" runat="server" CssClass="form-control" onchange="EvalEspacios()"></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label38" runat="server" style="color: red" Text="[*]"></asp:Label>
              <asp:Label ID="txtErrorCantidadLocalidades" runat="server" CssClass="text-danger"></asp:Label>
              <asp:Label ID="txtErrorComentariosEspacio" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div id="TablaEspacio1" runat="server" class="container-fluid">
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-7 col-centered" style="text-align: left">
                <label>Espacio 1</label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-7 col-centered" style="text-align: left">
               <asp:Label ID="LblErrorEspacio1" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Tipo de sala:</label>
            </div>
            <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="DdlTipoSala1" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
            <div class="col-md-4 col-left">
                <asp:Label ID="Label39" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Cantidad de Espectadores:</label>
            </div>
            <div class="col-md-1 col-centered" style="text-align: left">
                 <asp:TextBox ID="TextBoxCant1" runat="server" MaxLength="3" Columns="3" CssClass="form-control" onkeypress="return validatenumber(event)"></asp:TextBox>
            </div>
            <div class="col-md-6 col-left">
               <asp:Label ID="Label40" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Tipo de Asientos:</label>
            </div>
            <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="ddlTipoasien1" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
            <div class="col-md-4 col-left">
               <asp:Label ID="Label41" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
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
            <br />
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
           <br />
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
            <br /> 
         </div>
       </div>
       <div id="TablaEspacio2" runat="server" class="container-fluid">
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-7 col-centered" style="text-align: left">
                <label>Espacio 2</label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-7 col-centered" style="text-align: left">
               <asp:Label ID="LblErrorEspacio2" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Tipo de sala:</label>
            </div>
            <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="DdlTipoSala2" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
            <div class="col-md-4 col-left">
               <asp:Label ID="Label42" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Cantidad de Espectadores:</label>
            </div>
            <div class="col-md-1 col-centered" style="text-align: left">
                 <asp:TextBox ID="TextBoxCant2" runat="server" MaxLength="3" Columns="3" CssClass="form-control" onkeypress="return validatenumber(event)"></asp:TextBox>
            </div>
            <div class="col-md-6 col-left">
               <asp:Label ID="Label43" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Tipo de Asientos:</label>
            </div>
            <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="ddlTipoasien2" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
            <div class="col-md-4 col-left">
               <asp:Label ID="Label44" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
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
            <br />
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
            <br />
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
           <br /> 
         </div>
       </div>
       <div id="TablaEspacio3" runat="server" class="container-fluid">
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-7 col-centered" style="text-align: left">
                <label>Espacio 3</label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-7 col-centered" style="text-align: left">
               <asp:Label ID="LblErrorEspacio3" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Tipo de sala:</label>
            </div>
            <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="DdlTipoSala3" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
            <div class="col-md-4 col-left">
               <asp:Label ID="Label45" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Cantidad de Espectadores:</label>
            </div>
            <div class="col-md-1 col-centered" style="text-align: left">
                 <asp:TextBox ID="TextBoxCant3" runat="server" MaxLength="3" Columns="3" CssClass="form-control" onkeypress="return validatenumber(event)"></asp:TextBox>
            </div>
            <div class="col-md-6 col-left">
               <asp:Label ID="Label46" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Tipo de Asientos:</label>
            </div>
            <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="ddlTipoasien3" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
            <div class="col-md-4 col-left">
               <asp:Label ID="Label47" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
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
            <br />
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
            <br />
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
            <br /> 
         </div>
       </div>
       <div id="TablaEspacio4" runat="server" class="container-fluid">
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-7 col-centered" style="text-align: left">
                <label>Espacio 4</label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-4 col-left">
            </div>
            <div class="col-md-7 col-centered" style="text-align: left">
               <asp:Label ID="LblErrorEspacio4" runat="server" CssClass="text-danger"></asp:Label>
            </div>
            <div class="col-md-2 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Tipo de sala:</label>
            </div>
            <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="DdlTipoSala4" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
            <div class="col-md-4 col-left">
               <asp:Label ID="Label48" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Cantidad de Espectadores:</label>
            </div>
            <div class="col-md-1 col-centered" style="text-align: left">
                 <asp:TextBox ID="TextBoxCant4" runat="server" MaxLength="3" Columns="3" CssClass="form-control" onkeypress="return validatenumber(event)"></asp:TextBox>
            </div>
            <div class="col-md-6 col-left">
               <asp:Label ID="Label49" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
            </div>
         </div>
         <div class="row">
            <div class="col-md-1 col-left">
            </div>
            <div class="col-md-3 col-centered" style="text-align: center">
                <label>Tipo de Asientos:</label>
            </div>
            <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="ddlTipoasien4" runat="server" CssClass="form-control" ></asp:DropDownList>
            </div>
            <div class="col-md-4 col-left">
               <asp:Label ID="Label50" runat="server" style="color: red" Text="[*]"></asp:Label>
            </div>
            <div class="col-md-1 text-right">
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
       <div class="row">
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
              <label>Espacios Complementarios</label>
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
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">
              <asp:CheckBox ID="chkSalaEnsayo" runat="server" Visible="true" Checked="false"  Text="Sala de Ensayo" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="chkBarRestaurant" runat="server" Visible="true" Checked="false"  Text="Bar/Restaurant" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="chkGaleriaArte" runat="server" Visible="true" Checked="false"  Text="Galería de Arte" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="chkBiblioteca" runat="server" Visible="true" Checked="false"  Text="Biblioteca" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkOtros" runat="server" Visible="true" Checked="false"  Text="Otros" CssClass="form-control" Width="200px"/><br />
              <asp:TextBox ID="TextBoxObservaOtros" placeholder="Descripción" runat="server" CssClass="form-control pull-left" Width="200px" MaxLength="40"></asp:TextBox>
              <br /> 
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
              <label>Integrantes</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Integrante:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <asp:TextBox ID="txtIntegrante" runat="server" MaxLength="11" Columns="11" CssClass="form-control"
                 ToolTip="Ingrese el CUIL del integrante"></asp:TextBox>
          </div>
          <div class="col-md-1 col-left">
              <asp:Label ID="Label56" runat="server" style="color: white" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-5 col-left">
              <asp:Button ID="btnIntegrante" runat="server" Text="Agregar Integrante" CssClass="btn btn-primary" />
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-7 col-left">
              <asp:label ID="Label4" runat="server" CssClass="TextoComentario">(Ingrese el CUIL / CUIT de cada integrante y pulse el botón &quot;Agregar Integrante&quot;)</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <asp:Label ID="txtErrorIntegrante" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-9 col-centered" style="text-align: center; font-family:'Trebuchet MS'; font-size:small">
	            <!-- Gridview -->
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AllowPaging="True"
                          CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="idIntegrante">
                    <Columns>                  
                        <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" 
                            HeaderText="Id" DataField="idIntegrante" SortExpression="idIntegrante" 
                            ReadOnly="True" Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="22px"></ItemStyle>
                        </asp:BoundField>                       
                        <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" 
                            HeaderText="CUIL" DataField="cuit" SortExpression="cuit" visible="false" >
                            <ItemStyle HorizontalAlign="Center" Width="122px"></ItemStyle>
                        </asp:BoundField>                        
                        <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" 
                            HeaderText="Registro" DataField="codigoRegistro" 
                            SortExpression="codigoRegistro" visible="false" >
                            <ItemStyle HorizontalAlign="Center" Width="22px"></ItemStyle>
                        </asp:BoundField>                        
                        <asp:BoundField ItemStyle-Width="122" ItemStyle-HorizontalAlign="Center" 
                            HeaderText="CUIL" DataField="CUIL" SortExpression="CUIL" >
                            <ItemStyle HorizontalAlign="Center" Width="122px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField ItemStyle-Width="122" ItemStyle-HorizontalAlign="Center" 
                            HeaderText="Apellido y Nombre" DataField="apellidoynombre" SortExpression="apellidoynombre" >
                            <ItemStyle HorizontalAlign="Center" Width="122px"></ItemStyle>
                        </asp:BoundField>                        
                        <asp:TemplateField HeaderText="&nbsp;Actualización&nbsp;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"> 
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLinkModificar" runat="server" NavigateUrl="" CssClass="links">Actualización</asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:CommandField
                            ItemStyle-Width="160" ButtonType = "Image"  ShowEditButton = "False" ShowDeleteButton = "True"
                            DeleteImageUrl = "images/btnDesvincular.gif" DeleteText="Desvincular"
                            EditImageUrl = "images/btnEdit.gif" EditText="Edit"
                            UpdateImageUrl = "images/btnUpdate.gif" UpdateText="Update" 
                            CancelText = "Cancel" CancelImageUrl = "images/btnCancel.gif" 
                            ControlStyle-Font-Bold="True" >
                            <ControlStyle Font-Bold="True"></ControlStyle>
                            <ItemStyle Width="160px"></ItemStyle>
                        </asp:CommandField>                       
                    </Columns>
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>                    
                </asp:GridView>
                <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" ID="SqlDataSource1" runat="server"
                    SelectCommand = "SELECT I.idIntegrante AS idIntegrante,I.cuit AS cuit,I.codigoRegistro AS codigoRegistro,I.CUIL AS CUIL,
                                            LTrim(RTrim(R.apellido)) + ' ' + LTrim(RTrim(R.nombre)) + ' ' + LTrim(RTrim(R.Denominacion)) AS apellidoNombre
                                        FROM IntegrantesTemp I, RegisDig R WHERE I.cuil = R.cuil AND I.cuit = @cuit
                                        ORDER BY I.CUIL"
                    DeleteCommand = "DELETE FROM IntegrantesTemp WHERE idIntegrante = @idIntegrante"
                    CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter Name="cuit" Type="Decimal" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="idIntegrante" Type="Int32" />
                    </DeleteParameters>
                </asp:SqlDataSource>
	            <!-- End of Gridview -->
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">
              <label>Personas necesarias para llevar adelante la actividad de la Sala</label>
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">             
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
	  <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Cantidad:</label>
          </div>
          <div class="col-md-1 col-centered" style="text-align: left">
              <asp:TextBox ID="TextBoxPersonas" runat="server" MaxLength="2" Columns="3" CssClass="form-control" onkeypress="return validatenumber(event)"></asp:TextBox>
          </div>
          <div class="col-md-6 col-left">
              <asp:Label ID="Label14" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">
              <asp:Label ID="Label5" runat="server" Text="Además de los integrantes recién ingresados, ¿qué otras personas se requieren para el funcionamiento de la Sala?
                  (información optativa para fines estadísticos)" CssClass="TextoComentario"></asp:Label>
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">             
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
              <label>Tareas que Desempeñan</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">             
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
              <asp:CheckBox ID="ChkProduccion" runat="server" Visible="true" Checked="false"  Text="Producción" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkGestion" runat="server" Visible="true" Checked="false"  Text="Gestión" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkPrograma" runat="server" Visible="true" Checked="false"  Text="Programación" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkBoleteria" runat="server" Visible="true" Checked="false"  Text="Boletería" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkAsistencia" runat="server" Visible="true" Checked="false"  Text="Asistencia de Sala" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkLimpieza" runat="server" Visible="true" Checked="false"  Text="Limpieza" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkMantenimiento" runat="server" Visible="true" Checked="false"  Text="Mantenimiento" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkTecnica" runat="server" Visible="true" Checked="false"  Text="Técnica de Sala" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkControl" runat="server" Visible="true" Checked="false"  Text="Control de Ingreso" CssClass="form-control" Width="200px"/><br />
              <asp:CheckBox ID="ChkPersonasOtros" runat="server" Visible="true" Checked="false"  Text="Otros" CssClass="form-control" Width="200px"/><br />
              <asp:TextBox ID="TxtPersonasOtros" placeholder="Descripción" runat="server" CssClass="form-control pull-left" Width="200px" MaxLength="40"></asp:TextBox>
              <br /> 
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
             <asp:CheckBox ID="chkAcepto" runat="server" Text="Acepto" /> &nbsp;&nbsp;<asp:Label ID="Label51" runat="server" style="color: red" Text="[*]"></asp:Label> 
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
             <asp:Label ID="txtErrorAcepto" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:label CssClass="observacion" runat="server">Recibirá en la cuenta de correo electrónico consignada un mail conteniendo la información ingresada, el cual deberá validar siguiendo el link de verificación 
                que estará incluido en el mismo mensaje. Luego recibirá en esa misma cuenta de correo el mail de notificación de aceptación del trámite realizado, con las 
                debidas instrucciones para el envío posterior de la confirmación del trámite y demás material que correspondiera adjuntar.
             </asp:label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <h3>Datos del Responsable</h3>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
      
       <div id="DivTablaPalabras" runat="server" visible="false" class="container-fluid">
        <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-9 col-centered" style="text-align: center; font-family:'Trebuchet MS'; font-size:small">
               <!-- Gridview -->
               <asp:GridView ID="GridViewPalabras" runat="server" AllowSorting="True"  CssClass="table table-bordered"
                   AutoGenerateColumns="False" DataKeyNames="codigo" >
                   <Columns>
                       <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center"
                           HeaderText="Id" DataField="codigo" SortExpression="codigo"
                           ReadOnly="True" Visible="false">
                           <ItemStyle HorizontalAlign="Center" Width="22px"></ItemStyle>
                       </asp:BoundField>
                       <asp:BoundField ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center"
                           HeaderText="Denominación" DataField="apellidoNombre" SortExpression="apellidoNombre">
                           <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                       </asp:BoundField>
                       <asp:BoundField ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center"
                           HeaderText="Teléfonos" DataField="telefonos" SortExpression="telefonos">
                           <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                       </asp:BoundField>
                       <asp:BoundField ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center"
                           HeaderText="Correo Electrónico" DataField="email" SortExpression="email">
                           <ItemStyle HorizontalAlign="Center" Width="140px"></ItemStyle>
                       </asp:BoundField>
                   </Columns>
                   <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
               </asp:GridView>
               <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" ID="SqlDataSourcePalabras"
                        runat="server" SelectCommand="" CancelSelectOnNullParameter="false">
                   <SelectParameters>
                       <asp:Parameter Name="codigo" Type="Int32" />
                   </SelectParameters>
               </asp:SqlDataSource>
               <!-- End of Gridview -->
          </div>
          <div class="col-md-1 text-right">
          </div>
        </div>
        <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
                <asp:CheckBox ID="chkPalabra1" runat="server" Visible="false" Checked="false" Text="APELLIDO Y NOMBRE DEL RESPONSABLE" CssClass="form-control" Width="500px"  /><br />
                <asp:CheckBox ID="chkPalabra3" runat="server" Visible="false" Checked="false" Text="CANTIDAD DE LOCALIDADES" CssClass="form-control" Width="500px" /><br />
                <asp:CheckBox ID="chkPalabra4" runat="server" Visible="false" Checked="false" Text="CARACTERÍSTICAS DEL ESPACIO ESCÉNICO" CssClass="form-control" Width="500px" /><br />
                <asp:CheckBox ID="chkPalabra6" runat="server" Visible="false" Checked="false" Text="DESCRIPCIÓN DEL EQUIPAMIENTO TÉCNICO ACTUAL" CssClass="form-control" Width="500px" /><br />
                <asp:CheckBox ID="chkPalabra11" runat="server" Visible="false" Checked="false" Text="DIRECCIÓN DE CORREO ELECTRÓNICO DE LA SALA" CssClass="form-control" Width="500px" /><br />
                <asp:CheckBox ID="chkPalabra15" runat="server" Visible="false" Checked="false" Text="DIRECCIÓN DE CORREO ELECTRÓNICO DEL RESPONSABLE" CssClass="form-control" Width="500px" /><br />
                <asp:CheckBox ID="chkPalabra16" runat="server" Visible="false" Checked="false" Text="DOMICILIO DE LA SALA" CssClass="form-control" Width="500px" /><br />
                <asp:CheckBox ID="chkPalabra19" runat="server" Visible="false" Checked="false" Text="PAGINA WEB DE LA SALA" CssClass="form-control" Width="500px" /><br />
                <asp:CheckBox ID="chkPalabra25" runat="server" Visible="false" Checked="false" Text="TELEFONO DE LA SALA" CssClass="form-control" Width="500px" /><br />
                <asp:CheckBox ID="chkPalabra27" runat="server" Visible="false" Checked="false" Text="TELEFONO/S DEL RESPONSABLE" CssClass="form-control" Width="500px" /><br />
          </div>
          <div class="col-md-1 text-right">
          </div>
        </div>
       </div>
        <!-- Palabras a publicar -->
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:label CssClass="observacion" runat="server">Autorizo al Instituto Nacional del Teatro a que los datos consignados sean de acceso público (en virtud de lo establecido por la Ley
                               25.326 de Protección de Datos Personales) y sirvan de utilidad para uso estadístico y para la implementación de políticas públicas para el sector cultural</asp:label>
             <br />
             <asp:CheckBox ID="checkAutorizoPublicar" runat="server" Text="Acepto" Visible="True" /> &nbsp;&nbsp;<asp:Label ID="Label55" runat="server" style="color: red" Text="[*]"></asp:Label>
             <asp:Label ID="lblErrorcheckAutorizoPublicar" runat="server" CssClass="text-danger"></asp:Label>
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
              <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" /> 
              <asp:Button ID="btnCancelar" runat="server" class="btn btn-warning" Text="Cancelar" /> &nbsp;&nbsp;<asp:Label ID="Label54" runat="server" style="color: red" Text="[*]"></asp:Label>   
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <br />

  </div>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.0/css/bootstrap-datepicker.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.0/js/bootstrap-datepicker.min.js"></script>
    <script src="Scripts/bootstrap-datepicker.es.js" ></script>


    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                $('#datetimepicker1').datepicker({
                    language: 'es',
                    autoclose: true,
                    todayHighlight: true
                });
            });
        })(jQuery);
        (function ($) {
            $(document).ready(function () {
                $('#datetimepicker2').datepicker({
                    language: 'es',
                    autoclose: true,
                    todayHighlight: true
                });
            });
        })(jQuery);

        $(document).ready(function() {
            var desc = document.getElementById("<%=DivTablaEquipamiento.ClientID %>");
            var c=document.getElementById("<%=RadioButtonEquipa1.ClientID %>");
            if (c.checked) {
               desc.style.display = "block";
            }
            else{
               desc.style.display = "none";
            };
            var desc = document.getElementById("<%=DivTablaMudanza.ClientID %>");
            var c=document.getElementById("<%=RadioButtonEdificio1.ClientID %>");
            if (c.checked) {
               desc.style.display = "block";
            }
            else{
               desc.style.display = "none";
            };
            EvalEspacios();
            $("#aspnetForm").validate({
                rules: {
                    <%=txtDenominacion.UniqueID %>: {
                        required: true
                    },
                     <%=txtCP.UniqueID %>: {                       
                        required: true,
                        range: [1000 , 9999]
                    },                    
                     <%=txtDomicilio.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtPrefijo.UniqueID %>: {                       
                        required: false
                    },
                     <%=txtNumero.UniqueID %>: {                       
                        required: false
                    },
                     <%=txtMail.UniqueID %>: {                       
                        required: false,
                        email:true
                    },
                     <%=txtWeb.UniqueID %>: {                       
                        required: false
                    },
                     <%=txtLocalidades.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtEquipamiento.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtEspacioEscenico.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtEquipamiento.UniqueID %>: {                       
                        required: true
                    }
                }, messages: {
                    <%=txtDenominacion.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtCP.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        range:  "<span  style='color:red'>* Debe ingresar entre 1000 y 9999 *</span>"
                    },
                    <%=txtDomicilio.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtPrefijo.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtNumero.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtMail.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        email: "<span  style='color:red'>Debe ingresar un correo válido</span>"
                    },
                    <%=txtWeb.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtLocalidades.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtEquipamiento.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtEspacioEscenico.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtEquipamiento.UniqueID %>:{
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
    function validatenumber(key) {
        var keycode = (key.which) ? key.which : key.keyCode;
        var phn = document.getElementById('txtPhn');
        if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
            return false;
        }
        else {
                return true;
            }
        }
    </script>

    <script type="text/javascript">
        function TieneEquipo() {
           var desc = document.getElementById("<%=DivTablaEquipamiento.ClientID %>");
           desc.style.display = "block";
        }
    </script>

    <script type="text/javascript">
        function NoTieneEquipo() {
           var desc = document.getElementById("<%=DivTablaEquipamiento.ClientID %>");
           desc.style.display = "none";
        }
    </script>

    <script type="text/javascript">
        function Mudanza() {
           var desc = document.getElementById("<%=DivTablaMudanza.ClientID %>");
           desc.style.display = "block";
        }
    </script>

    <script type="text/javascript">
        function NoMudanza() {
           var desc = document.getElementById("<%=DivTablaMudanza.ClientID %>");
           desc.style.display = "none";
        }
    </script>

    <script type="text/javascript">
        function EvalEspacios() {
            var e = document.getElementById("<%=DdlCantidadEspacios.ClientID %>");
            var desc = document.getElementById("<%=TablaEspacio1.ClientID %>");
            var desc1 = document.getElementById("<%=TablaEspacio2.ClientID %>");
            var desc2 = document.getElementById("<%=TablaEspacio3.ClientID %>");
            var desc3 = document.getElementById("<%=TablaEspacio4.ClientID %>");
            var sCodigo = e.options[e.selectedIndex].value;
            if (sCodigo >= 1) {
                desc.style.display = "block";
            }
            else {
                desc.style.display = "none";
            }
            if (sCodigo >= 2) {
                desc1.style.display = "block";
            }
            else {
                desc1.style.display = "none";
            }
            if (sCodigo >= 3) {
                desc2.style.display = "block";
            }
            else {
                desc2.style.display = "none";
            }
            if (sCodigo == 4) {
                desc3.style.display = "block";
            }
            else {
                desc3.style.display = "none";
            }

        }
   </script>

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
