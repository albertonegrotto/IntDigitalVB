Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Partial Public Class registroAsistenteTecnico
    Inherits System.Web.UI.Page
    Dim quien As usuario
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sAccion As String
        Dim nCodigo As Integer
        quien = CType(Session("usuario"), usuario)
        Session("CUIT") = quien.Usuario
        Session("nID") = quien.Codigo
        If Not Page.IsPostBack Then
            sAccion = Request.QueryString("accion")
            nCodigo = Request.QueryString("codigo")
            Session.Add("CodRegistro", nCodigo)
            Session("sAccion") = sAccion
            If sAccion = "M" Then
                HyperLinkBack.Attributes.Add("href", "RegistroLista.aspx")
            End If
            If sAccion.ToUpper = "A" Then
                Inicializar()
                BorrarTemporal()
                'CargarTemporal()   'No se carga porque está vacía
                BtnGuardar.Text = "Confirmar Registro"
            ElseIf sAccion.ToUpper = "M" Then
                'SetUserId()
                CargarDatos(nCodigo)
                BtnGuardar.Text = "Confirmar Actualización de Registro"
                Session.Add("CODIGO", nCodigo)
                'Curriculum.Visible = False
                'Foto.Visible = False
            End If
            'Cargo la gridview
            SqlDataSource1.SelectParameters("cuit").DefaultValue = quien.Usuario 'Session("CUIT")
            GridView1.DataBind()
            GridView1.Visible = True

        Else
            'La respuesta
            MaintainScrollPositionOnPostBack = True
            If BtnGuardar.Text = "Confirmar Registro" Then
                GuardarAdjunto()
            End If
        End If

    End Sub

    Private Sub GuardarAdjunto()
        Dim wfecha As Date = DateTime.Now.ToString
        Dim wdia As Integer = wfecha.Day
        Dim wmes As Integer = wfecha.Month
        Dim wano As Integer = wfecha.Year
        Dim whora As Integer = wfecha.Hour
        Dim wminu As Integer = wfecha.Minute
        Dim woperador As String = Session("CUIL")
        If Session("UploadImporta1") Is Nothing Or UploadImporta.HasFile Then
            If UploadImporta.HasFile Then
                Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
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
        If Session("UploadImportaf1") Is Nothing Or UploadImportaf.HasFile Then
            If UploadImportaf.HasFile Then
                Dim FileName As String = Path.GetFileName(UploadImportaf.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(UploadImportaf.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".JPG" Or UCase(Extension) = ".JPEG" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        UploadImportaf.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("UploadImporta1f") = UploadImportaf
                    Session("UploadFileNamef") = FilePath
                    LabelNombreUploadf.Text = UploadImportaf.FileName
                End If
            End If
        Else
            If Session("UploadImporta1f") IsNot Nothing Then
                Try
                    Dim UploadImporta1f As FileUpload = CType(Session("UploadImporta1f"), FileUpload)
                    LabelNombreUploadf.Text = UploadImporta1f.FileName
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub
    Private Sub Inicializar()
        Dim cn As New SqlClient.SqlConnection(SqlConex)

        'Sectores / Grupos
        cn.Open()
        Dim sql As String = "SELECT codigo, descrip FROM Sectores WHERE codigo = 5" '& Session("SECTOR")
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        ddlSectores.DataSource = dr
        ddlSectores.DataTextField = "descrip"
        ddlSectores.DataValueField = "codigo"
        ddlSectores.DataBind()
        cn.Close()
        dr.Close()

        'Provincias
        cn.Open()
        Dim sql2 As String = "Select 0 as codigo, 'Seleccione Provincia' as descrip union SELECT codigo,descrip FROM Provin where region is not null" ' WHERE codigo = " & Session("PROVINCIA")
        Dim Psql2 As New SqlClient.SqlCommand(sql2, cn)
        Dim dr2 As SqlClient.SqlDataReader = Psql2.ExecuteReader
        ddlProvincias.DataSource = dr2
        ddlProvincias.DataTextField = "descrip"
        ddlProvincias.DataValueField = "codigo"
        ddlProvincias.DataBind()
        cn.Close()
        dr2.Close()

        ddlProvincias.SelectedValue = GetProvincia(quien.Codigo).ToString
        ddlProvincias.Enabled = False

    End Sub
    Private Function GetProvincia(ByVal codigo As Integer) As Integer
        Dim cn As New SqlConnection(SqlConex)
        Dim retorno As Integer
        cn.Open()
        Dim sql As String
        sql = "Select provincia from regisdig where codigo = " & codigo.ToString
        Dim cmd As New SqlCommand(sql, cn)
        retorno = cmd.ExecuteScalar
        cn.Close()
        Return retorno
    End Function
    Private Sub BorrarTemporal()
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        cn.Open()
        Dim sql5 As String
        sql5 = "DELETE FROM EspecialidadesTemp WHERE cuit = " & quien.Usuario ' Session("CUIT")
        Dim Psql5 As New SqlClient.SqlCommand(sql5, cn)
        Psql5.ExecuteNonQuery()
        cn.Close()
    End Sub

    Private Sub CargarTemporal(ByVal nCodigo As Integer)
        Dim cn As New SqlClient.SqlConnection(SqlConex)

        'Traspaso Especialidades a EspecialidadesTemp para trabajar temporalmente
        'Especialidades
        cn.Open()
        Dim sql4 As String
        sql4 = "INSERT INTO EspecialidadesTemp (cuit, codigoRegistro, actividad, descripcion) " &
                    "SELECT " & quien.Usuario & ", NULL, actividad, descripcion " &
                        "FROM Especialidades " &
                        "WHERE codigoRegistro = " & nCodigo
        Dim Psql4 As New SqlClient.SqlCommand(sql4, cn)
        Dim dr4 As SqlClient.SqlDataReader = Psql4.ExecuteReader
        cn.Close()
        dr4.Close()
    End Sub

    Private Sub CargarTemporalModificacion()
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        'Traspaso Especialidades a EspecialidadesTemp para trabajar temporalmente
        'Especialidades
        cn.Open()
        Dim sql5 As String
        sql5 = "DELETE FROM EspecialidadesTemp WHERE cuit = " & quien.Usuario ' Session("CUIT")
        Dim Psql5 As New SqlClient.SqlCommand(sql5, cn)
        Psql5.ExecuteNonQuery()
        cn.Close()

        cn.Open()
        Dim sql4 As String
        sql4 = "INSERT INTO EspecialidadesTemp (cuit, codigoRegistro, actividad, descripcion) " &
                    "SELECT " & Session("CUIT") & ", NULL, actividad, descripcion " &
                        "FROM Especialidades " &
                        "WHERE codigoRegistro = " & quien.Codigo ' Session("USER_ID")
        Dim Psql4 As New SqlClient.SqlCommand(sql4, cn)
        Dim dr4 As SqlClient.SqlDataReader = Psql4.ExecuteReader
        cn.Close()
        dr4.Close()
    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGuardar.Click
        Dim sResult As String
        Dim bSaved As Boolean
        Try
            If Not ValidarDatos() Then
                Return
            End If
            Dim sIdRegistro As String = ""
            If BtnGuardar.Text = "Confirmar Registro" Then
                bSaved = GuardarDatos()
                sIdRegistro = Session("CODIGO_REGISTRO")
            Else
                bSaved = ActualizarDatos()
                sIdRegistro = Session("CodRegistro")
            End If

            If Not bSaved Then
                txtErrorAcepto.Text = "Se produjo un error al guardar los datos, por favor intente mas tarde"
                Return
            End If

            'Enviar email
            'Dim sIdRegistro As String = quien.Codigo ' IIf(Session("CODIGO_REGISTRO") Is Nothing, Session("CODIGO"), Session("CODIGO_REGISTRO"))

            Dim sSector As String = RTrim(ddlSectores.SelectedItem.Text)
            Dim sProvincia As String = RTrim(ddlProvincias.SelectedItem.Text)

            'Dim sCV As String = RTrim(txtCV.Text.Trim.ToUpper)
            Dim wfecha As Date = CDate(TextDesde.Value)
            Dim sFechaInicio As String = Right("0" + Day(wfecha).ToString, 2) + "/" + Right("0" + Month(wfecha).ToString, 2) + "/" + Year(wfecha).ToString
            wfecha = CDate(TextDocencia.Value)
            Dim sFechaDocencia As String = Right("0" + Day(wfecha).ToString, 2) + "/" + Right("0" + Month(wfecha).ToString, 2) + "/" + Year(wfecha).ToString

            cn.Close()
            Dim Apellido As String = ""
            Dim Nombre As String = ""
            Dim CUIT As Decimal = Session("CUIT")
            Dim sql As String = "select apellido,nombre from REGISDIG where CUIL=" & CUIT
            cn.Open()
            Dim Psql As New SqlClient.SqlCommand(sql, cn)
            Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
            While dr.Read()
                Apellido = dr.GetString(0)
                Nombre = dr.GetString(1)
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

            Dim nTipoMail As Integer
            Dim sSubject As String
            Dim sBody As String

            If BtnGuardar.Text = "Confirmar Registro" Then
                nTipoMail = MAIL_ALTA_REGISTRO
                sSubject = "INTeatroDigital - Solicitud de Registro de Asistente Técnico"
                sBody = "Estimado usuario de INTeatroDigital: " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
                sBody += "Se ha recepcionado su gestión de: REGISTRO DE ASISTENTE TECNICO" & "<br />"
            Else
                nTipoMail = MAIL_MODIF_REGISTRO
                sSubject = "INTeatroDigital - Actualización de Registro de Asistente Técnico"
                sBody = "Estimado usuario de INTeatroDigital: " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
                sBody += "Registro INT Nº : " & CodigoRegistro & "<br />"
                sBody += "Se ha recepcionado su gestión de: ACTUALIZACION DE REGISTRO DE ASISTENTE TECNICO" & "<br />"
            End If

            sBody += "Quedando ingresados los siguientes datos del Registro:" & "<br />"
            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "Código de Ingreso (Sólo para uso del INT): " & sIdRegistro & "<br />"
            End If
            sBody += "Tipo: " & sSector & "<br />"
            sBody += "Provincia: " & sProvincia & "<br />"
            sBody += "Fecha de Inicio de actividades: " & sFechaInicio & "<br />"
            sBody += "Fecha de Inicio en la Docencia: " & sFechaDocencia & "<br />"
            sBody += "<br />"
            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "Usted ha realizado el trámite de Registro de ASISTENTE TECNICO en INTeatroDigital. Estamos " & "<br />"
            Else
                sBody += "Usted ha realizado el trámite de Actualización de Registro de ASISTENTE TECNICO en INTeatroDigital. Estamos " & "<br />"
            End If
            sBody += "trabajando en el procesamiento de sus datos. Debe clickear en el link que figura al final de este  " & "<br />"
            sBody += "mensaje, con el fin de validar su identidad como usuario. Al hacerlo, se le abrirá en el navegador " & "<br />"
            sBody += "de internet, la plataforma de INTeatroDigital directamente en la sección 'Imprimir Constancias', " & "<br />"
            sBody += "desde la cual deberá emitir y descargar la constancia de registro y enviarla por correo electrónico " & "<br />"
            If BtnGuardar.Text = "Confirmar Registro" Then
                sBody += "a la Representación del INT correspondiente a su Provincia, y en ese mismo mail deberá adjuntar " & "<br />"
                sBody += "copia de frente y dorso de su DNI." & "<br />"
            Else
                sBody += "a la Representación del INT correspondiente a su Provincia." & "<br />"
            End If
            sBody += "<br />"
            sBody += "Una vez recibida la documentación en la Sede Central del INT y procesado los datos, usted recibirá " & "<br />"
            sBody += "en esta dirección de correo electrónico la confirmación definitiva del trámite." & "<br />"
            sBody += "<br />"
            sBody += Mail.GetLink(nTipoMail, sIdRegistro, quien.Codigo) & "<br />"
            sBody += "<br />"

            sBody += "Si este mensaje no lo visualiza en formato HTML, debe copiar el hipervínculo "
            sBody += "que ve mas arriba en su navegador de internet.<br />"
            sBody += "<br />"
            sBody += "Gracias.<br />"
            sBody += "<br />"
            sBody += "INTeatroDigital.<br />"

            Dim nID As String = Session("nID")
            sResult = SendMail(Mail.GetMailTo(nID, TIPO_PERSONA), sSubject, sBody)

            'A la representación provincial:
            Dim sResult2 As String = ""
            Dim wemailprov As String = ""
            cn.Open()
            sql = "select mail from provinciasmail where idprovincia=" & ddlProvincias.SelectedValue
            Dim cdmm As New SqlClient.SqlCommand(sql, cn)
            Dim drp As SqlClient.SqlDataReader = cdmm.ExecuteReader
            While drp.Read
                wemailprov = drp.GetString(0)
            End While
            drp.Close()
            cn.Close()

            If BtnGuardar.Text = "Confirmar Registro" Then
                sSubject = "INTeatroDigital - Solicitud de Registro de Asistente Técnico"
                sBody = "REGISTRO de ASISTENTE TECNICO:  " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
            Else
                sSubject = "INTeatroDigital - Actualización de Registro de Asistente Técnico"
                sBody = "ACTUALIZACION DE REGISTRO de ASISTENTE TECNICO:  " & Nombre & " " & Apellido & " - " & CUIT & "<br />"
                sBody += "REGISTRO INT N°:" & CodigoRegistro & "<br />"
            End If
            sBody += "Quedando ingresados los siguientes datos del Registro:" & "<br />"
            sBody += "Código de Ingreso (sólo para uso del INT):  " & sIdRegistro & "<br />"
            sBody += "Tipo: " & sSector & "<br />"
            sBody += "Provincia: " & sProvincia & "<br />"
            sBody += "Fecha de Inicio de actividades: " & sFechaInicio & "<br />"
            sBody += "Fecha de Inicio en la Docencia: " & sFechaDocencia & "<br />"
            sBody += "<br />"
            sResult2 = SendMail(wemailprov, sSubject, sBody)

            If sResult = "OK" Then
                Response.Redirect("confirmaRegistro.aspx?r=ok&m=" & sResult)
            Else
                Response.Redirect("confirmaRegistro.aspx?r=er&m=" & sResult)
            End If
            'End of Enviar email

        Catch ex As Exception

        End Try
    End Sub

    Protected Function ValidarDatos()
        Dim dFecha As Date
        'Limpio los errores
        txtErrorActividad.Text = ""
        txtErrorEspecialidad.Text = ""
        txtErrorCV.Text = ""
        txtErrorAcepto.Text = ""
        txtErrorFechaInicio.Text = ""
        txtErrorFechaDocencia.Text = ""
        Try
            dFecha = CDate(TextDesde.Value)
        Catch ex As Exception
            txtErrorFechaInicio.Text = "Fecha inválida"
            TextDesde.Focus()
            Return False
        End Try

        Try
            If Not ValidarFecha(TextDesde.Value.Trim) Then
                txtErrorFechaInicio.Text = "Fecha inválida"
                TextDesde.Focus()
                Return False
            End If
        Catch ex As Exception
            txtErrorFechaInicio.Text = "Fecha inválida"
            TextDesde.Focus()
            Return False
        End Try

        Try
            dFecha = CDate(TextDocencia.Value)
        Catch ex As Exception
            txtErrorFechaDocencia.Text = "Fecha inválida"
            TextDocencia.Focus()
            Return False
        End Try
        Try
            If Not ValidarFecha(TextDocencia.Value.Trim) Then
                txtErrorFechaDocencia.Text = "Fecha inválida"
                TextDocencia.Focus()
                Return False
            End If
        Catch ex As Exception
            txtErrorFechaDocencia.Text = "Fecha inválida"
            TextDocencia.Focus()
            Return False
        End Try
        If txtCV.Text.Length > 3000 Then
            txtErrorCV.Text = "Máximo 3000 caracteres"
            txtCV.Focus()
            Return False
        End If

        'Al menos una especialidad cargada
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim nCantidad As Integer

        sSQLCmd = "SELECT count(*) AS cantidad " &
                        "FROM EspecialidadesTemp " &
                        "WHERE cuit = " & quien.Usuario ' Session("CUIT")

        MyConnection = New SqlConnection()
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
        MyCommand = New SqlCommand()
        MyCommand.CommandText = sSQLCmd
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())
        If nCantidad = 0 Then
            txtErrorEspecialidad.Text = "Debe al menos ingresar una especialidad"
            txtErrorEspecialidad.Focus()
            Return False
        End If
        'End of Al menos una especialidad cargada
        If chkAcepto.Checked = False Then
            chkAcepto.Focus()
            txtErrorAcepto.Text = "Debe aceptar los términos para continuar"
            Return False
        End If
        If checkAutorizoPublicar.Checked = False Then
            lblErrorcheckAutorizoPublicar.Text = "Debe aceptar los términos para continuar"
            checkAutorizoPublicar.Focus()
            Return False
        End If

        Dim fcv As Integer = 0
        If UploadImporta.HasFile Then
            Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                FailureText.Text = "El CV no es un documento Adobe .PDF o Word .DOC .DOCX"
                Return False
            Else
                fcv = 1
            End If
            Dim sizeInBytes As Long = UploadImporta.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "El CV tiene un tamaño mayor a 10 Mb"
                Return False
            Else
                fcv = 1
            End If
        Else
            If Session("UploadFileName") IsNot Nothing Then
                Try
                    Dim UploadImporta1 As FileUpload = CType(Session("UploadImporta1"), FileUpload)
                    Dim Extension As String = Path.GetExtension(UploadImporta1.PostedFile.FileName)
                    If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                        FailureText.Text = "El CV no es un documento Adobe PDF o Word DOC DOCX"
                        Return False
                    Else
                        fcv = 1
                    End If
                    Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                    If sizeInBytes / 1000000 > 10 Then
                        FailureText.Text = "El CV tiene un tamaño mayor a 10 Mb"
                        Return False
                    Else
                        fcv = 1
                    End If
                Catch ex As Exception
                End Try
            End If
            If fcv = 0 Then
                FailureText.Text = "Debe cargar el Currículum Vitae"
                Return False
            End If
        End If

        Return True

    End Function

    Protected Function GuardarDatos() As Integer
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim mIdRegistro As SqlParameter
        Dim nIdRegistro As Integer
        Dim sFechaInicio As String
        Dim sFechaDocencia As String

        Try
            Dim wfecha As Date = CDate(TextDesde.Value)
            sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            wfecha = CDate(TextDocencia.Value)
            sFechaDocencia = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)

            'INSERT Registro
            sSQLCmd = "INSERT INTO Registro " &
                            "(responsable, sector, provincia, " &
                            "cv, inicioactiv, inidocente, " &
                            "fechAlta) " &
                        "VALUES " &
                            "(" & quien.Codigo & ", 5" & ", " & ddlProvincias.SelectedValue & ", " &
                            "'" & txtCV.Text.Trim.ToUpper & "', Convert(datetime,'" & sFechaInicio & "'), Convert(datetime,'" & sFechaDocencia & "')," &
                            "getdate())  " &
                        "SET @nIdRegistro = SCOPE_IDENTITY()"

            mIdRegistro = New SqlParameter
            mIdRegistro.ParameterName = "@nIdRegistro"
            mIdRegistro.SqlDbType = SqlDbType.Int
            mIdRegistro.Direction = ParameterDirection.Output
            mIdRegistro.Value = -1
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.Parameters.Add(mIdRegistro)
            MyCommand.ExecuteNonQuery()
            nIdRegistro = mIdRegistro.Value
            MyCommand.Dispose()
            MyConnection.Dispose()

            'DELETE Especialidades
            sSQLCmd = "DELETE FROM Especialidades WHERE codigoRegistro = " & quien.Codigo ' Session("USER_ID")
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

            'INSERT Especialidades
            sSQLCmd = "INSERT INTO Especialidades (codigoRegistro, actividad, descripcion, fechAlta) " &
                            "SELECT  " & nIdRegistro & ", actividad, descripcion, getdate() " &
                                "FROM EspecialidadesTemp " &
                                "WHERE cuit = " & quien.Usuario ' Session("CUIT")
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

            'DELETE EspecialidadesTemp
            sSQLCmd = "DELETE FROM EspecialidadesTemp WHERE cuit = " & quien.Usuario ' Session("CUIT")
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

            Session.Add("CODIGO_REGISTRO", nIdRegistro)

        Catch ex As Exception
            GuardarDatos = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        Dim bGrabar As Boolean = False
        bGrabar = GrabarAdjuntos()
        If bGrabar = False Then
            GuardarDatos = False
            Return False
        End If

        GuardarDatos = True
    End Function

    Protected Function GrabarAdjuntos() As Integer
        Dim nIdRegistro As Integer = Session("CodRegistro")

        BorraAdjuntos()

        'Guardar CV
        Dim woperador As String = Session("CUIT")
        Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
        Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
        Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
        Dim fileSavePath As Object = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/CV")
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
                Return False
            End Try
        Else
            If Session("UploadFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("UploadImporta1"), FileUpload)
                FileName = UploadImporta1.FileName
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
                    Return False
                End Try
            End If
        End If

        'Guardar FOTO
        FileName = Path.GetFileName(UploadImportaf.PostedFile.FileName)
        Extension = Path.GetExtension(UploadImportaf.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/FOTO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If UploadImportaf.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                UploadImportaf.SaveAs(Filepath)
            Catch ex As Exception
                FailureText.Text = "No se pudo guardar imagen de la Foto"
                Return False
            End Try
        Else
            If Session("UploadFileNamef") IsNot Nothing Then
                Dim UploadImporta1f As FileUpload = CType(Session("UploadImporta1f"), FileUpload)
                FileName = UploadImporta1f.FileName
                If Len(RTrim(FileName)) = 0 Then
                    Dim Archivo As String = Session("UploadFileNamef")
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
                Dim Filepath As String = Session("UploadFileNamef")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    FailureText.Text = "No se pudo guardar imagen de la Foto"
                    Return False
                End Try
            End If
        End If
        Return True

    End Function


    Protected Function ActualizarDatos() As Integer
        Dim cn As New SqlClient.SqlConnection(SqlConex)
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim sFechaInicio As String
        Dim sFechaDocencia As String
        Try
            Dim wfecha As Date = CDate(TextDesde.Value)
            sFechaInicio = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            wfecha = CDate(TextDocencia.Value)
            sFechaDocencia = Year(wfecha) * 10000 + Month(wfecha) * 100 + Day(wfecha)
            Dim nCodigo As Integer = Session("CodRegistro")
            Dim nResponsable As Integer = 0
            cn.Close()
            cn.Open()
            Dim sql As String = "SELECT RESPONSABLE FROM REGISTRO WHERE codigo = " & nCodigo
            Dim Psql As New SqlClient.SqlCommand(sql, cn)
            Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
            While dr.Read()
                nResponsable = dr.GetInt32(0)
            End While
            dr.Close()
            cn.Close()
            If nResponsable <> 0 Then
                nCodigo = nResponsable
                Session("CODIGO") = nCodigo
            End If
            sSQLCmd = "UPDATE Registro " &
                         "SET provincia = " & ddlProvincias.SelectedItem.Value & ", " &
                         "cv = '" & txtCV.Text.Trim & "', " &
                         "inicioActiv = Convert(datetime,'" & sFechaInicio & "'), " &
                         "iniDocente = Convert(datetime,'" & sFechaDocencia & "'), " &
                          "fechModi = getdate() " &
                         "WHERE RESPONSABLE = " & nCodigo & " AND SECTOR=5"
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

            sSQLCmd = "DELETE FROM Especialidades WHERE codigoRegistro = " & Session("CODIGO")
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

            'INSERT Especialidades
            sSQLCmd = "INSERT INTO Especialidades (codigoRegistro, actividad, descripcion, fechAlta) " &
                            "SELECT  " & Session("CODIGO") & ", actividad, descripcion, getdate() " &
                                "FROM EspecialidadesTemp " &
                                "WHERE cuit = " & quien.Usuario
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.ExecuteNonQuery()
            MyCommand.Dispose()

            'DELETE EspecialidadesTemp
            sSQLCmd = "DELETE FROM EspecialidadesTemp WHERE cuit = " & Session("CUIT")
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

        Catch ex As Exception
            ActualizarDatos = False
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        Dim bGrabar As Boolean = False
        bGrabar = GrabarAdjuntos()
        If bGrabar = False Then
            ActualizarDatos = False
            Return False
        End If

        ActualizarDatos = True
    End Function

    Private Sub CargarDatos(ByVal nCodigo As Integer)
        'Const F_RESPONSABLE As Integer = 0
        Const F_SECTOR As Integer = 1
        Const F_PROVINCIA As Integer = 2
        Const F_CV As Integer = 3
        Const F_INICIO_ACTIVIDAD As Integer = 4
        Const F_INICIO_DOCENCIA As Integer = 5
        'Const F_FECHALTA As Integer = 6
        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader

        Try
            'Load Registro
            sSQLCmd = "SELECT responsable, sector, provincia, cv, inicioactiv, inidocente,fechAlta " &
                            "FROM Registro " &
                            "WHERE codigo = " & nCodigo.ToString

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()
            If MyReader.Read() Then
                Session.Add("SECTOR", MyReader.Item(F_SECTOR))
                Session.Add("PROVINCIA", MyReader.Item(F_PROVINCIA))
                'Inicializo las combos
                Inicializar()
                'Cargo Especialidades
                BorrarTemporal()
                CargarTemporal(nCodigo)

                ddlSectores.Text = MyReader.Item(F_SECTOR).ToString.Trim
                ' ddlProvincias.Text = MyReader.Item(F_PROVINCIA)
                txtCV.Text = MyReader.Item(F_CV).ToString.Trim
                TextDesde.Value = MyReader.Item(F_INICIO_ACTIVIDAD)
                TextDocencia.Value = MyReader.Item(F_INICIO_DOCENCIA)
            End If
            MyReader.Dispose()
            MyCommand.Dispose()
            'End of Load Registro

            MyConnection.Dispose()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnEspecialidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEspecialidad.Click
        Try
            If txtActividad.Text.Trim <> "" Then
                If txtEspecialidad.Text.Trim <> "" Then
                    If txtEspecialidad.Text.Length > 3000 Then
                        txtErrorActividad.Text = ""
                        txtErrorEspecialidad.Text = "Máximo 3000 caracteres"
                        Return
                    End If
                    If InsertEspecialidadTemp() Then
                        txtActividad.Text = ""
                        txtEspecialidad.Text = ""
                        txtErrorActividad.Text = ""
                        txtErrorEspecialidad.Text = ""
                        GridView1.DataBind()
                    End If
                Else
                    txtErrorActividad.Text = ""
                    txtErrorEspecialidad.Text = "Debe ingresar el programa y/o plan de trabajo"
                End If
            Else
                txtErrorActividad.Text = "Debe ingresar la especialidad"
                txtErrorEspecialidad.Text = ""
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Function InsertEspecialidadTemp() As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim mIdEspecialidad As SqlParameter
        Dim nIdEspecialidad As Integer

        Try
            'INSERT Especialidad
            sSQLCmd = "INSERT INTO EspecialidadesTemp (cuit, codigoRegistro, actividad, descripcion) VALUES (" & quien.Usuario & ", NULL, '" & txtActividad.Text.Trim.ToUpper & "', '" & txtEspecialidad.Text.Trim & "') SET @nIdEspecialidad = SCOPE_IDENTITY()"
            mIdEspecialidad = New SqlParameter
            mIdEspecialidad.ParameterName = "@nIdEspecialidad"
            mIdEspecialidad.SqlDbType = SqlDbType.Int
            mIdEspecialidad.Direction = ParameterDirection.Output
            mIdEspecialidad.Value = -1
            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()
            MyCommand = New SqlCommand()
            MyCommand.CommandText = sSQLCmd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection
            MyCommand.Parameters.Add(mIdEspecialidad)
            MyCommand.ExecuteNonQuery()
            nIdEspecialidad = mIdEspecialidad.Value

        Catch ex As Exception
            InsertEspecialidadTemp = False
            Dim mensaje As String = ex.Message
            'Response.Redirect("ErrorPage.aspx?errMessage=" & ex.Message & "&errSource=" & ex.Source)
        Finally
            MyCommand.Dispose()
            MyConnection.Dispose()
        End Try

        InsertEspecialidadTemp = True
    End Function

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Dim sAccion As String = Session("sAccion")
        If sAccion <> "M" Then
            Response.Redirect("menuFinal.aspx", False)
        Else
            Response.Redirect("RegistroLista.aspx", False)
        End If
    End Sub

    Protected Sub AceptoDJ_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles AceptoDJ.CheckedChanged
        tablaDatos.Visible = AceptoDJ.Checked
        'UserMsgBox(Me, "Todos los campos en los cuales debe adjuntar documentación, son de llenado OPTATIVO. En otro momento, cuando haya reunido dicha documentación, podrá realizar la actualización en su opción de Datos Personales.")
        CargAdjunto()
    End Sub

    Protected Sub BtnVisualiza_Click(sender As Object, e As EventArgs) Handles BtnVisualiza.Click
        FailureText.Text = ""
        If UploadImporta.HasFile Or Session("UploadImporta1") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(UploadImporta.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImporta.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If Session("UploadImporta1") Is Nothing Then
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

    Protected Sub BtnVisualizaf_Click(sender As Object, e As EventArgs) Handles BtnVisualizaf.Click
        FailureText.Text = ""
        If UploadImportaf.HasFile Or Session("UploadImporta1f") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(UploadImportaf.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(UploadImportaf.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If Session("UploadImporta1f") Is Nothing Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".JPG" And UCase(Extension) <> ".JPEG" Then
                    FailureText.Text = "No es una imagen .JPG o .JPEG"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    UploadImportaf.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("UploadImporta1f") = UploadImportaf
                Session("UploadFileNamef") = FilePath
            Else
                FilePath = Session("UploadFileNamef")
                Dim letra As String = Right(FilePath.Trim, 5)
                If Left(letra, 1) = "." Then
                    Extension = Right(FilePath.Trim, 5)
                Else
                    Extension = Right(FilePath.Trim, 4)
                End If
            End If
            If UCase(Extension) = ".JPG" Then

                Response.ContentType = "image/jpg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()

            Else
                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment;filename=" & FilePath)
                Response.TransmitFile(FilePath)
                Response.End()
            End If

        End If
    End Sub

    Private Sub CargAdjunto()
        Dim nCodigo As Integer = Session("CodRegistro")
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

        Dim sDocumento As String = ""
        Dim sPath As String = ""
        Dim fileSavePath As String = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/CV/")
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

        sDocumento = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/FOTO/")
        wlong = Len(fileSavePath)
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
            Session("UploadImporta1f") = UploadImporta
            Session("UploadFileNamef") = sPath
            Session("sDocumentof") = sDocumento
            LabelNombreUploadf.Text = sDocumento
            randomName = randomName + "2"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                FailureText.Text = "No se pudo guardar documento de CV"
                Return
            End Try
            Session("UploadFileNamef") = FilePathDest
        End If

    End Sub

    Private Sub BorraAdjuntos()
        Dim nCodigo As Integer = Session("CodRegistro")
        Dim sPath As String = ""
        Dim fileSavePath As String = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/CV/")
        Dim wlong As Integer = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                My.Computer.FileSystem.DeleteFile(sPath)
            Next
        Catch ex As Exception
        End Try

        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/FOTO/")
        wlong = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                My.Computer.FileSystem.DeleteFile(sPath)
            Next
        Catch ex As Exception
        End Try
    End Sub

End Class
