setTimeout(function () {
    var elements = document.getElementsByClassName("alertMessage");
    for (var i = 0; i < elements.length; i++) {
        elements[i].remove();
    }
}, 5000);

function CloseAlert(element) {
    element.remove();
}