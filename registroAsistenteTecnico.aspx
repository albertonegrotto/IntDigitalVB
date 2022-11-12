<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="registroAsistenteTecnico.aspx.vb" Inherits="INTeatroDig.registroAsistenteTecnico"
    Title="Registro de Asistente Técnico" Culture="es-AR" MaintainScrollPositionOnPostBack="True" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

       <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Registro de Capacitador Técnico</h2>
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
           <asp:HyperLink ID="HyperLink2" runat="server" 
                NavigateUrl="http://inteatro.gob.ar/intdigital/reglamentacion_registro.htm" CssClass="links" Target="_blank">
                Reglamentación del Registro Nacional del Teatro Independiente
           </asp:HyperLink>
            , la 
           <asp:HyperLink ID="HyperLink3" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/40000-44999/42762/norma.htm" CssClass="links" Target="_blank">
                Ley Nº 24.800
           </asp:HyperLink>
           y su 
           <asp:HyperLink ID="HyperLink4" runat="server" 
                NavigateUrl="http://www.infoleg.gob.ar/infolegInternet/anexos/45000-49999/46047/norma.htm" CssClass="links" Target="_blank">
                    Decreto Reglamentario Nº 991/97
           </asp:HyperLink><br />
           <asp:CheckBox ID="AceptoDJ" runat="server" Text="Acepto" AutoPostBack="True" /> &nbsp;&nbsp;<asp:Label ID="Label10" runat="server" style="color: red" Text="[*]"></asp:Label>
           <asp:Label ID="Label2" runat="server" CssClass="text-danger"></asp:Label>
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
              <label>Provincia:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="ddlProvincias" runat="server" CssClass="form-control"></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row" visible="false">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-7 col-left" style="text-align: left" >
             <%-- <label visible="false">Curriculum Vitae (no mas de 3000 caracteres):</label>--%>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row" visible="false">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-7 col-left" style="text-align: left">
              <asp:TextBox ID="txtCV" runat="server" Visible="false"
                  TextMode="multiline" MaxLength="3000" Columns="56" Rows="8" CssClass="form-control"
                  ToolTip="Ingrese su curriculum vitae, el mismo no debe contener mas de 3000 caracteres"></asp:TextBox>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row" id="Curriculum" runat="server">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Currículum Vitae : </label>
          </div>
          <div class="col-md-3 col-left">
             <asp:FileUpload ID="UploadImporta" runat="server" CssClass="form-control" onchange="EvalCV()"/>
             <asp:Label ID="Label7" runat="server" Text="Puede adjuntar un archivo en formato .doc .docx o .pdf de hasta 10 Mb." CssClass="TextoComentario"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Label ID="LabelNombreUpload" runat="server" CssClass="form-control" Height="50px"></asp:Label>
          </div>
          <div class="col-md-1 col-center">             
              <asp:Label ID="Label12" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 col-left">
             <asp:Button ID="BtnVisualiza" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <br />
       <div class="row" id="Foto" runat="server">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Foto : </label>
          </div>
          <div class="col-md-3 col-left">
             <asp:FileUpload ID="UploadImportaf" runat="server" CssClass="form-control" onchange="EvalFoto()"/>
             <asp:Label ID="Label3" runat="server" Text="Puede adjuntar un archivo en formato .jpg .jepg  de hasta 10 Mb." CssClass="TextoComentario"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Label ID="LabelNombreUploadf" runat="server" CssClass="form-control" Height="50px"></asp:Label>
          </div>
          <div class="col-md-2 col-left">
             <asp:Button ID="BtnVisualizaf" runat="server" Text="Visualizar" CssClass="btn btn-primary"  /> 
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
               <asp:Label ID="txtErrorCV" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-center" style="text-align: center">
              <label>Inicio de actividades:</label>
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
          <div class="col-md-4 col-left">
              <asp:Label ID="Label5" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:Label runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:Label>              
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fecha Inicio en la docencia:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker2" >
                    <input type="text" class="form-control" id="TextDocencia" placeholder="Docencia" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
             <asp:Label ID="txtErrorFechaDocencia" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label6" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:Label runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:Label>              
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-7 col-left" style="text-align: left">
              <label>Programa de la asistencia técnica y/o plan de trabajo</label>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label9" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-8 col-left" style="text-align: left">
             <asp:TextBox ID="txtEspecialidad" runat="server"
                 TextMode="multiline" MaxLength="3000" Columns="56" Rows="8" CssClass="form-control"
                 ToolTip="Ingrese el programa y/o plan de trabajo, el mismo no debe contener mas de 3000 caracteres">
             </asp:TextBox>
          </div>
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <asp:Label runat="server" CssClass="TextoComentario">(máximo 10, seleccione su especialidad, detalle su programa y/o plan de trabajo y pulse el botón "Agregar Especialidad")</asp:Label>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Especialidad:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtActividad" runat="server" MaxLength="50" Columns="40" CssClass="form-control"
                    ToolTip="Ingrese la especialidad"></asp:TextBox>
              <asp:Label ID="txtErrorActividad" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label8" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-left" style="text-align: center">
              <asp:Button ID="btnEspecialidad" runat="server" Text="Agregar Especialidad" CssClass="btn btn-primary" />
              <asp:Label ID="txtErrorEspecialidad" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-7 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-8 col-left" style="text-align: left; font-family:'Trebuchet MS'; font-size:small">
               <!-- Gridview -->
               <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AllowPaging="True"
                   DataSourceID="SqlDataSource1" CssClass="table table-bordered"
                   AutoGenerateColumns="False" DataKeyNames="idEspecialidad">
                   <Columns>
                       <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center"
                           HeaderText="Id" DataField="idEspecialidad" SortExpression="idEspecialidad"
                           ReadOnly="True" Visible="false">
                           <ItemStyle HorizontalAlign="Center" Width="22px"></ItemStyle>
                       </asp:BoundField>
                       <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center"
                           HeaderText="CUIT" DataField="cuit" SortExpression="cuit" Visible="false">
                           <ItemStyle HorizontalAlign="Center" Width="22px"></ItemStyle>
                       </asp:BoundField>
                       <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center"
                           HeaderText="Registro" DataField="codigoRegistro"
                           SortExpression="codigoRegistro" Visible="false">
                           <ItemStyle HorizontalAlign="Center" Width="22px"></ItemStyle>
                       </asp:BoundField>
                       <asp:BoundField ItemStyle-Width="422" ItemStyle-HorizontalAlign="Center"
                           HeaderText="Especialidad" DataField="actividad" SortExpression="actividad">
                           <ItemStyle HorizontalAlign="Center" Width="422px"></ItemStyle>
                       </asp:BoundField>
                       <asp:BoundField ItemStyle-Width="322" ItemStyle-HorizontalAlign="Center"
                           HeaderText="Descripción" DataField="descripcion" SortExpression="descripcion" Visible="false">
                           <ItemStyle HorizontalAlign="Center" Width="322px"></ItemStyle>
                       </asp:BoundField>
                       <asp:TemplateField HeaderText="&nbsp;Actualización&nbsp;" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                           <ItemTemplate>
                               <asp:HyperLink ID="HyperLinkModificar" runat="server" NavigateUrl="" CssClass="links">Actualización</asp:HyperLink>
                           </ItemTemplate>
                           <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                           <ItemStyle HorizontalAlign="Center"></ItemStyle>
                       </asp:TemplateField>
                       <asp:CommandField
                           ItemStyle-Width="160"
                           ButtonType="Image"
                           ShowEditButton="False"
                           ShowDeleteButton="True"
                           DeleteImageUrl="images/btnDelete.gif" DeleteText="Desvincular"
                           EditImageUrl="images/btnEdit.gif" EditText="Edit"
                           UpdateImageUrl="images/btnUpdate.gif" UpdateText="Update"
                           CancelText="Cancel" CancelImageUrl="images/btnCancel.gif"
                           ControlStyle-Font-Bold="True">
                           <ControlStyle Font-Bold="True"></ControlStyle>
                           <ItemStyle Width="160px"></ItemStyle>
                       </asp:CommandField>
                   </Columns>
                   <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
               </asp:GridView>
               <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" 
                   ID="SqlDataSource1" runat="server"
                   SelectCommand="SELECT E.idEspecialidad AS idEspecialidad, E.codigoRegistro AS codigoRegistro, E.cuit AS cuit, 
                                  E.actividad AS actividad, E.descripcion AS descripcion FROM EspecialidadesTemp E
                                  WHERE E.cuit = @cuit ORDER BY E.actividad"
                   DeleteCommand="DELETE FROM EspecialidadesTemp WHERE idEspecialidad = @idEspecialidad"
                   CancelSelectOnNullParameter="false">
                   <SelectParameters>
                      <asp:Parameter Name="cuit" Type="Decimal" />
                   </SelectParameters>
                   <DeleteParameters>
                      <asp:Parameter Name="idEspecialidad" Type="Int32" />
                   </DeleteParameters>
               </asp:SqlDataSource>
               <!-- End of Gridview -->

          </div>
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:label CssClass="observacion" runat="server">Los datos consignados tienen carácter de Declaración Jurada y deberán actualizarse toda vez que produzca cualquier cambio en relación a lo declarado precedentemente.</asp:label>
             <br />
             <asp:CheckBox ID="chkAcepto" runat="server" Text="Acepto" />  &nbsp;&nbsp;<asp:Label ID="Label55" runat="server" style="color: red" Text="[*]"></asp:Label>
             <asp:Label ID="txtErrorAcepto" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center;">
             <asp:label CssClass="observacion" runat="server">Recibirá en la cuenta de correo electrónico consignada un mail conteniendo la información ingresada, el cual deberá validar siguiendo el link de verificación 
                que estará incluido en el mismo mensaje. Luego recibirá en esa misma cuenta de correo el mail de notificación de aceptación del trámite realizado, con las 
                debidas instrucciones para el envío posterior de la confirmación del trámite y demás material que correspondiera adjuntar.</asp:label>             
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
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
             <asp:label CssClass="observacion" runat="server">Autorizo al Instituto Nacional del Teatro a que los datos consignados sean de acceso público (en virtud de lo establecido por la Ley 25.326 de Protección de Datos
                     Personales) y sirvan de utilidad para uso estadístico y para la implementación de políticas públicas para el sector cultural</asp:label>
             <br />
              <asp:CheckBox ID="checkAutorizoPublicar" runat="server" Text="Acepto" Visible="True" /> &nbsp;&nbsp;<asp:Label ID="Label4" runat="server" style="color: red" Text="[*]"></asp:Label>
             <asp:Label ID="lblErrorcheckAutorizoPublicar" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
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
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
               <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" />
               <asp:Button ID="btnCancelar" runat="server" class="btn btn-warning" Text="Cancelar"/> &nbsp;&nbsp;<asp:Label ID="Label11" runat="server" style="color: red" Text="[*]"></asp:Label>   
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
    </script>

    <script type="text/javascript">
        function EvalCV() {
          var sfile =document.getElementById("<%=UploadImporta.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelNombreUpload.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>
    
    <script type="text/javascript">
        function EvalFoto() {
          var sfile =document.getElementById("<%=UploadImportaf.ClientID %>").value;
          if (sfile.length > 0) 
          {
            var desc = document.getElementById("<%=LabelNombreUploadf.ClientID %>");
            desc.innerHTML = sfile.split("\\").pop();
          }
        }
    </script>

</asp:Content>
