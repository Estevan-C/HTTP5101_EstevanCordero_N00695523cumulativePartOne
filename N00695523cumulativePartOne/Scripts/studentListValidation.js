
/*JS used to test if the user has enter an empty string or null in the search bar */
window.onload = function () {

    // Get Form
    var formHandle = document.forms.formStudent;

    // Get Elements
    var nameError = document.getElementById("in_Name");
   

    // Get Form Value
    formHandle.onsubmit = studentValidation;

    function studentValidation() {
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