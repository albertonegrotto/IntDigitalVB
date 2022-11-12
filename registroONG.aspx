<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="registroONG.aspx.vb" Inherits="INTeatroDig.registroONG" 
    title="Registro de Entidad/Sociedad vinculada a la Actividad Teatral Independiente"  Culture="es-AR" MaintainScrollPositionOnPostBack="True"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Registro de Entidad/Sociedad vinculada a la Actividad Teatral Independiente</h2>
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
           <asp:CheckBox ID="AceptoDJ" runat="server" Text="Acepto" AutoPostBack="True" />  &nbsp;&nbsp;<asp:Label ID="Label20" runat="server" style="color: red" Text="[*]"></asp:Label>          
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
              <label>Provincia:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:DropDownList ID="ddlProvincias" runat="server" CssClass="form-control"
                    AutoPostBack="True"></asp:DropDownList>
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
              <label id="LabelFechaConstitucion" runat="server">Fecha de constitución: </label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker1" >
                    <input type="text" class="form-control" id="TextONG" placeholder="Inicio" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaONG" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-5 col-left">
             <asp:Label ID="Label21" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 text-right">
          </div>
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-7 col-left">
              <asp:label ID="Label2" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row"  id="ultiacta" runat="server" >
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fecha de última acta:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker2" >
                    <input type="text" class="form-control" id="TextActa" placeholder="Inicio" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaActa" runat="server" CssClass="text-danger"></asp:Label>   
          </div>
          <div class="col-md-5 col-left">
             <asp:Label ID="Label22" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row"  id="ultiacta2" runat="server" >
          <div class="col-md-1 text-right">
          </div>
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-7 col-left">
              <asp:label ID="Label3" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row" id="vigencia" runat="server" >
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Vigencia de autoridades<br />(según 
                estatuto):</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
               <asp:DropDownList ID="ddlVigencias" runat="server" CssClass="form-control" ></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label23" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
               <asp:Label ID="txtErrorVigencias" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <h3 id="LabelPresidente" runat="server">Presidente</h3>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nº de CUIL:</label>
          </div>
          <!-- Presidente -->
          <div class="col-md-2 col-centered" style="text-align: left">
                <asp:TextBox ID="txtCUIL1" runat="server" Text="" CssClass="form-control" onkeypress="return validate(event)"></asp:TextBox>
                <asp:Label ID="txtErrorCUIL1" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-5 col-left">
             <asp:Label ID="Label24" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fecha de Nacimiento: </label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker3" >
                    <input type="text" class="form-control" id="TextNacim1" placeholder="Inicio" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaNac1" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-5 col-left">
             <asp:Label ID="Label25" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 text-right">
          </div>
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-7 col-left">
              <asp:label ID="Label4" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-9 col-center">
              <asp:label ID="Label5" runat="server" CssClass="TextoComentario"> Los menores de 18 años con carácter de EMANCIPADOS deberán presentar ante el 
                I.N.T. copia autenticada de la documentación respectiva por la cual ha sido otorgada la emancipación.</asp:label>
                <asp:Label ID="txtErrorCheckBoxEdad1" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <asp:CheckBox ID="CheckBoxEdad1" runat="server" Text="Acepto" Width="80px" />
          </div>
          <div class="col-md-2 text-right">
          </div>
      </div>
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nombre/s:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:TextBox ID="txtNombre1" runat="server" MaxLength="50" Columns="30" CssClass="form-control" ToolTip="Nombre/s completo/s real" onkeypress="return validaten(event)"></asp:TextBox>
                <asp:Label ID="Label12" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
                <asp:Label ID="txtErrorNombre1" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label26" runat="server" style="color: red" Text="[*]"></asp:Label>
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
          <div class="col-md-3 col-centered" style="text-align: left">
             <asp:TextBox ID="txtApellido1" runat="server" MaxLength="50" Columns="30"  CssClass="form-control" ToolTip="Apellido/s completo/s real" onkeypress="return validatea(event)"></asp:TextBox>
             <asp:Label ID="Label13" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
             <asp:Label ID="txtErrorApellido1" runat="server" CssClass="text-danger"></asp:Label>
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
              <label>Género:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
             <asp:DropDownList ID="ddlSexo1" runat="server" Width="300px" CssClass="form-control" onchange="EvalSexo1()" ></asp:DropDownList>
             <asp:Label ID="lblErrorddlSexo1" runat="server" CssClass="text-danger"></asp:Label>
             <asp:TextBox ID="TextBoxDesexo1" runat="server" placeholder="Descripción" CssClass="form-control pull-left" Width="200px" MaxLength="50"></asp:TextBox> 
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label28" runat="server" style="color: red" Text="[*]"></asp:Label>
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
                <asp:DropDownList ID="ddlProvincia1" runat="server" CssClass="form-control"
                    AutoPostBack="True"></asp:DropDownList>
                <asp:Label ID="txtErrorProvincia1" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label29" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:DropDownList ID="ddlLocalidades1" runat="server" CssClass="form-control" ></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
             <div class="col-md-4 col-left">
                <asp:Label ID="Label30" runat="server" style="color: red" Text="[*]"></asp:Label>
             </div>
              <asp:Label ID="txtErrorLocalidades1" runat="server" CssClass="text-danger"></asp:Label>            
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
                <asp:TextBox ID="txtCP1" runat="server" Width="90px" Text="" CssClass="form-control"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    TargetControlID="txtCP1" 
                    Mask="9999"
                    MessageValidatorTip="true" 
                    MaskType="Number" 
                    AcceptNegative="Left" 
                    ErrorTooltipEnabled="True">
                </asp:MaskedEditExtender>
          </div>
          <div class="col-md-7 col-left">
               <div class="col-md-4 col-left">
                 <asp:Label ID="Label31" runat="server" style="color: red" Text="[*]"></asp:Label>
               </div>
               <asp:Label ID="txtErrorCP1" runat="server" CssClass="text-danger"></asp:Label> 
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Domicilio: </label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtDomicilio1" runat="server" MaxLength="100" Columns="30"  CssClass="form-control"
                ToolTip="Domicilio completo"></asp:TextBox>
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label32" runat="server" style="color: red" Text="[*]"></asp:Label>
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
            <asp:TextBox ID="txtPrefijo1" placeholder="Prefijo" runat="server" CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="txtNumero1" placeholder="Teléfono" runat="server" Width="230px" MaxLength="8" CssClass="form-control pull-left"></asp:TextBox>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Teléfono Celular: </label>
        </div>
        <div class="col-md-1 text-left" style="width: 30px">
            <label for="txtPrefijoCelular1" >549</label> 
        </div>
        <div class="col-md-1 col-centered" style="text-align: left">
            <asp:TextBox ID="txtPrefijoCelular1" placeholder="Prefijo" runat="server"  CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>                
        </div>
        <div class="col-md-1 text-right" style="width: 10px">
            <label for="txtNumeroCelular1" > 15 </label> 
        </div>
        <div class="col-md-2 text-right">
            <asp:TextBox ID="txtNumeroCelular1" placeholder="Teléfono" runat="server" CssClass="form-control pull-left" Width="230px" MaxLength="8"></asp:TextBox>
            <asp:Label ID="txtErrorTelefono1" runat="server" CssClass="text-danger"></asp:Label>
        </div>
        <div class="col-md-2 col-left">
           <asp:Label ID="Label33" runat="server" style="color: red" Text="[*]"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
      <!-- End of Presidente -->
      <br />
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <h3 id="LabelVicePresidente" runat="server">Vice-Presidente</h3>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
      <!-- Vicepresidente -->
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nº de CUIL</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <asp:TextBox ID="txtCUIL2" runat="server" Text="" onkeypress="return validate(event)" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="txtErrorCUIL2" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-5 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fecha de Nacimiento:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker4" >
                    <input type="text" class="form-control" id="TextNacim2" placeholder="Inicio" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaNac2" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-5 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 text-right">
          </div>
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-7 col-left">
              <asp:label ID="Label6" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-9 col-center">
              <asp:label ID="Label7" runat="server" CssClass="TextoComentario"> Los menores de 18 años con carácter de EMANCIPADOS deberán presentar ante el 
                I.N.T. copia autenticada de la documentación respectiva por la cual ha sido otorgada la emancipación.</asp:label>
                <asp:Label ID="txtErrorCheckBoxEdad2" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <asp:CheckBox ID="CheckBoxEdad2" runat="server" Text="Acepto" Width="80px" /><br />
          </div>
          <div class="col-md-2 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nombre/s:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:TextBox ID="txtNombre2" runat="server" MaxLength="50" Columns="30" CssClass="form-control" ToolTip="Nombre/s completo/s real" onkeypress="return validaten(event)"></asp:TextBox>
                <asp:Label ID="Label14" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
                <asp:Label ID="txtErrorNombre2" runat="server" CssClass="text-danger"></asp:Label>                
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
              <label>Apellido/s:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:TextBox ID="txtApellido2" runat="server" MaxLength="50" Columns="30" Cssclass="form-control" ToolTip="Apellido/s completo/s real" onkeypress="return validatea(event)"></asp:TextBox>
                <asp:Label ID="Label15" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
                <asp:Label ID="txtErrorApellido2" runat="server" CssClass="text-danger"></asp:Label>                
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
              <label>Género:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="DdlSexo2" runat="server" Width="300px" CssClass="form-control" onchange="EvalSexo2()" ></asp:DropDownList>
              <asp:Label ID="LblErrorDddlsexo2" runat="server" CssClass="text-danger"></asp:Label>
              <asp:TextBox ID="TextBoxDesexo2"  placeholder="Descripción" runat="server"  CssClass="form-control pull-left" Width="200px" MaxLength="50"></asp:TextBox> 
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
                <asp:DropDownList ID="ddlProvincia2" runat="server" CssClass="form-control"
                    AutoPostBack="True"></asp:DropDownList>
                <asp:Label ID="txtErrorProvincia2" runat="server" CssClass="text-danger"></asp:Label>
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
              <label>Localidad:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="ddlLocalidades2" runat="server" CssClass="form-control" ></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="txtErrorLocalidades2" runat="server" CssClass="text-danger"></asp:Label>            
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
                <asp:TextBox ID="txtCP2" runat="server" Width="90px" Text="" CssClass="form-control"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                    TargetControlID="txtCP2" 
                    Mask="9999"
                    MessageValidatorTip="true" 
                    MaskType="Number" 
                    AcceptNegative="Left" 
                    ErrorTooltipEnabled="True">
                </asp:MaskedEditExtender>
                <asp:Label ID="txtErrorCP2" runat="server" CssClass="text-danger"></asp:Label>    
          </div>
          <div class="col-md-6 col-left">
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Domicilio:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:TextBox ID="txtDomicilio2" runat="server" MaxLength="100" Columns="30" CssClass="form-control"
                ToolTip="Domocilio completo"></asp:TextBox>
                <asp:Label ID="txtErrorDomicilio2" runat="server" CssClass="text-danger"></asp:Label>
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
            <label>Teléfono: </label>
        </div>
        <div class="col-md-1 text-center" style="width: 30px">
            <label for="txtPrefijo2">0</label> 
        </div>
        <div class="col-md-6 col-centered" style="text-align: left"> 
            &nbsp;
            <asp:TextBox ID="txtPrefijo2" placeholder="Prefijo" runat="server" CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="txtNumero2" placeholder="Teléfono" runat="server" Width="230px" MaxLength="8" CssClass="form-control pull-left"></asp:TextBox>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Teléfono Celular: </label>
        </div>
        <div class="col-md-1 text-left" style="width: 30px">
            <label for="txtPrefijoCelular2" >549</label> 
        </div>
        <div class="col-md-1 col-centered" style="text-align: left">
            <asp:TextBox ID="txtPrefijoCelular2" placeholder="Prefijo" runat="server"  CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>                
        </div>
        <div class="col-md-1 text-right" style="width: 10px">
            <label for="txtNumeroCelular2" > 15 </label> 
        </div>
        <div class="col-md-4 text-right">
            <asp:TextBox ID="txtNumeroCelular2" placeholder="Teléfono" runat="server" CssClass="form-control pull-left" Width="230px" MaxLength="8"></asp:TextBox>
            <asp:Label ID="txtErrorTelefono2" runat="server" CssClass="text-danger"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
      <!-- End of Vicepresidente -->
      <br />
      <!-- Secretario -->
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <h3 id="LabelSecretario" runat="server">Secretario</h3>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nº de CUIL:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
                <asp:TextBox ID="txtCUIL3" runat="server" Text="" onkeypress="return validate(event)" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="txtErrorCUIL3" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-5 col-left">
             <asp:Label ID="Label34" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fecha de Nacimiento: </label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker5" >
                    <input type="text" class="form-control" id="TextNacim3" placeholder="Inicio" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaNac3" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-5 col-left">
             <asp:Label ID="Label35" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 text-right">
          </div>
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-7 col-left">
              <asp:label ID="Label8" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-9 col-center">
              <asp:label ID="Label9" runat="server" CssClass="TextoComentario"> Los menores de 18 años con carácter de EMANCIPADOS deberán presentar ante el 
                I.N.T. copia autenticada de la documentación respectiva por la cual ha sido otorgada la emancipación.</asp:label>
                <asp:Label ID="txtErrorCheckBoxEdad3" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
             <asp:CheckBox ID="CheckBoxEdad3" runat="server" Text="Acepto" Width="80px" />
          </div>
          <div class="col-md-2 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nombre/s:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtNombre3" runat="server" MaxLength="50" Columns="30"  CssClass="form-control" ToolTip="Nombre/s completo/s real" onkeypress="return validaten(event)"></asp:TextBox>
              <asp:Label ID="Label16" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
              <asp:Label ID="txtErrorNombre3" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label36" runat="server" style="color: red" Text="[*]"></asp:Label>
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
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:TextBox ID="txtApellido3" runat="server" MaxLength="50" Columns="30" CssClass="form-control" ToolTip="Apellido/s completo/s real" onkeypress="return validatea(event)"></asp:TextBox>
                <asp:Label ID="Label17" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
                <asp:Label ID="txtErrorApellido3" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label37" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Género:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="DdlSexo3" runat="server" Width="300px" CssClass="form-control" onchange="EvalSexo3()" ></asp:DropDownList>
              <asp:Label ID="LblErrorDdlsexo3" runat="server" CssClass="text-danger"></asp:Label>
              <asp:TextBox ID="TextBoxDesexo3"  placeholder="Descripción" runat="server"  CssClass="form-control pull-left" Width="200px" MaxLength="50"></asp:TextBox> 
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label38" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:DropDownList ID="ddlProvincia3" runat="server" CssClass="form-control"
                    AutoPostBack="True"></asp:DropDownList>
              <asp:Label ID="txtErrorProvincia3" runat="server" CssClass="text-danger"></asp:Label>
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
              <label>Localidad:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="ddlLocalidades3" runat="server" CssClass="form-control" ></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label40" runat="server" style="color: red" Text="[*]"></asp:Label>
             <asp:Label ID="txtErrorLocalidades3" runat="server" CssClass="text-danger"></asp:Label>
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
                <asp:TextBox ID="txtCP3" runat="server" Width="90px" Text="" CssClass="form-control"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                    TargetControlID="txtCP3" 
                    Mask="9999"
                    MessageValidatorTip="true" 
                    MaskType="Number" 
                    AcceptNegative="Left" 
                    ErrorTooltipEnabled="True">
                </asp:MaskedEditExtender>
                <asp:Label ID="txtErrorCP3" runat="server" CssClass="text-danger"></asp:Label>    
          </div>
          <div class="col-md-6 col-left">
             <asp:Label ID="Label41" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Domicilio:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtDomicilio3" runat="server" MaxLength="100" Columns="30" CssClass="form-control"
                ToolTip="Domocilio completo"></asp:TextBox>
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
            <label>Teléfono: </label>
        </div>
        <div class="col-md-1 text-center" style="width: 30px">
            <label for="txtPrefijo3">0</label> 
        </div>
        <div class="col-md-6 col-centered" style="text-align: left"> 
            &nbsp;
            <asp:TextBox ID="txtPrefijo3" placeholder="Prefijo" runat="server" CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="TxtNumero3" placeholder="Teléfono" runat="server" Width="230px" MaxLength="8" CssClass="form-control pull-left"></asp:TextBox>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Teléfono Celular: </label>
        </div>
        <div class="col-md-1 text-left" style="width: 30px">
            <label for="txtPrefijoCelular3" >549</label> 
        </div>
        <div class="col-md-1 col-centered" style="text-align: left">
            <asp:TextBox ID="txtPrefijoCelular3" placeholder="Prefijo" runat="server"  CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>                
        </div>
        <div class="col-md-1 text-right" style="width: 10px">
            <label for="txtNumeroCelular3" > 15 </label> 
        </div>
        <div class="col-md-2 text-right">
            <asp:TextBox ID="txtNumeroCelular3" placeholder="Teléfono" runat="server" CssClass="form-control pull-left" Width="230px" MaxLength="8"></asp:TextBox>
            <asp:Label ID="TxtErrorTelefono3" runat="server" CssClass="text-danger"></asp:Label>
        </div>
        <div class="col-md-2 col-left">
           <asp:Label ID="Label43" runat="server" style="color: red" Text="[*]"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
      <!-- End of Secretario -->
      <br />
      <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <h3 id="Labeltesorero" runat="server">Tesorero</h3>
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nº de CUIL:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
                <asp:TextBox ID="txtCUIL4" runat="server" Text="" onkeypress="return validate(event)" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="txtErrorCUIL4" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-5 col-left">
             <asp:Label ID="Label44" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Fecha de Nacimiento:</label>
          </div>
          <div class="col-md-2 col-centered" style="text-align: left">
              <div class="form-group">
                <div class="input-group date" id="datetimepicker6" >
                    <input type="text" class="form-control" id="TextNacim4" placeholder="Inicio" runat="server"/>
                    <span class="input-group-addon">
                         <span class="glyphicon glyphicon-calendar" style="color:dodgerblue"></span>
                    </span>                  
                </div>
              </div>
              <asp:Label ID="txtErrorFechaNac4" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-5 col-left">
             <asp:Label ID="Label45" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 text-right">
          </div>
          <div class="col-md-3 col-left">
          </div>
          <div class="col-md-7 col-left">
              <asp:label ID="Label10" runat="server" CssClass="TextoComentario">Formato día, mes y año de 4 dígitos (dd/mm/aaaa)</asp:label>
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-9 col-center">
              <asp:label ID="Label11" runat="server" CssClass="TextoComentario"> Los menores de 18 años con carácter de EMANCIPADOS deberán presentar ante el 
                I.N.T. copia autenticada de la documentación respectiva por la cual ha sido otorgada la emancipación.</asp:label>
                <asp:Label ID="txtErrorCheckBoxEdad4" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-1 col-left">
          </div>
       </div>
       <div class="row">
          <div class="col-md-2 col-left">
          </div>
          <div class="col-md-8 col-centered" style="text-align: center">
              <asp:CheckBox ID="CheckBoxEdad4" runat="server" Text="Acepto" Width="80px" />
          </div>
          <div class="col-md-2 text-right">
          </div>
       </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Nombre/s:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtNombre4" runat="server" MaxLength="50" Columns="30" CssClass="form-control" ToolTip="Nombre/s completo/s real" onkeypress="return validaten(event)"></asp:TextBox>
              <asp:Label ID="Label18" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
              <asp:Label ID="txtErrorNombre4" runat="server" CssClass="text-danger"></asp:Label>    
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label46" runat="server" style="color: red" Text="[*]"></asp:Label>
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
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:TextBox ID="txtApellido4" runat="server" MaxLength="50" Columns="30" CssClass="form-control" ToolTip="Apellido/s completo/s real" onkeypress="return validatea(event)"></asp:TextBox>
                <asp:Label ID="Label19" runat="server" Text="No se permite consignar letras acentuadas ni símbolos ni caracteres especiales" CssClass="TextoComentario"></asp:Label>
                <asp:Label ID="txtErrorApellido4" runat="server" CssClass="text-danger"></asp:Label>                
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label47" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Género:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:DropDownList ID="DdlSexo4" runat="server" Width="300px" CssClass="form-control" onchange="EvalSexo4()" ></asp:DropDownList>
              <asp:Label ID="lblErrorDdlsexo4" runat="server" CssClass="text-danger"></asp:Label>
              <asp:TextBox ID="TextBoxDesexo4"  placeholder="Descripción" runat="server"  CssClass="form-control pull-left" Width="200px" MaxLength="50"></asp:TextBox> 
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
              <label>Provincia:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
                <asp:DropDownList ID="ddlProvincia4" runat="server" CssClass="form-control"
                    AutoPostBack="True"></asp:DropDownList>
                <asp:Label ID="txtErrorProvincia4" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label49" runat="server" style="color: red" Text="[*]"></asp:Label>
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
              <asp:DropDownList ID="ddlLocalidades4" runat="server" CssClass="form-control" ></asp:DropDownList>
          </div>
          <div class="col-md-4 col-left">
              <asp:Label ID="Label50" runat="server" style="color: red" Text="[*]"></asp:Label>
              <asp:Label ID="txtErrorLocalidades4" runat="server" CssClass="text-danger"></asp:Label>            
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
                <asp:TextBox ID="txtCP4" runat="server" Width="90px" Text="" CssClass="form-control"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender8" runat="server"
                    TargetControlID="txtCP4" 
                    Mask="9999"
                    MessageValidatorTip="true" 
                    MaskType="Number" 
                    AcceptNegative="Left" 
                    ErrorTooltipEnabled="True">
                </asp:MaskedEditExtender>
                <asp:Label ID="txtErrorCP4" runat="server" CssClass="text-danger"></asp:Label>   
          </div>
          <div class="col-md-6 col-left">
             <asp:Label ID="Label51" runat="server" style="color: red" Text="[*]"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
      </div>
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-3 col-centered" style="text-align: center">
              <label>Domicilio:</label>
          </div>
          <div class="col-md-3 col-centered" style="text-align: left">
              <asp:TextBox ID="txtDomicilio4" runat="server" MaxLength="100" Columns="30"  CssClass="form-control"
                ToolTip="Domocilio completo"></asp:TextBox>
              <asp:Label ID="txtErrorDomicilio4" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-4 col-left">
             <asp:Label ID="Label52" runat="server" style="color: red" Text="[*]"></asp:Label>
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
            <label for="txtPrefijo4">0</label> 
        </div>
        <div class="col-md-6 col-centered" style="text-align: left"> 
            &nbsp;
            <asp:TextBox ID="txtPrefijo4" placeholder="Prefijo" runat="server" CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="TxtNumero4" placeholder="Teléfono" runat="server" Width="230px" MaxLength="8" CssClass="form-control pull-left"></asp:TextBox>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
     <div class="row">
        <div class="col-md-1 col-left">
        </div>
        <div class="col-md-3 col-centered" style="text-align: center">
            <label>Teléfono Celular: </label>
        </div>
        <div class="col-md-1 text-left" style="width: 30px">
            <label for="txtPrefijoCelular4" >549</label> 
        </div>
        <div class="col-md-1 col-centered" style="text-align: left">
            <asp:TextBox ID="txtPrefijoCelular4" placeholder="Prefijo" runat="server"  CssClass="form-control pull-left" Width="90px" MaxLength="3"></asp:TextBox>                
        </div>
        <div class="col-md-1 text-right" style="width: 10px">
            <label for="txtNumeroCelular4" > 15 </label> 
        </div>
        <div class="col-md-2 text-right">
            <asp:TextBox ID="txtNumeroCelular4" placeholder="Teléfono" runat="server" CssClass="form-control pull-left" Width="230px" MaxLength="8"></asp:TextBox>
            <asp:Label ID="TxtErrorTelefono4" runat="server" CssClass="text-danger"></asp:Label>
        </div>
        <div class="col-md-2 col-left">
           <asp:Label ID="Label53" runat="server" style="color: red" Text="[*]"></asp:Label>
        </div>
        <div class="col-md-1 text-right">
        </div>
     </div>
      <!-- End of Tesorero -->
      <br />
      <!-- DDJJ -->
      <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:label CssClass="observacion" runat="server">Los datos consignados tienen carácter de Declaración Jurada y deberán actualizarse toda vez que produzca cualquier cambio en relación a lo declarado precedentemente.</asp:label>
             <br />
             <asp:CheckBox ID="chkAcepto" runat="server" Text="Acepto" />&nbsp;&nbsp;<asp:Label ID="Label54" runat="server" style="color: red" Text="[*]"></asp:Label>      
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
       <!-- End of DDJJ -->
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
          <div class="col-md-9 col-centered" style="text-align: center; font-family:'Trebuchet MS'; font-size:small">
               <!-- Gridview -->
               <asp:GridView ID="GridViewPalabras" runat="server" AllowSorting="True"  CssClass="table table-bordered"
                   AutoGenerateColumns="False" DataKeyNames="codigo">
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
        <!-- Palabras a publicar -->
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
             <asp:label CssClass="observacion" runat="server">Autorizo al Instituto Nacional del Teatro a que los datos consignados sean de acceso público (en virtud de lo establecido por la Ley 25.326 de Protección de Datos
                     Personales) y sirvan de utilidad para uso estadístico y para la implementación de políticas públicas para el sector cultural</asp:label>
             <br />
             <asp:CheckBox ID="checkAutorizoPublicar" runat="server" Text="Acepto" Visible="True" /> &nbsp;&nbsp;<asp:Label ID="Label55" runat="server" style="color: red" Text="[*]"></asp:Label>
             <asp:Label ID="lblErrorcheckAutorizoPublicar" runat="server" CssClass="text-danger"></asp:Label>
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <!-- End of Palabras a publicar -->
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
       <div class="row">
          <div class="col-md-1 col-left">
          </div>
          <div class="col-md-10 col-centered" style="text-align: center">
              <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" /> 
              <asp:Button ID="btnCancelar" runat="server" class="btn btn-warning" Text="Cancelar" /> &nbsp;&nbsp;<asp:Label ID="Label56" runat="server" style="color: red" Text="[*]"></asp:Label>   
          </div>
          <div class="col-md-1 text-right">
          </div>
       </div>
       <br />
       <br />

   </div>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
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
        (function ($) {
            $(document).ready(function () {
                $('#datetimepicker4').datepicker({
                    language: 'es',
                    autoclose: true,
                    todayHighlight: true
                });
            });
        })(jQuery);
        (function ($) {
            $(document).ready(function () {
                $('#datetimepicker5').datepicker({
                    language: 'es',
                    autoclose: true,
                    todayHighlight: true
                });
            });
        })(jQuery);
        (function ($) {
            $(document).ready(function () {
                $('#datetimepicker6').datepicker({
                    language: 'es',
                    autoclose: true,
                    todayHighlight: true
                });
            });
        })(jQuery);

        $(document).ready(function () {
            EvalSexo1();
            EvalSexo2();
            EvalSexo3();
            EvalSexo4();

            $("#aspnetForm").validate({
                rules: {
                     <%=txtCUIL1.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtApellido1.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtNombre1.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtCP1.UniqueID %>: {                       
                        required: true,
                        range: [1000 , 9999]
                    },
                     <%=txtDomicilio1.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtCUIL2.UniqueID %>: {                       
                        required: false
                    },
                     <%=txtApellido2.UniqueID %>: {                       
                        required: false
                    },
                     <%=txtNombre2.UniqueID %>: {                       
                        required: false
                    },
                     <%=txtCP2.UniqueID %>: {                       
                        required: false,
                        range: [1000 , 9999]
                    },
                     <%=txtDomicilio2.UniqueID %>: {                       
                        required: false
                    },
                     <%=txtCUIL3.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtApellido3.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtNombre3.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtCP3.UniqueID %>: {                       
                        required: true,
                        range: [1000 , 9999]
                    },
                     <%=txtDomicilio3.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtCUIL4.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtApellido4.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtNombre4.UniqueID %>: {                       
                        required: true
                    },
                     <%=txtCP4.UniqueID %>: {                       
                        required: true,
                        range: [1000 , 9999]
                    }
                }, messages: {
                    <%=txtCUIL1.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtApellido1.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtNombre1.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtCP1.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        range:  "<span  style='color:red'>* Debe ingresar entre 1000 y 9999 *</span>"
                    },
                    <%=txtDomicilio1.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtCUIL2.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtApellido2.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtNombre2.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtCP2.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        range:  "<span  style='color:red'>* Debe ingresar entre 1000 y 9999 *</span>"
                    },
                    <%=txtDomicilio2.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtCUIL3.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtApellido3.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtNombre3.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtCP3.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        range:  "<span  style='color:red'>* Debe ingresar entre 1000 y 9999 *</span>"
                    },
                    <%=txtDomicilio3.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtCUIL4.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtApellido4.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtNombre4.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>"
                    },
                    <%=txtCP4.UniqueID %>:{
                        required: "<span  style='color:red'>* campo requerido *</span>",
                        range:  "<span  style='color:red'>* Debe ingresar entre 1000 y 9999 *</span>"
                    }
               }
            });
        });
    </script>       

    <script type="text/javascript">
    function validate(key) {
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
        function EvalSexo1() {
            var e = document.getElementById("<%=ddlSexo1.ClientID %>");
            var desc = document.getElementById("<%=TextBoxDesexo1.ClientID %>");
            var sCodigo = e.options[e.selectedIndex].value;
            if (sCodigo == 6) {
                desc.style.display = "block";
            }
            else {
                desc.value = "";
                desc.style.display = "none";
            }
        }
   </script>

   <script type="text/javascript">
        function EvalSexo2() {
            var e = document.getElementById("<%=DdlSexo2.ClientID %>");
            var desc = document.getElementById("<%=TextBoxDesexo2.ClientID %>");
            var sCodigo = e.options[e.selectedIndex].value;
            if (sCodigo == 6) {
                desc.style.display = "block";
            }
            else {
                desc.value = "";
                desc.style.display = "none";
            }
        }
   </script>

   <script type="text/javascript">
        function EvalSexo3() {
            var e = document.getElementById("<%=DdlSexo3.ClientID %>");
            var desc = document.getElementById("<%=TextBoxDesexo3.ClientID %>");
            var sCodigo = e.options[e.selectedIndex].value;
            if (sCodigo == 6) {
                desc.style.display = "block";
            }
            else {
                desc.value = "";
                desc.style.display = "none";
            }
        }
   </script>

   <script type="text/javascript">
        function EvalSexo4() {
            var e = document.getElementById("<%=DdlSexo4.ClientID %>");
            var desc = document.getElementById("<%=TextBoxDesexo4.ClientID %>");
            var sCodigo = e.options[e.selectedIndex].value;
            if (sCodigo == 6) {
                desc.style.display = "block";
            }
            else {
                desc.value = "";
                desc.style.display = "none";
            }
        }
   </script>


   <script type="text/javascript">
        function validatea(key) {
        var keycode = (key.which) ? key.which : key.keyCode;
         if ((keycode > 64 && keycode < 91) || (keycode > 96 && keycode < 123) || keycode == 8 || keycode == 241 || keycode == 209  || keycode == 32  || keycode == 34  || keycode == 39) {
            return true;
            }
        else
            {
            return false;
            }
        }
   </script>

   <script type="text/javascript">
        function validaten(key) {
        var keycode = (key.which) ? key.which : key.keyCode;
         if ((keycode > 64 && keycode < 91) || (keycode > 96 && keycode < 123) || keycode == 8 || keycode == 241 || keycode == 209  || keycode == 32 ) {
            return true;
            }
        else
            {
            return false;
            }
        }
   </script>      
   
    <%--<script type="text/javascript">
         (function ($) {
           $(document).ready(function () {
               alert("entro");
               $('#InFechaActa').datepicker({
                   alert("entro 2");
                   language: 'es',
                   autoclose: true,
                   todayHighlight: true
               });
         });
         })(jQuery);        
    </script>--%>

</asp:Content>
