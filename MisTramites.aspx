<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="MisTramites.aspx.vb" Inherits="INTeatroDig.MisTramites" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="Datos" runat="server" class="container-fluid">

     <div class="row">
        <div class="col-md-2 col-left">
        </div>
        <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Mis Trámites</h2>
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
                        <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" HeaderText="Expediente" DataField="numero" SortExpression="numero" ReadOnly="True" visible="false" />
                        <asp:BoundField ItemStyle-Width="22" ItemStyle-HorizontalAlign="Center" HeaderText="Año" DataField="ano" SortExpression="ano" visible="false" />
                        <asp:BoundField ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderText="Fecha" DataField="fecha" SortExpression="fecha" visible="false" />
                        <asp:BoundField ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderText="Asunto" DataField="asunto" SortExpression="asunto" ReadOnly="True" visible="false" />                       
                        <asp:BoundField ItemStyle-Width="220" ItemStyle-HorizontalAlign="Center" HeaderText="Descripción" DataField="descripcion" SortExpression="descripcion" />
                        <asp:BoundField ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center" HeaderText="Sctor Actual" DataField="depenactual" SortExpression="depenactual" />
                        <asp:BoundField ItemStyle-Width="140" ItemStyle-HorizontalAlign="Center" HeaderText="Desde" DataField="desde" SortExpression="desde"/>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" ID="SqlDataSource1"
                    runat="server"
                    SelectCommand = "select m.numero, m.ano,convert(char(10),m.fecori,103) as fecha,m.asunto,m.descripcion,t.descripcio as DepenActual,convert(char(10),v.mfecmov,103) Desde
                                     from REGISDIG g, SIGELTRA.dbo.MAESTRO m, SIGELTRA.dbo.tdepe t, SIGELTRA.dbo.movim v, REGISTRO r where m.beneficiario=r.REGISTRO and g.CODIGO=r.RESPONSABLE 
                                     and g.CUIL=@cuil and m.umadepe=t.codigo and v.mtipo=m.tipo and v.mnumero=m.numero and v.mano=m.ano and v.mpase=m.umopase 
"
                    CancelSelectOnNullParameter="false">
                    <SelectParameters>
                        <asp:Parameter Name="cuil" Type="Decimal" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:Parameter Name="cuil" Type="Decimal" />
                    </DeleteParameters>
                </asp:SqlDataSource>
	            <!-- End of Gridview -->
          </div>
          <div class="col-md-2 text-right">
          </div>
     </div>
     <br />
     <br />
  </div> 

</asp:Content>
