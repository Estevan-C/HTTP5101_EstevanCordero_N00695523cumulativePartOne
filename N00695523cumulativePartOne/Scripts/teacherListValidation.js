window.onload = function () {

    // Get Form
    var formHandle = document.forms.formTeacher;


    //Get Element
    var nameError = document.getElementById("in_Name");
 


    // Form Validation
    formHandle.onsubmit = teacherValidation;

    function teacherValidation() {
        var nameValue = formHandle.SearchKey;
        /* console.log(nameValue.value); Test to see what value was enter or if value was passed */

        if (nameValue.value === "" || nameValue.value === null) {
            nameError.style.background = "red";
            nameValue.focus();
            return false;
        }
    }

}