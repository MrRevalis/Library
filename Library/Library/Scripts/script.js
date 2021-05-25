setTimeout(function () {
    var elements = document.getElementsByClassName("alertMessage");
    for (var i = 0; i < elements.length; i++) {
        elements[i].remove();
    }
}, 5000);

function CloseAlert(element) {
    element.remove();
}


function ConfirmDelete(elementID, toDelete) {
    var deleteSpan = "delete_" + elementID;
    var confirmSpan = "confirm_" + elementID;

    if (toDelete) {
        document.getElementById(deleteSpan).style.display = "none";
        document.getElementById(confirmSpan).style.display = "";
    }
    else {
        document.getElementById(deleteSpan).style.display = "";
        document.getElementById(confirmSpan).style.display = "none";
    }

}