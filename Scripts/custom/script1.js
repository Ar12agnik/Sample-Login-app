console.log('script1.js');
function try_login() {
    const xhr = new XMLHttpRequest();
    xhr.open('POST', '/dashboard/Auth', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function () {
        json = JSON.parse(this.responseText);
        if (json['code'] === 200){
            window.location.href = `/dashboard/${json['redirect']}`
        }
        else {
            console.log(this.responseText)
        }
    }
    xhr.send(JSON.stringify({
        username: document.getElementById('floatingInput').value,
        password: document.getElementById('floatingPassword').value
    }));
}
function try_signup() {
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