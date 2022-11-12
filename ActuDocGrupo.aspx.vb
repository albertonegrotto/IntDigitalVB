Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.Packaging
Public Class ActuDocGrupo
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim quien As usuario = CType(Session("usuario"), usuario)
            Session("USER_ID") = quien.Codigo
            Session("CUIL") = quien.Usuario
            Dim nCodigo As Integer = Request.QueryString("codigo")
            Session.Add("CodRegistro", nCodigo)
            Inicializa()
        Else
            GuardarAdjunto()
            MaintainScrollPositionOnPostBack = True
        End If
    End Sub

    Private Sub Inicializa()
        Dim nCodigo As Integer = Session("CodRegistro")
        Dim wdenominacion As String = ""
        Dim wcantidad As Integer = 0
        Dim wequipo As Integer = 0
        Dim sql As String = "select denominacion,cantespacios,EQUIPPROPIO from REGISTRO where CODIGO=" & nCodigo
        cn.Open()
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read
            wdenominacion = dr.GetString(0)
            Try
                wcantidad = dr.GetInt32(1)
            Catch ex As Exception
                wcantidad = 0
            End Try
            Try
                wequipo = dr.GetInt32(2)
            Catch ex As Exception
                wequipo = 0
            End Try
        End While
        dr.Close()
        cn.Close()
        LblDenominacion.InnerText = wdenominacion
        If wequipo = 0 Then
            DivTablaEquipamiento.Visible = False
        End If
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
        FileUploadEquipa.Visible = False
        BtnVisualizaEquipa.Visible = False
        Dim sDocumento As String = ""
        Dim sPath As String = ""
        Dim fileSavePath As String = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/EQUIPAMIENTO/")
        Dim wlong As Integer = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
                FileUploadEquipa.Visible = True
                BtnVisualizaEquipa.Visible = True
            Next
        Catch ex As Exception
            sDocumento = ""
        End Try
        If Len(RTrim(sDocumento)) > 0 Then
            Session("FileUploadEquipa1") = FileUploadEquipa
            Session("FileUploadEquipaFileName") = sPath
            Session("sDocumentoEquipa") = sDocumento
            LabelUploadEquipa.Text = sDocumento
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                FailureText.Text = "No se pudo guardar documento de equipamiento"
                Return
            End Try
            Session("FileUploadEquipaFileName") = FilePathDest
        End If

        FileUploadEquipaSub.Visible = False
        BtnVisualizaEquipaSub.Visible = False
        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/EQUIPADQUIRIDO/")
        wlong = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
                FileUploadEquipaSub.Visible = True
                BtnVisualizaEquipaSub.Visible = True
            Next
        Catch ex As Exception
            sDocumento = ""
        End Try
        If Len(RTrim(sDocumento)) > 0 Then
            Session("FileUploadEquipaSub1") = FileUploadEquipaSub
            Session("FileUploadEquipaSubFileName") = sPath
            Session("sDocumentoEquipaSub") = sDocumento
            LabelUploadEquipaSub.Text = sDocumento
            randomName = randomName + "3"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                FailureText.Text = "No se pudo guardar documento de equipamiento adquirido"
                Return
            End Try
            Session("FileUploadEquipaSubFileName") = FilePathDest
        End If

        FileUploadTrayectoria.Visible = False
        BtnVisualizaTrayectoria.Visible = False
        sDocumento = ""
        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/TRAYECTORIA/")
        wlong = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                Dim wlong2 As Integer = Len(sPath)
                sDocumento = Mid(sPath, wlong + 1, wlong2 - wlong)
                FileUploadTrayectoria.Visible = True
                BtnVisualizaTrayectoria.Visible = True
            Next
        Catch ex As Exception
            sDocumento = ""
        End Try
        If Len(RTrim(sDocumento)) > 0 Then
            Session("FileUploadTrayectoria1") = FileUploadTrayectoria
            Session("FileUploadTrayectoriaFileName") = sPath
            Session("sDocumentoTrayectoria") = sDocumento
            LabelUploadTrayectoria.Text = sDocumento
            randomName = randomName + "4"
            If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
            End If
            Dim FilePathDest As String = (Server.MapPath("~/Temp/" & randomName & "/")) + sDocumento
            Try
                File.Copy(sPath, FilePathDest)
            Catch ex As Exception
                FailureText.Text = "No se pudo guardar documento de equipamiento adquirido"
                Return
            End Try
            Session("FileUploadTrayectoriaFileName") = FilePathDest
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
        If Session("FileUploadEquipa1") Is Nothing Or FileUploadEquipa.HasFile Then
            If FileUploadEquipa.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadEquipa.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".DOC" Or UCase(Extension) = ".DOCX" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadEquipa.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadEquipa1") = FileUploadEquipa
                    Session("FileUploadEquipaFileName") = FilePath
                    LabelUploadEquipa.Text = FileUploadEquipa.FileName
                    Session("sDocumentoEquipa") = FileUploadEquipa.FileName
                End If
            End If
        Else
            If Session("FileUploadEquipa1") IsNot Nothing Then
                Dim FileUploadEquipa1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                If FileUploadEquipa1.FileName.ToString = "" Then
                    LabelUploadEquipa.Text = Session("sDocumentoEquipa")
                Else
                    LabelUploadEquipa.Text = FileUploadEquipa1.FileName
                End If
            End If
        End If

        If Session("FileUploadTrayectoria1") Is Nothing Or FileUploadTrayectoria.HasFile Then
            If FileUploadTrayectoria.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUploadTrayectoria.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUploadTrayectoria.PostedFile.FileName)
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString
                If UCase(Extension) = ".PDF" Or UCase(Extension) = ".DOC" Or UCase(Extension) = ".DOCX" Then
                    Dim randomName As String = RTrim(woperador) + wdir
                    If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                        Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                    End If
                    Dim FilePath As String = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                    Try
                        FileUploadTrayectoria.SaveAs(FilePath)
                    Catch ex As Exception
                    End Try
                    Session("FileUploadTrayectoria1") = FileUploadTrayectoria
                    Session("FileUploadTrayectoriaFileName") = FilePath
                    LabelUploadTrayectoria.Text = FileUploadTrayectoria.FileName
                    Session("sDocumentoTrayectoria") = FileUploadTrayectoria.FileName
                End If
            End If
        Else
            If Session("FileUploadTrayectoria") IsNot Nothing Then
                Dim FileUploadTrayectoria1 As FileUpload = CType(Session("FileUploadTrayectoria1"), FileUpload)
                If FileUploadTrayectoria.FileName.ToString = "" Then
                    LabelUploadTrayectoria.Text = Session("sDocumentoEquipaSub")
                Else
                    LabelUploadTrayectoria.Text = FileUploadTrayectoria.FileName
                End If
            End If
        End If

    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("AdjuntosLista.aspx")
    End Sub

    Protected Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click
        'Guardar Lista de Equipamiento
        Dim nIdRegistro As Integer = Session("CodRegistro")
        Dim woperador As String = Session("CUIT")
        Dim Extension As String = ""
        Dim letra As String = ""
        Dim DocuControl As String = Session("sDocumentoEquipa")

        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                txtErrorEquipamiento.Text = "Equipamiento No es un documento Adobe .PDF o Word .DOC .DOCX"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadEquipa.HasFile Then
            Dim sizeInBytes As Long = FileUploadEquipa.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Equipamiento tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadEquipaFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    FailureText.Text = "Equipamiento tiene un tamaño mayor a 10 Mb"
                    Return
                End If
            End If
        End If

        DocuControl = Session("sDocumentoEquipaSub")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                txtErrorEquipamiento.Text = "Equipamiento Adquirido No es un documento Adobe .PDF o Word .DOC .DOCX"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadEquipaSub.HasFile Then
            Dim sizeInBytes As Long = FileUploadEquipaSub.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Equipamiento Adquirido tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadEquipaSubFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipaSub1"), FileUpload)
                Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    FailureText.Text = "Equipamiento Adquirido tiene un tamaño mayor a 10 Mb"
                    Return
                End If
            End If
        End If

        DocuControl = Session("sDocumentoTrayectoria")
        Try
            letra = Right(DocuControl.Trim, 5)
            If Left(letra, 1) = "." Then
                Extension = Right(DocuControl.Trim, 5)
            Else
                Extension = Right(DocuControl.Trim, 4)
            End If
            If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                txtErrorEquipamiento.Text = "Trayectoria No es un documento Adobe .PDF o Word .DOC .DOCX"
                Return
            End If
        Catch ex As Exception
        End Try
        If FileUploadTrayectoria.HasFile Then
            Dim sizeInBytes As Long = FileUploadTrayectoria.PostedFile.ContentLength
            If sizeInBytes / 1000000 > 10 Then
                FailureText.Text = "Trayectoria tiene un tamaño mayor a 10 Mb"
                Return
            End If
        Else
            If Session("FileUploadTrayectoria") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadTrayectoria"), FileUpload)
                Dim sizeInBytes As Long = UploadImporta1.PostedFile.ContentLength
                If sizeInBytes / 1000000 > 10 Then
                    FailureText.Text = "Trayectoria tiene un tamaño mayor a 10 Mb"
                    Return
                End If
            End If
        End If
        BorraAdjuntos()

        'Guardar Lista de Equipamiento
        Dim FileName As String = Path.GetFileName(FileUploadEquipa.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
        Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
        Dim fileSavePath As Object = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/EQUIPAMIENTO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadEquipa.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadEquipa.SaveAs(Filepath)
            Catch ex As Exception
                txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento"
                Return
            End Try
        Else
            If Session("FileUploadEquipaFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipa1"), FileUpload)
                FileName = Session("sDocumentoEquipa")
                Dim Filepath As String = Session("FileUploadEquipaFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento"
                    Return
                End Try
            End If
        End If

        'Guardar Lista de Equipamiento Adquirido
        FileName = Path.GetFileName(FileUploadEquipaSub.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadEquipaSub.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/EQUIPADQUIRIDO")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadEquipaSub.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadEquipaSub.SaveAs(Filepath)
            Catch ex As Exception
                txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento Adquirido"
                Return
            End Try
        Else
            If Session("FileUploadEquipaSubFileName") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadEquipaSub1"), FileUpload)
                FileName = Session("sDocumentoEquipaSub")
                Dim Filepath As String = Session("FileUploadEquipaSubFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    txtErrorEquipamiento.Text = "No se pudo guardar Lista de equipamiento Adquirido"
                    Return
                End Try
            End If
        End If

        'Guardar Trayectoria
        FileName = Path.GetFileName(FileUploadTrayectoria.PostedFile.FileName)
        Extension = Path.GetExtension(FileUploadTrayectoria.PostedFile.FileName)
        FolderPath = ConfigurationManager.AppSettings("FolderPath")
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nIdRegistro & "/TRAYECTORIA")
        Try
            MkDir(fileSavePath)
        Catch ex As Exception
        End Try
        If FileUploadTrayectoria.HasFile Then
            Dim Filepath As String = fileSavePath + "\" + FileName
            Try
                FileUploadTrayectoria.SaveAs(Filepath)
            Catch ex As Exception
                txtErrorTrayectoria.Text = "No se pudo guardar documento de Trayectoria"
                Return
            End Try
        Else
            If Session("FileUploadTrayectoria") IsNot Nothing Then
                Dim UploadImporta1 As FileUpload = CType(Session("FileUploadTrayectoria"), FileUpload)
                FileName = Session("sDocumentoTrayectoria")
                Dim Filepath As String = Session("FileUploadTrayectoriaFileName")
                Dim FilepathDest As String = fileSavePath + "\" + FileName
                Try
                    File.Copy(Filepath, FilepathDest)
                Catch ex As Exception
                    txtErrorTrayectoria.Text = "No se pudo guardar documento de Trayectoria"
                    Return
                End Try
            End If
        End If

        Response.Clear()
        Response.Redirect("ConfirmaDocumento.aspx", False)

    End Sub

    Private Sub BorraAdjuntos()
        Dim nCodigo As Integer = Session("CodRegistro")
        Dim sPath As String = ""
        Dim fileSavePath As String = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/EQUIPAMIENTO/")
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
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/EQUIPADQUIRIDO/")
        wlong = Len(fileSavePath)
        Try
            Dim files() As String = IO.Directory.GetFiles(fileSavePath)
            For Each file As String In files
                sPath = file.ToString
                My.Computer.FileSystem.DeleteFile(sPath)
            Next
        Catch ex As Exception
        End Try

        sPath = ""
        fileSavePath = Server.MapPath("~/Documentos/REGISTRO/" & nCodigo & "/TRAYECTORIA/")
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

    Protected Sub BtnVisualizaEquipa_Click(sender As Object, e As EventArgs) Handles BtnVisualizaEquipa.Click
        txtErrorEquipamiento.Text = ""
        If FileUploadEquipa.HasFile Or Session("FileUploadEquipa1") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadEquipa.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadEquipa.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadEquipa.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    txtErrorEquipamiento.Text = "No es un documento Adobe .PDF o Word .DOC .DOCX"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadEquipa.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadEquipa1") = FileUploadEquipa
                Session("FileUploadEquipaFileName") = FilePath
            Else
                FilePath = Session("FileUploadEquipaFileName")
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

    Protected Sub BtnVisualizaEquipaSub_Click(sender As Object, e As EventArgs) Handles BtnVisualizaEquipaSub.Click
        txtErrorEquipamiento.Text = ""
        If FileUploadEquipaSub.HasFile Or Session("FileUploadEquipaSub1") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadEquipaSub.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadEquipaSub.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadEquipaSub.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    txtErrorEquipamiento.Text = "No es un documento Adobe .PDF o Word .DOC .DOCX"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadEquipaSub.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadEquipaSub1") = FileUploadEquipaSub
                Session("FileUploadEquipaSubFileName") = FilePath
            Else
                FilePath = Session("FileUploadEquipaSubFileName")
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

    Protected Sub BtnVisualizaTrayectoria_Click(sender As Object, e As EventArgs) Handles BtnVisualizaTrayectoria.Click
        txtErrorEquipamiento.Text = ""
        If FileUploadTrayectoria.HasFile Or Session("FileUploadTrayectoria1") IsNot Nothing Then
            Dim woperador As String = Session("CUIT")
            Dim FileName As String = Path.GetFileName(FileUploadTrayectoria.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUploadTrayectoria.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
            Dim FilePath As String = ""
            If FileUploadTrayectoria.HasFile Then
                Dim wfecha As Date = DateTime.Now.ToString
                Dim wdia As Integer = wfecha.Day
                Dim wmes As Integer = wfecha.Month
                Dim wano As Integer = wfecha.Year
                Dim whora As Integer = wfecha.Hour
                Dim wminu As Integer = wfecha.Minute
                Dim wsegu As Integer = wfecha.Second
                Dim wdir As String = wano.ToString + wmes.ToString + wdia.ToString + whora.ToString + wminu.ToString + wsegu.ToString

                If UCase(Extension) <> ".PDF" And UCase(Extension) <> ".DOC" And UCase(Extension) <> ".DOCX" Then
                    txtErrorTrayectoria.Text = "No es un documento Adobe .PDF o Word .DOC .DOCX"
                    Return
                End If
                Dim randomName As String = RTrim(woperador) + wdir
                If Not Directory.Exists(Server.MapPath("~/Temp/" & randomName & "/")) Then
                    Directory.CreateDirectory(Server.MapPath("~/Temp/" & randomName & "/"))
                End If
                FilePath = (Server.MapPath("~/Temp/" & randomName & "/")) + FileName
                Try
                    FileUploadTrayectoria.SaveAs(FilePath)
                Catch ex As Exception
                End Try
                Session("FileUploadTrayectoria1") = FileUploadTrayectoria
                Session("FileUploadTrayectoriaFileName") = FilePath
            Else
                FilePath = Session("FileUploadTrayectoriaFileName")
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