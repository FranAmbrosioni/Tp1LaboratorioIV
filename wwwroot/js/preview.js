$("#seleccionImg").change(function () {
    var fileName = this.files[0].name;
    var fileSize = this.files[0].size;
    var mjError = '';

    if (fileSize > 3000000) {

        mjError = 'el archivo no puede superar los 3MB ';
    } else {

        var ext = fileName.split('.').pop();


        ext = ext.toLowerCase();

        switch (ext) {
            case 'jpg':
            case 'jpeg':
            case 'png':
                break;
            default:
                mjerror = 'El archivo no tiene la extensión adecuada';
        }
    }

    if (mjError == '') {
        readURL(this);
    } else {
        alert(mjError);
    }


});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#imagen").attr("src", e.target.result);
        }

        reader.readAsDataURL(input.files[0]); // leer el archivo string base64
    }
}
