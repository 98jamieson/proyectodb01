
@ModelType  Cliente

@Code


    ViewData("Title") = "setNewCliente"
End Code





<h2>Ingrese un Nuevo Cliente</h2>


@Using Html.BeginForm("setCliente", "Cliente")
    @<div>
        <label>Primer Nombre</label>
        @Html.TextBoxFor(Function(m) m.nombre1)
    @Html.ValidationMessageFor(Function(m) m.nombre1, "", New With {.class = "text-danger"})

                                                                                              )
    </div>
    @<div>
    <label>Segundo Nombre</label>
    @Html.TextBoxFor(Function(m) m.nombre2)
    @Html.ValidationMessageFor(Function(m) m.nombre2, "", New With {.class = "text-danger"})

    
</div>

    @<div>
    <div>
    </div>
    <label>Primer Apellido</label>
    @Html.TextBoxFor(Function(m) m.apellido1)
    @Html.ValidationMessageFor(Function(m) m.apellido1, "", New With {.class = "text-danger"})

</div>

    @<div>
        <input type="submit" value="Submit" />
    </div>
End Using