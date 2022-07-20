$(() => {
    //$("form-control").on('keyup', function () {
    //    ensureFormValidity();
    //});

    //function ensureFormValidity() {
    //    const isValid = isFormValid();
    //    $("#submit").prop('disabled', !isValid);
    //}
    //function isFormValid() {
    //    const name = $("#name").val();
    //    const content = $("#content").val();
    //    return name && content;
    //}
    $("#content, #name").on('keyup', () => {
        const isValid = $("#content").val() && $("#name").val();
        $("#submit").prop('disabled', !isValid);
    })
});