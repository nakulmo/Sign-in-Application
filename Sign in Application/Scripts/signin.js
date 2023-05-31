function Checkusername() {
    var isName = /^[a-zA-Z]+$/;
    let nameinput = document.getElementById("Username")
    if (nameinput.value.trim() === "") {
        setErrorfor(nameinput, "Empty user name");
    }
    else if (!isName.test(nameinput.value.trim())) {
        setErrorfor(nameinput, 'User name cannot be a numbers or special characters');
    }
    else {
        setSuccessFor(nameinput);
    }
}

function Checkpassword() {
    let pwdinput = document.getElementById("Password")
    if (pwdinput.value.trim() === "") {
        setErrorfor(pwdinput, "Empty Password");
    }
    else {
        setSuccessFor(pwdinput);
    }
}


function setErrorfor(input, message) {

    let submitbutton = document.getElementById("button")
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = 'smallshown';
    small.innerText = message;
    submitbutton.disabled = true;
}

function setSuccessFor(input) {
    let submitbutton = document.getElementById("button")
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallhidden"
    small.innerHTML = ""
    submitbutton.disabled = false
}

function Checkvalidation() {
    Checkusername();
    Checkpassword();

}
