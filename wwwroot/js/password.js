document.addEventListener('DOMContentLoaded', function () {
    const passwordField = document.getElementById('passwordField');
    const togglePasswordButton = document.getElementById('togglePassword');

    togglePasswordButton.addEventListener('click', function () {
        const type = passwordField.type === 'password' ? 'text' : 'password';
        passwordField.type = type;
    });
});