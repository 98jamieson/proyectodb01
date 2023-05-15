@ModelType  List(Of Cliente)
@Code
    ViewData("Title") = "Index"

End Code

<h2>Index</h2>


<div href="row">
    <a href="~/Cliente/setNewCliente" class="btn btn-success">Nuevo</a>
</div>
<table class="table">
    <thead>
        <tr>
            <th>documento</th>
            <th>nombre</th>
            <th>nombre2</th>
            <th>otro nombre</th>
            <th>apellido</th>
            <th>apellido 2</th>
        </tr>
    </thead>
    <tbody>

        @For Each item In Model
            @<tr>

                <td>
                    @item.cli_num_doc
                </td>
                <td>
                    @item.nombre1
                </td>
                <td>
                    @item.nombre2

                </td>
                <td>
                    @item.otronombre
                </td>
                <td>
                    @item.apellido1
                </td>

                <td>
                    @item.apellido2
                </td>

                <td>
                    <a class="btn btn-primary" href="Cliente/selectCliente/@item.cli_num_doc">Editar</a>
                </td>
                <td>
                    <a class="btn btn-danger" href="Cliente/deleteCliente/@item.cli_num_doc">Eliminar</a>
                </td>
            </tr>
        Next

    </tbody>
</table>
