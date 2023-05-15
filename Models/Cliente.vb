Imports System.ComponentModel.DataAnnotations

Public Class Cliente
    Public Property id_cliente As Integer
    Public Property cli_num_doc As Integer
    <Required(ErrorMessage:="Ingrese primer nombre")>
    <DataType(DataType.Text)>
    Public Property nombre1 As String

    <Required(ErrorMessage:="Ingrese segundo nombre")>
    <DataType(DataType.Text)>
    Public Property nombre2 As String
    Public Property otronombre As String

    <Required(ErrorMessage:="Ingrese primer apellido")>
    <DataType(DataType.Text)>
    Public Property apellido1 As String

    <Required(ErrorMessage:="Ingrese segundo apellido")>
    <DataType(DataType.Text)>
    Public Property apellido2 As String
    Public Property apellidoCasada As String

    Public Property estado As String
    Public Property profesion As String
    Public Property tipopersona As String
    Public Property nit As String

End Class
