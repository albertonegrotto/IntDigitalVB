Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.IO.Packaging
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing

Public Class ActualizaCVPersona
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim quien As usuario = CType(Session("usuario"), usuario)
            Session("USER_ID") = quien.Codigo
            Session("CUIL") = quien.Usuario
            Inicializa()
        Else
            GuardarAdjunto()
            MaintainScrollPositionOnPostBack = True
        End If
    End Sub

    Private Sub Inicializa()
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
                LabelNombreUpload.Text = Session("sDocumento")
            End If
        End If
    End Sub

    Protected Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click
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
        Response.Clear()
        Response.Redirect("ConfirmaDocumento.aspx", False)
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

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("AdjuntosLista.aspx")
    End Sub

End Class