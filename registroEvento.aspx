<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="registroEvento.aspx.vb" Inherits="INTeatroDig.registroEvento" 
    title="Registro de Eventos" Culture="es-AR" MaintainScrollPositionOnPostBack="True" %>
    
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Registro de Eventos</h2>
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
           <asp:CheckBox ID="AceptoDJ" runat="server" Text="Acepto" AutoPostBack="True" /> &nbsp;&nbsp;<asp:Label ID="Label8" runat="server" style="color: red" Text="[*]"></asp:Label>            
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
               <asp:DropDownList ID="ddlSectores" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
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
              <label>Denominación completa:</label>
          </div>
          <div class="col-md-4 col-centered" style="text-align: left">
              <asp:TextBox ID="txtDenominacion" runat="server" MaxLength="200" Columns="40" CssClass="form-control"
                  ToolTip="Ingrese la Denominación del Evento" onkeypress="return validate(event)"></asp:TextBox>
          </div>
          <div class="col-md-2 col-left">
              <asp:Label ID="Label9" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-7 col-center">
              <asp:label ID="Label2" runat="server" CssClass="TextoComentario">No se permite consignar letras acentuadas ni símbolos ni caracteres especiales</asp:label>
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
              <asp:DropDownList ID="ddlProvincias" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label5" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Localidad:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="ddlLocalidades" runat="server" CssClass="form-control"></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label6" runat="server" style="color: red" Text="[*]"></asp:Label>
              <asp:Label ID="txtErrorLocalidades" runat="server" CssClass="text-danger"></asp:Label>
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
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-center" style="text-align: center">
              <label>Fecha de Inicio de actividades:</label>
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
             <asp:Label ID="Label14" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:label ID="Label17" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>              
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Tipo de Evento:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <asp:RadioButton ID="radioPeriodico" runat="server" Text="Periódico" CssClass="form-control" GroupName="grupoTipoEvento" />
              <asp:RadioButton ID="radioOcasional" runat="server" Text="Ocasional" CssClass="form-control" Checked="false" GroupName="grupoTipoEvento" />
          </div>
          <div class="col-md-5 col-left">
              <asp:Label ID="Label7" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-7 col-center">
              <asp:label ID="Label3" runat="server" CssClass="TextoComentario">Si selecciona &#39;Periódico&#39; debe ingresar a continuación las fechas desde / hasta.</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <asp:Label ID="txtErrorTipoEvento" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fecha de Primera Edición desde:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker2" >
                    <input type="text" class="form-control" id="TextEdicion1" placeholder="1° Edicion" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaDesde" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">
              <asp:label ID="Label15" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>              
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fecha de Primera Edición hasta:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
                <div class="input-group date" id="datetimepicker3" >
                    <input type="text" class="form-control" id="TextHasta" placeholder="1° Edicion" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
                <asp:Label ID="txtErrorFechaHasta" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">
              <asp:label ID="Label16" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>              
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Público:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="DdlPublico" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
              <asp:Label ID="LblErrorDdlPublico" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label10" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
         <div class="col-md-1 col-left">
         </div>
         <div class="col-md-3 col-centered" style="text-align: center">
             <label>Actividades Complementarias: </label>
         </div>
         <div class="col-md-3 col-centered" style="text-align: left">
             <asp:DropDownList ID="ddlActividad" runat="server" CssClass="form-control" onchange="EvalActividad()" ></asp:DropDownList>
             <asp:Label ID="lblErrorddlActividad" runat="server" CssClass="text-danger"></asp:Label>
             <asp:TextBox ID="TextBoxDActividad"  runat="server" placeholder="Descripción" CssClass="form-control pull-left" Width="200px" MaxLength="50"></asp:TextBox> 
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
             <label>Tipo de Gestión: </label>
         </div>
         <div class="col-md-3 col-centered" style="text-align: left">
             <asp:DropDownList ID="Ddlgestion" runat="server" CssClass="form-control"></asp:DropDownList>
             <asp:Label ID="LblErrorDdlGestion" runat="server" CssClass="text-danger"></asp:Label>
         </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label11" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
         <div class="col-md-1 text-right">
         </div>
       </div>
       <div class="row">
         <div class="col-md-1 col-left">
         </div>
         <div class="col-md-3 col-centered" style="text-align: center">
             <label>Meses de Realización: </label>
         </div>
         <div class="col-md-7 col-centered" style="text-align: left">
         </div>
         <div class="col-md-1 text-right">
         </div>
       </div>
       <div class="row">
         <div class="col-md-1 col-left">
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkEnero" runat="server" Visible="true" Checked="false" Text="Enero" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkFebrero" runat="server" Visible="true" Checked="false" Text="Febrero" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkMarzo" runat="server" Visible="true" Checked="false" Text="Marzo" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkAbril" runat="server" Visible="true" Checked="false" Text="Abril" CssClass="form-control"/>
         </div>
         <div class="col-md-1 text-right">
         </div>
       </div>
       <div class="row">
         <div class="col-md-1 col-left">
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkMayo" runat="server" Visible="true" Checked="false" Text="Mayo" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkJunio" runat="server" Visible="true" Checked="false" Text="Junio" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkJulio" runat="server" Visible="true" Checked="false" Text="Julio" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkAgosto" runat="server" Visible="true" Checked="false" Text="Agosto" CssClass="form-control"/>
         </div>
       </div>
       <div class="row">
         <div class="col-md-1 col-left">
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkSetiembre" runat="server" Visible="true" Checked="false" Text="Setiembre" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkOctubre" runat="server" Visible="true" Checked="false" Text="Octubre" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkNoviembre" runat="server" Visible="true" Checked="false" Text="Noviembre" CssClass="form-control"/>
         </div>
         <div class="col-md-2 col-left">
            <asp:Checkbox ID="ChkDiciembre" runat="server" Visible="true" Checked="false" Text="Diciembre" CssClass="form-control"/>
         </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Integrantes</label>
          </div>
          <div class="col-md-7 col-centered" style="text-align: left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-9 col-center">
              <asp:label ID="Label4" runat="server" CssClass="TextoComentario">(Ingrese el CUIL / CUIT de cada integrante y pulse el botón &quot;Agregar Integrante&quot;)</asp:label>
          </div>
          <div class="col-md-1 col-left">
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
              <asp:Label ID="Label12" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
              <asp:Button ID="btnIntegrante" runat="server" Text="Agregar Integrante" CssClass="btn btn-primary" />
          </div>
          <div class="col-md-1 text-right">
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
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:label CssClass="observacion" runat="server">Los datos consignados tienen carácter de Declaración Jurada y deberán actualizarse toda vez que produzca cualquier cambio en relación a lo declarado precedentemente.</asp:label>
             <br />
             <asp:CheckBox ID="chkAcepto" runat="server" Text="Acepto" /> &nbsp;&nbsp;<asp:Label ID="Label13" runat="server" style="color: red" Text="[*]"></asp:Label>     
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
       <!-- Palabras a publicar -->
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <h3>Datos del Responsable</h3>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
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
       <br / />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
              <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" /> 
              <asp:Button ID="btnCancelar" runat="server" class="btn btn-warning" Text="Cancelar" /> &nbsp;&nbsp;<asp:Label ID="Label19" runat="server" style="color: red" Text="[*]"></asp:Label>   
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

        (function ($) {
            $(document).ready(function () {
                $('#datetimepicker3').datepicker({
                    language: 'es',
                    autoclose: true,
                    todayHighlight: true
                });
            });
        })(jQuery);

        $(document).ready(function() {
            EvalActividad()
            $("#aspnetForm").validate({
                rules: {
                    <%=txtDenominacion.UniqueID %>: {
                        required: true
                    },
                     <%=txtMail.UniqueID %>: {                       
                        required: false,
                        email:true
                    },
                     <%=txtWeb.UniqueID %>: {                       
                        required: false
                    }
                }, messages: {
                    <%=txtDenominacion.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtMail.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        email: "<span  style='color:red'>Debe ingresar un correo válido</span>"
                    },
                    <%=txtWeb.UniqueID %>:{
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
        function EvalActividad() {
            var e = document.getElementById("<%=ddlActividad.ClientID %>");
            var desc = document.getElementById("<%=TextBoxDActividad.ClientID %>");
            var sCodigo = e.options[e.selectedIndex].value;
            if (sCodigo == 4) {
                desc.style.display = "block";
            }
            else {
                desc.value = "";
                desc.style.display = "none";
            }
        }
   </script>

</asp:Content>
