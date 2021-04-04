
/*JS used to test if the user has enter an empty string or null in the search bar */
window.onload = function () {

    var formHandle = document.forms.formStudent;

    var nameError = document.getElementById("in_Name");

    formHandle.onsubmit = proccessForm;

    function proccessForm() {
        var nameValue = formHandle.SearchKey;
        /* console.log(nameValue.value); Test to see what value was enter or if value was passed */

        if (nameValue.value === "" || nameValue.value === null)
        {
            nameError.style.background = "red";
            nameValue.focus();
            return false;
        }
    }

}