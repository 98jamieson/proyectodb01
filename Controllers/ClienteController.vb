Imports System.Web.Mvc
Imports System.Configuration.ConfigurationManager
Imports System.Data.SqlClient
Imports System.Data
Imports Oracle.ManagedDataAccess.Client

Namespace Controllers
    Public Class ClienteController
        Inherits Controller
        <HttpGet>
        Function Index() As ActionResult

            Dim cn As String = ConfigurationManager.ConnectionStrings("proyectdb").ConnectionString
            Dim connection As New OracleConnection(cn)
            connection.Open()

            If (connection.State = ConnectionState.Open) Then
                Dim connectivity = True
            End If

            Dim cmd As New OracleCommand("proc_get_Clientes", connection)
            cmd.CommandType = CommandType.StoredProcedure

            Dim salida As New OracleParameter("resultado", OracleDbType.RefCursor)
            salida.Direction = ParameterDirection.Output

            cmd.Parameters.Add(salida)

            Dim da As New OracleDataAdapter(cmd)
            Dim tabla As New DataTable()
            da.Fill(tabla)
            Dim clientes As New List(Of Cliente)
            Dim mostrartabla As DataTable = tabla
            If mostrartabla.Rows.Count > 0 Then
                For Each row As DataRow In mostrartabla.Rows
                    Dim cli As New Cliente()
                    cli.cli_num_doc = row("CLI_NUMERO_DOCUMENTO")
                    cli.nombre1 = row("CLI_NOMBRE1")
                    cli.nombre2 = row("CLI_NOMBRE2")
                    cli.otronombre = If(Not IsDBNull(row("CLI_OTROSNOMBRES")), row("CLI_OTROSNOMBRES").ToString(), "")
                    'IsDBNull en caso retorne vacio 
                    cli.apellido1 = row("CLI_APELLIDO1").ToString()
                    cli.apellido2 = row("CLI_APELLIDO2")
                    clientes.Add(cli)
                Next
            Else
                Response.Write("no se encontro el cliente")

            End If

            Return View(clientes)
        End Function



        Function setNewCliente()
            Return View()
        End Function



        <HttpPost>
        Function setCliente(cliente As Cliente) As ActionResult
            'Conexion
            Dim cn As String = ConfigurationManager.ConnectionStrings("proyectdb").ConnectionString
            Dim connection As New OracleConnection(cn)
            connection.Open()

            If (connection.State = ConnectionState.Open) Then
                Dim connectivity = True
            End If
            'PROC
            Dim cmd As New OracleCommand("PROC_SET_CLIENTE", connection)
            cmd.CommandType = CommandType.StoredProcedure

            'Method
            cmd.Parameters.Add("CLI_IDCLIENTE", OracleDbType.Int32, ParameterDirection.Input).Value = 2
            cmd.Parameters.Add("CLI_NOMBRE1", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.nombre1
            cmd.Parameters.Add("CLI_NOMBRE2", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.nombre2
            cmd.Parameters.Add("CLI_OTROSNOMBRES", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.otronombre
            cmd.Parameters.Add("CLI_APELLIDO1", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.apellido1
            cmd.Parameters.Add("CLI_APELLIDO2", OracleDbType.Varchar2, ParameterDirection.Input).Value = "lastname2"
            cmd.Parameters.Add("CLI_APELLIDOCASADA", OracleDbType.Varchar2, ParameterDirection.Input).Value = "n/a"
            cmd.Parameters.Add("CLI_ESTADO", OracleDbType.Varchar2, ParameterDirection.Input).Value = 1
            cmd.Parameters.Add("CLI_PROFESION", OracleDbType.Varchar2, ParameterDirection.Input).Value = "developer"
            cmd.Parameters.Add("CLI_TIPO_PERSONA", OracleDbType.Varchar2, ParameterDirection.Input).Value = 2
            cmd.Parameters.Add("CLI_NIT", OracleDbType.Varchar2, ParameterDirection.Input).Value = 1453132

            cmd.ExecuteNonQuery()
            connection.Close()

            Return Redirect("~/Cliente")
        End Function

        Function setc()

            Dim cn As String = ConfigurationManager.ConnectionStrings("proyectdb").ConnectionString
            Dim connection As New OracleConnection(cn)
            connection.Open()

            If (connection.State = ConnectionState.Open) Then
                Dim connectivity = True
            End If

            Dim sql As String = "INSERT INTO apl_cliente (cli_idcliente, CLI_NOMBRE1,CLI_NOMBRE2,CLI_OTROSNOMBRES,CLI_APELLIDO1,CLI_APELLIDO2,CLI_APELLIDOCASADA,CLI_ESTADO,CLI_PROFESION,CLI_TIPO_PERSONA,CLI_NIT)" + "VALUES (:param1, :param2, :param3,:param4,: param5,:param6,:param7,:param8,:param9,:param10,:param11)"
            Using command As New OracleCommand(sql, connection)
                command.CommandType = CommandType.Text
                command.Parameters.Add(":param1", OracleDbType.Varchar2, 50).Value = 1
                command.Parameters.Add(":param2", OracleDbType.Varchar2, 50).Value = "name1" + ""
                command.Parameters.Add(":param3", OracleDbType.Varchar2, 50).Value = "name2"
                command.Parameters.Add(":param4", OracleDbType.Varchar2, 50).Value = "othername"
                command.Parameters.Add(":param5", OracleDbType.Varchar2, 50).Value = "lastname1"
                command.Parameters.Add(":param6", OracleDbType.Varchar2, 50).Value = "lastname2"
                command.Parameters.Add(":param7", OracleDbType.Varchar2, 50).Value = "n/a"
                command.Parameters.Add(":param8", OracleDbType.Varchar2, 50).Value = 1
                command.Parameters.Add(":param8", OracleDbType.Varchar2, 50).Value = "developer"
                command.Parameters.Add(":param8", OracleDbType.Varchar2, 50).Value = 2
                command.Parameters.Add(":param8", OracleDbType.Varchar2, 50).Value = 1453132

                command.ExecuteNonQuery()
            End Using


            Return View()
        End Function




        Function selectCliente(id As Int32) As ActionResult

            Dim cn As String = ConfigurationManager.ConnectionStrings("proyectdb").ConnectionString
            Dim connection As New OracleConnection(cn)
            connection.Open()


            If (connection.State = ConnectionState.Open) Then
                Dim connectivity = True
            End If

            Dim cliente = New Cliente()
            cliente.cli_num_doc = id

            Dim cmd As New OracleCommand("proc_get_Cliente", connection)
            cmd.CommandType = CommandType.StoredProcedure

            Dim salida As New OracleParameter("resultado", OracleDbType.RefCursor)
            salida.Direction = ParameterDirection.Output

            cmd.Parameters.Add(salida)
            cmd.Parameters.Add("CLI_NUMERO_DOCUMENTO", OracleDbType.Int32).Value = Cliente.cli_num_doc

            Using reader As OracleDataReader = cmd.ExecuteReader()
                If reader IsNot Nothing AndAlso reader.HasRows Then
                    reader.Read()
                    cliente.id_cliente = If(Not IsDBNull(reader("CLI_IDCLIENTE")), reader("CLI_IDCLIENTE").ToString(), "")
                    cliente.nombre1 = If(Not IsDBNull(reader("CLI_NOMBRE1")), reader("CLI_NOMBRE1").ToString(), "")
                    cliente.nombre2 = If(Not IsDBNull(reader("CLI_NOMBRE2")), reader("CLI_NOMBRE2").ToString(), "")
                    cliente.apellido1 = If(Not IsDBNull(reader("CLI_APELLIDO1")), reader("CLI_APELLIDO1").ToString(), "")
                    cliente.apellido2 = If(Not IsDBNull(reader("CLI_APELLIDO2")), reader("CLI_APELLIDO2").ToString(), "")
                    cliente.apellidoCasada = If(Not IsDBNull(reader("CLI_APELLIDOCASADA")), reader("CLI_APELLIDOCASADA").ToString(), "")
                    cliente.estado = If(Not IsDBNull(reader("CLI_ESTADO")), reader("CLI_ESTADO").ToString(), "")
                    cliente.profesion = If(Not IsDBNull(reader("CLI_PROFESION")), reader("CLI_PROFESION").ToString(), "")
                    cliente.tipopersona = If(Not IsDBNull(reader("CLI_TIPO_PERSONA")), reader("CLI_TIPO_PERSONA").ToString(), "")
                    cliente.nit = If(Not IsDBNull(reader("CLI_NIT")), reader("CLI_NIT").ToString(), "")

                Else
                    Response.Write("no se encontro el cliente")
                End If
            End Using
            Return View(cliente)

        End Function



        <HttpPost>
        Function updateCliente(cliente As Cliente)

            Dim cn As String = ConfigurationManager.ConnectionStrings("proyectdb").ConnectionString
            Dim connection As New OracleConnection(cn)
            connection.Open()

            Dim cmd = New OracleCommand("PROC_UPDATE_CLIENTES", connection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("CLI_NUMERO_DOCUMENTO", OracleDbType.Int32, ParameterDirection.Input).Value = cliente.cli_num_doc
            cmd.Parameters.Add("CLI_IDCLIENTE", OracleDbType.Int32, ParameterDirection.Input).Value = cliente.id_cliente
            cmd.Parameters.Add("CLI_NOMBRE1", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.nombre1
            cmd.Parameters.Add("CLI_NOMBRE2", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.nombre2
            cmd.Parameters.Add("p_CLI_OTROSNOMBRES", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.otronombre
            cmd.Parameters.Add("p_CLI_APELLIDO1", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.apellido1
            cmd.Parameters.Add("p_CLI_APELLIDO2", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.apellido2
            cmd.Parameters.Add("p_CLI_APELLIDOCASADA", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.apellidoCasada
            cmd.Parameters.Add("p_CLI_ESTADO", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.estado
            cmd.Parameters.Add("p_CLI_PROFESION", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.profesion
            cmd.Parameters.Add("p_CLI_TIPO_PERSONA", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.tipopersona
            cmd.Parameters.Add("p_CLI_NIT", OracleDbType.Varchar2, ParameterDirection.Input).Value = cliente.nit
            cmd.ExecuteNonQuery()
            connection.Close()

            Return Redirect("~/Cliente")
        End Function



        Function deleteCliente(id As Int32) As ActionResult
            Dim cn As String = ConfigurationManager.ConnectionStrings("proyectdb").ConnectionString
            Dim connection As New OracleConnection(cn)
            connection.Open()

            Dim cmd = New OracleCommand("PROC_DELETE_CLIENTE", connection)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("CLI_NUMERO_DOCUMENTO", OracleDbType.Int32, ParameterDirection.Input).Value = id
            cmd.Parameters.Add("p_CLI_ESTADO", OracleDbType.Varchar2, ParameterDirection.Input).Value = "0"

            cmd.ExecuteNonQuery()
            connection.Close()
            Return Redirect("~/Cliente")


        End Function


    End Class
End Namespace