<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="integrantes.aspx.vb" Inherits="INTeatroDig.integrantes" 
    title="Registro de Integrantes" Culture="es-AR"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>  
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
 <div id="Datos" runat="server" class="container-fluid">
  
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
          <h3>Registro de Integrantes</h3>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/menFinal.aspx" CssClass="linksBold">Volver</asp:HyperLink>
      </div>
   </div>
   <br />
   <br />
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
          <label> Nº de CUIT / CUIL:</label>
      </div>
      <div class="col-md-3 col-centered" style="text-align: left">
          <asp:TextBox ID="txtCUIT" runat="server" CssClass="form-control"></asp:TextBox>
          <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
               TargetControlID="txtCUIT" 
               Mask="99 99999999 9"
               MessageValidatorTip="true" 
               MaskType="Number" 
               AcceptNegative="Left" 
               ErrorTooltipEnabled="True">
          </asp:MaskedEditExtender>
          <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
               ControlExtender="MaskedEditExtender1"  
               ControlToValidate="txtCUIT"   
               IsValidEmpty="False"   
               EmptyValueMessage="Debe ingresar el CUIT o CUIL"  
               InvalidValueMessage="CUIT / CUIL inválido"  
               EmptyValueBlurredText=""   
               InvalidValueBlurredMessage=""   
               MaximumValueBlurredMessage=""   
               MinimumValueBlurredText=""  
               Display="Dynamic"   
               TooltipMessage="Ingrese su CUIT o CUIL sin espacios ni caracteres especiales, solo números" >
           </asp:MaskedEditValidator>                  
      </div>
      <div class="col-md-4 col-left">
      </div>
      <div class="col-md-1 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered" style="text-align: center">
          <asp:Label ID="txtErrorIntegrantes" runat="server" CssClass="text-danger"></asp:Label>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>
   <div class="row">
      <div class="col-md-1 col-left">
      </div>
      <div class="col-md-3 col-centered" style="text-align: center">
      </div>
      <div class="col-md-3 col-centered" style="text-align: left">
          <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" />
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
           <asp:GridView ID="GridView1" runat="server" 
               AllowSorting="true"
               AllowPaging="true" 
               DataSourceID="SqlDataSource1"
               CssClass="table table-bordered"
               PageSize="10"
               PageIndex="0"
               DataKeyNames = "codigo, idIntegrante">
               <Columns>
                   <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" HeaderText="id" DataField="idIntegrante" SortExpression="idIntegrante" ReadOnly="True" Visible="false" />
                   <asp:BoundField ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderText="Código Registro" DataField="codigo" SortExpression="codigo" />
                   <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" HeaderText="CUIT / CUIL" DataField="CUIL" SortExpression="CUIL" ReadOnly="True" />
                   <asp:BoundField ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderText="Nombre y Apellido" DataField="nombreCompleto" SortExpression="nombreCompleto" />
                   <asp:CommandField
                       ItemStyle-Width="160"
                       ButtonType = "Image"
                       ShowEditButton = "False"
                       ShowDeleteButton = "True"
                       DeleteImageUrl = "images/btnDesvincular.gif" DeleteText="Desvincular"
                       EditImageUrl = "images/btnEdit.gif" EditText="Edit"
                       UpdateImageUrl = "images/btnUpdate.gif" UpdateText="Update" 
                       CancelText = "Cancel" CancelImageUrl = "images/btnCancel.gif" 
                       ControlStyle-Font-Bold="True" />
                    </Columns>
           </asp:GridView>

           <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" ID="SqlDataSource1"
               runat="server"
               SelectCommand = "SELECT I.idIntegrante AS idIntegrante, I.codigoRegistro AS codigo, I.CUIL AS CUIL, I.fechaAlta AS fechaAlta,
                                LTrim(RTrim(R.nombre)) + ' ' + LTrim(RTrim(R.apellido)) AS nombreCompleto
                                FROM Integrantes I INNER JOIN Regisdig R ON I.CUIL = R.CUIL WHERE I.codigoRegistro = @codigo AND I.fechaBaja IS NULL 
                                ORDER BY nombreCompleto"
               DeleteCommand = "UPDATE Integrantes SET fechaBaja = getdate() WHERE idIntegrante = @idIntegrante"
               CancelSelectOnNullParameter="false">
               <SelectParameters>
                   <asp:Parameter Name="codigo" Type="Int32" />
               </SelectParameters>
               <DeleteParameters>
                   <asp:Parameter Name="idIntegrante" Type="Int32" />
               </DeleteParameters>
           </asp:SqlDataSource>
      </div>
      <div class="col-md-2 text-right">
      </div>
   </div>

  </div>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        EnableScriptLocalization="true" 
        EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>  

</asp:Content>
