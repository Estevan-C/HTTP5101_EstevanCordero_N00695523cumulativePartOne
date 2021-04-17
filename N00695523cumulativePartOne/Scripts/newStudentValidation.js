
window.onload = function () {

    // Call to Form
    var formHandle = document.forms.studentForm;

    // Call to Elements ID
    var sfirstNameError = document.getElementById("StudentFname");
    var slastNameError = document.getElementById("StudentLname");
    var sNumber = document.getElementById("StudentNumber");
    // var sEnrolDate = document.getElementById("StudentEnrolDate");

    // Call to Span for Error Messages
    var errorMsgFirstName = document.getElementById("errorMsgFirstName");
    var errorMsgLastName = document.getElementById("errorMsgLastName");
    var errorMsgNumber = document.getElementById("errorMsgNumber");
    // var errorMsgDate = document.getElementById("errorMsgDate");

    //Regex
    var studentNumberRegex = /\w\d\d\d\d/;
    // var studentEnrolDate = /^\d{4}-\d{2}-\d{2}$/;

    //Runs function on submit
    formHandle.onsubmit = validateForm;

    function validateForm() {

        // Grabs Values from the form.
        var firstNameValue = formHandle.StudentFname;
        var lastNameValue = formHandle.StudentLname;
        var StudentNumber = formHandle.StudentNumber;
        // var StudentDate = formHandle.StudentEnrolDate;


        //If statements to check
        if (firstNameValue.value === "" || firstNameValue.value === null) {
            sfirstNameError.style.backgroundColor = "red";
            firstNameValue.focus();
            errorMsgFirstName.innerHTML = "Please Enter A Name";
            return false;
        }
        if (lastNameValue.value === "" || lastNameValue.value === null) {
            slastNameError.style.backgroundColor = "red";
            lastNameValue.focus();
            errorMsgLastName.innerHTML = "Please Enter A Name";
            return false;
        }
        // Not sure why this doesn't work even though it shows a red indicator that its wrong?
        if (!studentNumberRegex.test(StudentNumber.value) || StudentNumber.value === "N" || StudentNumber.value === null) {
            sNumber.style.backgroundColor = "red";
            employeeNumValue.focus();
            errorMsgNumber.innerHTML = "Format Example: N0000";
            return false;
        }
       /* 
       if (!studentEnrolDate.test(StudentDate.value))
        {
            tHiredDate.style.backgroundColor = "red";
            hiredDateValue.focus();
            errorMsgDate.innerHTML = "Please Enter Select a Date";
            return false;
        }
        */
        //return false;
    }
}