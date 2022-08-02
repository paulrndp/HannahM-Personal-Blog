// upload image and preview
$('.newbtn').bind("click", function () {
    $('#pic').click();
});

function previewFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#img-container')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}
function profilePreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#img-profile')
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}
//tags
const tagin = new Tagin(document.querySelector('.tagin'), {
    enter: true
}) 