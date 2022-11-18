Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Public Class CambiodeMail
    Inherits System.Web.UI.Page
    Dim cn As SqlClient.SqlConnection = New SqlClient.SqlConnection(SqlConex)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            inicializa()
            TextBoxNuevoMail.Focus()
        End If
    End Sub

    Private Sub inicializa()
        Dim wemail As String = ""
        Dim quien As usuario = CType(Session("usuario"), usuario)
        Dim Cuil As Decimal = quien.Usuario
        cn.Open()
        Dim sql As String = "select email From REGISDIG where CUIL=" & Cuil
        Dim Psql As New SqlClient.SqlCommand(sql, cn)
        Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
        While dr.Read()
            Try
                wemail = dr.GetString(0)
            Catch ex As Exception
                wemail = ""
            End Try
        End While
        dr.Close()
        cn.Close()
        txtMailActual.Text = wemail
    End Sub

    Protected Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles BtnEnviar.Click
        FailureText.Text = ""
        Dim quien As usuario = CType(Session("usuario"), usuario)
        Dim Cuil As Decimal = quien.Usuario
        Dim wnuevo As String = RTrim(TextBoxNuevoMail.Text)
        If Len(RTrim(wnuevo)) = 0 Then
            FailureText.Text = "Ingrese nuevo Mail"
        End If
        Dim wconfirma As String = RTrim(TextBoxConfirma.Text)
        If Len(RTrim(wconfirma)) = 0 Then
            FailureText.Text = "Ingrese Confirmación"
        End If
        If wnuevo <> wconfirma Then
            FailureText.Text = "No coincide mail con confirmación"
            Return
        End If
        Dim arr As Integer = TextBoxNuevoMail.Text.Trim.IndexOf("@")
        If arr <= 0 And Len(TextBoxNuevoMail.Text.Trim) > 0 Then
            FailureText.Text = " Cuenta de Correo Electrónica errónea"
            Return
        End If
        cn.Open()
        Dim sql As String = "update REGISDIG set email='" & wnuevo & "' where cuil=" & Cuil
        Dim Cmd As New SqlClient.SqlCommand(sql, cn)
        Try
            Cmd.ExecuteNonQuery()
            FailureText.ForeColor = Drawing.Color.Blue
            FailureText.Font.Bold = True
            FailureText.Font.Size = 12
            FailureText.Text = "Mail modificado con éxito"
        Catch ex As Exception
            FailureText.Text = "Error al actualizar mail"
        End Try
        cn.Close()
        TextBoxNuevoMail.Enabled = False
        TextBoxConfirma.Enabled = False
        BtnEnviar.Visible = False
    End Sub
End Class