<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Desvincular.aspx.vb" Inherits="INTeatroDig.Desvincular"
    Title="Mis Vinculaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.0/css/font-awesome.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
  <div id="tablaDatos" runat="server" class="container-fluid">

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-centered page-institucional" style="text-align: center">
           <h2>Mis Vinculaciones</h2>
      </div>
      <div class="col-md-2 text-left">
          <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/menuFinal.aspx" CssClass="linksBold">Volver</asp:HyperLink>
      </div>
   </div>
   <div class="row">
      <div class="col-md-2 col-left">
      </div>
      <div class="col-md-8 col-left">
        <asp:GridView ID="GridView1" runat="server"
            AllowSorting="True"
            AllowPaging="True"
            DataSourceID="SqlDataSource1"
            AutoGenerateColumns="False"
            DataKeyNames="idIntegrante"
            CssClass="table table-bordered">
            <Columns>
                <asp:BoundField DataField="IDregisdig" HeaderText="IDregisdig"
                    InsertVisible="False" ReadOnly="True" SortExpression="IDregisdig" Visible="false" />
                <asp:BoundField DataField="IDIntegrante" HeaderText="IDIntegrante"
                    InsertVisible="False" ReadOnly="True" SortExpression="IDIntegrante" Visible="false" />
                <asp:BoundField DataField="TipoRegistro" HeaderText="TipoRegistro"
                    SortExpression="TipoRegistro" />
                <asp:BoundField DataField="IDRegistro" HeaderText="IDRegistro"
                    InsertVisible="False" ReadOnly="True" SortExpression="IDRegistro" Visible="false" />
                <asp:BoundField DataField="Denominacion" HeaderText="Denominacion"
                    SortExpression="Denominacion" />
                <asp:BoundField DataField="Alta" HeaderText="Alta" SortExpression="Alta" />
                <asp:TemplateField HeaderText=" ">
                    <ItemTemplate>
                        <asp:Button ID="deleteButton" runat="server" CommandName="Delete" Text="Desvincular" class="btn btn-warning"
                            OnClientClick="return confirm('Está Seguro que desea Eliminar esta vinculación?');" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:INTeatroDig %>" SelectCommand="select d.CODIGO IDregisdig, a.IDIntegrante, c.Descrip TipoRegistro, b.codigo IDRegistro, b.denominacion Denominacion , b.FechAlta Alta
                                 from integrantes a inner join registro b on a.codigoRegistro = b.CODIGO inner join sectores c on b.sector = c.codigo 
                                 inner join regisdig d on a.CUIL = d.CUIL where d.Codigo= @id and a.fechaBaja is null"
            DeleteCommand="update Integrantes set fechaBaja=getdate() WHERE idIntegrante = @idIntegrante and fechaBaja is null"
            CancelSelectOnNullParameter="false">
            <SelectParameters>
                <asp:ControlParameter ControlID="HiddenField1" Name="id" PropertyName="Value" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="idIntegrante" Type="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>
      </div>
      <div class="col-md-2 col-left">
      </div>
   </div> 

  </div>
</asp:Content>
