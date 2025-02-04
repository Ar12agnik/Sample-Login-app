console.log('script1.js');
var errors = 0
const email_validator = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
const paswd_validator = /^(?=.{8,})(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?":{}|<>]).*$/g
const n_validator = /^[a-zA-Z]+( [a-zA-Z]+)*$/
const mobileNo_ckh = /^\d{10}$/;
const alert_con = document.getElementById("AGNIK");
function try_login() {
    if (errors > 0) {
        alert(`${errors} no of errors found!`)
        return;
    }
    else {
        const xhr = new XMLHttpRequest();
        xhr.open('POST', '/dashboard/Auth', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onload = function () {
            json = JSON.parse(this.responseText);
            if (json['code'] === 200) {
                window.location.href = `/dashboard/${json['redirect']}`
            }
            else {
                show_alert(this.responseText["message"])
            }
        }
        xhr.send(JSON.stringify({
            username: document.getElementById('floatingInput').value,
            password: document.getElementById('floatingPassword').value
        }));
    }
}
function try_signup() {
    if (errors > 0) {
        alert(`${errors}  errors found! Please Fix them to submit!!`)
        return;
    }
    const xhr = new XMLHttpRequest();
    xhr.open('POST', "https://localhost:44340/dashboard/createuser", true)
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function () {
        json = JSON.parse(this.responseText);
        if (json['code'] == 200) {
            window.location.href = `/dashboard/${json['redirect_url']}`
        }
        else {
            alert(json['message']);
        }
    }
    let FirstName = document.getElementById('floatingFirstName').value
    let MiddleName = document.getElementById('floatingMiddleName').value
    let LastName = document.getElementById('floatingLastName').value
    let email = document.getElementById('floatingEmail').value
    let Username = document.getElementById('floatingUsername').value
    let Password1 = document.getElementById('floatingPassword1').value
    let Password2 = document.getElementById('floatingPassword2').value
    let phone = parseInt(document.getElementById('floatingPhone').value)
    let address = document.getElementById('floatingAddress').value
    if (Password1 === Password2) {
        json_obj = {
            Username: Username,
            _Credencials: {
                Email: email,
                Password: Password1,

            },
            _Detailes: {
            First_Name: FirstName,
            Middle_name: MiddleName,
            Last_Name: LastName,
            Address: address,
            Phone: phone
            }

        }
        xhr.send(JSON.stringify(json_obj))
    }
    else {
        alert("Passwords donot match")
    }

}


//validator code



function show_alert(message) {
    console.log(alert_con)
    alert_con.classList.remove('hide');
    alert_con.classList.add('show');
    alert_con.innerHTML = message;
    return false;
}
function hide_alert() {
    alert_con.classList.remove('show');
    alert_con.classList.add('hide');
}
function validate_email() {
    email = document.getElementById("floatingEmail").value
    if (!email_validator.test(email)) {
        display_error("floatingEmail", "Invalid Email Address!!")
    }
    else {
        remove_error("floatingEmail")
    }
}
function validate_password1() {
    password = document.getElementById("floatingPassword1").value
    if (!paswd_validator.test(password)) {
        display_error("floatingPassword1", "Password Should Contain at least one lowercase letter, uppercase letter, number, and a minimum length of 8 characters")
    }
    else {
        remove_error("floatingPassword1")
    }
}
function validate_phone_number() {
    Phone = document.getElementById("floatingPhone").value
    if (!mobileNo_ckh.test(Phone)) {
        display_error("floatingPhone", "Invalid Phone Number")
    }
    else {
        remove_error("floatingPhone")
    }

}
function validate_password2() {
    password = document.getElementById("floatingPassword2").value
    if (!paswd_validator.test(password)) {
        display_error("floatingPassword2", "Password Should Contain at least one lowercase letter, uppercase letter, number, and a minimum length of 8 characters")
    }
    else {
        remove_error("floatingPassword2")
    }
}
//function validate_name() {
//    name_ = document.getElementById("name").value
//    if (!n_validator.test(name_)) {
//        display_error("name", "Invalid Name")
//    } else {
//        remove_error('name')
//    }

//}
function display_error(id, message) {
    errors = errors+  1
    console.log(errors)
    
    element = document.getElementById(id)
    element.classList.add("is-invalid")
    const newelement = document.createElement("div")
    newelement.setAttribute('id', 'validationServer04Feedback')
    newelement.classList.add("invalid-feedback")
    newelement.textContent = message
    element.parentNode.appendChild(newelement)
}
function remove_error(id) {
    try {
        errors =errors - 1

        let element = document.getElementById(id);
        if (!element) throw new Error("Element not found");

        element.classList.remove("is-invalid");
        element.classList.add("is-valid");

        let errorElement = document.getElementById(id + "-feedback");
        if (errorElement) {
            errorElement.remove();
        }
    } catch (error) {
        console.error("remove_error: ", error.message);
    }
}
