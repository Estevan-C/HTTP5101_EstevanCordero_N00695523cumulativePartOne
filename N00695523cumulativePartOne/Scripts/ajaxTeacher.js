


function UpdateTeacher(TeacherId) {

    //Sending an Update request with the use of xmlrequest

    var URL = "https://localhost:44392/api/TeacherData/UpdateTeacher/"+TeacherId;
    // So whats happening is that when I used Route for the TeacherData its able to find the path,
    // Instead I get a new error in where no content is being passed.
    // See Images for more understanding.

    var rq = new XMLHttpRequest();
    // The request placed in a var.

    //Grabbing the elements with the values from the web page.

    var TeacherFname = document.getElementById('TeacherFname').value;
    var TeacherLname = document.getElementById('TeacherLname').value;
    var TeacherEmployeeNumber = document.getElementById('TeacherEmployeeNumber').value;
    var TeacherHireDate = document.getElementById('TeacherHireDate').value;
    var TeacherSalary = document.getElementById('TeacherSalary').value;

    //Object to hold the data
    var TeacherData = {
        "TeacherFname": TeacherFname,
        "TeacherLname": TeacherLname,
        "TeacherEmployeeNumber": TeacherEmployeeNumber,
        "TeacherHireDate": TeacherHireDate,
        "TeacherSalary": TeacherSalary
    };


    rq.open("POST", URL, true);
    rq.setRequestHeader("Content-Type", "application/json");
    rq.onreadystatechange = function () {
      
        if (rq.readyState == 4 && rq.status == 200) {
        }

    }

    rq.send(JSON.stringify(TeacherData));
}