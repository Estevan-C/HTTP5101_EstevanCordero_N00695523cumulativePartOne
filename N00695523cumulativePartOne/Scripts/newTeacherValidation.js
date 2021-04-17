
window.onload = function () {

    // Call to Form
    var formHandle = document.forms.teacherForm;

    // Call to Elements ID
    var tfirstNameError = document.getElementById("TeacherFname");
    var tlastNameError = document.getElementById("TeacherLname");
    var temployeeNumber = document.getElementById("TeacherEmployeeNumber");
    var tHiredDate = document.getElementById("TeacherHireDate");
   // var tSalary = document.getElementById("TeacherSalary");


    // Call to Span for Error Messages
    var errorMsgFirstName = document.getElementById("errorMsgFirstName");
    var errorMsgLastName = document.getElementById("errorMsgLastName");
    var errorMsgNumber = document.getElementById("errorMsgNumber");
    var errorMsgDate = document.getElementById("errorMsgDate");
   // var errorMsgSalary = document.getElementById("errorMsgSalary");

    //Regex
    var employeeNumberRegex = /\w\d\d\d/ ;
    var hiredDateRegex = /^\d{4}-\d{2}-\d{2}$/;
    //var salaryRegex = /^\d{2}.\d{2}$/;
    // Couldn't figure out how to write an regex for a decimal number.

    //Runs function when submit
    formHandle.onsubmit = validateForm;

    function validateForm() {

        // Grabs Values from the form
        var firstNameValue = formHandle.TeacherFname;
        var lastNameValue = formHandle.TeacherLname;
        var employeeNumValue = formHandle.TeacherEmployeeNumber;
        var hiredDateValue = formHandle.TeacherHireDate;
        //var salaryValue = formHandle.TeacherSalary;


        //If statements to check 
        if (firstNameValue.value === "" || firstNameValue.value === null)
        {
            tfirstNameError.style.backgroundColor = "red";
            firstNameValue.focus();
            errorMsgFirstName.innerHTML = "Please Enter A Name";
            return false;
        }
        if (lastNameValue.value === "" || lastNameValue.value === null) {
            tlastNameError.style.backgroundColor = "red";
            lastNameValue.focus();
            errorMsgLastName.innerHTML = "Please Enter A Name";
            return false;
        }
        if (!employeeNumberRegex.test(employeeNumValue.value))
        {
            temployeeNumber.style.backgroundColor = "red";
            employeeNumValue.focus();
            errorMsgNumber.innerHTML = "Format Example: T000";
            return false;
        }
        if (!hiredDateRegex.test(hiredDateValue.value))
        {
            tHiredDate.style.backgroundColor = "red";
            hiredDateValue.focus();
            errorMsgDate.innerHTML = "Please Enter Select a Date";
            return false;
        }
       /* if (!salaryRegex.test(salaryValue.value))
        {
            tSalary.style.backgroundColor = "red";
            salaryValue.focus();
            errorMsg.innerHTML = "Format Example: 00.00";
            return false;
        }*/
        //return false;
        
    }
}