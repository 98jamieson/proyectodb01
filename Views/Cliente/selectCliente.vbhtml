@ModelType  Cliente
@Code
    ViewData("Title") = "selectCliente"
End Code


<h1>MODICAR CLIENTE</h1>
<h2>cliente a modificar: </h2>
<h3>@Model.nombre1</h3>


@Using Html.BeginForm("updateCliente", "Cliente", method:=FormMethod.Post)
    @<div>
    @Html.HiddenFor(Function(m) m.cli_num_doc)
    @Html.HiddenFor(Function(m) m.id_cliente)
    @Html.HiddenFor(Function(m) m.otronombre)

    <label>Primer Nombre</label>
    @Html.EditorFor(Function(m) m.nombre1, New With {.htmlAttributes = New With {.class = "form-control"}})
    @Html.ValidationMessageFor(Function(m) m.nombre1, "", New With {.class = "text-danger"})
</div>
    @<div>
        <label>Segundo Nombre</label>
        @Html.EditorFor(Function(m) m.nombre2, New With {.htmlAttributes = New With {.class = "form-control"}})
        @Html.ValidationMessageFor(Function(m) m.nombre2, "", New With {.class = "text-danger"})
    </div>

    @<div>
        <label>Primer Apellido</label>
        @Html.EditorFor(Function(m) m.apellido1, New With {.htmlAttributes = New With {.class = "form-control"}})
        @Html.ValidationMessageFor(Function(m) m.apellido1, "", New With {.class = "text-danger"})
    </div>

    @<div>
        <input type="submit" value="Submit" />
    </div>
End Using
