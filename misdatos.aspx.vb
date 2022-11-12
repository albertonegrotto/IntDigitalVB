Public Partial Class misdatos
    Inherits System.Web.UI.Page
    Dim quien As usuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If quien Is Nothing Then
            quien = CType(Session("usuario"), usuario)
        End If
        Session("codigo") = quien.Codigo

        SqlDataSource1.SelectParameters("codigo").DefaultValue = quien.Codigo
        grillamisregistros.DataBind()
        SqlDataSource2.SelectParameters("id").DefaultValue = quien.Codigo
        GridView1.DataBind()
        lblNombre.Text = quien.Nombre

        Dim cn As SqlClient.SqlConnection = New SqlClient.SqlConnection(SqlConex)
        Dim vinculaciones As Integer = 0
        Dim registros As Integer = 0
        Dim Cuil As Decimal = quien.Usuario
        cn.Open()
        Dim sql As String = "select count(*) From INTEGRANTES where CUIL=" & Cuil & " and fechaBaja is null"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            vinculaciones = dr.GetInt32(0)
        End While
        dr.Close()
        cn.Close()
        sql = "SELECT count(r.codigo) FROM REGISDIG g, REGISTRO r WHERE g.CUIL=" & Cuil & " and r.RESPONSABLE=g.CODIGO and r.FECHBAJA is null"
        cn.Open()
        Dim Psqld As New SqlClient.SqlCommand(sql, cn)
        Dim drd As SqlClient.SqlDataReader = Psqld.ExecuteReader
        While drd.Read()
            registros = drd.GetInt32(0)
        End While
        drd.Close()
        cn.Close()
        If vinculaciones = 0 And registros <> 0 Then
            hfTab.Value = "home"
        Else
            hfTab.Value = "profile"
        End If
    End Sub

    Function getpersona() As String
        Return "<strong>Persona: </strong><span class='datolight'>" + quien.Persona + "</span>"
    End Function

    Function getCodigo() As String
        Return "<strong>Código de Ingreso: </strong><span class='datolight'>" + quien.Codigo.ToString + "</span>"
    End Function

    Function getCuit() As String
        Return "<strong>CUIT/CUIL: </strong><span class='datolight'>" + quien.Usuario.ToString + "</span>"
    End Function

    Function getSexo() As String
        If quien.Persona = "HUMANA" Then
            Return "<strong>Género: </strong><span class='datolight'>" + quien.Sexo.ToString + "</span>"
        Else
            Return "<strong>Personería: </strong><span class='datolight'>" + quien.personeria + "</span>"
        End If
    End Function

    Function getDomicilio() As String
        Return "<strong>Domicilio: </strong><span class='datolight'>" + quien.Direccion.ToString + "</span>"
    End Function

    Function getLocalidad() As String
        Return "<strong>Localidad: </strong><span class='datolight'>" + quien.localidad.ToString + "</span>"
    End Function

    Function getProvincia() As String
        Return "<strong>Provincia: </strong><span class='datolight'>" + quien.Provincia.ToString + "</span>"
    End Function

    Function getEmail() As String
        Return "<strong>Email: </strong><span class='datolight'>" + quien.Email.ToString + "</span>"
    End Function

    Function getTel() As String
        Return "<strong>Teléfono Particular: </strong><span class='datolight'>" + quien.TelefonoParticular + "</span>"

    End Function

    Function getCelu() As String
        If quien.Persona = "FISICA" Then
            Return "<strong>Teléfono Celular: </strong><span class='datolight'>" + quien.TelefonoCelular + "</span>"
        Else
            Return ""
        End If

    End Function

    Protected Sub verificar()
        quien = CType(Session("usuario"), usuario)
        Dim Codigo As Integer = quien.Codigo
        Dim Cuil As Decimal = 0
        Dim cn As SqlClient.SqlConnection = New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim sql As String = "select cuil From REGISDIG where codigo=" & Codigo
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            Cuil = dr.GetDecimal(0)
        End While
        dr.Close()
        cn.Close()
        For Each row As GridViewRow In GridView1.Rows
            Dim Cbx As CheckBox = TryCast(row.FindControl("CheckBox1"), CheckBox)
            Dim IdRegistro As String = RTrim(row.Cells(3).Text)
            Dim Verificado As String = RTrim(row.Cells(6).Text)
            If Cbx.Checked = True And Verificado = "&nbsp;" Then
                sql = "update integrantes set verificado=getdate() where codigoregistro=" & IdRegistro & " and cuil=" & Cuil
                cn.Open()
                Dim Cmd As New SqlClient.SqlCommand(sql, cn)
                Try
                    Cmd.ExecuteNonQuery()
                Catch ex As Exception
                End Try
                cn.Close()
            End If
        Next
        GridView1.DataBind()
    End Sub

    Private Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Cbx As CheckBox = TryCast(e.Row.FindControl("CheckBox1"), CheckBox)
            Dim Verificado As String = RTrim(e.Row.Cells(6).Text)
            If Verificado = "&nbsp;" Then
                Cbx.Enabled = True
            Else
                Cbx.Enabled = False
            End If
        End If
    End Sub

End Class