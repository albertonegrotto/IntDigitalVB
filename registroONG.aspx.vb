Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Public Class registroONG
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Dim quien As usuario
    Dim sAccion As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim nCodigo As Integer
        Dim aProvincias() As Integer = {0, 0, 0, 0, 0}
        Dim aLocalidades() As Integer = {0, 0, 0, 0, 0}
        quien = CType(Session("usuario"), usuario)

        If Not Page.IsPostBack Then
            'La primera vez
            sAccion = Request.QueryString("accion")
            nCodigo = Request.QueryString("codigo")
            Session("USER_ID") = quien.Codigo
            Session("CUIT") = quien.Usuario
            Session("SECTOR") = 8
            Session("sAccion") = sAccion
            If sAccion = "M" Then
                HyperLinkBack.Attributes.Add("href", "RegistroLista.aspx")
            End If
            'Cargo la gridviewPalabras
            'SqlDataSourcePalabras.SelectCommand = RegistroModulo.SetSQLGridviewPalabras()
            'SqlDataSourcePalabras.SelectParameters("codigo").DefaultValue = Session("USER_ID")
            'GridViewPalabras.DataBind()
            'GridViewPalabras.Visible = True

            Dim wcuit As Decimal = Session("CUIT")
            Dim wentidad As Integer = 0
            Dim sql As String = "SELECT identidadSociedad from REGISDIG WHERE CUIL = " & wcuit
            Dim Psql As New SqlClient.SqlCommand(sql, cn)
            cn.Open()
            Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
            While dr.Read()
                wentidad = dr.GetInt32(0)
            End While
            dr.Close()
            cn.Close()
            If wentidad >= 20 And wentidad <= 23 Then
                'Sociedades
                ultiacta.Visible = False
                ultiacta2.Visible = False
                vigencia.Visible = False
                LabelFechaConstitucion.InnerText = "Fecha Contrato Social"
                If wentidad = 20 Or wentidad = 21 Then
                    ' SA y SRL
                    LabelPresidente.InnerText = "Director 1"
                    LabelVicePresidente.InnerText = "Director 2"
                    LabelSecretario.InnerText = "Director 3"
                    Labeltesorero.InnerText = "Director 4"
                End If
                If wentidad = 22 Or wentidad = 23 Then
                    'De Hecho o Personas
                    LabelPresidente.InnerText = "Asociado 1"
                    LabelVicePresidente.InnerText = "Asociado 2"
                    LabelSecretario.InnerText = "Asociado 3"
                    Labeltesorero.InnerText = "Asociado 4"
                End If
            End If
            Session("Entidad") = wentidad

            Inicializar(aProvincias, aLocalidades)
            If sAccion.ToUpper = "A" Then
                BtnGuardar.Text = "Confirmar Registro"
                Dim wprov As Integer = quien.codprovin
                ddlProvincias.SelectedValue = wprov
                ddlProvincias.Enabled = False
            ElseIf sAccion.ToUpper = "M" Then
                CargarDatos(nCodigo)
                BtnGuardar.Text = "Confirmar Actualización de Registro"
                Session.Add("CODIGO", nCodigo)
            End If
        Else
            'La respuesta
            MaintainScrollPositionOnPostBack = True
        End If

    End Sub

    Private Sub Inicializar(ByRef aProvincias() As Integer, ByRef aLocalidades() As Integer)
        Dim cn As New SqlClient.SqlConnection(SqlConex)

        'Sectores / Grupos
        cn.Open()
        Dim sql As String = "SELECT codigo, descrip FROM Sectores WHERE codigo =" & Session("Entidad")
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        ddlSectores.DataSource = dr
        ddlSectores.DataTextField = "descrip"
        ddlSectores.DataValueField = "codigo"
        ddlSectores.DataBind()
        dr.Close()
        cn.Close()

        'Provincias
        cn.Open()
        Dim sql2 As String = "Select 0 codigo, 'Seleccione Provincia' descrip union SELECT codigo,descrip FROM Provin where region is not null ORDER BY codigo" ' WHERE codigo = " & Session("PROVINCIA")
        Dim Psql2 As New SqlClient.SqlCommand(sql2, cn)
        Dim dr2 As SqlClient.SqlDataReader = Psql2.ExecuteReader
        ddlProvincias.DataSource = dr2
        ddlProvincias.DataTextField = "descrip"
        ddlProvincias.DataValueField = "codigo"
        ddlProvincias.DataBind()
        dr2.Close()
        cn.Close()

        'Autoridades provincias
        cn.Open()
        Dim sql21 As String = "Select 0 as codigo, 'Seleccione Provincia' as descrip union SELECT codigo,descrip FROM Provin ORDER BY codigo"
        Dim Psql21 As New SqlClient.SqlCommand(sql21, cn)
        Dim dr21 As SqlClient.SqlDataReader = Psql21.ExecuteReader
        ddlProvincia1.DataSource = dr21
        ddlProvincia1.DataTextField = "descrip"
        ddlProvincia1.DataValueField = "codigo"
        ddlProvincia1.DataBind()
        dr21.Close()
        cn.Close()

        'ddlProvincia1.SelectedValue = Session("PROVINCIA")
        cn.Open()
        Dim sql22 As String = "Select 0 as codigo, ' Seleccione Provincia' as descrip union SELECT codigo,descrip FROM Provin ORDER BY codigo"
        Dim Psql22 As New SqlClient.SqlCommand(sql22, cn)
        Dim dr22 As SqlClient.SqlDataReader = Psql22.ExecuteReader
        ddlProvincia2.DataSource = dr22
        ddlProvincia2.DataTextField = "descrip"
        ddlProvincia2.DataValueField = "codigo"
        ddlProvincia2.DataBind()
        'ddlProvincia2.SelectedValue = Session("PROVINCIA")
        dr22.Close()
        cn.Close()

        cn.Open()
        Dim sql23 As String = "Select 0 as codigo, 'Seleccione Provincia' as descrip union SELECT codigo,descrip FROM Provin ORDER BY codigo"
        Dim Psql23 As New SqlClient.SqlCommand(sql23, cn)
        Dim dr23 As SqlClient.SqlDataReader = Psql23.ExecuteReader
        ddlProvincia3.DataSource = dr23
        ddlProvincia3.DataTextField = "descrip"
        ddlProvincia3.DataValueField = "codigo"
        ddlProvincia3.DataBind()
        'ddlProvincia3.SelectedValue = Session("PROVINCIA")
        dr23.Close()
        cn.Close()

        cn.Open()
        Dim sql24 As String = "Select 0 as codigo, 'Seleccione Provincia' as descrip union SELECT codigo,descrip FROM Provin ORDER BY codigo"
        Dim Psql24 As New SqlClient.SqlCommand(sql24, cn)
        Dim dr24 As SqlClient.SqlDataReader = Psql24.ExecuteReader
        ddlProvincia4.DataSource = dr24
        ddlProvincia4.DataTextField = "descrip"
        ddlProvincia4.DataValueField = "codigo"
        ddlProvincia4.DataBind()
        'ddlProvincia4.SelectedValue = Session("PROVINCIA")
        dr24.Close()
        cn.Close()

        'Autoridades localidades
       
        'Vigencia de las autoridades
        cn.Open()
        Dim sql7 As String = "select 0 as codigo,' Seleccione la vigencia' as descrip union select codigo, descrip from vigencia order by descrip"
        Dim Psql7 As New SqlClient.SqlCommand(sql7, cn)
        Dim dr7 As SqlClient.SqlDataReader = Psql7.ExecuteReader
        ddlVigencias.DataSource = dr7
        ddlVigencias.DataTextField = "descrip"
        ddlVigencias.DataValueField = "codigo"
        ddlVigencias.DataBind()
        dr7.Close()
        cn.Close()

        cn.Open()
        Dim sql8 As String = "select 0 as codigo,'Seleccione' as descrip union select codigo,descrip from sexo order by codigo"
        Dim Psql8 As New SqlClient.SqlCommand(sql8, cn)
        Dim dr8 As SqlClient.SqlDataReader = Psql8.ExecuteReader
        ddlSexo1.DataSource = dr8
        ddlSexo1.DataTextField = "descrip"
        ddlSexo1.DataValueField = "codigo"
        ddlSexo1.DataBind()
        dr8.Close()
        cn.Close()

        cn.Open()
        Dim sql9 As String = "select 0 as codigo,'Seleccione' as descrip union select codigo,descrip from sexo order by codigo"
        Dim Psql9 As New SqlClient.SqlCommand(sql9, cn)
        Dim dr9 As SqlClient.SqlDataReader = Psql9.ExecuteReader
        DdlSexo2.DataSource = dr9
        DdlSexo2.DataTextField = "descrip"
        DdlSexo2.DataValueField = "codigo"
        DdlSexo2.DataBind()
        dr9.Close()
        cn.Close()

        cn.Open()
        Dim sql10 As String = "select 0 as codigo,'Seleccione' as descrip union select codigo,descrip from sexo order by codigo"
        Dim Psql10 As New SqlClient.SqlCommand(sql10, cn)
        Dim dr10 As SqlClient.SqlDataReader = Psql8.ExecuteReader
        DdlSexo3.DataSource = dr10
        DdlSexo3.DataTextField = "descrip"
        DdlSexo3.DataValueField = "codigo"
        DdlSexo3.DataBind()
        dr10.Close()
        cn.Close()

        cn.Open()
        Dim sql11 As String = "select 0 as codigo,'Seleccione' as descrip union select codigo,descrip from sexo order by codigo"
        Dim Psql11 As New SqlClient.SqlCommand(sql11, cn)
        Dim dr11 As SqlClient.SqlDataReader = Psql11.ExecuteReader
        DdlSexo4.DataSource = dr11
        DdlSexo4.DataTextField = "descrip"
        DdlSexo4.DataValueField = "codigo"
        DdlSexo4.DataBind()
        dr11.Close()
        cn.Close()

        If aProvincias(1) <> 0 Then
            ddlProvincia1.Text = aProvincias(1)
            CargaLocalidades(ddlLocalidades1, ddlProvincia1.SelectedValue)
        End If
        If aLocalidades(1) <> 0 Then
            ddlLocalidades1.Text = aLocalidades(1)
        End If

        If aProvincias(2) <> 0 Then
            ddlProvincia2.Text = aProvincias(2)
            CargaLocalidades(ddlLocalidades2, ddlProvincia2.SelectedValue)
        End If
        If aLocalidades(2) <> 0 Then
            ddlLocalidades2.Text = aLocalidades(2)
        End If

        If aProvincias(3) <> 0 Then
            ddlProvincia3.Text = aProvincias(3)
            CargaLocalidades(ddlLocalidades3, ddlProvincia3.SelectedValue)
        End If
        If aLocalidades(3) <> 0 Then
            ddlLocalidades3.Text = aLocalidades(3)
        End If

        If aProvincias(4) <> 0 Then
            ddlProvincia4.Text = aProvincias(4)
            CargaLocalidades(ddlLocalidades4, ddlProvincia4.SelectedValue)
        End If
        If aLocalidades(4) <> 0 Then
            ddlLocalidades4.Text = aLocalidades(4)
        End If

    End Sub

    Private Sub InicializarLocalidades()

    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        FailureText.Text = ""
        Dim sResult As String
        Dim bSaved As Boolean

        If Not ValidarDatos() Then
            Return
        End If

        If BtnGuardar.Text = "Confirmar Registro" Then
            bSaved = GuardarDatos()
        Else
            bSaved = ActualizarDatos()
        End If

        If Not bSaved Then
            txtErrorAcepto.Text = "Se produjo un error al guardar los datos, por favor intente mas tarde"
            Return
        End If
        Dim wautoridad As Integer = 0
        'Enviar email
        Dim sIdRegistro As String = IIf(Session("CODIGO_REGISTRO") Is Nothing, Session("CODIGO"), Session("CODIGO_REGISTRO"))
        Dim sSector As String = RTrim(ddlSectores.SelectedItem.Text)
        Dim sProvincia As String = RTrim(ddlProvincias.SelectedItem.Text)

        Dim nTipoMail As Integer
        Dim sSubject As String
        Dim sBody As String

        If BtnGuardar.Text = "Confirmar Registro" Then
            nTipoMail = MAIL_ALTA_REGISTRO
            sSubject = "Solicitud de Inscripción de Registro Nº " & sIdRegistro
        Else
            nTipoMail = MAIL_MODIF_REGISTRO
            sSubject = "Solicitud de Actualización de Registro Nº " & sIdRegistro
        End If
        Dim wentidad As Integer = Session("Entidad")
        Dim sql As String = ""
        cn.Open()
        If wentidad <= 19 Then
            'Entidades
            sql = "select * from autoridades where codigoRegistro=" & sIdRegistro & " and autoridad=1 and cuil= " & txtCUIL1.Text
        End If
        If wentidad >= 20 And wentidad <= 21 Then
            'SA SRL
            sql = "select * from autoridades where codigoRegistro=" & sIdRegistro & " and autoridad=5 and cuil= " & txtCUIL1.Text
        End If
        If wentidad >= 22 And wentidad <= 24 Then
            'SA SRL
            sql = "select * from autoridades where codigoRegistro=" & sIdRegistro & " and autoridad=6 and cuil= " & txtCUIL1.Text
        End If
        Dim PsqlP As New SqlClient.SqlCommand(sql, cn)
        Dim drP As SqlClient.SqlDataReader = PsqlP.ExecuteReader
        If drP.HasRows Then
            ' Ya existe
            cn.Close()
            'drP.Close()
        Else
            Dim wcuil As Decimal = 0
            Try
                wcuil = CDec(txtCUIL1.Text)
            Catch ex As Exception
                wcuil = 0
            End Try
            Dim wfecha As Date = DateTime.Now
            Try
                wfecha = CDate(TextNacim1.Value)
            Catch ex As Exception
            End Try
            Dim wfechnac As String = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            Dim wapellido As String = txtApellido1.Text
            Dim wnombre As String = txtNombre1.Text
            Dim wsexo As Integer = ddlSexo1.SelectedValue
            If wsexo = 0 Then
                FailureText.Text = "Seleccione Género"
                lblErrorddlSexo1.Focus()
                Return
            End If
            Dim wprovincia As Integer = ddlProvincia1.SelectedValue
            Dim wlocalidad As Integer = ddlLocalidades1.SelectedValue
            Dim wcopost As Integer = 0
            Try
                wcopost = CInt(txtCP1.Text)
            Catch ex As Exception
                wcopost = 0
            End Try
            Dim wdomicilio As String = txtDomicilio1.Text
            Dim wprefijo As Integer = 0
            Try
                wprefijo = CInt(txtPrefijo1.Text)
            Catch ex As Exception
                wprefijo = 0
            End Try
            Dim wtelefono As Integer = 0
            Try
                wtelefono = CInt(txtNumero1.Text)
            Catch ex As Exception
                wtelefono = 0
            End Try
            Dim wpreficelu As Integer = 0
            Try
                wpreficelu = CInt(txtPrefijoCelular1.Text)
            Catch ex As Exception
                wpreficelu = 0
            End Try
            Dim wcelular As Integer = 0
            Try
                wcelular = CInt(txtNumeroCelular1.Text)
            Catch ex As Exception
                wcelular = 0
            End Try
            If wentidad <= 19 Then
                'Entidades
                wautoridad = 1
            End If
            If wentidad >= 20 And wentidad <= 21 Then
                'SA SRL
                wautoridad = 5
            End If
            If wentidad >= 22 And wentidad <= 24 Then
                'SA SRL
                wautoridad = 6
            End If

            sql = "Execute alta_autoridades @wcodigoRegistro,@wAUTORIDAD,@wCUIL," &
               "@WFECHNAC,@WAPELLIDO,@WNOMBRE,@WSEXO,@WPROVINCIA,@WLOCALIDAD,@WCOPOST," &
               "@WDOMICILIO,@WPREFIJO,@WTELEFONO,@WPREFICELU,@WCELULAR"
            Dim cmd As New SqlClient.SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@wcodigoRegistro", sIdRegistro)
            cmd.Parameters.AddWithValue("@wAUTORIDAD", wautoridad)
            cmd.Parameters.AddWithValue("@wCUIL", wcuil)
            cmd.Parameters.AddWithValue("@WFECHNAC", wfechnac)
            cmd.Parameters.AddWithValue("@WAPELLIDO", wapellido)
            cmd.Parameters.AddWithValue("@WNOMBRE", wnombre)
            cmd.Parameters.AddWithValue("@WSEXO", wsexo)
            cmd.Parameters.AddWithValue("@WPROVINCIA", wprovincia)
            cmd.Parameters.AddWithValue("@WLOCALIDAD", wlocalidad)
            cmd.Parameters.AddWithValue("@WCOPOST", wcopost)
            cmd.Parameters.AddWithValue("@WDOMICILIO", wdomicilio)
            cmd.Parameters.AddWithValue("@WPREFIJO", wprefijo)
            cmd.Parameters.AddWithValue("@WTELEFONO", wtelefono)
            cmd.Parameters.AddWithValue("@WPREFICELU", wpreficelu)
            cmd.Parameters.AddWithValue("@WCELULAR", wcelular)
            cn.Close()
            cn.Open()
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
            End Try
            cn.Close()
        End If

        If txtCUIL2.Text.Length > 0 Then
            If wentidad <= 19 Then
                'Entidades
                wautoridad = 2
            End If
            If wentidad >= 20 And wentidad <= 21 Then
                'SA SRL
                wautoridad = 5
            End If
            If wentidad >= 22 And wentidad <= 24 Then
                'SA SRL
                wautoridad = 6
            End If
            cn.Open()
            sql = "select * from autoridades where codigoRegistro=" & sIdRegistro & " and autoridad=" & wautoridad & " and cuil= " & txtCUIL2.Text
            Dim PsqlV As New SqlClient.SqlCommand(sql, cn)
            Dim drV As SqlClient.SqlDataReader = PsqlV.ExecuteReader
            If drV.HasRows Then
                ' Ya existe
                cn.Close()
                drV.Close()
            Else
                Dim wcuil As Decimal = 0
                Try
                    wcuil = CDec(txtCUIL2.Text)
                Catch ex As Exception
                    wcuil = 0
                End Try
                Dim wfecha As Date = DateTime.Now
                Try
                    wfecha = CDate(TextNacim2.Value)
                Catch ex As Exception
                End Try
                Dim wfechnac As String = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
                Dim wapellido As String = txtApellido2.Text
                Dim wnombre As String = txtNombre2.Text
                Dim wsexo As Integer = DdlSexo2.SelectedValue
                If wsexo = 0 Then
                    FailureText.Text = "Seleccione Género"
                    Return
                End If
                Dim wprovincia As Integer = ddlProvincia2.SelectedValue
                Dim wlocalidad As Integer = ddlLocalidades2.SelectedValue
                Dim wcopost As Integer = 0
                Try
                    wcopost = CInt(txtCP2.Text)
                Catch ex As Exception
                    wcopost = 0
                End Try
                Dim wdomicilio As String = txtDomicilio2.Text
                Dim wprefijo As Integer = 0
                Try
                    wprefijo = CInt(txtPrefijo2.Text)
                Catch ex As Exception
                    wprefijo = 0
                End Try
                Dim wtelefono As Integer = 0
                Try
                    wtelefono = CInt(txtNumero2.Text)
                Catch ex As Exception
                    wtelefono = 0
                End Try
                Dim wpreficelu As Integer = 0
                Try
                    wpreficelu = CInt(txtPrefijoCelular2.Text)
                Catch ex As Exception
                    wpreficelu = 0
                End Try
                Dim wcelular As Integer = 0
                Try
                    wcelular = CInt(txtNumeroCelular2.Text)
                Catch ex As Exception
                    wcelular = 0
                End Try

                sql = "Execute alta_autoridades @wcodigoRegistro,@wAUTORIDAD,@wCUIL," &
                   "@WFECHNAC,@WAPELLIDO,@WNOMBRE,@WSEXO,@WPROVINCIA,@WLOCALIDAD,@WCOPOST," &
                   "@WDOMICILIO,@WPREFIJO,@WTELEFONO,@WPREFICELU,@WCELULAR"
                Dim cmd As New SqlClient.SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@wcodigoRegistro", sIdRegistro)
                cmd.Parameters.AddWithValue("@wAUTORIDAD", wautoridad)
                cmd.Parameters.AddWithValue("@wCUIL", wcuil)
                cmd.Parameters.AddWithValue("@WFECHNAC", wfechnac)
                cmd.Parameters.AddWithValue("@WAPELLIDO", wapellido)
                cmd.Parameters.AddWithValue("@WNOMBRE", wnombre)
                cmd.Parameters.AddWithValue("@WSEXO", wsexo)
                cmd.Parameters.AddWithValue("@WPROVINCIA", wprovincia)
                cmd.Parameters.AddWithValue("@WLOCALIDAD", wlocalidad)
                cmd.Parameters.AddWithValue("@WCOPOST", wcopost)
                cmd.Parameters.AddWithValue("@WDOMICILIO", wdomicilio)
                cmd.Parameters.AddWithValue("@WPREFIJO", wprefijo)
                cmd.Parameters.AddWithValue("@WTELEFONO", wtelefono)
                cmd.Parameters.AddWithValue("@WPREFICELU", wpreficelu)
                cmd.Parameters.AddWithValue("@WCELULAR", wcelular)
                cn.Close()
                cn.Open()
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                End Try
                cn.Close()
            End If
        End If

        If wentidad <= 19 Then
            'Entidades
            wautoridad = 3
        End If
        If wentidad >= 20 And wentidad <= 21 Then
            'SA SRL
            wautoridad = 5
        End If
        If wentidad >= 22 And wentidad <= 24 Then
            'SA SRL
            wautoridad = 6
        End If
        If Len(txtCUIL3.Text.Trim) > 0 Then
            cn.Open()
            sql = "select * from autoridades where codigoRegistro=" & sIdRegistro & " and autoridad=" & wautoridad & " and cuil= " & txtCUIL3.Text
            Dim PsqlS As New SqlClient.SqlCommand(sql, cn)
            Dim drS As SqlClient.SqlDataReader = PsqlS.ExecuteReader
            If drS.HasRows Then
                ' Ya existe
                cn.Close()
                'drS.Close()
            Else
                Dim wcuil As Decimal = 0
                Try
                    wcuil = CDec(txtCUIL3.Text)
                Catch ex As Exception
                    wcuil = 0
                End Try
                Dim wfecha As Date = DateTime.Now
                Try
                    wfecha = CDate(TextNacim3.Value)
                Catch ex As Exception
                End Try
                Dim wfechnac As String = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
                Dim wapellido As String = txtApellido3.Text
                Dim wnombre As String = txtNombre3.Text
                Dim wsexo As Integer = DdlSexo3.SelectedValue
                If wsexo = 0 Then
                    FailureText.Text = "Seleccione Género"
                    Return
                End If
                Dim wprovincia As Integer = ddlProvincia3.SelectedValue
                Dim wlocalidad As Integer = ddlLocalidades3.SelectedValue
                Dim wcopost As Integer = 0
                Try
                    wcopost = CInt(txtCP3.Text)
                Catch ex As Exception
                    wcopost = 0
                End Try
                Dim wdomicilio As String = txtDomicilio3.Text
                Dim wprefijo As Integer = 0
                Try
                    wprefijo = CInt(txtPrefijo3.Text)
                Catch ex As Exception
                    wprefijo = 0
                End Try
                Dim wtelefono As Integer = 0
                Try
                    wtelefono = CInt(TxtNumero3.Text)
                Catch ex As Exception
                    wtelefono = 0
                End Try
                Dim wpreficelu As Integer = 0
                Try
                    wpreficelu = CInt(txtPrefijoCelular3.Text)
                Catch ex As Exception
                    wpreficelu = 0
                End Try
                Dim wcelular As Integer = 0
                Try
                    wcelular = CInt(txtNumeroCelular3.Text)
                Catch ex As Exception
                    wcelular = 0
                End Try

                sql = "Execute alta_autoridades @wcodigoRegistro,@wAUTORIDAD,@wCUIL," &
               "@WFECHNAC,@WAPELLIDO,@WNOMBRE,@WSEXO,@WPROVINCIA,@WLOCALIDAD,@WCOPOST," &
               "@WDOMICILIO,@WPREFIJO,@WTELEFONO,@WPREFICELU,@WCELULAR"
                Dim cmd As New SqlClient.SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@wcodigoRegistro", sIdRegistro)
                cmd.Parameters.AddWithValue("@wAUTORIDAD", wautoridad)
                cmd.Parameters.AddWithValue("@wCUIL", wcuil)
                cmd.Parameters.AddWithValue("@WFECHNAC", wfechnac)
                cmd.Parameters.AddWithValue("@WAPELLIDO", wapellido)
                cmd.Parameters.AddWithValue("@WNOMBRE", wnombre)
                cmd.Parameters.AddWithValue("@WSEXO", wsexo)
                cmd.Parameters.AddWithValue("@WPROVINCIA", wprovincia)
                cmd.Parameters.AddWithValue("@WLOCALIDAD", wlocalidad)
                cmd.Parameters.AddWithValue("@WCOPOST", wcopost)
                cmd.Parameters.AddWithValue("@WDOMICILIO", wdomicilio)
                cmd.Parameters.AddWithValue("@WPREFIJO", wprefijo)
                cmd.Parameters.AddWithValue("@WTELEFONO", wtelefono)
                cmd.Parameters.AddWithValue("@WPREFICELU", wpreficelu)
                cmd.Parameters.AddWithValue("@WCELULAR", wcelular)
                cn.Close()
                cn.Open()
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                End Try
                cn.Close()
            End If
        End If

        If wentidad <= 19 Then
            'Entidades
            wautoridad = 4
        End If
        If wentidad >= 20 And wentidad <= 21 Then
            'SA SRL
            wautoridad = 5
        End If
        If wentidad >= 22 And wentidad <= 24 Then
            'SA SRL
            wautoridad = 6
        End If
        If Len(txtCUIL4.Text.Trim) > 0 Then
            cn.Open()
            sql = "select * from autoridades where codigoRegistro=" & sIdRegistro & " and autoridad=" & wautoridad & " and cuil= " & txtCUIL4.Text
            Dim PsqlT As New SqlClient.SqlCommand(sql, cn)
            Dim drT As SqlClient.SqlDataReader = PsqlT.ExecuteReader
            If drT.HasRows Then
                ' Ya existe
                cn.Close()
                drT = Nothing
            Else
                Dim wcuil As Decimal = 0
                Try
                    wcuil = CDec(txtCUIL4.Text)
                Catch ex As Exception
                    wcuil = 0
                End Try
                Dim wfecha As Date = DateTime.Now
                Try
                    wfecha = CDate(TextNacim4.Value)
                Catch ex As Exception
                End Try
                Dim wfechnac As String = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
                Dim wapellido As String = txtApellido4.Text
                Dim wnombre As String = txtNombre4.Text
                Dim wsexo As Integer = DdlSexo4.SelectedValue
                If wsexo = 0 Then
                    FailureText.Text = "Seleccione Género"
                    Return
                End If
                Dim wprovincia As Integer = ddlProvincia4.SelectedValue
                Dim wlocalidad As Integer = ddlLocalidades4.SelectedValue
                Dim wcopost As Integer = 0
                Try
                    wcopost = CInt(txtCP4.Text)
                Catch ex As Exception
                    wcopost = 0
                End Try
                Dim wdomicilio As String = txtDomicilio4.Text
                Dim wprefijo As Integer = 0
                Try
                    wprefijo = CInt(txtPrefijo4.Text)
                Catch ex As Exception
                    wprefijo = 0
                End Try
                Dim wtelefono As Integer = 0
                Try
                    wtelefono = CInt(TxtNumero4.Text)
                Catch ex As Exception
                    wtelefono = 0
                End Try
                Dim wpreficelu As Integer = 0
                Try
                    wpreficelu = CInt(txtPrefijoCelular4.Text)
                Catch ex As Exception
                    wpreficelu = 0
                End Try
                Dim wcelular As Integer = 0
                Try
                    wcelular = CInt(txtNumeroCelular4.Text)
                Catch ex As Exception
                    wcelular = 0
                End Try

                sql = "Execute alta_autoridades @wcodigoRegistro,@wAUTORIDAD,@wCUIL," &
               "@WFECHNAC,@WAPELLIDO,@WNOMBRE,@WSEXO,@WPROVINCIA,@WLOCALIDAD,@WCOPOST," &
               "@WDOMICILIO,@WPREFIJO,@WTELEFONO,@WPREFICELU,@WCELULAR"
                Dim cmd As New SqlClient.SqlCommand(sql, cn)
                cmd.Parameters.AddWithValue("@wcodigoRegistro", sIdRegistro)
                cmd.Parameters.AddWithValue("@wAUTORIDAD", wautoridad)
                cmd.Parameters.AddWithValue("@wCUIL", wcuil)
                cmd.Parameters.AddWithValue("@WFECHNAC", wfechnac)
                cmd.Parameters.AddWithValue("@WAPELLIDO", wapellido)
                cmd.Parameters.AddWithValue("@WNOMBRE", wnombre)
                cmd.Parameters.AddWithValue("@WSEXO", wsexo)
                cmd.Parameters.AddWithValue("@WPROVINCIA", wprovincia)
                cmd.Parameters.AddWithValue("@WLOCALIDAD", wlocalidad)
                cmd.Parameters.AddWithValue("@WCOPOST", wcopost)
                cmd.Parameters.AddWithValue("@WDOMICILIO", wdomicilio)
                cmd.Parameters.AddWithValue("@WPREFIJO", wprefijo)
                cmd.Parameters.AddWithValue("@WTELEFONO", wtelefono)
                cmd.Parameters.AddWithValue("@WPREFICELU", wpreficelu)
                cmd.Parameters.AddWithValue("@WCELULAR", wcelular)
                cn.Close()
                cn.Open()
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                End Try
                cn.Close()
            End If
        End If
        cn.Close()

        Dim Apellido As String = ""
        Dim Nombre As String = ""
        Dim Denominacion As String = ""
        Dim Persona As Integer = 0
        Dim CUIT As Decimal = Session("CUIT")
        sql = "select apellido,nombre,denominacion,persona from REGISDIG where CUIL=" & CUIT
        cn.Open()
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            Apellido = dr.GetString(0)
            Nombre = dr.GetString(1)
            Denominacion = dr.GetString(2)
            Persona = dr.GetInt32(3)
        End While
        dr.Close()
        cn.Close()

        Dim CodigoRegistro As Decimal = 0
        sql = "select REGISTRO from REGISTRO where CODIGO=" & sIdRegistro
        cn.Open()
        Dim Psqlr As New SqlClient.SqlCommand(sql, cn)
        Dim drr As SqlClient.SqlDataReader = Psqlr.ExecuteReader
        While drr.Read()
            Try
                CodigoRegistro = drr.GetDecimal(0)
            Catch ex As Exception
                CodigoRegistro = 0
            End Try
        End While
        drr.Close()
        cn.Close()

        If wentidad <= 19 Then
            ' ENTIDADES
            If BtnGuardar.Text = "Confirmar Registro" Then
                nTipoMail = MAIL_ALTA_REGISTRO
                sSubject = "INTeatroDigital - Solicitud de Registro de Entidad"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: REGISTRO DE ENTIDAD"
            Else
                nTipoMail = MAIL_MODIF_REGISTRO
                sSubject = "INTeatroDigital - Actualización de Registro de Entidad"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE REGISTRO DE REGISTRO DE ENTIDAD " & "<br />"
                sBody += "REGISTRO INT N°: " & CodigoRegistro & "<br />"
            End If

            sBody += "Quedando ingresados los siguientes datos del Registro: " & "<br />"

            sBody += "Código de Ingreso (sólo para uso del INT): " & sIdRegistro & "<br />"
            sBody += "Tipo: " & sSector & "<br />"
            sBody += "Provincia: " & sProvincia & "<br />"
            If Persona = 1 Then
                sBody += "Responsable del Registro: " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
            Else
                sBody += "Responsable del Registro: " & Denominacion & " - " & CUIT & "<br />"
            End If
            sBody += "Fecha de Constitución: " & TextONG.Value & "<br />"
            sBody += "Fecha de Última Acta: " & TextActa.Value & "<br />"
            sBody += "Vigencia de Autoridades: " & ddlVigencias.SelectedItem.Text & "<br />"
            sBody += "<br />"

            sBody += "Datos del Presidente " & "<br />"
            sBody += "Nº de CUIL: " & txtCUIL1.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & TextNacim1.Value & "<br />"
            sBody += "Apellido y Nombre: " & txtApellido1.Text & " " & txtNombre1.Text & "<br />"
            'sBody += "Género: " & ddlSexo1.SelectedItem.Text & "<br />"
            sBody += "Provincia: " & ddlProvincia1.SelectedItem.Text & "<br />"
            sBody += "Localidad: " & ddlLocalidades1.SelectedItem.Text & "<br />"
            sBody += "Código Postal: " & txtCP1.Text & "<br />"
            sBody += "Domicilio: " & txtDomicilio1.Text & "<br />"
            sBody += "Teléfono Particular: " & txtPrefijo1.Text & " " & txtNumero1.Text & "<br />"
            sBody += "Teléfono Celular: " & txtPrefijoCelular1.Text & " " & txtNumeroCelular1.Text & "<br />"
            sBody += "<br />"

            If txtCUIL2.Text.Length > 0 Then
                sBody += "Datos del Vicepresidente " & "<br />"
                sBody += "Nº de CUIL: " & txtCUIL2.Text & "<br />"
                sBody += "Fecha de Nacimiento: " & TextNacim2.Value & "<br />"
                sBody += "Apellido y Nombre: " & txtApellido2.Text & " " & txtNombre2.Text & "<br />"
                'sBody += "Género: " & DdlSexo2.SelectedItem.Text & "<br />"
                sBody += "Provincia: " & ddlProvincia2.SelectedItem.Text & "<br />"
                sBody += "Localidad: " & ddlLocalidades2.SelectedItem.Text & "<br />"
                sBody += "Código Postal: " & txtCP2.Text & "<br />"
                sBody += "Domicilio: " & txtDomicilio2.Text & "<br />"
                sBody += "Teléfono Particular: " & txtPrefijo2.Text & " " & txtNumero2.Text & "<br />"
                sBody += "Teléfono Celular: " & txtPrefijoCelular2.Text & " " & txtPrefijoCelular2.Text & "<br />"
                sBody += "<br />"
            End If

            If txtCUIL3.Text.Length > 0 Then
                sBody += "Datos del Secretario " & "<br />"
                sBody += "Nº de CUIL: " & txtCUIL3.Text & "<br />"
                sBody += "Fecha de Nacimiento: " & TextNacim3.Value & "<br />"
                sBody += "Apellido y Nombre: " & txtApellido3.Text & " " & txtNombre3.Text & "<br />"
                'sBody += "Género: " & DdlSexo3.SelectedItem.Text & "<br />"
                sBody += "Provincia: " & ddlProvincia3.SelectedItem.Text & "<br />"
                sBody += "Localidad: " & ddlLocalidades3.SelectedItem.Text & "<br />"
                sBody += "Código Postal: " & txtCP3.Text & "<br />"
                sBody += "Domicilio: " & txtDomicilio3.Text & "<br />"
                sBody += "Teléfono Particular: " & txtPrefijo3.Text & " " & TxtNumero3.Text & "<br />"
                sBody += "Teléfono Celular: " & txtPrefijoCelular3.Text & " " & txtPrefijoCelular3.Text & "<br />"
                sBody += "<br />"
            End If

            If txtCUIL4.Text.Length > 0 Then
                sBody += "Datos del Tesorero " & "<br />"
                sBody += "Nº de CUIL: " & txtCUIL4.Text & "<br />"
                sBody += "Fecha de Nacimiento: " & TextNacim4.Value & "<br />"
                sBody += "Apellido y Nombre: " & txtApellido4.Text & " " & txtNombre4.Text & "<br />"
                'sBody += "Género: " & DdlSexo4.SelectedItem.Text & "<br />"
                sBody += "Provincia: " & ddlProvincia4.SelectedItem.Text & "<br />"
                sBody += "Localidad: " & ddlLocalidades4.SelectedItem.Text & "<br />"
                sBody += "Código Postal: " & txtCP4.Text & "<br />"
                sBody += "Domicilio: " & txtDomicilio4.Text & "<br />"
                sBody += "Teléfono Particular: " & txtPrefijo4.Text & " " & TxtNumero4.Text & "<br />"
                sBody += "Teléfono Celular: " & txtPrefijoCelular4.Text & " " & txtPrefijoCelular4.Text & "<br />"
                sBody += "<br />"
            End If

            If BtnGuardar.Text = "Confirmar Registro" Then
                    sBody += "Usted ha realizado el trámite de Registro de ENTIDAD en INTeatroDigital. Estamos trabajando en el" & "<br />"
                Else
                    sBody += "Usted ha realizado el trámite de Actualización de Registro de ENTIDAD en INTeatroDigital. Estamos trabajando en el" & "<br />"
                End If
                sBody += "procesamiento de sus datos. Debe clickear en el link que figura al final de este mensaje, " & "<br />"
                sBody += "con el fin de validar su identidad como usuario. Al hacerlo, se le abrirá en el navegador " & "<br />"
                sBody += "de internet, la plataforma de INTeatroDigital directamente en la sección 'Imprimir Constancias', " & "<br />"
                sBody += "desde la cual deberá emitir y descargar la constancia de registro y enviarla por correo electrónico " & "<br />"
                sBody += "a la Representación del INT correspondiente a su Provincia." & "<br />"
                If BtnGuardar.Text = "Confirmar Registro" Then
                    sBody += "En ese mismo mail deberá adjuntar:" & "<br />"
                    sBody += "- Si el responsable es una ENTIDAD: copia del Acta Constitutiva, del Estatuto, de la Resolución de " & "<br />"
                    sBody += "Personería Jurídica, del Certificado de Vigencia, de la Constancia de AFIP, del frente y dorso del " & "<br />"
                    sBody += "DNI de CADA UNA DE LAS AUTORIDADES REGISTRADAS y del Acta de Designación de Autoridades VIGENTE (si " & "<br />"
                    sBody += "la tuviere; sino la enviará posteriormente adjunta a otro correo electrónico)."
                    sBody += "- Si el responsable es una SOCIEDAD: copia del Contrato Social, de la Resolución de Personería "
                    sBody += "Jurídica, del Certificado de Vigencia, de la Constancia de AFIP y del frente y dorso del DNI de CADA "
                    sBody += "UNA DE LAS AUTORIDADES REGISTRADAS."
                End If
                sBody += "<br />"
                sBody += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá " & "<br />"
                sBody += "en esta dirección de correo electrónico la confirmación definitiva del trámite." & "<br />"
                sBody += "<br />"

                'sBody += Mail.GetTextoAviso(nTipoMail, sSector, sIdRegistro) & "<br />"
                'sBody += "<br />"

                sBody += Mail.GetLink(nTipoMail, sIdRegistro, Session("USER_ID")) & "<br />"
                sBody += "<br />"

                sBody += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
                sBody += "que ve mas arriba en su navegador de internet.<br />"
                sBody += "<br />"
                sBody += "Gracias.<br />"
                sBody += "<br />"
                sBody += "INTeatroDigital.<br />"

                sResult = SendMail(Mail.GetMailTo(Session("USER_ID"), TIPO_PERSONA), sSubject, sBody)
            Else
                ' SOCIEDADES
                If BtnGuardar.Text = "Confirmar Registro" Then
                nTipoMail = MAIL_ALTA_REGISTRO
                sSubject = "INTeatroDigital - Solicitud de Registro de Sociedad"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: REGISTRO DE SOCIEDAD"
            Else
                nTipoMail = MAIL_MODIF_REGISTRO
                sSubject = "INTeatroDigital - Actualización de Registro de Sociedad"
                sBody = "Estimada/o usuaria/o de INTeatroDigital:" & "<br />" & "<br />"
                sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE REGISTRO DE REGISTRO DE SOCIEDAD " & "<br />"
                sBody += "REGISTRO INT N°: " & CodigoRegistro & "<br />"
            End If

            sBody += "Quedando ingresados los siguientes datos del Registro: " & "<br />"

            sBody += "Código de Ingreso (sólo para uso del INT): " & sIdRegistro & "<br />"
            sBody += "Tipo: " & sSector & "<br />"
            sBody += "Provincia: " & sProvincia & "<br />"
            If Persona = 1 Then
                sBody += "Responsable del Registro: " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
            Else
                sBody += "Responsable del Registro: " & Denominacion & " - " & CUIT & "<br />"
            End If
            sBody += "Denominación: " & Denominacion & "<br />"
            sBody += "Fecha de Contrato Social: " & TextONG.Value & "<br />"
            sBody += "<br />"

            sBody += "Datos de Autoridad 1 " & "<br />"
            sBody += "Nº de CUIL: " & txtCUIL1.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & TextNacim1.Value & "<br />"
            sBody += "Apellido y Nombre: " & txtApellido1.Text & " " & txtNombre1.Text & "<br />"
            'sBody += "Género: " & ddlSexo1.SelectedItem.Text & "<br />"
            sBody += "Provincia: " & ddlProvincia1.SelectedItem.Text & "<br />"
            sBody += "Localidad: " & ddlLocalidades1.SelectedItem.Text & "<br />"
            sBody += "Código Postal: " & txtCP1.Text & "<br />"
            sBody += "Domicilio: " & txtDomicilio1.Text & "<br />"
            sBody += "Teléfono Particular: " & txtPrefijo1.Text & " " & txtNumero1.Text & "<br />"
            sBody += "Teléfono Celular: " & txtPrefijoCelular1.Text & " " & txtNumeroCelular1.Text & "<br />"
            sBody += "<br />"

            sBody += "Datos de Autoridad 2 " & "<br />"
            sBody += "Nº de CUIL: " & txtCUIL2.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & TextNacim2.Value & "<br />"
            sBody += "Apellido y Nombre: " & txtApellido2.Text & " " & txtNombre2.Text & "<br />"
            'sBody += "Género: " & DdlSexo2.SelectedItem.Text & "<br />"
            sBody += "Provincia: " & ddlProvincia2.SelectedItem.Text & "<br />"
            sBody += "Localidad: " & ddlLocalidades2.SelectedItem.Text & "<br />"
            sBody += "Código Postal: " & txtCP2.Text & "<br />"
            sBody += "Domicilio: " & txtDomicilio2.Text & "<br />"
            sBody += "Teléfono Particular: " & txtPrefijo2.Text & " " & txtNumero2.Text & "<br />"
            sBody += "Teléfono Celular: " & txtPrefijoCelular2.Text & " " & txtPrefijoCelular2.Text & "<br />"
            sBody += "<br />"

            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "Usted ha realizado el trámite de Registro de SOCIEDAD en INTeatroDigital. Estamos trabajando en el" & "<br />"
            Else
                sBody += "Usted ha realizado el trámite de Actualización de Registro de SOCIEDAD en INTeatroDigital. Estamos trabajando en el" & "<br />"
            End If
            sBody += "procesamiento de sus datos. Debe clickear en el link que figura al final de este mensaje, " & "<br />"
            sBody += "con el fin de validar su identidad como usuario. Al hacerlo, se le abrirá en el navegador " & "<br />"
            sBody += "de internet, la plataforma de INTeatroDigital directamente en la sección 'Imprimir Constancias', " & "<br />"
            sBody += "desde la cual deberá emitir y descargar la constancia de registro y enviarla por correo electrónico " & "<br />"
            sBody += "a la Representación del INT correspondiente a su Provincia." & "<br />"
            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "En ese mismo mail deberá adjuntar:" & "<br />"
                sBody += "copia del Contrato Social, de la Resolución de Personería Jurídica, del Certificado de Vigencia,   " & "<br />"
                sBody += "de la Constancia de AFIP y copia del frente y dorso del DNI de CADA UNA DE LAS AUTORIDADES REGISTRADAS. " & "<br />"
            End If
            sBody += "<br />"
            sBody += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá " & "<br />"
            sBody += "en esta dirección de correo electrónico la confirmación definitiva del trámite." & "<br />"
            sBody += "<br />"

            'sBody += Mail.GetTextoAviso(nTipoMail, sSector, sIdRegistro) & "<br />"
            'sBody += "<br />"

            sBody += Mail.GetLink(nTipoMail, sIdRegistro, Session("USER_ID")) & "<br />"
            sBody += "<br />"

            sBody += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
            sBody += "que ve mas arriba en su navegador de internet.<br />"
            sBody += "<br />"
            sBody += "Gracias.<br />"
            sBody += "<br />"
            sBody += "INTeatroDigital.<br />"

            sResult = SendMail(Mail.GetMailTo(Session("USER_ID"), TIPO_PERSONA), sSubject, sBody)
        End If

        'A la representación provincial:
        Dim sResult3 As String = ""
        Dim wemailprov As String = ""
        cn.Open()
        sql = "select mail from provinciasmail where idprovincia=" & ddlProvincias.SelectedValue
        Dim cdmm As New SqlClient.SqlCommand(sql, cn)
        Dim drpv As SqlClient.SqlDataReader = cdmm.ExecuteReader
        While drpv.Read
            wemailprov = drpv.GetString(0)
        End While
        drpv.Close()
        cn.Close()

        If wentidad <= 19 Then
            'ENTIDADES
            If BtnGuardar.Text = "Confirmar Registro" Then
                sSubject = "INTeatroDigital - Solicitud de Registro de Entidad"
                sBody = "REGISTRO de ENTIDAD: " & ddlSectores.SelectedItem.Text & "<br />" & "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro de Entidad"
                sBody = "ACTUALIZACION DE REGISTRO de ENTIDAD: " & ddlSectores.SelectedItem.Text & "<br />" & "<br />"
                sBody += "REGISTRO INT N°:  " & CodigoRegistro & "<br />"
            End If
            sBody += "Quedando ingresados los siguientes datos del Registro: " & "<br />"
            sBody += "Código de Ingreso (sólo para uso del INT): " & sIdRegistro & "<br />"
            sBody += "Tipo: " & sSector & "<br />"
            sBody += "Provincia: " & sProvincia & "<br />"
            If Persona = 1 Then
                sBody += "Responsable del Registro: " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
            Else
                sBody += "Responsable del Registro: " & Denominacion & " - " & CUIT & "<br />"
            End If
            sBody += "Fecha de Constitución: " & TextONG.Value & "<br />"
            sBody += "Fecha de Última Acta: " & TextActa.Value & "<br />"
            sBody += "Vigencia de Autoridades: " & ddlVigencias.SelectedItem.Text & "<br />"
            sBody += "<br />"
            sBody += "Datos del Presidente " & "<br />"
            sBody += "Nº de CUIL: " & txtCUIL1.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & TextNacim1.Value & "<br />"
            sBody += "Apellido y Nombre: " & txtApellido1.Text & " " & txtNombre1.Text & "<br />"
            'sBody += "Género: " & ddlSexo1.SelectedItem.Text & "<br />"
            sBody += "Provincia: " & ddlProvincia1.SelectedItem.Text & "<br />"
            sBody += "Localidad: " & ddlLocalidades1.SelectedItem.Text & "<br />"
            sBody += "Código Postal: " & txtCP1.Text & "<br />"
            sBody += "Domicilio: " & txtDomicilio1.Text & "<br />"
            sBody += "Teléfono Particular: " & txtPrefijo1.Text & " " & txtNumero1.Text & "<br />"
            sBody += "Teléfono Celular: " & txtPrefijoCelular1.Text & " " & txtNumeroCelular1.Text & "<br />"
            sBody += "<br />"

            If txtCUIL2.Text.Length > 0 Then
                sBody += "Datos del Vicepresidente " & "<br />"
                sBody += "Nº de CUIL: " & txtCUIL2.Text & "<br />"
                sBody += "Fecha de Nacimiento: " & TextNacim2.Value & "<br />"
                sBody += "Apellido y Nombre: " & txtApellido2.Text & " " & txtNombre2.Text & "<br />"
                'sBody += "Género: " & DdlSexo2.SelectedItem.Text & "<br />"
                sBody += "Provincia: " & ddlProvincia2.SelectedItem.Text & "<br />"
                sBody += "Localidad: " & ddlLocalidades2.SelectedItem.Text & "<br />"
                sBody += "Código Postal: " & txtCP2.Text & "<br />"
                sBody += "Domicilio: " & txtDomicilio2.Text & "<br />"
                sBody += "Teléfono Particular: " & txtPrefijo2.Text & " " & txtNumero2.Text & "<br />"
                sBody += "Teléfono Celular: " & txtPrefijoCelular2.Text & " " & txtPrefijoCelular2.Text & "<br />"
                sBody += "<br />"
            End If

            If txtCUIL3.Text.Length > 0 Then
                sBody += "Datos del Secretario " & "<br />"
                sBody += "Nº de CUIL: " & txtCUIL3.Text & "<br />"
                sBody += "Fecha de Nacimiento: " & TextNacim3.Value & "<br />"
                sBody += "Apellido y Nombre: " & txtApellido3.Text & " " & txtNombre3.Text & "<br />"
                'sBody += "Género: " & DdlSexo3.SelectedItem.Text & "<br />"
                sBody += "Provincia: " & ddlProvincia3.SelectedItem.Text & "<br />"
                sBody += "Localidad: " & ddlLocalidades3.SelectedItem.Text & "<br />"
                sBody += "Código Postal: " & txtCP3.Text & "<br />"
                sBody += "Domicilio: " & txtDomicilio3.Text & "<br />"
                sBody += "Teléfono Particular: " & txtPrefijo3.Text & " " & TxtNumero3.Text & "<br />"
                sBody += "Teléfono Celular: " & txtPrefijoCelular3.Text & " " & txtPrefijoCelular3.Text & "<br />"
                sBody += "<br />"
            End If

            If txtCUIL4.Text.Length > 0 Then
                sBody += "Datos del Tesorero " & "<br />"
                sBody += "Nº de CUIL: " & txtCUIL4.Text & "<br />"
                sBody += "Fecha de Nacimiento: " & TextNacim4.Value & "<br />"
                sBody += "Apellido y Nombre: " & txtApellido4.Text & " " & txtNombre4.Text & "<br />"
                'sBody += "Género: " & DdlSexo4.SelectedItem.Text & "<br />"
                sBody += "Provincia: " & ddlProvincia4.SelectedItem.Text & "<br />"
                sBody += "Localidad: " & ddlLocalidades4.SelectedItem.Text & "<br />"
                sBody += "Código Postal: " & txtCP4.Text & "<br />"
                sBody += "Domicilio: " & txtDomicilio4.Text & "<br />"
                sBody += "Teléfono Particular: " & txtPrefijo4.Text & " " & TxtNumero4.Text & "<br />"
                sBody += "Teléfono Celular: " & txtPrefijoCelular4.Text & " " & txtPrefijoCelular4.Text & "<br />"
                sBody += "<br />"
            End If
        Else
            'SOCIEDADES
            If BtnGuardar.Text = "Confirmar Registro" Then
                sSubject = "INTeatroDigital - Solicitud de Registro de Sociedad"
                sBody = "REGISTRO de SOCIEDAD: " & ddlSectores.SelectedItem.Text & "<br />" & "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro de Sociedad"
                sBody = "ACTUALIZACION DE REGISTRO de SOCIEDAD: " & ddlSectores.SelectedItem.Text & "<br />" & "<br />"
                sBody += "REGISTRO INT N°:  " & CodigoRegistro & "<br />"
            End If
            sBody += "Quedando ingresados los siguientes datos del Registro: " & "<br />"
            sBody += "Código de Ingreso (sólo para uso del INT): " & sIdRegistro & "<br />"
            sBody += "Tipo: " & sSector & "<br />"
            sBody += "Provincia: " & sProvincia & "<br />"
            If Persona = 1 Then
                sBody += "Responsable del Registro: " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
            Else
                sBody += "Responsable del Registro: " & Denominacion & " - " & CUIT & "<br />"
            End If
            sBody += "Fecha de Contrato Social: " & TextONG.Value & "<br />"
            sBody += "<br />"

            sBody += "Datos de Autoridad 1 " & "<br />"
            sBody += "Nº de CUIL: " & txtCUIL1.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & TextNacim1.Value & "<br />"
            sBody += "Apellido y Nombre: " & txtApellido1.Text & " " & txtNombre1.Text & "<br />"
            'sBody += "Género: " & ddlSexo1.SelectedItem.Text & "<br />"
            sBody += "Provincia: " & ddlProvincia1.SelectedItem.Text & "<br />"
            sBody += "Localidad: " & ddlLocalidades1.SelectedItem.Text & "<br />"
            sBody += "Código Postal: " & txtCP1.Text & "<br />"
            sBody += "Domicilio: " & txtDomicilio1.Text & "<br />"
            sBody += "Teléfono Particular: " & txtPrefijo1.Text & " " & txtNumero1.Text & "<br />"
            sBody += "Teléfono Celular: " & txtPrefijoCelular1.Text & " " & txtNumeroCelular1.Text & "<br />"
            sBody += "<br />"

            sBody += "Datos de Autoridad 2 " & "<br />"
            sBody += "Nº de CUIL: " & txtCUIL2.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & TextNacim2.Value & "<br />"
            sBody += "Apellido y Nombre: " & txtApellido2.Text & " " & txtNombre2.Text & "<br />"
            'sBody += "Género: " & DdlSexo2.SelectedItem.Text & "<br />"
            sBody += "Provincia: " & ddlProvincia2.SelectedItem.Text & "<br />"
            sBody += "Localidad: " & ddlLocalidades2.SelectedItem.Text & "<br />"
            sBody += "Código Postal: " & txtCP2.Text & "<br />"
            sBody += "Domicilio: " & txtDomicilio2.Text & "<br />"
            sBody += "Teléfono Particular: " & txtPrefijo2.Text & " " & txtNumero2.Text & "<br />"
            sBody += "Teléfono Celular: " & txtPrefijoCelular2.Text & " " & txtPrefijoCelular2.Text & "<br />"
            sBody += "<br />"

        End If

        If sResult = "OK" Then
            Response.Redirect("confirmaRegistro.aspx?r=ok&m=" & sResult)
        Else
            Response.Redirect("confirmaRegistro.aspx?r=er&m=" & sResult)
        End If
        'End of Enviar email

    End Sub

    Protected Function ValidarDatos()

        Dim dFechaActa As Date
        Dim dHoy As Date
        Dim dFecha As Date

        'Limpio los errores
        txtErrorVigencias.Text = ""
        txtErrorLocalidades1.Text = ""
        txtErrorTelefono1.Text = ""
        txtErrorLocalidades2.Text = ""
        txtErrorTelefono2.Text = ""
        txtErrorLocalidades3.Text = ""
        txtErrorTelefono3.Text = ""
        txtErrorLocalidades4.Text = ""
        txtErrorTelefono4.Text = ""
        txtErrorCheckBoxEdad1.Text = ""
        txtErrorCheckBoxEdad2.Text = ""
        txtErrorCheckBoxEdad3.Text = ""
        txtErrorCheckBoxEdad4.Text = ""
        txtErrorCUIL1.Text = ""
        txtErrorCUIL2.Text = ""
        txtErrorCUIL3.Text = ""
        txtErrorCUIL4.Text = ""
        txtErrorFechaONG.Text = ""
        txtErrorFechaActa.Text = ""
        txtErrorFechaNac1.Text = ""
        txtErrorFechaNac2.Text = ""
        txtErrorFechaNac3.Text = ""
        txtErrorFechaNac4.Text = ""
        txtErrorApellido1.Text = ""
        txtErrorNombre1.Text = ""
        txtErrorCP1.Text = ""
        txtErrorApellido2.Text = ""
        txtErrorNombre2.Text = ""
        txtErrorCP2.Text = ""
        txtErrorApellido3.Text = ""
        txtErrorNombre3.Text = ""
        txtErrorCP3.Text = ""
        txtErrorApellido4.Text = ""
        txtErrorNombre4.Text = ""
        txtErrorCP4.Text = ""
        txtErrorDomicilio4.Text = ""
        txtErrorAcepto.Text = ""
        lblErrorddlSexo1.Text = ""
        LblErrorDddlsexo2.Text = ""
        LblErrorDdlsexo3.Text = ""
        lblErrorDdlsexo4.Text = ""
        Dim wentidad As Integer = Session("Entidad")
        Try
            dFecha = CDate(TextONG.Value)
        Catch ex As Exception
            txtErrorFechaONG.Text = "Fecha inválida"
            TextONG.Focus()
            Return False
        End Try

        Try
            If Not ValidarFecha(TextONG.Value.Trim) Then
                txtErrorFechaONG.Text = "Fecha inválida"
                TextONG.Focus()
                Return False
            End If
        Catch ex As Exception
            txtErrorFechaONG.Text = "Fecha inválida"
            TextONG.Focus()
            Return False
        End Try
        If wentidad <= 19 Then
            Try
                dFecha = CDate(TextActa.Value)
            Catch ex As Exception
                txtErrorFechaActa.Text = "Fecha inválida"
                TextActa.Focus()
                Return False
            End Try

            Try
                If Not ValidarFecha(TextActa.Value.Trim) Then
                    txtErrorFechaActa.Text = "Fecha inválida"
                    TextActa.Focus()
                    Return False
                End If
            Catch ex As Exception
                txtErrorFechaActa.Text = "Fecha inválida"
                TextActa.Focus()
                Return False
            End Try
        End If

        'Presidente
        If ddlProvincia1.SelectedValue = 0 Then
            txtErrorProvincia1.Text = "Debe seleccionar una provincia"
            ddlProvincia1.Focus()
            Return False
        End If

        If ddlLocalidades1.SelectedValue = 0 Then
            txtErrorLocalidades1.Text = "Debe seleccionar una localidad"
            ddlLocalidades1.Focus()
            Return False
        End If

        'Valido CUIL
        If Not Validaciones.ValidarCUIT(txtCUIL1.Text.Trim()) Then
            txtErrorCUIL1.Text = "CUIT/CUIL erróneo"
            txtCUIL1.Focus()
            Return False
        End If

        Dim sFecha As Date = DateAndTime.Now
        Try
            sFecha = CDate(TextNacim1.Value)
        Catch ex As Exception
            TextNacim1.Focus()
            txtErrorFechaNac1.Text = "Fecha inválida"
            Return False
        End Try
        Dim sDia As String = Right("0" + Day(sFecha).ToString, 2)
        Dim sMes As String = Right("0" + Month(sFecha).ToString, 2)
        Dim sAno As String = Year(sFecha).ToString

        If Calcular_Edad(sDia + "/" + sMes + "/" + sAno) < 18 Then
            If CheckBoxEdad1.Checked = "false" Then
                txtErrorCheckBoxEdad1.Text = "Debe aceptar los términos para continuar"
                CheckBoxEdad1.Focus()
                Return False
            End If
        End If

        If txtApellido1.Text.Trim = "" Then
            txtErrorApellido1.Text = "Debe ingresar el apellido"
            Return False
        End If

        If txtNombre1.Text.Trim = "" Then
            txtErrorNombre1.Text = "Debe ingresar el nombre"
            Return False
        End If

        If txtCP1.Text.Trim = "" Then
            txtErrorCP1.Text = "Debe ingresar el código postal"
            Return False
        End If

        Try
            dFecha = CDate(TextNacim1.Value)
        Catch ex As Exception
            txtErrorFechaNac1.Text = "Fecha inválida"
            TextNacim1.Focus()
            Return False
        End Try

        Try
            If Not ValidarFecha(TextNacim1.Value.Trim) Then
                txtErrorFechaNac1.Text = "Fecha inválida"
                TextNacim1.Focus()
                Return False
            End If
        Catch ex As Exception
            txtErrorFechaNac1.Text = "Fecha inválida"
            TextNacim1.Focus()
            Return False
        End Try

        If Len(txtPrefijoCelular1.Text.Trim) = 0 Or Len(txtNumeroCelular1.Text.Trim) = 0 Then
            txtErrorTelefono1.Text = "El número de TE Celular es Obligatorio"
            txtPrefijoCelular1.Focus()
            Return False
        End If

        If ddlSexo1.SelectedValue = 0 Then
            lblErrorddlSexo1.Text = "Seleccione Género"
            ddlSexo1.Focus()
            Return False
        End If

        'End of Presidente

        Dim bCargaVice As Boolean
        If wentidad >= 20 And Len(RTrim(txtCUIL2.Text)) = 0 Then
            'Sociedades
            txtErrorCUIL2.Text = "Debe ingresar al menos dos autoridades"
            Return False
        End If

        If wentidad <> 18 Then
            'Vicepresidente
            bCargaVice = False
            If Len(RTrim(txtCUIL2.Text)) > 0 Then
                If ChequearVicepresidente() Then
                    'Si cargó al menos un item, debe cargar todos los datos obligatorios
                    If ddlProvincia2.SelectedValue = 0 Then
                        txtErrorProvincia2.Text = "Debe seleccionar una provincia"
                        ddlProvincia2.Focus()
                        Return False
                    End If

                    If ddlLocalidades2.SelectedValue = 0 Then
                        txtErrorLocalidades2.Text = "Debe seleccionar una localidad"
                        ddlLocalidades2.Focus()
                        Return False
                    End If

                    'Valido CUIL
                    If txtCUIL2.Text.Trim = "" Then
                        txtErrorCUIL2.Text = "Debe ingresar el CUIL"
                        txtCUIL2.Focus()
                        Return False
                    End If

                    If Not Validaciones.ValidarCUIT(txtCUIL2.Text.Trim()) Then
                        txtErrorCUIL2.Text = "CUIT/CUIL erróneo"
                        txtCUIL2.Focus()
                        Return False
                    End If

                    If TextNacim2.Value.Trim = "" Then
                        txtErrorFechaNac2.Text = "Debe ingresar la fecha de nacimiento"
                        TextNacim2.Focus()
                        Return False
                    End If

                    Try
                        sFecha = CDate(TextNacim2.Value)
                    Catch ex As Exception
                        Return False
                    End Try
                    sDia = Right("0" + Day(sFecha).ToString, 2)
                    sMes = Right("0" + Month(sFecha).ToString, 2)
                    sAno = Year(sFecha).ToString
                    If Calcular_Edad(sDia + "/" + sMes + "/" + sAno) < 18 Then
                        If CheckBoxEdad2.Checked = "false" Then
                            txtErrorCheckBoxEdad2.Text = "Debe aceptar los términos para continuar"
                            CheckBoxEdad2.Focus()
                            Return False
                        End If
                    End If

                    If txtApellido2.Text.Trim = "" Then
                        txtErrorApellido2.Text = "Debe ingresar el apellido"
                        txtApellido2.Focus()
                        Return False
                    End If

                    If txtNombre2.Text.Trim = "" Then
                        txtErrorNombre2.Text = "Debe ingresar el nombre"
                        txtNombre2.Focus()
                        Return False
                    End If

                    If txtCP2.Text.Trim = "" Then
                        txtErrorCP2.Text = "Debe ingresar el código postal"
                        txtCP2.Focus()
                        Return False
                    End If

                    If txtDomicilio2.Text.Trim = "" Then
                        txtErrorDomicilio2.Text = "Debe ingresar el domicilio"
                        txtDomicilio2.Focus()
                        Return False
                    End If

                    Try
                        dFecha = CDate(TextNacim2.Value)
                    Catch ex As Exception
                        txtErrorFechaNac2.Text = "Fecha inválida"
                        TextNacim2.Focus()
                        Return False
                    End Try

                    Try
                        If Not ValidarFecha(TextNacim2.Value.Trim) Then
                            txtErrorFechaNac2.Text = "Fecha inválida"
                            TextNacim2.Focus()
                            Return False
                        End If
                    Catch ex As Exception
                        txtErrorFechaNac2.Text = "Fecha inválida"
                        TextNacim2.Focus()
                        Return False
                    End Try

                    If Len(txtPrefijoCelular2.Text.Trim) = 0 Or Len(txtNumeroCelular2.Text.Trim) = 0 Then
                        txtErrorTelefono2.Text = "El número de TE Celular es Obligatorio"
                        txtPrefijoCelular2.Focus()
                        Return False
                    End If

                    If DdlSexo2.SelectedValue = 0 Then
                        LblErrorDddlsexo2.Text = "Seleccione Género"
                        Return False
                    End If

                End If
            End If
            'End of Vicepresidente

            'Secretario
            If wentidad < 20 And Len(RTrim(txtCUIL3.Text)) = 0 Then
                'Entidades
                txtErrorCUIL3.Text = "Debe ingresar los datos del Secretario"
                Return False
            End If

            If Len(RTrim(txtCUIL3.Text)) > 0 Then
                If ddlProvincia3.SelectedValue = 0 Then
                    txtErrorProvincia3.Text = "Debe seleccionar una provincia"
                    ddlProvincia3.Focus()
                    Return False
                End If

                If ddlLocalidades3.SelectedValue = 0 Then
                    txtErrorLocalidades3.Text = "Debe seleccionar una localidad"
                    ddlLocalidades3.Focus()
                    Return False
                End If

                'Valido CUIL
                If Not Validaciones.ValidarCUIT(txtCUIL3.Text.Trim()) Then
                    txtErrorCUIL3.Text = "CUIT/CUIL erróneo"
                    txtCUIL3.Focus()
                    Return False
                End If

                Try
                    sFecha = CDate(TextNacim3.Value)
                Catch ex As Exception
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString
                If Calcular_Edad(sDia + "/" + sMes + "/" + sAno) < 18 Then
                    If CheckBoxEdad3.Checked = "false" Then
                        txtErrorCheckBoxEdad3.Text = "Debe aceptar los términos para continuar"
                        CheckBoxEdad3.Focus()
                        Return False
                    End If
                End If

                If txtApellido3.Text.Trim = "" Then
                    txtErrorApellido3.Text = "Debe ingresar el apellido"
                    Return False
                End If

                If txtNombre3.Text.Trim = "" Then
                    txtErrorNombre3.Text = "Debe ingresar el nombre"
                    Return False
                End If

                If txtCP3.Text.Trim = "" Then
                    txtErrorCP3.Text = "Debe ingresar el código postal"
                    Return False
                End If

                Try
                    dFecha = CDate(TextNacim3.Value)
                Catch ex As Exception
                    txtErrorFechaNac3.Text = "Fecha inválida"
                    TextNacim3.Focus()
                    Return False
                End Try

                Try
                    If Not ValidarFecha(TextNacim3.Value.Trim) Then
                        txtErrorFechaNac3.Text = "Fecha inválida"
                        TextNacim3.Focus()
                        Return False
                    End If
                Catch ex As Exception
                    txtErrorFechaNac3.Text = "Fecha inválida"
                    TextNacim3.Focus()
                    Return False
                End Try

                If Len(txtPrefijoCelular3.Text.Trim) = 0 Or Len(txtNumeroCelular3.Text.Trim) = 0 Then
                    TxtErrorTelefono3.Text = "El número de TE Celular es Obligatorio"
                    txtPrefijoCelular3.Focus()
                    Return False
                End If

                If DdlSexo3.SelectedValue = 0 Then
                    LblErrorDdlsexo3.Text = "Seleccione Género"
                    Return False
                End If
            End If
            'End of Secretario

            If wentidad < 20 And Len(RTrim(txtCUIL4.Text)) = 0 Then
                'Entidades
                txtErrorCUIL4.Text = "Debe ingresar los datos del Tesorero"
                Return False
            End If
            'Tesorero
            If Len(RTrim(txtCUIL4.Text)) > 0 Then
                If ddlProvincia4.SelectedValue = 0 Then
                    txtErrorProvincia4.Text = "Debe seleccionar una provincia"
                    ddlProvincia4.Focus()
                    Return False
                End If

                If ddlLocalidades4.SelectedValue = 0 Then
                    txtErrorLocalidades4.Text = "Debe seleccionar una localidad"
                    ddlLocalidades4.Focus()
                    Return False
                End If

                'Valido CUIL
                If Not Validaciones.ValidarCUIT(txtCUIL4.Text.Trim()) Then
                    txtErrorCUIL4.Text = "CUIT/CUIL erróneo"
                    txtCUIL4.Focus()
                    Return False
                End If

                If txtApellido4.Text.Trim = "" Then
                    txtErrorApellido4.Text = "Debe ingresar el apellido"
                    Return False
                End If

                If txtNombre4.Text.Trim = "" Then
                    txtErrorNombre4.Text = "Debe ingresar el nombre"
                    Return False
                End If

                If txtCP4.Text.Trim = "" Then
                    txtErrorCP4.Text = "Debe ingresar el código postal"
                    Return False
                End If

                Try
                    dFecha = CDate(TextNacim4.Value)
                Catch ex As Exception
                    txtErrorFechaNac4.Text = "Fecha inválida"
                    TextNacim4.Focus()
                    Return False
                End Try

                Try
                    If Not ValidarFecha(TextNacim4.Value.Trim) Then
                        txtErrorFechaNac4.Text = "Fecha inválida"
                        TextNacim4.Focus()
                        Return False
                    End If
                Catch ex As Exception
                    txtErrorFechaNac4.Text = "Fecha inválida"
                    TextNacim4.Focus()
                    Return False
                End Try

                Try
                    sFecha = CDate(TextNacim4.Value)
                Catch ex As Exception
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString
                If Calcular_Edad(sDia + "/" + sMes + "/" + sAno) < 18 Then
                    If CheckBoxEdad4.Checked = "false" Then
                        txtErrorCheckBoxEdad4.Text = "Debe aceptar los términos para continuar"
                        CheckBoxEdad4.Focus()
                        Return False
                    End If
                End If

                If Len(txtPrefijoCelular4.Text.Trim) = 0 Or Len(txtNumeroCelular4.Text.Trim) = 0 Then
                    TxtErrorTelefono4.Text = "El número de TE Celular es Obligatorio"
                    txtPrefijoCelular4.Focus()
                    Return False
                End If

                If txtDomicilio4.Text.Trim = "" Then
                    txtErrorDomicilio4.Text = "Debe ingresar el domicilio"
                    txtDomicilio4.Focus()
                    Return False
                End If

                If DdlSexo4.SelectedValue = 0 Then
                    lblErrorDdlsexo4.Text = "Seleccione Género"
                    Return False
                End If
            End If

            'End of Tesorero

        End If

        If wentidad <= 19 Then
            'Validar ddlvigencias
            If ddlVigencias.Text = 0 Then
                txtErrorVigencias.Text = "Debe seleccionar la vigencia"
                ddlVigencias.Focus()
                Return False
            End If

            If ddlVigencias.Text <> 6 Then  'Vitalicio
                Try
                    sFecha = CDate(TextActa.Value)
                Catch ex As Exception
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString

                dFechaActa = Convert.ToDateTime(sDia & "/" & sMes & "/" & sAno)
                dHoy = Today

                dFechaActa = dFechaActa.AddYears(ddlVigencias.Text)
                If dFechaActa < dHoy Then
                    txtErrorVigencias.Text = "Acta vencida de mas de " & ddlVigencias.Text.ToString & IIf(ddlVigencias.Text = 1, " año", " años")
                    txtErrorVigencias.Text += ", los datos serán ingresados pero no estará habilitado para recibir subsidios"
                    If ViewState("ACTA_VENCIDA") Is Nothing Then
                        ViewState("ACTA_VENCIDA") = True
                        TextActa.Focus()
                        Return False
                    End If
                End If
            End If
        End If

        If wentidad <> 18 Then
            'Chequeo de CUITs repetidos
            If txtCUIL1.Text.Trim = txtCUIL2.Text.Trim Then
                txtErrorCUIL2.Text = "CUIL repetido"
                txtCUIL2.Focus()
                Return False
            End If

            If txtCUIL1.Text.Trim = txtCUIL3.Text.Trim Then
                txtErrorCUIL3.Text = "CUIL repetido"
                txtCUIL3.Focus()
                Return False
            End If

            If txtCUIL1.Text.Trim = txtCUIL4.Text.Trim Then
                txtErrorCUIL4.Text = "CUIL repetido"
                txtCUIL4.Focus()
                Return False
            End If

            If txtCUIL2.Text.Trim = txtCUIL3.Text.Trim Then
                txtErrorCUIL3.Text = "CUIL repetido"
                txtCUIL3.Focus()
                Return False
            End If

            If txtCUIL2.Text.Trim = txtCUIL4.Text.Trim Then
                txtErrorCUIL4.Text = "CUIL repetido"
                txtCUIL4.Focus()
                Return False
            End If

            If txtCUIL3.Text.Trim = txtCUIL4.Text.Trim And Len(txtCUIL3.Text.Trim) > 0 Then
                txtErrorCUIL4.Text = "CUIL repetido"
                txtCUIL4.Focus()
                Return False
            End If

        End If

        If chkAcepto.Checked = False Then
            txtErrorAcepto.Text = "Debe aceptar los términos para continuar"
            chkAcepto.Focus()
            Return False
        End If
        If checkAutorizoPublicar.Checked = False Then
            lblErrorcheckAutorizoPublicar.Text = "Debe aceptar los términos para continuar"
            checkAutorizoPublicar.Focus()
            Return False
        End If

        Return True

    End Function

    Private Function ChequearVicepresidente() As Boolean
        Try
            If txtCUIL2.Text.Trim <> "" Or TextNacim2.Value.Trim <> "" Or
                txtApellido2.Text.Trim <> "" Or txtNombre2.Text.Trim <> "" Or
                txtCP2.Text.Trim <> "" Or txtDomicilio2.Text.Trim <> "" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Function GuardarDatos() As Integer
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sFecha As Date = DateAndTime.Now
        Dim sDia As String
        Dim sMes As String
        Dim sAno As String
        Dim sSQLCmd As String
        Dim mIdRegistro As SqlParameter
        Dim nIdRegistro As Integer
        Dim sFechaONG As String
        Dim sFechaActa As String = ""
        Dim sFechaNac As String
        Dim nSexo As Integer
        Dim sPrefijo1 As Object
        Dim sTelefono1 As Object
        Dim sPrefijoCelular1 As Object
        Dim sNumeroCelular1 As Object
        Dim sPrefijo2 As Object
        Dim sTelefono2 As Object
        Dim sPrefijoCelular2 As Object
        Dim sNumeroCelular2 As Object
        Dim sPrefijo3 As Object
        Dim sTelefono3 As Object
        Dim sPrefijoCelular3 As Object
        Dim sNumeroCelular3 As Object
        Dim sPrefijo4 As Object
        Dim sTelefono4 As Object
        Dim sPrefijoCelular4 As Object
        Dim sNumeroCelular4 As Object
        If ddlProvincias.SelectedValue = 0 Then
            txtErrorVigencias.Text = "Debe seleccionar provincia"
            GuardarDatos = False
            Return False
        End If
        Dim wentidad As Integer = Session("Entidad")
        If wentidad <= 19 Then
            Try
                sFecha = CDate(TextActa.Value)
            Catch ex As Exception
                txtErrorVigencias.Text = "Error Fecha Acta"
                GuardarDatos = False
                Return False
            End Try
            sDia = Right("0" + Day(sFecha).ToString, 2)
            sMes = Right("0" + Month(sFecha).ToString, 2)
            sAno = Year(sFecha).ToString
            sFechaActa = sAno + sMes + sDia
        End If

        Try
            sFecha = CDate(TextONG.Value)
        Catch ex As Exception
            txtErrorVigencias.Text = "Error Fecha ONG"
            GuardarDatos = False
            Return False
        End Try
        sDia = Right("0" + Day(sFecha).ToString, 2)
        sMes = Right("0" + Month(sFecha).ToString, 2)
        sAno = Year(sFecha).ToString
        sFechaONG = sAno + sMes + sDia

        Try
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            'INSERT Registro
            If wentidad <= 19 Then
                sSQLCmd = "INSERT INTO Registro " &
                            "(responsable, sector, provincia, " &
                            "fechConsti, fechActa, vigencia, " &
                            "fechAlta) " &
                        "VALUES " &
                            "(" & Session("USER_ID") & ", " & Session("SECTOR") & ", " & ddlProvincias.SelectedValue & ", " &
                            "Convert(datetime,'" & sFechaONG & "'), Convert(datetime,'" & sFechaActa & "'), " & ddlVigencias.Text & ", " &
                            "getdate())  " &
                        "SET @nIdRegistro = SCOPE_IDENTITY()"
            Else
                sSQLCmd = "INSERT INTO Registro " &
                            "(responsable, sector, provincia, " &
                            "fechConsti,fechAlta) " &
                        "VALUES " &
                            "(" & Session("USER_ID") & ", " & Session("SECTOR") & ", " & ddlProvincias.SelectedValue & ", " &
                            "Convert(datetime,'" & sFechaONG & "'), " &
                            "getdate())  " &
                        "SET @nIdRegistro = SCOPE_IDENTITY()"
            End If

            mIdRegistro = New SqlParameter
            mIdRegistro.ParameterName = "@nIdRegistro"
            mIdRegistro.SqlDbType = SqlDbType.Int
            mIdRegistro.Direction = ParameterDirection.Output
            mIdRegistro.Value = -1

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection

            MyCommand.Parameters.Add(mIdRegistro)
            MyCommand.ExecuteNonQuery()
            nIdRegistro = mIdRegistro.Value
            MyCommand.Dispose()
            'End of INSERT Registro

            'INSERT Presidente
            Try
                sFecha = CDate(TextNacim1.Value)
            Catch ex As Exception
                txtErrorVigencias.Text = "Error Fecha Nacimiento"
                GuardarDatos = False
                Return False
            End Try
            sDia = Right("0" + Day(sFecha).ToString, 2)
            sMes = Right("0" + Month(sFecha).ToString, 2)
            sAno = Year(sFecha).ToString
            sFechaNac = sAno + sMes + sDia

            nSexo = ddlSexo1.SelectedValue

            sPrefijo1 = IIf(txtPrefijo1.Text.Trim <> "", txtPrefijo1.Text.Trim, "NULL")
            sTelefono1 = IIf(txtNumero1.Text.Trim <> "", txtNumero1.Text.Trim, "NULL")
            sPrefijoCelular1 = IIf(txtPrefijoCelular1.Text.Trim <> "", txtPrefijoCelular1.Text.Trim, "NULL")
            sNumeroCelular1 = IIf(txtNumeroCelular1.Text.Trim <> "", txtNumeroCelular1.Text.Trim, "NULL")

            'Borro registros
            sSQLCmd = "DELETE FROM Autoridades WHERE codigoRegistro = " & nIdRegistro
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()
            MyCommand.Dispose()

            Dim wautoridad As Integer = 0
            If wentidad < 20 Then
                wautoridad = 1
            End If
            If wentidad >= 20 And wentidad <= 21 Then
                wautoridad = 5
            End If
            If wentidad >= 22 And wentidad <= 23 Then
                wautoridad = 6
            End If

            sSQLCmd = "INSERT INTO Autoridades " &
                            "(codigoRegistro, autoridad, cuil, " &
                            "fechNac, apellido, nombre, " &
                            "sexo, provincia, localidad, copost, " &
                            "domicilio, prefijo, telefono, " &
                            "preficelu, celular, fechAlta) " &
                        "VALUES " &
                            "(" & nIdRegistro & ", " & wautoridad & ", " & txtCUIL1.Text.Trim & ", " &
                            "Convert(datetime,'" & sFechaNac & "'), '" & txtApellido1.Text.Trim.ToUpper & "', '" & txtNombre1.Text.Trim.ToUpper & "', " &
                            " " & nSexo & ", " & ddlProvincia1.Text & ", " & ddlLocalidades1.Text & ", " & txtCP1.Text.Trim & ", " &
                            "'" & txtDomicilio1.Text.Trim.ToUpper & "', " & sPrefijo1 & ", " & sTelefono1 & ", " &
                            " " & sPrefijoCelular1 & ", " & sNumeroCelular1 & ", getdate())"

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()

            MyCommand.Dispose()
            'End of INSERT Presidente

            'INSERT Vicepresidente
            If wentidad < 20 Then
                wautoridad = 2
            End If

            If Len(RTrim(txtCUIL2.Text)) > 0 Then
                Try
                    sFecha = CDate(TextNacim2.Value)
                Catch ex As Exception
                    txtErrorVigencias.Text = "Error Fecha Nacimiento"
                    GuardarDatos = False
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString
                sFechaNac = sAno + sMes + sDia
                nSexo = DdlSexo2.SelectedValue

                sPrefijo2 = IIf(txtPrefijo2.Text.Trim <> "", txtPrefijo2.Text.Trim, "NULL")
                sTelefono2 = IIf(txtNumero2.Text.Trim <> "", txtNumero2.Text.Trim, "NULL")
                sPrefijoCelular2 = IIf(txtPrefijoCelular2.Text.Trim <> "", txtPrefijoCelular2.Text.Trim, "NULL")
                sNumeroCelular2 = IIf(txtNumeroCelular2.Text.Trim <> "", txtNumeroCelular2.Text.Trim, "NULL")

                sSQLCmd = "INSERT INTO Autoridades " &
                                "(codigoRegistro, autoridad, cuil, " &
                                "fechNac, apellido, nombre, " &
                                "sexo, provincia, localidad, copost, " &
                                "domicilio, prefijo, telefono, " &
                                "preficelu, celular, fechAlta) " &
                            "VALUES " &
                                "(" & nIdRegistro & ", " & wautoridad & ", " & txtCUIL2.Text.Trim & ", " &
                                "Convert(datetime,'" & sFechaNac & "'), '" & txtApellido2.Text.Trim.ToUpper & "', '" & txtNombre2.Text.Trim.ToUpper & "', " &
                                " " & nSexo & ", " & ddlProvincia2.Text & ", " & ddlLocalidades2.Text & ", " & txtCP2.Text.Trim & ", " &
                                "'" & txtDomicilio2.Text.Trim.ToUpper & "', " & sPrefijo2 & ", " & sTelefono2 & ", " &
                                " " & sPrefijoCelular2 & ", " & sNumeroCelular2 & ", getdate())"

                MyCommand = New SqlCommand()
                MyCommand.CommandText = sSQLCmd
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection
                MyCommand.ExecuteNonQuery()
                MyCommand.Dispose()
            End If
            'End of INSERT Vicepresidente

            'INSERT Secretario

            If wentidad < 20 Then
                wautoridad = 3
            End If

            If Len(RTrim(txtCUIL3.Text)) > 0 Then
                Try
                    sFecha = CDate(TextNacim3.Value)
                Catch ex As Exception
                    txtErrorVigencias.Text = "Error Fecha Nacimiento"
                    GuardarDatos = False
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString
                sFechaNac = sAno + sMes + sDia
                nSexo = DdlSexo3.SelectedValue

                sPrefijo3 = IIf(txtPrefijo3.Text.Trim <> "", txtPrefijo3.Text.Trim, "NULL")
                sTelefono3 = IIf(txtNumero3.Text.Trim <> "", txtNumero3.Text.Trim, "NULL")
                sPrefijoCelular3 = IIf(txtPrefijoCelular3.Text.Trim <> "", txtPrefijoCelular3.Text.Trim, "NULL")
                sNumeroCelular3 = IIf(txtNumeroCelular3.Text.Trim <> "", txtNumeroCelular3.Text.Trim, "NULL")

                sSQLCmd = "INSERT INTO Autoridades " &
                            "(codigoRegistro, autoridad, cuil, " &
                            "fechNac, apellido, nombre, " &
                            "sexo, provincia, localidad, copost, " &
                            "domicilio, prefijo, telefono, " &
                            "preficelu, celular, fechAlta) " &
                        "VALUES " &
                            "(" & nIdRegistro & ", 3, " & txtCUIL3.Text.Trim & ", " &
                            "Convert(datetime,'" & sFechaNac & "'), '" & txtApellido3.Text.Trim.ToUpper & "', '" & txtNombre3.Text.Trim.ToUpper & "', " &
                            " " & nSexo & ", " & ddlProvincia3.Text & ", " & ddlLocalidades3.Text & ", " & txtCP3.Text.Trim & ", " &
                            "'" & txtDomicilio3.Text.Trim.ToUpper & "', " & sPrefijo3 & ", " & sTelefono3 & ", " &
                            " " & sPrefijoCelular3 & ", " & sNumeroCelular3 & ", getdate())"

                MyCommand = New SqlCommand()
                MyCommand.CommandText = sSQLCmd
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection
                MyCommand.ExecuteNonQuery()
                MyCommand.Dispose()
            End If

            'End of INSERT Secretario

            'INSERT Tesorero

            If wentidad < 20 Then
                wautoridad = 4
            End If

            If Len(RTrim(txtCUIL4.Text)) > 0 Then
                Try
                    sFecha = CDate(TextNacim4.Value)
                Catch ex As Exception
                    txtErrorVigencias.Text = "Error Fecha Nacimiento"
                    GuardarDatos = False
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString
                sFechaNac = sAno + sMes + sDia
                nSexo = DdlSexo4.SelectedValue

                sPrefijo4 = IIf(txtPrefijo4.Text.Trim <> "", txtPrefijo4.Text.Trim, "NULL")
                sTelefono4 = IIf(txtNumero4.Text.Trim <> "", txtNumero4.Text.Trim, "NULL")
                sPrefijoCelular4 = IIf(txtPrefijoCelular4.Text.Trim <> "", txtPrefijoCelular4.Text.Trim, "NULL")
                sNumeroCelular4 = IIf(txtNumeroCelular4.Text.Trim <> "", txtNumeroCelular4.Text.Trim, "NULL")

                sSQLCmd = "INSERT INTO Autoridades " &
                            "(codigoRegistro, autoridad, cuil, " &
                            "fechNac, apellido, nombre, " &
                            "sexo, provincia, localidad, copost, " &
                            "domicilio, prefijo, telefono, " &
                            "preficelu, celular, fechAlta) " &
                        "VALUES " &
                            "(" & nIdRegistro & ", " & wautoridad & ", " & txtCUIL4.Text.Trim & ", " &
                            "Convert(datetime,'" & sFechaNac & "'), '" & txtApellido4.Text.Trim.ToUpper & "', '" & txtNombre4.Text.Trim.ToUpper & "', " &
                            " " & nSexo & ", " & ddlProvincia4.Text & ", " & ddlLocalidades4.Text & ", " & txtCP4.Text.Trim & ", " &
                            "'" & txtDomicilio4.Text.Trim.ToUpper & "', " & sPrefijo4 & ", " & sTelefono4 & ", " &
                            " " & sPrefijoCelular4 & ", " & sNumeroCelular4 & ", getdate())"

                MyCommand = New SqlCommand()
                MyCommand.CommandText = sSQLCmd
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection
                MyCommand.ExecuteNonQuery()
                MyCommand.Dispose()
            End If
            'End of INSERT Tesorero

            MyConnection.Dispose()

            'INSERT Palabras
            'sSQLCmd = "INSERT INTO RegistroPalabras " & _
            '                "(codigoRegistro, " & _
            '                "PAGINA_WEB_DE_LA_ONG, " & _
            '                "TELEFONO_DE_LA_ONG) " & _
            '            "VALUES " & _
            '                "(" & nIdRegistro & ", " & _
            '                " " & IIf(chkPalabra17.Checked, 1, 0) & ", " & _
            '                " " & IIf(chkPalabra23.Checked, 1, 0) & _
            '                ")"

            'MyConnection = New SqlConnection()
            'MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            'MyConnection.Open()
            'MyCommand = New SqlCommand()
            'MyCommand.CommandText = sSQLCmd
            'MyCommand.CommandType = CommandType.Text
            'MyCommand.Connection = MyConnection
            'MyCommand.ExecuteNonQuery()
            'MyCommand.Dispose()
            'MyConnection.Dispose()
            'End of INSERT Palabras

            Session.Add("CODIGO_REGISTRO", nIdRegistro)

        Catch ex As Exception
            GuardarDatos = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try
        GuardarDatos = True
    End Function

    Protected Function ActualizarDatos() As Integer
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim sFechaONG As String
        Dim sFechaActa As String
        Dim sFechaNac As String
        Dim nSexo As Integer
        Dim sPrefijo1 As Object
        Dim sTelefono1 As Object
        Dim sPrefijoCelular1 As Object
        Dim sNumeroCelular1 As Object
        Dim sPrefijo2 As Object
        Dim sTelefono2 As Object
        Dim sPrefijoCelular2 As Object
        Dim sNumeroCelular2 As Object
        Dim sPrefijo3 As Object
        Dim sTelefono3 As Object
        Dim sPrefijoCelular3 As Object
        Dim sNumeroCelular3 As Object
        Dim sPrefijo4 As Object
        Dim sTelefono4 As Object
        Dim sPrefijoCelular4 As Object
        Dim sNumeroCelular4 As Object
        Dim wentidad As Integer = Session("Entidad")
        Dim sDesexo As String = ""
        Try
            'Abro la conexión
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            'UPDATE Registro
            Dim wfecha As Date = CDate(TextONG.Value)
            sFechaONG = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            wfecha = CDate(TextActa.Value)
            sFechaActa = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)

            If wentidad <= 19 Then
                sSQLCmd = "UPDATE Registro " &
                           "SET RESPONSABLE = " & Session("USER_ID") & ",  " &
                              "provincia = " & ddlProvincias.SelectedValue & ", " &
                              "fechConsti = Convert(datetime,'" & sFechaONG & "'), " &
                              "fechActa = Convert(datetime,'" & sFechaActa & "'), " &
                              "vigencia = '" & ddlVigencias.Text & "', " &
                              "fechModi =  getdate() " &
                         "WHERE codigo = " & Session("CODIGO")
            Else
                sSQLCmd = "UPDATE Registro " &
                           "SET RESPONSABLE = " & Session("USER_ID") & ",  " &
                              "provincia = " & ddlProvincias.SelectedValue & ", " &
                              "fechConsti = Convert(datetime,'" & sFechaONG & "'), " &
                              "fechModi =  getdate() " &
                         "WHERE codigo = " & Session("CODIGO")
            End If

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()
            MyCommand.Dispose()
            'End of UPDATE Registro
            Dim nIdRegistro As Integer = Session("CODIGO")

            'Guardo registros
            sSQLCmd = "insert into AUTORIDADESLOG (idAutoridades, codigoRegistro, AUTORIDAD, CUIL, FECHNAC, APELLIDO, NOMBRE, SEXO, PROVINCIA, LOCALIDAD, COPOST, DOMICILIO, PREFIJO, TELEFONO, PREFICELU, CELULAR, FECHALTA, FECHMODI, FECHBAJA, desexo) " &
                      " select idAutoridades, codigoRegistro, AUTORIDAD, CUIL, FECHNAC, APELLIDO, NOMBRE, SEXO, PROVINCIA, LOCALIDAD, COPOST, DOMICILIO, PREFIJO, TELEFONO, PREFICELU, CELULAR, FECHALTA,GETDATE() FECHMODI, FECHBAJA, desexo " &
                      " from AUTORIDADES where codigoRegistro= " & nIdRegistro
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()
            MyCommand.Dispose()

            'Borro registros
            sSQLCmd = "DELETE FROM Autoridades WHERE codigoRegistro = " & nIdRegistro
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()
            MyCommand.Dispose()

            Dim wautoridad As Integer = 0
            If wentidad < 20 Then
                wautoridad = 1
            End If
            If wentidad >= 20 And wentidad <= 21 Then
                wautoridad = 5
            End If
            If wentidad >= 22 And wentidad <= 23 Then
                wautoridad = 6
            End If
            Dim sFecha As Date = Nothing
            Try
                sFecha = CDate(TextNacim1.Value)
            Catch ex As Exception
                txtErrorVigencias.Text = "Error Fecha Nacimiento"
                ActualizarDatos = False
                Return False
            End Try
            Dim sDia As String = Right("0" + Day(sFecha).ToString, 2)
            Dim sMes As String = Right("0" + Month(sFecha).ToString, 2)
            Dim sAno As String = Year(sFecha).ToString
            sFechaNac = sAno + sMes + sDia
            nSexo = ddlSexo1.SelectedValue
            sPrefijo1 = IIf(txtPrefijo1.Text.Trim <> "", txtPrefijo1.Text.Trim, "NULL")
            sTelefono1 = IIf(txtNumero1.Text.Trim <> "", txtNumero1.Text.Trim, "NULL")
            sPrefijoCelular1 = IIf(txtPrefijoCelular1.Text.Trim <> "", txtPrefijoCelular1.Text.Trim, "NULL")
            sNumeroCelular1 = IIf(txtNumeroCelular1.Text.Trim <> "", txtNumeroCelular1.Text.Trim, "NULL")
            sDesexo = TextBoxDesexo1.Text.Trim

            sSQLCmd = "INSERT INTO Autoridades " &
                            "(codigoRegistro, autoridad, cuil, " &
                            "fechNac, apellido, nombre, " &
                            "sexo, provincia, localidad, copost, " &
                            "domicilio, prefijo, telefono, " &
                            "preficelu, celular, fechAlta, desexo) " &
                        "VALUES " &
                            "(" & nIdRegistro & ", " & wautoridad & ", " & txtCUIL1.Text.Trim & ", " &
                            "Convert(datetime,'" & sFechaNac & "'), '" & txtApellido1.Text.Trim.ToUpper & "', '" & txtNombre1.Text.Trim.ToUpper & "', " &
                            " " & nSexo & ", " & ddlProvincia1.Text & ", " & ddlLocalidades1.Text & ", " & txtCP1.Text.Trim & ", " &
                            "'" & txtDomicilio1.Text.Trim.ToUpper & "', " & sPrefijo1 & ", " & sTelefono1 & ", " &
                            " " & sPrefijoCelular1 & ", " & sNumeroCelular1 & ", getdate(), '" & sDesexo & "')"

            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()

            MyCommand.Dispose()
            'End of INSERT Presidente

            'INSERT Vicepresidente
            If wentidad < 20 Then
                wautoridad = 2
            End If
            If Len(RTrim(txtCUIL2.Text)) > 0 Then
                Try
                    sFecha = CDate(TextNacim2.Value)
                Catch ex As Exception
                    txtErrorVigencias.Text = "Error Fecha Nacimiento"
                    ActualizarDatos = False
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString
                sFechaNac = sAno + sMes + sDia
                nSexo = DdlSexo2.SelectedValue
                sPrefijo2 = IIf(txtPrefijo2.Text.Trim <> "", txtPrefijo2.Text.Trim, "NULL")
                sTelefono2 = IIf(txtNumero2.Text.Trim <> "", txtNumero2.Text.Trim, "NULL")
                sPrefijoCelular2 = IIf(txtPrefijoCelular2.Text.Trim <> "", txtPrefijoCelular2.Text.Trim, "NULL")
                sNumeroCelular2 = IIf(txtNumeroCelular2.Text.Trim <> "", txtNumeroCelular2.Text.Trim, "NULL")
                sDesexo = TextBoxDesexo2.Text.Trim
                sSQLCmd = "INSERT INTO Autoridades " &
                                "(codigoRegistro, autoridad, cuil, " &
                                "fechNac, apellido, nombre, " &
                                "sexo, provincia, localidad, copost, " &
                                "domicilio, prefijo, telefono, " &
                                "preficelu, celular, fechAlta) " &
                            "VALUES " &
                                "(" & nIdRegistro & ", " & wautoridad & ", " & txtCUIL2.Text.Trim & ", " &
                                "Convert(datetime,'" & sFechaNac & "'), '" & txtApellido2.Text.Trim.ToUpper & "', '" & txtNombre2.Text.Trim.ToUpper & "', " &
                                " " & nSexo & ", " & ddlProvincia2.Text & ", " & ddlLocalidades2.Text & ", " & txtCP2.Text.Trim & ", " &
                                "'" & txtDomicilio2.Text.Trim.ToUpper & "', " & sPrefijo2 & ", " & sTelefono2 & ", " &
                                " " & sPrefijoCelular2 & ", " & sNumeroCelular2 & ", getdate(), '" & sDesexo & "')"

                MyCommand = New SqlCommand()
                MyCommand.CommandText = sSQLCmd
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection
                MyCommand.ExecuteNonQuery()
                MyCommand.Dispose()
            End If
            'End of INSERT Vicepresidente

            'INSERT Secretario

            If wentidad < 20 Then
                wautoridad = 3
            End If
            If Len(RTrim(txtCUIL3.Text)) > 0 Then
                Try
                    sFecha = CDate(TextNacim3.Value)
                Catch ex As Exception
                    txtErrorVigencias.Text = "Error Fecha Nacimiento"
                    ActualizarDatos = False
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString
                sFechaNac = sAno + sMes + sDia
                nSexo = DdlSexo3.SelectedValue
                sPrefijo3 = IIf(txtPrefijo3.Text.Trim <> "", txtPrefijo3.Text.Trim, "NULL")
                sTelefono3 = IIf(txtNumero3.Text.Trim <> "", txtNumero3.Text.Trim, "NULL")
                sPrefijoCelular3 = IIf(txtPrefijoCelular3.Text.Trim <> "", txtPrefijoCelular3.Text.Trim, "NULL")
                sNumeroCelular3 = IIf(txtNumeroCelular3.Text.Trim <> "", txtNumeroCelular3.Text.Trim, "NULL")
                sDesexo = TextBoxDesexo3.Text.Trim
                sSQLCmd = "INSERT INTO Autoridades " &
                            "(codigoRegistro, autoridad, cuil, " &
                            "fechNac, apellido, nombre, " &
                            "sexo, provincia, localidad, copost, " &
                            "domicilio, prefijo, telefono, " &
                            "preficelu, celular, fechAlta) " &
                        "VALUES " &
                            "(" & nIdRegistro & ", 3, " & txtCUIL3.Text.Trim & ", " &
                            "Convert(datetime,'" & sFechaNac & "'), '" & txtApellido3.Text.Trim.ToUpper & "', '" & txtNombre3.Text.Trim.ToUpper & "', " &
                            " " & nSexo & ", " & ddlProvincia3.Text & ", " & ddlLocalidades3.Text & ", " & txtCP3.Text.Trim & ", " &
                            "'" & txtDomicilio3.Text.Trim.ToUpper & "', " & sPrefijo3 & ", " & sTelefono3 & ", " &
                            " " & sPrefijoCelular3 & ", " & sNumeroCelular3 & ", getdate(), '" & sDesexo & "')"

                MyCommand = New SqlCommand()
                MyCommand.CommandText = sSQLCmd
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection
                MyCommand.ExecuteNonQuery()
                MyCommand.Dispose()
            End If

            'End of INSERT Secretario

            'INSERT Tesorero

            If wentidad < 20 Then
                wautoridad = 4
            End If
            If Len(RTrim(txtCUIL4.Text)) > 0 Then
                Try
                    sFecha = CDate(TextNacim4.Value)
                Catch ex As Exception
                    txtErrorVigencias.Text = "Error Fecha Nacimiento"
                    ActualizarDatos = False
                    Return False
                End Try
                sDia = Right("0" + Day(sFecha).ToString, 2)
                sMes = Right("0" + Month(sFecha).ToString, 2)
                sAno = Year(sFecha).ToString
                sFechaNac = sAno + sMes + sDia
                nSexo = DdlSexo4.SelectedValue
                sPrefijo4 = IIf(txtPrefijo4.Text.Trim <> "", txtPrefijo4.Text.Trim, "NULL")
                sTelefono4 = IIf(txtNumero4.Text.Trim <> "", txtNumero4.Text.Trim, "NULL")
                sPrefijoCelular4 = IIf(txtPrefijoCelular4.Text.Trim <> "", txtPrefijoCelular4.Text.Trim, "NULL")
                sNumeroCelular4 = IIf(txtNumeroCelular4.Text.Trim <> "", txtNumeroCelular4.Text.Trim, "NULL")
                sDesexo = TextBoxDesexo4.Text.Trim
                sSQLCmd = "INSERT INTO Autoridades " &
                            "(codigoRegistro, autoridad, cuil, " &
                            "fechNac, apellido, nombre, " &
                            "sexo, provincia, localidad, copost, " &
                            "domicilio, prefijo, telefono, " &
                            "preficelu, celular, fechAlta) " &
                        "VALUES " &
                            "(" & nIdRegistro & ", " & wautoridad & ", " & txtCUIL4.Text.Trim & ", " &
                            "Convert(datetime,'" & sFechaNac & "'), '" & txtApellido4.Text.Trim.ToUpper & "', '" & txtNombre4.Text.Trim.ToUpper & "', " &
                            " " & nSexo & ", " & ddlProvincia4.Text & ", " & ddlLocalidades4.Text & ", " & txtCP4.Text.Trim & ", " &
                            "'" & txtDomicilio4.Text.Trim.ToUpper & "', " & sPrefijo4 & ", " & sTelefono4 & ", " &
                            " " & sPrefijoCelular4 & ", " & sNumeroCelular4 & ", getdate(), '" & sDesexo & "')"

                MyCommand = New SqlCommand()
                MyCommand.CommandText = sSQLCmd
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection
                MyCommand.ExecuteNonQuery()
                MyCommand.Dispose()
            End If
            'End of INSERT Tesorero

            MyConnection.Dispose()

            'UPDATE Palabras
            'sSQLCmd = "UPDATE RegistroPalabras " & _
            '               "SET PAGINA_WEB_DE_LA_ONG = " & IIf(chkPalabra17.Checked, 1, 0) & ",  " & _
            '                  "TELEFONO_DE_LA_ONG = " & IIf(chkPalabra23.Checked, 1, 0) & " " & _
            '             "WHERE codigoRegistro = " & Session("CODIGO")

            'MyConnection = New SqlConnection()
            'MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            'MyConnection.Open()
            'MyCommand = New SqlCommand()
            'MyCommand.CommandText = sSQLCmd
            'MyCommand.CommandType = CommandType.Text
            'MyCommand.Connection = MyConnection
            'MyCommand.ExecuteNonQuery()
            'MyCommand.Dispose()
            'MyConnection.Dispose()
            'End of UPDATE Palabras

        Catch ex As Exception
            ActualizarDatos = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            Try
                MyCommand.Dispose()
                MyConnection.Dispose()
            Catch ex As Exception
            End Try
        End Try

        ActualizarDatos = True
    End Function

    Private Function ExisteVicepresidente() As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim nCantidad As Integer
        Try
            sSQLCmd = "SELECT count(*) AS cantidad " &
                            "FROM Autoridades " &
                            "WHERE codigoRegistro = " & Session("CODIGO") & " AND " &
                                "autoridad = 2"

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.Connection.Open()
            nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())
            If nCantidad > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub CargarDatos(ByVal nCodigo As Integer)

        'Tabla Registro
        'Const F_RESPONSABLE As Integer = 0
        Const F_SECTOR As Integer = 1
        Const F_PROVINCIA As Integer = 2
        'Const F_DENOMINACION As Integer = 3
        'Const F_LOCALIDAD As Integer = 4
        Const F_FECHCONSTI As Integer = 3
        Const F_FECHACTA As Integer = 4
        Const F_VIGENCIA As Integer = 5
        'Const F_FECHALTA As Integer = 11
        'Tabla Autoridades
        Const FA_ID_AUTORIDADES As Integer = 0
        Const FA_CODIGO_REGISTRO As Integer = 1
        Const FA_AUTORIDAD As Integer = 2
        Const FA_CUIL As Integer = 3
        Const FA_FECHNAC As Integer = 4
        Const FA_APELLIDO As Integer = 5
        Const FA_NOMBRE As Integer = 6
        Const FA_SEXO As Integer = 7
        Const FA_PROVINCIA As Integer = 8
        Const FA_LOCALIDAD As Integer = 9
        Const FA_COPOST As Integer = 10
        Const FA_DOMICILIO As Integer = 11
        Const FA_PREFIJO As Integer = 12
        Const FA_TELEFONO As Integer = 13
        Const FA_PREFIJOCELU As Integer = 14
        Const FA_CELULAR As Integer = 15
        Const FA_FECHALTA As Integer = 16
        Const FA_TIPOREGISTRO As Integer = 17
        Dim wdesexo As String = ""
        Dim wentidad As Integer = Session("Entidad")
        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim i As Integer
        Dim aProvincias(4) As Integer
        Dim aLocalidades(4) As Integer
        For i = 0 To 4 Step 1
            aProvincias(i) = 0
            aLocalidades(i) = 0
        Next
        Try
            'Load Registro
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            If wentidad < 20 Then
                sSQLCmd = "SELECT responsable, sector, provincia,right('0'+convert(varchar(2),day(fechconsti)),2)+'/'+right('0'+convert(varchar(2),month(fechconsti)),2)+'/'+convert(char(4),year(fechconsti)) fechconsti," &
                          " right('0'+convert(varchar(2),day(fechacta)),2)+'/'+right('0'+convert(varchar(2),month(fechacta)),2)+'/'+convert(char(4),year(fechacta)) fechacta, vigencia," &
                          " right('0'+convert(varchar(2),day(fechalta)),2)+'/'+right('0'+convert(varchar(2),month(fechalta)),2)+'/'+convert(char(4),year(fechalta)) fechAlta " &
                            "FROM Registro " &
                            "WHERE codigo = " & nCodigo.ToString
            Else
                sSQLCmd = "SELECT responsable, sector, provincia,right('0'+convert(varchar(2),day(fechconsti)),2)+'/'+right('0'+convert(varchar(2),month(fechconsti)),2)+'/'+convert(char(4),year(fechconsti)) fechconsti," &
                          " right('0'+convert(varchar(2),day(fechalta)),2)+'/'+right('0'+convert(varchar(2),month(fechalta)),2)+'/'+convert(char(4),year(fechalta)) fechAlta " &
                            "FROM Registro " &
                            "WHERE codigo = " & nCodigo.ToString
            End If

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()
            If MyReader.Read() Then
                Session.Add("SECTOR", MyReader.Item(F_SECTOR))
                Session.Add("PROVINCIA", MyReader.Item(F_PROVINCIA))
                'ddlSectores.Text = MyReader.Item(F_SECTOR).ToString.Trim
                CargaProvincias(ddlProvincias)
                ddlProvincias.Text = MyReader.Item(F_PROVINCIA)
                TextONG.Value = MyReader.Item(F_FECHCONSTI)
                If wentidad < 20 Then
                    TextActa.Value = MyReader.Item(F_FECHACTA)
                    ddlVigencias.SelectedValue = MyReader.GetInt32(5)
                End If
            End If
            MyCommand.Dispose()
            MyConnection.Dispose()
            'End of Load Registro

            If wentidad < 20 Then
                'Entidades
                'Load presidente
                MyConnection = New SqlConnection()
                MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
                MyConnection.Open()
                sSQLCmd = "Select idAutoridades, codigoRegistro, autoridad, cuil," &
                            "right('0'+convert(varchar(2),day(fechNac)),2)+'/'+right('0'+convert(varchar(2),month(fechNac)),2)+'/'+convert(char(4),year(fechNac)) fechNac," &
                            "apellido, nombre, sexo, " &
                            "provincia, localidad, copost, domicilio, prefijo, telefono, preficelu, celular," &
                            "right('0'+convert(varchar(2),day(fechalta)),2)+'/'+right('0'+convert(varchar(2),month(fechalta)),2)+'/'+convert(char(4),year(fechalta)) fechAlta, " &
                            "'PRESIDENTE' AS tipoRegistro,isnull(desexo,'') as desexo " &
                        "FROM Autoridades " &
                        "WHERE codigoRegistro = " & nCodigo.ToString & " AND autoridad = 1"

                MyCommand = New SqlCommand(sSQLCmd, MyConnection)
                MyReader = MyCommand.ExecuteReader()

                If MyReader.Read() Then
                    txtCUIL1.Text = MyReader.Item(FA_CUIL)
                    TextNacim1.Value = MyReader.Item(FA_FECHNAC)
                    txtApellido1.Text = MyReader.Item(FA_APELLIDO)
                    txtNombre1.Text = MyReader.Item(FA_NOMBRE)
                    ddlSexo1.SelectedValue = MyReader.Item(FA_SEXO)
                    CargaProvincias(ddlProvincia1)
                    ddlProvincia1.Text = MyReader.Item(FA_PROVINCIA)
                    CargaLocalidades(ddlLocalidades1, ddlProvincia1.SelectedValue)
                    ddlLocalidades1.Text = MyReader.Item(FA_LOCALIDAD)
                    txtCP1.Text = MyReader.Item(FA_COPOST)
                    txtDomicilio1.Text = MyReader.Item(FA_DOMICILIO)
                    txtPrefijo1.Text = IIf(MyReader.IsDBNull(FA_PREFIJO), "", MyReader.Item(FA_PREFIJO))
                    txtNumero1.Text = IIf(MyReader.IsDBNull(FA_TELEFONO), "", MyReader.Item(FA_TELEFONO))
                    txtPrefijoCelular1.Text = IIf(MyReader.IsDBNull(FA_PREFIJOCELU), "", MyReader.Item(FA_PREFIJOCELU))
                    txtNumeroCelular1.Text = IIf(MyReader.IsDBNull(FA_CELULAR), "", MyReader.Item(FA_CELULAR))
                    aProvincias(1) = MyReader.Item(FA_PROVINCIA)
                    aLocalidades(1) = MyReader.Item(FA_LOCALIDAD)
                    TextBoxDesexo1.Text = MyReader.GetString(18)
                End If

                MyCommand.Dispose()
                MyConnection.Dispose()
                'End of Load presidente

                'Load vicepresidente
                MyConnection = New SqlConnection()
                MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
                MyConnection.Open()
                sSQLCmd = "SELECT idAutoridades, codigoRegistro, autoridad, cuil," &
                            "right('0'+convert(varchar(2),day(fechNac)),2)+'/'+right('0'+convert(varchar(2),month(fechNac)),2)+'/'+convert(char(4),year(fechNac)) fechNac," &
                            "apellido, nombre, sexo, " &
                            "provincia, localidad, copost, domicilio, prefijo, telefono, preficelu, celular," &
                            "right('0'+convert(varchar(2),day(fechalta)),2)+'/'+right('0'+convert(varchar(2),month(fechalta)),2)+'/'+convert(char(4),year(fechalta)) fechAlta, " &
                            "'PRESIDENTE' AS tipoRegistro,isnull(desexo,'') as desexo  " &
                        "FROM Autoridades " &
                        "WHERE codigoRegistro = " & nCodigo.ToString & " AND autoridad = 2"

                MyCommand = New SqlCommand(sSQLCmd, MyConnection)
                MyReader = MyCommand.ExecuteReader()
                If MyReader.Read() Then
                    txtCUIL2.Text = MyReader.Item(FA_CUIL)
                    TextNacim2.Value = MyReader.Item(FA_FECHNAC)
                    txtApellido2.Text = MyReader.Item(FA_APELLIDO)
                    txtNombre2.Text = MyReader.Item(FA_NOMBRE)
                    DdlSexo2.SelectedValue = MyReader.Item(FA_SEXO)
                    CargaProvincias(ddlProvincia2)
                    ddlProvincia2.Text = MyReader.Item(FA_PROVINCIA)
                    CargaLocalidades(ddlLocalidades2, ddlProvincia2.SelectedValue)
                    ddlLocalidades2.Text = MyReader.Item(FA_LOCALIDAD)
                    txtCP2.Text = MyReader.Item(FA_COPOST)
                    txtDomicilio2.Text = MyReader.Item(FA_DOMICILIO)
                    txtPrefijo2.Text = IIf(MyReader.IsDBNull(FA_PREFIJO), "", MyReader.Item(FA_PREFIJO))
                    txtNumero2.Text = IIf(MyReader.IsDBNull(FA_TELEFONO), "", MyReader.Item(FA_TELEFONO))
                    txtPrefijoCelular2.Text = IIf(MyReader.IsDBNull(FA_PREFIJOCELU), "", MyReader.Item(FA_PREFIJOCELU))
                    txtNumeroCelular2.Text = IIf(MyReader.IsDBNull(FA_CELULAR), "", MyReader.Item(FA_CELULAR))
                    aProvincias(2) = MyReader.Item(FA_PROVINCIA)
                    aLocalidades(2) = MyReader.Item(FA_LOCALIDAD)
                    TextBoxDesexo2.Text = MyReader.GetString(18)
                End If

                MyCommand.Dispose()
                MyConnection.Dispose()
                'End of Load vicepresidente

                'Load secretario
                MyConnection = New SqlConnection()
                MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
                MyConnection.Open()
                sSQLCmd = "SELECT idAutoridades, codigoRegistro, autoridad, cuil," &
                            "right('0'+convert(varchar(2),day(fechNac)),2)+'/'+right('0'+convert(varchar(2),month(fechNac)),2)+'/'+convert(char(4),year(fechNac)) fechNac," &
                            "apellido, nombre, sexo, " &
                            "provincia, localidad, copost, domicilio, prefijo, telefono, preficelu, celular," &
                            " right('0'+convert(varchar(2),day(fechalta)),2)+'/'+right('0'+convert(varchar(2),month(fechalta)),2)+'/'+convert(char(4),year(fechalta)) fechAlta, " &
                            "'PRESIDENTE' AS tipoRegistro,isnull(desexo,'') as desexo  " &
                        "FROM Autoridades " &
                        "WHERE codigoRegistro = " & nCodigo.ToString & " AND autoridad = 3"

                MyCommand = New SqlCommand(sSQLCmd, MyConnection)
                MyReader = MyCommand.ExecuteReader()
                If MyReader.Read() Then
                    txtCUIL3.Text = MyReader.Item(FA_CUIL)
                    TextNacim3.Value = MyReader.Item(FA_FECHNAC)
                    txtApellido3.Text = MyReader.Item(FA_APELLIDO)
                    txtNombre3.Text = MyReader.Item(FA_NOMBRE)
                    DdlSexo3.SelectedValue = MyReader.Item(FA_SEXO)
                    CargaProvincias(ddlProvincia3)
                    ddlProvincia3.Text = MyReader.Item(FA_PROVINCIA)
                    CargaLocalidades(ddlLocalidades3, ddlProvincia3.SelectedValue)
                    ddlLocalidades3.Text = MyReader.Item(FA_LOCALIDAD)
                    txtCP3.Text = MyReader.Item(FA_COPOST)
                    txtDomicilio3.Text = MyReader.Item(FA_DOMICILIO)
                    txtPrefijo3.Text = IIf(MyReader.IsDBNull(FA_PREFIJO), "", MyReader.Item(FA_PREFIJO))
                    TxtNumero3.Text = IIf(MyReader.IsDBNull(FA_TELEFONO), "", MyReader.Item(FA_TELEFONO))
                    txtPrefijoCelular3.Text = IIf(MyReader.IsDBNull(FA_PREFIJOCELU), "", MyReader.Item(FA_PREFIJOCELU))
                    txtNumeroCelular3.Text = IIf(MyReader.IsDBNull(FA_CELULAR), "", MyReader.Item(FA_CELULAR))
                    aProvincias(3) = MyReader.Item(FA_PROVINCIA)
                    aLocalidades(3) = MyReader.Item(FA_LOCALIDAD)
                    TextBoxDesexo3.Text = MyReader.GetString(18)
                End If

                MyCommand.Dispose()
                MyConnection.Dispose()
                'End of Load secretario

                'Load tesorero
                MyConnection = New SqlConnection()
                MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
                MyConnection.Open()
                sSQLCmd = "SELECT idAutoridades, codigoRegistro, autoridad, cuil," &
                            "right('0'+convert(varchar(2),day(fechNac)),2)+'/'+right('0'+convert(varchar(2),month(fechNac)),2)+'/'+convert(char(4),year(fechNac)) fechNac," &
                            "apellido, nombre, sexo, " &
                            "provincia, localidad, copost, domicilio, prefijo, telefono, preficelu, celular," &
                            " right('0'+convert(varchar(2),day(fechalta)),2)+'/'+right('0'+convert(varchar(2),month(fechalta)),2)+'/'+convert(char(4),year(fechalta)) fechAlta, " &
                            "'PRESIDENTE' AS tipoRegistro,isnull(desexo,'') as desexo  " &
                        "FROM Autoridades " &
                        "WHERE codigoRegistro = " & nCodigo.ToString & " AND autoridad = 4"

                MyCommand = New SqlCommand(sSQLCmd, MyConnection)
                MyReader = MyCommand.ExecuteReader()
                If MyReader.Read() Then
                    txtCUIL4.Text = MyReader.Item(FA_CUIL)
                    TextNacim4.Value = MyReader.Item(FA_FECHNAC)
                    txtApellido4.Text = MyReader.Item(FA_APELLIDO)
                    txtNombre4.Text = MyReader.Item(FA_NOMBRE)
                    DdlSexo4.SelectedValue = MyReader.Item(FA_SEXO)
                    CargaProvincias(ddlProvincia4)
                    ddlProvincia4.Text = MyReader.Item(FA_PROVINCIA)
                    CargaLocalidades(ddlLocalidades4, ddlProvincia4.SelectedValue)
                    ddlLocalidades4.Text = MyReader.Item(FA_LOCALIDAD)
                    txtCP4.Text = MyReader.Item(FA_COPOST)
                    txtDomicilio4.Text = MyReader.Item(FA_DOMICILIO)
                    txtPrefijo4.Text = IIf(MyReader.IsDBNull(FA_PREFIJO), "", MyReader.Item(FA_PREFIJO))
                    TxtNumero4.Text = IIf(MyReader.IsDBNull(FA_TELEFONO), "", MyReader.Item(FA_TELEFONO))
                    txtPrefijoCelular4.Text = IIf(MyReader.IsDBNull(FA_PREFIJOCELU), "", MyReader.Item(FA_PREFIJOCELU))
                    txtNumeroCelular4.Text = IIf(MyReader.IsDBNull(FA_CELULAR), "", MyReader.Item(FA_CELULAR))
                    aProvincias(4) = MyReader.Item(FA_PROVINCIA)
                    aLocalidades(4) = MyReader.Item(FA_LOCALIDAD)
                    TextBoxDesexo4.Text = MyReader.GetString(18)
                End If

                MyCommand.Dispose()
                MyConnection.Dispose()
                'End of Load tesorero

            Else
                'Sociedades
                Dim auto As Integer = 1
                MyConnection = New SqlConnection()
                MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
                MyConnection.Open()
                sSQLCmd = "SELECT idAutoridades, codigoRegistro, autoridad, cuil," &
                            "right('0'+convert(varchar(2),day(fechNac)),2)+'/'+right('0'+convert(varchar(2),month(fechNac)),2)+'/'+convert(char(4),year(fechNac)) fechNac," &
                            "apellido, nombre, sexo, " &
                            "provincia, localidad, copost, domicilio, prefijo, telefono, preficelu, celular," &
                            " right('0'+convert(varchar(2),day(fechalta)),2)+'/'+right('0'+convert(varchar(2),month(fechalta)),2)+'/'+convert(char(4),year(fechalta)) fechAlta, " &
                            "upper(d.descrip) AS tipoRegistro,isnull(desexo,'') as desexo   " &
                        "FROM Autoridades a, autoridad d " &
                        "WHERE codigoRegistro = " & nCodigo.ToString & " and a.autoridad=d.codigo order by idautoridades"

                MyCommand = New SqlCommand(sSQLCmd, MyConnection)
                MyReader = MyCommand.ExecuteReader()
                While MyReader.Read()
                    Select Case auto
                        Case 1
                            txtCUIL1.Text = MyReader.Item(FA_CUIL)
                            TextNacim1.Value = MyReader.Item(FA_FECHNAC)
                            txtApellido1.Text = MyReader.Item(FA_APELLIDO)
                            txtNombre1.Text = MyReader.Item(FA_NOMBRE)
                            ddlSexo1.SelectedValue = MyReader.Item(FA_SEXO)
                            CargaProvincias(ddlProvincia1)
                            ddlProvincia1.Text = MyReader.Item(FA_PROVINCIA)
                            CargaLocalidades(ddlLocalidades1, ddlProvincia1.SelectedValue)
                            ddlLocalidades1.Text = MyReader.Item(FA_LOCALIDAD)
                            txtCP1.Text = MyReader.Item(FA_COPOST)
                            txtDomicilio1.Text = MyReader.Item(FA_DOMICILIO)
                            txtPrefijo1.Text = IIf(MyReader.IsDBNull(FA_PREFIJO), "", MyReader.Item(FA_PREFIJO))
                            txtNumero1.Text = IIf(MyReader.IsDBNull(FA_TELEFONO), "", MyReader.Item(FA_TELEFONO))
                            txtPrefijoCelular1.Text = IIf(MyReader.IsDBNull(FA_PREFIJOCELU), "", MyReader.Item(FA_PREFIJOCELU))
                            txtNumeroCelular1.Text = IIf(MyReader.IsDBNull(FA_CELULAR), "", MyReader.Item(FA_CELULAR))
                            aProvincias(1) = MyReader.Item(FA_PROVINCIA)
                            aLocalidades(1) = MyReader.Item(FA_LOCALIDAD)
                            TextBoxDesexo1.Text = MyReader.GetString(18)
                        Case 2
                            txtCUIL2.Text = MyReader.Item(FA_CUIL)
                            TextNacim2.Value = MyReader.Item(FA_FECHNAC)
                            txtApellido2.Text = MyReader.Item(FA_APELLIDO)
                            txtNombre2.Text = MyReader.Item(FA_NOMBRE)
                            DdlSexo2.SelectedValue = MyReader.Item(FA_SEXO)
                            CargaProvincias(ddlProvincia2)
                            ddlProvincia2.Text = MyReader.Item(FA_PROVINCIA)
                            CargaLocalidades(ddlLocalidades2, ddlProvincia2.SelectedValue)
                            ddlLocalidades2.Text = MyReader.Item(FA_LOCALIDAD)
                            txtCP2.Text = MyReader.Item(FA_COPOST)
                            txtDomicilio2.Text = MyReader.Item(FA_DOMICILIO)
                            txtPrefijo2.Text = IIf(MyReader.IsDBNull(FA_PREFIJO), "", MyReader.Item(FA_PREFIJO))
                            txtNumero2.Text = IIf(MyReader.IsDBNull(FA_TELEFONO), "", MyReader.Item(FA_TELEFONO))
                            txtPrefijoCelular2.Text = IIf(MyReader.IsDBNull(FA_PREFIJOCELU), "", MyReader.Item(FA_PREFIJOCELU))
                            txtNumeroCelular2.Text = IIf(MyReader.IsDBNull(FA_CELULAR), "", MyReader.Item(FA_CELULAR))
                            aProvincias(2) = MyReader.Item(FA_PROVINCIA)
                            aLocalidades(2) = MyReader.Item(FA_LOCALIDAD)
                            TextBoxDesexo2.Text = MyReader.GetString(18)
                        Case 3
                            txtCUIL3.Text = MyReader.Item(FA_CUIL)
                            TextNacim3.Value = MyReader.Item(FA_FECHNAC)
                            txtApellido3.Text = MyReader.Item(FA_APELLIDO)
                            txtNombre3.Text = MyReader.Item(FA_NOMBRE)
                            DdlSexo3.SelectedValue = MyReader.Item(FA_SEXO)
                            CargaProvincias(ddlProvincia3)
                            ddlProvincia3.Text = MyReader.Item(FA_PROVINCIA)
                            CargaLocalidades(ddlLocalidades3, ddlProvincia3.SelectedValue)
                            ddlLocalidades3.Text = MyReader.Item(FA_LOCALIDAD)
                            txtCP3.Text = MyReader.Item(FA_COPOST)
                            txtDomicilio3.Text = MyReader.Item(FA_DOMICILIO)
                            txtPrefijo3.Text = IIf(MyReader.IsDBNull(FA_PREFIJO), "", MyReader.Item(FA_PREFIJO))
                            TxtNumero3.Text = IIf(MyReader.IsDBNull(FA_TELEFONO), "", MyReader.Item(FA_TELEFONO))
                            txtPrefijoCelular3.Text = IIf(MyReader.IsDBNull(FA_PREFIJOCELU), "", MyReader.Item(FA_PREFIJOCELU))
                            txtNumeroCelular3.Text = IIf(MyReader.IsDBNull(FA_CELULAR), "", MyReader.Item(FA_CELULAR))
                            aProvincias(3) = MyReader.Item(FA_PROVINCIA)
                            aLocalidades(3) = MyReader.Item(FA_LOCALIDAD)
                            TextBoxDesexo3.Text = MyReader.GetString(18)
                        Case 4
                            txtCUIL4.Text = MyReader.Item(FA_CUIL)
                            TextNacim4.Value = MyReader.Item(FA_FECHNAC)
                            txtApellido4.Text = MyReader.Item(FA_APELLIDO)
                            txtNombre4.Text = MyReader.Item(FA_NOMBRE)
                            DdlSexo4.SelectedValue = MyReader.Item(FA_SEXO)
                            CargaProvincias(ddlProvincia4)
                            ddlProvincia4.Text = MyReader.Item(FA_PROVINCIA)
                            CargaLocalidades(ddlLocalidades4, ddlProvincia4.SelectedValue)
                            ddlLocalidades4.Text = MyReader.Item(FA_LOCALIDAD)
                            txtCP4.Text = MyReader.Item(FA_COPOST)
                            txtDomicilio4.Text = MyReader.Item(FA_DOMICILIO)
                            txtPrefijo4.Text = IIf(MyReader.IsDBNull(FA_PREFIJO), "", MyReader.Item(FA_PREFIJO))
                            TxtNumero4.Text = IIf(MyReader.IsDBNull(FA_TELEFONO), "", MyReader.Item(FA_TELEFONO))
                            txtPrefijoCelular4.Text = IIf(MyReader.IsDBNull(FA_PREFIJOCELU), "", MyReader.Item(FA_PREFIJOCELU))
                            txtNumeroCelular4.Text = IIf(MyReader.IsDBNull(FA_CELULAR), "", MyReader.Item(FA_CELULAR))
                            aProvincias(4) = MyReader.Item(FA_PROVINCIA)
                            aLocalidades(4) = MyReader.Item(FA_LOCALIDAD)
                            TextBoxDesexo4.Text = MyReader.GetString(18)
                    End Select
                    auto = auto + 1
                End While
            End If

            'Load Palabras
            'sSQLCmd = "SELECT * " & _
            '                "FROM RegistroPalabras " & _
            '                "WHERE codigoRegistro = " & nCodigo.ToString
            'MyConnection = New SqlConnection()
            'MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            'MyConnection.Open()
            'MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            'MyReader = MyCommand.ExecuteReader()
            'If MyReader.Read() Then
            '    'If MyReader.Item(9) = 1 Then
            '    '    chkPalabra9.Checked = True
            '    'Else
            '    '    chkPalabra9.Checked = False
            '    'End If
            '    If MyReader.Item(17) = 1 Then
            '        chkPalabra17.Checked = True
            '    Else
            '        chkPalabra17.Checked = False
            '    End If
            '    If MyReader.Item(23) = 1 Then
            '        chkPalabra23.Checked = True
            '    Else
            '        chkPalabra23.Checked = False
            '    End If
            'End If
            'MyReader.Dispose()
            'MyCommand.Dispose()
            'End of Load Palabras

            'Carga las combos
            Inicializar(aProvincias, aLocalidades)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlProvincia1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincia1.SelectedIndexChanged
        Dim prov As String = ddlProvincia1.SelectedValue.ToString
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & prov & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        ddlLocalidades1.DataSource = dr6
        ddlLocalidades1.DataTextField = "nomloc"
        ddlLocalidades1.DataValueField = "codloc"
        ddlLocalidades1.DataBind()
        cn.Close()
    End Sub

    Protected Sub ddlProvincia2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincia2.SelectedIndexChanged
        Dim prov As String = ddlProvincia2.SelectedValue.ToString
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & prov & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        ddlLocalidades2.DataSource = dr6
        ddlLocalidades2.DataTextField = "nomloc"
        ddlLocalidades2.DataValueField = "codloc"
        ddlLocalidades2.DataBind()
        cn.Close()
    End Sub

    Protected Sub ddlProvincia3_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincia3.SelectedIndexChanged
        Dim prov As String = ddlProvincia3.SelectedValue.ToString
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & prov & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        ddlLocalidades3.DataSource = dr6
        ddlLocalidades3.DataTextField = "nomloc"
        ddlLocalidades3.DataValueField = "codloc"
        ddlLocalidades3.DataBind()
        cn.Close()
    End Sub

    Protected Sub ddlProvincia4_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincia4.SelectedIndexChanged
        Dim prov As String = ddlProvincia4.SelectedValue.ToString
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & prov & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        ddlLocalidades4.DataSource = dr6
        ddlLocalidades4.DataTextField = "nomloc"
        ddlLocalidades4.DataValueField = "codloc"
        ddlLocalidades4.DataBind()
        cn.Close()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim sAccion As String = Session("sAccion")
        If sAccion <> "M" Then
            Response.Redirect("menuFinal.aspx")
        Else
            Response.Redirect("RegistroLista.aspx")
        End If
    End Sub

    Protected Sub ddlProvincias_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincias.SelectedIndexChanged
        Dim sAccion As String = Session("sAccion")
        If sAccion.ToUpper = "A" Then
            ddlProvincia1.SelectedValue = ddlProvincias.SelectedValue
            ddlProvincia2.SelectedValue = ddlProvincias.SelectedValue
            ddlProvincia3.SelectedValue = ddlProvincias.SelectedValue
            ddlProvincia4.SelectedValue = ddlProvincias.SelectedValue
            CargaLocalidades(ddlLocalidades1, ddlProvincias.SelectedItem.Value)
            CargaLocalidades(ddlLocalidades2, ddlProvincias.SelectedItem.Value)
            CargaLocalidades(ddlLocalidades3, ddlProvincias.SelectedItem.Value)
            CargaLocalidades(ddlLocalidades4, ddlProvincias.SelectedItem.Value)

        End If
    End Sub
    Private Sub CargaProvincias(ByVal prov As DropDownList)
        cn.Open()
        Dim sql3 As String
        sql3 = "Select 0 codigo, 'Seleccione Provincia' descrip union SELECT codigo,descrip FROM Provin where region is not null ORDER BY codigo"
        'Else
        Dim Psql3 As New SqlClient.SqlCommand(sql3, cn)
        Dim dr3 As SqlClient.SqlDataReader = Psql3.ExecuteReader
        prov.DataSource = dr3
        prov.DataTextField = "descrip"
        prov.DataValueField = "codigo"
        prov.DataBind()
        dr3.Close()
        cn.Close()
    End Sub
    Private Sub CargaLocalidades(ByVal local As DropDownList, ByVal provincia As String)
        cn.Open()
        Dim sql3 As String
        sql3 = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia = " & provincia & " order by nomloc"
        'Else
        Dim Psql3 As New SqlClient.SqlCommand(sql3, cn)
        Dim dr3 As SqlClient.SqlDataReader = Psql3.ExecuteReader
        local.DataSource = dr3
        local.DataTextField = "nomloc"
        local.DataValueField = "codloc"
        local.DataBind()
        dr3.Close()
        cn.Close()
    End Sub

    Protected Sub AceptoDJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AceptoDJ.CheckedChanged
        tablaDatos.Visible = AceptoDJ.Checked
    End Sub
End Class