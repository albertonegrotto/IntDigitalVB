Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Public Class AltaBeneficiario
    Inherits System.Web.UI.Page
    Dim cn As New SqlClient.SqlConnection(SqlConSig)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If User.Identity.IsAuthenticated Then
                Dim quien As usuario = CType(Session("usuario"), usuario)
                Dim wcuit As Decimal = quien.Usuario
                Dim wente As Integer = 0
                Dim wdenominacion As String = ""
                Dim wactividad As Integer = 0
                Dim walta_saf As String = ""
                Dim wad_sb As String = ""
                Dim wdomicilio As String = ""
                Dim wdesprovi As String = ""
                Dim wlocalidad As String = ""
                Dim wcopost As String = ""
                Dim winactivo As String = ""
                Dim wfechbaja As String = ""
                cn.Open()
                Dim sql As String = "select ente,denominacion,actividad,convert(char(10),alta_saf,103) as alta_saf,ad_sb,domicilio,desprovi,localidad,copost,inactivo,convert(char(10),fechbaja,103) as fechbaja from entes where cuit=" & wcuit
                Dim Psql As New SqlClient.SqlCommand(sql, cn)
                Dim dr As SqlClient.SqlDataReader = Psql.ExecuteReader
                While dr.Read
                    Try
                        wente = dr.GetInt32(0)
                    Catch ex As Exception
                        wente = 0
                    End Try
                    Try
                        wdenominacion = dr.GetString(1)
                    Catch ex As Exception
                        wdenominacion = ""
                    End Try
                    Try
                        wactividad = dr.GetInt32(2)
                    Catch ex As Exception
                        wactividad = 0
                    End Try
                    Try
                        walta_saf = dr.GetString(3)
                    Catch ex As Exception
                        walta_saf = ""
                    End Try
                    Try
                        wad_sb = dr.GetString(4)
                    Catch ex As Exception
                        wad_sb = ""
                    End Try
                    Try
                        wdomicilio = dr.GetString(5)
                    Catch ex As Exception
                        wdomicilio = ""
                    End Try
                    Try
                        wdesprovi = dr.GetString(6)
                    Catch ex As Exception
                        wdesprovi = ""
                    End Try
                    Try
                        wlocalidad = dr.GetString(7)
                    Catch ex As Exception
                        wlocalidad = ""
                    End Try
                    Try
                        wcopost = dr.GetString(8)
                    Catch ex As Exception
                        wcopost = ""
                    End Try
                    Try
                        winactivo = dr.GetString(9)
                    Catch ex As Exception
                        winactivo = ""
                    End Try
                    Try
                        wfechbaja = dr.GetString(10)
                    Catch ex As Exception
                        wfechbaja = ""
                    End Try
                    TextBoxente.Text = wente
                    TextBoxdenominacion.Text = wdenominacion
                    TextBoxActividad.Text = wactividad
                    TextBoxAlta.Text = walta_saf
                    TextBoxad_sb.Text = wad_sb
                    TextBoxdomicilio.Text = wdomicilio
                    TextBoxDesprovi.Text = wdesprovi
                    TextBoxlocalidad.Text = wlocalidad
                    TextBoxcopost.Text = wcopost
                    TextBoxInactivo.Text = winactivo
                    TextBoxBaja.Text = wfechbaja
                End While
                If wente <> 0 Then
                    tablaDatos.Visible = True
                Else
                    tablaDatos2.Visible = True
                End If
            Else
                Response.Clear()
                Response.Redirect("http://www.inteatro.gob.ar", False)
            End If
        End If
    End Sub

End Class