Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.Packaging
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Partial Public Class ActualIndivFis
    Inherits System.Web.UI.Page

    Private Const F_CUIL As Integer = 0
    Private Const F_APELLIDO As Integer = 1
    Private Const F_NOMBRE As Integer = 2
    Private Const F_PROVINCIA As Integer = 3
    Private Const F_LOCALIDAD As Integer = 4
    Private Const F_DOMICILIO As Integer = 5

    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Dim aKeyFields(5) As String

    Dim quien As usuario
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                quien = CType(Session("usuario"), usuario)
                Session("id_provincia") = quien.codprovin
                If quien.Persona = "HUMANA" Then
                    Session("id_persona") = 1
                Else
                    Session("id_persona") = 2
                End If
                Session("Cuit") = quien.Usuario
                Session("USER_ID") = quien.Codigo
                TextBoxCUIT.Text = Session("Cuit")
                TextBoxCUIT.Enabled = False
                inicializa()
                ddlDia.Focus()
                'BtnEnviar.Enabled = False
            Else
                Response.Clear()
                Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        Else
            MaintainScrollPositionOnPostBack = True
            GuardarAdjunto()
        End If
    End Sub

    Private Sub GuardarAdjunto()
        If Session("UploadImporta1") Is Nothing Or UploadImporta.HasFile Then
            If UploadImporta.HasFile Then
                Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim woperador As String = Session("CUIL")
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".DOC" Or UCase(Extension) = ".DOCX" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        UploadImporta.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("UploadImporta1") = UploadImporta
                    Session("UploadFileName") = FilePath
                    LabelNombreUpload.Text = UploadImporta.FileName
                End If
            End If
        Else
            If Session("UploadImporta1") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("UploadImporta1"), FileUpload)
                    LabelNombreUpload.Text = UploadImporta1.FileName
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Public Sub inicializa()
        Dim Provincia As Integer = Session("id_provincia")
        Dim Persona As Integer = Session("id_persona")

        cn.Open()
        Dim sql As String = "Select 0 as codigo, 'Seleccione Provincia' as descrip union select codigo,descrip from provin where codigo>=2 and codigo<=94 order by codigo"
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        ddlProvincia.DataSource = dr
        ddlProvincia.DataTextField = "descrip"
        ddlProvincia.DataValueField = "codigo"
        ddlProvincia.DataBind()
        cn.Close()
        dr.Close()
        cn.Open()
        Dim sql1 As String = "select codigo,descrip from personas order by codigo"
        Dim Psql1 As New SqlClient.SqlCommand(sql1, cn)
        Dim dr1 As SqlClient.SqlDataReader = Psql1.ExecuteReader
        ddlPersona.DataSource = dr1
        ddlPersona.DataTextField = "descrip"
        ddlPersona.DataValueField = "codigo"
        ddlPersona.DataBind()
        cn.Close()
        dr1.Close()
        ddlProvincia.SelectedValue = Provincia
        ddlPersona.SelectedValue = Persona
        'ddlProvincia.Enabled = False
        ddlPersona.Enabled = False
        cn.Open()
        Dim sql2 As String = "select 0 as codigo,'Día' as descrip union select codigo,descrip from dias order by codigo"
        Dim Psql2 As New SqlClient.SqlCommand(sql2, cn)
        Dim dr2 As SqlClient.SqlDataReader = Psql2.ExecuteReader
        ddlDia.DataSource = dr2
        ddlDia.DataTextField = "descrip"
        ddlDia.DataValueField = "codigo"
        ddlDia.DataBind()
        cn.Close()
        dr2.Close()
        cn.Open()
        Dim sql3 As String = "select 0 as codigo,'Elija Mes' as descrip union select codigo,descrip from meses order by codigo"
        Dim Psql3 As New SqlClient.SqlCommand(sql3, cn)
        Dim dr3 As SqlClient.SqlDataReader = Psql3.ExecuteReader
        ddlMes.DataSource = dr3
        ddlMes.DataTextField = "descrip"
        ddlMes.DataValueField = "codigo"
        ddlMes.DataBind()
        cn.Close()
        dr3.Close()
        cn.Open()
        Dim sql4 As String = "select 0 as codigo,'Año' as descrip union select codigo,descrip from anios order by codigo"
        Dim Psql4 As New SqlClient.SqlCommand(sql4, cn)
        Dim dr4 As SqlClient.SqlDataReader = Psql4.ExecuteReader
        ddlAnio.DataSource = dr4
        ddlAnio.DataTextField = "descrip"
        ddlAnio.DataValueField = "codigo"
        ddlAnio.DataBind()
        cn.Close()
        dr4.Close()
        cn.Open()
        Dim sql5 As String = "select 0 as codigo,'Seleccione' as descrip union select codigo,descrip from sexo order by codigo"
        Dim Psql5 As New SqlClient.SqlCommand(sql5, cn)
        Dim dr5 As SqlClient.SqlDataReader = Psql5.ExecuteReader
        ddlSexo.DataSource = dr5
        ddlSexo.DataTextField = "descrip"
        ddlSexo.DataValueField = "codigo"
        ddlSexo.DataBind()
        cn.Close()
        dr5.Close()
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & Provincia & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        DdlLocalidad.DataSource = dr6
        DdlLocalidad.DataTextField = "nomloc"
        DdlLocalidad.DataValueField = "codloc"
        DdlLocalidad.DataBind()
        cn.Close()
        dr6.Close()
        cn.Open()
        Dim sql7 As String = "select 0 as codigo,' Seleccione Pregunta' as descrip union select codigo,descrip from recupcontra where codigo in (1,3,5)"
        Dim Psql7 As New SqlClient.SqlCommand(sql7, cn)
        Dim dr7 As SqlClient.SqlDataReader = Psql7.ExecuteReader
        DdlPregunta.DataSource = dr7
        DdlPregunta.DataTextField = "descrip"
        DdlPregunta.DataValueField = "codigo"
        DdlPregunta.DataBind()
        cn.Close()
        dr6.Close()
        cn.Open()
        Dim sql8 As String = "select 0 as codigo,' Seleccione Nacionalidad' as descrip union select codigo,descrip from nacional"
        Dim Psql8 As New SqlClient.SqlCommand(sql8, cn)
        Dim dr8 As SqlClient.SqlDataReader = Psql8.ExecuteReader
        DdlNacional.DataSource = dr8
        DdlNacional.DataTextField = "descrip"
        DdlNacional.DataValueField = "codigo"
        DdlNacional.DataBind()
        cn.Close()
        dr8.Close()
        cn.Open()
        Dim sql0 As String = "select 0 as codigo,'Seleccione' as descrip union select 1 as codigo,'SI' as descrip union select 2 as codigo,'NO' as descrip"
        Dim Psql0 As New SqlClient.SqlCommand(sql0, cn)
        Dim dr0 As SqlClient.SqlDataReader = Psql0.ExecuteReader
        DDlWhatsApp.DataSource = dr0
        DDlWhatsApp.DataTextField = "descrip"
        DDlWhatsApp.DataValueField = "codigo"
        DDlWhatsApp.DataBind()
        cn.Close()
        dr0.Close()
        cn.Open()
        Dim sql10 As String = "select 0 as codigo,' Seleccione Tipo de Formación' as descrip union select codigo,descrip from TIPOFORMACION"
        Dim Psql10 As New SqlClient.SqlCommand(sql10, cn)
        Dim dr10 As SqlClient.SqlDataReader = Psql10.ExecuteReader
        DdlFormacion.DataSource = dr10
        DdlFormacion.DataTextField = "descrip"
        DdlFormacion.DataValueField = "codigo"
        DdlFormacion.DataBind()
        cn.Close()
        dr10.Close()
        cn.Open()
        Dim sql11 As String = "select 0 as codigo,' Seleccione Título Alcanzado' as descrip union select codigo,descrip from TITULOALCANZADO"
        Dim Psql11 As New SqlClient.SqlCommand(sql11, cn)
        Dim dr11 As SqlClient.SqlDataReader = Psql11.ExecuteReader
        DdlTitulo.DataSource = dr11
        DdlTitulo.DataTextField = "descrip"
        DdlTitulo.DataValueField = "codigo"
        DdlTitulo.DataBind()
        cn.Close()
        dr11.Close()
        'DdlTitulo.Enabled = False
        Dim wCODIGO As Integer = 0
        Dim wPROVINCIA As Integer = 0
        Dim wPERSONA As Integer = 0
        Dim wFECHNAC As DateTime = Nothing
        Dim wAPELLIDO As String = ""
        Dim wNOMBRE As String = ""
        Dim wSEXO As Integer = 0
        Dim wLOCALIDAD As Integer = 0
        Dim wCOPOST As Integer = 0
        Dim wDOMIPART As String = ""
        Dim wPREFIPART As Integer = 0
        Dim wTELEPART As Integer = 0
        Dim wPREFICELU As Integer = 0
        Dim wCELUPART As Integer = 0
        Dim wEMAIL As String = ""
        Dim wCONTRASENA As String = ""
        Dim wPREGUNTA As Integer = 0
        Dim wRESPUESTA As String = ""
        Dim wNACIONAL As Integer = 0
        Dim wResidencia As Integer = 0
        Dim wobservaotros As String = ""
        Dim wdesexo As String = ""
        Dim WhatsApp As Integer = 0
        Dim wformacion As Integer = 0
        Dim wtitulo As Integer = 0
        cn.Open()
        Dim wuser As String = Session("Cuit").ToString
        Dim sql9 As String = "select * from REGISDIG where cuil=" & wuser & " and fechbaja is null"
        Dim Psql9 As New SqlClient.SqlCommand(sql9, cn)
        Dim dr9 As SqlClient.SqlDataReader = Psql9.ExecuteReader

        If dr9.HasRows = True Then
            While (dr9.Read())
                wCODIGO = dr9.GetInt32(0)
                wPROVINCIA = dr9.GetInt32(1)
                wPERSONA = dr9.GetInt32(2)
                wFECHNAC = dr9.GetDateTime(4)
                wAPELLIDO = RTrim(UCase(dr9.GetString(5)))
                wNOMBRE = RTrim(UCase(dr9.GetString(6)))
                wSEXO = dr9.GetInt32(7)
                wLOCALIDAD = dr9.GetInt32(8)
                wCOPOST = dr9.GetInt32(9)
                wDOMIPART = RTrim(UCase(dr9.GetString(10)))
                wPREFIPART = dr9.GetInt32(11)
                wTELEPART = dr9.GetInt32(12)
                wPREFICELU = dr9.GetInt32(13)
                wCELUPART = dr9.GetInt32(14)
                wEMAIL = RTrim(dr9.GetString(15))
                wCONTRASENA = RTrim(UCase(dr9.GetString(16)))
                wPREGUNTA = dr9.GetInt32(17)
                wRESPUESTA = RTrim(UCase(dr9.GetString(18)))
                wNACIONAL = dr9.GetInt32(24)
                wResidencia = dr9.GetInt32(25)
                Try
                    wobservaotros = dr9.GetString(52)
                Catch ex As Exception
                    wobservaotros = ""
                End Try
                Try
                    wdesexo = dr9.GetString(53)
                Catch ex As Exception
                    wdesexo = ""
                End Try
                Try
                    WhatsApp = dr9.GetInt32(54)
                Catch ex As Exception
                    WhatsApp = 0
                End Try
                Try
                    wformacion = dr9.GetInt32(56)
                Catch ex As Exception
                    wformacion = 0
                End Try
                Try
                    wtitulo = dr9.GetInt32(57)
                Catch ex As Exception
                    wtitulo = 0
                End Try
            End While
        End If
        LabelSoli.Text = wCODIGO.ToString
        TextBoxApellido.Text = wAPELLIDO
        TextBoxNombre.Text = wNOMBRE
        ddlSexo.SelectedValue = wSEXO
        TextBoxDsexo.Text = wdesexo
        ddlProvincia.SelectedValue = wPROVINCIA
        DdlLocalidad.SelectedValue = wLOCALIDAD
        TextBoxCopost.Text = wCOPOST.ToString
        DdlNacional.SelectedValue = wNACIONAL
        TextBoxDompar.Text = wDOMIPART
        TextBoxPrefTelPart.Text = IIf(wPREFIPART.ToString <> "0", wPREFIPART.ToString, "")
        TextBoxTelPart.Text = IIf(wTELEPART.ToString <> "0", wTELEPART.ToString, "")
        'TextBoxPrefCelu.Text = IIf(wPREFICELU.ToString <> "0", wPREFICELU.ToString, "")
        'TextBoxCelular.Text = IIf(wCELUPART.ToString <> "0", wCELUPART.ToString, "")
        DDlWhatsApp.SelectedValue = WhatsApp
        DdlFormacion.SelectedValue = wformacion
        DdlTitulo.SelectedValue = wtitulo

        'TextBoxMail.Text = wEMAIL
        'TextBoxConfMail.Text = wEMAIL
        'TextBoxContra.Text = wCONTRASENA
        If wPREGUNTA = 1 Or wPREGUNTA = 3 Or wPREGUNTA = 5 Then
            DdlPregunta.SelectedValue = wPREGUNTA
            TextBoxRespuesta.Text = wRESPUESTA
        End If
        'TextBoxReContra.Text = wCONTRASENA
        CheckBoxAcepto.Checked = True
        CheckBox1.Checked = True 'Aceptación correo
        TextBoxresid.Text = wResidencia.ToString
        Dim wfecha As String = wFECHNAC.ToString
        Dim wdia As Integer = Day(wFECHNAC)
        Dim wmes As Integer = Month(wFECHNAC)
        Dim wano As Integer = Year(wFECHNAC)
        ddlDia.SelectedValue = wdia
        ddlMes.SelectedValue = wmes
        ddlAnio.SelectedValue = wano - 1909
        cn.Close()

        cn.Open()
        Dim sqlpf As String = "select * from REGISDIGPROFESION where IDREGISIDIG=" & wCODIGO
        Dim Psqlpf As New SqlClient.SqlCommand(sqlpf, cn)
        Dim drpf As SqlClient.SqlDataReader = Psqlpf.ExecuteReader
        If drpf.HasRows = True Then
            While (drpf.Read())
                If drpf.GetInt32(1) = 1 Then
                    chkActividad1.Checked = True
                Else
                    chkActividad1.Checked = False
                End If
                If drpf.GetInt32(2) = 1 Then
                    chkActividad2.Checked = True
                Else
                    chkActividad2.Checked = False
                End If
                If drpf.GetInt32(3) = 1 Then
                    chkActividad3.Checked = True
                Else
                    chkActividad3.Checked = False
                End If
                If drpf.GetInt32(4) = 1 Then
                    chkActividad4.Checked = True
                Else
                    chkActividad4.Checked = False
                End If
                If drpf.GetInt32(5) = 1 Then
                    chkActividad5.Checked = True
                Else
                    chkActividad5.Checked = False
                End If
                If drpf.GetInt32(6) = 1 Then
                    chkActividad6.Checked = True
                Else
                    chkActividad6.Checked = False
                End If
                If drpf.GetInt32(7) = 1 Then
                    chkActividad7.Checked = True
                Else
                    chkActividad7.Checked = False
                End If
                If drpf.GetInt32(8) = 1 Then
                    chkActividad8.Checked = True
                Else
                    chkActividad8.Checked = False
                End If
                If drpf.GetInt32(9) = 1 Then
                    chkActividad9.Checked = True
                Else
                    chkActividad9.Checked = False
                End If
                If drpf.GetInt32(10) = 1 Then
                    chkActividad10.Checked = True
                Else
                    chkActividad10.Checked = False
                End If
                If drpf.GetInt32(11) = 1 Then
                    chkActividad11.Checked = True
                Else
                    chkActividad11.Checked = False
                End If
                If drpf.GetInt32(12) = 1 Then
                    chkActividad12.Checked = True
                Else
                    chkActividad12.Checked = False
                End If
                If drpf.GetInt32(13) = 1 Then
                    chkActividad13.Checked = True
                Else
                    chkActividad13.Checked = False
                End If
                If drpf.GetInt32(14) = 1 Then
                    chkActividad14.Checked = True
                Else
                    chkActividad14.Checked = False
                End If
                If drpf.GetInt32(15) = 1 Then
                    chkActividad15.Checked = True
                Else
                    chkActividad15.Checked = False
                End If
                If drpf.GetInt32(16) = 1 Then
                    chkActividad16.Checked = True
                Else
                    chkActividad16.Checked = False
                End If
                If drpf.GetInt32(17) = 1 Then
                    chkActividad17.Checked = True
                Else
                    chkActividad17.Checked = False
                End If
                If drpf.GetInt32(18) = 1 Then
                    chkActividad18.Checked = True
                Else
                    chkActividad18.Checked = False
                End If
                If drpf.GetInt32(19) = 1 Then
                    chkActividad19.Checked = True
                Else
                    chkActividad19.Checked = False
                End If
                If drpf.GetInt32(20) = 1 Then
                    chkActividad20.Checked = True
                Else
                    chkActividad20.Checked = False
                End If
                If drpf.GetInt32(21) = 1 Then
                    chkActividad21.Checked = True
                Else
                    chkActividad21.Checked = False
                End If
                If drpf.GetInt32(22) = 1 Then
                    chkActividad22.Checked = True
                Else
                    chkActividad22.Checked = False
                End If

            End While
        End If
        drpf.Close()
        cn.Close()

        cn.Open()
        Dim sqlpd As String = "select * from REGISDIGDISCIPLI where IDREGISDIG=" & wCODIGO
        Dim Psqlpd As New SqlClient.SqlCommand(sqlpd, cn)
        Dim drpd As SqlClient.SqlDataReader = Psqlpd.ExecuteReader
        If drpd.HasRows = True Then
            While (drpd.Read())
                If drpd.GetInt32(1) = 1 Then
                    ChkDiscipli1.Checked = True
                Else
                    ChkDiscipli1.Checked = False
                End If
                If drpd.GetInt32(2) = 1 Then
                    ChkDiscipli2.Checked = True
                Else
                    ChkDiscipli2.Checked = False
                End If
                If drpd.GetInt32(3) = 1 Then
                    ChkDiscipli3.Checked = True
                Else
                    ChkDiscipli3.Checked = False
                End If
                If drpd.GetInt32(4) = 1 Then
                    ChkDiscipli4.Checked = True
                Else
                    ChkDiscipli4.Checked = False
                End If
                If drpd.GetInt32(5) = 1 Then
                    ChkDiscipli5.Checked = True
                Else
                    ChkDiscipli5.Checked = False
                End If
                If drpd.GetInt32(6) = 1 Then
                    ChkDiscipli6.Checked = True
                Else
                    ChkDiscipli6.Checked = False
                End If
                If drpd.GetInt32(7) = 1 Then
                    ChkDiscipli7.Checked = True
                Else
                    ChkDiscipli7.Checked = False
                End If
                If drpd.GetInt32(8) = 1 Then
                    ChkDiscipli8.Checked = True
                Else
                    ChkDiscipli8.Checked = False
                End If
                If drpd.GetInt32(9) = 1 Then
                    ChkDiscipli9.Checked = True
                Else
                    ChkDiscipli9.Checked = False
                End If
                If drpd.GetInt32(10) = 1 Then
                    ChkDiscipli10.Checked = True
                Else
                    ChkDiscipli10.Checked = False
                End If
                If drpd.GetInt32(11) = 1 Then
                    ChkDiscipli11.Checked = True
                Else
                    ChkDiscipli11.Checked = False
                End If
                If drpd.GetInt32(12) = 1 Then
                    ChkDiscipli12.Checked = True
                Else
                    ChkDiscipli12.Checked = False
                End If
                If drpd.GetInt32(13) = 1 Then
                    ChkDiscipli13.Checked = True
                Else
                    ChkDiscipli13.Checked = False
                End If
                If drpd.GetInt32(14) = 1 Then
                    ChkDiscipli14.Checked = True
                Else
                    ChkDiscipli14.Checked = False
                End If
                If drpd.GetInt32(15) = 1 Then
                    ChkDiscipli15.Checked = True
                Else
                    ChkDiscipli15.Checked = False
                End If
                If drpd.GetInt32(16) = 1 Then
                    ChkDiscipli16.Checked = True
                Else
                    ChkDiscipli16.Checked = False
                End If
                If drpd.GetInt32(17) = 1 Then
                    ChkDiscipli17.Checked = True
                Else
                    ChkDiscipli17.Checked = False
                End If
                If drpd.GetInt32(18) = 1 Then
                    ChkDiscipli18.Checked = True
                Else
                    ChkDiscipli18.Checked = False
                End If
                If drpd.GetInt32(19) = 1 Then
                    ChkDiscipli19.Checked = True
                Else
                    ChkDiscipli19.Checked = False
                End If
                If drpd.GetInt32(20) = 1 Then
                    ChkDiscipli20.Checked = True
                Else
                    ChkDiscipli20.Checked = False
                End If
                If drpd.GetInt32(21) = 1 Then
                    ChkDiscipli21.Checked = True
                Else
                    ChkDiscipli21.Checked = False
                End If
                If drpd.GetInt32(22) = 1 Then
                    ChkDiscipli22.Checked = True
                Else
                    ChkDiscipli22.Checked = False
                End If
                If drpd.GetInt32(23) = 1 Then
                    ChkDiscipli23.Checked = True
                Else
                    ChkDiscipli23.Checked = False
                End If
                If drpd.GetInt32(24) = 1 Then
                    ChkDiscipli24.Checked = True
                Else
                    ChkDiscipli24.Checked = False
                End If
                If drpd.GetInt32(25) = 1 Then
                    ChkDiscipli25.Checked = True
                Else
                    ChkDiscipli25.Checked = False
                End If
                If drpd.GetInt32(26) = 1 Then
                    ChkDiscipli26.Checked = True
                Else
                    ChkDiscipli26.Checked = False
                End If
                If drpd.GetInt32(27) = 1 Then
                    ChkDiscipli27.Checked = True
                Else
                    ChkDiscipli27.Checked = False
                End If
                If drpd.GetInt32(28) = 1 Then
                    ChkDiscipli28.Checked = True
                Else
                    ChkDiscipli28.Checked = False
                End If
                If drpd.GetInt32(29) = 1 Then
                    ChkDiscipli29.Checked = True
                Else
                    ChkDiscipli29.Checked = False
                End If
                If drpd.GetInt32(30) = 1 Then
                    ChkDiscipli30.Checked = True
                Else
                    ChkDiscipli30.Checked = False
                End If
                If drpd.GetInt32(31) = 1 Then
                    ChkDiscipli31.Checked = True
                Else
                    ChkDiscipli31.Checked = False
                End If
                If drpd.GetInt32(32) = 1 Then
                    ChkDiscipli32.Checked = True
                Else
                    ChkDiscipli32.Checked = False
                End If
                If drpd.GetInt32(33) = 1 Then
                    ChkDiscipli33.Checked = True
                Else
                    ChkDiscipli33.Checked = False
                End If
                If drpd.GetInt32(34) = 1 Then
                    ChkDiscipli34.Checked = True
                Else
                    ChkDiscipli34.Checked = False
                End If
                If drpd.GetInt32(35) = 1 Then
                    ChkDiscipli35.Checked = True
                Else
                    ChkDiscipli35.Checked = False
                End If

            End While
        End If
        drpd.Close()
        cn.Close()

        'Redes Sociales

        cn.Open()
        Dim sqlr As String = "select REDSOCIAL from REGISDIGREDES where IDREGISDIG=" & LabelSoli.Text
        Dim Psqlr As New SqlClient.SqlCommand(sqlr, cn)
        Dim drr As SqlClient.SqlDataReader = Psqlr.ExecuteReader
        While drr.Read()
            Dim wred As Integer = drr.GetInt32(0)
            Select Case wred
                Case 1
                    facebook.Checked = True
                Case 2
                    instagram.Checked = True
                Case 3
                    twiter.Checked = True
                Case 4
                    youtube.Checked = True
            End Select
        End While
        drr.Close()
        cn.Close()

        aKeyFields(F_CUIL) = wuser
        aKeyFields(F_APELLIDO) = wAPELLIDO
        aKeyFields(F_NOMBRE) = wNOMBRE
        aKeyFields(F_PROVINCIA) = wPROVINCIA
        aKeyFields(F_LOCALIDAD) = wLOCALIDAD
        aKeyFields(F_DOMICILIO) = wDOMIPART
        ViewState("KEY_FIELDS") = aKeyFields
    End Sub

    Protected Sub BtnEnviar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnviar.Click
        'Begin of Nuevo
        'Limpio los campos
        FailureText.Text = ""
        txtErrorActividad.Text = ""
        lblErrorCheckBoxEdad.Text = ""
        lblErrorCheckBox1.Text = ""
        lblErrorCheckBoxAcepto.Text = ""
        lblErrorcheckAutorizoPublicar.Text = ""
        lblErrorTextBoxCUIT.Text = ""
        lblErrorFechaNacimiento.Text = ""
        lblErrorddlSexo.Text = ""
        lblErrorDdlLocalidad.Text = ""
        lblErrorTelefono.Text = ""
        lblErrorCelular.Text = ""
        lblErrorTextBoxMail.Text = ""
        'lblErrorTextBoxContra.Text = ""
        lblErrorDdlPregunta.Text = ""
        lblErrorTextBoxRespuesta.Text = ""
        lblErrorDdlNacional.Text = ""
        lblErrorTextBoxresid.Text = ""
        LabelErrorFormacion.Text = ""
        LabelErrorTitulo.Text = ""
        'Chequeo los checkbox
        If CheckBox1.Checked = False Then
            lblErrorCheckBox1.Text = " Debe aceptar los términos y Condiciones"
            CheckBox1.Focus()
            Return
        End If
        If CheckBoxAcepto.Checked = False Then
            lblErrorCheckBoxAcepto.Text = " Debe aceptar los términos y Condiciones"
            CheckBoxAcepto.Focus()
            Return
        End If
        If checkAutorizoPublicar.Checked = False Then
            lblErrorcheckAutorizoPublicar.Text = " Debe aceptar los términos y Condiciones"
            Return
        End If
        If Not chkActividad1.Checked And Not chkActividad2.Checked And Not chkActividad3.Checked And
            Not chkActividad4.Checked And Not chkActividad5.Checked And Not chkActividad6.Checked And
            Not chkActividad7.Checked And Not chkActividad8.Checked And Not chkActividad9.Checked And
            Not chkActividad10.Checked And Not chkActividad11.Checked And Not chkActividad12.Checked And
            Not chkActividad13.Checked And Not chkActividad14.Checked And Not chkActividad15.Checked And
            Not chkActividad16.Checked And Not chkActividad17.Checked And Not chkActividad18.Checked And
            Not chkActividad19.Checked And Not chkActividad20.Checked And Not chkActividad21.Checked And
            Not chkActividad22.Checked Then
            txtErrorActividad.Text = " Debe seleccionar al menos una profesión"
            chkActividad1.Focus()
            Return
        End If
        If Not ChkDiscipli1.Checked And Not ChkDiscipli2.Checked And Not ChkDiscipli3.Checked And
            Not ChkDiscipli4.Checked And Not ChkDiscipli5.Checked And Not ChkDiscipli6.Checked And
            Not ChkDiscipli7.Checked And Not ChkDiscipli8.Checked And Not ChkDiscipli9.Checked And
            Not ChkDiscipli10.Checked And Not ChkDiscipli11.Checked And Not ChkDiscipli12.Checked And
            Not ChkDiscipli13.Checked And Not ChkDiscipli14.Checked And Not ChkDiscipli15.Checked And
            Not ChkDiscipli16.Checked And Not ChkDiscipli17.Checked And Not ChkDiscipli18.Checked And
            Not ChkDiscipli19.Checked And Not ChkDiscipli20.Checked And Not ChkDiscipli21.Checked And
            Not ChkDiscipli20.Checked And Not ChkDiscipli21.Checked And Not ChkDiscipli22.Checked And
            Not ChkDiscipli23.Checked And Not ChkDiscipli24.Checked And Not ChkDiscipli25.Checked And
            Not ChkDiscipli26.Checked And Not ChkDiscipli27.Checked And Not ChkDiscipli28.Checked And
            Not ChkDiscipli29.Checked And Not ChkDiscipli30.Checked And Not ChkDiscipli31.Checked And
            Not ChkDiscipli32.Checked And Not ChkDiscipli33.Checked And Not ChkDiscipli34.Checked And
            Not ChkDiscipli35.Checked Then
            txtErrorDisciplina.Text = " Debe seleccionar al menos una disciplina"
            ChkDiscipli1.Focus()
            Return
        End If
        'End of chequeo los checkbox
        'Valido CUIL
        If Not Validaciones.ValidarCUIT(TextBoxCUIT.Text.Trim()) Then
            lblErrorTextBoxCUIT.Text = "CUIT/CUIL erróneo"
            TextBoxCUIT.Focus()
            Return
        End If
        Dim wcuit As Decimal
        wcuit = TextBoxCUIT.Text
        'Valido fecha de nacimiento
        Dim wd As Integer
        Dim wm As Integer
        Dim wa As Integer
        Try
            wd = ddlDia.SelectedItem.Text
            wm = ddlMes.SelectedValue
            wa = ddlAnio.SelectedItem.Text
        Catch ex As Exception
            lblErrorFechaNacimiento.Text = " Fecha de Nacimiento Incorrecta"
            ddlDia.Focus()
            Return
        End Try
        If Not Validaciones.ValidarFecha(wd, wm, wa) Then
            lblErrorFechaNacimiento.Text = "Fecha de Nacimiento Incorrecta"
            ddlDia.Focus()
            Return
        End If
        Dim wfechnac As DateTime = DateTime.Now
        If Not GetFecha(wd.ToString + "/" + wm.ToString + "/" + wa.ToString, wfechnac) Then
            lblErrorFechaNacimiento.Text = " Fecha de Nacimiento Incorrecta"
            ddlDia.Focus()
            Return
        End If
        If Calcular_Edad(wd.ToString + "/" + wm.ToString + "/" + wa.ToString) < 18 Then
            If CheckBoxEdad.Checked = False Then
                lblErrorCheckBoxEdad.Text = " Debe aceptar Condición de edad"
                CheckBoxEdad.Focus()
                Return
            End If
        End If
        If ddlSexo.SelectedValue = 0 Then
            lblErrorddlSexo.Text = " Debe Ingresar el sexo"
            ddlSexo.Focus()
            Return
        Else
            Dim wsexo As Integer = ddlSexo.SelectedValue
        End If
        If DdlLocalidad.SelectedValue = 0 Then
            lblErrorDdlLocalidad.Text = " Debe seleccionar localidad"
            DdlLocalidad.Focus()
            Return
        Else
            Dim wlocalidad As Integer = DdlLocalidad.SelectedValue
        End If
        'Validación teléfonos
        Dim wprefipart As Integer = 0
        Dim wtelepart As Integer = 0
        Dim wpreficelu As Integer = 0
        Dim wcelupart As Integer = 0
        If Len(RTrim(TextBoxPrefTelPart.Text)) > 0 Then
            Try
                wprefipart = CInt(TextBoxPrefTelPart.Text)
            Catch ex As Exception
                TextBoxPrefTelPart.Focus()
                lblErrorTelefono.Text = " Prefijo TE particular Incorrecto"
                Return
            End Try
            If wprefipart < 11 And wprefipart > 0 Then
                TextBoxPrefTelPart.Focus()
                lblErrorTelefono.Text = " Prefijo TE particular Incorrecto"
                Return
            End If
        End If
        If Len(RTrim(TextBoxTelPart.Text)) > 0 Then
            Try
                wtelepart = CInt(TextBoxTelPart.Text)
            Catch ex As Exception
                TextBoxPrefTelPart.Focus()
                lblErrorTelefono.Text = " Teléfono particular Incorrecto"
                Return
            End Try
            If wtelepart < 10000 And wtelepart > 0 Then
                TextBoxPrefTelPart.Focus()
                lblErrorTelefono.Text = " Teléfono particular Incorrecto"
                Return
            End If
        End If
        If Len(RTrim(TextBoxPrefTelPart.Text)) + Len(RTrim(TextBoxTelPart.Text)) > 10 Then
            lblErrorTelefono.Text = " Teléfono particular Incorrecto"
            TextBoxPrefTelPart.Focus()
            Return
        End If
        If Len(RTrim(TextBoxPrefCelu.Text)) > 0 Then
            Try
                wpreficelu = CInt(TextBoxPrefCelu.Text)
            Catch ex As Exception
                lblErrorCelular.Text = " Prefijo TE Celular Incorrecto"
                TextBoxPrefCelu.Focus()
                Return
            End Try
            If wpreficelu < 11 And wpreficelu > 0 Then
                lblErrorCelular.Text = " Prefijo TE Celular Incorrecto"
                TextBoxPrefCelu.Focus()
                Return
            End If
        Else
            lblErrorCelular.Text = " Complete datos del TE celular"
            TextBoxPrefCelu.Focus()
            Return
        End If
        If Len(RTrim(TextBoxCelular.Text)) > 0 Then
            Try
                wcelupart = CInt(TextBoxCelular.Text)
            Catch ex As Exception
                lblErrorCelular.Text = " Teléfono Celular Incorrecto"
                TextBoxPrefCelu.Focus()
                Return
            End Try
            If wtelepart < 10000 And wtelepart > 0 Then
                lblErrorCelular.Text = " Teléfono Celular Incorrecto"
                TextBoxPrefCelu.Focus()
                Return
            End If
        Else
            lblErrorCelular.Text = " Complete datos del TE celular"
            TextBoxPrefCelu.Focus()
            Return
        End If
        If Len(RTrim(TextBoxPrefCelu.Text)) + Len(RTrim(TextBoxCelular.Text)) > 10 Then
            lblErrorTelefono.Text = " Teléfono particular Incorrecto"
            TextBoxPrefTelPart.Focus()
            Return
        End If
        If (wprefipart = 0 Or wtelepart = 0) And (wpreficelu = 0 Or wcelupart = 0) Then
            lblErrorTelefono.Text = " Debe ingresar por lo menos un Teléfono"
            TextBoxPrefTelPart.Focus()
            Return
        End If
        'End of validación teléfonos
        Dim sMail As String
        Dim sMailConfirmacion As String
        sMail = TextBoxMail.Text.Trim
        sMailConfirmacion = TextBoxConfMail.Text.Trim
        If sMail <> sMailConfirmacion Then
            lblErrorTextBoxMail.Text = " No coincide el email y su confirmación"
            TextBoxConfMail.Focus()
            Return
        End If
        If YaExiste(sMail) Then
            lblErrorTextBoxMail.Text = " Esta cuenta de correo ya ha sido utilizada por otra persona"
            TextBoxConfMail.Focus()
            Return
        End If
        Dim arr As Integer = TextBoxMail.Text.Trim.IndexOf("@")
        If arr <= 0 And Len(TextBoxMail.Text.Trim) > 0 Then
            lblErrorTextBoxMail.Text = " Cuenta de Correo Electrónica errónea"
            TextBoxConfMail.Focus()
            Return
        End If

        Dim wpregunta As Integer = DdlPregunta.SelectedValue
        If wpregunta = 0 Then
            lblErrorDdlPregunta.Text = " Debe elegir pregunta de contraseña"
            DdlPregunta.Focus()
            Return
        End If
        Dim wrespuesta As String = RTrim(TextBoxRespuesta.Text).ToUpper
        If Len(wrespuesta) = 0 Then
            lblErrorTextBoxRespuesta.Text = " Debe ingresar respuesta de contraseña"
            TextBoxRespuesta.Focus()
            Return
        End If
        Dim wnacional As Integer = DdlNacional.SelectedValue
        If wnacional = 0 Then
            lblErrorDdlNacional.Text = " Debe elegir Nacionalidad"
            DdlNacional.Focus()
            Return
        End If
        Dim wresidencia As Integer = 0
        If wnacional = 2 Then
            Try
                wresidencia = CInt(TextBoxresid.Text)
            Catch ex As Exception
                lblErrorTextBoxresid.Text = " Residencia Incorrecta"
                TextBoxresid.Focus()
                Return
            End Try
        End If
        If wnacional = 2 And (wresidencia >= 0 And wresidencia <= 4) Then
            lblErrorTextBoxresid.Text = " Residencia Incorrecta, debe ser mayor a 4 años"
            TextBoxresid.Focus()
            Return
        End If
        Dim wcodigo As Integer = 0
        Try
            wcodigo = CInt(LabelSoli.Text)
        Catch ex As Exception
            FailureText.Text = " Solicitud Incorrecta"
            Return
        End Try
        Dim wformacion As Integer = DdlFormacion.SelectedValue
        If wformacion = 0 Then
            LabelErrorFormacion.Text = "Debe seleccionar Tipo de Formación "
            Return
        End If
        Dim wtitulo As Integer = DdlTitulo.SelectedValue
        If wformacion = 1 And wtitulo = 0 Then
            LabelErrorTitulo.Text = "Debe seleccionar Título alcanzado"
            Return
        End If

        'End of Nuevo
        Dim wapellido As String = RTrim(TextBoxApellido.Text).ToUpper
        Dim wnombre As String = RTrim(TextBoxNombre.Text).ToUpper
        Dim wcopost = CInt(TextBoxCopost.Text)
        Dim wdomipart As String = RTrim(TextBoxDompar.Text).ToUpper
        Dim wemail As String = RTrim(TextBoxMail.Text)
        Dim wobservaotros As String = ""

        If Funciones.CaracteresEspeciales(wnombre) Then
            FailureText.Text = "El nombre contiene caracteres especiales"
            TextBoxNombre.Focus()
            Return
        End If

        If Funciones.CaracteresEspecialesn(wapellido) Then
            FailureText.Text = "El apellido contiene caracteres especiales"
            TextBoxApellido.Focus()
            Return
        End If

        Dim sSql As String = "Execute actu_regisdig_fisica_activ @WPROVINCIA,@WPERSONA,@WCODIGO,@WFECHNAC,@WAPELLIDO," &
         "@WNOMBRE,@WSEXO,@WLOCALIDAD,@WCOPOST,@WDOMIPART,@WPREFIPART,@WTELEPART,@WPREFICELU,@WCELUPART,@WEMAIL," &
         "@WPREGUNTA,@WRESPUESTA,@WDENOMINACION,@WPERSONERIA,@WDOMICILIO,@WNACIONAL,@WRESIDENCIA," &
          "@actividad1, @actividad2, @actividad3, " &
          "@actividad4, @actividad5, @actividad6, " &
          "@actividad7, @actividad8, @actividad9, " &
          "@actividad10, @actividad11, @actividad12, " &
          "@actividad13, @actividad14, @actividad15, " &
          "@actividad16, @actividad17, @actividad18, @wobservaotros "

        Dim cmd As New SqlClient.SqlCommand(sSql, cn)
        If Session("id_provincia") <> ddlProvincia.SelectedItem.Value Then
            cmd.Parameters.AddWithValue("@WPROVINCIA", ddlProvincia.SelectedItem.Value)
        Else
            cmd.Parameters.AddWithValue("@WPROVINCIA", Session("id_provincia"))
        End If
        cmd.Parameters.AddWithValue("@WPERSONA", Session("id_persona"))
        cmd.Parameters.AddWithValue("@WCODIGO", wcodigo)
        cmd.Parameters.AddWithValue("@WFECHNAC", wfechnac)
        cmd.Parameters.AddWithValue("@WAPELLIDO", wapellido.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WNOMBRE", wnombre.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WSEXO", ddlSexo.SelectedValue)
        cmd.Parameters.AddWithValue("@WLOCALIDAD", DdlLocalidad.SelectedValue)
        cmd.Parameters.AddWithValue("@WCOPOST", wcopost)
        cmd.Parameters.AddWithValue("@WDOMIPART", wdomipart.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WPREFIPART", wprefipart)
        cmd.Parameters.AddWithValue("@WTELEPART", wtelepart)
        cmd.Parameters.AddWithValue("@WPREFICELU", wpreficelu)
        cmd.Parameters.AddWithValue("@WCELUPART", wcelupart)
        cmd.Parameters.AddWithValue("@WEMAIL", wemail)
        'cmd.Parameters.AddWithValue("@WCONTRASENA", wcontrasena)
        cmd.Parameters.AddWithValue("@WPREGUNTA", DdlPregunta.SelectedValue)
        cmd.Parameters.AddWithValue("@WRESPUESTA", wrespuesta.Trim.ToUpper)
        cmd.Parameters.AddWithValue("@WDENOMINACION", "")
        cmd.Parameters.AddWithValue("@WPERSONERIA", 0)
        cmd.Parameters.AddWithValue("@WDOMICILIO", "")
        cmd.Parameters.AddWithValue("@WNACIONAL", wnacional)
        cmd.Parameters.AddWithValue("@WRESIDENCIA", wresidencia)

        cmd.Parameters.AddWithValue("@actividad1", 0)
        cmd.Parameters.AddWithValue("@actividad2", 0)
        cmd.Parameters.AddWithValue("@actividad3", 0)
        cmd.Parameters.AddWithValue("@actividad4", 0)
        cmd.Parameters.AddWithValue("@actividad5", 0)
        cmd.Parameters.AddWithValue("@actividad6", 0)
        cmd.Parameters.AddWithValue("@actividad7", 0)
        cmd.Parameters.AddWithValue("@actividad8", 0)
        cmd.Parameters.AddWithValue("@actividad9", 0)
        cmd.Parameters.AddWithValue("@actividad10", 0)
        cmd.Parameters.AddWithValue("@actividad11", 0)
        cmd.Parameters.AddWithValue("@actividad12", 0)
        cmd.Parameters.AddWithValue("@actividad13", 0)
        cmd.Parameters.AddWithValue("@actividad14", 0)
        cmd.Parameters.AddWithValue("@actividad15", 0)
        cmd.Parameters.AddWithValue("@actividad16", 0)
        cmd.Parameters.AddWithValue("@actividad17", 0)
        cmd.Parameters.AddWithValue("@actividad18", 0)
        cmd.Parameters.AddWithValue("@wobservaotros", wobservaotros)
        cn.Close()
        cn.Open()
        Try
            Dim wcod As Integer = CInt(cmd.ExecuteScalar())
            Session("wsolicitud_") = wcodigo
            Session("id_provincia") = ddlProvincia.SelectedItem.Value
        Catch ex As Exception
            FailureText.Text = " Error al grabar Datos"
            Return
        End Try
        cn.Close()

        Dim wcant As Integer = 0
        cn.Open()
        Dim sqlpf As String = "select count(*) as cant from REGISDIGPROFESION where IDREGISIDIG=" & wcodigo
        Dim Psqlpf As New SqlClient.SqlCommand(sqlpf, cn)
        Dim drpf As SqlClient.SqlDataReader = Psqlpf.ExecuteReader
        While drpf.Read()
            wcant = drpf.GetInt32(0)
        End While
        drpf.Close()
        cn.Close()

        If wcant = 1 Then
            Dim Sqlf As String = "Execute actu_regisdigprofesion @WIDREGISDIG,@WPROFESION1,@WPROFESION2,@WPROFESION3,@WPROFESION4,@WPROFESION5,@WPROFESION6,@WPROFESION7,@WPROFESION8,@WPROFESION9,@WPROFESION10,@WPROFESION11," &
                  "@WPROFESION12,@WPROFESION13,@WPROFESION14,@WPROFESION15,@WPROFESION16,@WPROFESION17,@WPROFESION18,@WPROFESION19,@WPROFESION20,@WPROFESION21,@WPROFESION22"
            Dim cmdpf As New SqlClient.SqlCommand(Sqlf, cn)
            cmdpf.Parameters.AddWithValue("@WIDREGISDIG", wcodigo)
            cmdpf.Parameters.AddWithValue("@WPROFESION1", IIf(chkActividad1.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION2", IIf(chkActividad2.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION3", IIf(chkActividad3.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION4", IIf(chkActividad4.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION5", IIf(chkActividad5.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION6", IIf(chkActividad6.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION7", IIf(chkActividad7.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION8", IIf(chkActividad8.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION9", IIf(chkActividad9.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION10", IIf(chkActividad10.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION11", IIf(chkActividad11.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION12", IIf(chkActividad12.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION13", IIf(chkActividad13.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION14", IIf(chkActividad14.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION15", IIf(chkActividad15.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION16", IIf(chkActividad16.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION17", IIf(chkActividad17.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION18", IIf(chkActividad18.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION19", IIf(chkActividad19.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION20", IIf(chkActividad20.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION21", IIf(chkActividad21.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION22", IIf(chkActividad22.Checked = True, 1, 0))
            cn.Open()
            Try
                cmdpf.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos Profesion"
                Return
            End Try
            cn.Close()
        Else
            Dim Sqlf As String = "Execute alta_regisdigprofesion @WIDREGISDIG,@WPROFESION1,@WPROFESION2,@WPROFESION3,@WPROFESION4,@WPROFESION5,@WPROFESION6,@WPROFESION7,@WPROFESION8,@WPROFESION9,@WPROFESION10,@WPROFESION11," &
                  "@WPROFESION12,@WPROFESION13,@WPROFESION14,@WPROFESION15,@WPROFESION16,@WPROFESION17,@WPROFESION18,@WPROFESION19,@WPROFESION20,@WPROFESION21,@WPROFESION22"
            Dim cmdpf As New SqlClient.SqlCommand(Sqlf, cn)
            cmdpf.Parameters.AddWithValue("@WIDREGISDIG", wcodigo)
            cmdpf.Parameters.AddWithValue("@WPROFESION1", IIf(chkActividad1.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION2", IIf(chkActividad2.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION3", IIf(chkActividad3.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION4", IIf(chkActividad4.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION5", IIf(chkActividad5.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION6", IIf(chkActividad6.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION7", IIf(chkActividad7.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION8", IIf(chkActividad8.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION9", IIf(chkActividad9.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION10", IIf(chkActividad10.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION11", IIf(chkActividad11.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION12", IIf(chkActividad12.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION13", IIf(chkActividad13.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION14", IIf(chkActividad14.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION15", IIf(chkActividad15.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION16", IIf(chkActividad16.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION17", IIf(chkActividad17.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION18", IIf(chkActividad18.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION19", IIf(chkActividad19.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION20", IIf(chkActividad20.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION21", IIf(chkActividad21.Checked = True, 1, 0))
            cmdpf.Parameters.AddWithValue("@WPROFESION22", IIf(chkActividad22.Checked = True, 1, 0))
            cn.Open()
            Try
                cmdpf.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos Profesion"
                Return
            End Try
            cn.Close()
        End If

        wcant = 0
        cn.Open()
        Dim sqlpd As String = "select count(*) as cant from REGISDIGDISCIPLI where IDREGISDIG=" & wcodigo
        Dim Psqlpd As New SqlClient.SqlCommand(sqlpd, cn)
        Dim drpd As SqlClient.SqlDataReader = Psqlpd.ExecuteReader
        While drpd.Read()
            wcant = drpd.GetInt32(0)
        End While
        drpd.Close()
        cn.Close()

        If wcant = 1 Then
            Dim Sqld As String = "Execute actu_regisdigdiscipli @WIDREGISDIG,@WDISCIPLI1,@WDISCIPLI2,@WDISCIPLI3,@WDISCIPLI4,@WDISCIPLI5,@WDISCIPLI6,@WDISCIPLI7,@WDISCIPLI8,@WDISCIPLI9,@WDISCIPLI10,@WDISCIPLI11," &
                  "@WDISCIPLI12,@WDISCIPLI13,@WDISCIPLI14,@WDISCIPLI15,@WDISCIPLI16,@WDISCIPLI17,@WDISCIPLI18,@WDISCIPLI19,@WDISCIPLI20,@WDISCIPLI21,@WDISCIPLI22,@WDISCIPLI23,@WDISCIPLI24,@WDISCIPLI25," &
                  "@WDISCIPLI26,@WDISCIPLI27,@WDISCIPLI28,@WDISCIPLI29,@WDISCIPLI30,@WDISCIPLI31,@WDISCIPLI32,@WDISCIPLI33,@WDISCIPLI34,@WDISCIPLI35"
            Dim cmdpd As New SqlClient.SqlCommand(Sqld, cn)
            cmdpd.Parameters.AddWithValue("@WIDREGISDIG", wcodigo)
            cmdpd.Parameters.AddWithValue("@WDISCIPLI1", IIf(ChkDiscipli1.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI2", IIf(ChkDiscipli2.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI3", IIf(ChkDiscipli3.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI4", IIf(ChkDiscipli4.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI5", IIf(ChkDiscipli5.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI6", IIf(ChkDiscipli6.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI7", IIf(ChkDiscipli7.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI8", IIf(ChkDiscipli8.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI9", IIf(ChkDiscipli9.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI10", IIf(ChkDiscipli10.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI11", IIf(ChkDiscipli11.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI12", IIf(ChkDiscipli12.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI13", IIf(ChkDiscipli13.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI14", IIf(ChkDiscipli14.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI15", IIf(ChkDiscipli15.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI16", IIf(ChkDiscipli16.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI17", IIf(ChkDiscipli17.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI18", IIf(ChkDiscipli18.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI19", IIf(ChkDiscipli19.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI20", IIf(ChkDiscipli20.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI21", IIf(ChkDiscipli21.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI22", IIf(ChkDiscipli22.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI23", IIf(ChkDiscipli23.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI24", IIf(ChkDiscipli24.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI25", IIf(ChkDiscipli25.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI26", IIf(ChkDiscipli26.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI27", IIf(ChkDiscipli27.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI28", IIf(ChkDiscipli28.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI29", IIf(ChkDiscipli29.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI30", IIf(ChkDiscipli30.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI31", IIf(ChkDiscipli31.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI32", IIf(ChkDiscipli32.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI33", IIf(ChkDiscipli33.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI34", IIf(ChkDiscipli34.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI35", IIf(ChkDiscipli35.Checked = True, 1, 0))
            cn.Open()
            Try
                cmdpd.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos Disciplina"
                Return
            End Try
            cn.Close()
        Else
            Dim Sqld As String = "Execute alta_regisdigdiscipli @WIDREGISDIG,@WDISCIPLI1,@WDISCIPLI2,@WDISCIPLI3,@WDISCIPLI4,@WDISCIPLI5,@WDISCIPLI6,@WDISCIPLI7,@WDISCIPLI8,@WDISCIPLI9,@WDISCIPLI10,@WDISCIPLI11," &
                  "@WDISCIPLI12,@WDISCIPLI13,@WDISCIPLI14,@WDISCIPLI15,@WDISCIPLI16,@WDISCIPLI17,@WDISCIPLI18,@WDISCIPLI19,@WDISCIPLI20,@WDISCIPLI21,@WDISCIPLI22,@WDISCIPLI23,@WDISCIPLI24,@WDISCIPLI25," &
                  "@WDISCIPLI26,@WDISCIPLI27,@WDISCIPLI28,@WDISCIPLI29,@WDISCIPLI30,@WDISCIPLI31,@WDISCIPLI32,@WDISCIPLI33,@WDISCIPLI34,@WDISCIPLI35"
            Dim cmdpd As New SqlClient.SqlCommand(Sqld, cn)
            cmdpd.Parameters.AddWithValue("@WIDREGISDIG", wcodigo)
            cmdpd.Parameters.AddWithValue("@WDISCIPLI1", IIf(ChkDiscipli1.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI2", IIf(ChkDiscipli2.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI3", IIf(ChkDiscipli3.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI4", IIf(ChkDiscipli4.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI5", IIf(ChkDiscipli5.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI6", IIf(ChkDiscipli6.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI7", IIf(ChkDiscipli7.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI8", IIf(ChkDiscipli8.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI9", IIf(ChkDiscipli9.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI10", IIf(ChkDiscipli10.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI11", IIf(ChkDiscipli11.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI12", IIf(ChkDiscipli12.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI13", IIf(ChkDiscipli13.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI14", IIf(ChkDiscipli14.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI15", IIf(ChkDiscipli15.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI16", IIf(ChkDiscipli16.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI17", IIf(ChkDiscipli17.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI18", IIf(ChkDiscipli18.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI19", IIf(ChkDiscipli19.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI20", IIf(ChkDiscipli20.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI21", IIf(ChkDiscipli21.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI22", IIf(ChkDiscipli22.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI23", IIf(ChkDiscipli23.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI24", IIf(ChkDiscipli24.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI25", IIf(ChkDiscipli25.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI26", IIf(ChkDiscipli26.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI27", IIf(ChkDiscipli27.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI28", IIf(ChkDiscipli28.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI29", IIf(ChkDiscipli29.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI30", IIf(ChkDiscipli30.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI31", IIf(ChkDiscipli31.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI32", IIf(ChkDiscipli32.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI33", IIf(ChkDiscipli33.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI34", IIf(ChkDiscipli34.Checked = True, 1, 0))
            cmdpd.Parameters.AddWithValue("@WDISCIPLI35", IIf(ChkDiscipli35.Checked = True, 1, 0))
            cn.Open()
            Try
                cmdpd.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos Disciplina"
                Return
            End Try
            cn.Close()
        End If

        'Update whatsapp
        Dim Whatsapp As Integer = DDlWhatsApp.SelectedValue
        Dim Sql As String = "update REGISDIG set whatsapp=" & Whatsapp & ",formacion=" & wformacion & ",titulo=" & wtitulo & " where codigo=" & wcodigo
        Dim cmdw As New SqlClient.SqlCommand(Sql, cn)
        cn.Open()
        Try
            cmdw.ExecuteNonQuery()
        Catch ex As Exception
            FailureText.Text = " Error al grabar Datos"
            Return
        End Try
        cn.Close()
        ' REDES SOCIALES
        Sql = "delete from REGISDIGREDES where IDREGISDIG=" & wcodigo
        Dim cmdd As New SqlClient.SqlCommand(Sql, cn)
        cn.Open()
        Try
            cmdd.ExecuteNonQuery()
        Catch ex As Exception
            FailureText.Text = " Error al grabar Datos"
            Return
        End Try
        cn.Close()
        If facebook.Checked = True Then
            Sql = "insert into REGISDIGREDES (IDREGISDIG,REDSOCIAL) values (" & wcodigo & "," & 1 & ")"
            Dim cmdr As New SqlClient.SqlCommand(Sql, cn)
            cn.Open()
            Try
                cmdr.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos"
                Return
            End Try
            cn.Close()
        End If
        If instagram.Checked = True Then
            Sql = "insert into REGISDIGREDES (IDREGISDIG,REDSOCIAL) values (" & wcodigo & "," & 2 & ")"
            Dim cmdr As New SqlClient.SqlCommand(Sql, cn)
            cn.Open()
            Try
                cmdr.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos"
                Return
            End Try
            cn.Close()
        End If
        If twiter.Checked = True Then
            Sql = "insert into REGISDIGREDES (IDREGISDIG,REDSOCIAL) values (" & wcodigo & "," & 3 & ")"
            Dim cmdr As New SqlClient.SqlCommand(Sql, cn)
            cn.Open()
            Try
                cmdr.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos"
                Return
            End Try
            cn.Close()
        End If
        If youtube.Checked = True Then
            Sql = "insert into REGISDIGREDES (IDREGISDIG,REDSOCIAL) values (" & wcodigo & "," & 4 & ")"
            Dim cmdr As New SqlClient.SqlCommand(Sql, cn)
            cn.Open()
            Try
                cmdr.ExecuteNonQuery()
            Catch ex As Exception
                FailureText.Text = " Error al grabar Datos"
                Return
            End Try
            cn.Close()
        End If

        'CV Adjunto

        Dim Extension As String = ""
        If UploadImporta.HasFile Then
            Extension = Path.GetExtension(UploadImporta.PostedFile.FileName)
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                FailureText.Text = "El CV no es un documento Adobe .PDF o Word .DOC .DOCX"
                Return
            End If
            Dim sizeInBytes As Long = UploadImporta.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "El CV tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("UploadFileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("UploadImporta1"), FileUpload)
                    Try
                        Extension = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                    Catch ex As Exception
                        Dim FilePath As String = Session("UploadFileName")
                        Dim letra As String = Right(FilePath.Trim, 5)
                        If Left(letra, 1) = "." Then
                            Extension = Right(FilePath.Trim, 5)
                        Else
                            Extension = Right(FilePath.Trim, 4)
                        End If
                    End Try
                    If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                        FailureText.Text = "El CV no es un documento Adobe PDF o Word DOC DOCX"
                        Return
                    End If
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "El CV tiene un tamaño mayor a 10 Mb"
                        Return
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
        BorraAdjuntos()

        'Guardar CV
        Dim woperador As String = Session("CUIL")
        Dim nCodigo As Integer = Session("USER_ID")
        Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
        Extension = Path.GetExtension(UploadImporta.PostedFile.FileName)
        Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
        Dim fileSavePath As Object = Server.MapPath("~/Documentos/REGISDIG/" & nCodigo & "/CV")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If UploadImporta.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                UploadImporta.SaveAs(Filepath)
            Catch ex As Exception
                FailureText.Text = "No se pudo guardar documento de CV"
                Return
            End Try
        Else
            If Session("UploadFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("UploadImporta1"), FileUpload)
                FileName = Session("sDocumento")
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("UploadFileName")
                    Dim jm As Integer = Len(Archivo)
                    FileName = ""
                    While jm > 0
                        Dim letra As String = Mid(Archivo, jm, 1)
                        If letra <> "\" Then
                            FileName = letra + FileName
                        Else
                            jm = 0
                        End If
                        jm = jm - 1
                    End While
                End If
                Dim Filepath As String = Session("UploadFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    FailureText.Text = "No se pudo guardar documento de CV"
                    Return
                End Try
            End If
        End If

        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String

        'Actualizo los integrantes si se modificó el CUIL
        wcuit = TextBoxCUIT.Text.Trim
        If ViewState("KEY_FIELDS")(F_CUIL) <> wcuit Then
            ActualizarCUILIntegrantes(ViewState("KEY_FIELDS")(F_CUIL), wcuit)
        End If

        If Len(RTrim(wemail)) > 0 Then
            Dim wcodi As String = LabelSoli.Text.Trim.ToString
            Dim wpers As String = RTrim(ddlPersona.SelectedItem.Text)
            Dim wprov As String = RTrim(ddlProvincia.SelectedItem.Text)
            Dim wsex As String = RTrim(ddlSexo.SelectedItem.Text)
            Dim wlocali As String = RTrim(DdlLocalidad.SelectedItem.Text)
            Dim wnacion As String = RTrim(DdlNacional.SelectedItem.Text)
            Dim wresid As String = RTrim(TextBoxresid.Text)

            'Modificación de Persona Física
            'Enviar email
            Dim sResult As String
            Dim sSubject As String
            Dim sBody As String

            sSubject = "INTeatroDigital - Actualización de Datos de Persona Humana - CUIL " & TextBoxCUIT.Text

            sBody = "Estimada/o usuaria/o de INTeatroDigital: " & wnombre.Trim.ToUpper & " " & wapellido.Trim.ToUpper & " - " & TextBoxCUIT.Text & "<br />" & "<br />"
            sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE DATOS (PERSONA HUMANA)" & "<br />"
            sBody += "Quedando ingresados los siguientes datos:" & "<br />"
            sBody += "Persona: " & wpers & "<br />"
            sBody += "Provincia: " & wprov & "<br />"
            sBody += "CUIL / CUIT: " & TextBoxCUIT.Text & "<br />"
            sBody += "Fecha de Nacimiento: " & Left(wfechnac.ToString, 10) & "<br />"
            sBody += "Apellido y Nombre: " & RTrim(wapellido) & ", " & RTrim(wnombre) & "<br />"
            'sBody += "Sexo: " & wsex & "<br />"
            sBody += "Nacionalidad: " & wnacion & "<br />"
            'sBody += "Residencia: " & wresid & "<br />"
            sBody += "Localidad: " & wlocali & "<br />"
            sBody += "Domicilio: " & wdomipart & "<br />"
            sBody += "Teléfono Particular: " & wprefipart.ToString & " " & wtelepart.ToString & "<br />"
            sBody += "Teléfono Celular: " & wpreficelu.ToString & " 15" & wcelupart.ToString & "<br />"
            sBody += "<br />"

            'sBody += Mail.GetTextoAviso(MAIL_MODIF_INDIV_FIS) & "<br />"
            'sBody += "<br />"

            'sBody += "Click para confirmar<br />"
            sBody += Mail.GetLink(MAIL_MODIF_INDIV_FIS, LabelSoli.Text.Trim) & "<br />"
            sBody += "<br />"

            sBody += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
            sBody += "que ve mas arriba en su navegador de internet.<br />"
            sBody += "Lo invitamos a leer detenidamente la sección 'PREGUNTAS FRECUENTES' (ubicada en la barra superior de " & "<br />"
            sBody += "la plataforma de INTeatroDigital) con el objeto de familiarizarse con las particularidades del  " & "<br />"
            sBody += "Registro Nacional del Teatro Independiente.  " & "<br />"
            sBody += "<br />"
            sBody += "Por necesidad de asistencia técnica, consultas, reclamos o sugerencias sobre INTeatroDigital no dude  " & "<br />"
            sBody += "en ingresar a la sección 'FORMULARIO DE CONTACTO' (ubicada en la barra superior de la plataforma) y  " & "<br />"
            sBody += "llenar el formulario correspondiente.  " & "<br />"
            sBody += "<br />"
            sBody += "Gracias." & "<br />"
            sBody += "<br />"
            sBody += "INTeatroDigital" & "<br />"
            sResult = SendMail(Mail.GetMailTo(LabelSoli.Text.Trim, TIPO_PERSONA), sSubject, sBody)

            sMail = MAIL_CONTROL
            sResult = SendMail(sMail, sSubject, sBody)

            'End of Modificación de Persona Física
            'Campos claves modificados
            If CamposModificados() Then

                'Envío un mail a la provincia
                Dim MyReader As SqlDataReader
                Dim sResult2 As String = ""
                Dim sCodigo As String = ""
                Try
                    Dim sProv As Integer = ddlProvincia.SelectedItem.Value
                    'Obtengo el mail de la provinci
                    sSQLCmd = "select mail from provinciasmail where idprovincia=" & sProv
                    MyConnection = New SqlConnection()
                    MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
                    MyConnection.Open()
                    MyCommand = New SqlCommand(sSQLCmd, MyConnection)
                    MyReader = MyCommand.ExecuteReader()
                    Do While MyReader.Read()
                        sMail = MyReader.Item(0).ToString.Trim
                        sSubject = "INTeatroDigital - Actualización de Datos de Persona Humana - CUIL  " & TextBoxCUIT.Text
                        sBody = "ACTUALIZACIÓN DE DATOS PERSONALES del usuario de INTeatroDigital:  " & wnombre.Trim.ToUpper & " " & wapellido.Trim.ToUpper & " - " & TextBoxCUIT.Text & "<br />"
                        sBody += "Quedando ingresados los siguientes datos:  " & "<br />"
                        sBody += "Persona: " & wpers & "<br />"
                        sBody += "Provincia: " & wprov & "<br />"
                        sBody += "CUIL / CUIT: " & TextBoxCUIT.Text & "<br />"
                        sBody += "Fecha de Nacimiento: " & Left(wfechnac.ToString, 10) & "<br />"
                        sBody += "Apellido y Nombre: " & RTrim(wapellido) & ", " & RTrim(wnombre) & "<br />"
                        'sBody += "Sexo: " & wsex & "<br />"
                        sBody += "Nacionalidad: " & wnacion & "<br />"
                        'sBody += "Residencia: " & wresid & "<br />"
                        sBody += "Localidad: " & wlocali & "<br />"
                        sBody += "Domicilio: " & wdomipart & "<br />"
                        sBody += "Teléfono Particular: " & wprefipart.ToString & " " & wtelepart.ToString & "<br />"
                        sBody += "Teléfono Celular: " & wpreficelu.ToString & " 15" & wcelupart.ToString & "<br />"
                        sBody += "<br />"
                        sResult2 = SendMail(sMail, sSubject, sBody)

                        sMail = MAIL_CONTROL
                        sResult2 = SendMail(sMail, sSubject, sBody)
                    Loop
                    MyReader.Dispose()
                    MyCommand.Dispose()
                    MyConnection.Dispose()
                Catch ex As Exception
                End Try
            End If
            'End of Campos claves modificados

            If sResult = "OK" Then
                Session.Add("CUIT_TEMP", TextBoxCUIT.Text)
                Response.Redirect("confirmaRegistro.aspx?r=ok&m=" & sResult & "&t=f")
            Else
                Response.Redirect("confirmaRegistro.aspx?r=er&m=" & sResult & "&t=f")
            End If
            'End of Enviar email
        End If

        Response.Clear()
        Response.Redirect("ConfActuIndiv.aspx", False)

    End Sub

    Protected Function ActualizarCUILIntegrantes(ByVal sCUILViejo As String, ByVal sCUILNuevo As String) As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String

        Try
            'UPDATE Integrantes
            sSQLCmd = "UPDATE Integrantes " &
                           "SET CUIL = " & sCUILNuevo & "  " &
                         "WHERE CUIL = " & sCUILViejo

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()
            MyCommand.Dispose()
            MyConnection.Dispose()
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Function CamposModificados() As Boolean
        If ViewState("KEY_FIELDS")(F_CUIL) <> TextBoxCUIT.Text.Trim Or
            ViewState("KEY_FIELDS")(F_APELLIDO) <> TextBoxApellido.Text.Trim Or
            ViewState("KEY_FIELDS")(F_NOMBRE) <> TextBoxNombre.Text.Trim Or
            ViewState("KEY_FIELDS")(F_PROVINCIA) <> ddlProvincia.SelectedValue Or
            ViewState("KEY_FIELDS")(F_LOCALIDAD) <> DdlLocalidad.SelectedValue Or
            ViewState("KEY_FIELDS")(F_DOMICILIO) <> TextBoxDompar.Text.Trim Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function YaExiste(ByVal mail As String) As Boolean
        If Not cn.State = ConnectionState.Open Then
            cn.Open()
        End If
        Dim sql As String = String.Format("Select Count(*) from regisdig where email = '{0}' and CUIL<>" & TextBoxCUIT.Text, mail)
        Dim cmd As New SqlClient.SqlCommand(sql, cn)
        Dim veces As Integer = CType(cmd.ExecuteScalar, Integer)
        cn.Close()
        If veces > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub ddlProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlProvincia.SelectedIndexChanged
        Dim prov As String = ddlProvincia.SelectedValue.ToString
        cn.Open()
        Dim sql6 As String = "select 0 as codloc,' Seleccione Localidad' as nomloc union select codloc,nomloc from localidades where provincia= " & prov & "order by nomloc"
        Dim Psql6 As New SqlClient.SqlCommand(sql6, cn)
        Dim dr6 As SqlClient.SqlDataReader = Psql6.ExecuteReader
        DdlLocalidad.DataSource = dr6
        DdlLocalidad.DataTextField = "nomloc"
        DdlLocalidad.DataValueField = "codloc"
        DdlLocalidad.DataBind()
        cn.Close()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("menuFinal.aspx")
    End Sub

    Protected Sub AceptoDJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AceptoDJ.CheckedChanged
        tablaDatos.Visible = AceptoDJ.Checked
        CargAdjunto()
    End Sub

    Private Sub CargAdjunto()
        Dim wfecha As Date = DateTime.Now.ToString
        Dim wdia As Integer = wfecha.Day
        Dim wmes As Integer = wfecha.Month
        Dim wano As Integer = wfecha.Year
        Dim whora As Integer = wfecha.Hour
        Dim wminu As Integer = wfecha.Minute
        Dim wsegu As Integer = wfecha.Second
        Dim woperador As String = Session("CUIL")
        Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
        Dim randomName As String = RTrim(woperador) + wdir
        If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
            Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
        End If

        Dim nCodigo As Integer = Session("USER_ID")
        Dim sDocumento As String = ""
        Dim sPath As String = ""
        Dim fileSavePath As String = Server.MapPath("~/Documentos/REGISDIG/" & nCodigo & "/CV/")
        Dim wlong As Integer = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
            Next
        Catch ex As Exception
            sDocumento = ""
        End Try
        If Len(RTrim(sDocumento)) > 0 Then
            Session("UploadImporta1") = UploadImporta
            Session("UploadFileName") = sPath
            Session("sDocumento") = sDocumento
            LabelNombreUpload.Text = sDocumento
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                FailureText.Text = "No se pudo guardar documento de CV"
                Return
            End Try
            Session("UploadFileName") = FilePathDest
        End If
    End Sub

    Private Sub BorraAdjuntos()
        Dim nCodigo As Integer = Session("USER_ID")
        Dim sPath As String = ""
        Dim fileSavePath As String = Server.MapPath("~/Documentos/REGISDIG/" & nCodigo & "/CV/")
        Dim wlong As Integer = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                My.Computer.FileSystem.DeleteFile(sPath)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub BtnVisualiza_Click(sender As Object, e As EventArgs) Handles BtnVisualiza.Click
        FailureText.Text = ""
        If UploadImporta.HasFile Or Session("UploadImporta1") IsNot Nothing Then
            Dim woperador As String = Session("CUIL")
            Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If UploadImporta.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    FailureText.Text = "No es un documento Adobe .PDF o Word .DOC .DOCX"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    UploadImporta.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("UploadImporta1") = UploadImporta
                Session("UploadFileName") = FilePath
            Else
                FilePath = Session("UploadFileName")
                Dim letra As String = Right(FilePath.Trim, 1)
                If UCase(letra) = "X" Then
                    Extension = Right(FilePath.Trim, 5)
                Else
                    Extension = Right(FilePath.Trim, 4)
                End If
            End If

            If UCase(Extension) = ".PDF" Then
                Response.ContentType = "application/pdf"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

            If UCase(Extension) = ".DOC" Then
                Dim fileStream As New FileStream(FilePath, FileMode.Open)
                Dim bytBytes(fileStream.Length) As Byte
                fileStream.Read(bytBytes, 0, fileStream.Length)
                fileStream.Close()
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & FilePath)
                Response.ContentType = "application/msword"
                Response.BinaryWrite(bytBytes)
                Response.Flush()
                Response.Close()
                Response.End()
            End If

            If UCase(Extension) = ".DOCX" Then
                Dim fileStream As New FileStream(FilePath, FileMode.Open)
                Dim memStream As MemoryStream = New MemoryStream()
                memStream.SetLength(fileStream.Length)
                fileStream.Read(memStream.GetBuffer(), 0, fileStream.Length)
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & FilePath)
                Response.BinaryWrite(memStream.ToArray())
                Response.Flush()
                Response.Close()
                Response.End()
            End If

        End If

    End Sub

End Class