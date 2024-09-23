function ConsultarIdentificacion() {

    let Cedula = $("#Cedula").val();

    if (Cedula.length >= 9) {
    $.ajax({
      type: 'GET',
        url: 'https://apis.gometa.org/cedulas/' + Cedula,
      dataType: 'json',
      success: function (data) {
        $("#Nombre").val(data.nombre);
      }
    });
  }
  else {
    $("#Nombre").val("");
  }
}