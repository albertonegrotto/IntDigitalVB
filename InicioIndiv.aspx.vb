Public Partial Class InicioIndiv
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            inicializa()

            'If User.Identity.IsAuthenticated Then
            '    inicializa()
            'Else
            '    Response.Clear()
            '    Response.Redirect("http://www.inteatro.gob.ar", False)
            'End If
        End If
    End Sub

    Private Sub inicializa()
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim sql As String = "select '' as codigo,'Seleccione Provincia ' as descrip union select codigo,descrip from provin where codigo>=2 and codigo<=94 order by codigo"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        ddlProvincia.DataSource = dr
        ddlProvincia.DataTextField = "descrip"
        ddlProvincia.DataValueField = "codigo"
        ddlProvincia.DataBind()
        cn.Close()
        dr.Close()
        cn.Open()
        Dim sql1 As String = "select '' as codigo,'Seleccione Persona ' as descrip union select codigo,descrip from personas order by codigo"
        Dim Psql1 As New SqlClient.SqlCommand(sql1, cn)
        Dim dr1 As SqlClient.SqlDataReader = Psql1.ExecuteReader
        ddlPersona.DataSource = dr1
        ddlPersona.DataTextField = "descrip"
        ddlPersona.DataValueField = "codigo"
        ddlPersona.DataBind()
        dr1.Close()
        Dim sql2 As String = "select '' as codigo,'Seleccione ' as descrip union select distinct Tipo codigo, Tipo descrip from EntidadSociedad order by codigo"
        Dim Psql2 As New SqlClient.SqlCommand(sql2, cn)
        Dim drw As SqlClient.SqlDataReader = Psql2.ExecuteReader
        ddlEntidadSociedad.DataSource = drw
        ddlEntidadSociedad.DataTextField = "descrip"
        ddlEntidadSociedad.DataValueField = "codigo"
        ddlEntidadSociedad.DataBind()
        drw.Close()
        cn.Close()
        LabelEntidad.Visible = False
        LabelCategoria.Visible = False
        LabelProvincia.Visible = False
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        Dim wCodn_Prov As Integer = 0 'ddlProvincia.SelectedItem.Value
        Dim wCodn_Perso As Integer = ddlPersona.SelectedItem.Value
        Dim wEntSoc As String = ddlEntidadSociedad.SelectedItem.Value
        Dim wCat As Integer
        Try
            wCat = ddlCategoria.SelectedItem.Value
        Catch ex As Exception
            wCat = 0
        End Try
        FailureText.Text = ""
        If wCodn_Perso = 0 Then
            FailureText.Text = "Seleccione tipo de persona"
            Return
        End If
        'Borro la variable de sesión para asegurar que no se muestre el menú de impresión
        Session.Remove("USER_ID")
        'Borro para que no arrastre en las confirmaciones
        Session.Remove("CUIT")
        Session.Remove("CUIT_TEMP")
        If wCodn_Perso > 0 And (wCodn_Perso = 1 Or (wCodn_Perso = 2 And wEntSoc <> "" And wCat > 0)) Then
            Session.Add("id_provincia", wCodn_Prov)
            Session.Add("id_persona", wCodn_Perso)
            Session.Add("entidad_sociedad", wEntSoc)
            Session.Add("categoria", wCat)
            If wCodn_Perso = 1 Then
                Response.Clear()
                Response.Redirect("InicioIndivFis.aspx", False)
            Else
                Response.Clear()
                Response.Redirect("InicioIndivJur.aspx", False)
            End If
        Else
            FailureText.Text = "Falta Completar Datos"
        End If
    End Sub

    Protected Sub ddlPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPersona.SelectedIndexChanged
        If ddlPersona.SelectedItem.Value = 2 Then
            DatosSoc.Visible = True
            LabelEntidad.Visible = True
            LabelCategoria.Visible = True
        Else
            DatosSoc.Visible = False
            LabelEntidad.Visible = False
            LabelCategoria.Visible = False
        End If
    End Sub

    Protected Sub ddlEntidadSociedad_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlEntidadSociedad.SelectedIndexChanged
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim sql As String = "select '' as codigo,'Seleccione Categoría ' as descrip union select codigo, descrip from Sectores where " & ddlEntidadSociedad.SelectedItem.Value & " = 1"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        ddlCategoria.DataSource = dr
        ddlCategoria.DataTextField = "descrip"
        ddlCategoria.DataValueField = "codigo"
        ddlCategoria.DataBind()
        cn.Close()
        dr.Close()
    End Sub
End Class