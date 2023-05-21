
        function F_AgregarProducto(id){
            var value = "#" + id;
        var obj = {
            id: $(value).parent().parent().parent().find('.CodProducto').val(),
        cantidad: $(value.replace("AgregarProducto", "txtCantidad")).val()
            }
        var stock = {
            stockcan: $(value).parent().parent().parent().find('.StockProducto').val()
            }
        $.ajax({
            type: "Post",
        url: ("Agregar","Carrito"),
        data: obj,
        success: function (data) {
            location.href = data;
                },
        error: function (obj) {
                    if (obj.cantidad==null)
        alert("Tienes que poner la cantidad.");
                    else if (obj.cantidad > stock.stockcan) {
            alert('El producto no tiene stock suficiente.');
                    }
                }
            });
        }
        function F_DetalleProducto(id) {
            var value = "#" + id;
        var id = $(value).parent().parent().parent().find('.CodProducto').val()
        location.href = "/Producto/ObtenerProducto/?productoId="+id;
        }

