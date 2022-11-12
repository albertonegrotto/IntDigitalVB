<%@ Page Title="Documentación" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="AdjuntosLista.aspx.vb"
     Inherits="INTeatroDig.AdjuntosLista" MaintainScrollPositionOnPostBack="True" %>

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
           <h2>Actualización de Documentación Adjunta</h2>
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
                <asp:GridView ID="GridView1" runat="server" AllowSorting="true" AllowPaging="true"  DataSourceID="SqlDataSource1"
                    CssClass="table table-bordered" AutoGenerateColumns="False"  PageSize="20"  PageIndex="0" DataKeyNames = "codigo">
                    <HeaderStyle/>
                    <Columns>
                        <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" HeaderText="Id" DataField="codigo" SortExpression="codigo" ReadOnly="True" visible="false" />
                        <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" HeaderText="Sector" DataField="sector" SortExpression="sector" visible="false" />
                        <asp:BoundField ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderText="Correo electrónico" DataField="email" SortExpression="email" visible="false" />
                        <asp:BoundField ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderText="Página web" DataField="pagina" SortExpression="pagina" ReadOnly="True" visible="false" />                       
                        <asp:BoundField ItemStyle-Width="220" ItemStyle-HorizontalAlign="Center" HeaderText="Tipo de Registro" DataField="sector" SortExpression="sector" />
                        <asp:BoundField ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderText="Denominación" DataField="denominacion" SortExpression="denominacion" />
                        <asp:BoundField ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha Alta" DataField="fechAlta" SortExpression="fechAlta" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="&nbsp;Actualizar&nbsp;" HeaderStyle-Width="50" HeaderStyle-Font-Size="10" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"> 
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLinkModificar" runat="server" NavigateUrl="" Text="Actualización" CssClass="links"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" ID="SqlDataSource1"
                    runat="server"
                    SelectCommand = "SELECT codigo,0 sector,'CV '+rtrim(apellido)+' '+rtrim(nombre) denominacion, email,'' pagina, fechAlta, fechBaja 
                                        FROM regisdig WHERE codigo = @codigo AND fechBaja IS NULL and PERSONA=1
                                     union
                                     SELECT codigo, sector, denominacion, email, pagina, fechAlta, fechBaja 
                                        FROM registro WHERE responsable = @codigo AND fechBaja IS NULL and sector in (1,2,3,4,5)
                                     ORDER BY codigo"
                    CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter Name="codigo" Type="Int32" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="codigo" Type="Int32" />
                    </DeleteParameters>
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
