<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="registroImpresion.aspx.vb" Inherits="INTeatroDig.registroImpresion" 
    title="Registro / Impresión de Registro" Culture="es-AR" MaintainScrollPositionOnPostBack="True"%>

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
           <h2>Registro / Impresión de Constancias</h2>
        </div>
        <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/menuFinal.aspx" CssClass="linksBold">Volver</asp:HyperLink>
        </div>
     </div>
     <br />
     <div class="row">
        <div class="col-md-2 col-left">
        </div>
        <div class="col-md-8 col-centered" style="text-align: center; font-family:'Trebuchet MS'; font-size:small">
             <!-- Gridview -->
                <asp:GridView ID="GridView1" runat="server" 
                    AllowSorting="true"
                    AllowPaging="true" 
                    DataSourceID="SqlDataSource1"
                    CssClass="table table-bordered"
                    AutoGenerateColumns="False"
                    PageSize="10"
                    PageIndex="0"
                    DataKeyNames = "codigo">
                    <Columns>
                        <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Id" DataField="codigo" SortExpression="codigo" ReadOnly="True" visible="false" />                       
                        <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Tipo de Registro" DataField="sector" SortExpression="sector" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Denominación" DataField="denominacion" SortExpression="denominacion" />
                        <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Fecha de Alta" DataField="fechAlta" SortExpression="fechAlta" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Planilla de Confirmación de Datos" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"> 
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkValidacion" runat="server" NavigateUrl="&" Text="Ir" CssClass="links"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Constancia de Registro" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"> 
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkConstancia" runat="server" NavigateUrl="&" Text="Ir" CssClass="links" ></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" ID="SqlDataSource1"
                    runat="server"
                    SelectCommand = "SELECT r.codigo,s.descrip as sector, r.fechAlta, r.registro,r.denominacion  
                                     FROM registro r, sectores s WHERE r.responsable = @codigo AND fechBaja IS NULL
                                       and r.SECTOR=s.CODIGO ORDER BY codigo"
                    CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter Name="codigo" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
	            <!-- End of Gridview -->
        </div>
        <div class="col-md-2 col-left">
        </div>      
     </div>
  
  </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
        EnableScriptLocalization="true" 
        EnableScriptGlobalization="true">
    </asp:ToolkitScriptManager>  

</asp:Content>
