console.log("loginValidation.js");
const usernameButton = document.getElementById('UsernameButton');
const emailButton = document.getElementById('EmailButton');

const label = document.getElementById('LoginLabel');
const input = document.getElementById('LoginInput');
const span = document.getElementById('LoginSpan');
const form = document.getElementById('LoginForm');

function usernameLoginAccess() {
    console.log("usernameButton()")

    label.setAttribute('asp-for', 'Name');
    input.setAttribute('asp-for', 'Name');
    span.setAttribute('asp-validation-for', 'Name');
    form.setAttribute('asp-action', 'Login');
}

function emailLoginAccess() {
    console.log("emailButton()")

    label.setAttribute('asp-for', 'Email');
    input.setAttribute('asp-for', 'Email');
    span.setAttribute('asp-validation-for', 'Email');
    form.setAttribute('asp-action', 'LoginByEmail');
}
