Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Public Module RegistroModulo

    Public Function SetSQLGridviewPalabras() As String
        Dim sSqlCmd As String

        sSqlCmd = "SELECT codigo AS codigo, " & _
                            "LTrim(RTrim(apellido)) + ' ' + LTrim(RTrim(nombre)) + LTrim(RTrim(denominacion)) AS apellidoNombre, " & _
                            "CASE " & _
                                "WHEN (prefipart + telepart) > 0 AND (preficelu + celupart) > 0 " & _
                                    "THEN '(' + convert(varchar(10),prefipart) + ') ' + convert(varchar(10),telepart) + ' / ' + " & _
                                        "'(' + convert(varchar(10),preficelu) + ') ' + convert(varchar(10),celupart) " & _
                                "WHEN (prefipart + telepart) > 0 " & _
                                    "THEN '(' + convert(varchar(10),prefipart) + ') ' + convert(varchar(10),telepart) " & _
                                "WHEN (preficelu + celupart) > 0 " & _
                                    "THEN '(' + convert(varchar(10),preficelu) + ') ' + convert(varchar(10),celupart) " & _
                            "END AS telefonos, " & _
                            "email AS email " & _
                        "FROM RegisDig " & _
                        "WHERE codigo = @codigo"

        Return sSqlCmd

    End Function

    Public Function GetIdSector(ByVal sCodigoRegistro As String) As String
        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim nSector As Integer

        Try

            sSQLCmd = "SELECT sector " & _
                            "FROM Registro " & _
                            "WHERE codigo = " & sCodigoRegistro

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()

            If MyReader.Read() Then

                nSector = MyReader.Item(0)

            End If

            MyReader.Dispose()
            MyCommand.Dispose()

            MyConnection.Dispose()

            Return nSector

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function GetTipoRegistro(ByVal nTipoRegistro As Integer) As String

        Dim s As String

        Select Case nTipoRegistro
            Case Is = 1
                s = "Sala de Teatro Independiente"
            Case Is = 2
                s = "Grupo de Teatro Independiente"
            Case Is = 3
                s = "Grupo Comunitario"
            Case Is = 4
                s = "Grupo Vocacional"
            Case Is = 5
                s = "Asistente Técnico"
            Case Is = 6
                s = "Espectáculo Concertado"
            Case Is = 7
                s = "Publicación"
            Case Is = 8
                s = "ONG"
            Case Is = 9
                s = "Evento"
            Case Else
                s = ""
        End Select

        Return s

    End Function

    Public Function GetPalabras(ByVal sCodigo As String) As String
        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim s As String

        Try

            s = ""

            'Load Palabras
            sSQLCmd = "SELECT * " & _
                            "FROM RegistroPalabras " & _
                            "WHERE codigoRegistro = " & sCodigo

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()

            If MyReader.Read() Then

                'If Not MyReader.Item(F_CODIGO_REGISTRO).Equals(DBNull.Value) Then
                '    s += IIf(MyReader.Item(F_CODIGO_REGISTRO) = 1, "CODIGO REGISTRO: Si", "CODIGO REGISTRO: No") & "<BR />"
                'End If
                If Not MyReader.Item(F_APELLIDO_Y_NOMBRE_DEL_RESPONSABLE).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_APELLIDO_Y_NOMBRE_DEL_RESPONSABLE) = 1, "APELLIDO_Y_NOMBRE_DEL_RESPONSABLE: Si", "APELLIDO_Y_NOMBRE_DEL_RESPONSABLE: No") & "<BR />"
                End If
                If Not MyReader.Item(F_CANTIDAD_DE_INTEGRANTES).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_CANTIDAD_DE_INTEGRANTES) = 1, "CANTIDAD_DE_INTEGRANTES: Si", "CANTIDAD_DE_INTEGRANTES: No") & "<BR />"
                End If
                If Not MyReader.Item(F_CANTIDAD_DE_LOCALIDADES).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_CANTIDAD_DE_LOCALIDADES) = 1, "CANTIDAD_DE_LOCALIDADES: Si", "CANTIDAD_DE_LOCALIDADES: No") & "<BR />"
                End If
                If Not MyReader.Item(F_CARACTERISTICAS_DEL_ESPACIO_ESCENICO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_CARACTERISTICAS_DEL_ESPACIO_ESCENICO) = 1, "CARACTERISTICAS_DEL_ESPACIO_ESCENICO: Si", "CARACTERISTICAS_DEL_ESPACIO_ESCENICO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_CURRICULUM).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_CURRICULUM) = 1, "CURRICULUM: Si", "CURRICULUM: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL) = 1, "DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL: Si", "DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL_DEL_GRUPO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL_DEL_GRUPO) = 1, "DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL_DEL_GRUPO: Si", "DESCRIPCION_DEL_EQUIPAMIENTO_TECNICO_ACTUAL_DEL_GRUPO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRÓNICO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRÓNICO) = 1, "DIRECCION_DE_CORREO_ELECTRÓNICO: Si", "DIRECCION_DE_CORREO_ELECTRÓNICO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_ONG).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_ONG) = 1, "DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_ONG: Si", "DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_ONG: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_PUBLICACION).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_PUBLICACION) = 1, "DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_PUBLICACION: Si", "DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_PUBLICACION: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_SALA).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_SALA) = 1, "DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_SALA: Si", "DIRECCION_DE_CORREO_ELECTRONICO_DE_LA_SALA: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_ESPECTACULO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_ESPECTACULO) = 1, "DIRECCION_DE_CORREO_ELECTRONICO_DEL_ESPECTACULO: Si", "DIRECCION_DE_CORREO_ELECTRONICO_DEL_ESPECTACULO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_EVENTO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_EVENTO) = 1, "DIRECCION_DE_CORREO_ELECTRONICO_DEL_EVENTO: Si", "DIRECCION_DE_CORREO_ELECTRONICO_DEL_EVENTO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_GRUPO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_GRUPO) = 1, "DIRECCION_DE_CORREO_ELECTRONICO_DEL_GRUPO: Si", "DIRECCION_DE_CORREO_ELECTRONICO_DEL_GRUPO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE) = 1, "DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE: Si", "DIRECCION_DE_CORREO_ELECTRONICO_DEL_RESPONSABLE: No") & "<BR />"
                End If
                If Not MyReader.Item(F_DOMICILIO_DE_LA_SALA).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_DOMICILIO_DE_LA_SALA) = 1, "DOMICILIO_DE_LA_SALA: Si", "DOMICILIO_DE_LA_SALA: No") & "<BR />"
                End If
                If Not MyReader.Item(F_PAGINA_WEB_DE_LA_ONG).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_PAGINA_WEB_DE_LA_ONG) = 1, "PAGINA_WEB_DE_LA_ONG: Si", "PAGINA_WEB_DE_LA_ONG: No") & "<BR />"
                End If
                If Not MyReader.Item(F_PAGINA_WEB_DE_LA_PUBLICACION).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_PAGINA_WEB_DE_LA_PUBLICACION) = 1, "PAGINA_WEB_DE_LA_PUBLICACION: Si", "PAGINA_WEB_DE_LA_PUBLICACION: No") & "<BR />"
                End If
                If Not MyReader.Item(F_PAGINA_WEB_DE_LA_SALA).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_PAGINA_WEB_DE_LA_SALA) = 1, "PAGINA_WEB_DE_LA_SALA: Si", "PAGINA_WEB_DE_LA_SALA: No") & "<BR />"
                End If
                If Not MyReader.Item(F_PAGINA_WEB_DEL_ESPECTACULO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_PAGINA_WEB_DEL_ESPECTACULO) = 1, "PAGINA_WEB_DEL_ESPECTACULO: Si", "PAGINA_WEB_DEL_ESPECTACULO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_PAGINA_WEB_DEL_EVENTO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_PAGINA_WEB_DEL_EVENTO) = 1, "PAGINA_WEB_DEL_EVENTO: Si", "PAGINA_WEB_DEL_EVENTO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_PAGINA_WEB_DEL_GRUPO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_PAGINA_WEB_DEL_GRUPO) = 1, "PAGINA_WEB_DEL_GRUPO: Si", "PAGINA_WEB_DEL_GRUPO: No") & "<BR />"
                End If
                If Not MyReader.Item(F_TELEFONO_DE_LA_ONG).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_TELEFONO_DE_LA_ONG) = 1, "TELEFONO_DE_LA_ONG: Si", "TELEFONO_DE_LA_ONG: No") & "<BR />"
                End If
                If Not MyReader.Item(F_TELEFONO_DE_LA_REDACCION).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_TELEFONO_DE_LA_REDACCION) = 1, "TELEFONO_DE_LA_REDACCION: Si", "TELEFONO_DE_LA_REDACCION: No") & "<BR />"
                End If
                If Not MyReader.Item(F_TELEFONO_DE_LA_SALA).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_TELEFONO_DE_LA_SALA) = 1, "TELEFONO_DE_LA_SALA: Si", "TELEFONO_DE_LA_SALA: No") & "<BR />"
                End If
                If Not MyReader.Item(F_TELEFONOS).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_TELEFONOS) = 1, "TELEFONOS: Si", "TELEFONOS: No") & "<BR />"
                End If
                If Not MyReader.Item(F_TELEFONOS_DEL_RESPONSABLE).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_TELEFONOS_DEL_RESPONSABLE) = 1, "TELEFONOS_DEL_RESPONSABLE: Si", "TELEFONOS_DEL_RESPONSABLE: No") & "<BR />"
                End If
                If Not MyReader.Item(F_TIPO_DE_EVENTO).Equals(DBNull.Value) Then
                    s += IIf(MyReader.Item(F_TIPO_DE_EVENTO) = 1, "TIPO_DE_EVENTO: Si", "TIPO_DE_EVENTO: No") & "<BR />"
                End If

            End If

            MyReader.Dispose()
            MyCommand.Dispose()
            'End of Load Palabras

            MyConnection.Dispose()

            Return s

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function GetIntegrantes(ByVal sCodigoRegistro As String, _
                                    Optional ByVal bAsHTML As Boolean = False) As String

        Const F_CUIL As Integer = 0
        Const F_NOMBRE As Integer = 1

        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim s As String

        Try

            s = ""

            'Load Integrantes
            sSQLCmd = "SELECT I.CUIL AS CUIL, " &
                                "LTrim(RTrim(R.nombre)) + ' ' + LTrim(RTrim(R.apellido)) + ' ' + LTrim(RTrim(R.denominacion)) AS nombre " &
                            "FROM Integrantes I, " &
                                "RegisDig R " &
                            "WHERE I.CUIL = R.CUIL AND I.fechaBaja is null AND " &
                                "I.codigoRegistro =  " & sCodigoRegistro

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()

            Do While MyReader.Read()

                s += "CUIL:" & MyReader.Item(F_CUIL) & " / " & "Nombre o Denominación:" & MyReader.Item(F_NOMBRE)
                If bAsHTML Then s += "<BR />"

            Loop

            MyReader.Dispose()
            MyCommand.Dispose()
            'End of Load Integrantes

            MyConnection.Dispose()

            Return s

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function ExisteIntegrante(ByVal sCodigoRegistro As String, _
                                     ByVal sCUITIntegrante As String) As Boolean
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim sSQLCmd As String
        Dim nCantidad As Integer

        'Chequeo que esté dado de alta
        sSQLCmd = "SELECT count(*) AS cantidad " &
                        "FROM Integrantes " &
                        "WHERE codigoRegistro = " & sCodigoRegistro & " AND fechaBaja is null AND CUIL = '" & sCUITIntegrante & "'"

        MyConnection = New SqlConnection()
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString

        MyCommand = New SqlCommand()
        MyCommand.CommandText = sSQLCmd
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        nCantidad = Convert.ToInt32(MyCommand.ExecuteScalar())

        If nCantidad <> 0 Then
            Return True
        Else
            Return False
        End If

        MyCommand.Dispose()
        MyConnection.Dispose()

    End Function

    Public Function GetNombreRegistro(ByVal sIdRegistro As String) As String

        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim s As String

        Try

            s = ""
            sSQLCmd = "SELECT LTrim(RTrim(R.denominacion)) AS denominacion " & _
                            "FROM Registro R " & _
                            "WHERE R.codigo =  " & sIdRegistro

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()

            Do While MyReader.Read()

                s = MyReader.Item(0)

            Loop

            MyReader.Dispose()
            MyCommand.Dispose()
            MyConnection.Dispose()

            Return s

        Catch ex As Exception
            Return ""
        End Try

    End Function


    Public Function GetNombreRegisDig(ByVal sCUIL As String) As String

        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim s As String

        Try

            s = ""
            sSQLCmd = "SELECT LTrim(RTrim(R.nombre)) + ' ' + LTrim(RTrim(R.apellido)) + ' ' + LTrim(RTrim(R.denominacion)) AS nombre " & _
                            "FROM RegisDig R " & _
                            "WHERE R.cuil =  " & sCUIL

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()

            Do While MyReader.Read()

                s = MyReader.Item(0)

            Loop

            MyReader.Dispose()
            MyCommand.Dispose()
            MyConnection.Dispose()

            Return s

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function GetMail(ByVal sCUIL As String) As String

        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim s As String

        Try

            s = ""
            sSQLCmd = "SELECT R.email AS email " & _
                            "FROM RegisDig R " & _
                            "WHERE R.cuil =  " & sCUIL

            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()

            Do While MyReader.Read()

                s = MyReader.Item(0)

            Loop

            MyReader.Dispose()
            MyCommand.Dispose()
            MyConnection.Dispose()

            Return s

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function GetMailProvincia(ByVal sCUIL As String) As String

        Dim sSQLCmd As String
        Dim MyConnection As SqlConnection
        Dim MyCommand As SqlCommand
        Dim MyReader As SqlDataReader
        Dim s As String

        Try

            s = ""
            sSQLCmd = "SELECT P.mail AS mailRepresentante " & _
                        "FROM RegisDig R, " & _
                            "ProvinciasMail P " & _
                        "WHERE R.provincia = P.idProvincia AND " & _
                            "R.cuil = '" & sCUIL & "'"


            MyConnection = New SqlConnection()
            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("INTeatroDig").ConnectionString
            MyConnection.Open()

            MyCommand = New SqlCommand(sSQLCmd, MyConnection)
            MyReader = MyCommand.ExecuteReader()

            Do While MyReader.Read()

                s = MyReader.Item(0)

            Loop

            MyReader.Dispose()
            MyCommand.Dispose()
            MyConnection.Dispose()

            Return s

        Catch ex As Exception
            Return ""
        End Try

    End Function


End Module